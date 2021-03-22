using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web20.Models;

namespace Web20.Controllers
{
    public class PublicacoesController : Controller
    {
        private readonly AppDbContext _context;

        public PublicacoesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Publicacoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.RevistaMuseus.ToListAsync());
        }

        // GET: Publicacoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var revistaMuseu = await _context.RevistaMuseus
                .FirstOrDefaultAsync(m => m.Id == id);
            if (revistaMuseu == null)
            {
                return NotFound();
            }

            return View(revistaMuseu);
        }

        // GET: Publicacoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Publicacoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Aleph,Ibict,Issn,Titulo,CdPeriodicidade")] RevistaMuseu revistaMuseu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(revistaMuseu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(revistaMuseu);
        }

        // GET: Publicacoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var revistaMuseu = await _context.RevistaMuseus.FindAsync(id);
            if (revistaMuseu == null)
            {
                return NotFound();
            }
            return View(revistaMuseu);
        }

        // POST: Publicacoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Aleph,Ibict,Issn,Titulo,CdPeriodicidade")] RevistaMuseu revistaMuseu)
        {
            if (id != revistaMuseu.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(revistaMuseu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RevistaMuseuExists(revistaMuseu.Id))
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
            return View(revistaMuseu);
        }

        // GET: Publicacoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var revistaMuseu = await _context.RevistaMuseus
                .FirstOrDefaultAsync(m => m.Id == id);
            if (revistaMuseu == null)
            {
                return NotFound();
            }

            return View(revistaMuseu);
        }

        // POST: Publicacoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var revistaMuseu = await _context.RevistaMuseus.FindAsync(id);
            _context.RevistaMuseus.Remove(revistaMuseu);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RevistaMuseuExists(int id)
        {
            return _context.RevistaMuseus.Any(e => e.Id == id);
        }
    }
}
