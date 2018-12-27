using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XServicoOnline.Models
{
    public class Funcao: IdentityRole<string>
    {
        public virtual ICollection<UsuarioFuncao> UsuarioFuncao { get; set; }
        public virtual ICollection<FuncaoReivindicacao> FuncaoReivindicacao { get; set; }
    }
}
