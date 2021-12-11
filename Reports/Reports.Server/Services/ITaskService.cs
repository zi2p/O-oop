using System.Threading.Tasks;
using Reports.DAL.Entities;

namespace Reports.Server.Services
{
    public interface ITaskService
    {
        Task<TaskModelDTO> Create();

        Task<TaskModelDTO> FindById(int id);
    }
}