using System;

namespace BackupsExtra.Limits
{
    public class AllDateAndQuantityLimit : DateOrQuantityLimit
    {
        public AllDateAndQuantityLimit(DateTime date, int count)
            : base(date, count)
        {
        }
    }
}