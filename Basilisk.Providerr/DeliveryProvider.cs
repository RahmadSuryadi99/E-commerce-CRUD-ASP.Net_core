using Basilisk.DataAccess.Models;
using Basilisk.Repository;
using Basilisk.ViewModel.Delivery;
using Basilisk.ViewModel.Order;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Providerr
{
    public class DeliveryProvider : BaseProvider
    {
        private CultureInfo FormatIndo = CultureInfo.CreateSpecificCulture("id-ID");
        private const int _totalDataPerPage = 5;
        public static DeliveryProvider _instance = new DeliveryProvider();
        public static DeliveryProvider GetProvider()
        {
            return _instance;
        }

        public IEnumerable<GridDeliveryViewModel> GetDataIndex(string searchName)
        {
            var AllDeliveri = DeliveryRepository.GetRepository().GetAll();
            var dataDelivery = (from d in AllDeliveri
                                where d.CompanyName.Contains(searchName)
                                select new GridDeliveryViewModel
                                {
                                    Id = d.Id,
                                    CompannyName = d.CompanyName,
                                    Cost = d.Cost,
                                    Phone = d.Phone
                                }).AsEnumerable().ToList();

            return dataDelivery;

        }
        public IndexDeliveryViewModel GetIndex(int page, string searchName)
        {
            searchName = string.IsNullOrEmpty(searchName) ? "" : searchName;

            IEnumerable<GridDeliveryViewModel> data = GetDataIndex(searchName);
            var totalData = data.Count();
            var model = new IndexDeliveryViewModel
            {
                SearchName = searchName,
                TotalData = totalData,
                TotalHalaman = TotalHalaman(totalData),
                Grid = data.Skip(GetSkip(page)).Take(_totalDataPerPage)
            };
            return model;
        }

        public GridDeliveryViewModel GetDetail(long idDelivery)
        {
            var deliveries = DeliveryRepository.GetRepository().GetAll();
            var orders = OrderRepository.GetRepository().GetAll();
            var detail = (from d in deliveries.ToList()
                          where d.Id == idDelivery
                          select new GridDeliveryViewModel
                          {
                              Id = d.Id,
                              CompannyName = d.CompanyName,
                              Cost = d.Cost,
                              Phone = d.Phone,
                              Detail = (from o in orders
                                        where o.DeliveryId == idDelivery
                                        select new DetailDeliveryViewModel
                                        {
                                            Invoice = o.InvoiceNumber,
                                            DeliveryCost = o.DeliveryCost,
                                            DestinationAddress = o.DestinationAddress,
                                            DestinationCity = o.DestinationCity,
                                            DueDate = o.DueDate,
                                            DueDateString = ((DateTime)o.DueDate).ToString("dd MMMM yyyy", FormatIndo),
                                            OrderDate = o.OrderDate,
                                            OrderDateString = ((DateTime)o.OrderDate).ToString("dd MMMM yyyy", FormatIndo),
                                            ShippedDate = o.ShippedDate,
                                            ShippedDateString = ((DateTime)o.ShippedDate).ToString("dd MMMM yyyy", FormatIndo)
                                        }).AsEnumerable()
                          }).SingleOrDefault();
            return detail;
        }
        public string UpdateDeliveryOrder(string inoviceNumber)
        {
            var Orders = OrderRepository.GetRepository().GetSingle(inoviceNumber);
                if (Orders.ShippedDate == null)
                {
                    Orders.ShippedDate = DateTime.Now;
                OrderRepository.GetRepository().Update(Orders);
                }
                else
                {
                    return "Orderan sudah terkirim";
                }

                return null;
            
        }
    }
}
