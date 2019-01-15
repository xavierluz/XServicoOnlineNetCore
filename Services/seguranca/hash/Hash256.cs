using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Services.seguranca.hash
{
    internal class Hash256 : IHash
    {
        private byte[] salt = new byte[128 / 8];
        private string conteudo = string.Empty;
        private Hash256(string conteudo) => this.conteudo = conteudo;
        public static Hash256 Getinstance(string conteudo) => new Hash256(conteudo);
        public string Create()
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: conteudo,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA1,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));
            return hashed;
        }
    }
}
