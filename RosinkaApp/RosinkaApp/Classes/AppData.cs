using RosinkaApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RosinkaApp.Classes
{
     class AppData
    {
        public static RosinkaDbEntities Context = new RosinkaDbEntities();
        public static Frame MainFrame = new Frame();
        public static User currentUser = new User();

        public static User newUser = new User();
        public static Parent newParent = new Parent();
        public static Child newChild = new Child();

    }
}
