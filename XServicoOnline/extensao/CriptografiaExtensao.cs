using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XServicoOnline.Models;

namespace XServicoOnline.extensao
{
    public static class CriptografiaExtensao
    {
        public static async Task CriptografarUsuariosGrid(this List<Usuario> usuarios, string nomeUsuarioLogado)
        {
            foreach (var usuario in usuarios)
            {
                await usuario.CriptografarUsuarioGrid(nomeUsuarioLogado);
            }
        }
        public static async Task CriptografarUsuarios(this List<Usuario> usuarios, string nomeUsuarioLogado)
        {
            foreach (var usuario in usuarios)
            {
                await usuario.CriptografarUsuario(nomeUsuarioLogado);
            }
        }
    }
}
