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

        public async Task<Employee> Create(string name)
        {
            var employee = new Employee(_counter++, name);
            EntityEntry<Employee> employeeFromDb = await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public DbSet<TaskModel> GetList()
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

        public Report WeekReport()
        {
            return _teamLeader.Report();
        }

        public DbSet<TaskModel> WeekTasks()
        {
            return _context.Tasks;
        }

        public TaskModel AddTaskToReport(Report report, TaskModel task)
        {
            if (task.AssignedEmployee.Reports.Contains(report)) report.AddTask(task);
            return task;
        }

        public Report GetReport(Report report)
        {
            return _teamLeader.Report();
        }

        public void MakeTeamLeader(TeamLeader employee)
        {
            _teamLeader = employee;
        }

        public DbSet<Employee> Employees()
        {
            return _context.Employees;
        }

        public Employee FindById(int id)
        {
            return _context.Employees.FirstOrDefault(task => task.Id == id);
        }
        
        public Employee Delete(int id)
        {
            foreach (Employee employee in _teamLeader.Employees.Where(employee => employee.Id == id))
            {
                _teamLeader.Employees.Remove(employee);
                return employee;
            }
            return null;
        }

        public Employee Update(Employee entity)
        {
            _teamLeader.AddEmployee(entity);
            return entity;
        }

        public async Task<TaskModel> CreateTask()
        {
            var task = new TaskModel();
            EntityEntry<TaskModel> taskFromDb = await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public TaskModel GetTaskById(int id)
        {
            return _context.Tasks.FirstOrDefault(x => x.Id == id);
        }

        public List<TaskModel> FindTaskByDateBorn(DateTime born)
        {
            return _service.FindByDateBorn(born);
        }

        public List<TaskModel> FindTaskByLastUpdate(DateTime update)
        {
            return _service.FindByLastUpdate(update);
        }

        public List<TaskModel> FindTaskByEmployee(Employee employee)
        {
            return _service.FindByEmployee(employee);
        }

        public TaskModel FindTaskByEmployeeComment(Employee employee)
        {
            return _service.FindByEmployeeComment(employee);
        }

        public void ChangeEmployee(Employee lastEmployee, Employee nowEmployee, TaskModel task)
        {
            _service.ChangeEmployee(lastEmployee,nowEmployee,task);
        }

        public List<TaskModel> ListForEmployee(Employee employee)
        {
            return _service.ListForEmployee(employee);
        }
    }
}