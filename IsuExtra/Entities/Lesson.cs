namespace IsuExtra.Entities
{
    public class Lesson
    {
        public Lesson(string teacher, string auditorium)
        {
            Auditorium = auditorium;
            Teacher = teacher;
        }

        public string Teacher { get; }
        public string Auditorium { get; }
    }
}