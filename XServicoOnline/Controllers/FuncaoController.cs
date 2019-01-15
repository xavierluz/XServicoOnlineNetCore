using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using XServicoOnline.Models;

namespace XServicoOnline.Controllers
{
    public class FuncaoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Create()
        {
            return await Task.Run(() => View());
        }
        public async Task<IActionResult> Create(Funcao  funcao)
        {
            return await Task.Run(() => View());
        }
    }
}