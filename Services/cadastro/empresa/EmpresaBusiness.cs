using Services.cadastro.repositorio;
using Services.modelo.cadastro;
using Services.seguranca;
using Services.seguranca.hash;
using ServicesInterfaces.cadastro;
using ServicesInterfaces.seguranca;
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
        private static string GERAR_IV = "seAes2019empresa";
        private CadastroUnitOfWork cadastroUnitOfWork = null;
        private EmpresaRepositorio empresaRepositorio = null;
        private UsuarioRepositorio  usuarioRepositorio = null;
        private CriptografiaFactory criptografiaFactory = null;
        private string hashKey;
        private string hashIv;

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

        public override async Task<string> GetHashIvDaEmpresa(IEmpresa empresa, IUsuario usuario)
        {

            await cadastroUnitOfWork.CreateTransacao();

            IQueryable<Empresa> query = (from q in this.empresaRepositorio.cadastroContexto.Empresas
                                         join u in this.usuarioRepositorio.cadastroContexto.Usuarios on q.Id equals u.EmpresaId
                                         where q.Ativo == true
                                            && q.Id == empresa.Id
                                            && q.Email == empresa.Email
                                         select q);
            Empresa _empresa = await this.empresaRepositorio.GetAsync(query);
            cadastroUnitOfWork.Commit();
            return _empresa.VetorInicializacao;
        }

        public override async Task<string> GetHashIvDaEmpresa()
        {
            return await Task.Run(() => hashIv);
        }

        public override async Task<string> GetHashKeyDaEmpresa()
        {
            return await Task.Run(() => hashKey);
        }

        public override async Task<string> GetHashKeyDaEmpresa(IEmpresa empresa, IUsuario usuario)
        {
            await cadastroUnitOfWork.CreateTransacao();
    
            IQueryable<Empresa> query = (from q in this.empresaRepositorio.cadastroContexto.Empresas
                                         join u in this.usuarioRepositorio.cadastroContexto.Usuarios on q.Id equals u.EmpresaId
                                         where q.Ativo == true
                                            && q.Id == empresa.Id 
                                            && q.Email == empresa.Email 
                                         select q);
            Empresa _empresa =  await this.empresaRepositorio.GetAsync(query);
            return _empresa.Chave;
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
            return await Task.Run(() => SENHA_PADRAO);
        }

        public override async Task<IEmpresa> Incluir(IEmpresa empresa)
        {
            await cadastroUnitOfWork.CreateTransacao();
            try
            {
                Empresa _empresa = Empresa.GetInstance().GetEmpresa(empresa);
                _empresa.Ativo = true;
                string criptografarKey = string.Format("{0}{1}",_empresa.CnpjCpf, _empresa.Email);
                this.CreateHashKey(criptografarKey);
                _empresa.Chave = this.hashKey;
                _empresa.VetorInicializacao = GERAR_IV;

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

        private void CreateHashKey(string conteudo)
        {
            IHash hash = Hash256.GetInstance(conteudo);
            this.hashKey = hash.Create();
        }
        private void CreateHashIv(string conteudo)
        {
            IHash hash = Hash256.GetInstance(conteudo);
            this.hashIv = hash.Create();
        }

        public override async Task<IKeyIv> GetKeyIv(string userName)
        {
            KeyIv keyIv = KeyIv.GetInstance(userName);
            return await keyIv.Get();
        }

        public override async Task<IEmpresa> GetEmpresa(string userName)
        {
            await cadastroUnitOfWork.CreateTransacao();
            try
            {
                this.usuarioRepositorio = cadastroUnitOfWork.GetUsuarioRepositorio();
                IQueryable<Empresa> query = (from q in this.empresaRepositorio.cadastroContexto.Empresas
                                             join u in this.usuarioRepositorio.cadastroContexto.Usuarios on q.Id equals u.EmpresaId
                                             where q.Ativo == true
                                                && u.UserName.Equals(userName)
                                             select q);
                Empresa _empresa = await this.empresaRepositorio.GetAsync(query);
                cadastroUnitOfWork.Commit();

                return _empresa.GetEmpresa();
            }catch(Exception ex)
            {
                cadastroUnitOfWork.Rollback();
                throw ex;
            }
            finally
            {
                cadastroUnitOfWork.Dispose();
            }
        }
    }
}
