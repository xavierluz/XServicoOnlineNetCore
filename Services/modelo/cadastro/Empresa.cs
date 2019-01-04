using ServicesInterfaces.cadastro;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Services.modelo.cadastro
{
    public class Empresa : IEmpresa
    {
        protected Empresa()
        {
            this.IAlmoxarifados = new List<IAlmoxarifado>();
            this.Almoxarifados = new List<Almoxarifado>();
        }
        internal static Empresa GetInstance()
        {
            return new Empresa();
        }
        public Guid Id { get ; set ; }
        public string Chave { get ; set ; }
        public string CnpjCpf { get ; set ; }
        public string RazaoSocial { get ; set ; }
        public string NomeFantasia { get ; set ; }
        public string Logradouro { get ; set ; }
        public string Cep { get ; set ; }
        public string Bairro { get ; set ; }
        public string Cidade { get ; set ; }
        public string Site { get ; set ; }
        public string Telefone { get ; set ; }
        public string WhatsApp { get ; set ; }
        public virtual ICollection<Almoxarifado> Almoxarifados { get; set; }
        [NotMapped]
        public ICollection<IAlmoxarifado> IAlmoxarifados { get; set; }
    }
}
