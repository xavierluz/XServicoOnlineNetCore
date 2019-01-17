using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XServicoOnline.WebClasses
{
    public class JsonRetornoInclusaoAtualizacao:IJsonRetorno
    {
        private JsonRetornoInclusaoAtualizacao()
        {
            retorno = new Dictionary<string, string>();
        }
        internal static JsonRetornoInclusaoAtualizacao GetInstance()
        {
            return new JsonRetornoInclusaoAtualizacao();
        }
        internal IJsonRetorno Create(string valor)
        {
            retorno.Add("sucesso", valor);
            return this;
        }
        public Dictionary<string, string> retorno { get; private set; }
        internal void LimparMensagem()
        {
            retorno.Clear();
        }
    }
}
