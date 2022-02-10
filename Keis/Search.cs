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
    class Search
    {
        readonly string connectionString = ConfigurationManager.ConnectionStrings["keis"].ConnectionString;
        string keyPhrase = ""; //Search Query string

        public Object search(string keyPhrase)
        {
            this.keyPhrase = keyPhrase;
            MySqlConnection thisConnection = new MySqlConnection(connectionString); //Initialize Connection
            try
            {
                // Search Password from SQL Database
                thisConnection.Open();
                string query = "SELECT id, name, username, password, category, websiteURL, developerName from passwords WHERE name LIKE '" + "%" + this.keyPhrase + "%" +"'";
                MySqlCommand cmd = new MySqlCommand(query, thisConnection);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds, "LoadDataBinding"); 
                this.keyPhrase = ""; //Empty keyphrase
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
    }
}
