using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web20.Models;

namespace Web20
{
    public class RevistumsController : Controller
    {
        private readonly AppDbContext _context;

        public RevistumsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Revistums
        public async Task<IActionResult> Index(string searchstring)
        {
#warning Este método contem uma parte de código não elegante. A fim de evitar a consulta e resposta de todos os resultados de maneira desnecessária -- Não esquecer de buscar uma solução mais elegante para o problema
            int block = 0; //Variável a fim de impedir a consulta desnecessária de todos os itens do CRUD -- Favor tratar esse código maneira mais elegante
            var appDbContext = 
                _context.Revista.Where(r => r.Id.Equals(block)).Include(r => r.CdAquisicaoNavigation).Include(r => r.CdEditorNavigation).Include(r => r.CdPeriodicidadeNavigation).OrderBy(r => r.Titulo);
            

            var Revista = from r in _context.Revista
                        select r;
            if (!String.IsNullOrEmpty(searchstring))
            {
                Revista =
                    Revista.Where(r => r.Titulo.Contains(searchstring) || r.Aleph.Equals(searchstring)).Include(r => r.CdEditorNavigation).Include(r => r.CdPeriodicidadeNavigation).OrderBy(r => r.Titulo);
                return View(await Revista.ToListAsync());
            }
            return View(await appDbContext.ToListAsync());
        }

        // GET: Revistums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var revistum = await _context.Revista
                .Include(r => r.CdAquisicaoNavigation)
                .Include(r => r.CdEditorNavigation)
                .Include(r => r.CdPeriodicidadeNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (revistum == null)
            {
                return NotFound();
            }

            return View(revistum);
        }

        // GET: Revistums/Create
        public IActionResult Create()
        {
            ViewData["CdAquisicao"] = new SelectList(_context.Aquisicaos, "Id", "TipoAquisicao");
            ViewData["CdEditor"] = new SelectList(_context.Editors.OrderBy(e => e.NomeEditor), "Id", "NomeEditor");
            ViewData["CdPeriodicidade"] = new SelectList(_context.Periodicidades, "Id", "TipoPeriodicidade");
            return View();
        }

        // POST: Revistums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Aleph,Titulo,Ibict,Issn,Ativo,Chegada,CdAquisicao,CdEditor,CdPeriodicidade")] Revistum revistum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(revistum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CdAquisicao"] = new SelectList(_context.Aquisicaos, "Id", "TipoAquisicao", revistum.CdAquisicao);
            ViewData["CdEditor"] = new SelectList(_context.Editors.OrderBy(r => r.NomeEditor), "Id", "NomeEditor", revistum.CdEditor);
            ViewData["CdPeriodicidade"] = new SelectList(_context.Periodicidades, "Id", "TipoPeriodicidade", revistum.CdPeriodicidade);
            return View(revistum);
        }

        // GET: Revistums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var revistum = await _context.Revista.FindAsync(id);
            if (revistum == null)
            {
                return NotFound();
            }
            ViewData["CdAquisicao"] = new SelectList(_context.Aquisicaos, "Id", "TipoAquisicao", revistum.CdAquisicao);
            ViewData["CdEditor"] = new SelectList(_context.Editors, "Id", "NomeEditor", revistum.CdEditor);
            ViewData["CdPeriodicidade"] = new SelectList(_context.Periodicidades, "Id", "TipoPeriodicidade", revistum.CdPeriodicidade);
            return View(revistum);
        }

        // POST: Revistums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Aleph,Titulo,Ibict,Issn,Ativo,Chegada,CdAquisicao,CdEditor,CdPeriodicidade")] Revistum revistum)
        {
            if (id != revistum.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(revistum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RevistumExists(revistum.Id))
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
            ViewData["CdAquisicao"] = new SelectList(_context.Aquisicaos, "Id", "TipoAquisicao", revistum.CdAquisicao);
            ViewData["CdEditor"] = new SelectList(_context.Editors, "Id", "NomeEditor", revistum.CdEditor);
            ViewData["CdPeriodicidade"] = new SelectList(_context.Periodicidades, "Id", "TipoPeriodicidade", revistum.CdPeriodicidade);
            return View(revistum);
        }

        // GET: Revistums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var revistum = await _context.Revista
                .Include(r => r.CdAquisicaoNavigation)
                .Include(r => r.CdEditorNavigation)
                .Include(r => r.CdPeriodicidadeNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (revistum == null)
            {
                return NotFound();
            }

            return View(revistum);
        }

        // POST: Revistums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var revistum = await _context.Revista.FindAsync(id);
            _context.Revista.Remove(revistum);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RevistumExists(int id)
        {
            return _context.Revista.Any(e => e.Id == id);
        }
    }
}
