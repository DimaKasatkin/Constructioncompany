using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionСompany.Models
{
    public class Materials
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Packaging { get; set; } //упаковка
        public string Description { get; set; }
        public double Cost { get; set; }

        public int TypeOfJobId { get; set; }
        public TypeOfJob TypeOfJob { get; set; }
    }
}
