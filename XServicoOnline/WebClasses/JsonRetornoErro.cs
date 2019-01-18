using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XServicoOnline.WebClasses
{
    public class JsonRetornoErro : IJsonRetorno
    {
        public JsonRetornoErro()
        {
            retorno = new Dictionary<string, string>();
        }
        internal JsonRetornoErro Add(string valor)
        {
            retorno.Add("erro", valor);
            return this;
        }
        public Dictionary<string, string> retorno
        {
            get;private set;
        }
    }
}
