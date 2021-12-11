using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Reports.DAL.Entities;
using Reports.Server.Database;

namespace Reports.Server.Services
{
    public class EmployeeService : IEmployeeService
    {
        private int _counter = 0;
        private TeamLeader _teamLeader = new TeamLeader(9, "aboba");
        private const string DbPath = "employees.json";
        private readonly ReportsDatabaseContext _context;
        private Service _service = new Service();

        public EmployeeService(ReportsDatabaseContext context) {
            _context = context;
        }

        public async Task<EmployeeDTO> Create(string name)
        {
            var employee = new EmployeeDTO(_counter++, name);
            EntityEntry<EmployeeDTO> employeeFromDb = await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public DbSet<TaskModelDTO> GetList()
        {
            return _context.Tasks;
        }

        public string AddComment(Employee employee, string comment, TaskModel task)
        {
            employee.Comment(task,comment);
            return comment;
        }

        public string ChangePositions(TaskModel task)
        {
            if (task.Positions < 3) task.Positions++;
            return task.Positions == 2 ? "ACTIVE" : "RESOLVED";
        }

        public ReportDTO WeekReport()
        {
            return _teamLeader.Report().DTO;
        }

        public DbSet<TaskModelDTO> WeekTasks()
        {
            return _context.Tasks;
        }

        public TaskModelDTO AddTaskToReport(Report report, TaskModel task)
        {
            if (task.AssignedEmployee.Reports.Contains(report)) report.AddTask(task);
            return task.DTO;
        }

        public ReportDTO GetReport(Report report)
        {
            return _teamLeader.Report().DTO;
        }

        public void MakeTeamLeader(TeamLeader employee)
        {
            _teamLeader = employee;
        }

        public DbSet<EmployeeDTO> Employees()
        {
            return _context.Employees;
        }

        public EmployeeDTO FindById(int id)
        {
            return _context.Employees.FirstOrDefault(task => task.Id == id)!=null ? _context.Employees.FirstOrDefault(task => task.Id == id) : null;
        }
        
        public EmployeeDTO Delete(int id)
        {
            foreach (Employee employee in _teamLeader.Employees.Where(employee => employee.Id == id))
            {
                _teamLeader.Employees.Remove(employee);
                return employee.DTO;
            }
            return null;
        }

        public EmployeeDTO Update(Employee entity)
        {
            _teamLeader.AddEmployee(entity);
            return entity.DTO;
        }

        public async Task<TaskModelDTO> CreateTask()
        {
            var task = new TaskModelDTO();
            EntityEntry<TaskModelDTO> taskFromDb = await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public TaskModelDTO GetTaskById(int id)
        {
            return _context.Tasks.FirstOrDefault(x => x.Id == id);
        }

        public List<TaskModelDTO> FindTaskByDateBorn(DateTime born)
        {
            return _service.FindByDateBorn(born);
        }

        public List<TaskModelDTO> FindTaskByLastUpdate(DateTime update)
        {
            return _service.FindByLastUpdate(update);
        }

        public List<TaskModelDTO> FindTaskByEmployee(Employee employee)
        {
            return _service.FindByEmployee(employee);
        }

        public TaskModelDTO FindTaskByEmployeeComment(Employee employee)
        {
            return _service.FindByEmployeeComment(employee);
        }

        public void ChangeEmployee(Employee lastEmployee, Employee nowEmployee, TaskModel task)
        {
            _service.ChangeEmployee(lastEmployee,nowEmployee,task);
        }

        public List<TaskModelDTO> ListForEmployee(Employee employee)
        {
            return _service.ListForEmployee(employee);
        }
    }
}