using ServicesInterfaces.produto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XServicoOnline.ViewModels
{
    public class CategoriaTableViewModel
    {
        private CategoriaTableViewModel(ICategoria categoria) {
            this.Ativo = categoria.Ativo;
            this.Nome = categoria.Nome;
            this.Descricao = categoria.Descricao;
            this.Id = categoria.Id;
        }
        public static CategoriaTableViewModel GetInstance(ICategoria categoria)
        {
            return new CategoriaTableViewModel(categoria);
        }
        public int Id { get; set; }
        public String Nome { get; set; }
        public String Descricao { get; set; }
        public bool Ativo { get; set; }

        public static List<CategoriaTableViewModel> Ordenar(string ordenacao, string ordenacaoAscDesc, IList<CategoriaTableViewModel> tableCategoria)
        {
            List<CategoriaTableViewModel> retorno = new List<CategoriaTableViewModel>();

            try
            {
                // Sorting
                switch (ordenacao)
                {
                    case "0":
                        retorno = ordenacaoAscDesc.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? tableCategoria.OrderByDescending(p => p.Nome).ToList() : tableCategoria.OrderBy(p => p.Nome).ToList();
                        break;
                    case "1":
                        retorno = ordenacaoAscDesc.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? tableCategoria.OrderByDescending(p => p.Descricao).ToList() : tableCategoria.OrderBy(p => p.Descricao).ToList();
                        break;
                    case "2":
                        retorno = ordenacaoAscDesc.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? tableCategoria.OrderByDescending(p => p.Ativo).ToList() : tableCategoria.OrderBy(p => p.Ativo).ToList();
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
