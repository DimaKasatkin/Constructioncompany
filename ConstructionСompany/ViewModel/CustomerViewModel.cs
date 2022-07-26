using ConstructionСompany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionСompany.ViewModel
{
    public class CustomerViewModel
    {
        public IEnumerable<Customer> Customers { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
