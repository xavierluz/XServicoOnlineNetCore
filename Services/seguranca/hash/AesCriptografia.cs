using ServicesInterfaces.cadastro;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Services.seguranca.hash
{
    internal class AesCriptografia : CriptografiaStrategy
    {
        private string _conteudoParaCriptografar = string.Empty;
        private byte[] _conteudoCriptografado = null;
        private byte[] Key = null;
        private byte[] Iv = null;
        private IEmpresa empresa;
        private IUsuario usuario;
        private AesCriptografia(IEmpresa empresa, string conteudoParaCriptografar)
        {
            this._conteudoParaCriptografar = conteudoParaCriptografar;
            this.empresa = empresa;
        }
        private AesCriptografia(IUsuario usuario, string conteudoParaCriptografar)
        {
            this._conteudoParaCriptografar = conteudoParaCriptografar;
            this.usuario = usuario;
        }
        internal static AesCriptografia CreateKeyEmpresa(IEmpresa empresa,string conteudoParaCriptografar)
        {
            return new AesCriptografia(empresa,conteudoParaCriptografar);
        }
        internal static AesCriptografia CreateKeyUsuario(IUsuario usuario, string conteudoParaCriptografar)
        {
            return new AesCriptografia(usuario, conteudoParaCriptografar);
        }
        internal override Task<string> HashData
        {
            get { return getCriptografia(); }

        }

        internal override async Task CreateHashData()
        {
            using (Aes aes = Aes.Create())
            {
                SetKeyIv();
                byte[] ecriptado = EncryptStringEmBytesAes(this._conteudoParaCriptografar, this.Key, this.Iv);
                this._conteudoCriptografado = await Task.Run(()=> ecriptado);

                string roundtrip = DecryptStringParaBytesAes(this._conteudoCriptografado, this.Key, this.Iv);
            }
        }

        internal override async Task CreateToken()
        {
            this._conteudoParaCriptografar = string.Format("{0}{1}{2}{3}", DateTime.Now, DateTime.Now.Millisecond, new Random().Next().ToString(), Guid.NewGuid().ToString());
            await this.CreateHashData();
        }

        internal override async Task<string> GetToken()
        {
            await CreateToken();
            return await getCriptografia();
        }

        internal override async Task<bool> ValidarHashData(string hash1, string hash2)
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
        private async Task<string> getCriptografia()
        {
            var criptoGrafias = Task.Run(() =>
            {
                return this._conteudoCriptografado.ToString();
            });

            return await criptoGrafias;
        }

        private byte[] EncryptStringEmBytesAes(string conteudoACriptografar, byte[] Key, byte[] IV)
        {
            // Checar argumentos.
            if (conteudoACriptografar == null || conteudoACriptografar.Length <= 0)
                throw new ArgumentNullException("Conteudo a criptografar");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;

            // Create obejto AES
           
            using (Aes aesAlg = Aes.Create())
            {

                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create o objeto encryptor para executar a transformação de fluxo
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create o fluxos usando criptografia
                using (MemoryStream memoryStream   = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                        {
                            //Grave todos dados no fluxo
                            streamWriter.Write(conteudoACriptografar);
                        }
                        encrypted = memoryStream.ToArray();
                    }
                }
            }

            //Retorna os bytes criptografos em memória
            return encrypted;

        }
        private string DecryptStringParaBytesAes(byte[] conteudoCriptografado, byte[] Key, byte[] IV)
        {
            // Checar argumentos.
            if (conteudoCriptografado == null || conteudoCriptografado.Length <= 0)
                throw new ArgumentNullException("conteudo criptografado");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare variavel para retorno
            // texto descriptografado.
            string textoDescriptografado = null;

            // Create obejto AES
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create o objeto encryptor para executar a transformação de fluxo
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create o fluxos usando descriptografia
                using (MemoryStream msDecrypt = new MemoryStream(conteudoCriptografado))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Ler os bytes discriptografados do fluxo
                            // adiciona na variavel 
                            textoDescriptografado = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }

            return textoDescriptografado;

        }

        private void SetKeyIv()
        {
            if(this.usuario != null)
            {

            }
            else
            {
                this.Key = Encoding.UTF8.GetBytes(this.empresa.Chave);
                this.Iv = Encoding.UTF8.GetBytes(this.empresa.VetorInicializacao);
            }
        }
    }
}
