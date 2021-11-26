using System.IO;
using System.IO.Compression;
using Backups.Entities;
using BackupsExtra.Limits;
using BackupsExtra.Merge;
using BackupsExtra.Tools;

namespace BackupsExtra.Decorator
{
    public class DecoratorBackupJob : IDecorator
    {
        private readonly BackupJob _backupJob;
        private IMerge _merge;
        private ILimit _limit;

        public DecoratorBackupJob()
        {
            _backupJob = new BackupJob();
            _limit = new QuantityLimit(10);
            _merge = new Merge.Merge();
        }

        public void SetMethodMerge(IMerge merge)
        {
            _merge = merge;
        }

        public void SetMethodLimit(ILimit limit)
        {
            _limit = limit;
        }

        public BackupJob GetBackupJob()
        {
            return _backupJob;
        }

        public void DoMerge(RestorePoint p1, RestorePoint p2)
        {
            if (_backupJob.RestorePoints.Contains(p1) && _backupJob.RestorePoints.Contains(p2))
            {
                _merge.DoMerge(p1, p2);
            }
            else
            {
                throw new BackupsExtraException("the points don't belong to this backup job");
            }
        }

        public void Limits()
        {
            _limit.SetALimit(_backupJob);
        }

        public void SetObject(ObjectJ obj)
        {
            _backupJob.SetObject(obj);
            Limits();
        }

        public void DeleteObject(ObjectJ obj)
        {
            _backupJob.DeleteObject(obj);
            Limits();
        }

        public void SetAlgorithm(IMethods algorithm)
        {
            _backupJob.SetAlgorithm(algorithm);
        }

        public void SetRepository(IRepository repository)
        {
            _backupJob.SetRepository(repository);
        }

        public void MakeARestorePoint(DateSave date)
        {
            _backupJob.MakeARestorePoint(date);
            Limits();
        }

        public void RecoveryRestorePoint(RestorePoint rp, string path = null)
        {
            using ZipArchive archive = ZipFile.OpenRead(rp.Name);
            foreach (ZipArchiveEntry entry in archive.Entries)
            {
                entry.ExtractToFile(path != null
                    ? Path.Combine(path, entry.FullName)
                    : Path.Combine(rp.Name, entry.FullName));
            }
        }
    }
}