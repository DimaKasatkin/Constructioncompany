using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ConstructionСompany.Data;
using ConstructionСompany.Models;
using ConstructionСompany.ViewModel;
using ConstructionСompany.ViewModel.Filter;

namespace ConstructionСompany.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ConstructionCompanyContext _context;

        public OrdersController(ConstructionCompanyContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index(int? selectedCustomerId, int? selectedBrigadeId, int page = 1)
        {
            var pageSize = 5;
            var itemCount = _context.Orders.Count();
            var count = 0;

            IQueryable<Order> orders = _context.Orders;

            if (selectedBrigadeId.HasValue && selectedBrigadeId != 0)
            {
                count = orders.Where(order => order.BrigadeId == selectedBrigadeId && order.CompletionMark == true).Count();
            }

            if (selectedCustomerId.HasValue && selectedCustomerId != 0)
            {
                orders = orders.Where(order => order.CustomerId == selectedCustomerId);
            }

            orders = orders.Skip((page - 1) * pageSize)
                .Include(o => o.Brigade)
                .Include(o => o.Customer)
                .Include(o => o.TypeOfJobs)
                .Take(pageSize);

            return View(new OrderViewModel()
            {
                Orders = await orders.ToListAsync(),
                PageViewModel = new PageViewModel(itemCount, page, pageSize),
                OrderFilter = new OrderFilter(selectedCustomerId, selectedBrigadeId,
                                              await _context.Customers.ToListAsync(),
                                              await _context.Brigades.ToListAsync(), count)
            });
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Brigade)
                .Include(o => o.Customer)
                .Include(o => o.TypeOfJobs)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["BrigadeId"] = new SelectList(_context.Brigades, "Id", "Name");
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "FullName");
            ViewData["TypeOfJobId"] = new SelectList(_context.TypeOfJobs, "Id", "Name");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Cost,StartDate,EndDate,CompletionMark,PaymentMark,CustomerId,TypeOfJobId,BrigadeId")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrigadeId"] = new SelectList(_context.Brigades, "Id", "Name", order.BrigadeId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "FullName", order.CustomerId);
            ViewData["TypeOfJobId"] = new SelectList(_context.TypeOfJobs, "Id", "Name", order.TypeOfJobId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["BrigadeId"] = new SelectList(_context.Brigades, "Id", "Name", order.BrigadeId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "FullName", order.CustomerId);
            ViewData["TypeOfJobId"] = new SelectList(_context.TypeOfJobs, "Id", "Name", order.TypeOfJobId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Cost,StartDate,EndDate,CompletionMark,PaymentMark,CustomerId,TypeOfJobId,BrigadeId")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrigadeId"] = new SelectList(_context.Brigades, "Id", "Name", order.BrigadeId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "FullName", order.CustomerId);
            ViewData["TypeOfJobId"] = new SelectList(_context.TypeOfJobs, "Id", "Name", order.TypeOfJobId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Brigade)
                .Include(o => o.Customer)
                .Include(o => o.TypeOfJobs)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
