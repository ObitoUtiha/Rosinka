using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RosinkaApp.Entities
{
    public partial class User
    {
        public string MentorFullName => $"{LastName} {FirstName} {Patronymic}";

        public string MentorGroup => string.Join(", ", GroupMentor.Select(p => p.Group.GroupName));
        public List<Group> groupsUserList => GroupMentor.Select(p => p.Group).ToList();
    }
}
