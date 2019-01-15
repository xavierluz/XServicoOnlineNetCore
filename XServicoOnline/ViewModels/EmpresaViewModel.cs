using ServicesInterfaces.cadastro;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace XServicoOnline.ViewModels
{
    public class EmpresaViewModel : IEmpresa
    {
        public EmpresaViewModel()
        {
            this.IAlmoxarifados = new List<IAlmoxarifado>();
        }
        public Guid Id { get; set; }
        public string Chave { get; set; }
        public string CnpjCpf { get; set; }
        [Required(ErrorMessage = "Digite o nome da categoria de material")]
        [StringLength(100, ErrorMessage = "Digite no mínimo 3 e máximo 100 caracters", MinimumLength = 5)]
        public string RazaoSocial { get; set; }
        [Required(ErrorMessage = "Digite o nome da categoria de material")]
        [StringLength(100, ErrorMessage = "Digite no mínimo 3 e máximo 100 caracters", MinimumLength = 5)]
        public string NomeFantasia { get; set; }
        [Required(ErrorMessage = "Digite o nome da categoria de material")]
        [StringLength(100, ErrorMessage = "Digite no mínimo 3 e máximo 100 caracters", MinimumLength = 5)]
        public string Logradouro { get; set; }
        [Required(ErrorMessage = "Digite o nome da categoria de material")]
        [StringLength(8, ErrorMessage = "Digite o cep válido ", MinimumLength = 8)]
        public string Cep { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Site { get; set; }
        public string Telefone { get; set; }
        public string WhatsApp { get; set; }
        public bool Ativo { get; set; }
        [Required(ErrorMessage = "Digite o email da empresa")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set ; }
        public ICollection<IAlmoxarifado> IAlmoxarifados { get; set; }
        

        public IEmpresa GetEmpresa()
        {
            IEmpresa empresa = new EmpresaViewModel()
            {
                Id = this.Id,
                RazaoSocial = this.RazaoSocial,
                NomeFantasia = this.NomeFantasia,
                CnpjCpf = this.CnpjCpf,
                Logradouro = this.Logradouro,
                Cep = this.Cep,
                Bairro = this.Bairro,
                Cidade = this.Cidade,
                Site = this.Site,
                Telefone = this.Telefone,
                WhatsApp = this.WhatsApp,
                Ativo = this.Ativo,
                Email =  this.Email
            };

            return empresa;
        } 
    }
}
