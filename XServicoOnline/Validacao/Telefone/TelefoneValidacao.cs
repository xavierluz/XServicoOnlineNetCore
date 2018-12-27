using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace XServicoOnline.Validacao.Telefone
{
    public class TelefoneValidacao : ValidationAttribute
    {
        private int MinLength { get; set; }
        private string[] NomesPropriedades { get; set; }

        public TelefoneValidacao(int minLength,params string[] nomesPropriedades)
        {
            this.MinLength = minLength;
            this.NomesPropriedades = nomesPropriedades;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var properties = this.NomesPropriedades.Select(validationContext.ObjectType.GetProperty);
            var values = properties.Select(p => p.GetValue(validationContext.ObjectInstance, null)).OfType<string>();
            var totalLength = values.Sum(x => x.Length) + Convert.ToString(value).Length;
            if (totalLength < this.MinLength)
            {
                return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
            }
            return null;
        }
    }
}
