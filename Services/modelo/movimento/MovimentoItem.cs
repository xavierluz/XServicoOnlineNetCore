using Services.modelo.produto;
using ServicesInterfaces.movimento;
using ServicesInterfaces.produto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Services.modelo.movimento
{
    public class MovimentoItem : IMovimentoItem
    {
        protected MovimentoItem() { }
        internal static MovimentoItem GetInstance()
        {
            return new MovimentoItem();
        }
        public long Id { get; set; }
        public Guid MovimentoId { get; set; }
        public int MaterialId { get; set; }
        public string Lote { get; set; }
        public DateTime DataFabricacaoDoLote { get; set; }
        public DateTime DataVencimentoDoLote { get; set; }
        public int QuantidadeMovimentadaDoMaterial { get; set; }
        public decimal PrecoUnitarioDoMaterial { get; set; }
        public decimal ValorMovimentadoDoMaterial { get; set; }
        public int QuantidadeSaldoDoMaterial { get; set; }
        public decimal ValorSaldoDoMaterial { get; set; }
        public decimal ValorDesdobroDoMaterial { get; set; }
        public virtual Movimento Movimento { get; set; }
        public virtual Material Material { get; set; }
        [NotMapped]
        public IMovimento IMovimento { get; set; }
        [NotMapped]
        public IMaterial IMaterial { get; set; }
    }
}
