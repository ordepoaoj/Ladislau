using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web20.Models;

namespace Web20.Controllers
{
    public class EtiquetasController : Controller
    {
        private readonly AppDbContext _context;
        // GET: EtiquetasController

        public EtiquetasController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            
            var contexto = _context.Editors.Include(r => r.CodPaisNavigation.CodContinenteNavigation).OrderBy(r => r.CodPaisNavigation.CodContinente);
            return View(await contexto.ToListAsync());
        }

        public async Task<IActionResult> PDF()
        {
            var contexto = _context.Editors.Include(r => r.CodPaisNavigation).Include(r => r.CodPaisNavigation.CodContinenteNavigation);
            return new ViewAsPdf(await contexto.ToListAsync());
        }
    }
}
