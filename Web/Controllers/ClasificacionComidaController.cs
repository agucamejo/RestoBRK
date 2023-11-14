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
    public class ClasificacionComidaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClasificacionComidaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ClasificacionComida
        public async Task<IActionResult> Index()
        {
              return _context.ClasificacionComida != null ? 
                          View(await _context.ClasificacionComida.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.ClasificacionComida'  is null.");
        }

        // GET: ClasificacionComida/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ClasificacionComida == null)
            {
                return NotFound();
            }

            var clasificacionComida = await _context.ClasificacionComida
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clasificacionComida == null)
            {
                return NotFound();
            }

            return View(clasificacionComida);
        }

        // GET: ClasificacionComida/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ClasificacionComida/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,FechaRegistro")] ClasificacionComida clasificacionComida)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clasificacionComida);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(clasificacionComida);
        }

        // GET: ClasificacionComida/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ClasificacionComida == null)
            {
                return NotFound();
            }

            var clasificacionComida = await _context.ClasificacionComida.FindAsync(id);
            if (clasificacionComida == null)
            {
                return NotFound();
            }
            return View(clasificacionComida);
        }

        // POST: ClasificacionComida/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,FechaRegistro")] ClasificacionComida clasificacionComida)
        {
            if (id != clasificacionComida.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clasificacionComida);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClasificacionComidaExists(clasificacionComida.Id))
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
            return View(clasificacionComida);
        }

        // GET: ClasificacionComida/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ClasificacionComida == null)
            {
                return NotFound();
            }

            var clasificacionComida = await _context.ClasificacionComida
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clasificacionComida == null)
            {
                return NotFound();
            }

            return View(clasificacionComida);
        }

        // POST: ClasificacionComida/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ClasificacionComida == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ClasificacionComida'  is null.");
            }
            var clasificacionComida = await _context.ClasificacionComida.FindAsync(id);
            if (clasificacionComida != null)
            {
                _context.ClasificacionComida.Remove(clasificacionComida);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClasificacionComidaExists(int id)
        {
          return (_context.ClasificacionComida?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
