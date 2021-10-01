using Isu.Services;

namespace Isu.Entities
{
    public class Student
    {
        public Student(Group group, string name, int id)
        {
            Name = name;
            Group = group;
            Id = id;
        }

        public Group Group { get; set; }
        public string Name { get; }
        public int Id { get; }
    }
}