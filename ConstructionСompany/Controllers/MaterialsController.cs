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

namespace ConstructionСompany.Controllers
{
    public class MaterialsController : Controller
    {
        private readonly ConstructionCompanyContext _context;

        public MaterialsController(ConstructionCompanyContext context)
        {
            _context = context;
        }

        // GET: Materials
        public async Task<IActionResult> Index(int page = 1)
        {
            var pageSize = 5;
            var itemCount = _context.Materials.Count();

            IQueryable<Materials> materials = _context.Materials;

            materials = materials.Skip((page - 1) * pageSize)
                .Include(m => m.TypeOfJob)
                .Take(pageSize);

            return View(new MaterialViewModel()
            {
                Materials = await materials.ToListAsync(),
                PageViewModel = new PageViewModel(itemCount, page, pageSize)
            });
        }

        // GET: Materials/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materials = await _context.Materials
                .Include(m => m.TypeOfJob)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (materials == null)
            {
                return NotFound();
            }

            return View(materials);
        }

        // GET: Materials/Create
        public IActionResult Create()
        {
            ViewData["TypeOfJobId"] = new SelectList(_context.TypeOfJobs, "Id", "Name");
            return View();
        }

        // POST: Materials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Packaging,Description,Cost,TypeOfJobId")] Materials materials)
        {
            if (ModelState.IsValid)
            {
                _context.Add(materials);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TypeOfJobId"] = new SelectList(_context.TypeOfJobs, "Id", "Name", materials.TypeOfJobId);
            return View(materials);
        }

        // GET: Materials/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materials = await _context.Materials.FindAsync(id);
            if (materials == null)
            {
                return NotFound();
            }
            ViewData["TypeOfJobId"] = new SelectList(_context.TypeOfJobs, "Id", "Name", materials.TypeOfJobId);
            return View(materials);
        }

        // POST: Materials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Packaging,Description,Cost,TypeOfJobId")] Materials materials)
        {
            if (id != materials.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(materials);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaterialsExists(materials.Id))
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
            ViewData["TypeOfJobId"] = new SelectList(_context.TypeOfJobs, "Id", "Name", materials.TypeOfJobId);
            return View(materials);
        }

        // GET: Materials/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materials = await _context.Materials
                .Include(m => m.TypeOfJob)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (materials == null)
            {
                return NotFound();
            }

            return View(materials);
        }

        // POST: Materials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var materials = await _context.Materials.FindAsync(id);
            _context.Materials.Remove(materials);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MaterialsExists(int id)
        {
            return _context.Materials.Any(e => e.Id == id);
        }
    }
}
