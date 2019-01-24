using Services.cadastro.repositorio;
using Services.modelo.cadastro;
using ServicesInterfaces.cadastro;
using ServicesInterfaces.seguranca;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.seguranca
{
    internal class KeyIv : IKeyIv
    {
        private CadastroUnitOfWork cadastroUnitOfWork = null;
        private EmpresaRepositorio empresaRepositorio = null;
        private KeyIv() { }
        private KeyIv(string userName)
        {
            this.userName = userName;
            this.cadastroUnitOfWork = CadastroUnitOfWork.GetInstance(IsolationLevel.RepeatableRead);
            this.empresaRepositorio = this.cadastroUnitOfWork.GetEmpresaRepositorio();

        }
        internal static KeyIv GetInstance(string userName)
        {
            return new KeyIv(userName);
        }
        public string Chave { get; set; }
        public string VetorInicializacao { get; set; }
        public string userName { get; set; }

        internal async Task<IKeyIv> Get()
        {
            await this.cadastroUnitOfWork.CreateTransacao();
            try
            {
                IQueryable<Empresa> query = (from q in this.empresaRepositorio.cadastroContexto.Empresas
                                             join u in this.empresaRepositorio.cadastroContexto.Usuarios on q.Id equals u.EmpresaId
                                             where u.UserName  == this.userName
                                             select q
                                             );

                Empresa empresa = await this.empresaRepositorio.GetAsync(query);
                this.cadastroUnitOfWork.Commit();

                if (empresa == null)
                {
                    throw new Exception("Usuário não pertence a uma empresa cadastrada");
                }

                IKeyIv keyIv = new KeyIv();
                keyIv.userName = this.userName;
                keyIv.VetorInicializacao = empresa.VetorInicializacao;
                keyIv.Chave = empresa.Chave;

                return keyIv;
            }
            catch(Exception ex)
            {
                this.cadastroUnitOfWork.Rollback();
                throw ex;
            }
            finally
            {
                this.cadastroUnitOfWork.Dispose();
            }
        }
       
    }
}
