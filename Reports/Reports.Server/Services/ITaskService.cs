using System.Threading.Tasks;
using Reports.DAL.Entities;

namespace Reports.Server.Services
{
    public interface ITaskService
    {
        Task<TaskModel> Create();

        Task<TaskModel> FindById(int id);
    }
}