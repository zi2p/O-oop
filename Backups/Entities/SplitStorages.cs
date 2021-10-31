using System.Collections.Generic;

namespace Backups.Entities
{
    public class SplitStorages : IMethods
    {
        private IRepository _repository;
        public SplitStorages()
        {
        }

        public List<Storage> Doing(List<ObjectJ> docs, string way)
        {
            var storages = new List<Storage>();
            foreach (ObjectJ doc in docs)
            {
                _repository.AddFileToArchive(doc.File, _repository.CreateArchive(way, doc.Name));
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