using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace XServicoOnline.ViewModels
{
    public class LoginViewModel
    {
        [StringLength(50, ErrorMessage = "Digite no mínimo 5 caracters!", MinimumLength = 5)]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email é obrigatório!")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [StringLength(50, ErrorMessage = "Digite no mínimo 8 caracters, Maiusculas, Minusculas e caracters especiais", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Senha é obrigatório!")]
        [Display(Name = "Senha")]
        public string PasswordHash { get; set; }
        [Display(Name = "Manter conectado")]
        public bool ManterConectado { get; set; }
    }
}
