using Services.modelo.movimento;
using ServicesInterfaces.cadastro;
using ServicesInterfaces.movimento;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Services.modelo.cadastro
{
    public class Almoxarifado : IAlmoxarifado
    {
        protected Almoxarifado()
        {
            this.Movimentos = new List<Movimento>();
            this.IMovimentos = new List<IMovimento>();
        }
        internal static Almoxarifado GetInstance()
        {
            return new Almoxarifado();
        }
        public int Id { get ; set ; }
        public Guid EmpresaId { get ; set ; }
        public string Descricao { get ; set ; }
        public string Indentificacao { get ; set ; }
        public virtual Empresa Empresa { get; set; }
        public virtual ICollection<Movimento> Movimentos { get; set; }
        [NotMapped]
        public IEmpresa IEmpresa { get; set; }
        [NotMapped]
        public ICollection<IMovimento> IMovimentos { get ; set ; }

        #region "Métodos publicos"
        public static Almoxarifado GetAlmoxarifado(IAlmoxarifado almoxarifado)
        {
            return new Almoxarifado
            {
                Id = almoxarifado.Id,
                Descricao = almoxarifado.Descricao,
                Empresa = Empresa.GetInstance().GetEmpresa(almoxarifado.IEmpresa),
                EmpresaId = almoxarifado.EmpresaId,
                Indentificacao = almoxarifado.Indentificacao
            };
        }
        #endregion
    }
}
