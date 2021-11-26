using System;
using Backups.Entities;
using BackupsExtra.Tools;

namespace BackupsExtra.Limits
{
    public class DateOrQuantityLimit : ILimit
    {
        private DateTime _date;
        private int _quantity;

        public DateOrQuantityLimit(DateTime date, int count)
        {
            if (count <= 0)
            {
                throw new BackupsExtraException("all points are deleted");
            }

            _quantity = count;
            _date = date;
        }

        public void SetALimit(BackupJob bj) // сохраняет наибольшее число точек возврата
        {
            if (bj.RestorePoints.Count <= _quantity) return;
            if (bj.RestorePoints[_quantity - 1].GetDate().Year <= _date.Year &&
                bj.RestorePoints[_quantity - 1].GetDate().Month <= _date.Month &&
                bj.RestorePoints[_quantity - 1].GetDate().Day <= _date.Day)
            {
                bj.RestorePoints.RemoveAt(_quantity);
            }

            int i;
            for (i = _quantity - 1; i < bj.RestorePoints.Count; i++)
            {
                if (bj.RestorePoints[i].GetDate().Year < _date.Year ||
                    bj.RestorePoints[i].GetDate().Month < _date.Month ||
                    bj.RestorePoints[i].GetDate().Day < _date.Day) continue;
                bj.RestorePoints.RemoveAt(i);
                break;
            }
        }
    }
}