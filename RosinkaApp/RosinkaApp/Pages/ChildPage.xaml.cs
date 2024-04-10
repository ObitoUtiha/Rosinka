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
    /// Interaction logic for ChildPage.xaml
    /// </summary>
    public partial class ChildPage : Page
    {
        private List<Child> childrenList = new List<Child>();
        public ChildPage()
        {
           // nameCmb.SelectedIndex = 0;
            InitializeComponent();
            childtList.ItemsSource = AppData.Context.Child.ToList();
            groupCmb.ItemsSource = AppData.Context.Group.ToList();
        }

        private void delBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            ChildAddEditWindow childAddEditWindow = new ChildAddEditWindow((sender as Button).DataContext as Child);
            childAddEditWindow.ShowDialog();
            Update();
        }

        private void Update()
        {
            childrenList = AppData.Context.Child.ToList();

            // Строка поиска по параметрам: ФИО, ДЕНЬ РОЖДЕНИЯ
            if (!string.IsNullOrWhiteSpace(searchTb.Text))
            {
                childrenList = childrenList.Where(p => p.FirstName.ToLower().Trim().Contains(searchTb.Text.ToLower().Trim()) ||
                p.LastName.ToLower().Trim().Contains(searchTb.Text.ToLower().Trim()) ||
                p.Patronymic.ToLower().Trim().Contains(searchTb.Text.ToLower().Trim()) ||
                p.Birthday.Value.DayOfYear.ToString().ToLower().Trim().Contains(searchTb.Text.ToLower().Trim())).ToList();
            }

            // Выборка из группы
            if (groupCmb.SelectedIndex != -1)
                childrenList = childrenList.Where(p => p.ChildGroup.Select(i=>i.Group).Contains(groupCmb.SelectedItem as Group)).ToList();

            // Сортировка А-Я, Я-А.
            if (nameCmb.SelectedIndex != -1)
            {
                if (nameCmb.SelectedIndex == 0)
                    childrenList = childrenList.OrderBy(p => p.ChildFullName).ToList();
                else
                    childrenList = childrenList.OrderByDescending(p => p.ChildFullName).ToList();

            }

            childtList.ItemsSource = childrenList;
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

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            groupCmb.SelectedIndex = -1;
            searchTb.Text = string.Empty;
            nameCmb.SelectedIndex = -1;
            Update();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            ChildAddEditWindow childAddEditWindow = new ChildAddEditWindow(null);
            childAddEditWindow.ShowDialog();
            Update();
        }
    }
}
