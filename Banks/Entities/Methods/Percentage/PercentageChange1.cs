using System;
using System.Collections.Generic;
using System.Linq;

namespace Banks.Entities.Methods.Percentage
{
    public class PercentageChange1 : IMethodPercentageChange
    {
        public PercentageChange1()
        {
            Table = new List<Tuple<uint, uint, double>>();
        }

        public List<Tuple<uint, uint, double>> Table { get; }

        public void AddСondition(uint start, uint finish, double proc)
        {
            var tuple = new Tuple<uint, uint, double>(start, finish, proc);
            foreach (Tuple<uint, uint, double> t in Table)
            {
                if (t.Item1 <= start || t.Item1 >= finish) continue;
                var newTuple = new Tuple<uint, uint, double>(finish, t.Item2, t.Item3);
                Table.Remove(t);
                Table.Add(newTuple);
            }

            Table.Add(tuple);
        }

        public double GetPercentage(double sum)
        {
            return (from t in Table where sum < t.Item2 && sum > t.Item1 select sum * t.Item3).FirstOrDefault();
        }
    }
}