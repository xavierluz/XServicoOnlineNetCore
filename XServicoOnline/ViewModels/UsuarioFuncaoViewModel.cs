using Microsoft.AspNetCore.Mvc.Rendering;
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
           
        }
        public Usuario Usuario { get; set; }
        public ICollection<String> FuncoesId { get; set; }
        public SelectList Funcoes { get; set; }
        public IList<SelectPureOptions> FuncoesSelecionadas { get; set; }
    }
}
