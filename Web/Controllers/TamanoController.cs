using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Models;

namespace Web.Controllers
{
    public class TamanoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TamanoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tamano
        public async Task<IActionResult> Index()
        {
              return _context.Tamano != null ? 
                          View(await _context.Tamano.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Tamano'  is null.");
        }

        // GET: Tamano/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tamano == null)
            {
                return NotFound();
            }

            var tamano = await _context.Tamano
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tamano == null)
            {
                return NotFound();
            }

            return View(tamano);
        }

        // GET: Tamano/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tamano/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,FechaRegistro")] Tamano tamano)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tamano);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tamano);
        }

        // GET: Tamano/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tamano == null)
            {
                return NotFound();
            }

            var tamano = await _context.Tamano.FindAsync(id);
            if (tamano == null)
            {
                return NotFound();
            }
            return View(tamano);
        }

        // POST: Tamano/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,FechaRegistro")] Tamano tamano)
        {
            if (id != tamano.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tamano);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TamanoExists(tamano.Id))
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
            return View(tamano);
        }

        // GET: Tamano/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tamano == null)
            {
                return NotFound();
            }

            var tamano = await _context.Tamano
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tamano == null)
            {
                return NotFound();
            }

            return View(tamano);
        }

        // POST: Tamano/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tamano == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Tamano'  is null.");
            }
            var tamano = await _context.Tamano.FindAsync(id);
            if (tamano != null)
            {
                _context.Tamano.Remove(tamano);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TamanoExists(int id)
        {
          return (_context.Tamano?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
