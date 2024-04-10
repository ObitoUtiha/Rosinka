using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RosinkaApp.Entities
{
    public partial class Child
    {
        public string ChildFullName => $"{LastName} {FirstName} {Patronymic}";

    }
}
