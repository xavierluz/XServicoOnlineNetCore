using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using XServicoOnline.Models;
using XServicoOnline.ViewModels;

namespace XServicoOnline.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
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
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Register()
        {
            return await Task.Run(() => View(new Usuario()));
        }
        #region "Métodos Publicos"
        [HttpPost]
        [AllowAnonymous]
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
    
        #endregion
    }
}