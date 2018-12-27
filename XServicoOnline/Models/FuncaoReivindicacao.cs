using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XServicoOnline.Models
{
    public class FuncaoReivindicacao: IdentityRoleClaim<string>
    {
        public virtual Funcao Funcao { get; set; }
    }
}
