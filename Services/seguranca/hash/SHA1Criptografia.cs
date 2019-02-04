using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Services.seguranca.hash
{
    internal class SHA1Criptografia: CriptografiaStrategy
    {
        private string _conteudoParaCriptografar = string.Empty;
        private string _conteudoCriptografado = string.Empty;
        private SHA1Criptografia(string conteudoParaCriptografar)
        {
            this._conteudoParaCriptografar = conteudoParaCriptografar;
        }
        internal static SHA1Criptografia Create(string conteudoParaCriptografar)
        {
            return new SHA1Criptografia(conteudoParaCriptografar);
        }

        internal override async Task<string> Get()
        {
            return await getCriptografia();
        }
        internal async override Task Create()
        {
            SHA1 sha1 = SHA1.Create();

            byte[] hashData = sha1.ComputeHash(Encoding.Default.GetBytes(this._conteudoParaCriptografar));

            StringBuilder builderSha1 = new StringBuilder();

            for (int i = 0; i < hashData.Length; i++)
            {
                builderSha1.Append(hashData[i].ToString());
            }
            this._conteudoCriptografado = await Task.Run(() => builderSha1.ToString());
        }

        internal async override Task<bool> ValidarHashData(string hash1, string hash2)
        {

            if (string.Compare(hash1, hash2) == 0)
            {
                return await Task.Run(() => true);
            }
            else
            {
                return await Task.Run(() => false);
            }
        }
        private Task<string> getCriptografia()
        {
            var criptoGrafias = Task.Run(() =>
            {
                return this._conteudoCriptografado;
            });

            return criptoGrafias;
        }

        internal override async Task CreateToken()
        {
            this._conteudoParaCriptografar = string.Format("{0}{1}{2}{3}", DateTime.Now, DateTime.Now.Millisecond, new Random().Next().ToString(), Guid.NewGuid().ToString());
            await this.Create();
        }

       
        internal override void AdicionarConteudo(string conteudoParaCriptografar)
        {
            this._conteudoParaCriptografar = conteudoParaCriptografar;
        }
    }
}
