using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "username is required")]
        [StringLength(20, ErrorMessage ="username can't be more than 20 characters.")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "password is required")]
        [StringLength(20, ErrorMessage = "password can't be more than 20 characters.")]

        public string? Password { get; set; }
    }
}
