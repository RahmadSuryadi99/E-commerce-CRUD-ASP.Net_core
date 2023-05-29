using Basilisk.ViewModel.Supplier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.ViewModel
{
    public class IndexSupplier
    {
        public string SearchName { get; set; }
        public int TotalHalaman { get; set; }
        public IEnumerable<GridSupplierViewModel> Grid { get; set; }
    }
}
