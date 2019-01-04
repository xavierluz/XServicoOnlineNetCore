using ServicesInterfaces.produto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServicesInterfaces.movimento
{
    public interface IMovimentoItem
    {
        long Id { get; set; }
        Guid MovimentoId { get; set; }
        int MaterialId { get; set; }
        String Lote { get; set; }
        DateTime DataFabricacaoDoLote { get; set; }
        DateTime DataVencimentoDoLote { get; set; }
        int QuantidadeMovimentadaDoMaterial { get; set; }
        Decimal PrecoUnitarioDoMaterial { get; set; }
        Decimal ValorMovimentadoDoMaterial { get; set; }
        int QuantidadeSaldoDoMaterial { get; set; }
        Decimal ValorSaldoDoMaterial { get; set; }
        Decimal ValorDesdobroDoMaterial { get; set; }
        IMovimento IMovimento { get; set; }
        IMaterial IMaterial { get; set; } 
    }
}
