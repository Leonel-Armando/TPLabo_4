using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TPLabo_4.Data;
using TPLabo_4.Models;

namespace TPLabo_4.Controllers
{
    public class CalidadesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CalidadesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Calidades
        public async Task<IActionResult> Index(int page = 1, int pageSize = 3)
        {
            return _context.calidades != null ?
                        View(await _context.calidades.ToListAsync()) :
                        Problem("Entity set 'AppDBcontext.calidades'  is null.");
        }
        [Authorize]
        // GET: Calidades/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.calidades == null)
            {
                return NotFound();
            }

            var calidad = await _context.calidades
                .FirstOrDefaultAsync(m => m.Id == id);
            if (calidad == null)
            {
                return NotFound();
            }

            return View(calidad);
        }
        [Authorize]
        // GET: Calidades/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Calidades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TipoMadera,Artesanal")] Calidad calidad)
        {
            if (ModelState.IsValid)
            {
                _context.Add(calidad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(calidad);
        }
        [Authorize]
        // GET: Calidades/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.calidades == null)
            {
                return NotFound();
            }

            var calidad = await _context.calidades.FindAsync(id);
            if (calidad == null)
            {
                return NotFound();
            }
            return View(calidad);
        }

        // POST: Calidades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TipoMadera,Artesanal")] Calidad calidad)
        {
            if (id != calidad.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(calidad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CalidadExists(calidad.Id))
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
            return View(calidad);
        }
        [Authorize]

        // GET: Calidades/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.calidades == null)
            {
                return NotFound();
            }

            var calidad = await _context.calidades
                .FirstOrDefaultAsync(m => m.Id == id);
            if (calidad == null)
            {
                return NotFound();
            }

            return View(calidad);
        }

        // POST: Calidades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.calidades == null)
            {
                return Problem("Entity set 'AppDBcontext.calidades'  is null.");
            }
            var calidad = await _context.calidades.FindAsync(id);
            if (calidad != null)
            {
                _context.calidades.Remove(calidad);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CalidadExists(int id)
        {
            return (_context.calidades?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
