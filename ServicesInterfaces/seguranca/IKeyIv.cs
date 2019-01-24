using ServicesInterfaces.cadastro;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServicesInterfaces.seguranca
{
    public interface IKeyIv
    {
        string Chave { get; set; }
        string VetorInicializacao { get; set; }
        string userName { get; set; }
    }
}
