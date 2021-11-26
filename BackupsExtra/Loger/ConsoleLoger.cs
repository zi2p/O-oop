using System;
using Backups.Entities;

namespace BackupsExtra.Loger
{
    public class ConsoleLoger : ILoger
    {
        private bool _date;

        public ConsoleLoger(bool yesOrNo = true)
        {
            _date = yesOrNo;
        }

        public void DoingStorage(string name)
        {
            if (_date)
            {
                Console.Write(DateTime.Now + "    ");
            }

            Console.Write("Doing storage whose name is " + name + " .\n");
        }

        public void DoingRestorePoint(string name)
        {
            if (_date)
            {
                Console.Write(DateTime.Now + "    ");
            }

            Console.Write("Doing restore point whose name is " + name + " .\n");
        }

        public void DoingMerge(RestorePoint p1, RestorePoint p2)
        {
            if (_date)
            {
                Console.Write(DateTime.Now + "    ");
            }

            Console.Write("Doing merge " + p1.Name + " -> " + p2.Name + " .\n");
        }
    }
}