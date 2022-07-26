using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionСompany.Models
{
    public class TypeOfJob
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Cost { get; set; }

        public ICollection<Order> Orders { get; set; }
        public ICollection<Materials> Materials { get; set; }

        public TypeOfJob()
        {
            Orders = new List<Order>();
            Materials = new List<Materials>();
        }
    }
}
