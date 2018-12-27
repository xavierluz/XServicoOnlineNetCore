using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using XServicoOnline.ViewModels;

namespace XServicoOnline.Controllers
{
    public class LoginController : Controller
    {
       
        public IActionResult Login()
        {
            return View();
        }
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            return await Task.Run(() => View());
        }
    }
}