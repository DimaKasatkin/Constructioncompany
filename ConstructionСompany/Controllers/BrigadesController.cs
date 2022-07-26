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
    public class BrigadesController : Controller
    {
        private readonly ConstructionCompanyContext _context;

        public BrigadesController(ConstructionCompanyContext context)
        {
            _context = context;
        }

        // GET: Brigades
        public async Task<IActionResult> Index(int page = 1)
        {
            var pageSize = 5;
            var itemCount = _context.Brigades.Count();

            IQueryable<Brigade> brigades = _context.Brigades;
      
            brigades = brigades.Skip((page - 1) * pageSize)
                .Take(pageSize);

            return View(new BrigadeViewModel()
            {
                Brigades = await brigades.ToListAsync(),
                PageViewModel = new PageViewModel(itemCount, page, pageSize)
            });
        }

        // GET: Brigades/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brigade = await _context.Brigades
                .FirstOrDefaultAsync(m => m.Id == id);
            if (brigade == null)
            {
                return NotFound();
            }

            return View(brigade);
        }

        // GET: Brigades/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Brigades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Brigade brigade)
        {
            if (ModelState.IsValid)
            {
                _context.Add(brigade);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(brigade);
        }

        // GET: Brigades/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brigade = await _context.Brigades.FindAsync(id);
            if (brigade == null)
            {
                return NotFound();
            }
            return View(brigade);
        }

        // POST: Brigades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Brigade brigade)
        {
            if (id != brigade.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(brigade);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BrigadeExists(brigade.Id))
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
            return View(brigade);
        }

        // GET: Brigades/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brigade = await _context.Brigades
                .FirstOrDefaultAsync(m => m.Id == id);
            if (brigade == null)
            {
                return NotFound();
            }

            return View(brigade);
        }

        // POST: Brigades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var brigade = await _context.Brigades.FindAsync(id);
            _context.Brigades.Remove(brigade);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BrigadeExists(int id)
        {
            return _context.Brigades.Any(e => e.Id == id);
        }
    }
}
