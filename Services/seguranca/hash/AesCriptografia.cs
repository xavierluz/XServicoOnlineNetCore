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
        private string _conteudo = string.Empty;
        private byte[] _conteudoRetorno = null;
        private byte[] Key = null;
        private byte[] Iv = null;
        private IEmpresa empresa;
        private IUsuario usuario;
        private bool descriptografia = false;
        private AesCriptografia(IEmpresa empresa)
        {
            this.descriptografia = false;
            this.empresa = empresa;
        }
        private AesCriptografia(IUsuario usuario)
        {
            this.descriptografia = false;
            this.usuario = usuario;
        }
        private AesCriptografia(IEmpresa empresa, bool descriptografia)
        {
            this.descriptografia = descriptografia;
            this.empresa = empresa;
        }
        private AesCriptografia(IUsuario usuario, bool descriptografia)
        {
            this.descriptografia = descriptografia;
            this.usuario = usuario;
        }
        internal static AesCriptografia CreateCriptografiaEmpresa(IEmpresa empresa)
        {
            return new AesCriptografia(empresa);
        }
        internal static AesCriptografia CreateCriptografiaUsuario(IUsuario usuario)
        {
            return new AesCriptografia(usuario);
        }
        internal static AesCriptografia CreateDesCriptografiaEmpresa(IEmpresa empresa)
        {
            return new AesCriptografia(empresa,true);
        }
        internal static AesCriptografia CreateDesCriptografiaUsuario(IUsuario usuario)
        {
            return new AesCriptografia(usuario, true);
        }
        internal override async Task<string> Get()
        {
            if (this._conteudoRetorno ==null || this._conteudoRetorno.Length < 1)
                return await Task.Run(() => _conteudo);

            return await getCriptografia();
        }
        

        internal override async Task Create()
        {
            using (Aes aes = Aes.Create())
            {
                SetKeyIv();
                if (this.descriptografia)
                {
                    var bytes = await getBytesBase64(this._conteudo);
                    this._conteudo = DecryptStringParaBytesAes(bytes, this.Key, this.Iv);
                }
                else
                {
                    byte[] ecriptado = EncryptStringEmBytesAes(this._conteudo, this.Key, this.Iv);
                    this._conteudoRetorno = await Task.Run(() => ecriptado);
                }
            }
        }

        internal override async Task CreateToken()
        {
            this._conteudo = string.Format("{0}{1}{2}{3}", DateTime.Now, DateTime.Now.Millisecond, new Random().Next().ToString(), Guid.NewGuid().ToString());
            await this.Create();
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
                return Convert.ToBase64String(this._conteudoRetorno);
            });

            return await criptoGrafias;
        }
        private async Task<byte[]> getBytesBase64(string conteudoBase64)
        {
            var bytesBase = Task.Run(() =>
            {
                return Convert.FromBase64String(conteudoBase64);
            });

            return await bytesBase;
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
                aesAlg.Mode = CipherMode.CBC;
                aesAlg.Padding = PaddingMode.PKCS7;
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
                aesAlg.Mode = CipherMode.CBC;
                aesAlg.Padding = PaddingMode.PKCS7;
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
                this.Key = Convert.FromBase64String(this.empresa.Chave);
                this.Iv = Encoding.UTF8.GetBytes(this.empresa.VetorInicializacao);
            }
        }

        internal override void AdicionarConteudo(string conteudo)
        {
            this._conteudo = conteudo;
        }
    }
}
