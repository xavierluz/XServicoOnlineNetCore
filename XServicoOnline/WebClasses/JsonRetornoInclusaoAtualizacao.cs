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
        internal static JsonRetornoInclusaoAtualizacao Create(string valor)
        {
            var JsonRetorno = new JsonRetornoInclusaoAtualizacao();
            JsonRetorno.retorno.Add("sucesso", valor);
            return JsonRetorno;
        }
        public Dictionary<string, string> retorno { get; private set; }

    }
}
