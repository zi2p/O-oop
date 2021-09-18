using System;

namespace Isu
{
    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine(G());
        }

        private static string G()   // почему не ломается
        {
            return null;
        }
    }
}
