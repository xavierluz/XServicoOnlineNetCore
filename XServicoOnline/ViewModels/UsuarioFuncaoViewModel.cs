using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XServicoOnline.Models;

namespace XServicoOnline.ViewModels
{
    public class UsuarioFuncaoViewModel
    {
        public UsuarioFuncaoViewModel()
        {
            this.Funcoes = new List<Funcao>();
            this.FuncoesSelecionadas = new List<Funcao>();
        }
        public Usuario Usuario { get; set; }
        public ICollection<Funcao> Funcoes { get; set; }
        public ICollection<Funcao> FuncoesSelecionadas { get; set; }
    }
}
