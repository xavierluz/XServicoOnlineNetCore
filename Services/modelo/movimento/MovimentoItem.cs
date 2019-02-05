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

        #region "Métodos publicos"
        public static MovimentoItem GetMovimentoItem(IMovimentoItem movimentoItem)
        {
            if (movimentoItem != null)
            {
                return new MovimentoItem
                {
                    DataFabricacaoDoLote = movimentoItem.DataFabricacaoDoLote,
                    DataVencimentoDoLote = movimentoItem.DataVencimentoDoLote,
                    Id = movimentoItem.Id,
                    Material = Material.GetInstance().GetMaterial(movimentoItem.IMaterial),
                    Lote = movimentoItem.Lote,
                    MaterialId = movimentoItem.MaterialId,
                    MovimentoId = movimentoItem.MovimentoId,
                    PrecoUnitarioDoMaterial = movimentoItem.PrecoUnitarioDoMaterial,
                    QuantidadeMovimentadaDoMaterial = movimentoItem.QuantidadeMovimentadaDoMaterial,
                    QuantidadeSaldoDoMaterial = movimentoItem.QuantidadeSaldoDoMaterial,
                    ValorDesdobroDoMaterial = movimentoItem.ValorDesdobroDoMaterial,
                    ValorMovimentadoDoMaterial = movimentoItem.ValorMovimentadoDoMaterial,
                    ValorSaldoDoMaterial = movimentoItem.ValorSaldoDoMaterial,
                };
            }
            else
            {
                return new MovimentoItem();
            }
        }
        public IMovimentoItem GetMovimentoItem()
        {
            IMovimentoItem movimentoItem = new MovimentoItem
            {
                DataFabricacaoDoLote = this.DataFabricacaoDoLote,
                DataVencimentoDoLote = this.DataVencimentoDoLote,
                Id = this.Id,
                IMaterial = this.Material,
                Lote = this.Lote,
                MaterialId = this.MaterialId,
                MovimentoId = this.MovimentoId,
                PrecoUnitarioDoMaterial = this.PrecoUnitarioDoMaterial,
                QuantidadeMovimentadaDoMaterial = this.QuantidadeMovimentadaDoMaterial,
                QuantidadeSaldoDoMaterial = this.QuantidadeSaldoDoMaterial,
                ValorDesdobroDoMaterial = this.ValorDesdobroDoMaterial,
                ValorMovimentadoDoMaterial = this.ValorMovimentadoDoMaterial,
                ValorSaldoDoMaterial = this.ValorSaldoDoMaterial,
            };
            return movimentoItem;
        }
        #endregion
    }
}
