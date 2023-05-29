using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.ViewModel.OrderDetails
{
    public class GridOrderDetailsViewModel
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Harga { get; set; }
        public decimal Discount { get; set; }
    }
}
