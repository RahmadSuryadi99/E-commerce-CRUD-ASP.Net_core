using Basilisk.DataAccess.Models;
using Basilisk.ViewModel.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Basilisk.ViewModel.Delivery
{
    public class GridDeliveryViewModel
    {
        public long Id {  get; set; }
        public string CompannyName { get; set; }
        public string Phone { get; set; }   
        public decimal Cost { get; set; }
        public IEnumerable<DetailDeliveryViewModel> Detail { get; set; }
    }
}
