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
    public class PromocionesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PromocionesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Promociones
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Promociones.Include(p => p.ListaPrecios).Include(p => p.MetodoPago).Include(p => p.MontoVariacion);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Promociones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Promociones == null)
            {
                return NotFound();
            }

            var promociones = await _context.Promociones
                .Include(p => p.ListaPrecios)
                .Include(p => p.MetodoPago)
                .Include(p => p.MontoVariacion)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (promociones == null)
            {
                return NotFound();
            }

            return View(promociones);
        }

        // GET: Promociones/Create
        public IActionResult Create()
        {
            ViewData["ListaPreciosRefId"] = new SelectList(_context.ListaPrecios, "Id", "Producto");
            ViewData["MetodoPagoRefId"] = new SelectList(_context.MetodoPagos, "Id", "Descripcion");
            ViewData["MontoVariacionRefId"] = new SelectList(_context.MetodoPagos, "Id", "MontoVariacion");
            return View();
        }

        // POST: Promociones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,ListaPreciosRefId,MetodoPagoRefId,MontoVariacionRefId,TarifaPrecio,ValidoHasta,FechaRegistro")] Promociones promociones)
        {
            if (ModelState.IsValid)
            {
                if (promociones.ListaPreciosRefId != null && promociones.ListaPreciosRefId != 0)
                {
                    var listaPrecio = await _context.ListaPrecios.FindAsync(promociones.ListaPreciosRefId);
                    var metodoPago = await _context.MetodoPagos.FindAsync(promociones.MetodoPagoRefId);
                    promociones.TarifaPrecio = listaPrecio.Precio - (listaPrecio.Precio * metodoPago.MontoVariacion / 100);
                }
                else
                {
                    promociones.TarifaPrecio = 0;
                }

                _context.Add(promociones);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ListaPreciosRefId"] = new SelectList(_context.ListaPrecios, "Id", "Producto", promociones.ListaPreciosRefId);
            ViewData["MetodoPagoRefId"] = new SelectList(_context.MetodoPagos, "Id", "Descripcion", promociones.MetodoPagoRefId);
            ViewData["MontoVariacionRefId"] = new SelectList(_context.MetodoPagos, "Id", "MontoVariacion", promociones.MontoVariacionRefId);
            return View(promociones);
        }

        // GET: Promociones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Promociones == null)
            {
                return NotFound();
            }

            var promociones = await _context.Promociones.FindAsync(id);
            if (promociones == null)
            {
                return NotFound();
            }
            ViewData["ListaPreciosRefId"] = new SelectList(_context.ListaPrecios, "Id", "Producto", promociones.ListaPreciosRefId);
            ViewData["MetodoPagoRefId"] = new SelectList(_context.MetodoPagos, "Id", "Descripcion", promociones.MetodoPagoRefId);
            ViewData["MontoVariacionRefId"] = new SelectList(_context.MetodoPagos, "Id", "MontoVariacion", promociones.MontoVariacionRefId);
            return View(promociones);
        }

        // POST: Promociones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,ListaPreciosRefId,MetodoPagoRefId,MontoVariacionRefId,TarifaPrecio,ValidoHasta,FechaRegistro")] Promociones promociones)
        {
            if (id != promociones.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (promociones.ListaPreciosRefId != null && promociones.ListaPreciosRefId != 0)
                    {
                        var listaPrecio = await _context.ListaPrecios.FindAsync(promociones.ListaPreciosRefId);
                        var metodoPago = await _context.MetodoPagos.FindAsync(promociones.MetodoPagoRefId);
                        promociones.TarifaPrecio = listaPrecio.Precio - (listaPrecio.Precio * metodoPago.MontoVariacion / 100);
                    }
                    else
                    {
                        promociones.TarifaPrecio = 0;
                    }
                        
                    _context.Update(promociones);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PromocionesExists(promociones.Id))
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
            ViewData["ListaPreciosRefId"] = new SelectList(_context.ListaPrecios, "Id", "Producto", promociones.ListaPreciosRefId);
            ViewData["MetodoPagoRefId"] = new SelectList(_context.MetodoPagos, "Id", "Descripcion", promociones.MetodoPagoRefId);
            ViewData["MontoVariacionRefId"] = new SelectList(_context.MetodoPagos, "Id", "MontoVariacion", promociones.MontoVariacionRefId);
            return View(promociones);
        }

        // GET: Promociones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Promociones == null)
            {
                return NotFound();
            }

            var promociones = await _context.Promociones
                .Include(p => p.ListaPrecios)
                .Include(p => p.MetodoPago)
                .Include(p => p.MontoVariacion)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (promociones == null)
            {
                return NotFound();
            }

            return View(promociones);
        }

        // POST: Promociones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Promociones == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Promociones'  is null.");
            }
            var promociones = await _context.Promociones.FindAsync(id);
            if (promociones != null)
            {
                _context.Promociones.Remove(promociones);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PromocionesExists(int id)
        {
          return (_context.Promociones?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
