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
    public class FerreteriasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;


        public FerreteriasController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            this._env = env;
        }
       
        // GET: Ferreterias
        public async Task<IActionResult> Index()
        {
            return _context.ferreterias != null ?
                        View(await _context.ferreterias.ToListAsync()) :
                        Problem("Entity set 'AppDBcontext.ferreterias'  is null.");
        }
        [Authorize]
        // GET: Ferreterias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ferreterias == null)
            {
                return NotFound();
            }

            var ferreteria = await _context.ferreterias
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ferreteria == null)
            {
                return NotFound();
            }

            return View(ferreteria);
        }
        [Authorize]
        // GET: Ferreterias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ferreterias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,nombre,Stock,fotografia,precio")] Ferreteria ferreteria)
        {
            if (ModelState.IsValid)
            {
                ferreteria.fotografia = cargarFoto("");

                _context.Add(ferreteria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ferreteria);
        }
        [Authorize]
        // GET: Ferreterias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ferreterias == null)
            {
                return NotFound();
            }

            var ferreteria = await _context.ferreterias.FindAsync(id);
            if (ferreteria == null)
            {
                return NotFound();
            }
            return View(ferreteria);
        }

        // POST: Ferreterias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,nombre,Stock,fotografia,precio")] Ferreteria ferreteria)
        {
            if (id != ferreteria.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string nuevaFoto =
                       cargarFoto(string.IsNullOrEmpty(ferreteria.fotografia) ? "" : ferreteria.fotografia);

                    if (!string.IsNullOrEmpty(nuevaFoto))
                        ferreteria.fotografia = nuevaFoto;

                    _context.Update(ferreteria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FerreteriaExists(ferreteria.Id))
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
            return View(ferreteria);
        }
        [Authorize]
        // GET: Ferreterias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ferreterias == null)
            {
                return NotFound();
            }

            var ferreteria = await _context.ferreterias
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ferreteria == null)
            {
                return NotFound();
            }

            return View(ferreteria);
        }

        // POST: Ferreterias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ferreterias == null)
            {
                return Problem("Entity set 'AppDBcontext.ferreterias'  is null.");
            }
            var ferreteria = await _context.ferreterias.FindAsync(id);
            if (ferreteria != null)
            {
                _context.ferreterias.Remove(ferreteria);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FerreteriaExists(int id)
        {
            return (_context.ferreterias?.Any(e => e.Id == id)).GetValueOrDefault();
        }
       private string cargarFoto(string fotoAnterior)
        {
            var archivos = HttpContext.Request.Form.Files;
            if (archivos != null && archivos.Count > 0)
            {
                var archivoFoto = archivos[0];
                if (archivoFoto.Length > 0)
                {
                    var pathDestino = Path.Combine(_env.WebRootPath, "imagenes\\Ferreteria");
                    fotoAnterior = Path.Combine(pathDestino, fotoAnterior);
                    if (System.IO.File.Exists(fotoAnterior))
                        System.IO.File.Delete(fotoAnterior);

                    var archivoDestino = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(archivoFoto.FileName);

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
