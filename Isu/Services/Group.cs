using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Tools;

namespace Isu.Services
{
    public class Group
    {
        public Group(string name)
        {
            if (name[0] != 'M' || name[1] != '3' || name.Length != 5)
            {
                throw new IsuException(
                    "error: group's name is not correct, name mast be look like M3XYY, where X - course number, YY - group number");
            }

            if (name[2] != '1' && name[2] != '2' && name[2] != '3' && name[2] != '4')
            {
                throw new IsuException(
                    "error: group's name must contain the course number from 1 to 4");
            }

            Name = name;
        }

        public string Name { get; set; }
        public List<Student> Students { get; private set; }

        public void AddStudent(Student student)
        {
            if (Students.Count > 22) throw new IsuException("error: there are many students in this group, select another group \n");
            Students.Add(student);
        }

        public Student FindStudent(string name)
        {
            return Students.FirstOrDefault(student => student.Name == name);
        }

        public void DeleteStudent(Student student)
        {
            Students.Remove(student);
        }
    }
}