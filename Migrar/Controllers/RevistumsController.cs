using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;
using Web20.Models;

namespace Web20
{
    [Authorize(Roles = "Administrador, Usuario, Editor, Coordenador")]
    public class RevistumsController : Controller
    {
        private readonly AppDbContext _context;

        public RevistumsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public string Index(string search, bool notUsed)
        {
            return "From [HttpGet]Index: filter on " + search;
        }

        public async Task<IActionResult> Index(string search)
        {
#warning Este método contem uma parte de código não elegante. A fim de evitar a consulta e resposta de todos os resultados de maneira desnecessária -- Não esquecer de buscar uma solução mais elegante para o problema
            int block = 0; //Variável a fim de impedir a consulta desnecessária de todos os itens do CRUD -- Favor tratar esse código maneira mais elegante
            var appDbContext = 
                _context.Revista.Where(r => r.Id.Equals(block)).Include(r => r.CdAquisicaoNavigation).Include(r => r.CdEditorNavigation).Include(r => r.CdPeriodicidadeNavigation).OrderBy(r => r.Titulo);
            

            var Revista = from r in _context.Revista
                        select r;
            if (!String.IsNullOrEmpty(search))
            {
                Revista =
                    Revista.Where(r => r.Titulo.Contains(search) || r.Aleph.Equals(search) || r.Issn.Contains(search)).Where(r => r.Ativo.Equals(true)).Include(r => r.CdEditorNavigation).Include(r => r.CdPeriodicidadeNavigation).OrderBy(r => r.Titulo);
                return View(await Revista.ToListAsync());
            }
            return View(await appDbContext.ToListAsync());
        }

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

        [Authorize(Roles = "Administrador, Editor, Coordenador")]
        public IActionResult Create()
        {
            ViewData["CdAquisicao"] = new SelectList(_context.Aquisicaos, "Id", "TipoAquisicao");
            ViewData["CdEditor"] = new SelectList(_context.Editors.OrderBy(e => e.NomeEditor), "Id", "NomeEditor");
            ViewData["CdPeriodicidade"] = new SelectList(_context.Periodicidades, "Id", "TipoPeriodicidade");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Editor, Coordenador")]
        public async Task<IActionResult> Create([Bind("Id,Aleph,Titulo,Ibict,Issn,Ativo,Chegada,CdAquisicao,CdEditor,CdPeriodicidade")] Revistum revistum)
        {
            if (revistum.CdPeriodicidade == null || revistum.CdEditor == null || revistum.CdAquisicao == null)
            {
                TempData["ErroRevista"] = "A revista está com dados incompletos.";
                return View();
            }

            UniqueRevistum unica = new UniqueRevistum(_context);
            if (ModelState.IsValid && unica.verificar(revistum.Titulo.ToString(), revistum.Ibict.ToString(), revistum.Issn.ToString(), revistum.Aleph.ToString()) == false)
            {
                _context.Add(revistum);
                await _context.SaveChangesAsync();
                TempData["SucessoRevista"] = "A revista " + revistum.Titulo.ToString() + " foi cadastrado com sucesso.";
                return RedirectToAction(nameof(Index));
            }
            ViewData["CdAquisicao"] = new SelectList(_context.Aquisicaos, "Id", "TipoAquisicao", revistum.CdAquisicao);
            ViewData["CdEditor"] = new SelectList(_context.Editors.OrderBy(r => r.NomeEditor), "Id", "NomeEditor", revistum.CdEditor);
            ViewData["CdPeriodicidade"] = new SelectList(_context.Periodicidades, "Id", "TipoPeriodicidade", revistum.CdPeriodicidade);

            TempData["ErroRevista"] = "A revista " + revistum.Titulo.ToString() +  " contem dados da revista " + unica.nome(revistum.Titulo.ToString(), revistum.Ibict.ToString(), revistum.Issn.ToString(), revistum.Aleph.ToString()) + "." ;
            return View();
        }

        [Authorize(Roles = "Administrador, Editor, Coordenador")]
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Editor, Coordenador")]
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

        public async Task<IActionResult> PDF()
        {
            return new ViewAsPdf(await _context.Revista.Include(r => r.CdAquisicaoNavigation).Include(r => r.CdEditorNavigation).Include(r => r.CdPeriodicidadeNavigation).OrderBy(r => r.Titulo).ToListAsync());
        }

        public async Task<IActionResult> Excel()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Revista");
                var linha = 1;

                worksheet.Cell(linha, 1).Value = "Revista";
                worksheet.Cell(linha, 2).Value = "Aleph";
                worksheet.Cell(linha, 3).Value = "IBICT";
                worksheet.Cell(linha, 4).Value = "ISSN";
                worksheet.Cell(linha, 5).Value = "Editor";
                worksheet.Cell(linha, 6).Value = "Aquisição";
                worksheet.Cell(linha, 7).Value = "Periodicidade";

                foreach (var revista
                    in _context.Revista.Include(r => r.CdAquisicaoNavigation).Include(r => r.CdEditorNavigation).Include(r => r.CdPeriodicidadeNavigation).OrderBy(r => r.Titulo))
                {
                    linha++;
                    worksheet.Cell(linha, 1).Value = revista.Titulo;
                    worksheet.Cell(linha, 2).Value = revista.Aleph;
                    worksheet.Cell(linha, 3).Value = revista.Ibict;
                    worksheet.Cell(linha, 4).Value = revista.Issn;
                    worksheet.Cell(linha, 5).Value = revista.CdEditorNavigation.NomeEditor;
                    worksheet.Cell(linha, 6).Value = revista.CdAquisicaoNavigation.TipoAquisicao;
                    worksheet.Cell(linha, 7).Value = revista.CdPeriodicidadeNavigation.TipoPeriodicidade;

                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Revista.xlsx");
                }

            }

        }

        // GET: Revistums/Delete/5
        [Authorize(Roles = "Administrador, Coordenador")]
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
        [Authorize(Roles = "Administrador, Coordenador")]
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