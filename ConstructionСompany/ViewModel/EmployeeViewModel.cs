using ConstructionСompany.Models;
using ConstructionСompany.ViewModel.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionСompany.ViewModel
{
    public class EmployeeViewModel
    {
        public IEnumerable<Employee> Employees { get; set; }
        public EmployeeFilter EmployeeFilter { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
