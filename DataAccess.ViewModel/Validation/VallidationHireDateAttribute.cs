using Basilisk.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.ViewModel.Validation
{

    public class VallidationHireDateAttribute : ValidationAttribute
    {
        public string ValidationType { get; set; }
        public string phon { get; set; }
        
        //public VallidationHireDateAttribute() { }
        //public VallidationHireDateAttribute(string phone) {

        //    //return new ValidationResult("tes");
        //}
        
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                using (var context = new BasiliskTFContext())
                {
                    var tglLahir = (DateTime)validationContext.ObjectInstance.GetType().GetProperty("BirthDate").GetValue(validationContext.ObjectInstance);
                    var tglHire = (DateTime)validationContext.ObjectInstance.GetType().GetProperty("HireDate").GetValue(validationContext.ObjectInstance);
                    TimeSpan hasil = DateTime.Now - tglLahir;
                    int umur = hasil.Days / 365;

                    if (ValidationType == "phone")
                    {
                        return new ValidationResult("tes");
                    }
                    else if(ValidationType == "HireDate")
                    {
                    
                        if (tglHire<tglLahir)
                        {
                            return new ValidationResult("blom lahir blok");
                        }
                        else if(umur <= 19)
                        {
                            return new ValidationResult(ErrorMessage);
                        }
                    
                    }
                }
            }
            return ValidationResult.Success;
        }
    }
}
