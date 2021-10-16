using Isu.Entities;

namespace IsuExtra.Entities
{
    public class Timetable // у ученика может быть максимум 8 пар в день, учится он 6 дней в неделю (воскресенье выходной)
    {
        public Timetable(Lesson[,] table, Group group)
        {
            Table = new Lesson[6, 8]
            {
                { null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null },
            };
            Table = table;
            Group = group;
        }

        public Lesson[,] Table { get; set; }
        public Group Group { get; }
    }
}