using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Services.cadastro;
using Services.modelo.cadastro;
using ServicesInterfaces.banco;
using ServicesInterfaces.cadastro;
using XServicoOnline.Models;
using XServicoOnline.ViewModels;
using XServicoOnline.WebClasses;
using IsolationLevel = System.Data.IsolationLevel;
using Usuario = XServicoOnline.Models.Usuario;

namespace XServicoOnline.Controllers
{
    [Authorize(Roles ="AdministradorSistema")]
    public class EmpresaController : Controller
    {
        
        private IJsonRetorno jsonRetorno = null;
        private readonly CultureInfo cultureInfo = new CultureInfo("pt-br");
        private readonly JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();
        private JsonResult JsonResultado = null;
        private EmpresaAbstract  empresaAbstract = null;
        private IsolationLevel isolationLevel = IsolationLevel.ReadCommitted;

        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly RoleManager<Funcao> _roleManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        public EmpresaController(
        UserManager<Usuario> userManager,
        SignInManager<Usuario> signInManager,
        RoleManager<Funcao> roleManager,
        IEmailSender emailSender,
        ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailSender = emailSender;
            _logger = logger;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Create()
        {
            return await Task.Run(() => View());
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> Create(EmpresaViewModel empresaViewModel)
        {
            var jsonMensagemRetorno = JsonRetornoInclusaoAtualizacao.GetInstance();
            try
            {
                this.isolationLevel = IsolationLevel.RepeatableRead;
                this.empresaAbstract = CadastroFactory.GetInstance().CreateEmpresa(this.isolationLevel);
                IEmpresa empresa = await this.empresaAbstract.Incluir(empresaViewModel.GetEmpresa());
                Usuario usuario = new Usuario();
                usuario.Nome = empresaViewModel.RazaoSocial;
                usuario.PhoneNumber = empresaViewModel.Telefone;
                usuario.RegisterDate = DateTime.Now;
                usuario.UserName = empresaViewModel.Email;
                usuario.PasswordHash = await this.empresaAbstract.GetSenhaPadraoDoUsuarioEmpresa();
                usuario.EmpresaId = empresa.Id;
                await this.empresaAbstract.Commit();
       
                    var resultado = await this._userManager.CreateAsync(usuario, usuario.PasswordHash);
                    if (resultado.Succeeded)
                    {
                       
                        var token = await this._userManager.GenerateEmailConfirmationTokenAsync(usuario);

                        await this._userManager.ConfirmEmailAsync(usuario, token);
                        await this._userManager.SetLockoutEnabledAsync(usuario, false);
                        var role = await _roleManager.RoleExistsAsync("AdministradorEmpresa");
                        Funcao funcao = null;
                        if (!role)
                        {
                            funcao = new Funcao();
                            funcao.Name = "AdministradorEmpresa";
                            var resultadoRole = await _roleManager.CreateAsync(funcao);
                        }
                        else
                        {
                            funcao = await _roleManager.FindByNameAsync("AdministradorEmpresa");
                        }

                        await this._userManager.AddToRoleAsync(usuario, funcao.Name);
                        var tokenTelefone = await this._userManager.GenerateChangePhoneNumberTokenAsync(usuario, usuario.PhoneNumber);
                        await this._userManager.VerifyChangePhoneNumberTokenAsync(usuario, tokenTelefone, usuario.PhoneNumber);
                        var code = await this._userManager.GenerateEmailConfirmationTokenAsync(usuario);
                        var callbackUrl = Url.Page(
                                                    "/Account/ConfirmEmail",
                                                    pageHandler: null,
                                                    values: new { userId = usuario.Id, Code = code },
                        protocol: Request.Scheme);
                      
                        try
                        {
                            await _emailSender.SendEmailAsync(usuario.Email, "Confirm your email",
                           $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
                        }
                        catch (Exception ex)
                        {
                            jsonMensagemRetorno.LimparMensagem();
                            jsonMensagemRetorno.Add(ex.Message);
                        }
                        await _signInManager.SignInAsync(usuario, isPersistent: false);

                    }
                    this.jsonRetorno = jsonMensagemRetorno.Add("Inclusão realizado com sucesso");
                    foreach (var error in resultado.Errors)
                    {
                        jsonMensagemRetorno.LimparMensagem();
                        this.jsonRetorno = jsonMensagemRetorno.Add(error.Description);
                    }
   
            }
            catch (Exception ex)
            {
                JsonRetornoErro jsonRetornoErro = new JsonRetornoErro();
                this.jsonRetorno = jsonRetornoErro.Add(ex.Message);
                await this.empresaAbstract.Rollback();
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