using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reports.DAL.Entities;

namespace Reports.Server.Services
{
    public interface IEmployeeService
    {
        Task<EmployeeDTO> Create(string name);
        EmployeeDTO FindById(int id);
        EmployeeDTO Delete(int id);
        DbSet<EmployeeDTO> Employees();
        EmployeeDTO Update(Employee entity);
        Task<TaskModelDTO> CreateTask();
        TaskModelDTO GetTaskById(int id);
        List<TaskModelDTO> FindTaskByDateBorn(DateTime born);
        List<TaskModelDTO> FindTaskByLastUpdate(DateTime update);
        List<TaskModelDTO> FindTaskByEmployee(Employee employee);
        TaskModelDTO FindTaskByEmployeeComment(Employee employee);
        void ChangeEmployee(Employee lastEmployee, Employee nowEmployee, TaskModel task);
        List<TaskModelDTO> ListForEmployee(Employee employee);
        DbSet<TaskModelDTO> GetList();
        string AddComment(Employee employee, string comment, TaskModel task);
        string ChangePositions(TaskModel task);
        ReportDTO WeekReport();
        DbSet<TaskModelDTO> WeekTasks();
        TaskModelDTO AddTaskToReport(Report report, TaskModel task);
        ReportDTO GetReport(Report report);
    }
}