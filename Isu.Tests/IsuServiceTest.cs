using Isu.Services;
using Isu.Tools;
using NUnit.Framework;
using System.IO;

namespace Isu.Tests
{
    public class Tests
    {
        private IsuService _isuService;

        [SetUp]
        public void Setup()
        {
            _isuService = new IsuService();
        }

        [Test]
        public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
        {
            var group = new Group("M3105");
            var student = _isuService.AddStudent(group, "Хащук Денис Васильевич");
            var newGroup = new Group("M3100");
            _isuService.AddStudent(newGroup, student.Name);
            Assert.AreEqual(student.Group,group);
            Assert.AreEqual(_isuService.GetStudent(student.ID),student);
        }

        [Test]
        public void ReachMaxStudentPerGroup_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                var group = new Group("M3105");

                for (int i = 0; i < 22; i++)
                {
                    var student = _isuService.AddStudent(group, i.ToString());
                    group.AddStudent(student);
                }
                _isuService.AddStudent(group, "Пинчук Анастасия Дмитриевна");
            });
        }

        [Test]
        public void CreateGroupWithInvalidName_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                Group group1 = _isuService.AddGroup("N4100");
                Group group2 = _isuService.AddGroup("M3508");
                Group group3 = _isuService.AddGroup("M31000");
            });
        }

        [Test]
        public void TransferStudentToAnotherGroup_GroupChanged()
        {
            var group = new Group("M3105");
            Student student = _isuService.AddStudent(group, "Хащук Денис Васильевич");
            Assert.AreEqual(student.Group.Name,group.Name);
            var newGroup = new Group("M3100");
            _isuService.ChangeStudentGroup(student, newGroup);
            Assert.AreEqual(_isuService.FindStudent("Хащук Денис Васильевич").Group.Name,newGroup.Name);
        }
    }
}