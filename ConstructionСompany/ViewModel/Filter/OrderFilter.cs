using ConstructionСompany.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionСompany.ViewModel.Filter
{
    public class OrderFilter
    {
        public int? SelectedCustomerId { get; set; }
        public SelectList Customers { get; set; }
        public int? SelectedBrigadeId { get; set; }
        public SelectList Brigades { get; set; }

        public int Count { get; set; }

        public OrderFilter(int? selelectedCustomerId, int? selectedBrigadeId, IList<Customer> customers, IList<Brigade> brigades, int count)
        {
            brigades.Insert(0, new Brigade()
            {
                Id = 0,
                Name = "All"
            });
            customers.Insert(0, new Customer()
            {
                Id = 0,
                FullName = "All"
            });

            SelectedCustomerId = selelectedCustomerId;
            SelectedBrigadeId = selectedBrigadeId;
            Brigades = new SelectList(brigades, "Id", "Name", selectedBrigadeId);
            Customers = new SelectList(customers, "Id", "FullName", selelectedCustomerId);
            Count = count;
        }
    }
}
