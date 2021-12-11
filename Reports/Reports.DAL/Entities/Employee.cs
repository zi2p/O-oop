using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reports.DAL.Entities
{
    public class Employee // работник
    {
        public int Id { get; set; }
        public List<Report> Reports;

        public string Name { get; set; }
        public int DirectorId { get; }

        [ForeignKey("DirectorId")] public Director Director { get; }
        [NotMapped]
        public List<TaskModel> Tasks { get; set; }

        private Employee()
        {
        }

        public Employee(int id, string name)
        {

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name), "Name is invalid");
            }

            Id = id;
            Name = name;
        }

        public void TakeTask(TaskModel task)
        {
            if (task.Positions != 1) return;
            task.AppointmentEmployee(this);
            Tasks.Add(task);
        }

        public TaskModel AddTask()
        {
            var task = new TaskModel
            {
                AssignedEmployee = this,
                Born = DateTime.Now,
                Update = DateTime.Now
            };
            return task;
        }

        public void Comment(TaskModel task, string str)
        {
            task.AddComment(str, this);
        }

        public void ReservedTask(TaskModel task)
        {
            if (task.Positions != 1) return;
            task.Positions = 2;
            task.AssignedEmployee = this;
        }

        public Report Report()
        {
            var report = new Report(this);
            foreach (TaskModel task in Tasks)
            {
                report.AddTask(task);
            }
            report.ClouseReport();
            Reports.Add(report);
            return report;
        }
    }
}