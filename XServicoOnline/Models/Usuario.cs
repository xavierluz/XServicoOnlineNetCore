using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.bases;
using Services.cadastro;
using Services.seguranca;
using ServicesInterfaces.cadastro;
using ServicesInterfaces.seguranca;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Transactions;
using XServicoOnline.Data;
using XServicoOnline.Validacao.Telefone;

namespace XServicoOnline.Models
{
    public class Usuario : IdentityUser<string>
    {
        private IEmpresa empresaLogado = null;
        private CriptografiaFactory criptografiaFactory = null;

        public Usuario()
        {
            this.dbConnection = PostgreSqlFactory.GetInstance().GetConnection();
            this.optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            this.optionsBuilder.UseNpgsql(this.dbConnection);
            this.applicationDbContext = new ApplicationDbContext(this.optionsBuilder.Options);
        }
        #region "Atributos publicos"
        [NotMapped]
        public int totalRegistrosRetorno { get; protected set; }
        [NotMapped]
        public int registroIndex { get; protected set; }
        [NotMapped]
        public int totalRegistroPorPagina { get; protected set; }
        [NotMapped]
        public string filtro { get; protected set; }
        [NotMapped]
        private ApplicationDbContext applicationDbContext = null;
        [NotMapped]
        private DbContextOptionsBuilder<ApplicationDbContext> optionsBuilder;
        [NotMapped]
        private DbConnection dbConnection = null;
        [NotMapped]
        public string EmpresaIdCriptografada { get; set; }
        #endregion

        #region "Atributos de manipulação"
        [TempData]
        [NotMapped]
        public string StatusMessage { get; set; }
        [TempData]
        [NotMapped]
        public string returnUrl { get; set; }
        [NotMapped]
        [StringLength(20, ErrorMessage = "Digite no mínimo 8 caracters, Maiusculas, Minusculas e caracters especiais", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Confirmar Senha é obrigatório!")]
        [Display(Name = "Confirmar Senha")]
        [CompareAttribute("PasswordHash", ErrorMessage = "Senhas não confere")]
        public string PasswordHashCorfirmed { get; set; }
        #endregion
        #region "Atributos das entidades personalizados"
        public override string Id { get => base.Id; set => base.Id = value; }
        [Required(ErrorMessage = "Empresa é obrigatório!")]
        public Guid EmpresaId { get; set; }
        [Display(Name = "Usuário")]
        [Required(ErrorMessage = "Usuário é obrigatório!")]
        [StringLength(50, ErrorMessage = "Digite no mínimo 5 caracters!", MinimumLength = 5)]
        public override string UserName { get => base.UserName; set => base.UserName = value; }
        public override string NormalizedUserName { get => base.NormalizedUserName; set => base.NormalizedUserName = value; }
        [StringLength(50, ErrorMessage = "Digite no mínimo 5 caracters!", MinimumLength = 5)]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email é obrigatório!")]
        [Display(Name = "Email")]
        public override string Email { get => base.Email; set => base.Email = value; }
        public override string NormalizedEmail { get => base.NormalizedEmail; set => base.NormalizedEmail = value; }
        public override bool EmailConfirmed { get => base.EmailConfirmed; set => base.EmailConfirmed = value; }
        [StringLength(50, ErrorMessage = "Digite no mínimo 8 caracters, Maiusculas, Minusculas e caracters especiais", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Senha é obrigatório!")]
        [Display(Name = "Senha")]
        public override string PasswordHash { get => base.PasswordHash; set => base.PasswordHash = value; }
        public override string SecurityStamp { get => base.SecurityStamp; set => base.SecurityStamp = value; }
        public override string ConcurrencyStamp { get => base.ConcurrencyStamp; set => base.ConcurrencyStamp = value; }
        [TelefoneValidacao(8, ErrorMessage = "Digite telefone válido!")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Telefone")]
        public override string PhoneNumber { get => base.PhoneNumber; set => base.PhoneNumber = value; }
        public override bool PhoneNumberConfirmed { get => base.PhoneNumberConfirmed; set => base.PhoneNumberConfirmed = value; }
        public override bool TwoFactorEnabled { get => base.TwoFactorEnabled; set => base.TwoFactorEnabled = value; }
        public override DateTimeOffset? LockoutEnd { get => base.LockoutEnd; set => base.LockoutEnd = value; }
        public override bool LockoutEnabled { get => base.LockoutEnabled; set => base.LockoutEnabled = value; }
        public override int AccessFailedCount { get => base.AccessFailedCount; set => base.AccessFailedCount = value; }
        [DataType(DataType.DateTime)]
        [Display(Name = "Data cadastro")]
        public DateTime RegisterDate { get; set; }
        [Required(ErrorMessage = "Nome do usuário é obrigatório")]
        [StringLength(50, ErrorMessage = "Digite entre 5 a 50 caracters", MinimumLength = 5)]
        public String Nome { get; set; }
        public virtual ICollection<UsuarioReivindicacao> UsuarioReivindicacao { get; set; }
        public virtual ICollection<UsuarioLogin> UsuarioLogin { get; set; }
        public virtual ICollection<UsuarioToken> UsuarioToken { get; set; }
        public virtual ICollection<UsuarioFuncao> UsuarioFuncao { get; set; }
        #endregion
        #region "Métodos públicos"
        public async Task<List<Usuario>> GetUsuariosParaMontarGrid(IsolationLevel isolationLevel, int paginaIndex, string filtro, int registroPorPagina)
        {

            using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions { IsolationLevel = isolationLevel }, TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    IQueryable<Usuario> query;
                    if (paginaIndex < 0)
                        paginaIndex = 0;
                    if (!string.IsNullOrEmpty(filtro) && !string.IsNullOrWhiteSpace(filtro))
                    {
                        query = (from q in this.applicationDbContext.Set<Usuario>()
                                 where q.Nome.ToUpper().Contains(filtro.ToUpper())
                                   && q.UserName.ToUpper().Contains(filtro.ToUpper())
                                   && q.Email.ToUpper().Contains(filtro.ToUpper())
                                 select q);
                        this.totalRegistrosRetorno = await query.AsNoTracking().CountAsync();

                        query = query.Skip(paginaIndex).Take(registroPorPagina);
                    }
                    else
                    {
                        query = (from q in this.applicationDbContext.Set<Usuario>()
                                 select q);
                        this.totalRegistrosRetorno = await query.AsNoTracking().CountAsync();
                        query = query.Skip(paginaIndex).Take(registroPorPagina);
                    }
                    List<Usuario> usuarios = await query.AsNoTracking().ToListAsync();
                    scope.Complete();
                    return usuarios;

                }
                catch (Exception ex)
                {

                    throw ex;
                }
                finally
                {
                    scope.Dispose();
                }
            }

        }
        public async Task<IKeyIv> GetKeyIv(string userName)
        {
            EmpresaAbstract empresaAbstract = CadastroFactory.GetInstance().CreateEmpresa(System.Data.IsolationLevel.ReadUncommitted);
            return await empresaAbstract.GetKeyIv(userName);
        }
        public async Task<IEmpresa> GetEmpresa(string userName)
        {
            EmpresaAbstract empresaAbstract = CadastroFactory.GetInstance().CreateEmpresa(System.Data.IsolationLevel.ReadUncommitted);
            return await empresaAbstract.GetEmpresa(userName);
        }

        public async Task CriptografarUsuario(string nomeUsuarioLogado)
        {
            this.empresaLogado = await this.GetEmpresa(nomeUsuarioLogado);
            this.criptografiaFactory = CriptografiaFactory.Create(CadastroFactory.GetInstance().CreateAesCriptografia(this.empresaLogado));
            this.criptografiaFactory.AdicionarConteudo(this.Id);
            await this.criptografiaFactory.Create();
            this.Id = await this.criptografiaFactory.Get();
            this.criptografiaFactory.AdicionarConteudo(this.UserName);
            await this.criptografiaFactory.Create();
            this.UserName = await this.criptografiaFactory.Get();
            this.criptografiaFactory.AdicionarConteudo(this.UserName);
            await this.criptografiaFactory.Create();
            this.Email = await this.criptografiaFactory.Get();
            this.criptografiaFactory.AdicionarConteudo(this.EmpresaId.ToString());
            await this.criptografiaFactory.Create();
            this.EmpresaIdCriptografada = await this.criptografiaFactory.Get();
            criptografiaFactory = null;
            this.empresaLogado = null;
        }
        public async Task CriptografarUsuarioGrid(string nomeUsuarioLogado)
        {
            this.empresaLogado = await this.GetEmpresa(nomeUsuarioLogado);
            this.criptografiaFactory = CriptografiaFactory.Create(CadastroFactory.GetInstance().CreateAesCriptografia(this.empresaLogado));
            this.criptografiaFactory.AdicionarConteudo(this.Id);
            await this.criptografiaFactory.Create();
            this.Id = await this.criptografiaFactory.Get();
            this.criptografiaFactory.AdicionarConteudo(this.EmpresaId.ToString());
            await this.criptografiaFactory.Create();
            this.EmpresaIdCriptografada = await this.criptografiaFactory.Get();
            criptografiaFactory = null;
            this.empresaLogado = null;
        }
        #endregion
    }
}
