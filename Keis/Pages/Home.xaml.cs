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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Keis.Pages
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        string connectionString = ConfigurationManager.ConnectionStrings["keis"].ConnectionString;
        PasswordManager pManager = new PasswordManager();
        User user;

        public Home(string id, string username, bool isAdmin)
        {
            InitializeComponent();
            dataGridPasswords.Columns[0].Visibility = Visibility.Hidden;
            MessageBox.Show("Welcome "+ username);
            user = new User(id, username, isAdmin); //Initialize User
            loadData(); //Load Password info
        }

        private void loadData()
        {
            if (user.isAdmin) //Checks if a user is an admin
                dataGridPasswords.DataContext = pManager.viewAllPasswords();
            else
                dataGridPasswords.DataContext = pManager.viewPasswords(user.userId);
        }

        private void logout(object sender, RoutedEventArgs e)
        {
            Login log = new Login();
            log.Show();
            user.logout();
            this.Close();
        }

        private void refresh(object sender, RoutedEventArgs e)
        {
            dataGridPasswords.Items.Refresh();
            SearchInput.Text = "";
            loadData();
        }

        private void passwordVisible(object sender, RoutedEventArgs e)
        {
            if(PasswordCheck.IsChecked==true)
            dataGridPasswords.Columns[4].Visibility = Visibility.Visible;
            else
            dataGridPasswords.Columns[4].Visibility = Visibility.Collapsed;
        }

        private void searchFtn(object sender, RoutedEventArgs e)
        {
            dataGridPasswords.DataContext = pManager.searchPassword(SearchInput.Text);
        }

        private void addPassword(object sender, RoutedEventArgs e)
        {
            NewPassword np = new NewPassword(user.userId);
            np.Show();
        }
        private void editBtn(object sender, RoutedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)((Button)e.Source).DataContext;
            String passwordId = dataRowView[0].ToString();

            EditPassword ep = new EditPassword(passwordId, user.userId);
            ep.Show();
        }

        private void addRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

        private void deleteBtn(object sender, RoutedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)((Button)e.Source).DataContext;
            String passwordId = dataRowView[0].ToString();

            pManager.deletePassword(passwordId);
            MessageBox.Show("Password Deleted");
            loadData();
        }

        private void sort(object sender, RoutedEventArgs e)
        {
            dataGridPasswords.DataContext = pManager.sortPasswords(user.userId);
        }
    }
}
