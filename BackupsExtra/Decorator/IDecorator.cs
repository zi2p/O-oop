using Backups.Entities;

namespace BackupsExtra.Decorator
{
    public interface IDecorator
    {
        public void DoMerge(RestorePoint p1, RestorePoint p2);
        public void Limits();
    }
}