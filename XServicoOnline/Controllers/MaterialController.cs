using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Services.bases;
using Services.produto;
using ServicesInterfaces.banco;
using ServicesInterfaces.produto;
using XServicoOnline.ViewModels;
using XServicoOnline.WebClasses;

namespace XServicoOnline.Controllers
{
    [Authorize]
    public class MaterialController : Controller
    {
        private IJsonRetorno jsonRetorno = null;
        private readonly CultureInfo cultureInfo = new CultureInfo("pt-br");
        private readonly JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();
        private JsonResult JsonResultado = null;
        private CategoriaAbstract categoriaAbstract = null;
        private ClassificacoAbstract classificacoAbstract = null;
        private MaterialAbstract materialAbstract = null;
        private IsolationLevel isolationLevel = IsolationLevel.ReadCommitted;
        public MaterialController()
        {
            jsonSerializerSettings.Culture = this.cultureInfo;
        }
        public IActionResult Index()
        {
            return View();
        }
        #region "Categoria"
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> CreateCategoria()
        {
            return await Task.Run(()=> View());
        }
        [HttpPost]
        public async Task<JsonResult> CreateCategoria(CategoriaViewModel categoriaViewModel)
        {
            var jsonMensagemRetorno = JsonRetornoInclusaoAtualizacao.GetInstance();
            try
            {

                this.isolationLevel = NivelIsolamentoBancoDeDados.GetLerDadosComitado();

                categoriaAbstract = ProdutoFactory.GetInstance().CreateCategoria(this.isolationLevel);
                ICategoria categoria = await categoriaAbstract.Incluir(categoriaViewModel.GetICategoria());
                this.jsonRetorno = jsonMensagemRetorno.Create("Inclusão realizado com sucesso");

            }catch(Exception ex)
            {
                this.jsonRetorno = JsonRetornoErro.Create(ex.Message);

            }
            finally
            {
                this.JsonResultado = Json(jsonRetorno, jsonSerializerSettings);
            }
            return this.JsonResultado;
        }
        [HttpGet]
        public async Task<IActionResult> EditarCategoria(int categoriaId)
        {
            try
            {
                this.isolationLevel = NivelIsolamentoBancoDeDados.GetLerDadosComitado();

                categoriaAbstract = ProdutoFactory.GetInstance().CreateCategoria(this.isolationLevel);
                ICategoria categoria = await categoriaAbstract.ConsultarPorId(categoriaId);
                CategoriaViewModel categoriaViewModel = new CategoriaViewModel();
               

                return View(categoriaViewModel.GetICategoria(categoria));
            }
            catch(Exception ex)
            {
                throw ex;
            }
           
        }
        [HttpPost]
        public async Task<JsonResult> EditarCategoria(CategoriaViewModel categoriaViewModel)
        {
            var jsonMensagemRetorno = JsonRetornoInclusaoAtualizacao.GetInstance();
            try
            {
                this.isolationLevel = NivelIsolamentoBancoDeDados.GetLerDadosComitado();
                categoriaAbstract = ProdutoFactory.GetInstance().CreateCategoria(this.isolationLevel);
                ICategoria categoria = await categoriaAbstract.Atualizar(categoriaViewModel.GetICategoria());
                this.jsonRetorno = jsonMensagemRetorno.Create("Atualização realizado com sucesso");
            }
            catch(Exception ex)
            {
                this.jsonRetorno = JsonRetornoErro.Create(ex.Message);
            }
            finally
            {
                this.JsonResultado = Json(jsonRetorno, jsonSerializerSettings);
            }

            return this.JsonResultado;
        }
        [HttpGet]
        public async Task<IActionResult> CategoriaIndex()
        {
            return await Task.Run(() => View());
        }
        [HttpPost]
        public async Task<JsonResult> GetCategorias()
        {
            JsonResult jsonResultado = null;
            string search = Request.Form["search[value]"].ToString();
            string draw = Request.Form["draw"].ToString();
            string order = Request.Form["order[0][column]"].ToString();
            string orderDir = Request.Form["order[0][dir]"].ToString();
            int startRec = Convert.ToInt32(Request.Form["start"].ToString());
            int pageSize = Convert.ToInt32(Request.Form["length"].ToString());
            this.isolationLevel = NivelIsolamentoBancoDeDados.GetLerDadosNaoComitado();

            categoriaAbstract = ProdutoFactory.GetInstance().CreateCategoria(this.isolationLevel);
            IList<ICategoria> categorias = await categoriaAbstract.getCategoriasParaMontarGrid(startRec, search, pageSize);
            int totalRegistros = categoriaAbstract.totalRegistrosRetorno;

            IList<CategoriaTableViewModel> categoriaTableViewModels = ((List<ICategoria>)categorias).ConvertAll(new Converter<ICategoria, CategoriaTableViewModel>(CategoriaTableViewModel.GetInstance));
            var retorno = CategoriaTableViewModel.Ordenar(order, orderDir, categoriaTableViewModels);

            jsonResultado =  Json(new { draw = Convert.ToInt32(draw), recordsTotal = totalRegistros, recordsFiltered = totalRegistros, data = retorno });
            return jsonResultado;
        }
        #endregion
        #region "Classificação"
        [HttpGet]
        public async Task<IActionResult> ClassificacaoIndex()
        {
            return await Task.Run(() => View());
        }
        [HttpPost]
        public async Task<JsonResult> GetClassificacoes()
        {

            string search = Request.Form["search[value]"].ToString();
            string draw = Request.Form["draw"].ToString();
            string order = Request.Form["order[0][column]"].ToString();
            string orderDir = Request.Form["order[0][dir]"].ToString();
            int startRec = Convert.ToInt32(Request.Form["start"].ToString());
            int pageSize = Convert.ToInt32(Request.Form["length"].ToString());
            this.isolationLevel = NivelIsolamentoBancoDeDados.GetLerDadosNaoComitado();

            this.classificacoAbstract = ProdutoFactory.GetInstance().CreateClassificao(this.isolationLevel);
            IList<IClassificacao> classificacoes  = await this.classificacoAbstract.GetClassificacaoesParaMontarGrid(startRec, search, pageSize);
            int totalRegistros = this.classificacoAbstract.totalRegistrosRetorno;

            IList<ClassificacaoTableViewModel> classificacaoTableViewModels = ((List<IClassificacao>)classificacoes).ConvertAll(new Converter<IClassificacao, ClassificacaoTableViewModel>(ClassificacaoTableViewModel.GetInstance));
            var retorno = ClassificacaoTableViewModel.Ordenar(order, orderDir, classificacaoTableViewModels);

            this.JsonResultado = Json(new { draw = Convert.ToInt32(draw), recordsTotal = totalRegistros, recordsFiltered = totalRegistros, data = retorno });
            return this.JsonResultado;
        }
        [HttpGet]
        public async Task<IActionResult> CreateClassificacao()
        {
            return await Task.Run(() => View());
        }
        [HttpPost]
        public async Task<IActionResult> CreateClassificacao(ClassificacaoViewModel classificacaoViewModel)
        {
            var jsonMensagemRetorno = JsonRetornoInclusaoAtualizacao.GetInstance();
            try
            {
                this.isolationLevel = NivelIsolamentoBancoDeDados.GetLerDadosComitado();
                this.classificacoAbstract = ProdutoFactory.GetInstance().CreateClassificao(this.isolationLevel);
                IClassificacao classificacao= await classificacoAbstract.Incluir(classificacaoViewModel.GetClassificacao());
                this.jsonRetorno = jsonMensagemRetorno.Create("Inclusão realizado com sucesso");
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
        [HttpGet]
        public async Task<IActionResult> EditarClassificacao(int classificacaoId)
        {
            try
            {
                this.isolationLevel = NivelIsolamentoBancoDeDados.GetLerDadosComitado();

                this.classificacoAbstract = ProdutoFactory.GetInstance().CreateClassificao(this.isolationLevel);
                IClassificacao classificacao = await this.classificacoAbstract.ConsultarPorId(classificacaoId);
                ClassificacaoViewModel classificacaoViewModel = new ClassificacaoViewModel();


                return View(classificacaoViewModel.GetClassificacao(classificacao));
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [HttpPost]
        public async Task<JsonResult> EditarClassificacao(ClassificacaoViewModel classificacaoViewModel)
        {
            var jsonMensagemRetorno = JsonRetornoInclusaoAtualizacao.GetInstance();
            try
            {
                this.isolationLevel = NivelIsolamentoBancoDeDados.GetLerDadosComitado();
                this.classificacoAbstract = ProdutoFactory.GetInstance().CreateClassificao(this.isolationLevel);
                IClassificacao classificacao = await this.classificacoAbstract.Atualizar(classificacaoViewModel.GetClassificacao());
                this.jsonRetorno = jsonMensagemRetorno.Create("Atualização realizado com sucesso");
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
        #endregion
        #region "Material"
        [HttpGet]
        public async Task<IActionResult> MaterialIndex()
        {
            return await Task.Run(() => View());
        }
        [HttpPost]
        public async Task<JsonResult> GetMateriais()
        {

            string search = Request.Form["search[value]"].ToString();
            string draw = Request.Form["draw"].ToString();
            string order = Request.Form["order[0][column]"].ToString();
            string orderDir = Request.Form["order[0][dir]"].ToString();
            int startRec = Convert.ToInt32(Request.Form["start"].ToString());
            int pageSize = Convert.ToInt32(Request.Form["length"].ToString());
            this.isolationLevel = NivelIsolamentoBancoDeDados.GetLerDadosNaoComitado();

            this.materialAbstract = ProdutoFactory.GetInstance().CreateMaterial(this.isolationLevel);
            IList<IMaterial> materiais = await this.materialAbstract.GetMateriaisParaMontarGrid(startRec, search, pageSize);
            int totalRegistros = this.materialAbstract.totalRegistrosRetorno;

            IList<MaterialTableViewModel> materiaisTableViewModels = ((List<IMaterial>)materiais).ConvertAll(new Converter<IMaterial , MaterialTableViewModel>(MaterialTableViewModel.GetInstance));
            var retorno = MaterialTableViewModel.Ordenar(order, orderDir, materiaisTableViewModels);

            this.JsonResultado = Json(new { draw = Convert.ToInt32(draw), recordsTotal = totalRegistros, recordsFiltered = totalRegistros, data = retorno });
            return this.JsonResultado;
        }
        [HttpGet]
        public async Task<IActionResult> CreateMaterial()
        {
            
            MaterialViewModel materialViewModel = new MaterialViewModel();
            var categorias = await GetTodasCategorias();
            var classificacoes = await GetTodasClassificacoes();
            materialViewModel.Categorias = new SelectList(categorias, "Id", "Nome");
            materialViewModel.Classificacoes = new SelectList(classificacoes, "Id", "Nome");
            return await Task.Run(() => View(materialViewModel));
        }
        [HttpPost]
        public async Task<IActionResult> CreateMaterial(MaterialViewModel materilaViewModel)
        {
            var jsonMensagemRetorno = JsonRetornoInclusaoAtualizacao.GetInstance();
            try
            {
                this.isolationLevel = NivelIsolamentoBancoDeDados.GetLerDadosComitado();
                this.materialAbstract = ProdutoFactory.GetInstance().CreateMaterial(this.isolationLevel);
                IMaterial material = await this.materialAbstract.Incluir(materilaViewModel.GetMaterial());
                this.jsonRetorno = jsonMensagemRetorno.Create("Inclusão realizado com sucesso");
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
        [HttpGet]
        public async Task<IActionResult> EditarMaterial(int materialId)
        {
            try
            {
                this.isolationLevel = NivelIsolamentoBancoDeDados.GetLerDadosComitado();

                this.materialAbstract = ProdutoFactory.GetInstance().CreateMaterial(this.isolationLevel);
                IMaterial material = await this.materialAbstract.ConsultarPorId(materialId);
                MaterialViewModel materialViewModel = new MaterialViewModel();
                var categorias = await GetTodasCategorias();
                var classificacoes = await GetTodasClassificacoes();
             
                var _material = materialViewModel.GetMaterial(material);
                _material.Categorias = new SelectList(categorias, "Id", "Nome");
                _material.Classificacoes = new SelectList(classificacoes, "Id", "Nome");
                return View(_material);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [HttpPost]
        public async Task<JsonResult> EditarMaterial(MaterialViewModel  materilaViewModel)
        {
            var jsonMensagemRetorno = JsonRetornoInclusaoAtualizacao.GetInstance();
            try
            {
                this.isolationLevel = NivelIsolamentoBancoDeDados.GetLerDadosComitado();
                this.materialAbstract = ProdutoFactory.GetInstance().CreateMaterial(this.isolationLevel);
                IMaterial material = await this.materialAbstract.Atualizar(materilaViewModel.GetMaterial());
                this.jsonRetorno = jsonMensagemRetorno.Create("Atualização realizado com sucesso");
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
        #endregion
        #region "Métodos privados"
        private async Task<IList<ICategoria>> GetTodasCategorias()
        {
            this.isolationLevel = NivelIsolamentoBancoDeDados.GetLerDadosComitado();
            this.categoriaAbstract = ProdutoFactory.GetInstance().CreateCategoria(this.isolationLevel);
            return await this.categoriaAbstract.GetCategorias();
        }
        private async Task<IList<IClassificacao>> GetTodasClassificacoes()
        {
            this.isolationLevel = NivelIsolamentoBancoDeDados.GetLerDadosComitado();
            this.classificacoAbstract = ProdutoFactory.GetInstance().CreateClassificao(this.isolationLevel);
            return await this.classificacoAbstract.GetClassificacoes();
        }
        #endregion
    }
}