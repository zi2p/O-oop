using Backups.Entities;
using BackupsExtra.Tools;

namespace BackupsExtra.Limits
{
    public class QuantityLimit : ILimit
    {
        private int _quantity;

        public QuantityLimit(int count)
        {
            if (count <= 0)
            {
                throw new BackupsExtraException("all points are deleted");
            }

            _quantity = count;
        }

        public void Limit(BackupJob bj)
        {
            if (bj.RestorePoints.Count > _quantity) bj.RestorePoints.RemoveAt(_quantity);
        }
    }
}