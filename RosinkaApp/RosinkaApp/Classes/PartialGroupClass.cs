using RosinkaApp.Entities;
using RosinkaApp.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace RosinkaApp.Entities 
{
    public partial class Group
    {
        public Visibility VisualCheck => GroupName == "AddNewItem" ? Visibility.Collapsed : Visibility.Visible;
        public string AddBackImage => VisualCheck == Visibility.Collapsed ? "../Resources/add.png" : null;
    }
}
