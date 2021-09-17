namespace Isu.Services
{
    public class Student
    {
        private Group group;
        private string name;
        private int id;

        public Student(Group group, string name, int id)
        {
            this.name = name;
            this.group = group;
            this.id = id;
        }

        public string GetName()
        {
            return name;
        }

        public int GetID()
        {
            return id;
        }

        public Group GetGroup()
        {
            return @group;
        }
    }
}