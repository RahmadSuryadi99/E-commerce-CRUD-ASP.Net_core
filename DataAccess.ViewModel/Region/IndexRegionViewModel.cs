using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.ViewModel.Region
{
    public class IndexRegionViewModel
    {
        public string searchName { get; set; }
        public IEnumerable<RegionViewModel> grid { get; set; }
    }
}
