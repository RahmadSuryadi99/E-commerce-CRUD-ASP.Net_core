using Basilisk.DataAccess.Models;
using Basilisk.ViewModel;
using Basilisk.ViewModel.Order;
using Basilisk.Providerr;
using Basilisk.ViewModel.OrderDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;

namespace Basilisk.Web.Controllers
{
    [Authorize(Roles ="Administrator,Salesman,Customer")]
    public class OrderController : BaseController
    {
        public IActionResult Index()
        {
            SetUsernameRole(User.Claims);

            var model = OrderProvider.GetProvider().GetIndex();
            return View(model);
        }
        public IActionResult Detail(string invoiceNumber)
        {
            DetailOrderViewModel model = OrderProvider.GetProvider().GetDetail(invoiceNumber);
            return View(model);
        }

    }
}
