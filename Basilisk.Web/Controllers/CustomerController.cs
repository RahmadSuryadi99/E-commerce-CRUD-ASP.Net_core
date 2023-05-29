using Basilisk.DataAccess.Models;
using Basilisk.Providerr;
using Basilisk.ViewModel.Cart;
using Basilisk.ViewModel.Customer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace Basilisk.Web.Controllers
{
    [Authorize(Roles = "Administrator,Customer,Salesman")]
    public class CustomerController : BaseController
    {
        private BasiliskTFContext _context = new BasiliskTFContext();
        [HttpGet]
        public IActionResult Index()
        {
            SetUsernameRole(User.Claims);
            var customer = CustomerProvider.GetProvider().GetIndex();
            return View(customer);
        }
        [HttpGet]
        public IActionResult CheckCart()
        {
            SetUsernameRole(User.Claims);
            var idCustomer = Convert.ToInt64(GetUsername(User.Claims));
            var models = CustomerProvider.GetProvider().GetIndexCart(idCustomer);
            ViewBag.TotalHarga = models.TotalHarga.ToString("C2");
            return View("cart", models);
        }

        [HttpGet]
        public IActionResult ShowProduct()
        {
            SetUsernameRole(User.Claims);
            var idCustomer = Convert.ToInt64(GetUsername(User.Claims));
            var model = CustomerProvider.GetProvider().GetAllProduct(idCustomer);
            return View("Product", model);
        }

        [AcceptVerbs("POST", "GET")]
        public IActionResult MinQuantity(long id)
        {

            SetUsernameRole(User.Claims);
            var idCustomer = Convert.ToInt64(GetUsername(User.Claims));
            CustomerProvider.GetProvider().SetMinQuantity(id);
            return RedirectToAction("CheckCart", "Customer");
        }

        [AcceptVerbs("POST", "GET")]
        public IActionResult PlusQuantity(long id)
        {

            SetUsernameRole(User.Claims);
            var idCustomer = Convert.ToInt64(GetUsername(User.Claims));
            CustomerProvider.GetProvider().SetPlusQuantity(id);
            return RedirectToAction("CheckCart", "Customer");
        }

        [AcceptVerbs("POST", "GET")]
        public IActionResult DeleteProduct(long id)
        {

            SetUsernameRole(User.Claims);
            CustomerProvider.GetProvider().DeleteProduct(id);
            return RedirectToAction("CheckCart", "Customer");
        }

        [AcceptVerbs("POST", "GET")]
        public IActionResult AddToCart(long idProduct)
        {

            SetUsernameRole(User.Claims);
            var idCustomer = Convert.ToInt64(GetUsername(User.Claims));
            CustomerProvider.GetProvider().AddProductToChart(idProduct, idCustomer);
            return NoContent();
        }

        [AcceptVerbs("POST", "GET")]
        public IActionResult CheckOut(IndexCartViewModel model)
        {

            SetUsernameRole(User.Claims);
            var idCustomer = Convert.ToInt64(GetUsername(User.Claims));
            string HasilCheckCeklist = CustomerProvider.GetProvider().PostDataCheckOut(idCustomer, model);
            if (HasilCheckCeklist != null)
            {
                return NoContent();
            }

            return RedirectToAction("Index");
        }

        [AcceptVerbs("POST", "GET")]
        public IActionResult CheckedCart(long id, bool status)
        {

            SetUsernameRole(User.Claims);
            var idCustomer = Convert.ToInt64(GetUsername(User.Claims));
            CustomerProvider.GetProvider().PostCheck(id, status, idCustomer);
            return RedirectToAction("CheckCart");
        }

        [AcceptVerbs("POST", "GET")]
        public IActionResult CheckedAllCart(long id, bool status)
        {

            SetUsernameRole(User.Claims);
            var idCustomer = Convert.ToInt64(GetUsername(User.Claims));
            CustomerProvider.GetProvider().PostCheckAll(id, status, idCustomer);
            return RedirectToAction("CheckCart");
        }
        [AcceptVerbs("POST", "GET")]
        public IActionResult ShowOrders()
        {

            SetUsernameRole(User.Claims);
            var idCustomer = Convert.ToInt64(GetUsername(User.Claims));
            var model = CustomerProvider.GetProvider().GetCustomerOrders(idCustomer);

            return View("CustomerOrders", model);
        }
        [AcceptVerbs("POST", "GET")]
        public IActionResult ReceiveOrder(string invoiceNumber)
        {
            SetUsernameRole(User.Claims);
            CustomerProvider.GetProvider().SetPesananDiterima(invoiceNumber);
            return RedirectToAction("ShowOrders");
        }
        [AcceptVerbs("POST", "GET")]
        public IActionResult ShowDetailOrder(string invoiceNumber)
        {

            SetUsernameRole(User.Claims);
            var idCustomer = Convert.ToInt64(GetUsername(User.Claims));
            var model = CustomerProvider.GetProvider().GetDetailOrder(invoiceNumber);
            ViewBag.IdCus = idCustomer;
            ViewBag.InvoiceNum = invoiceNumber;
            return View("DetailOrder", model);
        }
    }

}
