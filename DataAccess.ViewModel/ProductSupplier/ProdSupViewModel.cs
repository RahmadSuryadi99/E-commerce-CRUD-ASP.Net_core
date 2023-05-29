using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basilisk.ViewModel.ProdSupCat;
using Basilisk.ViewModel.Supplier;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Basilisk.ViewModel.ProductSupplier
{
    public class ProdSupViewModel
    {
        public GridSupplierViewModel GridSupplier { get; set; }
        public IEnumerable<GridProdSupCatViewModel> Grip { get; set; }
    }
}
