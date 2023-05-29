using Basilisk.DataAccess.Models;
using Basilisk.Repository;
using Basilisk.ViewModel.Cart;
using Basilisk.ViewModel.Customer;
using System.Globalization;
using System.Transactions;

namespace Basilisk.Providerr
{
    public class CustomerProvider : BaseProvider
    {
        public static CustomerProvider _instance = new CustomerProvider();
        public static CustomerProvider GetProvider()
        {
            return _instance;
        }

        public List<GridCustomerViewModel> GetIndex()
        {
            var customers = CustomerRepository.GetRepository().GetAll();
            var carts = CartRepository.GetRepository().GetAll();
            var customer = (from cus in customers.ToList()
                            select new GridCustomerViewModel
                            {
                                Id = cus.Id,
                                CompanyName = cus.CompanyName,
                                ContactPerson = cus.ContactPerson,
                                Phone = cus.Phone,
                                Address = cus.Address,
                                City = cus.City,
                                Email = cus.Email,
                                TotalProduct = carts.Where(c => c.CustomerId == cus.Id).Sum(a => a.Quantity)
                            }).AsEnumerable().ToList();
            return customer;
        }
        public List<GridCartViewModel> GetDataIndexCart(long idCustomer)
        {
            var carts = CartRepository.GetRepository().GetAll();
            var products = ProductRepository.GetRepository().GetAll();
            var supplier = SupplierRepository.GetRepository().GetAll();

            var enity = (from cart in carts.ToList()
                         join prod in products.ToList() on cart.ProductId equals prod.Id
                         join sup in supplier.ToList() on prod.SupplierId equals sup.Id
                         where cart.CustomerId == idCustomer
                         group sup by new { sup.CompanyName, sup.Id, cart.CustomerId } into g
                         select new GridCartViewModel
                         {
                             IdSuplier = g.Key.Id,
                             NamaToko = g.Key.CompanyName,
                             Products = (from crt in carts.ToList()
                                         join pr in products on crt.ProductId equals pr.Id
                                         where pr.SupplierId == g.Key.Id && crt.CustomerId == g.Key.CustomerId
                                         select new CartProductViewModel()
                                         {
                                             IdCard = crt.Id,
                                             Id = pr.Id,
                                             NamaProduk = pr.Name,
                                             Harga = pr.Price,
                                             Quantity = crt.Quantity,
                                             Checked = crt.Checked,
                                             TotalHarga = pr.Price * crt.Quantity
                                         }).ToList(),
                             CheckedAll = (carts.Where(c => c.Checked == true && c.CustomerId == idCustomer && c.Product.SupplierId == g.Key.Id).Count()) ==
                               (carts.Where(c => c.CustomerId == idCustomer && c.Product.SupplierId == g.Key.Id).Count()) ? true : false
                         }).ToList();
            return enity;
        }
        public IndexCartViewModel GetIndexCart(long idCustomer)
        {

            var carts = GetDataIndexCart(idCustomer);
            var models = new IndexCartViewModel()
            {
                IdCus = idCustomer,
                TotalHarga = carts.Sum(a => a.Products.Where(a => a.Checked == true).Sum(b => b.TotalHarga)),
                Carts = carts
            };
            return models;
        }

        public GridOrdersCustomerViewModel GetCustomerOrders(long idCustomer)
        {
            var formatId = CultureInfo.CreateSpecificCulture("id-ID");
            var orders = OrderRepository.GetRepository().GetAll();

            var customerDb = CustomerRepository.GetRepository().GetSingle(idCustomer);
            var order = (from o in orders
                         where o.CustomerId == idCustomer
                         select new OrderCustmerViewModel
                         {
                             DeliveryCost = o.DeliveryCost,
                             DeliveryCostString = o.DeliveryCost.ToString("C2", formatId),
                             DestinationAddress = o.DestinationAddress,
                             DestinationCity = o.DestinationCity,
                             DestinationPostalCode = o.DestinationPostalCode,
                             DueDate = o.DueDate,
                             DueDateString = ((DateTime)o.DueDate).ToString("dd MMMM yyyy", formatId),
                             InvoiceNumber = o.InvoiceNumber,
                             OrderDate = o.OrderDate,
                             OrderDateString = (o.OrderDate).ToString("dd MMMM yyyy", formatId),
                             ShippedDate = o.ShippedDate,
                             ShippedDateString = o.ShippedDate.Value.ToString("dd MMMM yyyy", formatId)
                         }).ToList();
            var customer = new GridCustomerViewModel();
            MappingModel(customer, customerDb);
            customer.TotalProduct = order.Count();

            var model = new GridOrdersCustomerViewModel
            {
                Customer = customer,
                ListOrder = order,
            };
            return model;
        }
        public void SetPesananDiterima(string inoviceNumber)
        {
            var order = OrderRepository.GetRepository().GetSingle(inoviceNumber);
            if (order.DueDate == null && order.ShippedDate != null)
            {
                order.DueDate = DateTime.Now;
                OrderRepository.GetRepository().Update(order);
            }
        }

        public List<CartProductViewModel> GetDetailOrder(string invoiceNumber)
        {
            var products = ProductRepository.GetRepository().GetAll();
            var orders = OrderRepository.GetRepository().GetAll();
            var orderDetails = OrderDetailRepository.GetRepository().GetAll();

            var product = (from o in orders.ToList()
                           join od in orderDetails.ToList() on o.InvoiceNumber equals od.InvoiceNumber
                           join p in products on od.ProductId equals p.Id
                           where od.InvoiceNumber == invoiceNumber
                           select new CartProductViewModel
                           {
                               Id = od.Id,
                               Harga = od.UnitPrice,
                               NamaProduk = p.Name,
                               Quantity = od.Quantity,
                               TotalHarga = od.Quantity * od.UnitPrice,
                           }).ToList();
            return product;
        }
        public ProductCardViewModel GetAllProduct(long idCustomer)
        {
            var products = ProductRepository.GetRepository().GetAll();
            var model = new ProductCardViewModel()
            {
                IdCustomer = idCustomer,
                Prodak = products
            };
            return model;

        }
        public void SetMinQuantity(long id)
        {
            var cart = CartRepository.GetRepository().GetSingle(id);

            if (cart.Quantity <= 1)
            {
                CartRepository.GetRepository().Delete(cart);
            }
            else
            {
                cart.Quantity--;
                CartRepository.GetRepository().Update(cart);
            }
        }
        public void SetPlusQuantity(long id)
        {
            var cart = CartRepository.GetRepository().GetSingle(id);
            cart.Quantity++;
            CartRepository.GetRepository().Update(cart);
        }
        public void DeleteProduct(long id)
        {
            using (TransactionScope scope = new TransactionScope())
            { }
            var cart = CartRepository.GetRepository().GetSingle(id);
            CartRepository.GetRepository().Delete(id);

        }
        public void AddProductToChart(long idProduct, long idCustomer)
        {
            var carts = CartRepository.GetRepository().GetAll();

            var checkCarts = carts.Any(a => a.ProductId == idProduct && a.CustomerId == idCustomer);
            if (checkCarts)
            {
                var cart = carts.SingleOrDefault(a => a.ProductId == idProduct && a.CustomerId == idCustomer);
                cart.Quantity++;
                CartRepository.GetRepository().Update(cart);
            }
            else
            {
                Cart cart = new Cart
                {
                    CustomerId = idCustomer,
                    ProductId = idProduct,
                    Quantity = 1,
                    Checked = false,
                };
                CartRepository.GetRepository().Insert(cart);
            }

        }
        public string PostDataCheckOut(long idCus, IndexCartViewModel model)
        {
            try
            {
                var Orders = OrderRepository.GetRepository().GetAll();
                Order Oldorder = Orders.OrderByDescending(a => a.InvoiceNumber).Take(1).SingleOrDefault();
                string dateNow = DateTime.Now.ToString("MM-yy");
                string InvoiceNumber = Oldorder.InvoiceNumber.Substring(0, 5);//00-00
                string noInv = Oldorder.InvoiceNumber.Substring(6, 4);

                if (dateNow == InvoiceNumber)
                {
                    string noData = (int.Parse(noInv) + 1).ToString("D4");
                    InvoiceNumber += "-" + noData;
                }
                else
                {
                    InvoiceNumber = dateNow + "-" + "0001";
                }

                var carts = CartRepository.GetRepository().GetAll().Where(a => a.CustomerId == idCus);
                var checkCartCeklist = carts.Any(a => a.CustomerId == idCus && a.Checked == true);
                if (checkCartCeklist)
                {
                    Order order = new Order
                    {
                        InvoiceNumber = InvoiceNumber,
                        CustomerId = idCus,
                        SalesEmployeeNumber = "J101",
                        OrderDate = DateTime.Now,
                        DeliveryId = 1,
                        DeliveryCost = 20000,
                        DestinationAddress = "Menara Sudirman, 3rd Floor,Jl. Jend. Sudirman Kav. 60,",
                        DestinationCity = "Jakarta",
                        DestinationPostalCode = "11240"
                    };

                    List<OrderDetail> orderDet = new List<OrderDetail>();
                    List<Cart> sendCart = new List<Cart>();
                    foreach (var item in model.Carts)
                    {
                        foreach (var prod in item.Products)
                        {
                            if (prod.Checked)
                            {
                                var Ordet = new OrderDetail
                                {
                                    InvoiceNumber = InvoiceNumber,
                                    ProductId = prod.Id,
                                    UnitPrice = prod.Harga,
                                    Quantity = prod.Quantity,
                                    Discount = 0
                                };
                                var Allcarts = CartRepository.GetRepository().GetAll();
                                var cart = Allcarts.SingleOrDefault(a => a.CustomerId == idCus && a.ProductId == prod.Id);
                                orderDet.Add(Ordet);
                                sendCart.Add(cart);
                            }
                        }
                    }
                    TransactionRepository.OrderProduct(order, orderDet, sendCart);
                }
                else
                {
                    return "Tidak TercheckList";
                }
            }
            catch
            {
                throw;
            }
            return null;
        }

        public void PostCheck(long id, bool status, long idCustomer)
        {
            var checkCart = CartRepository.GetRepository().GetSingle(id);
            checkCart.Checked = status;
            CartRepository.GetRepository().Update(checkCart);

        }
        public void PostCheckAll(long id, bool status, long idCustomer)
        {
            var carts = CartRepository.GetRepository().GetAll();
            var checkCart = carts.Where(c => c.CustomerId == idCustomer && c.Product.SupplierId == id).ToList().AsEnumerable();
            foreach (var item in checkCart)
            {
                item.Checked = status;
                CartRepository.GetRepository().Update(item);
            }
        }

    }

}
