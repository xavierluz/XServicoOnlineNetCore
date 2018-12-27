using System;
using System.Collections.Generic;
using System.Text;

namespace Services.produto.categoria
{
    public class CategoriaTable
    {
        public int Id { get; set; }
        public String Nome { get; set; }
        public String Descricao { get; set; }
        public bool Ativo { get; set; }
    }
}
