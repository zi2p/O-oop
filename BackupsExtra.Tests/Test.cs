using System.IO;
using Backups.Entities;
using BackupsExtra.ExtraService;
using NUnit.Framework;

namespace BackupsExtra.Tests
{
    public class Test
    {
        private Service _service;
        
        [SetUp]
        public void Setup()
        {
            _service = new Service();
        }

        [Test]
        public void First()
        {
            // 1. Cоздаю бекапную джобу
            // 2. Указываю Split storages
            // 3. Добавляю в джобу два файла
            // 4. Запускаю создание точки 
            // 5. Удаляю один из файлов
            // 6. Запускаю создание
            // 7. Проверяю, что создано две точки и три стораджа
            var repository = new Repository();
            string backupPath = Directory.GetCurrentDirectory() + "\\Backups";
            string currentPath = Directory.GetCurrentDirectory();
            if (!Directory.Exists(backupPath)) Directory.CreateDirectory(backupPath);
            repository.SetWay(backupPath);
            _service.MakeMethodBJ(new SplitStorages(), new Repository());
            if (!File.Exists(currentPath + "\\1.txt"))
            {
                FileStream file = File.Create(currentPath + "\\1.txt");
                file.Close();
            }
            var file1 = new FileInfo(currentPath + "\\1.txt");
            if (!File.Exists(currentPath + "\\2.txt"))
            {
                FileStream file = File.Create(currentPath + "\\2.txt");
                file.Close();
            }
            var file2 = new FileInfo(currentPath + "\\2.txt");
            var obj1 = new ObjectJ(file1);
            var obj2 = new ObjectJ(file2);
            _service.SetObject(obj1);
            _service.SetObject(obj2);
            var dt1 = new DateSave(29, 10, 2021);
            RestorePoint rp1 = _service.MakeARestorePoint(dt1);
            _service.DeleteObject(obj1);
            var dt2 = new DateSave(30, 10, 2021);
            RestorePoint rp2 = _service.MakeARestorePoint(dt2);
            Assert.That(2.Equals(_service.GetStoragesInRestorePoint(rp1).Count));
            Assert.That(1.Equals(_service.GetStoragesInRestorePoint(rp2).Count));
        }
    }
}