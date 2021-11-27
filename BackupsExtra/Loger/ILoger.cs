using Backups.Entities;

namespace BackupsExtra.Loger
{
    public interface ILoger
    {
        public void LogStorageFile(string name);
        public void LogRestorePoint(string name);
        public void LogMergeRestorePoints(RestorePoint p1, RestorePoint p2);
    }
}