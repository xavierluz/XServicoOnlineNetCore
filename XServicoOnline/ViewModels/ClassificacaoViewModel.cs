using ServicesInterfaces.produto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace XServicoOnline.ViewModels
{
    public class ClassificacaoViewModel : IClassificacao
    {
        public ClassificacaoViewModel()
        {
            IMateriais = new List<IMaterial>();
        }

        public int Id { get ; set ; }
        [Required(ErrorMessage = "Digite o nome da classificação de material")]
        [StringLength(50, ErrorMessage = "Digite no mínimo 3 e máximo 50 caracters", MinimumLength = 3)]
        public String Nome { get; set; }
        [MaxLength(500)]
        [DataType(DataType.MultilineText)]
        public String Descricao { get; set; }
        public bool Ativo { get; set; }
        public ICollection<IMaterial> IMateriais { get; set; }

        public IClassificacao GetClassificacao()
        {
            IClassificacao classificacao = new ClassificacaoViewModel()
            {
                Ativo = this.Ativo,
                Id = this.Id,
                Descricao = this.Descricao,
                Nome = this.Nome,
                IMateriais = this.IMateriais
            };
            return classificacao;
        }
        public ClassificacaoViewModel GetClassificacao(IClassificacao  classificacao)
        {
            ClassificacaoViewModel _classificacao = new ClassificacaoViewModel()
            {
                Ativo = classificacao.Ativo,
                Id = classificacao.Id,
                Descricao = classificacao.Descricao,
                Nome = classificacao.Nome,
                IMateriais = classificacao.IMateriais
            };
            return _classificacao;
        }
    }
}
