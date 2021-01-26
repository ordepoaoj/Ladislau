using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web20.Models;

namespace Web20.Controllers
{
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public AdminController (RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
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
    }
}
