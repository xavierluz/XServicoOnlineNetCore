using ServicesInterfaces.produto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace XServicoOnline.ViewModels
{
    public class CategoriaViewModel : ICategoria
    {
        public CategoriaViewModel()
        {
            IMateriais = new List<IMaterial>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Digite o nome da categoria de material")]
        [StringLength(50, ErrorMessage = "Digite no mínimo 3 e máximo 50 caracters", MinimumLength = 3)]
        public String Nome { get;set; }
        [MaxLength(500)]
        [DataType(DataType.MultilineText)]
        public String Descricao { get; set; }
        public bool Ativo { get; set; }
        public ICollection<IMaterial> IMateriais { get ; set ; }
        
        public ICategoria GetICategoria()
        {
            ICategoria categoria = new CategoriaViewModel()
            {
                Ativo = this.Ativo,
                Id = this.Id,
                Descricao = this.Descricao,
                Nome = this.Nome,
                IMateriais = this.IMateriais
            };
            return categoria;
        }
        public CategoriaViewModel GetICategoria(ICategoria categoria)
        {
            CategoriaViewModel _categoria = new CategoriaViewModel()
            {
                Ativo = categoria.Ativo,
                Id = categoria.Id,
                Descricao = categoria.Descricao,
                Nome = categoria.Nome,
                IMateriais = categoria.IMateriais
            };
            return _categoria;
        }
    }
}
