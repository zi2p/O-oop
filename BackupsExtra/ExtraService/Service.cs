using System.Collections.Generic;
using System.IO;
using System.Linq;
using Backups.Entities;
using BackupsExtra.Decorator;
using BackupsExtra.Loger;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace BackupsExtra.ExtraService
{
    public class Service
    {
        private DecoratorBackupJob bJ = new DecoratorBackupJob();
        private ILoger _loger = new FileLoger("loger.txt", true);

        public void MakeMethodBJ(IMethods name, IRepository repository)
        {
            bJ.SetAlgorithm(name);
            bJ.SetRepository(repository);
        }

        public void SetLoger(ILoger loger)
        {
            _loger = loger;
        }

        public List<Storage> GetStoragesInRestorePoint(RestorePoint rP)
        {
            _loger.LogStorageFile(rP.Name);
            return (from rp in bJ.GetBackupJob().RestorePoints where rp.GetDate() == rP.GetDate() select rp.GetList())
                .FirstOrDefault();
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
            RestorePoint rp = bJ.GetBackupJob().MakeARestorePoint(dt);
            _loger.LogRestorePoint(rp.Name);
            return rp;
        }

        public void DoingMerge(RestorePoint p1, RestorePoint p2)
        {
            bJ.DoMerge(p1, p2);
            _loger.LogMergeRestorePoints(p1, p2);
        }

        public void RecoveryRestorePoint(RestorePoint rp, string path = null)
        {
            bJ.RecoveryRestorePoint(rp, path);
        }

        public void SaveProgram()
        {
            File.WriteAllText("lab-5.json", JsonConvert.SerializeObject(bJ));
        }

        public void RecoverProgram(string nameJsonFile)
        {
            if (!File.Exists(nameJsonFile)) return;
            string newFile = File.ReadAllText(nameJsonFile);
            bJ = JsonSerializer.Deserialize<DecoratorBackupJob>(newFile);
        }
    }
}