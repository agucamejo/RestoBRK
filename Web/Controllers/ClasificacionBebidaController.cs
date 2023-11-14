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
    public class ClasificacionBebidaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClasificacionBebidaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ClasificacionBebida
        public async Task<IActionResult> Index()
        {
              return _context.ClasificacionBebida != null ? 
                          View(await _context.ClasificacionBebida.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.ClasificacionBebida'  is null.");
        }

        // GET: ClasificacionBebida/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ClasificacionBebida == null)
            {
                return NotFound();
            }

            var clasificacionBebida = await _context.ClasificacionBebida
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clasificacionBebida == null)
            {
                return NotFound();
            }

            return View(clasificacionBebida);
        }

        // GET: ClasificacionBebida/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ClasificacionBebida/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,FechaRegistro")] ClasificacionBebida clasificacionBebida)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clasificacionBebida);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(clasificacionBebida);
        }

        // GET: ClasificacionBebida/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ClasificacionBebida == null)
            {
                return NotFound();
            }

            var clasificacionBebida = await _context.ClasificacionBebida.FindAsync(id);
            if (clasificacionBebida == null)
            {
                return NotFound();
            }
            return View(clasificacionBebida);
        }

        // POST: ClasificacionBebida/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,FechaRegistro")] ClasificacionBebida clasificacionBebida)
        {
            if (id != clasificacionBebida.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clasificacionBebida);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClasificacionBebidaExists(clasificacionBebida.Id))
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
            return View(clasificacionBebida);
        }

        // GET: ClasificacionBebida/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ClasificacionBebida == null)
            {
                return NotFound();
            }

            var clasificacionBebida = await _context.ClasificacionBebida
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clasificacionBebida == null)
            {
                return NotFound();
            }

            return View(clasificacionBebida);
        }

        // POST: ClasificacionBebida/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ClasificacionBebida == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ClasificacionBebida'  is null.");
            }
            var clasificacionBebida = await _context.ClasificacionBebida.FindAsync(id);
            if (clasificacionBebida != null)
            {
                _context.ClasificacionBebida.Remove(clasificacionBebida);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClasificacionBebidaExists(int id)
        {
          return (_context.ClasificacionBebida?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
