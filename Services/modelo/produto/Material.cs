using ServicesInterfaces.produto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Services.modelo.produto
{
    public class Material:IMaterial
    {
        protected Material() { }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int categoriaId { get; set; }
        public virtual Categoria Categoria { get; set; }
        public int classificacaoId { get; set; }
        public virtual Classificacao Classificacao { get; set; }
        public bool Ativo { get; set; }

        [NotMapped]
        public ICategoria ICategoria { get; set; }
        [NotMapped]
        public IClassificacao IClassificacao { get; set; }
        internal static Material GetInstance()
        {
            return new Material();
        }

    }
}
