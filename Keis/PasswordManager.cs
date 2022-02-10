using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keis
{
    class PasswordManager
    {
        string connectionString = ConfigurationManager.ConnectionStrings["keis"].ConnectionString;
        PasswordCrypto passwordCrypto = new PasswordCrypto();
        Search Searcher = new Search();
        public Object viewPasswords(string userId)
        {
            MySqlConnection thisConnection = new MySqlConnection(connectionString); //Initialize Connection
            try
            {
                //Query Password by the User Id
                thisConnection.Open();
                string query = "SELECT id, name, username, password, category, websiteURL, developerName from passwords WHERE userId = '" + userId + "'";
                MySqlCommand cmd = new MySqlCommand(query, thisConnection);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds, "LoadDataBinding");
                return ds;
            }
            catch (Exception m)
            {
                Console.WriteLine(m);
                return thisConnection;
            }
            finally
            {
                thisConnection.Close(); //Close Connection
            }
        }

        public Object sortPasswords(string userId)
        {
            MySqlConnection thisConnection = new MySqlConnection(connectionString); //Initialize Connection
            try
            {
                //Sort Password by the lastUPdated value
                thisConnection.Open();
                string query = "SELECT id, name, username, password, category, websiteURL, developerName from passwords WHERE userId = '" + userId + "' ORDER BY lastUpdated DESC";
                MySqlCommand cmd = new MySqlCommand(query, thisConnection);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds, "LoadDataBinding");
                return ds;
            }
            catch (Exception m)
            {
                Console.WriteLine(m);
                return thisConnection;
            }
            finally
            {
                thisConnection.Close(); //Close Connection
            }
        }

        public Object viewAllPasswords()
        {
            MySqlConnection thisConnection = new MySqlConnection(connectionString); //Initialize Connection
            try
            {
                //View All Passwords (Admin can only do this)
                thisConnection.Open();
                string query = "SELECT id, name, username, password, category, websiteURL, developerName from passwords";
                MySqlCommand cmd = new MySqlCommand(query, thisConnection);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds, "LoadDataBinding");
                return ds;
            }
            catch (Exception m)
            {
                Console.WriteLine(m);
                return thisConnection;
            }
            finally
            {
                thisConnection.Close();
            }
        }

        public Dictionary<string, string> viewPassword(string id)
        {
            MySqlConnection thisConnection = new MySqlConnection(connectionString); //Initialize Connection
            Dictionary<string, string> password = new Dictionary<string, string>();
            try
            {
                //Query Password by the Parent Id
                thisConnection.Open();
                string query = "SELECT id, name, username, password, category, websiteURL, developerName from passwords WHERE id = '" + id + "'";
                MySqlCommand cmd = new MySqlCommand(query, thisConnection);
                MySqlDataReader reader= cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        password.Add("id", reader.GetString(0));
                        password.Add("name", reader.GetString(1));
                        password.Add("username", reader.GetString(2));
                        password.Add("password", passwordCrypto.decrypt(reader.GetString(3)));
                        password.Add("category", reader.GetString(4));
                        if (!reader.IsDBNull(5))
                            password.Add("websiteURL", reader.GetString(5));
                        if (!reader.IsDBNull(6))
                            password.Add("developerName", reader.GetString(6));
                    }
                    reader.Close();
                    return password;
                }
                return password;
            }
            catch (Exception m)
            {
                Console.WriteLine(m);
                return password;
            }
            finally
            {
                thisConnection.Close(); //Close Connection
            }
        }

        public void deletePassword(string id)
        {
            Console.WriteLine("Delete Password");
            MySqlConnection thisConnection = new MySqlConnection(connectionString); //Initialize Connection
            try
            {
                //Delete Password by Id from SQL Database
                thisConnection.Open();
                string query = "DELETE from passwords WHERE id = '" + id + "'";
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
    
        //Search Password
        public Object searchPassword(string word)
        {
            return Searcher.search(word);
        }
    }
};
