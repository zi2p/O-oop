using System;
using System.Collections.Generic;

namespace Isu.Services
{
    public class Group
    {
        private string group;
        private List<Student> students = new List<Student>();
        
        public Group(string group = "M3100")
        {
            this.group = group;
        }
        
        // public void AddGroup(string nameGroup)
        // {
        //     var gr = new Group(nameGroup);
        //     listGroups.AddGroups(gr);
        // }

        public void AddStudents(Student name)
        {
            students.Add(name);
        }

        public List<Student> GetList()
        {
            return students;
        }

        public string GetNameOfGroup()
        {
            return group;
        }
    }
}