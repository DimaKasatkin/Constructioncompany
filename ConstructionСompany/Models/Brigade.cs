using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionСompany.Models
{
    public class Brigade
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Employee> Employees { get; set; }
        public ICollection<Order> Orders { get; set; }

        public Brigade()
        {
            Orders = new List<Order>();
            Employees = new List<Employee>();
        }
    }
}
