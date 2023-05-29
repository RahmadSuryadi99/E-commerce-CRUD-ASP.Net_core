using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.ViewModel.Delivery
{
    public class IndexDeliveryViewModel
    {
        public int TotalData { get; set; }
        public int TotalHalaman { get; set; }
        public string SearchName { get; set; }  
        public IEnumerable<GridDeliveryViewModel> Grid { get; set; }

    }
}
