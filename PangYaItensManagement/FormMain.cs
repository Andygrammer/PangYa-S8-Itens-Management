using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using PangYaItensManagement.DAO;
using PangYaItensManagement.Model;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace PangYaItensManagement
{
    public partial class Form_main : Form
    {
        private User user = null;
        private Card card = null;
        private static List<Card> cardsList = new List<Card>();
        private static int cardQuantity;

        public Form_main() { }

        public Form_main(User user)
        {
            InitializeComponent();
            this.user = user;
        }

        private void Form_main_Load(object sender, EventArgs e)
        {
            GetCardInfoToRegisterOnDatabaseOnLoad();

            Thread thread = new Thread(new ThreadStart(this.AssynchronousLoadUserInfo))
            {
                IsBackground = true
            };
            thread.Start();
        }

        /// <summary>
        /// Load user info corresponding to database info
        /// </summary>
        private void AssynchronousLoadUserInfo()
        {
            panel_big_card.BackgroundImage = PangYaItensManagement.Properties.Resources.default_card;

            UserDAO userDAO = new UserDAO();
            string userNickName = user.Nickname;

            SetControlPropertyValue(lbl_user, "Text", userNickName);

            if (userNickName.Equals("..."))
            {
                SetControlPropertyValue(lbl_status, "Text", "Fail");
            }
            else
            {
                SetControlPropertyValue(lbl_status, "Text", "Connected");
                SetControlPropertyValue(lbl_status, "ForeColor", Color.Green);
            }

            this.Invoke((MethodInvoker)delegate ()
            {
                TabPage page = tabControl_card_vol.SelectedTab;
                Control.ControlCollection col = page.Controls;

                List<Control> listOfCardButtons = GetCardButtons();
                Control[] arrayOfCardButtons = listOfCardButtons.ToArray();

                string albumPage = lbl_card_album_page.Text.Split(' ')[1].Substring(0, 1);

                ListCards(listOfCardButtons, Convert.ToInt32(albumPage) - 1);
            });
        }

        #region delegate for multithreading windows forms
        /// <summary>
        /// Delegate to change Windows Forms components with multithreading
        /// </summary>
        /// <param name="oControl"></param>
        /// <param name="propName"></param>
        /// <param name="propValue"></param>
        delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        private void SetControlPropertyValue(Control oControl, string propName, object propValue)
        {
            if (oControl.InvokeRequired)
            {
                SetControlValueCallback d = new SetControlValueCallback(SetControlPropertyValue);
                oControl.Invoke(d, new object[] { oControl, propName, propValue });
            }
            else
            {
                Type t = oControl.GetType();

                PropertyInfo[] props = t.GetProperties();

                foreach (PropertyInfo p in props)

                {
                    if (p.Name.ToUpper() == propName.ToUpper())
                    {
                        p.SetValue(oControl, propValue, null);
                    }

                }

            }
        }

        #endregion delegate for multithreading windows forms

        public static void ListCards(List<Control> listOfCardButtons, int page)
        {
            CardDAO cardDAO = new CardDAO();
            cardsList = cardDAO.GetAllCards();
            Control[] arrayOfCardButtons = listOfCardButtons.ToArray();

            int buttonCount = 0;
            int cardButtons = arrayOfCardButtons.Length - 2;

            //Clear album cards before fill up
            foreach (Button button in arrayOfCardButtons)
            {
                button.Image = null;
            }

            MemoryStream memoryStream = new MemoryStream();

            foreach (Button button in arrayOfCardButtons)
            {
                if (listOfCardButtons[buttonCount].Name.Contains("card") && buttonCount < cardButtons && page * cardButtons + buttonCount < cardsList.Count)
                {
                    memoryStream = new MemoryStream(cardsList[page * cardButtons + buttonCount].ImageBytes);
                    button.Image = new Bitmap(memoryStream);
                    buttonCount++;
                    memoryStream.Close();
                }
            }
        }

        private void Btn_next_album_page_Click(object sender, EventArgs e)
        {
            int albumPage = Convert.ToInt32(lbl_card_album_page.Text.Split(' ')[1].Substring(0, 1));

            List<Control> listOfCardButtons = GetCardButtons();

            switch (albumPage)
            {
                case 1:
                    lbl_card_album_page.Text = "Page 2/3";
                    btn_previous_album_page.Enabled = true;
                    ListCards(listOfCardButtons, 1);
                    break;
                case 2:
                    lbl_card_album_page.Text = "Page 3/3";
                    btn_previous_album_page.Enabled = true;
                    btn_next_album_page.Enabled = false;
                    ListCards(listOfCardButtons, 2);
                    break;
            }
        }

        private List<Control> GetCardButtons()
        {
            return this.flowLayoutPanel1.Controls.OfType<Button>().Cast<Control>().ToList();
        }

        private void Btn_previous_album_page_Click(object sender, EventArgs e)
        {
            int albumPage = Convert.ToInt32(lbl_card_album_page.Text.Split(' ')[1].Substring(0, 1));

            List<Control> listOfCardButtons = GetCardButtons();

            switch (albumPage)
            {
                case 2:
                    lbl_card_album_page.Text = "Page 1/3";
                    btn_previous_album_page.Enabled = false;
                    btn_next_album_page.Enabled = true;
                    ListCards(listOfCardButtons, 0);
                    break;
                case 3:
                    lbl_card_album_page.Text = "Page 2/3";
                    btn_previous_album_page.Enabled = true;
                    btn_next_album_page.Enabled = true;
                    ListCards(listOfCardButtons, 1);
                    break;
            }
        }

        private void GetCardClickedInfo(int buttonIndex)
        {
            List<Card> listOfCards = new List<Card>();
            listOfCards = cardsList;
            int currentAlbumPage = Convert.ToInt32(lbl_card_album_page.Text.Substring(5, 1));
            int pageOffset = 0;

            switch (currentAlbumPage)
            {
                case 1:
                    pageOffset = 0;
                    break;
                case 2:
                    pageOffset = 15;
                    break;
                case 3:
                    pageOffset = 30;
                    break;
                case 4:
                    pageOffset = 45;
                    break;
            }

            int cardID = listOfCards[buttonIndex - 1 + pageOffset].ID;
            string cardName = listOfCards[buttonIndex - 1 + pageOffset].Name;
            string cardType = listOfCards[buttonIndex - 1 + pageOffset].Type;

            Card result = listOfCards.First(s => s.ID == cardID);

            SetCardBigImageAndInfo(result.ImageBigBytes, cardName, cardType, cardID);
        }

        private void SetCardBigImageAndInfo(byte[] imageBigBytes, string cardName, string cardType, int cardID)
        {
            using (MemoryStream memoryStream = new MemoryStream(imageBigBytes))
            {
                panel_big_card.BackgroundImage = new Bitmap(memoryStream);
            }

            lbl_card_name2.Text = cardName;
            lbl_card_type2.Text = cardType;

            int currentUserID = user.Id;
            CardDAO cardDAO = new CardDAO();

            int cardQuantity = cardDAO.GetSpecificCardQuantity(currentUserID, cardID);
            txt_quantity.Text = cardQuantity.ToString();
        }

        #region Click events for cards buttons
        private void Btn_card1_Click(object sender, EventArgs e)
        {
            GetCardClickedInfo(1);
        }

        private void Btn_card2_Click(object sender, EventArgs e)
        {
            GetCardClickedInfo(2);
        }

        private void Btn_card3_Click(object sender, EventArgs e)
        {
            GetCardClickedInfo(3);
        }

        private void Btn_card4_Click(object sender, EventArgs e)
        {
            GetCardClickedInfo(4);
        }

        private void Btn_card5_Click(object sender, EventArgs e)
        {
            GetCardClickedInfo(5);
        }

        private void Btn_card6_Click(object sender, EventArgs e)
        {
            GetCardClickedInfo(6);
        }

        private void Btn_card7_Click(object sender, EventArgs e)
        {
            GetCardClickedInfo(7);
        }

        private void Btn_card8_Click(object sender, EventArgs e)
        {
            GetCardClickedInfo(8);
        }

        private void Btn_card9_Click(object sender, EventArgs e)
        {
            GetCardClickedInfo(9);
        }

        private void Btn_card10_Click(object sender, EventArgs e)
        {
            GetCardClickedInfo(10);
        }

        private void Btn_card11_Click(object sender, EventArgs e)
        {
            GetCardClickedInfo(11);
        }

        private void Btn_card12_Click(object sender, EventArgs e)
        {
            GetCardClickedInfo(12);
        }

        private void Btn_card13_Click(object sender, EventArgs e)
        {
            GetCardClickedInfo(13);
        }

        private void Btn_card14_Click(object sender, EventArgs e)
        {
            GetCardClickedInfo(14);
        }

        private void Btn_card15_Click(object sender, EventArgs e)
        {
            GetCardClickedInfo(15);
        }

        #endregion Click events for cards buttons

        private void Txt_quantity_Click(object sender, EventArgs e)
        {
            txt_quantity.SelectAll();
        }

        private void Btn_set_card_Click(object sender, EventArgs e)
        {
            GetCardInfoToRegisterOnDatabase();

        }
        private void GetCardInfoToRegisterOnDatabase()
        {
            CardDAO cardDAO = new CardDAO();
            List<Card> listOfCards = new List<Card>();
            listOfCards = cardDAO.GetAllCards();

            Card result = listOfCards.First(s => s.Name == lbl_card_name2.Text);
            card = new Card
            {
                ID = result.ID,
                Type = result.Type
            };
            cardQuantity = Convert.ToInt32(txt_quantity.Text);
            
            cardDAO.SetSpecificCardQuantity(user.Id, card.ID, cardQuantity, card.Type);
        }

        private void GetCardInfoToRegisterOnDatabaseOnLoad()
        {
            CardDAO cardDAO = new CardDAO();
            List<Card> listOfCards = new List<Card>();
            listOfCards = cardDAO.GetAllCards();

            Card result = listOfCards.First(s => s.Name == lbl_card_name2.Text);
            card = new Card
            {
                ID = result.ID,
                Type = result.Type
            };
            cardQuantity = cardDAO.GetSpecificCardQuantity(user.Id, card.ID);
            SetControlPropertyValue(txt_quantity, "Text", cardQuantity.ToString());
            cardDAO.SetSpecificCardQuantity(user.Id, card.ID, cardQuantity, card.Type);
        }
    }
}
