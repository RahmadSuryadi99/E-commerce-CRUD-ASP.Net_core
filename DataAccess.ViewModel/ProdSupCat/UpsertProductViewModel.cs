using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.ViewModel.ProdSupCat
{
    public class UpsertProductViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long? SupplyerId { get; set; }
        public long CategoryId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int OnOrder { get; set; }
        public bool Discontinue { get; set; }
        public List<SelectListItem> DropdownSupplier { get; set; }
        public List<SelectListItem> DropdownCategory { get; set; }
        public List<DropDownViewModel> DropdownSupplierCustom { get; set; }
        public List<DropDownViewModel> DropDownCategoryCustom { get; set; }
    }
}
