using ServicesInterfaces.produto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Services.modelo.produto
{
    public class Categoria:ICategoria
    {
        protected Categoria() {
            Materiais = new List<Material>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public virtual ICollection<Material> Materiais { get; set; }

        [NotMapped]
        public ICollection<IMaterial> IMateriais { get ; set; }

        internal static Categoria GetInstance()
        {
            return new Categoria();
        }
        internal ICategoria GetCategoria()
        {
            ICategoria categoria = new Categoria();
            categoria.Ativo = this.Ativo;
            categoria.Descricao = this.Descricao;
            categoria.Id = this.Id;
            categoria.Nome = this.Nome;
            return categoria;
        } 
    }
}
