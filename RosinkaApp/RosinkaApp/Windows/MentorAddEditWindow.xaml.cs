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
    /// Interaction logic for MentorAddEditWindow.xaml
    /// </summary>
    public partial class MentorAddEditWindow : Window
    {
        User _currentUser = new User();
        List<Role> _roles = new List<Role>();
        public MentorAddEditWindow(User user)
        {
            InitializeComponent();
            _roles = AppData.Context.Role.ToList();
            RoleCb.ItemsSource = _roles;
            _currentUser = user;
            if(user != null )
            {
                DateOfAcceptanceTb.SelectedDate = user.DateOfAcceptance;
                FullNameTb.Text = user.MentorFullName;
                PasswordTb.Text = user.Password;
                PhoneNumberTb.Text = user.PhoneNumber;
                RoleCb.SelectedItem = user.Role;
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(DateOfAcceptanceTb.Text) || DateOfAcceptanceTb.SelectedDate == null || string.IsNullOrWhiteSpace(FullNameTb.Text) || string.IsNullOrWhiteSpace(PasswordTb.Text) ||
                string.IsNullOrWhiteSpace(PhoneNumberTb.Text) || RoleCb.SelectedIndex == -1)
            {
                MessageBox.Show("Ошибка!\nВозможно вы не ввели все данные в текстовые поля");
                return;
            }
            string[] fullName = FullNameTb.Text.Split(' ');
            if( fullName.Length < 3 )
            {
                MessageBox.Show("Ошибка!\nВозможно вы не ввели все данные в текстовые поля");
                return;
            }
            if (_currentUser == null)
            {
                AppData.Context.User.Add(new User
                {
                    DateOfAcceptance = DateOfAcceptanceTb.SelectedDate,
                    LastName = fullName[0],
                    FirstName = fullName[1],
                    Patronymic = fullName[2],
                    Password = PasswordTb.Text,
                    PhoneNumber = PhoneNumberTb.Text,
                    Role = RoleCb.SelectedItem as Role
                }) ;
            }
            else
            {
                _currentUser.DateOfAcceptance = DateOfAcceptanceTb.SelectedDate;
                _currentUser.LastName = fullName[0];
                _currentUser.FirstName = fullName[1];
                _currentUser.Patronymic = fullName[2];
                _currentUser.Password = PasswordTb.Text;
                _currentUser.PhoneNumber = PhoneNumberTb.Text;
                _currentUser.Role = RoleCb.SelectedItem as Role;
            }
            AppData.Context.SaveChanges();
        }
    }
}
