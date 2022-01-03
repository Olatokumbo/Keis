using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keis
{
    class Website : Category
    {
        string websiteURL;
        string category = "website";
        PasswordCrypto passwordCrypto = new PasswordCrypto();
        public Website(string name, string username, string password, string websiteURL, string userId)
        {
            this.name = name;
            this.username = username;
            this.password = passwordCrypto.encrypt(password);
            this.websiteURL = websiteURL;
            this.userId = userId;
        }
        public void savePassword()
        {
            Console.WriteLine("Save Password");
            MySqlConnection thisConnection = new MySqlConnection(connectionString);
            DateTime myDateTime = DateTime.Now;
            string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
            //dateCreated = sqlFormattedDate;

            try
            {
                thisConnection.Open();
                string query = "INSERT INTO passwords(name, username, password, category, dateCreated, lastUpdated, websiteURL, userId) VALUES ('" + name + "', '" + username + "', '" + password + "','" + category + "','" + sqlFormattedDate + "', '" + sqlFormattedDate + "','" + websiteURL + "', '"+ userId +"')";
                MySqlCommand cmd = new MySqlCommand(query, thisConnection);
                MySqlDataReader row = cmd.ExecuteReader();
            }
            catch (Exception m)
            {
                Console.WriteLine(m);
            }
            finally
            {
                thisConnection.Close();
            }
        }
    }
}
