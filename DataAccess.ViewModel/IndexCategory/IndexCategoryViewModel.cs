using Basilisk.ViewModel.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.ViewModel.IndexCategory
{
    public class IndexCategoryViewModel
    {
        
        public int TotalData { get; set; }
        public int TotalHalaman { get; set; }
        public string SearchName { get; set; }
        public IEnumerable<GridCategoryViewModel> GridCategoryIndex { get; set; }
    }
}
