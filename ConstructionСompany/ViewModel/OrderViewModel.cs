using ConstructionСompany.Models;
using ConstructionСompany.ViewModel.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionСompany.ViewModel
{
    public class OrderViewModel
    {
        public IEnumerable<Order> Orders { get; set; }
        public OrderFilter OrderFilter { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
