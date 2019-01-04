using Services.modelo.movimento;
using ServicesInterfaces.movimento;
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
        public int classificacaoId { get; set; }
        public bool Ativo { get; set; }
        #region "navegação(relacionamento)"
        public virtual Categoria Categoria { get; set; }
        public virtual Classificacao Classificacao { get; set; }
        public virtual ICollection<MovimentoItem> MovimentoItens { get; set; }
        #endregion
        #region "não mapeadas"
        [NotMapped]
        public ICategoria ICategoria { get; set; }
        [NotMapped]
        public IClassificacao IClassificacao { get; set; }
        [NotMapped]
        public ICollection<IMovimentoItem> IMovimentoItens { get; set ; }
        #endregion
        internal static Material GetInstance()
        {
            return new Material();
        }
        internal IMaterial GetMaterial()
        {
            IMaterial material = new Material();
            material.Descricao = this.Descricao;
            material.Ativo = this.Ativo;
            material.Id = this.Id;
            material.Nome = this.Nome;
            material.categoriaId = this.categoriaId;
            material.classificacaoId = this.classificacaoId;
            if (this.Categoria != null)
                material.ICategoria = this.Categoria.GetCategoria();
            if (this.Classificacao != null)
                material.IClassificacao = this.Classificacao.GetClassificacao();
            material.classificacaoId = this.classificacaoId;
            return material;
        }
        internal Material GetMaterial(IMaterial  material)
        {
            Material _material = new Material();
            _material.Descricao = material.Descricao;
            _material.Ativo = material.Ativo;
            _material.Id = material.Id;
            _material.Nome = material.Nome;
            _material.categoriaId = material.categoriaId;
            _material.classificacaoId = material.classificacaoId;
            if (material.ICategoria != null)
                _material.Categoria.GetCategoria(material.ICategoria);
            if (material.IClassificacao != null)
                _material.Classificacao.GetClassificacao(material.IClassificacao);
            _material.classificacaoId = material.classificacaoId;
            return _material;
        }
        
    }
}
