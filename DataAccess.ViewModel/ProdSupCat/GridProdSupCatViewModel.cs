using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.ViewModel.ProdSupCat
{
    public class GridProdSupCatViewModel
    {
        public long ID { get; set; }
        public string ProductName { get; set; }
        public string Supplier { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int OnOrder { get; set; }
        public bool Discontinue { get; set; }

        public string GetFormattedPrice()
        {
            return $"{this.Price.ToString("C", CultureInfo.CreateSpecificCulture("id-ID"))}";
        }
        public string GetFormattedDisc()
        {
            string result;
            if (this.Discontinue == true)
            {
                result = "Discontinued";
            }
            else result = "Continued";
            return result ;
        }
    }
}
