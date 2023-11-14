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
    public class TipoBebidaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TipoBebidaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TipoBebida
        public async Task<IActionResult> Index()
        {
              return _context.TipoBebida != null ? 
                          View(await _context.TipoBebida.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.TipoBebida'  is null.");
        }

        // GET: TipoBebida/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TipoBebida == null)
            {
                return NotFound();
            }

            var tipoBebida = await _context.TipoBebida
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoBebida == null)
            {
                return NotFound();
            }

            return View(tipoBebida);
        }

        // GET: TipoBebida/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoBebida/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,FechaRegistro")] TipoBebida tipoBebida)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoBebida);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoBebida);
        }

        // GET: TipoBebida/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TipoBebida == null)
            {
                return NotFound();
            }

            var tipoBebida = await _context.TipoBebida.FindAsync(id);
            if (tipoBebida == null)
            {
                return NotFound();
            }
            return View(tipoBebida);
        }

        // POST: TipoBebida/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,FechaRegistro")] TipoBebida tipoBebida)
        {
            if (id != tipoBebida.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoBebida);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoBebidaExists(tipoBebida.Id))
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
            return View(tipoBebida);
        }

        // GET: TipoBebida/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TipoBebida == null)
            {
                return NotFound();
            }

            var tipoBebida = await _context.TipoBebida
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoBebida == null)
            {
                return NotFound();
            }

            return View(tipoBebida);
        }

        // POST: TipoBebida/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TipoBebida == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TipoBebida'  is null.");
            }
            var tipoBebida = await _context.TipoBebida.FindAsync(id);
            if (tipoBebida != null)
            {
                _context.TipoBebida.Remove(tipoBebida);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoBebidaExists(int id)
        {
          return (_context.TipoBebida?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
