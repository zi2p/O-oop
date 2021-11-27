using System;
using Backups.Entities;

namespace BackupsExtra.Limits
{
    public class AllDateAndQuantityLimit : DateOrQuantityLimit
    {
        public AllDateAndQuantityLimit(DateTime date, int count)
            : base(date, count)
        {
        }

        public new void Limit(BackupJob bj)
        {
            if (bj.RestorePoints.Count <= Quantity) return;
            if (bj.RestorePoints[Quantity - 1].GetDate().Year <= Date.Year &&
                bj.RestorePoints[Quantity - 1].GetDate().Month <= Date.Month &&
                bj.RestorePoints[Quantity - 1].GetDate().Day <= Date.Day)
            {
                bj.RestorePoints.RemoveAt(Quantity);
            }

            int i;
            for (i = 0; i < Quantity - 1; i++)
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