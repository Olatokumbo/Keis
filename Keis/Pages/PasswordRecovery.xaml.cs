using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
using System.Windows.Shapes;

namespace Keis.Pages
{
    /// <summary>
    /// Interaction logic for PasswordRecovery.xaml
    /// </summary>
    public partial class PasswordRecovery : Window
    {
        readonly string connectionString = ConfigurationManager.ConnectionStrings["keis"].ConnectionString;
        private List<string> questionCollection = new List<string> { "Your Middle Name", "Your Mothers Maiden Name", "Your first pets name" };
        PasswordCrypto passwordCrypto = new PasswordCrypto();
        string securityQuestion, securityAnswer, username, password;

        public PasswordRecovery()
        {
            InitializeComponent();
            combobox1.ItemsSource = questionCollection;
            combobox1.IsEnabled = false;
            this.Password.IsEnabled = false;
            this.Verify.IsEnabled = false;
        }

        private void verify(object sender, RoutedEventArgs e)
        {
            if (this.Password.Text == this.securityAnswer)
            {
                MessageBox.Show("Your Password is: " + passwordCrypto.decrypt(this.password), "Success");
            }
            else
            {
                MessageBox.Show("Incorrect Answer", "Failed");
            }
        }

        private void login(object sender, MouseButtonEventArgs e)
        {
            Login log = new Login();
            log.Show();
            this.Hide();
        }

        private void search(object sender, RoutedEventArgs e)
        {
            MySqlConnection thisConnection = new MySqlConnection(connectionString);
            try
            {
                thisConnection.Open();
                string query = "SELECT username, securityQuestion, securityAnswer, password FROM users WHERE username='"+ this.Username.Text +"' ";
                MySqlCommand cmd = new MySqlCommand(query, thisConnection);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        this.username = reader.GetString(0);
                        this.securityQuestion = reader.GetString(1);
                        this.securityAnswer = reader.GetString(2);
                        this.password = reader.GetString(3);


                        combobox1.SelectedItem = this.securityQuestion;
                        this.Verify.IsEnabled = true;
                        this.Search.IsEnabled = false;
                        this.Password.IsEnabled = true;
                        this.Username.IsEnabled = false;
                    }
                    reader.Close();
               
                }
                else
                {
                    MessageBox.Show("Username not Found", "Error");
                }
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
