using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Tools;

namespace Isu.Services
{
    public class Group
    {
        private string _group;
        private List<Student> _students = new List<Student>();

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

            _group = name;
        }

        public void AddStudent(Student student)
        {
            if (_students.Count > 22) throw new IsuException("error: there are many students in this group, select another group \n");
            _students.Add(student);
        }

        public Student FindStudent(string name)
        {
            return _students.FirstOrDefault(student => student.GetName() == name);
        }

        public void DeleteStudent(Student student)
        {
            _students.Remove(student);
        }

        public List<Student> GetList()
        {
            return _students;
        }

        public string GetName()
        {
            return _group;
        }
    }
}