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
    public class TipoComidaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TipoComidaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TipoComida
        public async Task<IActionResult> Index()
        {
              return _context.TipoComida != null ? 
                          View(await _context.TipoComida.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.TipoComida'  is null.");
        }

        // GET: TipoComida/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TipoComida == null)
            {
                return NotFound();
            }

            var tipoComida = await _context.TipoComida
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoComida == null)
            {
                return NotFound();
            }

            return View(tipoComida);
        }

        // GET: TipoComida/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoComida/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,FechaRegistro")] TipoComida tipoComida)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoComida);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoComida);
        }

        // GET: TipoComida/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TipoComida == null)
            {
                return NotFound();
            }

            var tipoComida = await _context.TipoComida.FindAsync(id);
            if (tipoComida == null)
            {
                return NotFound();
            }
            return View(tipoComida);
        }

        // POST: TipoComida/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,FechaRegistro")] TipoComida tipoComida)
        {
            if (id != tipoComida.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoComida);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoComidaExists(tipoComida.Id))
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
            return View(tipoComida);
        }

        // GET: TipoComida/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TipoComida == null)
            {
                return NotFound();
            }

            var tipoComida = await _context.TipoComida
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoComida == null)
            {
                return NotFound();
            }

            return View(tipoComida);
        }

        // POST: TipoComida/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TipoComida == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TipoComida'  is null.");
            }
            var tipoComida = await _context.TipoComida.FindAsync(id);
            if (tipoComida != null)
            {
                _context.TipoComida.Remove(tipoComida);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoComidaExists(int id)
        {
          return (_context.TipoComida?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
