using Backups.Entities;

namespace BackupsExtra.Limits
{
    public interface ILimit
    {
        public void Limit(BackupJob bj);
    }
}