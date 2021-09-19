using Isu.Services;
using Isu.Tools;
using NUnit.Framework;
using Isu.Entities;

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
            Group group = _isuService.AddGroup("M3105");
            Student student = _isuService.AddStudent(group, "Хащук Денис Васильевич");
            Assert.Contains(student, group.Students);
        }

        [Test]
        public void ReachMaxStudentPerGroup_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                Group group = _isuService.AddGroup("M3105");

                for (int i = 0; i < 22; i++)
                {
                    Student student = _isuService.AddStudent(group, i.ToString());
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
            Group group = _isuService.AddGroup("M3105");
            Student student = _isuService.AddStudent(group, "Хащук Денис Васильевич");

            Group newGroup = _isuService.AddGroup("M3100");
            _isuService.ChangeStudentGroup(student, newGroup);
            Assert.That(newGroup.Name.Equals(student.Group.Name));
            Assert.Contains(student, newGroup.Students);
            CollectionAssert.DoesNotContain(group.Students, student);
        }
    }
}