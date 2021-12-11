using System.Collections.Generic;
using System.Linq;

namespace Reports.DAL.Entities
{
    public class Director : Employee
    {
        public List<Employee> Subordinates { get; set; }
        public Director(int id, string name) 
            : base(id, name)
        {
        }

        public void AddSubordinates(Employee employee)
        {
            Subordinates.Add(employee);
        }
        public void DismissEmployee(Employee employee)
        {
            foreach (Employee people in Subordinates.Where(people => people.Id == employee.Id))
            {
                Subordinates.Remove(employee);
            }
        }
    }
}