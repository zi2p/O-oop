using System.Collections.Generic;
using System.Linq;

namespace Isu.Services
{
    public class CourseNumber
    {
        public CourseNumber(char number, Group group)
        {
            Number = number;
            Groups.Add(group);
        }

        public char Number { get; }
        public List<Group> Groups { get; private set; }
        public void AddGroup(Group group)
        {
            Groups.Add(group);
        }
    }
}