using System.Collections.Generic;
using Isu.Tools;

namespace Isu.Entities
{
    public class Group
    {
        public Group(string name)
        {
            if (name[1] != '3' || name.Length != 5)
            {
                throw new IsuException(
                "error: group's name is not correct, name must be look like *3XYY, where X - course number, YY - group number, * - MegaFaculty");
            }

            if (name[2] != '1' && name[2] != '2' && name[2] != '3' && name[2] != '4')
            {
                throw new IsuException(
                    "error: group's name must contain the course number from 1 to 4");
            }

            Name = name;
            CourseNumber = int.Parse(name[2].ToString());

            Students = new List<Student>();
        }

        public string Name { get; }
        public int CourseNumber { get; }
        public List<Student> Students { get; }

        public void AddStudent(Student student)
        {
            if (Students.Count > 22) throw new IsuException("error: there are many students in this group, select another group \n");
            Students.Add(student);
        }

        public void DeleteStudent(Student student)
        {
            Students.Remove(student);
        }

        public bool HasStudent(Student student)
        {
            return Students.Contains(student);
        }
    }
}