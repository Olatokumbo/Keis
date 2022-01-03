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
    /// Interaction logic for NewPassword.xaml
    /// </summary>
    public partial class NewPassword : Window
    {
        private List<string> categoryCollection = new List<string> {"Desktop", "Website", "Game" };
        PasswordManager pManager = new PasswordManager();
        PasswordGen newPassword = new PasswordGen();
        string userId;
        public NewPassword(string userId)
        {
            InitializeComponent();
            combobox1.ItemsSource = categoryCollection;
            combobox1.SelectedItem = categoryCollection[0];
            WebLabel.Visibility = Visibility.Collapsed;
            WebsiteURL.Visibility = Visibility.Collapsed;
            DevLabel.Visibility = Visibility.Collapsed;
            DeveloperName.Visibility = Visibility.Collapsed;
            this.userId = userId;
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

        private void addPassword(object sender, RoutedEventArgs e)
        {
            //if(Name.Text.Length>=3 && (Username.Text.Length>=3 && ))
            switch (combobox1.SelectedIndex)
            {
                case 0:
                    if (Name.Text.Length >= 3 && Username.Text.Length >= 3 && Password.Text.Length >= 3)
                    {
                        Desktop desktop = new Desktop(Name.Text, Username.Text, Password.Text, userId);
                        desktop.savePassword();
                    }
                    else MessageBox.Show("Invalid inputs", "Error");
                    break;
                case 1:
                    if (Name.Text.Length >= 3 && Username.Text.Length >= 3 && Password.Text.Length >= 3 && WebsiteURL.Text.Length >=3)
                    {
                        Website website = new Website(Name.Text, Username.Text, Password.Text, WebsiteURL.Text, userId);
                        website.savePassword();
                    }
                    else MessageBox.Show("Invalid inputs", "Error");
                    break;
                default:
                    if (Name.Text.Length >= 3 && Username.Text.Length >= 3 && Password.Text.Length >= 3 && DeveloperName.Text.Length >=3)
                    {
                        Game game = new Game(Name.Text, Username.Text, Password.Text, DeveloperName.Text, userId);
                        game.savePassword();
                    }
                    else MessageBox.Show("Invalid inputs", "Error");
                    break;
            }
            this.Close();
        }

        private void generatePwdBtn(object sender, RoutedEventArgs e)
        {
            Password.Text = newPassword.generatePassword();

        }
    }
}
