using Basilisk.ViewModel.OrderDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.ViewModel.Order
{
    public class DetailOrderViewModel
    {
        public string SearchName { get; set; }
        public OrdersViewModel GridOrder { get; set; }
        public IEnumerable<GridOrderDetailsViewModel> GridOrderDetails { get; set; }
    }
}
