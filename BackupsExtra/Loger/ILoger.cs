using Backups.Entities;

namespace BackupsExtra.Loger
{
    public interface ILoger
    {
        public void DoingStorage(string name);
        public void DoingRestorePoint(string name);
        public void DoingMerge(RestorePoint p1, RestorePoint p2);
    }
}