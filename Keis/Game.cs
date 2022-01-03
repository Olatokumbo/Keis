﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keis
{
    class Game: Category
    {
        public string developerName;
        string category = "game";
        PasswordCrypto passwordCrypto = new PasswordCrypto();

        public Game(string name, string username, string password, string developerName, string userId)
        {
            this.name = name;
            this.username = username;
            this.password = passwordCrypto.encrypt(password);
            this.developerName = developerName;
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
                string query = "INSERT INTO passwords(name, username, password, category, dateCreated, lastUpdated, developerName, userId) VALUES ('" + name + "', '" + username + "', '" + password + "','" + category + "','" + sqlFormattedDate + "', '" + sqlFormattedDate + "','" + developerName + "', '" + userId + "')";
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
