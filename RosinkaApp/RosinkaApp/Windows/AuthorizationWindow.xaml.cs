using RosinkaApp.Classes;
using RosinkaApp.Entities;
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

namespace RosinkaApp.Windows
{
    /// <summary>
    /// Interaction logic for AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        public User curUser = new User();
        public AuthorizationWindow()
        {
            InitializeComponent();
        }

        private void enterBtn_Click(object sender, RoutedEventArgs e)
        {
            string err = "";
            if (string.IsNullOrWhiteSpace(loginTb.Text))
                err += "Вы не ввели логин\n";
            if (string.IsNullOrWhiteSpace(passTb.Password))
                err += "Вы не ввели пароль\n";
            if(!string.IsNullOrWhiteSpace(err))
            {
                MessageBox.Show(err);
                return;
            }
            string loginString = String.Join("", loginTb.Text.Split('(', ')', '-', '+', '_')).Replace(" ", "");
            try
            {
            curUser = AppData.Context.User.Where(p => p.PhoneNumber == loginString && p.Password == passTb.Password).FirstOrDefault();
            }
            catch (Exception)
            {
            }
            if (curUser == null)
                MessageBox.Show("Вы неправильно ввели логин и/или пароль");
            else
            {
                AppData.currentUser = curUser;
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }

        }

        private void loginTb_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
