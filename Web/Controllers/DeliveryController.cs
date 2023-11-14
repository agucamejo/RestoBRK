using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Migrations;
using Web.Models;

namespace Web.Controllers
{
    public class DeliveryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DeliveryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Delivery
        public async Task<IActionResult> Index(string search)
        {

            var deliveries = await _context.Deliveries.ToListAsync();

            if (!string.IsNullOrEmpty(search))
            {
                search = search.Trim();

                deliveries = deliveries
                    .Where(lp => lp.NombreCliente.Contains(search, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            return View(deliveries);
        }

        // GET: Delivery/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var model = new Delivery();
            model.Productos.Add(new ProductosPedido());

            if (id == null || _context.Deliveries == null)
            {
                return NotFound();
            }

            var delivery = await _context.Deliveries
                .Include(d => d.MetodoPago)
                .Include(d => d.Promocion)
                .Include(d => d.Productos)
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

            if (delivery == null)
            {
                return NotFound();
            }

            ViewData["ListaPrecioRefId"] = new SelectList(producto, "Id", "ProductoPrecio");
            ViewData["MetodoPagoRefId"] = new SelectList(_context.MetodoPagos, "Id", "Descripcion");
            ViewData["PromocionRefId"] = new SelectList(promociones, "Id", "DescPromocionMetodo");

            return View(delivery);
        }

        // GET: Delivery/Create
        public IActionResult Create()
        {
            var model = new Delivery();
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

        // POST: Delivery/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Productos,MetodoPagoRefId,PromocionRefId,NombreCliente,DireccionCliente,PrecioPedido,FechaRegistro")] Delivery delivery)
        {
            var coincideMetodoPromocion = true;
            if (delivery.PromocionRefId.HasValue)
            {
                var metodoPago = _context.MetodoPagos.Where(x => x.Id.Equals(delivery.MetodoPagoRefId)).FirstOrDefault();
                var promocion = _context.Promociones.Include(p => p.MetodoPago).Where(x => x.Id.Equals(delivery.PromocionRefId)).FirstOrDefault();

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
                _context.Add(delivery);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                if (!coincideMetodoPromocion)
                    ModelState.AddModelError("ValidationError", "El método de pago de la promoción no coincide con el elegido para abonar este pedido.");
            }


            ViewData["ListaPrecioRefId"] = new SelectList(producto, "Id", "ProductoPrecio");
            ViewData["MetodoPagoRefId"] = new SelectList(_context.MetodoPagos, "Id", "Descripcion", delivery.MetodoPagoRefId);
            ViewData["PromocionRefId"] = new SelectList(promociones, "Id", "DescPromocionMetodo", delivery.PromocionRefId);
            return View(delivery);
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
        public async Task<ActionResult> AddProducto(Delivery delivery)
        {

            delivery.Productos.Add(new ProductosPedido());

            var producto = _context.ListaPrecios
               .Select(x => new
               {
                   x.Id,
                   ProductoPrecio = x.Producto + " - " + x.Precio
               });

            ViewData["ListaPrecioRefId"] = new SelectList(producto, "Id", "ProductoPrecio");

            return PartialView("ProductosPedido", delivery);
        }

        // GET: Delivery/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var model = new Delivery();
            model.Productos.Add(new ProductosPedido());

            if (id == null || _context.Deliveries == null)
            {
                return NotFound();
            }

            var delivery = _context.Deliveries
                .Include(f => f.MetodoPago)
                .Include(f => f.Promocion)
                .Include(f => f.Productos)
                .Where(x => x.Id.Equals(id))
                .FirstOrDefault();
            //var delivery = await _context.Deliveries.FindAsync(id);
            if (delivery == null)
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

            ViewData["ListaPrecioRefId"] = new SelectList(producto, "Id", "ProductoPrecio", delivery);
            ViewData["MetodoPagoRefId"] = new SelectList(_context.MetodoPagos, "Id", "Descripcion", delivery.MetodoPagoRefId);
            ViewData["PromocionRefId"] = new SelectList(promociones, "Id", "DescPromocionMetodo", delivery.PromocionRefId);
            return View(delivery);
        }

        // POST: Delivery/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Productos, MetodoPagoRefId,PromocionRefId,NombreCliente,DireccionCliente,PrecioPedido,FechaRegistro")] Delivery delivery)
        {
            if (id != delivery.Id)
            {
                return NotFound();
            }

            var coincideMetodoPromocion = true;
            if (delivery.PromocionRefId.HasValue)
            {
                var metodoPago = _context.MetodoPagos.Where(x => x.Id.Equals(delivery.MetodoPagoRefId)).FirstOrDefault();
                var promocion = _context.Promociones.Include(p => p.MetodoPago).Where(x => x.Id.Equals(delivery.PromocionRefId)).FirstOrDefault();

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
                    var productosPedido = _context.ProductosPedidos.Where(x => x.DeliveryId.Equals(delivery.Id));
                    _context.ProductosPedidos.RemoveRange(productosPedido);

                    _context.Update(delivery);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeliveryExists(delivery.Id))
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

            var promocionSelectedValue = delivery.PromocionRefId.HasValue ? delivery.PromocionRefId.Value.ToString() : null;

            ViewData["ListaPrecioRefId"] = new SelectList(producto, "Id", "ProductoPrecio", delivery);
            ViewData["MetodoPagoRefId"] = new SelectList(_context.MetodoPagos, "Id", "Descripcion", delivery.MetodoPagoRefId);
            ViewData["PromocionRefId"] = new SelectList(promociones, "Id", "DescPromocionMetodo", delivery.PromocionRefId);
            return View(delivery);
        }

        // GET: Delivery/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Deliveries == null)
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

            var delivery = await _context.Deliveries
                .Include(d => d.MetodoPago)
                .Include(d => d.Promocion)
                .Include(d => d.Productos)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (delivery == null)
            {
                return NotFound();
            }

            return View(delivery);
        }

        // POST: Delivery/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Deliveries == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Deliveries'  is null.");
            }
            var delivery = await _context.Deliveries
                .Include(t => t.Productos)
                .Include(f => f.Promocion)
                .Include(f => f.MetodoPago)
                .FirstOrDefaultAsync(t => t.Id == id);
            if (delivery != null)
            {
                _context.Deliveries.Remove(delivery);
                _context.ProductosPedidos.RemoveRange(delivery.Productos);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeliveryExists(int id)
        {
          return (_context.Deliveries?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
