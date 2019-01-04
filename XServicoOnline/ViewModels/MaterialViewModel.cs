using Microsoft.AspNetCore.Mvc.Rendering;
using ServicesInterfaces.movimento;
using ServicesInterfaces.produto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace XServicoOnline.ViewModels
{
    public class MaterialViewModel : IMaterial
    {
        public MaterialViewModel() {
            this.IMovimentoItens = new List<IMovimentoItem>();
        }

        public int Id { get ; set ; }
        [Required(ErrorMessage = "Digite o nome do material")]
        [StringLength(50, ErrorMessage = "Digite no mínimo 3 e máximo 50 caracters", MinimumLength = 3)]
        public String Nome { get; set; }
        [MaxLength(500)]
        [DataType(DataType.MultilineText)]
        public String Descricao { get; set; }
        public bool Ativo { get; set; }
        public ICategoria ICategoria { get ; set ; }
        public IClassificacao IClassificacao { get ; set ; }
        [Required(ErrorMessage = "Selecione a categoria do material")]
        [Display(Name = "Categoria")]
        public int categoriaId { get ; set ; }
        [Required(ErrorMessage = "Selecione a classificação do material")]
        [Display(Name ="Classificação")]
        public int classificacaoId { get ; set ; }
        public SelectList Categorias { get; set; }
        public SelectList Classificacoes { get; set; }
        public ICollection<IMovimentoItem> IMovimentoItens { get; set ; }

        internal IMaterial GetMaterial()
        {
            IMaterial material = new MaterialViewModel();
            material.Descricao = this.Descricao;
            material.Ativo = this.Ativo;
            material.Id = this.Id;
            material.Nome = this.Nome;
            material.categoriaId = this.categoriaId;
            material.classificacaoId = this.classificacaoId;
            material.ICategoria = this.ICategoria;
            material.IClassificacao = this.IClassificacao;
            material.classificacaoId = this.classificacaoId;
            return material;
        }
        internal MaterialViewModel GetMaterial(IMaterial material)
        {
            MaterialViewModel _material = new MaterialViewModel();
            _material.Descricao = material.Descricao;
            _material.Ativo = material.Ativo;
            _material.Id = material.Id;
            _material.Nome = material.Nome;
            _material.categoriaId = material.categoriaId;
            _material.classificacaoId = material.classificacaoId;
            _material.ICategoria = material.ICategoria;
            _material.IClassificacao = material.IClassificacao;
            _material.classificacaoId = material.classificacaoId;
            return _material;
        }
    }
}
