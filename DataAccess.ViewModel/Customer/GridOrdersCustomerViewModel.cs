using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.ViewModel.Customer
{
    public class GridOrdersCustomerViewModel
    {
       public GridCustomerViewModel Customer { get; set; }

        public int totalDataOrder { get; set; }
       public List<OrderCustmerViewModel> ListOrder { get; set; }
    }
}
