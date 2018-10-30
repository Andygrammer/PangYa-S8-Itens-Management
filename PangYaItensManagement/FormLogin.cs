using System;
using System.Windows.Forms;
using PangYaItensManagement.DAO;
using PangYaItensManagement.Model;
using System.Reflection;
using System.Threading;
using System.Drawing;
using System.Collections.Generic;

namespace PangYaItensManagement
{
    public partial class Form_Login : Form
    {
        User user = null;
        UserDAO userDAO = null;
        static bool isDatabaseConnected = false;

        public Form_Login()
        {
            InitializeComponent();
        }

        private void Form_Login_Shown(object sender, EventArgs e)
        {
            Login();

            Thread threadLogin = new Thread(new ThreadStart(this.AssynchronousDatabaseStatus))
            {
                IsBackground = true
            };
            threadLogin.Start();
        }

        /// <summary>
        /// Log into server database
        /// </summary>
        private void Login()
        {
            user = new User();
            userDAO = new UserDAO();

            if (userDAO.IsConnected() != null)
            {
                isDatabaseConnected = true;

                bool userExists = userDAO.CheckUserExists(user);

                if (userExists)
                {
                    this.Hide();
                    Form formItemsMain = new Form_main(user);
                    formItemsMain.Closed += (s, args) => this.Close();
                    formItemsMain.Show();
                }

                else
                {
                    List<User> usersList = new List<User>();

                    usersList = userDAO.GetAllUsers();

                    listBox_available_logins.Invoke((MethodInvoker)(() => listBox_available_logins.Items.Clear()));

                    if (usersList != null)
                    {
                        foreach (User user in usersList)
                        {
                            listBox_available_logins.Invoke((MethodInvoker)(() => listBox_available_logins.Items.Add(user.Id + " |  " + user.Nickname)));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Check whether database server is on
        /// </summary>
        private void AssynchronousDatabaseStatus()
        {
            userDAO = new UserDAO();

            //if (userDAO.IsConnected() != null)
            if (isDatabaseConnected)
            {
                SetControlPropertyValue(lbl_server_database_status, "Text", "Database on");
                SetControlPropertyValue(lbl_server_database_status, "ForeColor", Color.Green);
            }
        }

        /// <summary>
        /// Log into server database by click on Login button
        /// </summary>
        private void LoginByClick()
        {
            string selectedUser = listBox_available_logins.GetItemText(listBox_available_logins.SelectedItem);
            string currentUserID = String.Empty;
            string currentUserNickName = String.Empty;

            if (selectedUser != String.Empty)
            {
                currentUserID = (selectedUser.Split(' ')[0]).Trim();
                currentUserNickName = (selectedUser.Split(' ')[3]).Trim();

                userDAO = new UserDAO();
                user.Id = Convert.ToInt32(currentUserID);
                user.Nickname = currentUserNickName;
                bool userExists = false;

                if (userDAO.IsConnected() != null)
                {
                    userExists = userDAO.CheckUserExistsFromLoginButton(user);
                }

                if (userExists)
                {
                    this.Hide();
                    Form formItemsMain = new Form_main(user);
                    formItemsMain.Closed += (s, args) => this.Close();
                    formItemsMain.Show();
                }
            }

            else
            {
                MessageBox.Show("Please, select one account to login", "Ops!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void Btn_connect_database_Click(object sender, EventArgs e)
        {
            AssynchronousDatabaseStatus();
        }

        private void Btn_login_Click(object sender, EventArgs e)
        {
            LoginByClick();
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
    }
}
