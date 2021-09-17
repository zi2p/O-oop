using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Tools;

namespace Isu.Services
{
    public class IsuService
    {
        private List<CourseNumber> listOfCourses = new List<CourseNumber>();
        private int smallestFreeID = 100000;
        public Group AddGroup(string name)
        {
            if (FindGroup(name) != null) throw new IsuException("error: group with this name exists \n");
            var group = new Group(name);
            foreach (CourseNumber cn in listOfCourses.Where(cn => name[2] == cn.GetNumber()))
            {
                cn.AddGroup(@group);
                return @group;
            }

            var courseNumber =
                new CourseNumber(name[2], @group); // если это первая группа которая существует на курсе
            listOfCourses.Add(courseNumber);
            return @group;
        }

        public Student AddStudent(Group group, string name)
        {
            if (FindStudent(name) != null) throw new IsuException("error: student with this name exists \n");
            if (group.GetList().Count > 22) throw new IsuException("error: there are many students in this group, select another group \n");
            var student = new Student(group, name, smallestFreeID);
            group.AddStudents(student);
            smallestFreeID++;
            return student;
        }

        public Student GetStudent(int id)
        {
            if (id >= smallestFreeID) throw new IsuException("error: wrong student's id \n");
            foreach (Student student in from cn in listOfCourses from @group in cn.GetList() from student in @group.GetList() where student.GetID() == id select student)
                return student;

            throw new IsuException("error: wrong student's id, student not found \n :c \n");
        }

        public Student FindStudent(string name)
        {
            foreach (Student student in from cn in listOfCourses
                from @group in cn.GetList()
                from student in @group.GetList()
                where student.GetName() == name
                select student)
                return student;
            throw new IsuException("error: wrong student's name \n");
        }

        public List<Student> FindStudents(string groupName)
        {
            foreach (Group @group in from cn in listOfCourses from @group in cn.GetList() where @group.GetNameOfGroup() == groupName select @group)
                return @group.GetList();

            throw new IsuException("error: wrong group's name \n");
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            var classmates = new List<Student>();
            foreach (Group group in listOfCourses.Where(cn => cn == courseNumber).SelectMany(cn => cn.GetList()))
                classmates.AddRange(group.GetList().ToArray());

            if (classmates.Count == 0) throw new IsuException("error: wrong course's name \n");
            return classmates;
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            foreach (CourseNumber cn in listOfCourses.Where(cn => cn == courseNumber))
                return cn.GetList();

            throw new IsuException("error: wrong course's name \n");
        }

        public Group FindGroup(string groupName)
        {
            foreach (Group @group in from cn in listOfCourses from @group in cn.GetList() where @group.GetNameOfGroup() == groupName select @group)
                return @group;

            throw new IsuException("error: wrong group's name, group's name must look like M3XYY, where X - course number, YY - group number \n");
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            if (FindStudent(student.GetName()) == student)
            {
                student.GetGroup().DeleteStudent(student); // удалить студента из старой группы
                AddStudent(newGroup, student.GetName());  // создать студентна как бы заново
            }
            else
            {
                throw new IsuException("error: wrong group or student \n");
            }
        }
    }
}