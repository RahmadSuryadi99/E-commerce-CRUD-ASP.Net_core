using Basilisk.Providerr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Basilisk.Web.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class DeliveryController : BaseController
    {
        public IActionResult Index(string searchName="",int page = 1)
        {

            SetUsernameRole(User.Claims);
            var model = DeliveryProvider.GetProvider().GetIndex(page,searchName);
            return View(model);
        }
        public IActionResult Detail(long id)
        {

            var model = DeliveryProvider.GetProvider().GetDetail(id);
            return View(model);
        }

        public IActionResult SendOrder(long id, string invoiceNum) 
        {
             string alertText = DeliveryProvider.GetProvider().UpdateDeliveryOrder(invoiceNum);

            return RedirectToAction("Detail",new {id});
        }
    }
}
