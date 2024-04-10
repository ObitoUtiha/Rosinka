using MaterialDesignThemes.Wpf;
using RosinkaApp.Classes;
using RosinkaApp.Entities;
using RosinkaApp.Pages;
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
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace RosinkaApp.Windows
{
    /// <summary>
    /// Interaction logic for GroupAddEditWindow.xaml
    /// </summary>
    public partial class GroupAddEditWindow : Window
    {
        List<Child> _childList = new List<Child> ();
        List<User> _userList  = new List<User> ();
        List<GroupMentor> _groupMentor = new List<GroupMentor>();
        Group _currentGroup = new Group();
        List<GroupMentor> _currentGroupMentors = new List<GroupMentor>();
        List<ChildGroup> _currentChildGroup = new List<ChildGroup> ();
        public GroupAddEditWindow(Group currentGroup)
        {
            InitializeComponent();
            groupType.ItemsSource = AppData.Context.GroupType.ToList();
            RoomCmb.ItemsSource = AppData.Context.Room.ToList();
            _groupMentor = AppData.Context.GroupMentor.ToList();
            _currentChildGroup = AppData.Context.ChildGroup.ToList();
            _currentGroup = currentGroup;
            if(currentGroup != null )
            {
                groupName.Text = currentGroup.GroupName;
                groupType.SelectedItem = currentGroup.GroupType;
                _userList = _groupMentor.Where(p=>p.GroupId == currentGroup.GoupID).Select(p => p.User).ToList();
                _childList = AppData.Context.ChildGroup.Where(p => p.GroupId == currentGroup.GoupID).Select(p=>p.Child).ToList();
                childList.ItemsSource = _childList;
                mentorList.ItemsSource = _userList;
                StatusTb.Text = currentGroup.GroupStatus;
                RoomCmb.SelectedItem = currentGroup.Room;
                _currentGroupMentors = _groupMentor.Where(p => p.Group == _currentGroup).ToList();
                _currentChildGroup = _currentChildGroup.Where(p => p.Group == _currentGroup).ToList();
            }
            
        }


        private void AddMentor_Click(object sender, RoutedEventArgs e)
        {
            var newUser = new MentorWindow();
            newUser.ShowDialog();
            if (newUser.DialogResult == true)
            {
                if(_userList.Contains(AppData.newUser))
                {
                    MessageBox.Show("Данный воспитатель уже выбран");
                    AppData.newUser = null;
                    return;
                }
                _userList.Add(AppData.newUser);
            }
            else
            {   
                MessageBox.Show("Вы не выбрали воспитателя");
            }
            AppData.newUser = null;
            mentorList.ItemsSource = _userList;
            mentorList.Items.Refresh();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(StatusTb.Text) || _childList == null || _childList.Count == 0 || _userList == null || _userList.Count == 0 || RoomCmb.SelectedIndex == -1 || groupType.SelectedIndex == -1||
                string.IsNullOrWhiteSpace(groupName.Text))
            {
                MessageBox.Show("Проверьте корректность введённых данных");
                return;
            }
            if(_currentGroup != null)
            {
                _currentGroup.GroupName = groupName.Text;
                _currentGroup.GroupType = groupType.SelectedItem as GroupType;
                _currentGroup.Room = RoomCmb.SelectedItem as Room;
                _currentGroup.GroupStatus = StatusTb.Text.ToLower();
                foreach(var item in _currentGroupMentors)
                {
                   AppData.Context.GroupMentor.Remove(item);
                }
                foreach (var item in _currentChildGroup)
                {
                    AppData.Context.ChildGroup.Remove(item);
                }
                for (int i = 0; i < _childList.Count; i++)
                {
                    ChildGroup newChildGroup = new ChildGroup()
                    {
                        Child = _childList[i],
                        Group = _currentGroup
                    };
                    AppData.Context.ChildGroup.Add(newChildGroup);
                }
                for (int i = 0; i < _userList.Count; i++)
                {
                    GroupMentor newMentorGroup = new GroupMentor()
                    {
                        User = _userList[i],
                        Group = _currentGroup
                    };
                    AppData.Context.GroupMentor.Add(newMentorGroup);
                }
            }
            else 
            {
                Group newGroup = new Group
                {
                    GroupName = groupName.Text,
                    GroupType = groupType.SelectedItem as GroupType,
                    Room = RoomCmb.SelectedItem as Room,
                    GroupStatus = StatusTb.Text.ToLower()
                };
                AppData.Context.Group.Add(newGroup);
                AppData.Context.SaveChanges();
                for (int i = 0; i < _childList.Count; i++)
                {
                    ChildGroup newChildGroup = new ChildGroup()
                    {
                        Child = _childList[i],
                        Group = newGroup
                    };
                    AppData.Context.ChildGroup.Add(newChildGroup);
                }
                for (int i = 0; i < _userList.Count; i++)
                {
                    GroupMentor newMentorGroup = new GroupMentor()
                    {
                        User = _userList[i],
                        Group = newGroup
                    };
                    AppData.Context.GroupMentor.Add(newMentorGroup);
                }
            }
            AppData.Context.SaveChanges();
            this.Close();
        }

        private void ChildViewBtn_Click(object sender, RoutedEventArgs e)
        {
            ChildAddEditWindow mentorAddEditWindow = new ChildAddEditWindow((sender as Button).DataContext as Child);
            mentorAddEditWindow.ShowDialog();
        }

        private void DelChildBtn_Click(object sender, RoutedEventArgs e)
        {
            _childList.Remove((sender as Button).DataContext as Child);
            childList.ItemsSource = _childList;
            childList.Items.Refresh();
        }

        private void DelMentordBtn_Click(object sender, RoutedEventArgs e)
        {
            _userList.Remove((sender as Button).DataContext as User);
            mentorList.ItemsSource = _userList;
            mentorList.Items.Refresh();
        }

        private void MentorViewBtn_Click(object sender, RoutedEventArgs e)
        {
            MentorAddEditWindow mentorAddEditWindow = new MentorAddEditWindow((sender as Button).DataContext as User);
            mentorAddEditWindow.ShowDialog();
        }

        private void AddChild_Click(object sender, RoutedEventArgs e)
        {
            var newChild = new ChildWindow();
            newChild.ShowDialog();
            if (newChild.DialogResult == true)
            {
                if (_childList.Contains(AppData.newChild))
                {
                    MessageBox.Show("Данный воспитатель уже выбран");
                    AppData.newChild = null;
                    return;
                }
                _childList.Add(AppData.newChild);
            }
            else
            {
                MessageBox.Show("Вы не выбрали воспитателя");
            }
            AppData.newChild = null;
            childList.ItemsSource = _childList;
            childList.Items.Refresh();
        }
    }
}
