using System.Linq;
using Backups.Entities;

namespace BackupsExtra.Merge
{
    public class Merge : IMerge
    {
        /* - Если в старой точке есть объект и в новой точке есть объект - нужно оставить новый, а старый можно удалять
           - Если в старой точке есть объект, а в новоей его нет - нужно перенести его в новую точку */
        public void DoMerge(RestorePoint point1, RestorePoint point2)
        {
            // merge p1 -> p2
            foreach (Storage storage1 in point1.GetList().Where(storage1 => !point2.GetList().Contains(storage1)))
            {
                point2.GetList().Add(storage1);
            }
        }
    }
}