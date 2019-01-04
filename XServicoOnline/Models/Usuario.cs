using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;
using System.Threading.Tasks;
using XServicoOnline.Validacao.Telefone;

namespace XServicoOnline.Models
{
    public class Usuario: IdentityUser<string>
    {
      
        #region "Atributos de manipulação"
        [TempData]
        [NotMapped]
        public string StatusMessage { get; set; }
        [TempData]
        [NotMapped]
        public string returnUrl { get; set; }
        [NotMapped]
        [StringLength(20, ErrorMessage = "Digite no mínimo 8 caracters, Maiusculas, Minusculas e caracters especiais", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Confirmar Senha é obrigatório!")]
        [Display(Name = "Confirmar Senha")]
        [CompareAttribute("PasswordHash", ErrorMessage = "Senhas não confere")]
        public string PasswordHashCorfirmed { get; set; }
        #endregion
        #region "Atributos das entidades personalizados"
        public override string Id { get => base.Id; set => base.Id = value; }
        public Guid EmpresaId { get; set; }
        [Display(Name ="Usuário")]
        [Required(ErrorMessage ="Usuário é obrigatório!")]
        [StringLength(50,ErrorMessage ="Digite no mínimo 5 caracters!", MinimumLength =5)]
        public override string UserName { get => base.UserName; set => base.UserName = value; }
        public override string NormalizedUserName { get => base.NormalizedUserName; set => base.NormalizedUserName = value; }
        [StringLength(50, ErrorMessage = "Digite no mínimo 5 caracters!", MinimumLength = 5)]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage ="Email é obrigatório!")]
        [Display(Name = "Email")]
        public override string Email { get => base.Email; set => base.Email = value; }
        public override string NormalizedEmail { get => base.NormalizedEmail; set => base.NormalizedEmail = value; }
        public override bool EmailConfirmed { get => base.EmailConfirmed; set => base.EmailConfirmed = value; }
        [StringLength(50, ErrorMessage = "Digite no mínimo 8 caracters, Maiusculas, Minusculas e caracters especiais", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Senha é obrigatório!")]
        [Display(Name = "Senha")]
        public override string PasswordHash { get => base.PasswordHash; set => base.PasswordHash = value; }
        public override string SecurityStamp { get => base.SecurityStamp; set => base.SecurityStamp = value; }
        public override string ConcurrencyStamp { get => base.ConcurrencyStamp; set => base.ConcurrencyStamp = value; }
        [TelefoneValidacao(8, ErrorMessage = "Digite telefone válido!")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Telefone")]
        public override string PhoneNumber { get => base.PhoneNumber; set => base.PhoneNumber = value; }
        public override bool PhoneNumberConfirmed { get => base.PhoneNumberConfirmed; set => base.PhoneNumberConfirmed = value; }
        public override bool TwoFactorEnabled { get => base.TwoFactorEnabled; set => base.TwoFactorEnabled = value; }
        public override DateTimeOffset? LockoutEnd { get => base.LockoutEnd; set => base.LockoutEnd = value; }
        public override bool LockoutEnabled { get => base.LockoutEnabled; set => base.LockoutEnabled = value; }
        public override int AccessFailedCount { get => base.AccessFailedCount; set => base.AccessFailedCount = value; }
        [DataType(DataType.DateTime)]
        [Display(Name = "Data cadastro")]
        public DateTime RegisterDate { get; set; }
        [Required(ErrorMessage ="Nome do usuário é obrigatório")]
        [StringLength(50,ErrorMessage ="Digite entre 5 a 50 caracters",MinimumLength = 5)]
        public String Nome { get; set; }
        public virtual ICollection<UsuarioReivindicacao> UsuarioReivindicacao { get; set; }
        public virtual ICollection<UsuarioLogin> UsuarioLogin { get; set; }
        public virtual ICollection<UsuarioToken> UsuarioToken { get; set; }
        public virtual ICollection<UsuarioFuncao> UsuarioFuncao { get; set; }
        #endregion
       
    }
}
