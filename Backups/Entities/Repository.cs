using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace Backups.Entities
{
    public class Repository : IRepository// физичекое хранилище
    {
        public Repository()
        {
            Objects = new List<ObjectJ>();
        }

        public string Way { get; private set; }

        public List<ObjectJ> Objects { get; }

        public void SetName(string name)
        {
            Way = name;
        }

        public void SetWay(string way)
        {
            Way = way;
        }

        public void MakeListInRepository(List<ObjectJ> objs)
        {
            Objects.AddRange(objs);
        }

        public void SaveTo(string path, List<ObjectJ> objectJs)
        {
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            foreach (ObjectJ objectJob in objectJs)
            {
                objectJob.File.CopyTo(path + "\\" + objectJob.File.Name);
            }
        }

        public FileInfo CreateArchive(string path, string name)
        {
            if (File.Exists(path + "\\" + name + ".zip"))
                return new FileInfo(path + "\\" + name + ".zip");
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            FileStream file = File.Create(path + "\\" + name + ".zip");
            file.Close();
            return new FileInfo(path + "\\" + name + ".zip");
        }

        public void AddFileToArchive(FileInfo fileToCompress, FileInfo archive)
        {
            using var zipToOpen = new FileStream(archive.FullName, FileMode.Open);
            using var zipArchive = new ZipArchive(zipToOpen, ZipArchiveMode.Update);
            ZipArchiveEntry entry = zipArchive.CreateEntry(fileToCompress.Name);
            using var writer = new BinaryWriter(entry.Open());
            writer.Write(File.ReadAllBytes(fileToCompress.FullName));
        }
    }
}