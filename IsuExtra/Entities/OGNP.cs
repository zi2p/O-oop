using System;
using System.Collections.Generic;
using Isu.Entities;

namespace IsuExtra.Entities
{
    public class OGNP
    {
        public OGNP(MegaFaculty megaFaculty, string nameOfCurses, int numberOfGroup)
        {
            MegaFaculty = megaFaculty;
            NumberOfSeats = numberOfGroup * 22;
            Name = nameOfCurses;
            FreeSeats = numberOfGroup * 22;
            Timetables = new List<Timetable>();
            Groups = new List<Group>();
            for (int i = 0; i < numberOfGroup; i++)
            {
                Groups.Add(new Group((i + 33200).ToString()));
            }
        }

        public string Name { get; }
        public MegaFaculty MegaFaculty { get; }
        public int NumberOfSeats { get; set; }

        public int FreeSeats { get; set; }
        public List<Timetable> Timetables { get; }

        public List<Group> Groups { get; }

        public void AddGroup(string nameGroup)
        {
            FreeSeats += 22;
            NumberOfSeats += 22;
        }

        public void AddGroupTimetable(Timetable table)
        {
            Timetables.Add(table);
        }
    }
}