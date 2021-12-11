using System;

namespace Reports.DAL.Entities
{
    public class TaskModelDTO
    {
        public int IdDTO { get; set; }

        public Employee AssignedEmployeeDTO { get; set; }
        public uint PositionsDTO { get; set; } // 1-open, 2-active, 3-resolved
        public string CommentDTO { get; set; }
        public Employee CommenterDTO { get; set; }
        public DateTime BornDTO { get; set; }
        public DateTime UpdateDTO { get; set; }

        public int Id
        {
            get;
            set;
        }

        public TaskModelDTO(int id)
        {

            IdDTO = id;
            PositionsDTO = 1;
            BornDTO = DateTime.Now;
            UpdateDTO = BornDTO;
        }

        public TaskModelDTO()
        {
            BornDTO = DateTime.Now;
        }
    }
}