using ConstructionСompany.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionСompany.ViewModel.Filter
{
    public class EmployeeFilter
    {
        public int? SelectedBrigadeId { get; set; }
        public int? SelectedPositionId { get; set; }
        public SelectList Brigades { get; set; }
        public SelectList Positions { get; set; }
        
        public EmployeeFilter(int? selelectedBrigadeId, int? selectedPositionId, IList<Brigade> brigades, IList<Position> positions)
        {
            positions.Insert(0, new Position()
            {
                Id = 0,
                Name = "All"
            });
            brigades.Insert(0, new Brigade()
            {
                Id = 0,
                Name = "All"
            });

            SelectedBrigadeId = selelectedBrigadeId;
            SelectedPositionId = SelectedPositionId;
            Positions = new SelectList(positions, "Id", "Name", selectedPositionId);
            Brigades = new SelectList(brigades, "Id", "Name", selelectedBrigadeId);
        }
    }
}
