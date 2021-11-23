using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Web20.Interfaces;
using Web20.Models;

namespace Web20.Controllers
{
    [Authorize]
    public class PendenciaController : Controller
    {
        private readonly IPendenciasServicos pendenciasServicos;
        private readonly AppDbContext _context;
       
        public PendenciaController(AppDbContext context, IPendenciasServicos pendenciasServicos)
        {
            _context = context;
            this.pendenciasServicos = pendenciasServicos;
        }

        public async Task<IActionResult> Index()
        {
            var Pendencias = pendenciasServicos.PendenciasMensais(_context);

            ViewData["Bimestral"] = pendenciasServicos.PendenciasBimestrais(_context);

            ViewData["Trimestral"] = pendenciasServicos.PendenciasTrimestrais(_context);

            ViewData["Quadrimestral"] = pendenciasServicos.PendenciasQuadrimestrais(_context);

            ViewData["Semestral"] = pendenciasServicos.PendenciasSemestrais(_context);

            ViewData["Anual"] = pendenciasServicos.PendenciasAnuais(_context);

            return View(Pendencias);
        }

    }
}
