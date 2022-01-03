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
using System.Windows.Shapes;

namespace Keis.Pages
{
    /// <summary>
    /// Interaction logic for EditPassword.xaml
    /// </summary>
    public partial class EditPassword : Window
    {
        private List<string> categoryCollection = new List<string> { "desktop", "website", "game" };
        private Dictionary<string, string> passwordData;
        PasswordManager pManager = new PasswordManager();
        string userId, id;
        public EditPassword(string id, string user)
        {
            InitializeComponent();
            userId = user;
            this.id = id;
            combobox1.ItemsSource = categoryCollection;
            passwordData = pManager.viewPassword(id);
            Name.Text = passwordData["name"];
            Username.Text = passwordData["username"];
            Password.Text = passwordData["password"];
            combobox1.SelectedItem = passwordData["category"];
            combobox1.IsEnabled = false;
            if (passwordData.ContainsKey("websiteURL"))
                WebsiteURL.Text = passwordData["websiteURL"];
            else if (passwordData.ContainsKey("developerName"))
                DeveloperName.Text = passwordData["developerName"];
        }

        private void combobox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (combobox1.SelectedIndex)
            {
                case 0:
                    WebLabel.Visibility = Visibility.Collapsed;
                    WebsiteURL.Visibility = Visibility.Collapsed;
                    DevLabel.Visibility = Visibility.Collapsed;
                    DeveloperName.Visibility = Visibility.Collapsed;
                    break;
                case 1:
                    WebLabel.Visibility = Visibility.Visible;
                    WebsiteURL.Visibility = Visibility.Visible;
                    DevLabel.Visibility = Visibility.Collapsed;
                    DeveloperName.Visibility = Visibility.Collapsed;
                    break;
                case 2:
                    WebLabel.Visibility = Visibility.Collapsed;
                    WebsiteURL.Visibility = Visibility.Collapsed;
                    DevLabel.Visibility = Visibility.Visible;
                    DeveloperName.Visibility = Visibility.Visible;
                    break;
                default:
                    WebLabel.Visibility = Visibility.Collapsed;
                    WebsiteURL.Visibility = Visibility.Collapsed;
                    DevLabel.Visibility = Visibility.Collapsed;
                    DeveloperName.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void savePwdBtn(object sender, RoutedEventArgs e)
        {
            switch (combobox1.SelectedIndex)
            {
                case 0:
                    if (Name.Text.Length >= 3 && Username.Text.Length >= 3 && Password.Text.Length >= 3)
                    {
                        Desktop desktop = new Desktop(Name.Text, Username.Text, Password.Text, userId);
                        desktop.editPassword(id);
                    }
                    else MessageBox.Show("Invalid inputs", "Error");
                    break;
                default:

                    break;
            }
            this.Close();
        }
    }
}
