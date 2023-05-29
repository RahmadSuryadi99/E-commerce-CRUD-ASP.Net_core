using Basilisk.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.ViewModel.Cart
{
    public class ProductCardViewModel
    {
        public long IdCustomer { get; set; }
        public IEnumerable< Product> Prodak { get; set; } 
        
    }
}
