using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionСompany.Models
{
    public class Order
    {
        public int Id { get; set; }
        public double Cost { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool CompletionMark { get; set; }
        public bool PaymentMark { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int TypeOfJobId { get; set; }
        public TypeOfJob TypeOfJobs { get; set; }

        public int BrigadeId { get; set; }
        public Brigade Brigade { get; set; }

    }
}
