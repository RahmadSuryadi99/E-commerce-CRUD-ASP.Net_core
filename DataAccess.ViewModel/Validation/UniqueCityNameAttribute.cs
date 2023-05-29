using Basilisk.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.ViewModel.Validation
{
    public class UniqueCityNameAttribute : ValidationAttribute
    {
        public int Length { get; set; }
        public UniqueCityNameAttribute() { }
        public UniqueCityNameAttribute(int length)
        {
            this.Length = length;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value != null)
            {
                using (var context  = new BasiliskTFContext())
                {
                    var id = (long)validationContext.ObjectInstance.GetType().GetProperty("Id").GetValue(validationContext.ObjectInstance);
                    var cek = context.Regions.Any(a => a.City == value.ToString() && a.Id!=id);
                    if (cek)
                    {
                        return new ValidationResult(ErrorMessage);
                    }
                }
            }
            return ValidationResult.Success;  
        }
    }
}
