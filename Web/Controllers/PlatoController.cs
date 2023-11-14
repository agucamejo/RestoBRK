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
    public class PlatoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PlatoController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Plato
        public async Task<IActionResult> Index()
        {
            var restoBRKContext = _context.Platos.Include(p => p.ClasificacionComida).Include(p => p.Disponibilidad).Include(p => p.Porcion).Include(p => p.TipoComida);
            return View(await restoBRKContext.ToListAsync());
        }

        // GET: Plato/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Platos == null)
            {
                return NotFound();
            }

            var plato = await _context.Platos
                .Include(p => p.ClasificacionComida)
                .Include(p => p.Disponibilidad)
                .Include(p => p.Porcion)
                .Include(p => p.TipoComida)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plato == null)
            {
                return NotFound();
            }

            return View(plato);
        }

        // GET: Plato/Create
        public IActionResult Create()
        {
            ViewData["ClasificacionComidaRefId"] = new SelectList(_context.ClasificacionComida, "Id", "Descripcion");
            ViewData["DisponibilidadRefId"] = new SelectList(_context.Disponibilidad, "Id", "Descripcion");
            ViewData["PorcionRefId"] = new SelectList(_context.Porciones, "Id", "Descripcion");
            ViewData["TipoComidaRefId"] = new SelectList(_context.TipoComida, "Id", "Descripcion");
            return View();
        }

        // POST: Plato/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PlatoViewModel model)
        {
            string uniqueFileName = UploadedFile(model);
            if (ModelState.IsValid)
            {
                Plato plato = new Plato()
                {
                    ImagemComida = uniqueFileName,
                    ClasificacionComidaRefId = model.ClasificacionComidaRefId,
                    DisponibilidadRefId = model.DisponibilidadRefId,
                    Descripcion = model.Descripcion,
                    FechaRegistro = model.FechaRegistro,
                    PorcionRefId = model.PorcionRefId,
                    TipoComidaRefId = model.TipoComidaRefId,
                    Precio = model.Precio,
                };
                _context.Add(plato);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClasificacionComidaRefId"] = new SelectList(_context.ClasificacionComida, "Id", "Descripcion", model.ClasificacionComidaRefId);
            ViewData["DisponibilidadRefId"] = new SelectList(_context.Disponibilidad, "Id", "Descripcion", model.DisponibilidadRefId);
            ViewData["PorcionRefId"] = new SelectList(_context.Porciones, "Id", "Descripcion", model.PorcionRefId);
            ViewData["TipoComidaRefId"] = new SelectList(_context.TipoComida, "Id", "Descripcion", model.TipoComidaRefId);
            return View(model);
        }

        private string UploadedFile(PlatoViewModel model)
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

        // GET: Plato/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Platos == null)
            {
                return NotFound();
            }

            var plato = await _context.Platos.FindAsync(id);
            var existingImageBytes = System.IO.File.ReadAllBytes(Path.Combine(_webHostEnvironment.WebRootPath, "images", plato.ImagemComida));
            var existingImage = new FormFile(new MemoryStream(existingImageBytes), 0, existingImageBytes.Length, "Imagem", plato.ImagemComida);

            PlatoViewModel platoViewModel = new PlatoViewModel()
            {

                ClasificacionComidaRefId = plato.ClasificacionComidaRefId,
                DisponibilidadRefId = plato.DisponibilidadRefId,
                Descripcion = plato.Descripcion,
                FechaRegistro = plato.FechaRegistro,
                PorcionRefId = plato.PorcionRefId,
                TipoComidaRefId = plato.TipoComidaRefId,
                Precio = plato.Precio,
                Imagem = existingImage,
            };

            if (plato == null)
            {
                return NotFound();
            }
            ViewData["ClasificacionComidaRefId"] = new SelectList(_context.ClasificacionComida, "Id", "Descripcion", plato.ClasificacionComidaRefId);
            ViewData["DisponibilidadRefId"] = new SelectList(_context.Disponibilidad, "Id", "Descripcion", plato.DisponibilidadRefId);
            ViewData["PorcionRefId"] = new SelectList(_context.Porciones, "Id", "Descripcion", plato.PorcionRefId);
            ViewData["TipoComidaRefId"] = new SelectList(_context.TipoComida, "Id", "Descripcion", plato.TipoComidaRefId);
            return View(platoViewModel);
        }

        // POST: Plato/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PlatoViewModel model)
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
                    var plato = await _context.Platos.FindAsync(id);

                    plato.ImagemComida = uniqueFileName;
                    plato.ClasificacionComidaRefId = model.ClasificacionComidaRefId;
                    plato.DisponibilidadRefId = model.DisponibilidadRefId;
                    plato.Descripcion = model.Descripcion;
                    plato.FechaRegistro = model.FechaRegistro;
                    plato.PorcionRefId = model.PorcionRefId;
                    plato.TipoComidaRefId = model.TipoComidaRefId;
                    plato.Precio = model.Precio;

                    _context.Update(plato);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlatoExists(model.Id))
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
            ViewData["ClasificacionComidaRefId"] = new SelectList(_context.ClasificacionComida, "Id", "Descripcion", model.ClasificacionComidaRefId);
            ViewData["DisponibilidadRefId"] = new SelectList(_context.Disponibilidad, "Id", "Descripcion", model.DisponibilidadRefId);
            ViewData["PorcionRefId"] = new SelectList(_context.Porciones, "Id", "Descripcion", model.PorcionRefId);
            ViewData["TipoComidaRefId"] = new SelectList(_context.TipoComida, "Id", "Descripcion", model.TipoComidaRefId);
            return View(model);
        }

        // GET: Plato/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Platos == null)
            {
                return NotFound();
            }

            var plato = await _context.Platos
                .Include(p => p.ClasificacionComida)
                .Include(p => p.Disponibilidad)
                .Include(p => p.Porcion)
                .Include(p => p.TipoComida)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plato == null)
            {
                return NotFound();
            }

            return View(plato);
        }

        // POST: Plato/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Platos == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Platos'  is null.");
            }
            var plato = await _context.Platos.FindAsync(id);
            if (plato != null)
            {
                _context.Platos.Remove(plato);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlatoExists(int id)
        {
          return (_context.Platos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
