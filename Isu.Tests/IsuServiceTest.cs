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
            _isuService.AddStudent(group, "Хащук Денис Васильевич");
            var newGroup = new Group("M3100");
            _isuService.AddStudent(newGroup, "Хащук Денис Васильевич");
        }

        [Test]
        public void ReachMaxStudentPerGroup_ThrowException()
        {
            var group = new Group("M3105");
            var sr = new StreamReader("M3105.txt");
            while (!sr.EndOfStream)
            {
                _isuService.AddStudent(group, sr.ReadLine());
            }
            sr.Close();
            _isuService.AddStudent(group, "Пинчук Анастасия Дмитриевна");
        }

        [Test]
        public void CreateGroupWithInvalidName_ThrowException()
        {
            Group group1 = _isuService.AddGroup("N4100");
            Group group2 = _isuService.AddGroup("M3508");
            Group group3 = _isuService.AddGroup("M31000");
        }

        [Test]
        public void TransferStudentToAnotherGroup_GroupChanged()
        {
            var group = new Group("M3105");
            Student student = _isuService.AddStudent(group, "Хащук Денис Васильевич");
            var newGroup = new Group("M3100");
            _isuService.ChangeStudentGroup(student, newGroup);
        }
    }
}