using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace Keis.Pages
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        string id, username, password;
        string connectionString = ConfigurationManager.ConnectionStrings["keis"].ConnectionString;
        PasswordCrypto passwordCrypto = new PasswordCrypto();
        //User user;
        public Login()
        {
            InitializeComponent();
        }

        private void forgotPassword(object sender, MouseButtonEventArgs e)
        {
            PasswordRecovery PR = new PasswordRecovery();
            PR.Show();
            this.Close();
        }

        private void login(object sender, RoutedEventArgs e)
        {
            password = passwordCrypto.encrypt(Password.Text);
            try
            {
                MySqlConnection thisConnection = new MySqlConnection(connectionString);
                thisConnection.Open();
                string query = "SELECT * from users WHERE username ='" + Username.Text + "' AND password ='" + password + "'";
                MySqlCommand cmd = new MySqlCommand(query, thisConnection);
                MySqlDataReader row = cmd.ExecuteReader();
                if (row.HasRows)
                {
                    while (row.Read())
                    {
                        id = row["id"].ToString();
                        username = row["username"].ToString();
                        password = row["password"].ToString();
                        
                    }
                    //user = new User(id, username, false);
                    //If credentials are valid
                    Home hm = new Home(id, username);
                    hm.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid Credentials");
                }
            }
            catch (Exception m)
            {
                Console.WriteLine(m);
            }
        }

        private void register(object sender, MouseButtonEventArgs e)
        {
            Register reg = new Register();
            reg.Show();
            this.Hide();
        }
    }
}
