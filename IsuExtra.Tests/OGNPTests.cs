using System;
using System.Collections.Generic;
using Isu.Entities;
using Isu.Services;
using IsuExtra.Entities;
using IsuExtra.Services;
using NUnit.Framework;

namespace IsuExtra.Tests
{
    public class Tests
    {
        private OGNPServices _ognpService;
        private IsuService _isuService;

        [SetUp]
        public void Setup()
        {
            _ognpService = new OGNPServices();
            _isuService = new IsuService();
        }

        [Test]
        public void AddingANewOGNP()
        {
            string nameOGNP = "physical processes";
            var megaFaculty = _ognpService.AddMegaFaculty("FTF");
            OGNP newOGNP = _ognpService.AddOGNP(megaFaculty, nameOGNP, 2);
            Assert.Contains(newOGNP, _ognpService.GetList());
        }

        [Test]
        public void RecordingAStudentOnACertainOGNP()
        {
            Group isuGroup = _isuService.AddGroup("M3200");
            Student student = _isuService.AddStudent(isuGroup, "Шевченко Валерий");
            string nameOGNP1 = "physical processes";
            var megaFaculty1 = _ognpService.AddMegaFaculty("FTF");
            OGNP newOGNP1 = _ognpService.AddOGNP(megaFaculty1, nameOGNP1, 2);
            string nameOGNP2 = "application development";
            var megaFaculty2 = _ognpService.AddMegaFaculty("TINT");
            OGNP newOGNP2 = _ognpService.AddOGNP(megaFaculty2, nameOGNP2, 2);
            string[,] tableISU = new string[6, 8];
            tableISU[0, 1] = "OOP";
            tableISU[0, 2] = "OOP";
            string[,] tableOGNP1 = new string[6, 8];
            tableOGNP1[0, 1] = "modeling";
            string[,] tableOGNP2 = new string[6, 8];
            tableOGNP2[0, 3] = "modeling";
            _ognpService.AddTimetable(isuGroup, tableISU);
            _ognpService.AddGroupTimetableInOGNP(newOGNP1, newOGNP1.Groups[0], tableOGNP1);
            _ognpService.AddGroupTimetableInOGNP(newOGNP1, newOGNP1.Groups[1], tableOGNP2);
            Assert.Catch<Exception>(() =>
            {
                _ognpService.StudentRegistration(newOGNP2,student);
            });
            _ognpService.StudentRegistration(newOGNP1,student);
            Assert.Contains(student, newOGNP1.Groups[1].Students);
            CollectionAssert.DoesNotContain(newOGNP1.Groups[0].Students, student);
        }

        [Test]
        public void RemoveAStudentFromTheRecord()
        {
            Group isuGroup = _isuService.AddGroup("M3200");
            Student student = _isuService.AddStudent(isuGroup, "Шевченко Валерий");
            string nameOGNP1 = "physical processes";
            var megaFaculty1 = _ognpService.AddMegaFaculty("FTF");
            OGNP newOGNP1 = _ognpService.AddOGNP(megaFaculty1, nameOGNP1, 1);
            string[,] tableISU = new string[6, 8];
            tableISU[0, 1] = "OOP";
            tableISU[0, 2] = "OOP";
            string[,] tableOGNP1 = new string[6, 8];
            tableOGNP1[0, 3] = "modeling";
            _ognpService.AddTimetable(isuGroup, tableISU);
            _ognpService.AddGroupTimetableInOGNP(newOGNP1, newOGNP1.Groups[0], tableOGNP1);
            _ognpService.StudentRegistration(newOGNP1,student);
            Assert.Contains(student, newOGNP1.Groups[0].Students);
            _ognpService.DeletStudentOGNP(newOGNP1,student);
            CollectionAssert.DoesNotContain(newOGNP1.Groups[0].Students, student);
        }

        [Test]
        public void GettingAListOfStudentsByOGNP()
        {
            Group isuGroup = _isuService.AddGroup("M3200");
            Student student1 = _isuService.AddStudent(isuGroup, "Шевченко Валерий");
            Student student2 = _isuService.AddStudent(isuGroup, "Андреев Артем");
            Student student3 = _isuService.AddStudent(isuGroup, "Кутузов Михаил");
            Student student4 = _isuService.AddStudent(isuGroup, "Профе Диана");
            string nameOGNP = "physical processes";
            var megaFaculty = _ognpService.AddMegaFaculty("FTF");
            OGNP newOGNP = _ognpService.AddOGNP(megaFaculty, nameOGNP, 2);
            string[,] tableISU = new string[6, 8];
            tableISU[0, 1] = "OOP";
            tableISU[0, 2] = "OOP";
            string[,] tableOGNP1 = new string[6, 8];
            tableOGNP1[0, 3] = "modeling";
            _ognpService.AddTimetable(isuGroup, tableISU);
            _ognpService.AddGroupTimetableInOGNP(newOGNP, newOGNP.Groups[0], tableOGNP1);
            _ognpService.StudentRegistration(newOGNP,student1);
            _ognpService.StudentRegistration(newOGNP,student2);
            _ognpService.StudentRegistration(newOGNP,student3);
            _ognpService.StudentRegistration(newOGNP,student4);
            List<Student> students = _ognpService.GetListStudentsOGNPCourse(newOGNP);
            Assert.Contains(student1, students);
            Assert.Contains(student2, students);
            Assert.Contains(student3, students);
            Assert.Contains(student4, students);
        }
        
        [Test]
        public void GettingAListOfStudentsForACertainGroupOfOGNP()
        {
            Group isuGroup = _isuService.AddGroup("M3200");
            Student student1 = _isuService.AddStudent(isuGroup, "Шевченко Валерий");
            Student student2 = _isuService.AddStudent(isuGroup, "Андреев Артем");
            Student student3 = _isuService.AddStudent(isuGroup, "Кутузов Михаил");
            Student student4 = _isuService.AddStudent(isuGroup, "Профе Диана");
            string nameOGNP = "physical processes";
            var megaFaculty = _ognpService.AddMegaFaculty("FTF");
            OGNP newOGNP = _ognpService.AddOGNP(megaFaculty, nameOGNP, 1);
            string[,] tableISU = new string[6, 8];
            tableISU[0, 1] = "OOP";
            tableISU[0, 2] = "OOP";
            string[,] tableOGNP1 = new string[6, 8];
            tableOGNP1[0, 3] = "modeling";
            _ognpService.AddTimetable(isuGroup, tableISU);
            _ognpService.AddGroupTimetableInOGNP(newOGNP, newOGNP.Groups[0], tableOGNP1);
            _ognpService.StudentRegistration(newOGNP,student1);
            _ognpService.StudentRegistration(newOGNP,student2);
            _ognpService.StudentRegistration(newOGNP,student3);
            _ognpService.StudentRegistration(newOGNP,student4);
            List<Student> students = _ognpService.GetListStudentsOGNPGroup(newOGNP.Groups[0]);
            Assert.Contains(student1, students);
            Assert.Contains(student2, students);
            Assert.Contains(student3, students);
            Assert.Contains(student4, students);
        }
        
        [Test]
        public void GettingAListOfStudentsWhoHaveNotEnrolledInTheOGNP()
        {
            Group isuGroup = _isuService.AddGroup("M3200");
            Student student1 = _isuService.AddStudent(isuGroup, "Шевченко Валерий");
            Student student2 = _isuService.AddStudent(isuGroup, "Андреев Артем");
            Student student3 = _isuService.AddStudent(isuGroup, "Кутузов Михаил");
            Student student4 = _isuService.AddStudent(isuGroup, "Профе Диана");
            string nameOGNP = "physical processes";
            var megaFaculty = _ognpService.AddMegaFaculty("FTF");
            OGNP newOGNP = _ognpService.AddOGNP(megaFaculty, nameOGNP, 1);
            string[,] tableISU = new string[6, 8];
            tableISU[0, 1] = "OOP";
            tableISU[0, 2] = "OOP";
            string[,] tableOGNP = new string[6, 8];
            tableOGNP[0, 3] = "modeling";
            _ognpService.AddTimetable(isuGroup, tableISU);
            _ognpService.AddGroupTimetableInOGNP(newOGNP, newOGNP.Groups[0], tableOGNP);
            _ognpService.StudentRegistration(newOGNP,student1);
            _ognpService.StudentRegistration(newOGNP,student2);
            OGNP newOGNP1 = _ognpService.AddOGNP(megaFaculty, "solving physical problems", 1);
            string[,] tableOGNP1 = new string[6, 8];
            tableOGNP1[0, 3] = "modeling";
            _ognpService.AddGroupTimetableInOGNP(newOGNP1, newOGNP1.Groups[0], tableOGNP1);
            _ognpService.StudentRegistration(newOGNP1,student3);
            List<Student> students = _ognpService.FindStudentsWhoHaveNotEnrolledInTheOGNP(isuGroup);
            Assert.Contains(student4, students);
        }
    }
}