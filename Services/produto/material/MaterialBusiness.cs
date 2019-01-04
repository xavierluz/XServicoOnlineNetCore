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

namespace Services.produto.material
{
    internal class MaterialBusiness : MaterialAbstract
    {
        private ProdutoUnitOfWork produtoUnitOfWork = null;
        private BaseProdutoRepositorio<Material> materialRepositorio = null;
        private DbContextOptionsBuilder<ProdutoContexto> optionsBuilder;
        private CategoriaAbstract categoriaAbstract = null;
        private ClassificacoAbstract classificacoAbstract = null;
        private MaterialBusiness(IsolationLevel isolationLevel) : base(isolationLevel)
        {
            this.dbConnection = PostgreSqlFactory.GetInstance().GetConnection();
            optionsBuilder = new DbContextOptionsBuilder<ProdutoContexto>();
            optionsBuilder.UseNpgsql(this.dbConnection);

            this.produtoUnitOfWork = ProdutoUnitOfWork.GetInstance(optionsBuilder.Options, this.isolationLevel);
            this.materialRepositorio = this.produtoUnitOfWork.MaterialRepositorio;
        }

        public static MaterialBusiness GetInstancia(IsolationLevel isolationLevel)
        {
            return new MaterialBusiness(isolationLevel);
        }

        public override async Task<IMaterial> Atualizar(IMaterial material)
        {
            await produtoUnitOfWork.CreateTransacao();
            try
            {
                Material _material = Material.GetInstance().GetMaterial(material);
                await this.materialRepositorio.AtualizarAsync(_material);
                await produtoUnitOfWork.SalvarAsync();
                produtoUnitOfWork.Commit();

                return _material.GetMaterial();
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

        public override async Task<IMaterial> ConsultarPorId(int materialId)
        {
            await produtoUnitOfWork.CreateTransacao();
            try
            {
                IQueryable<Material> query = (from q in this.materialRepositorio.produtoContexto.Materials
                                               where q.Id == materialId
                                                 && q.Ativo == true
                                               select q);
                Material material = await this.materialRepositorio.GetAsync(query);
                produtoUnitOfWork.Commit();
                return material.GetMaterial();
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

        public override async Task<IList<IMaterial>> GetMateriais()
        {
            IQueryable<Material> query = (from q in this.materialRepositorio.produtoContexto.Materials
                                           where q.Ativo == true
                                           select q);
            List<Material> materials = await this.materialRepositorio.GetsAsync(query);
            return materials.ConvertAll(new Converter<Material, IMaterial>(ma => ma.GetMaterial()));
        }

        public override async Task<IList<IMaterial>> GetMateriaisParaMontarGrid(int paginaIndex, string filtro, int registroPorPagina)
        {
            await produtoUnitOfWork.CreateTransacao();
            try
            {
                IQueryable<Material> query;
                if (paginaIndex < 0)
                    paginaIndex = 0;
                if (!string.IsNullOrEmpty(filtro) && !string.IsNullOrWhiteSpace(filtro))
                {
                    query = (from q in this.materialRepositorio.produtoContexto.Materials
                             where q.Nome.ToUpper().Contains(filtro.ToUpper())
                               && q.Descricao.ToUpper().Contains(filtro.ToUpper())
                               && q.Ativo.ToString().Contains(filtro)
                             select q);
                    this.totalRegistrosRetorno = await this.materialRepositorio.GetCountAsync(query);
                    query = query.Skip(paginaIndex).Take(registroPorPagina);
                }
                else
                {
                    query = (from q in this.materialRepositorio.produtoContexto.Materials
                             select q);
                    this.totalRegistrosRetorno = await this.materialRepositorio.GetCountAsync(query);
                    query = query.Skip(paginaIndex).Take(registroPorPagina);
                }
                List<Material> materiais = await this.materialRepositorio.GetsAsync(query);
                produtoUnitOfWork.Commit();
                return materiais.ConvertAll(new Converter<Material, IMaterial>(mat => mat.GetMaterial()));

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

        public override async Task<IMaterialStatus> GetMaterialStatus()
        {
            IMaterialStatus materialStatus = MaterialStatus.GetInstance();

            materialStatus.QuantidadeDeMaterial = await GetQuantidade();
            this.categoriaAbstract = ProdutoFactory.GetInstance().CreateCategoria(this.isolationLevel);
            this.classificacoAbstract = ProdutoFactory.GetInstance().CreateClassificao(this.isolationLevel);
            materialStatus.QuantidadeDeCategoria = await this.categoriaAbstract.GetQuantidade();
            materialStatus.QuantidadeDeClassificacao = await this.classificacoAbstract.GetQuantidade();

            return materialStatus;

        }

        public override async Task<Int32> GetQuantidade()
        {
            IQueryable<Material> query = (from q in this.materialRepositorio.produtoContexto.Materials
                                          where q.Ativo == true
                                          select q);
            var quantidade = await this.materialRepositorio.GetCountAsync(query);
            return quantidade;
            //public abstract Task<IList<Int32>> GetQuantidade();
        }
       
        public override async Task<IMaterial> Incluir(IMaterial material)
        {
            await produtoUnitOfWork.CreateTransacao();
            try
            {
                Material _material = Material.GetInstance().GetMaterial(material);
                _material.Ativo = true;
                await this.materialRepositorio.AdicionarAsync(_material);
                await produtoUnitOfWork.SalvarAsync();
                produtoUnitOfWork.Commit();

                return _material.GetMaterial();
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
