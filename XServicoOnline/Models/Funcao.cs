using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services.bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using XServicoOnline.Data;

namespace XServicoOnline.Models
{
    public class Funcao: IdentityRole<string>
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
        public Funcao()
        {
            this.dbConnection = PostgreSqlFactory.GetInstance().GetConnection();
            this.optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            this.optionsBuilder.UseNpgsql(this.dbConnection);
            this.applicationDbContext = new ApplicationDbContext(this.optionsBuilder.Options);
        }

        public virtual ICollection<UsuarioFuncao> UsuarioFuncao { get; set; }
        public virtual ICollection<FuncaoReivindicacao> FuncaoReivindicacao { get; set; }
        public override string Id { get => base.Id; set => base.Id = value; }
        [Required(ErrorMessage = "Nome do usuário é obrigatório")]
        [StringLength(50, ErrorMessage = "Digite entre 5 a 50 caracters", MinimumLength = 5)]
        [Display(Name="Nome")]
        public override string Name { get => base.Name; set => base.Name = value; }
        public override string NormalizedName { get => base.NormalizedName; set => base.NormalizedName = value; }
        public override string ConcurrencyStamp { get => base.ConcurrencyStamp; set => base.ConcurrencyStamp = value; }

        #region "Métodos públicos"
        public async Task<List<Funcao>> GetFuncoesParaMontarGrid(IsolationLevel isolationLevel, int paginaIndex, string filtro, int registroPorPagina)
        {

            using(var scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions { IsolationLevel = isolationLevel },TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    IQueryable<Funcao> query;
                    if (paginaIndex < 0)
                        paginaIndex = 0;
                    if (!string.IsNullOrEmpty(filtro) && !string.IsNullOrWhiteSpace(filtro))
                    {
                        query = (from q in this.applicationDbContext.Set<Funcao>()
                                 where q.Name.ToUpper().Contains(filtro.ToUpper())
                                   && q.Id.ToUpper().Contains(filtro.ToUpper())
                                 select q);
                        this.totalRegistrosRetorno = await query.AsNoTracking().CountAsync();

                        query = query.Skip(paginaIndex).Take(registroPorPagina);
                    }
                    else
                    {
                        query = (from q in this.applicationDbContext.Set<Funcao>()
                                 select q);
                        this.totalRegistrosRetorno = await query.AsNoTracking().CountAsync();
                        query = query.Skip(paginaIndex).Take(registroPorPagina);
                    }
                    List<Funcao> Funcoes = await query.AsNoTracking().ToListAsync();
                    scope.Complete();
                    return Funcoes;

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
        #endregion
    }
}
