using System.Collections.Generic;

namespace Reports.DAL.Entities
{
    public class ReportDTO
    {
        public int PositionsDTO { get; set; }  // 0-not start, 1-start, 2-finish
        public Employee EmployeeDTO { get; set; }
        public List<TaskModel> TasksDTO { get; set; }
        public int IdDTO { get; set; }

        public ReportDTO(Employee employee)
        {
            EmployeeDTO = employee;
            TasksDTO = new List<TaskModel>();
            PositionsDTO = 0;
        }
    }
}