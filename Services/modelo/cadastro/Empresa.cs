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
        public Guid Id { get; set; }
        //Aes KEY
        public string Chave { get; set; }
        //Aes IV
        public string VetorInicializacao { get; set; }
        public string CnpjCpf { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string Logradouro { get; set; }
        public string Cep { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Site { get; set; }
        public string Telefone { get; set; }
        public string WhatsApp { get; set; }
        public bool Ativo { get; set; }
        public string Email { get; set; }
        public virtual ICollection<Almoxarifado> Almoxarifados { get; set; }
        [NotMapped]
        public ICollection<IAlmoxarifado> IAlmoxarifados { get; set; }
       

        public Empresa GetEmpresa(IEmpresa empresa)
        {
            Empresa _empresa = new Empresa()
            {
                Id = empresa.Id,
                Chave = empresa.Chave,
                CnpjCpf = empresa.CnpjCpf,
                RazaoSocial = empresa.RazaoSocial,
                NomeFantasia = empresa.NomeFantasia,
                Logradouro = empresa.Logradouro,
                Cep = empresa.Cep,
                Bairro = empresa.Bairro,
                Cidade = empresa.Cidade,
                Site = empresa.Site,
                Telefone = empresa.Telefone,
                WhatsApp = empresa.WhatsApp,
                Ativo = empresa.Ativo,
                Email = empresa.Email
            };
        return _empresa;
        }
        public IEmpresa GetEmpresa()
        {
            IEmpresa _empresa = new Empresa();

           _empresa.Id = this.Id;
            _empresa.Chave = this.Chave;
            _empresa.CnpjCpf = this.CnpjCpf;
            _empresa.RazaoSocial = this.RazaoSocial;
            _empresa.NomeFantasia = this.NomeFantasia;
            _empresa.Logradouro = this.Logradouro;
            _empresa.Cep = this.Cep;
            _empresa.Bairro = this.Bairro;
            _empresa.Cidade = this.Cidade;
            _empresa.Site = this.Site;
            _empresa.Telefone = this.Telefone;
            _empresa.WhatsApp = this.WhatsApp;
            _empresa.Ativo = this.Ativo;
            _empresa.Email = this.Email;
            if (this.Almoxarifados != null)
                _empresa.IAlmoxarifados = this.IAlmoxarifados;
            return _empresa;
        }
    }
}
