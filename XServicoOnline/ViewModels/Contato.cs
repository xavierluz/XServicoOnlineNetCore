using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace XServicoOnline.ViewModels
{
    public class Contato
    {
        public int ContatoId { get; set; }
        public String Nome { get; set; }
        public String Endereco { get; set; }
        public String Cidade { get; set; }
        public String Estado { get; set; }
        public String Cep { get; set; }
        [DataType(DataType.EmailAddress)]
        public String Email { get; set; }
    }
}
