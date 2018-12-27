using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace XServicoOnline.Validacao
{
    public class ValidarFormato : ValidationAttribute
    {
        private int Formato { get; set; }
        private string[] NomesPropriedades { get; set; }

        public ValidarFormato(int minLength, params string[] nomesPropriedades)
        {
            this.Formato = minLength;
            this.NomesPropriedades = nomesPropriedades;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var properties = this.NomesPropriedades.Select(validationContext.ObjectType.GetProperty);
            var values = properties.Select(p => p.GetValue(validationContext.ObjectInstance, null)).OfType<string>();
            var totalLength = values.Sum(x => x.Length) + Convert.ToString(value).Length;
           
            return null;
        }
    }
}
