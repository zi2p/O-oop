using System.Collections.Generic;
using System.Linq;
using Isu.Entities;
using Isu.Tools;

namespace Isu.Services
{
    public class IsuService
    {
        private List<CourseNumber> _сourses = new List<CourseNumber>();
        private int _smallestFreeId = 100000;
        public Group AddGroup(string name)
        {
            // if (FindGroup(name) != null) throw new IsuException("error: group with this name exists \n");
            var group = new Group(name);
            CourseNumber course = _сourses.Find(courseNumber => courseNumber.Number == group.CourseNumber);
            course?.AddGroup(group);
            return group;
        }

        public Student AddStudent(Group group, string name)
        {
            var student = new Student(group, name, _smallestFreeId);
            group.AddStudent(student);
            _smallestFreeId++;
            return student;
        }

        public Student GetStudent(int id)
        {
            if (id >= _smallestFreeId) throw new IsuException("error: wrong student's id \n");
            foreach (Student student in _сourses.SelectMany(course => course.Groups.Select(@group => @group.Students.Find(st => st.Id == id)).Where(student => student != null)))
            {
                return student;
            }

            throw new IsuException("error: wrong student's id, student not found \n :c \n");
        }

        public Student FindStudent(string name)
        {
            return _сourses.SelectMany(course => course.Groups.Select(@group => @group.Students.Find(st => st.Name == name)).Where(student => student != null)).FirstOrDefault();
        }

        public List<Student> FindStudents(string groupName)
        {
            foreach (CourseNumber course in _сourses)
            {
                Group group = course.Groups.Find(group => group.Name.Equals(groupName));
                if (group != null)
                    return group.Students;
            }

            return null;
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            var students = new List<Student>();
            foreach (Group group in FindGroups(courseNumber))
            {
                students.AddRange(group.Students);
            }

            return students;
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            return courseNumber.Groups;
        }

        public Group FindGroup(string groupName)
        {
            return _сourses.Select(course => course.Groups.Find(gr => gr.Name.Equals(groupName))).FirstOrDefault(@group => @group != null);
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            newGroup.AddStudent(student);
            student.Group.DeleteStudent(student);
            student.Group = newGroup;
        }
    }
}