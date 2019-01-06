using System;
using System.Collections.Generic;
using System.Text;

namespace ServicesInterfaces.cadastro
{
    public interface IEmpresa
    {
        Guid Id { get; set; }
        String Chave { get; set; }
        String CnpjCpf { get; set; }
        String RazaoSocial { get; set; }
        String NomeFantasia { get; set; }
        String Logradouro { get; set; }
        String Cep { get; set; }
        String Bairro { get; set; }
        String Cidade { get; set; }
        String Site { get; set; }
        String Telefone { get; set; }
        String WhatsApp { get; set; }
        bool Ativo { get; set; }
        ICollection<IAlmoxarifado> IAlmoxarifados { get; set; } 
    }
}
