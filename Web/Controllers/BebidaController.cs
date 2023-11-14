using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Models;
using Web.ViewModels;

namespace Web.Controllers
{
    public class BebidaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BebidaController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Bebida
        public async Task<IActionResult> Index()
        {
            var restoBRKContext = _context.Bebidas.Include(p => p.ClasificacionBebida).Include(p => p.Tamano).Include(p => p.TipoBebida);
            return View(await restoBRKContext.ToListAsync());
        }

        // GET: Bebida/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Bebidas == null)
            {
                return NotFound();
            }

            var bebida = await _context.Bebidas
                .Include(b => b.ClasificacionBebida)
                .Include(b => b.Tamano)
                .Include(b => b.TipoBebida)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bebida == null)
            {
                return NotFound();
            }

            return View(bebida);
        }

        // GET: Bebida/Create
        public IActionResult Create()
        {
            ViewData["ClasificacionBebidaRefId"] = new SelectList(_context.ClasificacionBebida, "Id", "Descripcion");
            ViewData["TamanoRefId"] = new SelectList(_context.Tamano, "Id", "Descripcion");
            ViewData["TipoBebidaRefId"] = new SelectList(_context.TipoBebida, "Id", "Descripcion");
            return View();
        }

        // POST: Bebida/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BebidaViewModel model)
        {
            string uniqueFileName = UploadedFile(model);
            if (ModelState.IsValid)
            {
                Bebida bebida = new Bebida()
                {
                    ImagemBebida = uniqueFileName,
                    ClasificacionBebidaRefId = model.ClasificacionBebidaRefId,
                    Descripcion = model.Descripcion,
                    FechaRegistro = model.FechaRegistro,
                    TamanoRefId = model.TamanoRefId,
                    TipoBebidaRefId = model.TipoBebidaRefId,
                    Precio = model.Precio,
                };
                _context.Add(bebida);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClasificacionBebidaRefId"] = new SelectList(_context.ClasificacionBebida, "Id", "Descripcion", model.ClasificacionBebidaRefId);
            ViewData["TamanoRefId"] = new SelectList(_context.Tamano, "Id", "Descripcion", model.TamanoRefId);
            ViewData["TipoBebidaRefId"] = new SelectList(_context.TipoBebida, "Id", "Descripcion", model.TipoBebidaRefId);
            return View(model);
        }

        private string UploadedFile(BebidaViewModel model)
        {
            string uniqueFileName = null;

            if (model.Imagem != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Imagem.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Imagem.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        // GET: Bebida/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Bebidas == null)
            {
                return NotFound();
            }

            var bebida = await _context.Bebidas.FindAsync(id);
            var existingImageBytes = System.IO.File.ReadAllBytes(Path.Combine(_webHostEnvironment.WebRootPath, "images", bebida.ImagemBebida));
            var existingImage = new FormFile(new MemoryStream(existingImageBytes), 0, existingImageBytes.Length, "Imagem", bebida.ImagemBebida);

            BebidaViewModel bebidaViewModel = new BebidaViewModel()
            {

                ClasificacionBebidaRefId = bebida.ClasificacionBebidaRefId,
                Descripcion = bebida.Descripcion,
                FechaRegistro = bebida.FechaRegistro,
                TamanoRefId = bebida.TamanoRefId,
                TipoBebidaRefId = bebida.TipoBebidaRefId,
                Precio = bebida.Precio,
                Imagem = existingImage,
            };

            if (bebida == null)
            {
                return NotFound();
            }
            ViewData["ClasificacionBebidaRefId"] = new SelectList(_context.ClasificacionBebida, "Id", "Descripcion", bebida.ClasificacionBebidaRefId);
            ViewData["TamanoRefId"] = new SelectList(_context.Tamano, "Id", "Descripcion", bebida.TamanoRefId);
            ViewData["TipoBebidaRefId"] = new SelectList(_context.TipoBebida, "Id", "Descripcion", bebida.TipoBebidaRefId);
            return View(bebidaViewModel);
        }

        // POST: Bebida/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BebidaViewModel model)
        {
            string uniqueFileName = UploadedFile(model);
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var bebida = await _context.Bebidas.FindAsync(id);

                    bebida.ImagemBebida = uniqueFileName;
                    bebida.ClasificacionBebidaRefId = model.ClasificacionBebidaRefId;
                    bebida.Descripcion= model.Descripcion;
                    bebida.FechaRegistro = model.FechaRegistro;
                    bebida.TamanoRefId = model.TamanoRefId;
                    bebida.TipoBebidaRefId = model.TipoBebidaRefId;
                    bebida.Precio = model.Precio;

                    _context.Update(bebida);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BebidaExists(model.Id))
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
            ViewData["ClasificacionBebidaRefId"] = new SelectList(_context.ClasificacionBebida, "Id", "Descripcion", model.ClasificacionBebidaRefId);
            ViewData["TamanoRefId"] = new SelectList(_context.Tamano, "Id", "Descripcion", model.TamanoRefId);
            ViewData["TipoBebidaRefId"] = new SelectList(_context.TipoBebida, "Id", "Descripcion", model.TipoBebidaRefId);
            return View(model);
        }

        // GET: Bebida/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Bebidas == null)
            {
                return NotFound();
            }

            var bebida = await _context.Bebidas
                .Include(b => b.ClasificacionBebida)
                .Include(b => b.Tamano)
                .Include(b => b.TipoBebida)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bebida == null)
            {
                return NotFound();
            }

            return View(bebida);
        }

        // POST: Bebida/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Bebidas == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Bebidas'  is null.");
            }
            var bebida = await _context.Bebidas.FindAsync(id);
            if (bebida != null)
            {
                _context.Bebidas.Remove(bebida);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BebidaExists(int id)
        {
          return (_context.Bebidas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
