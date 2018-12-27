using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.bases;
using Services.produto;
using ServicesInterfaces.banco;
using ServicesInterfaces.produto;
using XServicoOnline.ViewModels;

namespace XServicoOnline.Controllers
{
    public class MaterialController : Controller
    {
        private CategoriaAbstract categoriaAbstract = null;
        private IsolationLevel isolationLevel = IsolationLevel.ReadCommitted;
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> CreateCategoria()
        {
            return await Task.Run(()=> View());
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategoria(CategoriaViewModel categoriaViewModel)
        {

            this.isolationLevel = NivelIsolamentoBancoDeDados.GetLerDadosComitado();

            categoriaAbstract = ProdutoFactory.GetInstance().CreateInstance(this.isolationLevel);
            ICategoria categoria = await categoriaAbstract.Incluir(categoriaViewModel.GetICategoria());

            return View(categoria);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> CategoriaIndex()
        {
            return await Task.Run(() => View());
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> GetCategorias()
        {
            string search = Request.Form["search[value]"].ToString();
            string draw = Request.Form["draw"].ToString();
            string order = Request.Form["order[0][column]"].ToString();
            string orderDir = Request.Form["order[0][dir]"].ToString();
            int startRec = Convert.ToInt32(Request.Form["start"].ToString());
            int pageSize = Convert.ToInt32(Request.Form["length"].ToString());
            this.isolationLevel = NivelIsolamentoBancoDeDados.GetLerDadosNaoComitado();

            categoriaAbstract = ProdutoFactory.GetInstance().CreateInstance(this.isolationLevel);
            IList<ICategoria> categorias = await categoriaAbstract.getCategoriasParaMontarGrid(startRec, search, pageSize);
            int totalRegistros = categorias.Count;

            IList<CategoriaTableViewModel> categoriaTableViewModels = ((List<ICategoria>)categorias).ConvertAll(new Converter<ICategoria, CategoriaTableViewModel>(CategoriaTableViewModel.GetInstance));
            var retorno = CategoriaTableViewModel.Ordenar(order, orderDir, categoriaTableViewModels);

            return Json(new { draw = Convert.ToInt32(draw), recordsTotal = totalRegistros, recordsFiltered = totalRegistros, data = retorno });
        }
    }
}