using Backups.Entities;

namespace BackupsExtra.Merge
{
    public interface IMerge
    {
        public void DoMerge(RestorePoint point1, RestorePoint point2);
    }
}