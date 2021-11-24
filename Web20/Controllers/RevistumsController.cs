using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Security.Claims;
using System.Threading.Tasks;
using Web20.Interfaces;
using Web20.Models;

namespace Web20
{
    [Authorize(Roles = "Administrador, Usuario, Editor, Coordenador")]
    public class RevistumsController : Controller
    {
        private readonly AppDbContext context;
        private readonly IRevistaServicos revistaServicos;

        public RevistumsController(AppDbContext context, IRevistaServicos revistaServicos)
        {
            this.context = context;
            this.revistaServicos = revistaServicos;
        }

        [HttpPost]
        public string Index(string search, bool notUsed)
        {
            return "From [HttpGet]Index: filter on " + search;
        }

        public async Task<IActionResult> Index(string search)
        {
            return View(revistaServicos.ListarRevista(context, search));
        }

        public async Task<IActionResult> Details(int? id)
        {

            var revistum = revistaServicos.DetalharRevista(context, id);
            if (revistum == null)
                return NotFound();

            ViewData["Atualizacao"] = revistaServicos.ListarAtualizacao(context, (int)id);

            return View(revistum);
        }

        [Authorize(Roles = "Administrador, Editor, Coordenador")]
        public IActionResult Create()
        {
            ViewData["CdAquisicao"] = new SelectList(context.Aquisicaos, "Id", "TipoAquisicao");
            ViewData["CdEditor"] = new SelectList(context.Editors.OrderBy(e => e.NomeEditor), "Id", "NomeEditor");
            ViewData["CdPeriodicidade"] = new SelectList(context.Periodicidades, "Id", "TipoPeriodicidade");
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

            UniqueRevistum unica = new UniqueRevistum(context);
            if (ModelState.IsValid && unica.verificar(revistum.Titulo.ToString(), revistum.Ibict.ToString(), revistum.Issn.ToString(), revistum.Aleph.ToString()) == false)
            {

                context.Add(revistum);
                await context.SaveChangesAsync();

                Atualizacao atualizacao = new Atualizacao()
                {
                    CdRevista = revistum.Id,
                    CdUsuario = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    DtChegada = revistum.Chegada,
                    DtAtualizacao = DateTime.Today
                };

                context.Atualizacaos.Add(atualizacao);
                await context.SaveChangesAsync();

                TempData["SucessoRevista"] = "A revista " + revistum.Titulo.ToString() + " foi cadastrado com sucesso.";
                return RedirectToAction(nameof(Index));
            }
            ViewData["CdAquisicao"] = new SelectList(context.Aquisicaos, "Id", "TipoAquisicao", revistum.CdAquisicao);
            ViewData["CdEditor"] = new SelectList(context.Editors.OrderBy(r => r.NomeEditor), "Id", "NomeEditor", revistum.CdEditor);
            ViewData["CdPeriodicidade"] = new SelectList(context.Periodicidades, "Id", "TipoPeriodicidade", revistum.CdPeriodicidade);

            TempData["ErroRevista"] = "A revista " + revistum.Titulo.ToString() + " contem dados da revista " + unica.nome(revistum.Titulo.ToString(), revistum.Ibict.ToString(), revistum.Issn.ToString(), revistum.Aleph.ToString()) + ".";
            return View();
        }

        [Authorize(Roles = "Administrador, Editor, Coordenador")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var revistum = await context.Revista.FindAsync(id);
            if (revistum == null)
            {
                return NotFound();
            }
            ViewData["CdAquisicao"] = new SelectList(context.Aquisicaos, "Id", "TipoAquisicao", revistum.CdAquisicao);
            ViewData["CdEditor"] = new SelectList(context.Editors, "Id", "NomeEditor", revistum.CdEditor);
            ViewData["CdPeriodicidade"] = new SelectList(context.Periodicidades, "Id", "TipoPeriodicidade", revistum.CdPeriodicidade);
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
                    context.Update(revistum);
                    await context.SaveChangesAsync();

                    Atualizacao atualizacao = new Atualizacao()
                    {
                        CdRevista = revistum.Id,
                        CdUsuario = User.FindFirstValue(ClaimTypes.NameIdentifier),
                        DtChegada = revistum.Chegada,
                        DtAtualizacao = DateTime.Today
                    };

                    context.Atualizacaos.Add(atualizacao);
                    await context.SaveChangesAsync();

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
            ViewData["CdAquisicao"] = new SelectList(context.Aquisicaos, "Id", "TipoAquisicao", revistum.CdAquisicao);
            ViewData["CdEditor"] = new SelectList(context.Editors, "Id", "NomeEditor", revistum.CdEditor);
            ViewData["CdPeriodicidade"] = new SelectList(context.Periodicidades, "Id", "TipoPeriodicidade", revistum.CdPeriodicidade);
            return View(revistum);
        }

        public async Task<IActionResult> PDF()
        {
            return new ViewAsPdf(await context.Revista.Include(r => r.CdAquisicaoNavigation).Include(r => r.CdEditorNavigation).Include(r => r.CdPeriodicidadeNavigation).OrderBy(r => r.Titulo).ToListAsync());
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
                worksheet.Cell(linha, 8).Value = "Ultima Chegada";




                foreach (var revista
                    in context.Revista.Include(r => r.CdAquisicaoNavigation).Include(r => r.CdEditorNavigation).Include(r => r.CdPeriodicidadeNavigation).OrderBy(r => r.Titulo))
                {


                    linha++;
                    worksheet.Cell(linha, 1).Value = revista.Titulo;
                    worksheet.Cell(linha, 2).Value = revista.Aleph;
                    worksheet.Cell(linha, 3).Value = revista.Ibict;
                    worksheet.Cell(linha, 4).Value = revista.Issn;
                    worksheet.Cell(linha, 5).Value = revista.CdEditorNavigation.NomeEditor;
                    worksheet.Cell(linha, 6).Value = revista.CdAquisicaoNavigation.TipoAquisicao;
                    worksheet.Cell(linha, 7).Value = revista.CdPeriodicidadeNavigation.TipoPeriodicidade;
                    worksheet.Cell(linha, 8).Value = revista.Chegada;


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

            var revistum = await context.Revista
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
            var listaAtualizacao = context.Atualizacaos.Where(a => a.CdRevista == id).ToList();
            foreach (var lista in listaAtualizacao)
            {
                context.Atualizacaos.Remove(lista);
                await context.SaveChangesAsync();
            }
            var revistum = await context.Revista.FindAsync(id);
            context.Revista.Remove(revistum);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RevistumExists(int id)
        {
            return context.Revista.Any(e => e.Id == id);
        }
    }
}