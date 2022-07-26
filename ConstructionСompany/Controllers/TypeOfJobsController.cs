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
    public class TypeOfJobsController : Controller
    {
        private readonly ConstructionCompanyContext _context;

        public TypeOfJobsController(ConstructionCompanyContext context)
        {
            _context = context;
        }

        // GET: TypeOfJobs
        public async Task<IActionResult> Index(bool sortTypeByMaterial, int page = 1)
        {
            var pageSize = 5;
            var itemCount = _context.TypeOfJobs.Count();

            IQueryable<TypeOfJob> typeOfJobs = _context.TypeOfJobs;

            if (sortTypeByMaterial)
            {
                typeOfJobs = typeOfJobs.Where(typeOfJobs => typeOfJobs.Materials.Count() >= 3);
            }

            typeOfJobs = typeOfJobs.Skip((page - 1) * pageSize)
                .Take(pageSize);

            return View(new TypeOfJobViewModel()
            {
                TypeOfJobs = await typeOfJobs.ToListAsync(),
                PageViewModel = new PageViewModel(itemCount, page, pageSize),
                TypeOfJobFilter = new TypeOfJobFilter(sortTypeByMaterial)
            });
        }

        // GET: TypeOfJobs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeOfJob = await _context.TypeOfJobs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeOfJob == null)
            {
                return NotFound();
            }

            return View(typeOfJob);
        }

        // GET: TypeOfJobs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TypeOfJobs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Cost")] TypeOfJob typeOfJob)
        {
            if (ModelState.IsValid)
            {
                _context.Add(typeOfJob);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(typeOfJob);
        }

        // GET: TypeOfJobs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeOfJob = await _context.TypeOfJobs.FindAsync(id);
            if (typeOfJob == null)
            {
                return NotFound();
            }
            return View(typeOfJob);
        }

        // POST: TypeOfJobs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Cost")] TypeOfJob typeOfJob)
        {
            if (id != typeOfJob.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(typeOfJob);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypeOfJobExists(typeOfJob.Id))
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
            return View(typeOfJob);
        }

        // GET: TypeOfJobs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeOfJob = await _context.TypeOfJobs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeOfJob == null)
            {
                return NotFound();
            }

            return View(typeOfJob);
        }

        // POST: TypeOfJobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var typeOfJob = await _context.TypeOfJobs.FindAsync(id);
            _context.TypeOfJobs.Remove(typeOfJob);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypeOfJobExists(int id)
        {
            return _context.TypeOfJobs.Any(e => e.Id == id);
        }
    }
}
