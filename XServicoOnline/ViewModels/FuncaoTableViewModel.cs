using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XServicoOnline.Models;

namespace XServicoOnline.ViewModels
{
    public class FuncaoTableViewModel
    {
       
        public static FuncaoTableViewModel GetInstance(Funcao funcao)
        {
            return new FuncaoTableViewModel(funcao);
        }
        private FuncaoTableViewModel(Funcao funcao)
        {
            this.Id = funcao.Id;
            this.Name = funcao.Name;
        }
        public string Id { get; set; }
        public String Name { get; set; }

        public static List<FuncaoTableViewModel> Ordenar(string ordenacao, string ordenacaoAscDesc, IList<FuncaoTableViewModel> tableFuncao)
        {
            List<FuncaoTableViewModel> retorno = new List<FuncaoTableViewModel>();

            try
            {
                // Sorting
                switch (ordenacao)
                {
                    case "0":
                        retorno = ordenacaoAscDesc.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? tableFuncao.OrderByDescending(p => p.Name).ToList() : tableFuncao.OrderBy(p => p.Name).ToList();
                        break;
                    case "1":
                        retorno = ordenacaoAscDesc.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? tableFuncao.OrderByDescending(p => p.Id).ToList() : tableFuncao.OrderBy(p => p.Id).ToList();
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
