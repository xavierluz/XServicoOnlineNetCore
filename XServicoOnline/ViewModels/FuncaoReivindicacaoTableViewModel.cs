using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XServicoOnline.Models;

namespace XServicoOnline.ViewModels
{
    public class FuncaoReivindicacaoTableViewModel
    {
        private FuncaoReivindicacaoTableViewModel(FuncaoReivindicacao  funcaoReivindicacao)
        {
            this.Id = funcaoReivindicacao.Id;
            this.Reivindicacao = funcaoReivindicacao.ClaimType;
            this.Valor  = funcaoReivindicacao.ClaimValue;
            this.FuncaoId = funcaoReivindicacao.RoleId;
        }
        public static FuncaoReivindicacaoTableViewModel GetInstance(FuncaoReivindicacao funcaoReivindicacao)
        {
            return new FuncaoReivindicacaoTableViewModel(funcaoReivindicacao);
        }
        public int Id { get; set; }
        public String Reivindicacao { get; set; }
        public String Valor { get; set; }
        public String FuncaoId { get; set; }

        public static List<FuncaoReivindicacaoTableViewModel> Ordenar(string ordenacao, string ordenacaoAscDesc, IList<FuncaoReivindicacaoTableViewModel> tableFuncaoReivindicacao)
        {
            List<FuncaoReivindicacaoTableViewModel> retorno = new List<FuncaoReivindicacaoTableViewModel>();

            try
            {
                // Sorting
                switch (ordenacao)
                {
                    case "0":
                        retorno = ordenacaoAscDesc.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? tableFuncaoReivindicacao.OrderByDescending(p => p.Reivindicacao).ToList() : tableFuncaoReivindicacao.OrderBy(p => p.Reivindicacao).ToList();
                        break;
                    case "1":
                        retorno = ordenacaoAscDesc.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? tableFuncaoReivindicacao.OrderByDescending(p => p.Valor).ToList() : tableFuncaoReivindicacao.OrderBy(p => p.Valor).ToList();
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
