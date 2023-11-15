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
    public class MesasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MesasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Mesas
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Mesas.Include(m => m.MetodoPago).Include(m => m.Promocion);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Mesas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var model = new Mesas();
            model.Productos.Add(new ProductosPedido());

            if (id == null || _context.Mesas == null)
            {
                return NotFound();
            }

            var mesas = await _context.Mesas
                .Include(m => m.MetodoPago)
                .Include(m => m.Promocion)
                .Include(m => m.Productos)
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

            if (mesas == null)
            {
                return NotFound();
            }

            ViewData["ListaPrecioRefId"] = new SelectList(producto, "Id", "ProductoPrecio");
            ViewData["MetodoPagoRefId"] = new SelectList(_context.MetodoPagos, "Id", "Descripcion");
            ViewData["PromocionRefId"] = new SelectList(promociones, "Id", "DescPromocionMetodo");

            return View(mesas);
        }

        // GET: Mesas/Create
        public IActionResult Create()
        {
            var model = new Mesas();
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

        // POST: Mesas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Productos,MetodoPagoRefId,NroMesa,PromocionRefId,PrecioPedido,FechaRegistro")] Mesas mesas)
        {
            var coincideMetodoPromocion = true;
            if (mesas.PromocionRefId.HasValue)
            {
                var metodoPago = _context.MetodoPagos.Where(x => x.Id.Equals(mesas.MetodoPagoRefId)).FirstOrDefault();
                var promocion = _context.Promociones.Include(p => p.MetodoPago).Where(x => x.Id.Equals(mesas.PromocionRefId)).FirstOrDefault();

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
                _context.Add(mesas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                if (!coincideMetodoPromocion)
                    ModelState.AddModelError("ValidationError", "El método de pago de la promoción no coincide con el elegido para abonar este pedido.");
            }

            ViewData["ListaPrecioRefId"] = new SelectList(producto, "Id", "ProductoPrecio");
            ViewData["MetodoPagoRefId"] = new SelectList(_context.MetodoPagos, "Id", "Descripcion", mesas.MetodoPagoRefId);
            ViewData["PromocionRefId"] = new SelectList(promociones, "Id", "DescPromocionMetodo", mesas.PromocionRefId);
           
            return View(mesas);
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
        public async Task<ActionResult> AddProducto(Mesas mesas)
        {

            mesas.Productos.Add(new ProductosPedido());

            var producto = _context.ListaPrecios
               .Select(x => new
               {
                   x.Id,
                   ProductoPrecio = x.Producto + " - " + x.Precio
               });

            ViewData["ListaPrecioRefId"] = new SelectList(producto, "Id", "ProductoPrecio");

            return PartialView("ProductosPedido", mesas);
        }

        // GET: Mesas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var model = new Mesas();
            model.Productos.Add(new ProductosPedido());

            if (id == null || _context.Mesas == null)
            {
                return NotFound();
            }

            //var mesas = await _context.Mesas.FindAsync(id);
            var mesas = _context.Mesas
                .Include(f => f.MetodoPago)
                .Include(f => f.Promocion)
                .Include(f => f.Productos)
                .Where(x => x.Id.Equals(id))
                .FirstOrDefault();

            if (mesas == null)
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

            ViewData["ListaPrecioRefId"] = new SelectList(producto, "Id", "ProductoPrecio", mesas);
            ViewData["MetodoPagoRefId"] = new SelectList(_context.MetodoPagos, "Id", "Descripcion", mesas.MetodoPagoRefId);
            ViewData["PromocionRefId"] = new SelectList(promociones, "Id", "DescPromocionMetodo", mesas.PromocionRefId);
            
            return View(mesas);
        }

        // POST: Mesas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Productos,MetodoPagoRefId,NroMesa,PromocionRefId,PrecioPedido,FechaRegistro")] Mesas mesas)
        {
            if (id != mesas.Id)
            {
                return NotFound();
            }

            var coincideMetodoPromocion = true;
            if (mesas.PromocionRefId.HasValue)
            {
                var metodoPago = _context.MetodoPagos.Where(x => x.Id.Equals(mesas.MetodoPagoRefId)).FirstOrDefault();
                var promocion = _context.Promociones.Include(p => p.MetodoPago).Where(x => x.Id.Equals(mesas.PromocionRefId)).FirstOrDefault();

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
                    var productosPedido = _context.ProductosPedidos.Where(x => x.MesaId.Equals(mesas.Id));
                    _context.ProductosPedidos.RemoveRange(productosPedido);

                    _context.Update(mesas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MesasExists(mesas.Id))
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

            var promocionSelectedValue = mesas.PromocionRefId.HasValue ? mesas.PromocionRefId.Value.ToString() : null;

            ViewData["ListaPrecioRefId"] = new SelectList(producto, "Id", "ProductoPrecio", mesas);
            ViewData["MetodoPagoRefId"] = new SelectList(_context.MetodoPagos, "Id", "Descripcion", mesas.MetodoPagoRefId);
            ViewData["PromocionRefId"] = new SelectList(promociones, "Id", "DescPromocionMetodo", mesas.PromocionRefId);

            return View(mesas);
        }

        // GET: Mesas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Mesas == null)
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

            var mesas = await _context.Mesas
                .Include(m => m.MetodoPago)
                .Include(m => m.Promocion)
                .Include(m => m.Productos)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mesas == null)
            {
                return NotFound();
            }

            return View(mesas);
        }

        // POST: Mesas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Mesas == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Mesas'  is null.");
            }

            var mesas = await _context.Mesas
                .Include(m => m.Productos)
                .Include(m => m.Promocion)
                .Include(m => m.MetodoPago)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (mesas != null)
            {
                _context.Mesas.Remove(mesas);
                _context.ProductosPedidos.RemoveRange(mesas.Productos);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MesasExists(int id)
        {
          return (_context.Mesas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
