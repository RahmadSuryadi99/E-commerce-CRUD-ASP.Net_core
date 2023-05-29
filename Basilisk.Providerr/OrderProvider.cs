using Basilisk.DataAccess.Models;
using Basilisk.Repository;
using Basilisk.ViewModel.Order;
using Basilisk.ViewModel.OrderDetails;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Providerr
{
    public class OrderProvider
    {
        public static OrderProvider _Instance = new OrderProvider();
        public static OrderProvider GetProvider()
        {
            return _Instance;
        }
        public List<OrdersViewModel> GetIndex(string idSales = "")
        {
            var orders = OrderRepository.GetRepository().GetAll();
            var customers = CustomerRepository.GetRepository().GetAll();
            var orderDetails = OrderDetailRepository.GetRepository().GetAll();

            var model = (from ord in orders.ToList()
                         join cus in customers on ord.CustomerId equals cus.Id
                         join ordet in orderDetails on ord.InvoiceNumber equals ordet.InvoiceNumber into detail
                         from result in detail.DefaultIfEmpty()
                         where ord.SalesEmployeeNumber.Contains(idSales)
                         group result by new { ord.InvoiceNumber, cus.CompanyName, ord.OrderDate, ord.DeliveryCost } into grup
                         select new OrdersViewModel
                         {
                             InvoiceNumber = grup.Key.InvoiceNumber,
                             CustomerName = grup.Key.CompanyName,
                             OrderDate = grup.Key.OrderDate,
                             TotalPembayaran = grup.Sum(r => r==null?0: (r.UnitPrice * r.Quantity)) == 0 ?
                                         "Rp.0,00" : (grup.Sum(r => (r.UnitPrice * r.Quantity) * (1 - (r.Discount / 100))) + grup.Key.DeliveryCost).ToString("C2"),
                         }).ToList();
            return model;
        }
        public DetailOrderViewModel GetDetail(string invoiceNumber)
        {
            var allOrders = OrderRepository.GetRepository().GetAll();
            var allOrderDetails = OrderDetailRepository.GetRepository().GetAll();
            var salesmen = SalesmanRepository.GetRepository().GetAll();
            var customers = CustomerRepository.GetRepository().GetAll();

            var indoFormat = CultureInfo.CreateSpecificCulture("id-ID");
            var orders = (from ord in allOrders.ToList()
                          join cus in customers on ord.CustomerId equals cus.Id
                          join sal in salesmen.ToList() on ord.SalesEmployeeNumber equals sal.EmployeeNumber
                          where ord.InvoiceNumber == invoiceNumber
                          select new OrdersViewModel 
                          {
                              InvoiceNumber = invoiceNumber,
                              CustomerName = cus.CompanyName,
                              SalesName = sal.FirstName,
                              //OrderDate = ord.OrderDate.ToString("dd MMMM yyyy",CultureInfo.CreateSpecificCulture("id-ID")),
                              OrderDate = ord.OrderDate,
                              ShippedDate = ord.ShippedDate,
                              DueDate = ord.DueDate,
                              DesAddress = ord.DestinationAddress,
                              DesCity = ord.DestinationCity,
                              DesPostalCode = ord.DestinationPostalCode,
                              DesCost = ord.DeliveryCost.ToString("C0", indoFormat)
                          }).SingleOrDefault();

            var orderDetails = (from ordet in allOrderDetails
                                where ordet.InvoiceNumber == invoiceNumber
                                select new GridOrderDetailsViewModel
                                {
                                    ProductName = ordet.Product.Name,
                                    Quantity = ordet.Quantity,
                                    Harga = ordet.UnitPrice,
                                    Discount = ordet.Discount / 100
                                }).AsEnumerable().ToList();

            var model = new DetailOrderViewModel()
            {
                GridOrder = orders,
                GridOrderDetails = orderDetails
            };
            return model;
        }
    }

}
