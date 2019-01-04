using ServicesInterfaces.cadastro;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Services.modelo.cadastro
{
    public class Almoxarifado : IAlmoxarifado
    {
        protected Almoxarifado() { }
        internal static Almoxarifado GetInstance()
        {
            return new Almoxarifado();
        }
        public int Id { get ; set ; }
        public Guid EmpresaId { get ; set ; }
        public string Descricao { get ; set ; }
        public string Indentificacao { get ; set ; }
        public Empresa Empresa { get; set; }
        [NotMapped]
        public IEmpresa IEmpresa { get; set; }
    }
}
