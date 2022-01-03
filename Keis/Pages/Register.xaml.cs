using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
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

namespace Keis.Pages
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        private string username, password, securityQuestion, securityAnswer;
        string connectionString = ConfigurationManager.ConnectionStrings["keis"].ConnectionString;
        private List<string> questionCollection = new List<string>{"Your Middle Name", "Your Mothers Maiden Name", "Your first pets name"};
        PasswordCrypto passwordCrypto = new PasswordCrypto();

        public Register()
        {
            InitializeComponent();
            combobox1.ItemsSource = questionCollection;
            combobox1.SelectedItem = questionCollection[0];
        }


        private void combobox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           securityQuestion=combobox1.SelectedItem.ToString();
        }

        private void register(object sender, RoutedEventArgs e)
        {
            username = Username.Text;
            password = passwordCrypto.encrypt(Password.Text);
            securityAnswer = SecurityAnswer.Text;

            MySqlConnection thisConnection = new MySqlConnection(connectionString);
            thisConnection.Open();
            string query = "SELECT * from users WHERE username ='" + username + "'";
            MySqlCommand cmd = new MySqlCommand(query, thisConnection);
            MySqlDataReader row = cmd.ExecuteReader();
            if (row.HasRows)
            {
                MessageBox.Show("Username already exists", "Error");
                thisConnection.Close();
            }
            else
            {
                thisConnection.Close();
                try {
                    thisConnection = new MySqlConnection(connectionString);
                    thisConnection.Open();
                    query = "INSERT into users (username, password, securityQuestion, securityAnswer) VALUES ('" + username + "', '" + password + "', '" + securityQuestion + "', '" + securityAnswer + "')";
                    cmd = new MySqlCommand(query, thisConnection);
                    row = cmd.ExecuteReader();
                    MessageBox.Show("Successfully Registered", "Success");
                    thisConnection.Close();
                    //If credentials are valid
                    Login lg = new Login();
                    lg.Show();
                    this.Close();
                }
                catch (Exception m)
                {
                    Console.WriteLine(m);
                }
            }

            }

        private void login(object sender, MouseButtonEventArgs e)
        {
            Login log = new Login();
            log.Show();
            this.Hide();
        }
    }
}
