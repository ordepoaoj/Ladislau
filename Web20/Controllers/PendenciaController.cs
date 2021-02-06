using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;
using Web20.Models;

namespace Web20.Controllers
{
    [Authorize]
    public class PendenciaController : Controller
    {
        private readonly AppDbContext _context;

        public PendenciaController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Pendencia
        public async Task<IActionResult> Index()
        {
            DbFunctions db = null;
            int mensal = 1;
            int anual = 2;
            int trimestral = 3;
            int bimestral = 4;
            int quadrimestral = 5;
            int semestral = 8;
            DateTime hoje = DateTime.Today;

            var appDbContext = _context.Revista.Where(r => SqlServerDbFunctionsExtensions.DateDiffDay(db, r.Chegada, hoje) >= 60 && r.CdPeriodicidade == mensal)
                .Include(r => r.CdAquisicaoNavigation)
                    .Include(r => r.CdEditorNavigation)
                        .Include(r => r.CdPeriodicidadeNavigation)
                            .OrderBy(r => r.CdPeriodicidade).OrderBy(r => r.Titulo);

            ViewData["Bimestral"] = _context.Revista.Where(r => SqlServerDbFunctionsExtensions.DateDiffDay(db, r.Chegada, hoje) >= 90 && r.CdPeriodicidade == bimestral)
                .Include(r => r.CdEditorNavigation)
                    .Include(r => r.CdPeriodicidadeNavigation)
                        .OrderBy(r => r.Titulo);

            ViewData["Trimestral"] = _context.Revista.Where(r => SqlServerDbFunctionsExtensions.DateDiffDay(db, r.Chegada, hoje) >= 120 && r.CdPeriodicidade == trimestral)
                .Include(r => r.CdEditorNavigation)
                    .Include(r => r.CdPeriodicidadeNavigation)
                        .OrderBy(r => r.Titulo);
            ViewData["Quadrimestral"] = _context.Revista.Where(r => SqlServerDbFunctionsExtensions.DateDiffDay(db, r.Chegada, hoje) >= 150 && r.CdPeriodicidade == quadrimestral)
                .Include(r => r.CdEditorNavigation)
                    .Include(r => r.CdPeriodicidadeNavigation)
                        .OrderBy(r => r.Titulo);

            ViewData["Semestral"] = _context.Revista.Where(r => SqlServerDbFunctionsExtensions.DateDiffDay(db, r.Chegada, hoje) >= 210 && r.CdPeriodicidade == semestral)
                .Include(r => r.CdEditorNavigation)
                    .Include(r => r.CdPeriodicidadeNavigation)
                        .OrderBy(r => r.Titulo);

            ViewData["Anual"] = _context.Revista.Where(r => SqlServerDbFunctionsExtensions.DateDiffDay(db, r.Chegada, hoje) >= 395 && r.CdPeriodicidade == anual)
                .Include(r => r.CdEditorNavigation)
                    .Include(r => r.CdPeriodicidadeNavigation)
                        .OrderBy(r => r.Titulo);

            return View(await appDbContext.ToListAsync());
        }

    }  
}
