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
            this.name = name; //Set Website Name
            this.username = username; //Set Username
            this.password = passwordCrypto.encrypt(password); //Set encrypted Password
            this.websiteURL = websiteURL; //Set Website URL
            this.userId = userId; //Set UserId
        }
        public void savePassword()
        {
            Console.WriteLine("Save Password");
            MySqlConnection thisConnection = new MySqlConnection(connectionString); //Initialize Connection
            DateTime myDateTime = DateTime.Now; //Current Date Info
            string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
            try
            {
                //Insert Password into SQL Database
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
                thisConnection.Close(); //Close Connection
            }
        }
    
        public void editPassword(string id)
        {
            Console.WriteLine("Edit Password");
            MySqlConnection thisConnection = new MySqlConnection(connectionString); //Initialize Connection
            DateTime myDateTime = DateTime.Now; //Current Date Info
            string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
            lastUpdated = sqlFormattedDate; 

            try
            {
                //Update Password in Database
                thisConnection.Open();
                string query = "UPDATE passwords SET name ='" + name + "', username ='" + username + "', websiteURL = '"+ websiteURL +"', password ='" + password + "', category ='" + category + "', lastUpdated ='" + lastUpdated + "' WHERE id ='" + id + "'";
                MySqlCommand cmd = new MySqlCommand(query, thisConnection);
                MySqlDataReader row = cmd.ExecuteReader();
            }
            catch (Exception m)
            {
                Console.WriteLine(m);
            }
            finally
            {
                thisConnection.Close(); //Close Connection
            }
        }
    }
}
