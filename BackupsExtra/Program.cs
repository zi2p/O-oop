using System.IO;
using Backups.Entities;
using BackupsExtra.ExtraService;

namespace BackupsExtra
{
    internal class Program
    {
        private static void Main()
        {
            /* 1. Cоздаю бекапную джобу, указываю путь директории для хранения бекапов
               2. Указываю Single storage
               3. Добавляю в джобу два файла
               4. Запускаю создание точки
               5. Проверяю, что созданы директории и файлы
               6. Создаю еще одну точку
               6. Мерджу точки
            backup job -> name -> curdir */
                var service = new Service();
                string curDirectory1 = Directory.GetCurrentDirectory();
                var repository1 = new Repository();
                repository1.SetWay(curDirectory1 + "\\Backups");
                service.MakeMethodBJ(new SingleStorage(), repository1);
                if (!File.Exists(curDirectory1 + "\\3.txt"))
                {
                    FileStream file = File.Create(curDirectory1 + "\\3.txt");
                    file.Close();
                }

                if (!File.Exists(curDirectory1 + "\\4.txt"))
                {
                    FileStream file = File.Create(curDirectory1 + "\\4.txt");
                    file.Close();
                }

                var file1 = new FileInfo(curDirectory1 + "\\3.txt");
                var file2 = new FileInfo(curDirectory1 + "\\4.txt");
                var obj1 = new ObjectJ(file1);
                var obj2 = new ObjectJ(file2);
                service.SetObject(obj1);
                service.SetObject(obj2);
                var dt1 = new DateSave(30, 10, 2021);
                RestorePoint rp1 = service.MakeARestorePoint(dt1);

                string curDirectory2 = Directory.GetCurrentDirectory();
                var repository2 = new Repository();
                repository2.SetWay(curDirectory2 + "\\Backups");
                service.MakeMethodBJ(new SingleStorage(), repository2);
                if (!File.Exists(curDirectory2 + "\\5.txt"))
                {
                    FileStream file = File.Create(curDirectory2 + "\\5.txt");
                    file.Close();
                }

                if (!File.Exists(curDirectory2 + "\\6.txt"))
                {
                    FileStream file = File.Create(curDirectory2 + "\\6.txt");
                    file.Close();
                }

                var file3 = new FileInfo(curDirectory2 + "\\5.txt");
                var file4 = new FileInfo(curDirectory2 + "\\6.txt");
                var obj3 = new ObjectJ(file3);
                var obj4 = new ObjectJ(file4);
                service.SetObject(obj3);
                service.SetObject(obj4);
                var dt2 = new DateSave(26, 11, 2021);
                RestorePoint rp2 = service.MakeARestorePoint(dt2);

                service.DoingMerge(rp1, rp2);
        }
    }
}
