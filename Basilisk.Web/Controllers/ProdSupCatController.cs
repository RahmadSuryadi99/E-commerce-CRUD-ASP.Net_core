using Basilisk.DataAccess.Models;
using Basilisk.Providerr;
using Basilisk.ViewModel;
using Basilisk.ViewModel.IndexProduct;
using Basilisk.ViewModel.ProdSupCat;
using Basilisk.ViewModel.Supplier;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;


namespace Basilisk.Web.Controllers
{
    [Authorize]
    public class ProdSupCatController : BaseController
    {
        public static BasiliskTFContext _context = new BasiliskTFContext();
        public IActionResult Index(int page=1,string searchName = "", string searchCategory = "", string searchSupplier = "")
        {
            SetUsernameRole(User.Claims);
            var model = ProductProvider.GetProvider().GetIndex(page,searchName,searchCategory,searchSupplier);
            return View(model);
        }
        [HttpGet]
        public IActionResult Add()
        {
            var model = ProductProvider.GetProvider().GetDropDownAdd();
            return View(model);
        }
        [HttpPost]
        public IActionResult Add(UpsertProductViewModel viewModel)
        {
            ProductProvider.GetProvider().PostDataAdd(viewModel);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(long id)
        {
            var model = ProductProvider.GetProvider().GetEdit(id);
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(UpsertProductViewModel viewModel)
        {
            ProductProvider.GetProvider().PostEdit(viewModel);

            return RedirectToAction("Index");
        }

        [AcceptVerbs("POST","GET")]
        public IActionResult Delete(long id)
        {
            ProductProvider.GetProvider().Delete(id);
            return RedirectToAction("Index");
        }

    }
}
