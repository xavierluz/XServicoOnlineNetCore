using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Services.cadastro;
using ServicesInterfaces.banco;
using ServicesInterfaces.cadastro;
using XServicoOnline.ViewModels;
using XServicoOnline.WebClasses;

namespace XServicoOnline.Controllers
{
    public class EmpresaController : Controller
    {
        private IJsonRetorno jsonRetorno = null;
        private readonly CultureInfo cultureInfo = new CultureInfo("pt-br");
        private readonly JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();
        private JsonResult JsonResultado = null;
        private EmpresaAbstract  empresaAbstract = null;
        private IsolationLevel isolationLevel = IsolationLevel.ReadCommitted;

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return await Task.Run(() => View());
        }

        [HttpPost]
        public async Task<JsonResult> Create(EmpresaViewModel empresaViewModel)
        {
            try
            {
                this.isolationLevel = IsolationLevel.RepeatableRead;
                this.empresaAbstract = CadastroFactory.GetInstance().CreateEmpresa(this.isolationLevel);
                IEmpresa empresa = await this.empresaAbstract.Incluir(empresaViewModel.GetEmpresa());
                this.jsonRetorno = JsonRetornoInclusaoAtualizacao.Create("Inclusão realizado com sucesso");
            }
            catch (Exception ex)
            {
                this.jsonRetorno = JsonRetornoErro.Create(ex.Message);

            }
            finally
            {
                this.JsonResultado = Json(jsonRetorno, jsonSerializerSettings);
            }
            return this.JsonResultado;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> GetEmpresas()
        {
            JsonResult jsonResultado = null;
            string search = Request.Form["search[value]"].ToString();
            string draw = Request.Form["draw"].ToString();
            string order = Request.Form["order[0][column]"].ToString();
            string orderDir = Request.Form["order[0][dir]"].ToString();
            int startRec = Convert.ToInt32(Request.Form["start"].ToString());
            int pageSize = Convert.ToInt32(Request.Form["length"].ToString());
            this.isolationLevel = NivelIsolamentoBancoDeDados.GetLerDadosNaoComitado();

            this.empresaAbstract  = CadastroFactory.GetInstance().CreateEmpresa(this.isolationLevel);
            IList<IEmpresa> empresas = await empresaAbstract.getEmpresasParaMontarGrid(startRec, search, pageSize);
            int totalRegistros = empresaAbstract.totalRegistrosRetorno;

            IList<EmpresaTableViewModel>  empresaTableViewModels = ((List<IEmpresa>)empresas).ConvertAll(new Converter<IEmpresa, EmpresaTableViewModel>(EmpresaTableViewModel.GetInstance));
            var retorno = EmpresaTableViewModel
.Ordenar(order, orderDir, empresaTableViewModels);

            jsonResultado = Json(new { draw = Convert.ToInt32(draw), recordsTotal = totalRegistros, recordsFiltered = totalRegistros, data = retorno });
            return jsonResultado;
        }
    }
}