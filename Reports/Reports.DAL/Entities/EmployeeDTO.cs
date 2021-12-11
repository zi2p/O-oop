using System;
using System.Collections.Generic;

namespace Reports.DAL.Entities
{
    public class EmployeeDTO
    {
        public int IdDTO { get; set; }
        public List<Report> ReportsDTO;

        public string NameDTO { get; set; }
        public int DirectorIdDTO { get; }
        public List<TaskModel> TasksDTO { get; set; }

        public int Id
        {
            get;
            set;
        }

        public EmployeeDTO()
        {
        }

        public EmployeeDTO(int id, string name)
        {

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name), "Name is invalid");
            }

            IdDTO = id;
            NameDTO = name;
        }
    }
    
}