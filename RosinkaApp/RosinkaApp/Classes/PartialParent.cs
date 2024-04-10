using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RosinkaApp.Entities
{
    public partial class Parent
    {
        public string ParentBirthday
        {
            get
            {
                if(Passport == null)
                {
                    return "";
                }
                return Passport.Birthday.Value.ToShortDateString();
            }
            set { }
        }

        public string ParentFullName
        {
            get
            {
                return $"{LastName} {FirstName} {Patronymic}";
            }
            set { }
        }
            
        public string ParentChilds => string.Join(", ", ChildParent.Select(p=>p.Child.ChildFullName));
    }
}
