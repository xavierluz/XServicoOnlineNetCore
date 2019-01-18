using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services.bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Transactions;
using XServicoOnline.Data;

namespace XServicoOnline.Models
{
    public class FuncaoReivindicacao: IdentityRoleClaim<string>
    {
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
        #endregion
        public FuncaoReivindicacao()
        {
            this.dbConnection = PostgreSqlFactory.GetInstance().GetConnection();
            this.optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            this.optionsBuilder.UseNpgsql(this.dbConnection);
            this.applicationDbContext = new ApplicationDbContext(this.optionsBuilder.Options);
        }
        public virtual Funcao Funcao { get; set; }
        public override int Id { get => base.Id; set => base.Id = value; }
        [Required(ErrorMessage = "Função da reivindicação é obrigatório")]
        public override string RoleId { get => base.RoleId; set => base.RoleId = value; }
        [Required(ErrorMessage = "Nome da reivindicação é obrigatório")]
        [StringLength(100, ErrorMessage = "Digite entre 5 a 100 caracters", MinimumLength = 5)]
        [Display(Name = "Reivindicação")]
        public override string ClaimType { get => base.ClaimType; set => base.ClaimType = value; }
        [Required(ErrorMessage = "Valor da reivindicação é obrigatório")]
        [StringLength(100, ErrorMessage = "Digite entre 5 a 100 caracters", MinimumLength = 5)]
        [Display(Name = "Valor")]
        public override string ClaimValue { get => base.ClaimValue; set => base.ClaimValue = value; }
        #region "Métodos públicos"
        public async Task<List<FuncaoReivindicacao>> GetFuncaoReivindicacoesParaMontarGrid(IsolationLevel isolationLevel, int paginaIndex, string filtro, int registroPorPagina)
        {

            using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions { IsolationLevel = isolationLevel }, TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    IQueryable<FuncaoReivindicacao> query;
                    if (paginaIndex < 0)
                        paginaIndex = 0;
                    if (!string.IsNullOrEmpty(filtro) && !string.IsNullOrWhiteSpace(filtro))
                    {
                        query = (from q in this.applicationDbContext.Set<FuncaoReivindicacao>()
                                 where q.ClaimType.ToUpper().Contains(filtro.ToUpper())
                                   && q.ClaimValue.ToUpper().Contains(filtro.ToUpper())
                                 select q);
                        this.totalRegistrosRetorno = await query.AsNoTracking().CountAsync();

                        query = query.Skip(paginaIndex).Take(registroPorPagina);
                    }
                    else
                    {
                        query = (from q in this.applicationDbContext.Set<FuncaoReivindicacao>()
                                 select q);
                        this.totalRegistrosRetorno = await query.AsNoTracking().CountAsync();
                        query = query.Skip(paginaIndex).Take(registroPorPagina);
                    }
                    List<FuncaoReivindicacao> FuncaoReivindicacoes = await query.AsNoTracking().ToListAsync();
                    scope.Complete();
                    return FuncaoReivindicacoes;

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

        public override Claim ToClaim()
        {
            return base.ToClaim();
        }

        public override void InitializeFromClaim(Claim other)
        {
            base.InitializeFromClaim(other);
        }
        #endregion
    }
}
