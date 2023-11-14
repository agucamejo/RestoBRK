using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCore.BulkExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Web.Data;
using Web.Migrations;
using Web.Models;

namespace Web.Controllers
{
    public class ListaPreciosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ListaPreciosController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: ListaPrecios
        public async Task<IActionResult> Index(string sortOrder, string search)
        {
            ViewData["PrecioSortParm"] = String.IsNullOrEmpty(sortOrder) ? "precio_desc" : "";

            var listaPrecios = await _context.ListaPrecios.ToListAsync();

            if (!string.IsNullOrEmpty(search))
            {
                search = search.Trim();

                listaPrecios = listaPrecios
                    .Where(lp => lp.Producto.Contains(search, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            switch (sortOrder)
            {
                case "precio_desc":
                    listaPrecios = listaPrecios.OrderByDescending(lp => lp.Precio).ToList();
                    break;
                default:
                    listaPrecios = listaPrecios.OrderBy(lp => lp.Precio).ToList();
                    break;
            }

            return View(listaPrecios);
        }




        // GET: ListaPrecios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ListaPrecios == null)
            {
                return NotFound();
            }

            var listaPrecios = await _context.ListaPrecios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (listaPrecios == null)
            {
                return NotFound();
            }

            return View(listaPrecios);
        }

        // GET: ListaPrecios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ListaPrecios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Producto,Precio,FechaRegistro")] ListaPrecios listaPrecios)
        {
            if (ModelState.IsValid)
            {
                _context.Add(listaPrecios);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(listaPrecios);
        }

        // GET: ListaPrecios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ListaPrecios == null)
            {
                return NotFound();
            }

            var listaPrecios = await _context.ListaPrecios.FindAsync(id);
            if (listaPrecios == null)
            {
                return NotFound();
            }
            return View(listaPrecios);
        }

        // POST: ListaPrecios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Producto,Precio,FechaRegistro")] ListaPrecio listaPrecios)
        {
            if (id != listaPrecios.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(listaPrecios);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ListaPreciosExists(listaPrecios.Id))
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
            return View(listaPrecios);
        }

        // GET: ListaPrecios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ListaPrecios == null)
            {
                return NotFound();
            }

            var listaPrecios = await _context.ListaPrecios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (listaPrecios == null)
            {
                return NotFound();
            }

            return View(listaPrecios);
        }

        // POST: ListaPrecios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ListaPrecios == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ListaPrecios'  is null.");
            }
            var listaPrecios = await _context.ListaPrecios.FindAsync(id);
            if (listaPrecios != null)
            {
                _context.ListaPrecios.Remove(listaPrecios);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ListaPreciosExists(int id)
        {
          return (_context.ListaPrecios?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public IActionResult ImportarListaPrecios()
        {
            return View();
        }

        [HttpPost, ActionName("MostrarDatos")]
        public IActionResult MostrarDatos([FromForm] IFormFile ArchivoExcel)
        {
            if (ArchivoExcel != null)
            {
                Stream stream = ArchivoExcel.OpenReadStream();

                IWorkbook MiExcel = null;

                if (Path.GetExtension(ArchivoExcel.FileName) == ".xlsx")
                {
                    MiExcel = new XSSFWorkbook(stream);
                }
                else
                {
                    MiExcel = new HSSFWorkbook(stream);
                }
                ISheet HojaExcel = MiExcel.GetSheetAt(0);
                int cantidadFilas = HojaExcel.LastRowNum;

                List<ListaPrecio> lista = new List<ListaPrecio>();

                for (int i = 1; i <= cantidadFilas; i++)
                {

                    IRow fila = HojaExcel.GetRow(i);

                    lista.Add(new ListaPrecio
                    {
                        Producto = fila.GetCell(0).ToString(),
                        Precio = Decimal.Parse(fila.GetCell(1).ToString()),
                        FechaRegistro = DateTime.Now

                    });
                }

                return StatusCode(StatusCodes.Status200OK, lista);
            }
            else
            {
                return View();
            }

        }

        [HttpPost, ActionName("EnviarDatos")]
        public IActionResult EnviarDatos([FromForm] IFormFile ArchivoExcel)
        {
            if (ArchivoExcel != null)
            {
                Stream stream = ArchivoExcel.OpenReadStream();

                IWorkbook MiExcel = null;

                if (Path.GetExtension(ArchivoExcel.FileName) == ".xlsx")
                {
                    MiExcel = new XSSFWorkbook(stream);
                }
                else
                {
                    MiExcel = new HSSFWorkbook(stream);
                }
                ISheet HojaExcel = MiExcel.GetSheetAt(0);

                int cantidadFilas = HojaExcel.LastRowNum;
                List<ListaPrecio> lista = new List<ListaPrecio>();

                for (int i = 1; i <= cantidadFilas; i++)
                {
                    IRow fila = HojaExcel.GetRow(i);

                    lista.Add(new ListaPrecio
                    {
                        Producto = fila.GetCell(0).ToString(),
                        Precio = Decimal.Parse(fila.GetCell(1).ToString()),
                        FechaRegistro = DateTime.Now
                    });
                }

                _context.BulkInsert(lista);

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            else
            {
                return View();
            }

        }

        public IActionResult DownloadFile()
        {
            var filepath = Path.Combine(_webHostEnvironment.WebRootPath, "archivos", "ListaDePrecios.xlsx");
            return File(System.IO.File.ReadAllBytes(filepath), "application/vnd.ms-excel", Path.GetFileName(filepath));
        }

    }
}
