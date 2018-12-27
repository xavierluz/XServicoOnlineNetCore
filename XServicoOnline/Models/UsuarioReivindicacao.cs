using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XServicoOnline.Models
{
    public class UsuarioReivindicacao : IdentityUserClaim<string>
    {
        public virtual Usuario Usuario { get; set; }
    }
}
