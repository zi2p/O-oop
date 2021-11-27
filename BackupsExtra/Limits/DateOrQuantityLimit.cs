using System;
using Backups.Entities;
using BackupsExtra.Tools;

namespace BackupsExtra.Limits
{
    public class DateOrQuantityLimit : ILimit
    {
        protected DateOrQuantityLimit(DateTime date, int count)
        {
            if (count <= 0)
            {
                throw new BackupsExtraException("all points are deleted");
            }

            Quantity = count;
            Date = date;
        }

        public DateTime Date { get; }
        public int Quantity { get; }

        public static DateOrQuantityLimit CreateInstance(DateTime date, int count)
        {
            return new DateOrQuantityLimit(date, count);
        }

        public void Limit(BackupJob bj) // сохраняет наибольшее число точек возврата
        {
            if (bj.RestorePoints.Count <= Quantity) return;
            if (bj.RestorePoints[Quantity - 1].GetDate().Year <= Date.Year &&
                bj.RestorePoints[Quantity - 1].GetDate().Month <= Date.Month &&
                bj.RestorePoints[Quantity - 1].GetDate().Day <= Date.Day)
            {
                bj.RestorePoints.RemoveAt(Quantity);
            }

            int i;
            for (i = Quantity - 1; i < bj.RestorePoints.Count; i++)
            {
                if (bj.RestorePoints[i].GetDate().Year < Date.Year ||
                    bj.RestorePoints[i].GetDate().Month < Date.Month ||
                    bj.RestorePoints[i].GetDate().Day < Date.Day) continue;
                bj.RestorePoints.RemoveAt(i);
                break;
            }
        }
    }
}