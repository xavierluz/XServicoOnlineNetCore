using ServicesInterfaces.produto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XServicoOnline.ViewModels
{
    public class MaterialTableViewModel
    {
        private MaterialTableViewModel(IMaterial  material)
        {
            this.Ativo = material.Ativo;
            this.Nome = material.Nome;
            this.Descricao = material.Descricao;
            this.Id = material.Id;
        }
        public static MaterialTableViewModel GetInstance(IMaterial material)
        {
            return new MaterialTableViewModel(material);
        }
        public int Id { get; set; }
        public String Nome { get; set; }
        public String Descricao { get; set; }
        public bool Ativo { get; set; }

        public static List<MaterialTableViewModel> Ordenar(string ordenacao, string ordenacaoAscDesc, IList<MaterialTableViewModel>  materialTable)
        {
            List<MaterialTableViewModel> retorno = new List<MaterialTableViewModel>();

            try
            {
                // Sorting
                switch (ordenacao)
                {
                    case "0":
                        retorno = ordenacaoAscDesc.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? materialTable.OrderByDescending(p => p.Nome).ToList() : materialTable.OrderBy(p => p.Nome).ToList();
                        break;
                    case "1":
                        retorno = ordenacaoAscDesc.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? materialTable.OrderByDescending(p => p.Descricao).ToList() : materialTable.OrderBy(p => p.Descricao).ToList();
                        break;
                    case "2":
                        retorno = ordenacaoAscDesc.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? materialTable.OrderByDescending(p => p.Ativo).ToList() : materialTable.OrderBy(p => p.Ativo).ToList();
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
