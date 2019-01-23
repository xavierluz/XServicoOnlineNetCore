using System;
using System.Collections.Generic;
using System.Transactions;
using System.Globalization;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using XServicoOnline.Models;
using XServicoOnline.ViewModels;
using XServicoOnline.WebClasses;
using Microsoft.EntityFrameworkCore;

namespace XServicoOnline.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private IJsonRetorno jsonRetorno = null;
        private readonly CultureInfo cultureInfo = new CultureInfo("pt-br");
        private readonly JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();
        private JsonResult JsonResultado = null;
        private IsolationLevel isolationLevel = IsolationLevel.ReadCommitted;

        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly RoleManager<Funcao> _roleManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        public AccountController(
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
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
               
                var result = await _signInManager.PasswordSignInAsync(loginViewModel.Email, loginViewModel.PasswordHash, loginViewModel.ManterConectado, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return RedirectToAction("Index", "Home");
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new
                    {
                        ReturnUrl = "Login",
                        RememberMe = loginViewModel.ManterConectado
                    });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View();
                }
            }

            // If we got this far, something failed, redisplay form
            return View();
        }
        #region "Usuarios"
        [Authorize(Roles = "AdministradorEmpresa")]
        public async Task<IActionResult> UsuarioIndex()
        {
            return await Task.Run(() => View());

        }
        [HttpPost]
        [Authorize(Roles = "AdministradorEmpresa")]
        public async Task<JsonResult> GetUsuarios()
        {
            JsonResult jsonResultado = null;
            string search = Request.Form["search[value]"].ToString();
            string draw = Request.Form["draw"].ToString();
            string order = Request.Form["order[0][column]"].ToString();
            string orderDir = Request.Form["order[0][dir]"].ToString();
            int startRec = Convert.ToInt32(Request.Form["start"].ToString());
            int pageSize = Convert.ToInt32(Request.Form["length"].ToString());
            this.isolationLevel = IsolationLevel.ReadUncommitted;

            Usuario usuario = new Usuario();
            List<Usuario> usuarios = await usuario.GetUsuariosParaMontarGrid(this.isolationLevel, startRec, search, pageSize);
            List<UsuarioTableViewModel> usuarioTableViewModels = usuarios.ConvertAll(new Converter<Usuario, UsuarioTableViewModel>(UsuarioTableViewModel.GetInstance));

            var retorno = usuarioTableViewModels.Ordenar(order, orderDir);

            int totalRegistros = usuario.totalRegistrosRetorno;

            jsonResultado = Json(new { draw = Convert.ToInt32(draw), recordsTotal = totalRegistros, recordsFiltered = totalRegistros, data = retorno });
            return jsonResultado;
        }
        [HttpGet]
        [Authorize(Roles = "AdministradorEmpresa")]
        public async Task<IActionResult> Register()
        {
            return await Task.Run(() => View(new Usuario()));
        }
        
        [HttpPost]
        [Authorize(Roles = "AdministradorEmpresa")]
        public async Task<IActionResult> Register(Usuario usuarioView)
        {
            usuarioView.RegisterDate = DateTime.Now;
            usuarioView.UserName = usuarioView.Email;
            var resultado = await this._userManager.CreateAsync(usuarioView, usuarioView.PasswordHash);
            if (resultado.Succeeded)
            {
                //var token = await this._userManager.GenerateEmailConfirmationTokenAsync(usuarioView);

                //await this._userManager.ConfirmEmailAsync(usuarioView, token);
                //await this._userManager.SetLockoutEnabledAsync(usuarioView, false);
                //var role = await _roleManager.RoleExistsAsync("Admin");
                //Funcao funcao = new Funcao();
                //if (!role)
                //{
                //    funcao.Name = "Admin";
                //    var resultadoRole = await _roleManager.CreateAsync(funcao);
                //}
                //await this._userManager.AddToRoleAsync(usuarioView, funcao.Name);
                //var tokenTelefone = await this._userManager.GenerateChangePhoneNumberTokenAsync(usuarioView, usuarioView.PhoneNumber);
                // await this._userManager.VerifyChangePhoneNumberTokenAsync(usuarioView, tokenTelefone, usuarioView.PhoneNumber);
               // var code = await this._userManager.GenerateEmailConfirmationTokenAsync(usuarioView);
               // var callbackUrl = Url.Page(
               // "/Account/ConfirmEmail",
               // pageHandler: null,
               // values: new { userId = usuarioView.Id, Code = code },
               // protocol: Request.Scheme);

               // await _emailSender.SendEmailAsync(usuarioView.Email, "Confirm your email",
               //$"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
                await _signInManager.SignInAsync(usuarioView, isPersistent: false);
                return View(Url.Content("~/"));
            }
            foreach (var error in resultado.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> EditarUsuario(string usuarioId)
        {
            Usuario usuario = await _userManager.FindByIdAsync(usuarioId);
            return View(usuario);
        }
        [HttpPost]
        public async Task<JsonResult> EditarUsuario(Usuario usuarioView )
        {
            var jsonMensagemRetorno = JsonRetornoInclusaoAtualizacao.GetInstance();
            try
            {
                var usuario = await _userManager.FindByIdAsync(usuarioView.Id);
                if (usuario != null)
                {
                    usuario.Nome = usuario.Nome;
                    usuario.PhoneNumber = usuario.PhoneNumber;

                    await _userManager.UpdateAsync(usuario);
                    this.jsonRetorno = jsonMensagemRetorno.Add("Alteração realizado com sucesso");
                }
                else
                {
                    JsonRetornoErro jsonRetornoErro = new JsonRetornoErro();
                    jsonMensagemRetorno.LimparMensagem();
                    this.jsonRetorno = jsonRetornoErro.Add("Usuário não encontrado ou deletado");
                }

            }
            catch (Exception ex)
            {
                JsonRetornoErro jsonRetornoErro = new JsonRetornoErro();
                jsonMensagemRetorno.LimparMensagem();
                this.jsonRetorno = jsonRetornoErro.Add(ex.Message);
            }
            finally
            {
                this.JsonResultado = Json(jsonRetorno, jsonSerializerSettings);
            }
            return this.JsonResultado;
        }
        [HttpGet]
        public async Task<IActionResult> GerenciarUsuario(string usuarioId)
        {
            Usuario usuario = await _userManager.FindByIdAsync(usuarioId);
            UsuarioFuncaoViewModel usuarioFuncaoViewModel = new UsuarioFuncaoViewModel();
            usuarioFuncaoViewModel.Usuario = usuario;
            List<Funcao> funcoes = await this._roleManager.Roles.ToListAsync();
            usuarioFuncaoViewModel.Funcoes = funcoes;

            return View(usuarioFuncaoViewModel);
        }
        [HttpPost]
        public async Task<JsonResult> GerenciarUsuario(UsuarioFuncaoViewModel usuarioFuncaoViewModel)
        {
            var jsonMensagemRetorno = JsonRetornoInclusaoAtualizacao.GetInstance();
            try
            {
                if (usuarioFuncaoViewModel.Usuario != null)
                {
                    if (usuarioFuncaoViewModel.Usuario != null)
                    {
                        foreach (var funcao in usuarioFuncaoViewModel.FuncoesSelecionadas)
                            await this._userManager.AddToRoleAsync(usuarioFuncaoViewModel.Usuario, funcao.Name);
                        
                        this.jsonRetorno = jsonMensagemRetorno.Add("Alteração realizado com sucesso");
                    }
                }
                else
                {
                    JsonRetornoErro jsonRetornoErro = new JsonRetornoErro();
                    jsonMensagemRetorno.LimparMensagem();
                    this.jsonRetorno = jsonRetornoErro.Add("Usuário não encontrado ou deletado");
                }

            }
            catch (Exception ex)
            {
                JsonRetornoErro jsonRetornoErro = new JsonRetornoErro();
                jsonMensagemRetorno.LimparMensagem();
                this.jsonRetorno = jsonRetornoErro.Add(ex.Message);
            }
            finally
            {
                this.JsonResultado = Json(jsonRetorno, jsonSerializerSettings);
            }
            return this.JsonResultado;
        }
        #endregion

        #region "Funçoes(role)"
        [Authorize(Roles = "AdministradorEmpresa")]
        public async Task<IActionResult> FuncaoIndex()
        {
            return await Task.Run(() => View());

        }
        [HttpPost]
        [Authorize(Roles = "AdministradorEmpresa")]
        public async Task<JsonResult> GetFuncoes()
        {
            JsonResult jsonResultado = null;
            string search = Request.Form["search[value]"].ToString();
            string draw = Request.Form["draw"].ToString();
            string order = Request.Form["order[0][column]"].ToString();
            string orderDir = Request.Form["order[0][dir]"].ToString();
            int startRec = Convert.ToInt32(Request.Form["start"].ToString());
            int pageSize = Convert.ToInt32(Request.Form["length"].ToString());
            this.isolationLevel = IsolationLevel.ReadUncommitted;

            Funcao funcao = new Funcao();
            List<Funcao> funcoes = await funcao.GetFuncoesParaMontarGrid(this.isolationLevel, startRec, search, pageSize);
            List<FuncaoTableViewModel> funcoeTableViewModels = funcoes.ConvertAll(new Converter<Funcao, FuncaoTableViewModel>(FuncaoTableViewModel.GetInstance));
            var retorno = FuncaoTableViewModel.Ordenar(order, orderDir, funcoeTableViewModels);

            int totalRegistros = funcao.totalRegistrosRetorno;

            jsonResultado = Json(new { draw = Convert.ToInt32(draw), recordsTotal = totalRegistros, recordsFiltered = totalRegistros, data = retorno });
            return jsonResultado;
        }
        [HttpGet]
        public async Task<IActionResult> CreateFuncao()
        {
            return await Task.Run(() => View());
        }
        [HttpPost]
        public async Task<JsonResult> CreateFuncao(Funcao funcaoView)
        {
            var jsonMensagemRetorno = JsonRetornoInclusaoAtualizacao.GetInstance();
            if (!ModelState.IsValid)
            {
                jsonMensagemRetorno.LimparMensagem();
                jsonMensagemRetorno.Add(ModelState[funcaoView.Name].Errors.ToString());
                this.JsonResultado = Json(jsonRetorno, jsonSerializerSettings);

                return this.JsonResultado;
            }
           
            try
            {
                var role = await _roleManager.RoleExistsAsync(funcaoView.Name);
                if (!role)
                {
                    await _roleManager.CreateAsync(funcaoView);
                    this.jsonRetorno = jsonMensagemRetorno.Add("Inclusão realizado com sucesso");
                }
                else
                {
                    JsonRetornoErro jsonRetornoErro = new JsonRetornoErro();
                    jsonMensagemRetorno.LimparMensagem();
                    this.jsonRetorno = jsonRetornoErro.Add("Função já está cadastrada!");
                }
            }catch(Exception ex)
            {
                JsonRetornoErro jsonRetornoErro = new JsonRetornoErro();
                jsonMensagemRetorno.LimparMensagem();
                this.jsonRetorno = jsonRetornoErro.Add(ex.Message);
            }
            finally
            {
                _roleManager.Dispose();
                this.JsonResultado = Json(jsonRetorno, jsonSerializerSettings);

            }
            return this.JsonResultado;
        }
        [HttpGet]
        public async Task<IActionResult> EditarFuncao(string funcaoId)
        {
            Funcao funcao = await _roleManager.FindByIdAsync(funcaoId);
            return View(funcao);
        }
        [HttpPost]
        public async Task<JsonResult> EditarFuncao(Funcao funcaoView)
        {
            var jsonMensagemRetorno = JsonRetornoInclusaoAtualizacao.GetInstance();
            try
            {
                var funcao = await _roleManager.FindByIdAsync(funcaoView.Id);
                if (funcao != null)
                {
                    funcao.Name = funcaoView.Name;
                    funcao.NormalizedName = funcaoView.Name;

                    await _roleManager.UpdateAsync(funcao);
                    this.jsonRetorno = jsonMensagemRetorno.Add("Alteração realizado com sucesso");
                }
                else
                {
                    JsonRetornoErro jsonRetornoErro = new JsonRetornoErro();
                    jsonMensagemRetorno.LimparMensagem();
                    this.jsonRetorno = jsonRetornoErro.Add("Função não encontrada ou deletada");
                }
                
            }catch(Exception ex)
            {
                JsonRetornoErro jsonRetornoErro = new JsonRetornoErro();
                jsonMensagemRetorno.LimparMensagem();
                this.jsonRetorno = jsonRetornoErro.Add(ex.Message);
            }
            finally
            {
                this.JsonResultado = Json(jsonRetorno, jsonSerializerSettings);
            }
            return this.JsonResultado;
        }
        #endregion

        #region "Função Reinvidicação"
        [Authorize(Roles = "AdministradorEmpresa")]
        public async Task<IActionResult> FuncaoReivindicacaoIndex()
        {
            return await Task.Run(() => View());

        }
        [HttpPost]
        [Authorize(Roles = "AdministradorEmpresa")]
        public async Task<JsonResult> GetFuncoesReivindicacoes()
        {
            JsonResult jsonResultado = null;
            string search = Request.Form["search[value]"].ToString();
            string draw = Request.Form["draw"].ToString();
            string order = Request.Form["order[0][column]"].ToString();
            string orderDir = Request.Form["order[0][dir]"].ToString();
            int startRec = Convert.ToInt32(Request.Form["start"].ToString());
            int pageSize = Convert.ToInt32(Request.Form["length"].ToString());
            this.isolationLevel = IsolationLevel.ReadUncommitted;

            FuncaoReivindicacao funcaoReivindicacao = new FuncaoReivindicacao();
            List<FuncaoReivindicacao> funcaoReivindicacoes = await funcaoReivindicacao.GetFuncaoReivindicacoesParaMontarGrid(this.isolationLevel, startRec, search, pageSize);
            List<FuncaoReivindicacaoTableViewModel> funcaoReivindicacaoTableViewModels = funcaoReivindicacoes.ConvertAll(new Converter<FuncaoReivindicacao, FuncaoReivindicacaoTableViewModel>(FuncaoReivindicacaoTableViewModel.GetInstance));
            var retorno = FuncaoReivindicacaoTableViewModel.Ordenar(order, orderDir, funcaoReivindicacaoTableViewModels);

            int totalRegistros = funcaoReivindicacao.totalRegistrosRetorno;

            jsonResultado = Json(new { draw = Convert.ToInt32(draw), recordsTotal = totalRegistros, recordsFiltered = totalRegistros, data = retorno });
            return jsonResultado;
        }
        [HttpGet]
        public async Task<IActionResult> CreateFuncaoReivindicacao(string funcaoId)
        {
            FuncaoReivindicacao funcaoReivindicacao = new FuncaoReivindicacao();
            funcaoReivindicacao.RoleId = funcaoId;
            return await Task.Run(() => View(funcaoReivindicacao));
        }
        [HttpPost]
        public async Task<JsonResult> CreateFuncaoReivindicacao(FuncaoReivindicacao funcaoReivindicacaoView)
        {
            var jsonMensagemRetorno = JsonRetornoInclusaoAtualizacao.GetInstance();
            if (!ModelState.IsValid)
            {
                jsonMensagemRetorno.LimparMensagem();
                JsonRetornoErro jsonRetornoErro = new JsonRetornoErro();
                foreach (var key in ModelState.Keys)
                {
                    if (ModelState[key].Errors.Count > 0)
                        this.jsonRetorno = jsonRetornoErro.Add(ModelState[key].Errors[0].ErrorMessage);
                }
               
                this.JsonResultado = Json(jsonRetorno, jsonSerializerSettings);

                return this.JsonResultado;
            }

            try
            {
                Funcao funcao = await _roleManager.FindByIdAsync(funcaoReivindicacaoView.RoleId);

                var resultado = await _roleManager.AddClaimAsync(funcao, funcaoReivindicacaoView.ToClaim());
                if (resultado.Succeeded)
                {
                    this.jsonRetorno = jsonMensagemRetorno.Add("Inclusão realizado com sucesso");
                }
                else
                {
                    jsonMensagemRetorno.LimparMensagem();
                    JsonRetornoErro jsonRetornoErro = new JsonRetornoErro();
                    foreach (var erro in resultado.Errors)
                    {
                        this.jsonRetorno = jsonRetornoErro.Add(erro.Description);
                    }

                }
            }
            catch (Exception ex)
            {
                jsonMensagemRetorno.LimparMensagem();
                JsonRetornoErro jsonRetornoErro = new JsonRetornoErro();
                this.jsonRetorno = jsonRetornoErro.Add(ex.Message);
            }
            finally
            {
                _roleManager.Dispose();
                this.JsonResultado = Json(jsonRetorno, jsonSerializerSettings);

            }
            return this.JsonResultado;
        }
   
        [HttpPost]
        public async Task<JsonResult> RemoverFuncaoReivindicacao(string funcaoId, int funcaoReivindicacaoId)
        {
            var jsonMensagemRetorno = JsonRetornoInclusaoAtualizacao.GetInstance();
            if (!ModelState.IsValid)
            {
                jsonMensagemRetorno.LimparMensagem();
                JsonRetornoErro jsonRetornoErro = new JsonRetornoErro();
                foreach (var key in ModelState.Keys)
                {
                    if (ModelState[key].Errors.Count > 0)
                        this.jsonRetorno = jsonRetornoErro.Add(ModelState[key].Errors[0].ErrorMessage);
                }

                this.JsonResultado = Json(jsonRetorno, jsonSerializerSettings);

                return this.JsonResultado;
            }

            try
            {
                Funcao funcao = await _roleManager.FindByIdAsync(funcaoId);
                FuncaoReivindicacao funcaoReivindicacao = new FuncaoReivindicacao();
                var _funcaoReivindicacao = await funcaoReivindicacao.GetFuncaoReivindicacaoAsync(funcaoReivindicacaoId);
                var resultado = await _roleManager.RemoveClaimAsync(funcao, _funcaoReivindicacao.ToClaim());
                if (resultado.Succeeded)
                {
                    this.jsonRetorno = jsonMensagemRetorno.Add("Exclusão realizado com sucesso");
                }
                else
                {
                    jsonMensagemRetorno.LimparMensagem();
                    JsonRetornoErro jsonRetornoErro = new JsonRetornoErro();
                    foreach (var erro in resultado.Errors)
                    {
                        this.jsonRetorno = jsonRetornoErro.Add(erro.Description);
                    }

                }
            }
            catch (Exception ex)
            {
                jsonMensagemRetorno.LimparMensagem();
                JsonRetornoErro jsonRetornoErro = new JsonRetornoErro();
                this.jsonRetorno = jsonRetornoErro.Add(ex.Message);
            }
            finally
            {
                _roleManager.Dispose();
                this.JsonResultado = Json(jsonRetorno, jsonSerializerSettings);

            }
            return this.JsonResultado;
        }
        #endregion
    }
}