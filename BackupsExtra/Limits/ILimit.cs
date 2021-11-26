using Backups.Entities;

namespace BackupsExtra.Limits
{
    public interface ILimit
    {
        public void SetALimit(BackupJob bj);
    }
}