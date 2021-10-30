using System.Collections.Generic;
using System.IO;

namespace Backups.Entities
{
    public interface IRepository
    {
        public string Way { get; }
        public List<ObjectJ> Objects { get; }
        public void SetName(string name);
        public void MakeListInRepository(List<ObjectJ> objs);
        public void SaveTo(string path, List<ObjectJ> objectJs);
        public FileInfo CreateArchive(string path, string name);
        public void AddFileToArchive(FileInfo fileToCompress, FileInfo archive);
    }
}