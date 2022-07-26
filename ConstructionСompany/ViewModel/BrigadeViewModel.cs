using ConstructionСompany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionСompany.ViewModel
{
    public class BrigadeViewModel
    {
        public IEnumerable<Brigade> Brigades { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
