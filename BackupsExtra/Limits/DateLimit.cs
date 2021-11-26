using System;
using System.Linq;
using Backups.Entities;
using BackupsExtra.Tools;

namespace BackupsExtra.Limits
{
    public class DateLimit : ILimit
    {
        private DateTime _date;

        public DateLimit(DateTime date)
        {
            _date = date;
        }

        public void SetALimit(BackupJob bj)
        {
            int count = -1 + bj.RestorePoints.TakeWhile(rp => rp.GetDate().Year < _date.Year || rp.GetDate().Month < _date.Month || rp.GetDate().Day < _date.Day).Count();
            if (count <= 0)
            {
                throw new BackupsExtraException("all points are deleted");
            }

            bj.RestorePoints.RemoveAt(count);
        }
    }
}