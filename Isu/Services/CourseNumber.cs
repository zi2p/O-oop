using System.Collections.Generic;

namespace Isu.Services
{
    public class CourseNumber
    {
        private string number;
        private List<Group> listOfGroups = new List<Group>();

        public CourseNumber(string nameOfCourse, Group group)
        {
            number = nameOfCourse;
            listOfGroups.Add(group);
        }

        public void AddGroup(Group group)
        {
            listOfGroups.Add(group);
        }

        public string GetNumber()
        {
            return number;
        }

        public List<Group> GetList()
        {
            return listOfGroups;
        }
        
    }
}