using Basilisk.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.ViewModel.Cart
{
    public class GridCartViewModel
    {
        public string NamaToko { get; set; }
        public long IdSuplier { get; set; }
        public bool CheckedAll { get; set; }
        public List<CartProductViewModel> Products { get; set; } = new List<CartProductViewModel>();
    }
}
