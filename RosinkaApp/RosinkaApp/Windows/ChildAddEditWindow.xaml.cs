using Microsoft.Win32;
using RosinkaApp.Classes;
using RosinkaApp.Entities;
using RosinkaApp.Pages;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for ChildAddEditWindow.xaml
    /// </summary>
    public partial class ChildAddEditWindow : Window
    {
        private List<ChildParent> parents = new List<ChildParent>();
        private List<ChildGroup> groups = new List<ChildGroup>();
        private Child _child = new Child();
        private ChildGroup _childGroup = new ChildGroup();
        private List<ChildParent> _childParent = new List<ChildParent>();
        private List<Parent> _currentParents= new List<Parent>();

        public ChildAddEditWindow(Child currentChild)
        {
            InitializeComponent();
            groups = AppData.Context.ChildGroup.ToList();
            parents = AppData.Context.ChildParent.ToList();
            if(AppData.currentUser.RoleId != 1)
            {
                adminPanel.Visibility = Visibility.Collapsed;
            }
            _child = currentChild;
            GroupName.ItemsSource = groups;
            HealthGroup.IsEnabled = false;
            if (currentChild != null)
            {
                _childGroup = groups.Where(p => p.Child == _child).FirstOrDefault();
                _childParent = parents.Where(p => p.Child == _child).ToList();
                _currentParents = _childParent.Select(p => p.Parent).ToList();
                ChildFullName.Text = currentChild.ChildFullName;
                Birthday.SelectedDate = currentChild.Birthday;
                if (currentChild.HealthCard != null)
                {
                    HealthGroup.Text = currentChild.HealthCard.HealthGroup;
                }
                GroupName.SelectedItem = _childGroup;
                if (_childGroup != null)
                {
                    RoomName.Text = _childGroup.Group.Room.RoomName;
                }
                parentList.ItemsSource = _currentParents;
                ChildPhoto.DataContext = currentChild.ChildPhoto;
            }
        }

        private void birthCertificateBtn_Click(object sender, RoutedEventArgs e)
        {
            HealthCardWindow healthCardWindow = new HealthCardWindow(_child);
            healthCardWindow.Show();
        }

        private void healthCardBtn_Click(object sender, RoutedEventArgs e)
        {
            BirthCertificateWindow birthCertificateWindow = new BirthCertificateWindow(_child);
            birthCertificateWindow.Show();
        }


        private byte[] _img;

        private void downloadPhotoBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Images |*.png;*.jpg;*.jpeg";
            if (ofd.ShowDialog() == true)
            {
                BitmapImage image = new BitmapImage(new Uri(ofd.FileName));

                image.DecodePixelHeight = 200;
                image.DecodePixelWidth = 300;
                _img = File.ReadAllBytes(ofd.FileName);
                ChildPhoto.DataContext = _img;
            }
        }

        private void delPhotoBtn_Click(object sender, RoutedEventArgs e)
        {
            _img = null;
            ChildPhoto.DataContext = _img;
        }

        private void confirmBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ChildFullName.Text) ||  Birthday.SelectedDate == null || Birthday.Text == "")
            {
                MessageBox.Show("Проверьте корректность данных, либо заполнение полей/списков");
                return;
            }
            string[] fullName = ChildFullName.Text.Split(' ');
            if (fullName.Length < 3)
            {
                MessageBox.Show("Ошибка!\nВозможно вы не ввели все данные в текстовые поля");
                return;
            }
            if (_child == null)
            {
                Child newChild = new Child()
                {
                    LastName = fullName[0],
                    FirstName = fullName[1],
                    Patronymic = fullName[2],
                    Birthday = Birthday.SelectedDate,
                    ChildPhoto = _img
                };
                AppData.Context.Child.Add(newChild);
                AppData.Context.SaveChanges();
                for (int i = 0; i < _currentParents.Count; i++)
                {
                    ChildParent newChildParent = new ChildParent()
                    {
                        Parent = _currentParents[i],
                        Child = newChild
                    };
                    AppData.Context.ChildParent.Add(newChildParent);
                }
            }
            else if(_child != null)
            {
                _child.LastName = fullName[0];
                _child.FirstName = fullName[1]; 
                _child.Patronymic = fullName[2];
                _child.Birthday = Birthday.SelectedDate;
                _child.ChildPhoto = _img;
                foreach (var item in _childParent)
                {
                    AppData.Context.ChildParent.Remove(item);
                }
                for (int i = 0; i < _currentParents.Count; i++)
                {
                    ChildParent newChildParent = new ChildParent()
                    {
                        Parent = _currentParents[i],
                        Child = _child
                    };
                    AppData.Context.ChildParent.Add(newChildParent);
                }
            }
            AppData.Context.SaveChanges();
            this.Close();
        }


        private void GroupName_DataContextChanged_1(object sender, DependencyPropertyChangedEventArgs e)
        {
            RoomName.Text = (GroupName.SelectedItem as Group).Room.RoomName;
        }

        private void ParentTb_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ChildParent parent = (sender as TextBlock).DataContext as ChildParent;
            ParentAddEditWindow parentAddEditWindow = new ParentAddEditWindow(parent.Parent);
            parentAddEditWindow.ShowDialog();
        }

        private void BtnReport_Click(object sender, RoutedEventArgs e)
        {
            ReportWindow reportWindow = new ReportWindow(_child, _childParent);
            reportWindow.ShowDialog();
        }

        private void DelParentBtn_Click(object sender, RoutedEventArgs e)
        {
            _currentParents.Remove((sender as Button).DataContext as Parent);
            parentList.ItemsSource = _currentParents;
            parentList.Items.Refresh();
        }

        private void ParentViewBtn_Click(object sender, RoutedEventArgs e)
        {
            Parent parent = (sender as Button).DataContext as Parent;
            ParentAddEditWindow parentAddEditWindow = new ParentAddEditWindow(parent);
            parentAddEditWindow.ShowDialog();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            var newParent = new ParentWindow();
            newParent.ShowDialog();
            if (newParent.DialogResult == true)
            {
                if (_currentParents.Contains(AppData.newParent))
                {
                    MessageBox.Show("Данный родитель уже выбран");
                    AppData.newChild = null;
                    return;
                }
                _currentParents.Add(AppData.newParent);
            }
            else
            {
                MessageBox.Show("Вы не выбрали родителя");
            }
            AppData.newChild = null;
            parentList.ItemsSource = _currentParents;
            parentList.Items.Refresh();
        }
    }
}
