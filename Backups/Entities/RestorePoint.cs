using System;
using System.Collections.Generic;

namespace Backups.Entities
{
    public class RestorePoint
    {
        private DateSave _date;
        private List<Storage> _copy;

        public RestorePoint(DateSave date, List<Storage> newCopy)
        {
            _date = date;
            _copy = newCopy;
            Name = DateTime.Now.ToString("yyyy-M-d hh-mm-ss");
        }

        public string Name { get; }

        public DateSave GetDate()
        {
            return _date;
        }

        public List<Storage> GetList()
        {
            return _copy;
        }
    }
}