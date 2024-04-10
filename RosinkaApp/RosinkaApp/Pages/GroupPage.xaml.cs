using ControlzEx.Standard;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using RosinkaApp.Windows;

namespace RosinkaApp.Pages
{
    /// <summary>
    /// Interaction logic for GroupPage.xaml
    /// </summary>
    public partial class GroupPage : Page
    {
        private List<List<Group>> groupsList = new List<List<Group>>();

        public void Update()
        {
            groupsList.Clear();
            if (ViewBtn.IsChecked == true)
            {
                groupsList.Add(AppData.Context.Group.Where(p => p.GroupType.GroupTypeId == 1).ToList());
                groupsList.Add(AppData.Context.Group.Where(p => p.GroupType.GroupTypeId == 2).ToList());
                groupsList.Add(AppData.Context.Group.Where(p => p.GroupType.GroupTypeId == 3).ToList());
                groupsList.Add(AppData.Context.Group.Where(p => p.GroupType.GroupTypeId == 4).ToList());
                groupsList.Add(AppData.Context.Group.Where(p => p.GroupType.GroupTypeId == 5).ToList());
            }
            else
            {
                groupsList.Add(AppData.Context.Group.Where(p => p.GroupType.GroupTypeId == 1 && p.GroupStatus != "закрыта").ToList());
                groupsList.Add(AppData.Context.Group.Where(p => p.GroupType.GroupTypeId == 2 && p.GroupStatus != "закрыта").ToList());
                groupsList.Add(AppData.Context.Group.Where(p => p.GroupType.GroupTypeId == 3 && p.GroupStatus != "закрыта").ToList());
                groupsList.Add(AppData.Context.Group.Where(p => p.GroupType.GroupTypeId == 4 && p.GroupStatus != "закрыта").ToList());
                groupsList.Add(AppData.Context.Group.Where(p => p.GroupType.GroupTypeId == 5 && p.GroupStatus != "закрыта").ToList());
            }

            int i = 1;
            if (AppData.currentUser.Role.RoleId == 1)
                foreach (var group in groupsList)
                    group.Insert(0, new Group() { GroupName = "AddNewItem", GroupTypeId = i++ });

            nurseryList.ItemsSource = groupsList[0].ToList();
            juniorList.ItemsSource = groupsList[1].ToList();
            middleList.ItemsSource = groupsList[2].ToList();
            hightList.ItemsSource = groupsList[3].ToList();
            schoolList.ItemsSource = groupsList[4].ToList();
        }


        public GroupPage()
        {
            InitializeComponent();
            Update();

        }

        private void GroupClick_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (((sender as Border).DataContext as Group).GroupName == "AddNewItem")
            {
                GroupAddEditWindow groupAddEditWindow = new GroupAddEditWindow(null);
                groupAddEditWindow.ShowDialog();
                Update();
                return;
            }
            else
            {
                GroupAddEditWindow groupAddEditWindow = new GroupAddEditWindow((sender as Border).DataContext as Group);
                groupAddEditWindow.ShowDialog();
                Update();
                return;
            }
            
        }

        private void ViewBtn_Checked(object sender, RoutedEventArgs e)
        {
            Update();
        }

        private void ViewBtn_Unchecked(object sender, RoutedEventArgs e)
        {
            Update();
        }
    }
}
