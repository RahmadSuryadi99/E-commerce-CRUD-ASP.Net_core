using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.ViewModel.Salesman
{
    public class DetailSalesViewModel
    {
        public SalesmanViewModel Sales { get; set; }
        public List<salesmanRegionViewModel> Grid { get; set; }

    }
}
