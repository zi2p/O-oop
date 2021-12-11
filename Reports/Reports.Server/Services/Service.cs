using System;
using System.Collections.Generic;
using System.Linq;
using Reports.DAL.Entities;

namespace Reports.Server.Services
{
    public class Service
    {
        public DateTime NowTime = DateTime.Now;
        public List<TaskModel> Tasks = new List<TaskModel>();
        public Service()
        {
        }

        public TaskModel FindById(int id)
        {
            return Tasks.FirstOrDefault(task => task.Id == id);
        }

        public List<TaskModel> FindByDateBorn(DateTime born)
        {
            return Tasks.Where(task => task.Born == born).ToList();
        }

        public List<TaskModel> FindByLastUpdate(DateTime update)
        {
            return Tasks.Where(task => task.Update == update).ToList();
        }

        public List<TaskModel> FindByEmployee(Employee employee)
        {
            return Tasks.Where(task => task.AssignedEmployee.Id == employee.Id).ToList();
        }

        public TaskModel FindByEmployeeComment(Employee employee)
        {
            return Tasks.FirstOrDefault(task => task.Commenter.Id == employee.Id);
        }

        public void ChangeEmployee(Employee lastEmployee, Employee nowEmployee, TaskModel task)
        {
            if (task.Positions == 3) return;
            if (task.AssignedEmployee.Id == lastEmployee.Id) task.AssignedEmployee = nowEmployee;
        }

        public List<TaskModel> ListForEmployee(Employee employee)
        {
            return Tasks.Where(task => task.AssignedEmployee.Id == employee.Id).ToList();
        }
    }
}