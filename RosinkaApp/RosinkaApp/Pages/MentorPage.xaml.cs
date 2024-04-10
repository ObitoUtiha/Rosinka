using RosinkaApp.Classes;
using RosinkaApp.Entities;
using RosinkaApp.Windows;
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

namespace RosinkaApp.Pages
{
    /// <summary>
    /// Interaction logic for MentorPage.xaml
    /// </summary>
    public partial class MentorPage : Page
    {
        private List<Group> groups = AppData.Context.Group.ToList();
        private List<User> _users = new List<User>();
        public MentorPage()
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

            if(groupCmb.SelectedIndex > -1)
            {
                _users = _users.Where(p => p.groupsUserList.Contains(groupCmb.SelectedItem as Group)).ToList();
            }

            // Сортировка А-Я, Я-А.
            if (nameCmb.SelectedIndex != -1)
            {
                if (nameCmb.SelectedIndex == 0)
                    _users = _users.OrderBy(p => p.MentorFullName).ToList();
                else
                    _users = _users.OrderByDescending(p => p.MentorFullName).ToList();

            }

            mentorList.ItemsSource = _users;
        }

        private void searchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }

        private void nameCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void groupCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void delBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            MentorAddEditWindow mentorAddEditWindow = new MentorAddEditWindow((sender as Button).DataContext as User);
            mentorAddEditWindow.ShowDialog();
            Update();

        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            groupCmb.SelectedIndex = -1;
            searchTb.Text = string.Empty;
            nameCmb.SelectedIndex = -1;
            Update();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            MentorAddEditWindow mentorAddEditWindow = new MentorAddEditWindow(null);
            mentorAddEditWindow.ShowDialog();
            Update();
        }
    }
}
