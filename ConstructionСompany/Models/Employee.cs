using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionСompany.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public string Sex { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string PassportData { get; set; }

        public int PositionId { get; set; }
        public Position Position { get; set; }

        public int BrigadeId { get; set; }
        public Brigade Brigade { get; set; }
    }
}
