using System.Collections.Generic;
using System.Linq;
using Backups.Entities;

namespace Backups.Services
{
    public class BackupsService
    {
        private BackupJob bJ = new BackupJob();
        public void MakeMethodBJ(IMethods name, IRepository repository)
        {
            bJ.SetAlgorithm(name);
            bJ.SetRepository(repository);
        }

        public List<Storage> GetStoragesInRestorePoint(RestorePoint rP)
        {
            return (from rp in bJ.RestorePoints where rp.GetDate() == rP.GetDate() select rp.GetList()).FirstOrDefault();
        }

        public void SetObject(ObjectJ obj)
        {
            bJ.SetObject(obj);
        }

        public void DeleteObject(ObjectJ obj)
        {
            bJ.DeleteObject(obj);
        }

        public RestorePoint MakeARestorePoint(DateSave dt)
        {
            return bJ.MakeARestorePoint(dt);
        }
    }
}