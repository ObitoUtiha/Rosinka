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
    /// Interaction logic for ParentWindow.xaml
    /// </summary>
    public partial class ParentWindow : Window
    {
        List<Parent> _childParents = AppData.Context.Parent.ToList();
        public ParentWindow()
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

        private void nameCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void searchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }

        private void ParentBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AppData.newParent = (sender as Border).DataContext as Parent;
            this.DialogResult = true;
            this.Close();
        }
    }
}
