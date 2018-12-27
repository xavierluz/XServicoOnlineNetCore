using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using XServicoOnline.Models;

namespace XServicoOnline.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        public HomeController(UserManager<Usuario> userManager)
        {
            this._userManager = userManager;
        }
        private async Task<string> GetNomeUsuarioLogado()
        {
            Usuario usr = await GetUsuarioLogadoAsync();
            return usr?.Nome;
        }

        private Task<Usuario> GetUsuarioLogadoAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.nomeUsuarioLogado = await GetNomeUsuarioLogado();
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
