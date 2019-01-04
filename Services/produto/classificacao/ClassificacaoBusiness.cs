using Microsoft.EntityFrameworkCore;
using Services.bases;
using Services.modelo.produto;
using Services.produto.contexto;
using Services.produto.repositorio;
using ServicesInterfaces.produto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.produto.classificacao
{
    internal class ClassificacaoBusiness : ClassificacoAbstract
    {
        private ProdutoUnitOfWork produtoUnitOfWork = null;
        private BaseProdutoRepositorio<Classificacao> classificacoRepositorio = null;
        private DbContextOptionsBuilder<ProdutoContexto> optionsBuilder;
        private ClassificacaoBusiness(IsolationLevel isolationLevel) : base(isolationLevel)
        {
            this.dbConnection = PostgreSqlFactory.GetInstance().GetConnection();
            optionsBuilder = new DbContextOptionsBuilder<ProdutoContexto>();
            optionsBuilder.UseNpgsql(this.dbConnection);

            this.produtoUnitOfWork = ProdutoUnitOfWork.GetInstance(optionsBuilder.Options, this.isolationLevel);
            this.classificacoRepositorio = this.produtoUnitOfWork.ClassificaoRepositorio;
        }
        public static ClassificacaoBusiness GetInstancia(IsolationLevel isolationLevel)
        {
            return new ClassificacaoBusiness(isolationLevel);
        }
      
        public override async Task<IClassificacao> Atualizar(IClassificacao classificacao)
        {
            await produtoUnitOfWork.CreateTransacao();
            try
            {
                Classificacao _classificacao = Classificacao.GetInstance().GetClassificacao(classificacao);
                _classificacao.Ativo = true;
                await this.classificacoRepositorio.AtualizarAsync(_classificacao);
                await produtoUnitOfWork.SalvarAsync();
                produtoUnitOfWork.Commit();

                return _classificacao.GetClassificacao();
            }

            catch (Exception ex)
            {
                produtoUnitOfWork.Rollback();
                throw ex;
            }
            finally
            {
                produtoUnitOfWork.Dispose();
            }
        }

        public override async Task<IClassificacao> ConsultarPorId(int classificacaoId)
        {
            await produtoUnitOfWork.CreateTransacao();
            try
            {
                IQueryable<Classificacao> query;

                query = (from q in this.classificacoRepositorio.produtoContexto.Classificacaos
                         where q.Ativo == true
                           && q.Id == classificacaoId
                         select q);

                Classificacao classificacoes = await this.classificacoRepositorio.GetAsync(query);
                produtoUnitOfWork.Commit();
                return classificacoes.GetClassificacao();
            }
            catch (Exception ex)
            {
                produtoUnitOfWork.Rollback();
                throw ex;
            }
            finally
            {
                produtoUnitOfWork.Dispose();
            }
        }

        public override async Task<IList<IClassificacao>> GetClassificacaoesParaMontarGrid(int paginaIndex, string filtro, int registroPorPagina)
        {
            await produtoUnitOfWork.CreateTransacao();
            try
            {
                IQueryable<Classificacao> query;
                if (paginaIndex < 0)
                    paginaIndex = 0;
                if (!string.IsNullOrEmpty(filtro) && !string.IsNullOrWhiteSpace(filtro))
                {
                    query = (from q in this.classificacoRepositorio.produtoContexto.Classificacaos
                             where q.Nome.ToUpper().Contains(filtro.ToUpper())
                               && q.Descricao.ToUpper().Contains(filtro.ToUpper())
                               && q.Ativo.ToString().Contains(filtro)
                             select q);
                    this.totalRegistrosRetorno = await this.classificacoRepositorio.GetCountAsync(query);
                    query = query.Skip(paginaIndex).Take(registroPorPagina);
                }
                else
                {
                    query = (from q in this.classificacoRepositorio.produtoContexto.Classificacaos
                             select q);
                    this.totalRegistrosRetorno = await this.classificacoRepositorio.GetCountAsync(query);
                    query = query.Skip(paginaIndex).Take(registroPorPagina);
                }
                List<Classificacao> classificacaos = await this.classificacoRepositorio.GetsAsync(query);
                produtoUnitOfWork.Commit();
                return classificacaos.ConvertAll(new Converter<Classificacao, IClassificacao>(cla => cla.GetClassificacao()));

            }
            catch (Exception ex)
            {
                produtoUnitOfWork.Rollback();
                throw ex;
            }
            finally
            {
                produtoUnitOfWork.Dispose();
            }
        }

        public override async Task<IList<IClassificacao>> GetClassificacoes()
        {
            await produtoUnitOfWork.CreateTransacao();
            try
            {
                IQueryable<Classificacao> query;
            
                query = (from q in this.classificacoRepositorio.produtoContexto.Classificacaos
                            where q.Ativo == true
                            select q);

                List<Classificacao> classificacoes = await this.classificacoRepositorio.GetsAsync(query);
                produtoUnitOfWork.Commit();
                return classificacoes.ConvertAll(new Converter<Classificacao, IClassificacao>(cla => cla.GetClassificacao()));
            }
            catch (Exception ex)
            {
                produtoUnitOfWork.Rollback();
                throw ex;
            }
            finally
            {
                produtoUnitOfWork.Dispose();
            }
        }

        public override async Task<int> GetQuantidade()
        {
            IQueryable<Classificacao> query;

            query = (from q in this.classificacoRepositorio.produtoContexto.Classificacaos
                     where q.Ativo == true
                     select q);
            return await this.classificacoRepositorio.GetCountAsync(query);
        }

        public override async Task<IClassificacao> Incluir(IClassificacao classificacao)
        {
            await produtoUnitOfWork.CreateTransacao();
            try
            {
                Classificacao _classificacao = Classificacao.GetInstance().GetClassificacao(classificacao);
                _classificacao.Ativo = true;
                await this.classificacoRepositorio.AdicionarAsync(_classificacao);
                await produtoUnitOfWork.SalvarAsync();
                produtoUnitOfWork.Commit();

                return _classificacao.GetClassificacao();
            }

            catch (Exception ex)
            {
                produtoUnitOfWork.Rollback();
                throw ex;
            }
            finally
            {
                produtoUnitOfWork.Dispose();
            }
        }
    }
}
