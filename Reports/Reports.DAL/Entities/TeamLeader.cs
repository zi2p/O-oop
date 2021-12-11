using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Reports.DAL.Entities
{
    public class TeamLeader : Employee 
    {
        [NotMapped]
        public List<Employee> Employees { get; set; }
        [NotMapped]
        public List<Employee> Subordinates { get; set; }

        public TeamLeader(int id, string name) 
            : base(id, name)
        {
        }
        public void AddSubordinates(Employee employee)
        {
            Subordinates.Add(employee);
            Employees.Add(employee);
        }

        public void AddEmployee(Employee employee)
        {
            Employees.Add(employee);
        }

        public Employee DismissEmployee(Employee employee)
        {
            foreach (Employee people in Subordinates.Where(people => people.Id == employee.Id))
            {
                Subordinates.Remove(employee);
            }

            if (Employees.Any(people => people.Id == employee.Id))
            {
                Employees.Remove(employee);
            }

            return employee;
        }

        public Report SaveReports()
        {
            var rep = new Report(this);
            foreach (TaskModel task in from employee in Employees from report in Reports from task in report.Tasks select task)
            {
                rep.AddTask(task);
            }

            return rep;
        }
    }
}