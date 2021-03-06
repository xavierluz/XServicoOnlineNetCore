﻿using Services.modelo.movimento;
using ServicesInterfaces.cadastro;
using ServicesInterfaces.movimento;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Services.modelo.cadastro
{
    public class Usuario : IUsuario
    {
        protected Usuario() {
            this.Movimentos = new List<Movimento>();
            this.IMovimentos = new List<IMovimento>();
        }
        internal static Usuario GetInstance()
        {
            return new Usuario();
        }
        public string Id { get; set; }
        public Guid EmpresaId { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public DateTime RegisterDate { get; set; }
        public string Nome { get; set; }
        public virtual ICollection<Movimento> Movimentos { get; set; }

        [NotMapped]
        public ICollection<IMovimento> IMovimentos { get; set ; }

        #region "Métodos publicos"
        public static Usuario GetUsuario(IUsuario usuario)
        {
            return new Usuario
            {
                Id = usuario.Id,
                EmpresaId = usuario.EmpresaId,
                UserName = usuario.UserName,
                NormalizedUserName = usuario.NormalizedUserName,
                Email = usuario.Email,
                NormalizedEmail = usuario.NormalizedEmail,
                EmailConfirmed = usuario.EmailConfirmed,
                PasswordHash = usuario.PasswordHash,
                SecurityStamp = usuario.SecurityStamp,
                ConcurrencyStamp = usuario.ConcurrencyStamp,
                PhoneNumber = usuario.PhoneNumber,
                PhoneNumberConfirmed = usuario.PhoneNumberConfirmed,
                TwoFactorEnabled = usuario.TwoFactorEnabled,
                LockoutEnd = usuario.LockoutEnd,
                LockoutEnabled = usuario.LockoutEnabled,
                AccessFailedCount = usuario.AccessFailedCount,
                RegisterDate = usuario.RegisterDate,
                Nome = usuario.Nome
            };
        }
        #endregion
    }
}
