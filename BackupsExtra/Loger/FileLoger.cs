using System;
using System.IO;
using Backups.Entities;

namespace BackupsExtra.Loger
{
    public class FileLoger : ILoger
    {
        private bool _date;
        private FileInfo _file;
        private StreamWriter _streamWriter;

        public FileLoger(string way, bool yesOrNo = true)
        {
            _date = yesOrNo;
            var file = new FileInfo(way);
            _file = file;
            StreamWriter sw = _file.CreateText();
            _streamWriter = sw;
        }

        public void DoingStorage(string name)
        {
            if (_date)
            {
                _streamWriter.Write(DateTime.Now + "    ");
            }

            _streamWriter.Write("Doing storage whose name is " + name + " .\n");
        }

        public void DoingRestorePoint(string name)
        {
            if (_date)
            {
                _streamWriter.Write(DateTime.Now + "    ");
            }

            _streamWriter.Write("Doing restore point whose name is " + name + " .\n");
        }

        public void DoingMerge(RestorePoint p1, RestorePoint p2)
        {
            if (_date)
            {
                _streamWriter.Write(DateTime.Now + "    ");
            }

            _streamWriter.Write("Doing merge " + p1.Name + " -> " + p2.Name + " .\n");
        }
    }
}