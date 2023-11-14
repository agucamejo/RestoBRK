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
    public class PorcionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PorcionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Porcion
        public async Task<IActionResult> Index()
        {
              return _context.Porciones != null ? 
                          View(await _context.Porciones.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Porciones'  is null.");
        }

        // GET: Porcion/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Porciones == null)
            {
                return NotFound();
            }

            var porcion = await _context.Porciones
                .FirstOrDefaultAsync(m => m.Id == id);
            if (porcion == null)
            {
                return NotFound();
            }

            return View(porcion);
        }

        // GET: Porcion/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Porcion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,FechaRegistro")] Porcion porcion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(porcion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(porcion);
        }

        // GET: Porcion/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Porciones == null)
            {
                return NotFound();
            }

            var porcion = await _context.Porciones.FindAsync(id);
            if (porcion == null)
            {
                return NotFound();
            }
            return View(porcion);
        }

        // POST: Porcion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,FechaRegistro")] Porcion porcion)
        {
            if (id != porcion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(porcion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PorcionExists(porcion.Id))
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
            return View(porcion);
        }

        // GET: Porcion/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Porciones == null)
            {
                return NotFound();
            }

            var porcion = await _context.Porciones
                .FirstOrDefaultAsync(m => m.Id == id);
            if (porcion == null)
            {
                return NotFound();
            }

            return View(porcion);
        }

        // POST: Porcion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Porciones == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Porciones'  is null.");
            }
            var porcion = await _context.Porciones.FindAsync(id);
            if (porcion != null)
            {
                _context.Porciones.Remove(porcion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PorcionExists(int id)
        {
          return (_context.Porciones?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
