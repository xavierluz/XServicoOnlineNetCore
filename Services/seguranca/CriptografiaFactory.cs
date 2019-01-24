using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.seguranca
{
    public class CriptografiaFactory
    {
        private CriptografiaStrategy _strategy = null;


        private CriptografiaFactory(CriptografiaStrategy strategy)
        {
            this._strategy = strategy;
        }
        public static CriptografiaFactory Create(CriptografiaStrategy strategy)
        {
            return new CriptografiaFactory(strategy);
        }

        public async Task<string> GetHashData()
        {
            return await this._strategy.GetHashData(); 
        }

        public async Task CreateHashData()
        {
            await _strategy.CreateHashData();
        }
        public Task<bool> ValidarHashData(string hash1, string hash2)
        {
            return _strategy.ValidarHashData(hash1, hash2);
        }

        public async Task<string> GetToken()
        {
            return await _strategy.GetToken();
        }
        public void AdicionarConteudoParaCriptografar(string conteudoParaCriptografar)
        {
            _strategy.AdicionarConteudoParaCriptografar(conteudoParaCriptografar);
        }
    }
}
