using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using PangYaItensManagement.Model;
using PangYaItensManagement.Properties;

namespace PangYaItensManagement.DAO
{
    public class UserDAO : Connection
    {
        /// <summary>
        /// Check whether a user exists
        /// </summary>
        /// <param name="user">User object</param>
        /// <returns>True or false for user exists</returns>
        public bool CheckUserExists(User user)
        {
            string userNickName = String.Empty;
            bool userExists = false;

            if (IsConnected() != null)
            {
                //If config file has info about user credentials
                if (!Properties.Settings.Default.userId.Equals(String.Empty))
                {
                    user.Id = Properties.Settings.Default.userId;
                    user.Nickname = Properties.Settings.Default.userNickname;

                    using (mySqlConnection)
                    {
                        try
                        {
                            using (MySqlCommand mySqlCommand = new MySqlCommand(Resources.ResourceManager.GetString("procGetUserInfo"), mySqlConnection))
                            {
                                mySqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                                mySqlCommand.Parameters.AddWithValue("@IDUSER", user.Id);

                                if (mySqlCommand.ExecuteScalar() != null)
                                {
                                    userExists = true;
                                }
                                else
                                {
                                    userExists = false;
                                }
                            }
                        }
                        catch (Exception exception)
                        {
                            string message = "\n\nPlease, check whether your server database has the procedure: \n\n" +
                                Resources.ResourceManager.GetString("procGetUserInfo");
                            MessageBox.Show(exception.Message + message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                }
            }

            return userExists;
        }

        /// <summary>
        /// Check whether a user exists from a specific item selected in listBox
        /// </summary>
        /// <param name="user">User object</param>
        /// <returns>True or false for user exists from a login button click</returns>
        public bool CheckUserExistsFromLoginButton(User user)
        {
            bool userExists = false;

            using (mySqlConnection)
            {
                try
                {
                    using (MySqlCommand mySqlCommand = new MySqlCommand(Resources.ResourceManager.GetString("procGetUserInfo"), mySqlConnection))
                    {
                        mySqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        mySqlCommand.Parameters.AddWithValue("@IDUSER", user.Id);

                        if (mySqlCommand.ExecuteScalar() != null)
                        {
                            userExists = true;
                        }
                        else
                        {
                            userExists = false;
                        }
                    }
                }
                catch (Exception exception)
                {
                    string message = "\n\nPlease, check whether your server database has the procedure: \n\n" +
                        Resources.ResourceManager.GetString("procGetUserInfo");
                    MessageBox.Show(exception.Message + message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            return userExists;
        }

        /// <summary>
        /// Select all users from database
        /// </summary>
        /// <returns>A list of PangYa S8 users (players)</returns>
        public List<User> GetAllUsers()
        {
            List<User> usersList = new List<User>();
            int userID;
            string userNickName = String.Empty;

            #region Connect to database
            IsConnected();
            #endregion Connect to database

            using (mySqlConnection)
            {
                try
                {
                    using (MySqlCommand mySqlCommand = new MySqlCommand(Resources.ResourceManager.GetString("procGetAllUsers"), mySqlConnection))
                    {
                        mySqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                        using (MySqlDataReader reader = mySqlCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                userID = Convert.ToInt32(String.Format("{0}", reader["UID"]));
                                userNickName = (String.Format("{0}", reader["NICK"]));
                                User user = new User
                                {
                                    Id = userID,
                                    Nickname = userNickName
                                };
                                usersList.Add(user);
                            }
                        }
                    }
                }
                catch (Exception exception)
                {
                    string message = "\n\nPlease, check whether your server database has the procedure: \n\n" +
                        Resources.ResourceManager.GetString("procGetAllUsers");
                    MessageBox.Show(exception.Message + message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }

            return usersList;
        }

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
    }
}
