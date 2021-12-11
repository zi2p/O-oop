using System.Collections.Generic;

namespace Reports.DAL.Entities
{
    public class Report
    {
        public int Positions { get; set; }  // 0-not start, 1-start, 2-finish
        public Employee Employee { get; set; }
        public List<TaskModel> Tasks { get; set; }
        public int Id { get; set; }
        public ReportDTO DTO { get; set; }

        public Report(Employee employee)
        {
            Employee = employee;
            Tasks = new List<TaskModel>();
            Positions = 0;
            DTO = new ReportDTO(employee);
        }
        public void AddTask(TaskModel task)
        {
            Tasks.Add(task);
            Positions = 1;
        }
        public void ClouseReport()
        {
            Positions = 2;
        }
    }
}