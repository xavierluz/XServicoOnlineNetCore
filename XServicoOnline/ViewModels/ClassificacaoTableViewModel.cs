using ServicesInterfaces.produto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XServicoOnline.ViewModels
{
    public class ClassificacaoTableViewModel
    {
        private ClassificacaoTableViewModel(IClassificacao classificao)
        {
            this.Ativo = classificao.Ativo;
            this.Nome = classificao.Nome;
            this.Descricao = classificao.Descricao;
            this.Id = classificao.Id;
        }
        public static ClassificacaoTableViewModel GetInstance(IClassificacao  classificacao)
        {
            return new ClassificacaoTableViewModel(classificacao);
        }
        public int Id { get; set; }
        public String Nome { get; set; }
        public String Descricao { get; set; }
        public bool Ativo { get; set; }

        public static List<ClassificacaoTableViewModel> Ordenar(string ordenacao, string ordenacaoAscDesc, IList<ClassificacaoTableViewModel> tableClassificacao)
        {
            List<ClassificacaoTableViewModel> retorno = new List<ClassificacaoTableViewModel>();

            try
            {
                // Sorting
                switch (ordenacao)
                {
                    case "0":
                        retorno = ordenacaoAscDesc.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? tableClassificacao.OrderByDescending(p => p.Nome).ToList() : tableClassificacao.OrderBy(p => p.Nome).ToList();
                        break;
                    case "1":
                        retorno = ordenacaoAscDesc.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? tableClassificacao.OrderByDescending(p => p.Descricao).ToList() : tableClassificacao.OrderBy(p => p.Descricao).ToList();
                        break;
                    case "2":
                        retorno = ordenacaoAscDesc.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? tableClassificacao.OrderByDescending(p => p.Ativo).ToList() : tableClassificacao.OrderBy(p => p.Ativo).ToList();
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
