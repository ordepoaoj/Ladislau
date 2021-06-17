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

namespace Web20.Controllers
{
    [Authorize]
    public class EditorsController : Controller
    {
        private readonly AppDbContext _context;

        public EditorsController(AppDbContext context)
        {
            _context = context;
        }
      
        [HttpPost]
        
        public string Index(string search, bool notUsed)
        {
            return "From [HttpPost]Index: filter on " + search;
        }

        // GET: Editors
        public async Task<IActionResult> Index(string search)
        {
            var appDbContext = _context.Editors.Include(e => e.CodPaisNavigation).OrderBy(e => e.NomeEditor);
            

            var Editor = from e in _context.Editors
                         select e;
            if(!String.IsNullOrEmpty(search))
            {
                Editor = Editor.Where(s => s.NomeEditor.Contains(search)).Include(e => e.CodPaisNavigation).OrderBy(e => e.NomeEditor);
                
                return View(await Editor.ToListAsync());
            }
            return View(await appDbContext.ToListAsync());
        }

        // GET: Editors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var editor = await _context.Editors
                .Include(e => e.Revista)
                .FirstOrDefaultAsync(m => m.Id == id);

            var Revista = from r in _context.Revista
                          select r;

            ViewData["Revista"] = Revista.Where(r => r.CdEditor.Equals(id));

            if (editor == null)
            {
                return NotFound();
            }

            return View(editor);
        }

        public async Task<IActionResult> Pendente(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var Revista = from e in _context.Revista
                          select e;
            int valor = Revista.Where(s => s.CdEditor.Equals(id)).Count();
            var editor = await _context.Editors
                .Include(e => e.CodPaisNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (editor == null)
            {
                return NotFound();
            }

            return View(editor);
        }

        // GET: Editors/Create
        public IActionResult Create()
        {
            ViewData["CodPais"] = new SelectList(_context.PaisEditors.OrderBy(x => x.NomePais), "Id", "NomePais");
            return View();
        }

        // POST: Editors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NomeEditor,Endereco,CodPais,CodPostal,Email,Telefone")] Editor editor)
        {
            UniqueEditor editorRepetido = new UniqueEditor(_context);
            if (ModelState.IsValid && editorRepetido.verificar(editor.NomeEditor.ToString()) == true)
            {
                _context.Add(editor);
                await _context.SaveChangesAsync();
                TempData["Sucesso"] = "O Editor " + editor.NomeEditor.ToString() + " foi cadastrado com sucesso.";
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodPais"] = new SelectList(_context.PaisEditors, "Id", "NomePais", editor.CodPais);
            TempData["ErroEditor"] = "O Editor " + editor.NomeEditor.ToString() + " já foi cadastrado antes.";
            return View(editor);
        }

        // GET: Editors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            
            if (id == null)
            {
                return NotFound();
            }

            var editor = await _context.Editors.FindAsync(id);
            ViewData["CodPais"] = new SelectList(_context.PaisEditors, "Id", "NomePais", editor.CodPais);
            if (editor == null)
            {
                return NotFound();
            }
            
            return View(editor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NomeEditor,Endereco,CodPostal,CodPais,Email,Telefone")] Editor editor)
        {
            UniqueEditor editorCadastrador = new UniqueEditor(_context);
            if (id != editor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    _context.Update(editor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EditorExists(editor.Id))
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
            ViewData["CodPais"] = new SelectList(_context.PaisEditors, "Id", "Id", editor.CodPais);
            return View(editor);
        }

        // GET: Editors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            
            var Revista = from e in _context.Revista
                          select e;
            int valor = Revista.Where(s => s.CdEditor.Equals(id)).Count();
            if (valor == 0)
            {
                 var editor = await _context.Editors
                .Include(e => e.CodPaisNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            return View(editor);
            }
            return RedirectToAction(nameof(Pendente), new {id = id });

        }

        public async Task<IActionResult> PDF()
        {
            var appDbContext = _context.Editors.Include(e => e.CodPaisNavigation).OrderBy(e => e.NomeEditor);
            return new ViewAsPdf(await appDbContext.ToListAsync());
        }

        public async Task<IActionResult> Excel ()
        {
            var appDbContext = _context.Editors.Include(e => e.CodPaisNavigation).OrderBy(e => e.NomeEditor);
            

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Editor");
                var linha = 1;

                worksheet.Cell(linha, 1).Value = "Nome do Editor";
                worksheet.Cell(linha, 2).Value = "Endereço";
                worksheet.Cell(linha, 3).Value = "País / Região";

                foreach (var editor in appDbContext)
                {
                    linha++;
                    worksheet.Cell(linha, 1).Value = editor.NomeEditor;
                    worksheet.Cell(linha, 2).Value = editor.Endereco;
                    worksheet.Cell(linha, 3).Value = editor.CodPaisNavigation.NomePais;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Editor.xlsx");
                }

            }

        }

        // POST: Editors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var editor = await _context.Editors.FindAsync(id);
            _context.Editors.Remove(editor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EditorExists(int id)
        {
            return _context.Editors.Any(e => e.Id == id);
        }

    }
}
