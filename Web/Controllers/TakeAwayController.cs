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
    public class TakeAwayController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TakeAwayController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TakeAway
        public async Task<IActionResult> Index(string search)
        {
            var takeAways = await _context.TakeAways.ToListAsync();

            if (!string.IsNullOrEmpty(search))
            {
                search = search.Trim();

                takeAways = takeAways
                    .Where(lp => lp.NombreCliente.Contains(search, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
            return View(takeAways);
        }

        // GET: TakeAway/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var model = new TakeAway();
            model.Productos.Add(new ProductosPedido());

            if (id == null || _context.TakeAways == null)
            {
                return NotFound();
            }

            var takeAway = await _context.TakeAways
                .Include(t => t.MetodoPago)
                .Include(t => t.Promocion)
                .Include(t => t.Productos)
                .FirstOrDefaultAsync(m => m.Id == id);

            var promociones = _context.Promociones
               .Include(p => p.MetodoPago)
               .Select(x => new
               {
                   x.Id,
                   DescPromocionMetodo = x.Descripcion + " - " + x.MetodoPago.Descripcion
               });

            var producto = _context.ListaPrecios
              .Select(x => new
              {
                  x.Id,
                  ProductoPrecio = x.Producto + " - " + x.Precio
              });

            if (takeAway == null)
            {
                return NotFound();
            }

            ViewData["ListaPrecioRefId"] = new SelectList(producto, "Id", "ProductoPrecio");
            ViewData["MetodoPagoRefId"] = new SelectList(_context.MetodoPagos, "Id", "Descripcion");
            ViewData["PromocionRefId"] = new SelectList(promociones, "Id", "DescPromocionMetodo");

            return View(takeAway);
        }

        // GET: TakeAway/Create
        public IActionResult Create()
        {
            var model = new TakeAway();
            model.Productos.Add(new ProductosPedido());

            var promociones = _context.Promociones
               .Include(p => p.MetodoPago)
               .Select(x => new
               {
                   x.Id,
                   DescPromocionMetodo = x.Descripcion + " - " + x.MetodoPago.Descripcion
               });

            var producto = _context.ListaPrecios
               .Select(x => new
               {
                   x.Id,
                   ProductoPrecio = x.Producto + " - " + x.Precio
               });

            ViewData["ListaPrecioRefId"] = new SelectList(producto, "Id", "ProductoPrecio");
            ViewData["MetodoPagoRefId"] = new SelectList(_context.MetodoPagos, "Id", "Descripcion");
            ViewData["PromocionRefId"] = new SelectList(promociones, "Id", "DescPromocionMetodo");

            return View(model);
        }

        // POST: TakeAway/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Productos,MetodoPagoRefId,HorarioEntrega,PromocionRefId,NombreCliente,PrecioPedido,FechaRegistro")] TakeAway takeAway)
        {
            var coincideMetodoPromocion = true;
            if (takeAway.PromocionRefId.HasValue)
            {
                var metodoPago = _context.MetodoPagos.Where(x => x.Id.Equals(takeAway.MetodoPagoRefId)).FirstOrDefault();
                var promocion = _context.Promociones.Include(p => p.MetodoPago).Where(x => x.Id.Equals(takeAway.PromocionRefId)).FirstOrDefault();

                if (promocion != null)
                {
                    coincideMetodoPromocion = metodoPago.Descripcion.Equals(promocion.MetodoPago.Descripcion);
                }
                else
                {
                    coincideMetodoPromocion = true;
                }
            }

            var promociones = _context.Promociones
               .Include(p => p.MetodoPago)
               .Select(x => new
               {
                   x.Id,
                   DescPromocionMetodo = x.Descripcion + " - " + x.MetodoPago.Descripcion
               });

            var producto = _context.ListaPrecios
               .Select(x => new
               {
                   x.Id,
                   ProductoPrecio = x.Producto + " - " + x.Precio
               });

            if (ModelState.IsValid && coincideMetodoPromocion)
            {
                _context.Add(takeAway);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                if (!coincideMetodoPromocion)
                    ModelState.AddModelError("ValidationError", "El método de pago de la promoción no coincide con el elegido para abonar este pedido.");
            }


            ViewData["ListaPrecioRefId"] = new SelectList(producto, "Id", "ProductoPrecio");
            ViewData["MetodoPagoRefId"] = new SelectList(_context.MetodoPagos, "Id", "Descripcion", takeAway.MetodoPagoRefId);
            ViewData["PromocionRefId"] = new SelectList(promociones, "Id", "DescPromocionMetodo", takeAway.PromocionRefId);
            
            return View(takeAway);
        }

        [HttpGet]
        public IActionResult GetPrecioProducto(int listaPrecioId)
        {
            var precioProducto = _context.ListaPrecios
               .Where(x => x.Id == listaPrecioId)
               .Select(x => x.Precio)
               .FirstOrDefault();

            return Json(precioProducto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddProducto(TakeAway takeAway)
        {

            takeAway.Productos.Add(new ProductosPedido());

            var producto = _context.ListaPrecios
               .Select(x => new
               {
                   x.Id,
                   ProductoPrecio = x.Producto + " - " + x.Precio
               });

            ViewData["ListaPrecioRefId"] = new SelectList(producto, "Id", "ProductoPrecio");

            return PartialView("ProductosPedido", takeAway);
        }

        // GET: TakeAway/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var model = new TakeAway();
            model.Productos.Add(new ProductosPedido());

            if (id == null || _context.TakeAways == null)
            {
                return NotFound();
            }

            var takeAway = _context.TakeAways
                .Include(f => f.MetodoPago)
                .Include(f => f.Promocion)
                .Include(f => f.Productos)
                .Where(x => x.Id.Equals(id))
                .FirstOrDefault(); 

            //var takeAway = await _context.TakeAways.FindAsync(id);

            if (takeAway == null)
            {
                return NotFound();
            }
            var promociones = _context.Promociones
              .Include(p => p.MetodoPago)
              .Select(x => new
              {
                  x.Id,
                  DescPromocionMetodo = x.Descripcion + " - " + x.MetodoPago.Descripcion
              });

            var producto = _context.ListaPrecios
               .Select(x => new
               {
                   x.Id,
                   ProductoPrecio = x.Producto + " - " + x.Precio
               });

            ViewData["ListaPrecioRefId"] = new SelectList(producto, "Id", "ProductoPrecio", takeAway);
            ViewData["MetodoPagoRefId"] = new SelectList(_context.MetodoPagos, "Id", "Descripcion", takeAway.MetodoPagoRefId);
            ViewData["PromocionRefId"] = new SelectList(promociones, "Id", "DescPromocionMetodo", takeAway.PromocionRefId);
            
            return View(takeAway);
        }


        // POST: TakeAway/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Productos,MetodoPagoRefId,HorarioEntrega,PromocionRefId,NombreCliente,PrecioPedido,FechaRegistro")] TakeAway takeAway)
        {
            if (id != takeAway.Id)
            {
                return NotFound();
            }

            var coincideMetodoPromocion = true;
            if (takeAway.PromocionRefId.HasValue)
            {
                var metodoPago = _context.MetodoPagos.Where(x => x.Id.Equals(takeAway.MetodoPagoRefId)).FirstOrDefault();
                var promocion = _context.Promociones.Include(p => p.MetodoPago).Where(x => x.Id.Equals(takeAway.PromocionRefId)).FirstOrDefault();

                if (promocion != null)
                {
                    coincideMetodoPromocion = metodoPago.Descripcion.Equals(promocion.MetodoPago.Descripcion);
                }
                else
                {
                    coincideMetodoPromocion = true;
                }
            }

            if (ModelState.IsValid && coincideMetodoPromocion)
            {
                try
                {
                    var productosPedido = _context.ProductosPedidos.Where(x => x.TakeAwayId.Equals(takeAway.Id));
                    _context.ProductosPedidos.RemoveRange(productosPedido);

                    _context.Update(takeAway);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TakeAwayExists(takeAway.Id))
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
            else
            {
                if (!coincideMetodoPromocion)
                    ModelState.AddModelError("ValidationError", "El método de pago de la promoción no coincide con el elegido para abonar este pedido.");
            }

            var promociones = _context.Promociones
               .Include(p => p.MetodoPago)
               .Select(x => new
               {
                   x.Id,
                   DescPromocionMetodo = x.Descripcion + " - " + x.MetodoPago.Descripcion
               });

            var producto = _context.ListaPrecios
               .Select(x => new
               {
                   x.Id,
                   ProductoPrecio = x.Producto + " - " + x.Precio
               });

            var promocionSelectedValue = takeAway.PromocionRefId.HasValue ? takeAway.PromocionRefId.Value.ToString() : null;

            ViewData["ListaPrecioRefId"] = new SelectList(producto, "Id", "ProductoPrecio", takeAway);
            ViewData["MetodoPagoRefId"] = new SelectList(_context.MetodoPagos, "Id", "Descripcion", takeAway.MetodoPagoRefId);
            ViewData["PromocionRefId"] = new SelectList(promociones, "Id", "DescPromocionMetodo", takeAway.PromocionRefId);

            return View(takeAway);
        }

        // GET: TakeAway/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TakeAways == null)
            {
                return NotFound();
            }

            var producto = _context.ListaPrecios
               .Select(x => new
               {
                   x.Id,
                   ProductoPrecio = x.Producto + " - " + x.Precio
               });

            var promociones = _context.Promociones
               .Include(p => p.MetodoPago)
               .Select(x => new
               {
                   x.Id,
                   DescPromocionMetodo = x.Descripcion + " - " + x.MetodoPago.Descripcion
               });


            ViewData["ListaPrecioRefId"] = new SelectList(producto, "Id", "ProductoPrecio");
            ViewData["MetodoPagoRefId"] = new SelectList(_context.MetodoPagos, "Id", "Descripcion");
            ViewData["PromocionRefId"] = new SelectList(promociones, "Id", "DescPromocionMetodo");

            var takeAway = await _context.TakeAways
                .Include(t => t.MetodoPago)
                .Include(t => t.Promocion)
                .Include(t => t.Productos)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (takeAway == null)
            {
                return NotFound();
            }

            return View(takeAway);
        }

        // POST: TakeAway/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TakeAways == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TakeAways'  is null.");
            }

            var takeAway = await _context.TakeAways
                .Include(t => t.Productos)
                .Include(t => t.Promocion)
                .Include(t => t.MetodoPago)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (takeAway != null)
            {
                _context.TakeAways.Remove(takeAway);
                _context.ProductosPedidos.RemoveRange(takeAway.Productos);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TakeAwayExists(int id)
        {
          return (_context.TakeAways?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
