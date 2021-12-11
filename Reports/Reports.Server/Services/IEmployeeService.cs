using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reports.DAL.Entities;

namespace Reports.Server.Services
{
    public interface IEmployeeService
    {
        Task<Employee> Create(string name);
        Employee FindById(int id);
        Employee Delete(int id);
        DbSet<Employee> Employees();
        Employee Update(Employee entity);
        Task<TaskModel> CreateTask();
        TaskModel GetTaskById(int id);
        List<TaskModel> FindTaskByDateBorn(DateTime born);
        List<TaskModel> FindTaskByLastUpdate(DateTime update);
        List<TaskModel> FindTaskByEmployee(Employee employee);
        TaskModel FindTaskByEmployeeComment(Employee employee);
        void ChangeEmployee(Employee lastEmployee, Employee nowEmployee, TaskModel task);
        List<TaskModel> ListForEmployee(Employee employee);
        DbSet<TaskModel> GetList();
        string AddComment(Employee employee, string comment, TaskModel task);
        string ChangePositions(TaskModel task);
        Report WeekReport();
        DbSet<TaskModel> WeekTasks();
        TaskModel AddTaskToReport(Report report, TaskModel task);
        Report GetReport(Report report);
    }
}