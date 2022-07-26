using ConstructionСompany.Models;
using ConstructionСompany.ViewModel.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionСompany.ViewModel
{
    public class TypeOfJobViewModel
    {
        public IEnumerable<TypeOfJob> TypeOfJobs { get; set; }
        public TypeOfJobFilter TypeOfJobFilter { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
