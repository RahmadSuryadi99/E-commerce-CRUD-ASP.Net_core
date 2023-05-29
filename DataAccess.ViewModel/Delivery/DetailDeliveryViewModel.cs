using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.ViewModel.Delivery
{
    public class DetailDeliveryViewModel
    {
        public string Invoice { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderDateString { get; set; }
        public DateTime? ShippedDate { get; set; }
        public string? ShippedDateString { get; set; }
        public DateTime? DueDate { get; set; }
        public string? DueDateString { get; set; }
        public decimal DeliveryCost { get; set; }
        public string DestinationAddress { get; set; }
        public string DestinationCity { get; set; }


    }
}
