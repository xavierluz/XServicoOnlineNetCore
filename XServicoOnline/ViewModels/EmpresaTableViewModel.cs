using ServicesInterfaces.cadastro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XServicoOnline.ViewModels
{
    public class EmpresaTableViewModel
    {
        private EmpresaTableViewModel(IEmpresa empresa)
        {
            this.Ativo = empresa.Ativo;
            this.RazaoSocial = empresa.RazaoSocial;
            this.NomeFantasia = empresa.NomeFantasia;
            this.CnpjCpf = empresa.CnpjCpf;
            this.Id = empresa.Id;
        }
        public static EmpresaTableViewModel GetInstance(IEmpresa empresa)
        {
            return new EmpresaTableViewModel(empresa);
        }
        public Guid Id { get; set; }
        public String RazaoSocial { get; set; }
        public String NomeFantasia { get; set; }
        public String CnpjCpf { get; set; }
        public bool Ativo { get; set; }

        public static List<EmpresaTableViewModel> Ordenar(string ordenacao, string ordenacaoAscDesc, IList<EmpresaTableViewModel> tableEmpresa)
        {
            List<EmpresaTableViewModel> retorno = new List<EmpresaTableViewModel>();

            try
            {
                // Sorting
                switch (ordenacao)
                {
                    case "0":
                        retorno = ordenacaoAscDesc.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? tableEmpresa.OrderByDescending(p => p.RazaoSocial).ToList() : tableEmpresa.OrderBy(p => p.RazaoSocial).ToList();
                        break;
                    case "1":
                        retorno = ordenacaoAscDesc.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? tableEmpresa.OrderByDescending(p => p.NomeFantasia).ToList() : tableEmpresa.OrderBy(p => p.NomeFantasia).ToList();
                        break;
                    case "2":
                        retorno = ordenacaoAscDesc.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? tableEmpresa.OrderByDescending(p => p.CnpjCpf).ToList() : tableEmpresa.OrderBy(p => p.CnpjCpf).ToList();
                        break;
                    case "3":
                        retorno = ordenacaoAscDesc.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? tableEmpresa.OrderByDescending(p => p.Ativo).ToList() : tableEmpresa.OrderBy(p => p.Ativo).ToList();
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

            return retorno;
        }
    }
}
