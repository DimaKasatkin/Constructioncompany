using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionСompany.ViewModel.Filter
{
    public class TypeOfJobFilter
    {
        public bool TypeFilter { get; set; }

        public TypeOfJobFilter(bool typeFilter)
        {
            TypeFilter = typeFilter;
        }
    }
}
