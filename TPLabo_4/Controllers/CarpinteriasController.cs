using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using TPLabo_4.Data;
using TPLabo_4.Models;

namespace TPLabo_4.Controllers
{
    public class CarpinteriasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public CarpinteriasController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ImportExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                ModelState.AddModelError("file", "Please select a file");
                return View("Index");
            }

            using (var package = new ExcelPackage(file.OpenReadStream()))
            {
                var worksheet = package.Workbook.Worksheets[0];
                var rowCount = worksheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++)
                {
                    var carpinteria = new Carpinteria
                    {
                        nombre = worksheet.Cells[row, 1].Value.ToString(),
                        Stock = bool.Parse(worksheet.Cells[row, 2].Value.ToString()),
                        fotografia = worksheet.Cells[row, 3].Value.ToString(),
                        precio = int.Parse(worksheet.Cells[row, 4].Value.ToString()),
                        descripcion = worksheet.Cells[row, 5].Value.ToString(),
                        IdCalidad = int.Parse(worksheet.Cells[row, 6].Value.ToString())
                    };

                    _context.Add(carpinteria);
                }

                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Carpinterias
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.carpinterias.Include(a => a.Calidad);
            return View(await applicationDbContext.ToListAsync());
        }
        [Authorize]
        // GET: Carpinterias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.carpinterias == null)
            {
                return NotFound();
            }

            var carpinteria = await _context.carpinterias
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carpinteria == null)
            {
                return NotFound();
            }

            return View(carpinteria);
        }
        [Authorize]
        // GET: Carpinterias/Create
        public IActionResult Create()
        {
            ViewData["IdCalidad"] = new SelectList(_context.calidades, "Id", "TipoMadera");
            return View();
        }

        // POST: Carpinterias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,nombre,Stock,fotografia,precio,descripcion,IdCalidad")] Carpinteria carpinteria)
        {
            if (ModelState.IsValid)
            {
                carpinteria.fotografia = cargarFoto("");
                _context.Add(carpinteria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCalidad"] = new SelectList(_context.calidades, "Id", "Id", carpinteria.IdCalidad);
            return View(carpinteria);
        }
        [Authorize]
        // GET: Carpinterias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.carpinterias == null)
            {
                return NotFound();
            }

            var carpinteria = await _context.carpinterias.FindAsync(id);
            if (carpinteria == null)
            {
                return NotFound();
            }
            ViewData["IdCalidad"] = new SelectList(_context.calidades, "id", "TipoMadera", carpinteria.IdCalidad);
            return View(carpinteria);
        }

        // POST: Carpinterias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,nombre,Stock,fotografia,precio,descripcion,IdCalidad")] Carpinteria carpinteria)
        {
            if (id != carpinteria.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string nuevaFoto = cargarFoto(string.IsNullOrEmpty(carpinteria.fotografia) ? "" : carpinteria.fotografia);

                    if (!string.IsNullOrEmpty(nuevaFoto))
                        carpinteria.fotografia = nuevaFoto;
                    _context.Update(carpinteria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarpinteriaExists(carpinteria.Id))
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
            ViewData["IdCalidad"] = new SelectList(_context.calidades, "id", "id", carpinteria.IdCalidad);
            return View(carpinteria);
        }
        [Authorize]
        // GET: Carpinterias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.carpinterias == null)
            {
                return NotFound();
            }

            var carpinteria = await _context.carpinterias
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carpinteria == null)
            {
                return NotFound();
            }

            return View(carpinteria);
        }

        // POST: Carpinterias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.carpinterias == null)
            {
                return Problem("Entity set 'AppDBcontext.carpinterias'  is null.");
            }
            var carpinteria = await _context.carpinterias.FindAsync(id);
            if (carpinteria != null)
            {
                _context.carpinterias.Remove(carpinteria);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarpinteriaExists(int id)
        {
            return (_context.carpinterias?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        private string cargarFoto(string fotoAnterior)
        {
            var archivos = HttpContext.Request.Form.Files;
            if (archivos != null && archivos.Count > 0)
            {
                var archivoFoto = archivos[0];
                if (archivoFoto.Length > 0)
                {
                    var pathDestino = Path.Combine(_env.WebRootPath, "imagenes\\Carpinteria");

                    fotoAnterior = Path.Combine(pathDestino, fotoAnterior);
                    if (System.IO.File.Exists(fotoAnterior))
                        System.IO.File.Delete(fotoAnterior);

                    var archivoDestino = Guid.NewGuid().ToString().Replace("-", "");
                    archivoDestino += Path.GetExtension(archivoFoto.FileName);

                    using (var filestream = new FileStream(Path.Combine(pathDestino, archivoDestino), FileMode.Create))
                    {
                        archivoFoto.CopyTo(filestream);
                        return archivoDestino;
                    };
                }
            }
            return "";
        }
    }
}
