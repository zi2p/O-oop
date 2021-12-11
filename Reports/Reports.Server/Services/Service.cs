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

        public TaskModelDTO FindById(int id)
        {
            return Tasks.FirstOrDefault(task => task.Id == id)!=null ? Tasks.FirstOrDefault(task => task.Id == id)?.DTO : null;
        }

        public List<TaskModelDTO> FindByDateBorn(DateTime born)
        {
            return (from task in Tasks where task.Born == born select task.DTO).ToList();
        }

        public List<TaskModelDTO> FindByLastUpdate(DateTime update)
        {
            return (from task in Tasks where task.Update == update select task.DTO).ToList();
        }

        public List<TaskModelDTO> FindByEmployee(Employee employee)
        {
            return (from task in Tasks where task.AssignedEmployee.Id == employee.Id select task.DTO).ToList();
        }

        public TaskModelDTO FindByEmployeeComment(Employee employee)
        {
            return Tasks.FirstOrDefault(task => task.Commenter.Id == employee.Id)!=null ? Tasks.FirstOrDefault(task => task.Commenter.Id == employee.Id)?.DTO : null;
        }

        public void ChangeEmployee(Employee lastEmployee, Employee nowEmployee, TaskModel task)
        {
            if (task.Positions == 3) return;
            if (task.AssignedEmployee.Id == lastEmployee.Id) task.AssignedEmployee = nowEmployee;
        }

        public List<TaskModelDTO> ListForEmployee(Employee employee)
        {
            return (from task in Tasks where task.AssignedEmployee.Id == employee.Id select task.DTO).ToList();
        }
    }
}