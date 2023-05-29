using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.ViewModel.Supplier
{
    public class CreateUpdateSupViewModel
    {
        public long ID { get; set; }

        [Required(ErrorMessage = "wajib Diisi")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "wajib Diisi")]
        public string ContactPerson { get; set; }

        [Required(ErrorMessage = "wajib Diisi")]
        public string JobTitle { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
