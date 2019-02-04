using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.cadastro;
using Services.seguranca;
using ServicesInterfaces.cadastro;
using XServicoOnline.Models;

namespace XServicoOnline.Controllers.bases
{
    public class BaseController : Controller
    {

        protected Usuario gerenciarUsuario = null;
        protected IEmpresa empresaLogado = null;
        protected CriptografiaFactory criptografiaFactory = null;
        public BaseController()
        {
            this.gerenciarUsuario = new Usuario();

        }
        protected async Task CreateEmpresaDoUsuarioLogado(string nomeUsuario)
        {
            this.empresaLogado = await this.gerenciarUsuario.GetEmpresa(nomeUsuario);
           
        }
        protected async Task CreateCriptografia()
        {
            await Task.Run(() =>
               this.criptografiaFactory = CriptografiaFactory.Create(CadastroFactory.GetInstance().CreateAesCriptografia(this.empresaLogado))
           );
        }
        protected async Task CreateDesCriptografia()
        {
            await Task.Run(() =>
               this.criptografiaFactory = CriptografiaFactory.Create(CadastroFactory.GetInstance().CreateAesDesCriptografia(this.empresaLogado))
           );
        }
        protected async Task CriptografiaAdicionarConteudo(string conteudo)
        {
            await Task.Run(() =>
               this.criptografiaFactory.AdicionarConteudo(conteudo)
            );
        }
        protected async Task SetCriptografia()
        {
            await this.criptografiaFactory.Create();
        }
        protected async Task<String> GetCriptografiaOuDescriptografia()
        {
            return await criptografiaFactory.Get();
        }

    }
}