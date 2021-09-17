using System.Collections.Generic;

namespace Isu.Services
{
    public class CourseNumber
    {
        private char _number;
        private List<Group> _listOfGroups = new List<Group>();

        public CourseNumber(char number, Group group)
        {
            _number = number;
            _listOfGroups.Add(group);
        }

        public void AddGroup(Group group)
        {
            _listOfGroups.Add(group);
        }

        public char GetNumber()
        {
            return _number;
        }

        public List<Group> GetList()
        {
            return _listOfGroups;
        }
    }
}