using System.Collections.Generic;
using System.Linq;

namespace Backups.Entities
{
    public interface IMethods
    {
        public List<Storage> Doing(List<ObjectJ> docs, string way);

        public void SetRepository(IRepository repository);
    }
}