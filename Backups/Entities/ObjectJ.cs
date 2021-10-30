using System.IO;

namespace Backups.Entities
{
    public class ObjectJ
    {
        public ObjectJ(FileInfo file)
        {
            Name = file.Name;
            File = file;
        }

        public FileInfo File { get; }
        public string Name { get; }
    }
}