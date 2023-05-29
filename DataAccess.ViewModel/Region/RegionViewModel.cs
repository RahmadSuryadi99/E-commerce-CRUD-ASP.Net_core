using Basilisk.ViewModel.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.ViewModel.Region
{
    public class RegionViewModel
    {
        public long Id { get; set; }

        [DisplayName("City Name")]
        [Required(ErrorMessage = "Wajib Diisi")]
        [MaxLength(length:20,ErrorMessage ="City Maxsimal 5 karakter")]
        [UniqueCityName (ErrorMessage ="udah ada")]
        public string City { get; set; }

        [DisplayName("Remark")]
        [UniqueCityName(ErrorMessage = "udah ada")]
        public string Remark { get; set; }
    }
}
