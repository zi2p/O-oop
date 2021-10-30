namespace Backups.Entities
{
    public class Storage // резервные копии объектов
    {
        public Storage(ObjectJ obj)
        {
            Name = obj.Name;
            ObjectJob = obj;
        }

        public string Name { get; }
        public ObjectJ ObjectJob { get; }
    }
}