using Basilisk.ViewModel.ProdSupCat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.ViewModel.IndexProduct
{
    public class GridIndexProductViewModel
    {
        public string SearchName { get; set; }
        public string SearchCategory { get; set; }
        public string SearchSuppllier { get; set; }
        public int TotalData { get; set; }  
        public IEnumerable<GridProdSupCatViewModel> Grid {get; set;}
    }
}
