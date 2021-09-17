namespace Isu.Services
{
    public class Student
    {
        private Group _group;
        private string _name;
        private int _id;

        public Student(Group group, string name, int id)
        {
            _name = name;
            _group = group;
            _id = id;
        }

        public string GetName()
        {
            return _name;
        }

        public int GetID()
        {
            return _id;
        }

        public Group GetGroup()
        {
            return _group;
        }
    }
}