using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.ViewModel.Category
{
    public class CreateUpdateViewModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage ="harap di isi")]
        [MinLength(3,ErrorMessage ="panjang kurang dari 3")]
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
