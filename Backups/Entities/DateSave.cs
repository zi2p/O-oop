using System;

namespace Backups.Entities
{
    public class DateSave
    {
        public DateSave(int day, int month, int year)
        {
            Day = day;
            Month = month;
            Year = year;
        }

        public int Day { get; }
        public int Month { get; }
        public int Year { get; }
        public static bool operator ==(DateSave dt1, DateSave dt2)
        {
            return dt1.Day == dt2.Day && dt1.Month == dt2.Month && dt1.Year == dt2.Year;
        }

        public static bool operator !=(DateSave dt1, DateSave dt2)
        {
            return !(dt1 == dt2);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((DateSave)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Day, Month, Year);
        }

        protected bool Equals(DateSave other)
        {
            return Day == other.Day && Month == other.Month && Year == other.Year;
        }
    }
}