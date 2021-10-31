using System.IO;
using Backups.Entities;
using Backups.Services;

namespace Backups
{
    internal class Program
    {
    // 1. Cоздаю бекапную джобу, указываю путь директории для хранения бекапов
    // 2. Указываю Single storage
    // 3. Добавляю в джобу два файла
    // 4. Запускаю создание точки
    // 5. Проверяю, что созданы директории и файлы
        private static void Main()
        {
            // backup job -> name -> curdir
            var service = new BackupsService();
            string curDirectory = Directory.GetCurrentDirectory();
            var repository = new Repository();
            repository.SetWay(curDirectory + "\\Backups");
            service.MakeMethodBJ(new SingleStorage(), repository);
            if (!File.Exists(curDirectory + "\\3.txt"))
            {
                FileStream file = File.Create(curDirectory + "\\3.txt");
                file.Close();
            }

            if (!File.Exists(curDirectory + "\\4.txt"))
            {
                FileStream file = File.Create(curDirectory + "\\4.txt");
                file.Close();
            }

            var file1 = new FileInfo(curDirectory + "\\3.txt");
            var file2 = new FileInfo(curDirectory + "\\4.txt");
            var obj1 = new ObjectJ(file1);
            var obj2 = new ObjectJ(file2);
            service.SetObject(obj1);
            service.SetObject(obj2);
            var dt1 = new DateSave(29, 10, 2021);
            RestorePoint rp1 = service.MakeARestorePoint(dt1);
        }
    }
}
