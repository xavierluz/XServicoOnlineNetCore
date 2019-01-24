using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.seguranca
{
    public abstract class CriptografiaStrategy
    {
        internal abstract Task<string> GetHashData();
        internal abstract Task CreateHashData();
        internal abstract Task<bool> ValidarHashData(string hash1, string hash2);
        internal abstract Task CreateToken();
        internal abstract Task<string> GetToken();
        internal abstract void AdicionarConteudoParaCriptografar(string conteudoParaCriptografar);
    }
}
