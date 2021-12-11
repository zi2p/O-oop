using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Reports.DAL.Entities;
using Reports.Server.Database;

namespace Reports.Server.Services
{
    public class TaskService : ITaskService
    {
        private const string DbPath = "tasks.json";
        private readonly ReportsDatabaseContext _context;

        public TaskService(ReportsDatabaseContext context) {
            _context = context;
        }
        
        public async Task<TaskModelDTO> Create()
        {
            var task = new TaskModelDTO();
            EntityEntry<TaskModelDTO> taskFromDb = await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
            return task;
        }
        
        public async Task<TaskModelDTO> FindById(int id)
        {
            return await _context.Tasks.FirstOrDefaultAsync(task => task.Id == id);
        }
    }
}