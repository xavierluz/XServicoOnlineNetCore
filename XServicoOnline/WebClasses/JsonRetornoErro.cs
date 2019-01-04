using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XServicoOnline.WebClasses
{
    public class JsonRetornoErro : IJsonRetorno
    {
        private JsonRetornoErro()
        {
            retorno = new Dictionary<string, string>();
        }
        internal static JsonRetornoErro Create(string valor)
        {
            var JsonRetorno = new JsonRetornoErro();
            JsonRetorno.retorno.Add("erro", valor);
            return JsonRetorno;
        }
        public Dictionary<string, string> retorno
        {
            get;private set;
        }
    }
}
