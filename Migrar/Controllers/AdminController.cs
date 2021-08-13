using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using Web20.Models;

namespace Web20.Controllers
{
    [Authorize(Roles = "Administrador, Coordenador")]
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly AppDbContext _context;

        public AdminController (RoleManager<IdentityRole> roleManager, AppDbContext Context)
        {
            this.roleManager = roleManager;
            _context = Context;
        }

        [HttpGet]
        public IActionResult CriarRegra()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CriarRegra (AspNetRole criar)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole {
                    Name = criar.Name
                };
                IdentityResult resultado = await roleManager.CreateAsync(identityRole);

                return RedirectToAction("index", "home");
            }
            return View(criar);
        }
        

        public async Task<IActionResult> CriarRegraUsuarioAsync(string id)
        {
            var user = await _context.AspNetUsers.FindAsync(id);
            ViewData["RoleId"] = new SelectList(_context.AspNetRoles, "Id", "Name");
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "FirstName",user.FirstName);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CriarRegraUsuario ([Bind("UserId,RoleId")] AspNetUserRole aspNetUserRole)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aspNetUserRole);
                await _context.SaveChangesAsync();
                return RedirectToAction("index", "home");
            }
            ViewData["RoleId"] = new SelectList(_context.AspNetRoles, "Id", "Id", aspNetUserRole.RoleId);
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", aspNetUserRole.UserId);
            return View(aspNetUserRole);
        }
    }
}
