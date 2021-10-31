using System;
using System.Collections.Generic;
using System.IO;

namespace Backups.Entities
{
    public class SingleStorage : IMethods
    {
        private IRepository _repository;
        public SingleStorage()
        {
        }

        public List<Storage> Doing(List<ObjectJ> docs, string way)
        {
            var storages = new List<Storage>();
            FileInfo file = _repository.CreateArchive(way, DateTime.Now.ToString("yyyy-M-d dddd"));
            foreach (ObjectJ doc in docs)
            {
                _repository.AddFileToArchive(doc.File, file);
                storages.Add(new Storage(doc));
            }

            return storages;
        }

        public void SetRepository(IRepository repository)
        {
            if (repository is not null) _repository = repository;
        }
    }
}