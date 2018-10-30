using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace PangYaItensManagement.DAO
{
    public class Connection
    {
        protected MySqlConnection mySqlConnection = null;

        /// <summary>
        /// Connect to PangYa S8 database
        /// </summary>
        /// <param name="user">Database server user - default is root </param>
        /// <param name="password">Database server password - default is ""</param>
        /// <param name="server">Database server name - default is localhost</param>
        /// <param name="databaseName">Database name - default is pangya</param>
        /// <returns>The connection to MySQL database</returns>
        public MySqlConnection Connect(string user, string password, string server = "localhost", string databaseName = "pangya")
        {
            try
            {
                string connectionString = "DATABASE =" + databaseName + "; " +
                                          "SERVER =" + server + "; " +
                                          "UID = " + user + "; " +
                                          "PWD = " + password;
                mySqlConnection = new MySqlConnection(connectionString);
                mySqlConnection.Open();

                return mySqlConnection;
            }
            catch (Exception exception)
            {
                string message = "\n\nPlease, check whether server database is running and the following data is correct: \n\n" +
                    "- User ID [" + Properties.Resources.ResourceManager.GetString("UID") + "]\n" +
                    "- Password [" + Properties.Resources.ResourceManager.GetString("password") + "]\n" +
                    "- Database server name (localhost or link) [" + Properties.Resources.ResourceManager.GetString("databaseServer") + "]\n" +
                    "- Database name [" + Properties.Resources.ResourceManager.GetString("databaseName") + "]";
                MessageBox.Show(exception.Message + message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
    }
}
