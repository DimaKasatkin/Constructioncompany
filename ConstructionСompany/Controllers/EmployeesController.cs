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
    public class EmployeesController : Controller
    {
        private readonly ConstructionCompanyContext _context;

        public EmployeesController(ConstructionCompanyContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index(int? selectedBrigadeId, int? selectedPositionId, int page = 1)
        {
            var pageSize = 5;
            var itemCount = _context.Employees.Count();

            IQueryable<Employee> employees = _context.Employees;

            if (selectedBrigadeId.HasValue && selectedBrigadeId != 0)
            {
                employees = employees.Where(employee => employee.BrigadeId == selectedBrigadeId);
            }

            if (selectedPositionId.HasValue && selectedPositionId != 0)
            {
                employees = employees.Where(employee => employee.PositionId == selectedPositionId);
            }

            employees = employees.Skip((page - 1) * pageSize)
                .Include(e => e.Brigade).Include(e => e.Position)
                .Take(pageSize);

            return View(new EmployeeViewModel()
            {
                Employees = await employees.ToListAsync(),
                PageViewModel = new PageViewModel(itemCount, page, pageSize),
                EmployeeFilter = new EmployeeFilter(selectedBrigadeId, selectedPositionId, 
                                                    await _context.Brigades.ToListAsync(), await _context.Positions.ToListAsync())
            });
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.Brigade)
                .Include(e => e.Position)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewData["BrigadeId"] = new SelectList(_context.Brigades, "Id", "Name");
            ViewData["PositionId"] = new SelectList(_context.Positions, "Id", "Name");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FullName,Age,Sex,Address,Phone,PassportData,PositionId,BrigadeId")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrigadeId"] = new SelectList(_context.Brigades, "Id", "Name", employee.BrigadeId);
            ViewData["PositionId"] = new SelectList(_context.Positions, "Id", "Name", employee.PositionId);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["BrigadeId"] = new SelectList(_context.Brigades, "Id", "Name", employee.BrigadeId);
            ViewData["PositionId"] = new SelectList(_context.Positions, "Id", "Name", employee.PositionId);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,Age,Sex,Address,Phone,PassportData,PositionId,BrigadeId")] Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
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
            ViewData["BrigadeId"] = new SelectList(_context.Brigades, "Id", "Name", employee.BrigadeId);
            ViewData["PositionId"] = new SelectList(_context.Positions, "Id", "Name", employee.PositionId);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.Brigade)
                .Include(e => e.Position)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}
