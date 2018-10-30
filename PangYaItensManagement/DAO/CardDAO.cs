using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using PangYaItensManagement.Model;
using PangYaItensManagement.Properties;

namespace PangYaItensManagement.DAO
{
    public class CardDAO : Connection
    {
        /// <summary>
        /// Check whether server database is open
        /// </summary>
        /// <returns>The connection to MySQL database</returns>
        public MySqlConnection IsConnected()
        {
            return Connect(Properties.Resources.ResourceManager.GetString("UID"),
                    Properties.Resources.ResourceManager.GetString("password"),
                    Properties.Resources.ResourceManager.GetString("databaseServer"),
                    Properties.Resources.ResourceManager.GetString("databaseName"));
        }

        /// <summary>
        /// Select all cards from database
        /// </summary>
        /// <returns>A list of PangYa S8 cards</returns>
        public List<Card> GetAllCards()
        {
            List<Card> cardsList = new List<Card>();
            int cardID;
            int cardPackID;
            string cardType;
            string cardName = String.Empty;

            #region Connect to database
            IsConnected();
            #endregion Connect to database

            using (mySqlConnection)
            {
                try
                {
                    using (MySqlCommand mySqlCommand = new MySqlCommand(Resources.ResourceManager.GetString("procGetAllCards"), mySqlConnection))
                    {
                        mySqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                        using (MySqlDataReader reader = mySqlCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                cardID = Convert.ToInt32(String.Format("{0}", reader["TypeID_Card"]));
                                cardPackID = Convert.ToInt32(String.Format("{0}", reader["TypeID_Pack"]));
                                cardType = String.Format("{0}", reader["Tipo"]);
                                cardName = String.Format("{0}", reader["Nome"]);
                                byte[] imageBytes = (byte[])reader["Image"];
                                byte[] imageBigBytes = (byte[])reader["Image_Big"];

                                Card card = new Card
                                {
                                    ID = cardID,
                                    CardPackID = cardPackID,
                                    Type = cardType,
                                    Name = cardName,
                                    ImageBytes = imageBytes,
                                    ImageBigBytes = imageBigBytes
                                };
                                cardsList.Add(card);
                            }
                        }
                    }
                }
                catch (Exception exception)
                {
                    string message = "\n\nPlease, check whether your server database has the procedure: \n\n" +
                        Resources.ResourceManager.GetString("procGetAllCards");
                    MessageBox.Show(exception.Message + message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }

            return cardsList;
        }

        /// <summary>
        /// Select quantity of a specific card owned by the player
        /// </summary>
        /// <returns>The quantity of a specific card owned by the player </returns>
        public int GetSpecificCardQuantity(int userID, int cardID)
        {
            int cardQuantity;

            #region Connect to database
            IsConnected();
            #endregion Connect to database

            using (mySqlConnection)
            {
                try
                {
                    using (MySqlCommand mySqlCommand = new MySqlCommand(Resources.ResourceManager.GetString("procGetSpecificCardQuantity"), mySqlConnection))
                    {
                        mySqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        mySqlCommand.Parameters.AddWithValue("@USERID", userID);
                        mySqlCommand.Parameters.AddWithValue("@CARDID", cardID);

                        using (MySqlDataReader reader = mySqlCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                cardQuantity = Convert.ToInt32(String.Format("{0}", reader["QNTD"]));
                            }
                            else
                            {
                                return 0;
                            }
                        }
                    }
                }
                catch (Exception exception)
                {
                    string message = "\n\nPlease, check whether your server database has the procedure: \n\n" +
                        Resources.ResourceManager.GetString("procGetSpecificCardQuantity");
                    MessageBox.Show(exception.Message + message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 0;
                }
            }

            return cardQuantity;
        }

        /// <summary>
        /// Set a new quantity of a specific card for the player
        /// </summary>
        public void SetSpecificCardQuantity(int userID, int cardID, int cardQuantity, string cardType)
        {
            #region Connect to database
            IsConnected();
            #endregion Connect to database

            using (mySqlConnection)
            {
                try
                {
                    using (MySqlCommand mySqlCommand = new MySqlCommand(Resources.ResourceManager.GetString("procSetSpecificCardQuantity"), mySqlConnection))
                    {
                        mySqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        mySqlCommand.Parameters.AddWithValue("@USERID", userID);
                        mySqlCommand.Parameters.AddWithValue("@CARDID", cardID);
                        mySqlCommand.Parameters.AddWithValue("@CARDQUANTITY", cardQuantity);
                        mySqlCommand.Parameters.AddWithValue("@CARDTYPE", cardType);
                        mySqlCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception exception)
                {
                    string message = "\n\nPlease, check whether your server database has the procedure: \n\n" +
                        Resources.ResourceManager.GetString("procGetSpecificCardQuantity");
                    MessageBox.Show(exception.Message + message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
