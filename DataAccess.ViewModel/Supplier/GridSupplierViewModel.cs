using Basilisk.ViewModel.ProdSupCat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.ViewModel.Supplier
{
    public class GridSupplierViewModel
    {
        public long ID { get; set; }
        public string CompanyName { get; set; }
        public string ContactPerson { get; set; }
        public string JobTitle { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Email { get; set;}

        //public IEnumerable< GridProdSupCatViewModel> grid { get; set; }


    }
}
