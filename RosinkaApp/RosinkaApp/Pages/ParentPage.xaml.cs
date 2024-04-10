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
    /// Interaction logic for ParentPage.xaml
    /// </summary>
    /// 
    public partial class ParentPage : Page
    {
        List<Parent> _childParents = AppData.Context.Parent.ToList();
        public ParentPage()
        {
            InitializeComponent();
            parentList.ItemsSource = _childParents;
        }

        private void Update()
        {
            _childParents = AppData.Context.Parent.ToList();


            // Строка поиска по параметрам: ФИО, ДЕНЬ РОЖДЕНИЯ
            if (!string.IsNullOrWhiteSpace(searchTb.Text))
            {
                _childParents = _childParents.Where(p => p.FirstName.ToLower().Trim().Contains(searchTb.Text.ToLower().Trim()) ||
                p.LastName.ToLower().Trim().Contains(searchTb.Text.ToLower().Trim()) ||
                p.Patronymic.ToLower().Trim().Contains(searchTb.Text.ToLower().Trim()) ||
                p.Passport.Birthday.Value.DayOfYear.ToString().ToLower().Trim().Contains(searchTb.Text.ToLower().Trim())).ToList();
            }


            // Сортировка А-Я, Я-А.
            if (nameCmb.SelectedIndex != -1)
            {
                if (nameCmb.SelectedIndex == 0)
                    _childParents = _childParents.OrderBy(p => p.ParentFullName).ToList();
                else
                    _childParents = _childParents.OrderByDescending(p => p.ParentFullName).ToList();

            }

            parentList.ItemsSource = _childParents;
        }

        private void searchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }

        private void delBtn_Click(object sender, RoutedEventArgs e)
        {
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            ParentAddEditWindow parentAddEditWindow = new ParentAddEditWindow((sender as Button).DataContext as Parent);
            parentAddEditWindow.ShowDialog();
            Update();
        }

        private void nameCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            searchTb.Text = string.Empty;
            nameCmb.SelectedIndex = -1;
            Update();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            ParentAddEditWindow parentAddEditWindow = new ParentAddEditWindow(null);
            parentAddEditWindow.ShowDialog();
            Update();
        }
    }
}
