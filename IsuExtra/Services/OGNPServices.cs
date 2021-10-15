using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Entities;
using IsuExtra.Entities;

namespace IsuExtra.Services
{
    public class OGNPServices
    {
        private readonly List<OGNP> _ognPs = new List<OGNP>();
        private readonly List<Timetable> _timetables = new List<Timetable>();
        public OGNP AddOGNP(MegaFaculty megaFaculty, string nameOfCurses, int numberOfGroup)
        {
            OGNP ognp = new OGNP(megaFaculty, nameOfCurses, numberOfGroup);
            _ognPs.Add(ognp);
            return ognp;
        }

        public MegaFaculty AddMegaFaculty(Group group)
        {
            var megaFaculty = new MegaFaculty(group);
            return megaFaculty;
        }

        public MegaFaculty AddMegaFaculty(string name)
        {
            var megaFaculty = new MegaFaculty(name);
            return megaFaculty;
        }

        public OGNP FindOGNP(string name)
        {
            return _ognPs.FirstOrDefault(ognp => ognp.Name == name);
        }

        public List<OGNP> GetList()
        {
            return _ognPs;
        }

        public void StudentRegistration(OGNP ognp, Student person)
        {
            var mISU = new MegaFaculty(person.Group);
            if (mISU.Name == ognp.MegaFaculty.Name) throw new Exception("you cannot enroll in courses of your faculty");
            if (ognp.FreeSeats == 0) throw new Exception("we're out of seats");
            foreach (Group varGroup in ognp.Groups)
            {
                if (!NoIntersections(person.Group, varGroup)) continue;
                varGroup.AddStudent(person);
                ognp.FreeSeats--;
                return;
            }

            throw new Exception("you cannot enroll in this course due to schedule intersections");
        }

        public void DeletStudentOGNP(OGNP ognp, Student person)
        {
            foreach (Group group in ognp.Groups)
            {
                if (!group.Students.Contains(person)) continue;
                group.DeleteStudent(person);
                ognp.FreeSeats++;
                return;
            }

            throw new Exception("you have not been enrolled in this course");
        }

        public bool NoIntersections(Group isuGroup, Group ognpGroup)
        {
            Timetable isuTable = null;
            Timetable ognpTable = null;
            foreach (Timetable tt in _timetables.Where(tt => tt.Group.Name == isuGroup.Name))
            {
                isuTable = tt;
            }

            foreach (Timetable tt in _timetables.Where(tt => tt.Group.Name == ognpGroup.Name))
            {
                ognpTable = tt;
            }

            if (isuTable == null || ognpTable == null) throw new Exception("group not found");
            for (int time = 0; time < 8; time++)
            {
                for (int day = 0; day < 6; day++)
                {
                    if (ognpTable.Table[day, time] == null) continue;
                    if (isuTable.Table[day, time] != null) return false;
                }
            }

            return true;
        }

        public void AddTimetable(Group group, string[,] table)
        {
            var timetable = new Timetable(table, group);
            _timetables.Add(timetable);
        }

        public void AddGroupTimetableInOGNP(OGNP ognp, Group group, string[,] table)
        {
            var timetable = new Timetable(table, new Group(group.Name));
            ognp.AddGroupTimetable(timetable);
            _timetables.Add(timetable);
        }

        public List<Student> GetListStudentsOGNPCourse(OGNP ognp)
        {
            var result = new List<Student>();

            foreach (Group group in ognp.Groups)
            {
                result.AddRange(group.Students);
            }

            return result;
        }

        public List<Student> GetListStudentsOGNPGroup(Group ognpGroup)
        {
            return ognpGroup.Students;
        }

        public List<Student> FindStudentsWhoHaveNotEnrolledInTheOGNP(Group isuGroup)
        {
            var result = new List<Student>();
            bool find = false;
            foreach (Student st in isuGroup.Students)
            {
                foreach (OGNP unused in from ognp in _ognPs from @group in from @group in ognp.Groups from person in @group.Students.Where(person => st.Name == person.Name) select @group select ognp)
                {
                    find = true;
                }

                if (!find) result.Add(st);
                find = false;
            }

            return result;
        }
    }
}