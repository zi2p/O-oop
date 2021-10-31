using System;
using System.Collections.Generic;
using System.Linq;

namespace Backups.Entities
{
    public class BackupJob
    {
        private IMethods _storageAlgorithm;
        private IRepository _repository;
        public BackupJob()
        {
            Name = DateTime.Now.ToString("yyyy-M-d hh-mm-ss");
            RestorePoints = new List<RestorePoint>();
            Objects = new List<ObjectJ>();
            _storageAlgorithm = null;
            _repository = null;
        }

        public string Name { get; }

        public List<RestorePoint> RestorePoints { get; }
        private List<ObjectJ> Objects { get; }

        public void SetObject(ObjectJ obj)
        {
            Objects.Add(obj);
        }

        public void DeleteObject(ObjectJ obj)
        {
            if (!Objects.Any(objct => obj.Name == objct.Name)) return;
            Objects.Remove(obj);
        }

        public void SetAlgorithm(IMethods algorithm)
        {
            if (algorithm is not null) _storageAlgorithm = algorithm;
        }

        public void SetRepository(IRepository repository)
        {
            if (repository is not null) _repository = repository;
        }

        public RestorePoint MakeARestorePoint(DateSave date)
        {
            _storageAlgorithm.SetRepository(_repository);
            var objectsToStorages = Objects.Select(obj => new Storage(obj)).ToList();

            var rp = new RestorePoint(date, objectsToStorages);
            _storageAlgorithm.Doing(Objects, _repository.Way + "\\" + Name + "\\" + rp.Name);
            RestorePoints.Add(rp);
            return rp;
        }
    }
}