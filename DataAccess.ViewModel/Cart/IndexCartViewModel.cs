using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.ViewModel.Cart
{
    public class IndexCartViewModel
    {
       public List<long> idProd { get; set; }
       public long IdCus { get; set; }
       public decimal TotalHarga { get; set; }
       public List<GridCartViewModel> Carts { get; set; } = new List<GridCartViewModel>();

    }
}
