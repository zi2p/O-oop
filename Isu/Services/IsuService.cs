﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using Isu.Tools;

namespace Isu.Services
{
    public class IsuService
    {
        private List<CourseNumber> _listOfCourses = new List<CourseNumber>();
        private int _smallestFreeId = 100000;
        public Group AddGroup(string name)
        {
            if (FindGroup(name) != null) throw new IsuException("error: group with this name exists \n");
            var group = new Group(name);
            foreach (CourseNumber cn in _listOfCourses.Where(cn => name[2] == cn.Number))
            {
                cn.AddGroup(@group);
                return @group;
            }

            var courseNumber =
                new CourseNumber(name[2], @group);
            _listOfCourses.Add(courseNumber);
            return @group;
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
            foreach (Student student in
                from cn in _listOfCourses
                from @group in cn.Groups
                from student in @group.Students
                where student.ID == id
                select student)
                return student;

            throw new IsuException("error: wrong student's id, student not found \n :c \n");
        }

        public Student FindStudent(string name)
        {
            foreach (Student student in from cn in _listOfCourses
                from @group in cn.Groups
                from student in @group.Students
                where student.Name == name
                select student)
                return student;
            return null;

            // другой линк через .Where
        }

        public List<Student> FindStudents(string groupName)
        {
            foreach (Group @group in from cn in _listOfCourses from @group in cn.Groups where @group.Name == groupName select @group)
                return @group.Students;

            return null;
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            var classmates = new List<Student>();
            foreach (Group group in _listOfCourses.Where(cn => cn == courseNumber).SelectMany(cn => cn.Groups))
                classmates.AddRange(group.Students.ToArray());

            if (classmates.Count == 0) throw new IsuException("error: wrong course's name \n");
            return classmates;
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            foreach (CourseNumber cn in _listOfCourses.Where(cn => cn == courseNumber))
                return cn.Groups;

            return null;
        }

        public Group FindGroup(string groupName)
        {
            foreach (Group @group in from cn in _listOfCourses from @group in cn.Groups where @group.Name == groupName select @group)
                return @group;

            return null;
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            student.Group.DeleteStudent(student); // удалить студента из старой группы
            var newStudent = new Student(newGroup, student.Name, student.ID);
            newGroup.AddStudent(newStudent); // создать студентна как бы заново
        }
    }
}