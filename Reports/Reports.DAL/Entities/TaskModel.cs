using System;

namespace Reports.DAL.Entities
{
    public class TaskModel
    {
        public int Id { get; set; }

        public Employee AssignedEmployee { get; set; }
        public uint Positions { get; set; } // 1-open, 2-active, 3-resolved
        public string Comment { get; set; }
        public Employee Commenter { get; set; }
        public DateTime Born { get; set; }
        public DateTime Update { get; set; } 

        public TaskModel(int id)
        {

            Id = id;
            Positions = 1;
            Born = DateTime.Now;
            Update = Born;
        }

        public TaskModel()
        {
            Born = DateTime.Now;
        }

        public void AppointmentEmployee(Employee employee)
        {
            AssignedEmployee = employee;
            Positions = 2;
            Update = DateTime.Now;
        }

        public void AddComment(string comment, Employee employee)
        {
            if (Positions == 1) Comment = comment;
            Update = DateTime.Now;
            Commenter = employee;
        }
    }
}