using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Services.produto;
using ServicesInterfaces.banco;
using ServicesInterfaces.produto;
using XServicoOnline.ViewModels;

namespace XServicoOnline.Controllers
{
    [ApiController]
    public class ApiMaterialController : ControllerBase
    {
        private readonly CultureInfo cultureInfo = new CultureInfo("pt-br");
        private readonly JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();
        private JsonResult JsonResultado = null;
        private MaterialAbstract materialAbstract = null;
        private IsolationLevel isolationLevel = IsolationLevel.ReadCommitted;

        [HttpGet]
        [Route("api/Material/GetQuantidade")]
        public async Task<MaterialStatusViewModel> GetQuantidadeStatus()
        {
            this.isolationLevel = NivelIsolamentoBancoDeDados.GetLerDadosComitado();
            materialAbstract = ProdutoFactory.GetInstance().CreateMaterial(this.isolationLevel);
            IMaterialStatus materialStatus = await materialAbstract.GetMaterialStatus();
            return new MaterialStatusViewModel().GetMaterialStatus(materialStatus);
        }
    }
}