using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Services.modelo.movimento;
using Services.movimento.repositorio;
using ServicesInterfaces.movimento;

namespace Services.movimento.entrada
{
    internal class EntradaBusiness : MovimentoAbstract
    {
        private MovimentoUnitOfWork movimentoUnitOfWork = null;
        private MovimentoRepositorio movimentoRepositorio = null;

        private EntradaBusiness(IsolationLevel isolationLevel) : base(isolationLevel)
        {
            this.movimentoUnitOfWork = MovimentoUnitOfWork.GetInstance(base.isolationLevel);
            this.movimentoRepositorio = this.movimentoUnitOfWork.GetMovimentoRepositorio();
        }
     
        internal static MovimentoAbstract GetInstance(IsolationLevel isolationLevel) 
        {
            return new EntradaBusiness(isolationLevel);
        }

        public override async Task<IMovimento> Atualizar(IMovimento movimento)
        {
            await this.movimentoUnitOfWork.CreateTransacao();
            try
            {
                Movimento _movimento = Movimento.GetMovimento(movimento);

                await this.movimentoRepositorio.AtualizarAsync(_movimento);
                await movimentoUnitOfWork.SalvarAsync();

                return _movimento.GetMovimento();
            }

            catch (Exception ex)
            {
                this.movimentoUnitOfWork.Rollback();
                throw ex;
            }
            finally
            {
                this.movimentoUnitOfWork.Dispose();
                this.movimentoRepositorio = null;
            }
        }

        public override Task Commit()
        {
            throw new NotImplementedException();
        }

        public override Task<IMovimento> ConsultarPorId(Guid movimentoId)
        {
            throw new NotImplementedException();
        }

        public override Task<IMovimento> GetMovimento(string userName)
        {
            throw new NotImplementedException();
        }

        public override Task<IList<IMovimento>> GetMovimentos()
        {
            throw new NotImplementedException();
        }

        public override async Task<IList<IMovimento>> getMovimentosParaMontarGrid(int paginaIndex, string filtro, int registroPorPagina)
        {
            await this.movimentoUnitOfWork.CreateTransacao();
            try
            {
                IQueryable<Movimento> query;
                if (paginaIndex < 0)
                    paginaIndex = 0;
                if (!string.IsNullOrEmpty(filtro) && !string.IsNullOrWhiteSpace(filtro))
                {
                    query = (from q in this.movimentoRepositorio.movimentoContexto.Movimentos
                             where q.DataMovimento.ToString().ToUpper().Contains(filtro.ToUpper())
                               && q.Usuario.Nome.ToString().ToUpper().Contains(filtro.ToUpper())
                               && q.Almoxarifado.Descricao.ToUpper().Contains(filtro.ToUpper())
                               && q.TipoMovimento.Tipo.ToUpper().Contains(filtro.ToUpper())
                               && q.Ativo.ToString().Contains(filtro)
                             select q);
                    this.totalRegistrosRetorno = await this.movimentoRepositorio.GetCountAsync(query);
                    query = query.Skip(paginaIndex).Take(registroPorPagina);
                }
                else
                {
                    query = (from q in this.movimentoRepositorio.movimentoContexto.Movimentos
                             select q);
                    this.totalRegistrosRetorno = await this.movimentoRepositorio.GetCountAsync(query);
                    query = query.Skip(paginaIndex).Take(registroPorPagina);
                }
                List<Movimento> movimentos = await this.movimentoRepositorio.GetsAsync(query);
                this.movimentoUnitOfWork.Commit();
                return movimentos.ConvertAll(new Converter<Movimento, IMovimento>(emp => emp.GetMovimento()));

            }
            catch (Exception ex)
            {
                this.movimentoUnitOfWork.Rollback();
                throw ex;
            }
            finally
            {
                this.movimentoUnitOfWork.Dispose();
            }
        }

        public override Task<int> GetQuantidade()
        {
            throw new NotImplementedException();
        }

        public override async Task<IMovimento> Incluir(IMovimento movimento)
        {
            await this.movimentoUnitOfWork.CreateTransacao();
            try
            {
                Movimento _movimento = Movimento.GetMovimento(movimento);

                await this.movimentoRepositorio.AdicionarAsync(_movimento);
                await movimentoUnitOfWork.SalvarAsync();

                return _movimento.GetMovimento();
            }

            catch (Exception ex)
            {
                this.movimentoUnitOfWork.Rollback();
                throw ex;
            }
            finally
            {
                this.movimentoUnitOfWork.Dispose();
                this.movimentoRepositorio = null;
            }
        }

        public override Task Rollback()
        {
            throw new NotImplementedException();
        }
    }
}
