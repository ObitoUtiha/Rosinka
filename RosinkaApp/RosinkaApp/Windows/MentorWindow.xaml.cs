using RosinkaApp.Classes;
using RosinkaApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
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
    /// Interaction logic for MentorWindow.xaml
    /// </summary>
    public partial class MentorWindow : Window
    {
        private List<Group> groups = AppData.Context.Group.ToList();
        private List<User> _users = new List<User>();
        public MentorWindow()
        {
            InitializeComponent();
            _users = AppData.Context.User.ToList();
            mentorList.ItemsSource = _users;
            groupCmb.ItemsSource = groups;
        }

        private void Update()
        {
            _users = AppData.Context.User.ToList();

            if (!string.IsNullOrWhiteSpace(searchTb.Text))
            {
                _users = _users.Where(p => p.FirstName.ToLower().Trim().Contains(searchTb.Text.ToLower().Trim()) ||
                p.LastName.ToLower().Trim().Contains(searchTb.Text.ToLower().Trim()) ||
                p.Patronymic.ToLower().Trim().Contains(searchTb.Text.ToLower().Trim()) ||
                p.PhoneNumber.ToLower().Trim().Contains(searchTb.Text.ToLower().Trim())).ToList();
            }

            if (groupCmb.SelectedIndex > -1)
            {
                _users = _users.Where(p => p.groupsUserList.Contains(groupCmb.SelectedItem as Group)).ToList();
            }

            mentorList.ItemsSource = _users;
        }

        private void TakeUserBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
             AppData.newUser = (sender as Border).DataContext as User;
             this.DialogResult = true;
             this.Close();

        }

        private void searchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }

        private void groupCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void nameCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }
    }
}
