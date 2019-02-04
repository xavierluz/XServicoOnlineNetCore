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

        public async Task<string> Get()
        {
            return await this._strategy.Get(); 
        }

        public async Task Create()
        {
            await _strategy.Create();
        }
        public Task<bool> ValidarHashData(string hash1, string hash2)
        {
            return _strategy.ValidarHashData(hash1, hash2);
        }

        public async Task<string> GetToken()
        {
            return await _strategy.Get();
        }
        public void AdicionarConteudo(string conteudo)
        {
            _strategy.AdicionarConteudo(conteudo);
        }
    }
}
