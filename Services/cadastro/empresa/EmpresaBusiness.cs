using Services.cadastro.repositorio;
using Services.modelo.cadastro;
using Services.seguranca;
using Services.seguranca.hash;
using ServicesInterfaces.cadastro;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.cadastro.empresa
{
    internal class EmpresaBusiness : EmpresaAbstract
    {
        private static string SENHA_PADRAO = "@S2rvico0n@line";
        private CadastroUnitOfWork cadastroUnitOfWork = null;
        private EmpresaRepositorio empresaRepositorio = null;
        private UsuarioRepositorio  usuarioRepositorio = null;
        private CriptografiaFactory criptografiaFactory = null;
        
        private EmpresaBusiness(IsolationLevel isolationLevel) : base(isolationLevel)
        {
            this.cadastroUnitOfWork = CadastroUnitOfWork.GetInstance(base.isolationLevel);
            this.empresaRepositorio = this.cadastroUnitOfWork.GetEmpresaRepositorio();
        }

        internal static  EmpresaBusiness GetInstance(IsolationLevel isolationLevel)
        {
            return new EmpresaBusiness(isolationLevel);
        }
        public override async Task<IEmpresa> Atualizar(IEmpresa empresa)
        {
            await cadastroUnitOfWork.CreateTransacao();
            try
            {
                Empresa _empresa = Empresa.GetInstance().GetEmpresa(empresa);
                await this.empresaRepositorio.AtualizarAsync(_empresa);
                cadastroUnitOfWork.Commit();

                return _empresa.GetEmpresa();
            }

            catch (Exception ex)
            {
                cadastroUnitOfWork.Rollback();
                throw ex;
            }
            finally
            {
                cadastroUnitOfWork.Dispose();
            }
        }

        public override async Task Commit()
        {
            if (cadastroUnitOfWork != null)
            {
                await Task.Run(() => cadastroUnitOfWork.Commit());
                cadastroUnitOfWork.Dispose();
            }
        }

        public override async Task<IEmpresa> ConsultarPorId(Guid empresaId)
        {
            await cadastroUnitOfWork.CreateTransacao();
            try
            {
                IQueryable<Empresa> query = (from q in this.empresaRepositorio.cadastroContexto.Empresas
                                               where q.Id == empresaId
                                                 && q.Ativo == true
                                               select q);
                Empresa empresa = await this.empresaRepositorio.GetAsync(query);
                cadastroUnitOfWork.Commit();
                return empresa.GetEmpresa();
            }
            catch (Exception ex)
            {
                cadastroUnitOfWork.Rollback();
                throw ex;
            }
            finally
            {
                cadastroUnitOfWork.Dispose();
            }
        }

        public override async Task<IList<IEmpresa>> GetEmpresas()
        {
            IQueryable<Empresa> query = (from q in this.empresaRepositorio.cadastroContexto.Empresas
                                           where q.Ativo == true
                                           select q);
            List<Empresa> empresas = await this.empresaRepositorio.GetsAsync(query);
            return empresas.ConvertAll(new Converter<Empresa, IEmpresa>(emp => emp.GetEmpresa()));
        }

        public override async Task<IList<IEmpresa>> getEmpresasParaMontarGrid(int paginaIndex, string filtro, int registroPorPagina)
        {
            await cadastroUnitOfWork.CreateTransacao();
            try
            {
                IQueryable<Empresa> query;
                if (paginaIndex < 0)
                    paginaIndex = 0;
                if (!string.IsNullOrEmpty(filtro) && !string.IsNullOrWhiteSpace(filtro))
                {
                    query = (from q in this.empresaRepositorio.cadastroContexto.Empresas
                             where q.RazaoSocial.ToUpper().Contains(filtro.ToUpper())
                               && q.NomeFantasia.ToUpper().Contains(filtro.ToUpper())
                               && q.CnpjCpf.ToUpper().Contains(filtro.ToUpper())
                               && q.Ativo.ToString().Contains(filtro)
                             select q);
                    this.totalRegistrosRetorno = await this.empresaRepositorio.GetCountAsync(query);
                    query = query.Skip(paginaIndex).Take(registroPorPagina);
                }
                else
                {
                    query = (from q in this.empresaRepositorio.cadastroContexto.Empresas
                             select q);
                    this.totalRegistrosRetorno = await this.empresaRepositorio.GetCountAsync(query);
                    query = query.Skip(paginaIndex).Take(registroPorPagina);
                }
                List<Empresa> empresas = await this.empresaRepositorio.GetsAsync(query);
                cadastroUnitOfWork.Commit();
                return empresas.ConvertAll(new Converter<Empresa, IEmpresa>(emp => emp.GetEmpresa()));

            }
            catch (Exception ex)
            {
                cadastroUnitOfWork.Rollback();
                throw ex;
            }
            finally
            {
                cadastroUnitOfWork.Dispose();
            }
        }

        public override async Task<int> GetQuantidade()
        {
            IQueryable<Empresa> query = (from q in this.empresaRepositorio.cadastroContexto.Empresas
                                           where q.Ativo == true
                                           select q);
            return await this.empresaRepositorio.GetCountAsync(query);
        }

        public async override Task<string> GetSenhaPadraoDoUsuarioEmpresa()
        {
            IHash hash = Hash256.Getinstance(SENHA_PADRAO);
            return await Task.Run(() => hash.Create());
        }

        public override async Task<IEmpresa> Incluir(IEmpresa empresa)
        {
            await cadastroUnitOfWork.CreateTransacao();
            try
            {
                Empresa _empresa = Empresa.GetInstance().GetEmpresa(empresa);
                _empresa.Ativo = true;
                string criptografar = string.Format("{0}{1}", _empresa.RazaoSocial, _empresa.CnpjCpf);
                criptografiaFactory = CriptografiaFactory.Create(SHA1Criptografia.Create(criptografar));
                await criptografiaFactory.CreateHashData();
                _empresa.Chave = await criptografiaFactory.GetToken();

                await this.empresaRepositorio.AdicionarAsync(_empresa);
                await cadastroUnitOfWork.SalvarAsync();

                return _empresa.GetEmpresa();
            }

            catch (Exception ex)
            {
                cadastroUnitOfWork.Rollback();
                throw ex;
            }
            
        }

        public override async Task Rollback()
        {
            if (cadastroUnitOfWork != null)
            {
                await Task.Run(() => cadastroUnitOfWork.Rollback());
                cadastroUnitOfWork.Dispose();
            }
        }
    }
}
