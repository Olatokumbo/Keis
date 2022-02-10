using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keis
{
    class Category
    {
        public string name, username, password, userId;
        public string dateCreated, lastUpdated;
        
        string category = "desktop";
        protected string connectionString = ConfigurationManager.ConnectionStrings["keis"].ConnectionString;
        protected PasswordGen pGen = new PasswordGen();

        public string generatePassword()
        {
            return pGen.generatePassword();
        }
        public void savePassword()
        {
            Console.WriteLine("Save Password");
            MySqlConnection thisConnection = new MySqlConnection(connectionString); //Initialize Connection
            DateTime myDateTime = DateTime.Now; //Current Date
            string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
            dateCreated = sqlFormattedDate;
            lastUpdated = sqlFormattedDate;
            
            try
            {
                //Insert Password into database
                thisConnection.Open();
                string query = "INSERT INTO passwords(name, username, password, category, dateCreated, lastUpdated, userId) VALUES ('" + name + "', '" + username + "', '" + password + "','" + category + "','" + sqlFormattedDate + "', '"+ sqlFormattedDate + "','" + userId + "')";
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
            MySqlConnection thisConnection = new MySqlConnection(connectionString); //Initial Connection
            DateTime myDateTime = DateTime.Now; //Current Date
            string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
            lastUpdated = sqlFormattedDate; 

            try
            {
                //Update Password
                thisConnection.Open();
                string query = "UPDATE passwords SET name ='" + name + "', username ='" + username + "', password ='" + password + "', category ='" + category + "', lastUpdated ='" + lastUpdated + "' WHERE id ='" + id + "'";
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
