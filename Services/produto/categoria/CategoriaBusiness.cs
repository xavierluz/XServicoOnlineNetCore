using Microsoft.EntityFrameworkCore;
using Services.bases;
using Services.modelo.produto;
using Services.produto.contexto;
using Services.produto.repositorio;
using ServicesInterfaces.banco;
using ServicesInterfaces.produto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.produto.categoria
{
    internal class CategoriaBusiness : CategoriaAbstract
    {
        private ProdutoUnitOfWork produtoUnitOfWork = null;
        private BaseProdutoRepositorio<Categoria> categoriaRepositorio = null;
        private DbContextOptionsBuilder<ProdutoContexto> optionsBuilder;
        private CategoriaBusiness(IsolationLevel isolationLevel) : base(isolationLevel)
        {
            this.dbConnection = PostgreSqlFactory.GetInstance().GetConnection();
            optionsBuilder = new DbContextOptionsBuilder<ProdutoContexto>();
            optionsBuilder.UseNpgsql(this.dbConnection);
            this.produtoUnitOfWork = ProdutoUnitOfWork.GetInstance(optionsBuilder.Options, this.isolationLevel);
            this.categoriaRepositorio = this.produtoUnitOfWork.CategoriaRepositorio;
        }
        public static CategoriaBusiness GetInstancia (IsolationLevel isolationLevel) 
        {
            return new CategoriaBusiness(isolationLevel);
        }

        public override async Task<ICategoria> Atualizar(ICategoria categoria)
        {
            await produtoUnitOfWork.CreateTransacao();
            try
            {
                Categoria _categoria = Categoria.GetInstance().GetCategoria(categoria);
                await this.categoriaRepositorio.AtualizarAsync(_categoria);
                await produtoUnitOfWork.SalvarAsync();
                produtoUnitOfWork.Commit();

                return _categoria.GetCategoria();
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

        public override async Task<ICategoria> ConsultarPorId(int categoriaId)
        {
            await produtoUnitOfWork.CreateTransacao();
            try
            {
                IQueryable<Categoria> query = (from q in this.categoriaRepositorio.produtoContexto.Categorias
                                               where q.Id == categoriaId
                                                 && q.Ativo == true
                                               select q);
                Categoria categoria = await this.categoriaRepositorio.GetAsync(query);
                produtoUnitOfWork.Commit();
                return categoria.GetCategoria();
            }
            catch(Exception ex)
            {
                produtoUnitOfWork.Rollback();
                throw ex;
            }
            finally
            {
                produtoUnitOfWork.Dispose();
            }
        }

        public override async Task<IList<ICategoria>> GetCategorias()
        {
            IQueryable<Categoria> query = (from q in this.categoriaRepositorio.produtoContexto.Categorias
                                           where q.Ativo == true
                                           select q);
            List<Categoria> categorias = await this.categoriaRepositorio.GetsAsync(query);
            return categorias.ConvertAll(new Converter<Categoria, ICategoria>(Categoria.GetInstance().GetCategoria));
        }

        public override async Task<IList<ICategoria>> getCategoriasParaMontarGrid(int paginaIndex, string filtro, int registroPorPagina)
        {
            await produtoUnitOfWork.CreateTransacao();
            try
            {
                IQueryable<Categoria> query;
                if (paginaIndex < 0)
                    paginaIndex = 0;
                if (!string.IsNullOrEmpty(filtro) &&   !string.IsNullOrWhiteSpace(filtro))
                {
                    query = (from q in this.categoriaRepositorio.produtoContexto.Categorias
                             where q.Nome.ToUpper().Contains(filtro.ToUpper())
                               && q.Descricao.ToUpper().Contains(filtro.ToUpper())
                               && q.Ativo.ToString().Contains(filtro)
                             select q);
                    this.totalRegistrosRetorno = await this.categoriaRepositorio.GetCountAsync(query);
                    query = query.Skip(paginaIndex).Take(registroPorPagina);
                }
                else
                {
                    query = (from q in this.categoriaRepositorio.produtoContexto.Categorias
                             select q);
                    this.totalRegistrosRetorno = await this.categoriaRepositorio.GetCountAsync(query);
                    query = query.Skip(paginaIndex).Take(registroPorPagina);
                }
                List<Categoria> categorias = await this.categoriaRepositorio.GetsAsync(query);
                produtoUnitOfWork.Commit();
                return categorias.ConvertAll(new Converter<Categoria, ICategoria>(categoria => categoria.GetCategoria()));

            }
            catch(Exception ex)
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
            IQueryable<Categoria> query = (from q in this.categoriaRepositorio.produtoContexto.Categorias
                                           where q.Ativo == true
                                           select q);
            return await this.categoriaRepositorio.GetCountAsync(query);
        }

        public override async Task<ICategoria> Incluir(ICategoria categoria)
        {
            await produtoUnitOfWork.CreateTransacao();
            try
            {
                Categoria _categoria = Categoria.GetInstance().GetCategoria(categoria);
                _categoria.Ativo = true;
                await this.categoriaRepositorio.AdicionarAsync(_categoria);
                await produtoUnitOfWork.SalvarAsync();
                produtoUnitOfWork.Commit();

                return _categoria.GetCategoria();
            }

            catch(Exception ex)
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
