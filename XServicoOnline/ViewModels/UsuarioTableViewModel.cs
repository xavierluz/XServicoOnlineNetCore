using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XServicoOnline.Models;

namespace XServicoOnline.ViewModels
{
    public class UsuarioTableViewModel
    {
        public static UsuarioTableViewModel GetInstance(Usuario usuario)
        {
            return new UsuarioTableViewModel(usuario);
        }
        private UsuarioTableViewModel(Usuario usuario)
        {
            this.Id = usuario.Id;
            this.Nome = usuario.Nome;
            this.UserName = usuario.UserName;
            this.Email = usuario.Email;
        }
        public String Id { get; set; }
        public String Nome { get; set; }
        public String UserName { get; set; }
        public String Email { get; set; }

    }
    internal static class OrdenarUsuario
    {
        public static List<UsuarioTableViewModel> Ordenar(this IList<UsuarioTableViewModel> tableUsuario, string ordenacao, string ordenacaoAscDesc)
        {
            List<UsuarioTableViewModel> retorno = new List<UsuarioTableViewModel>();

            try
            {
                // Sorting
                switch (ordenacao)
                {
                    case "0":
                        retorno = ordenacaoAscDesc.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? tableUsuario.OrderByDescending(p => p.Nome).ToList() : tableUsuario.OrderBy(p => p.Nome).ToList();
                        break;
                    case "1":
                        retorno = ordenacaoAscDesc.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? tableUsuario.OrderByDescending(p => p.UserName).ToList() : tableUsuario.OrderBy(p => p.UserName).ToList();
                        break;
                    case "2":
                        retorno = ordenacaoAscDesc.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? tableUsuario.OrderByDescending(p => p.Email).ToList() : tableUsuario.OrderBy(p => p.Email).ToList();
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
