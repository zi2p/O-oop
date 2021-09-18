namespace Isu.Services
{
    public class Student
    {
        public Student(Group group, string name, int id)
        {
            Name = name;
            Group = group;
            ID = id;
        }

        public Group Group { get; }
        public string Name { get; }
        public int ID { get; }
    }
}