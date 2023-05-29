using Basilisk.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.ViewModel.Order
{
    public class OrdersViewModel
    {
        public string InvoiceNumber { get; set; }
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public string TotalPembayaran { get; set; }
        public decimal Ongkir { get; set; }
        public List<OrderDetail> orderDetails { get; set; }
        public DateTime? ShippedDate { get; set; }
        public DateTime? DueDate { get; set; }
        public string DesAddress { get; set; }
        public string DesCity { get; set; }
        public string DesPostalCode { get; set; }
        public string DesCost { get; set; }
        public string SalesName { get; set; }
        public string EmployeeNumber { get; set; }
    }
}
