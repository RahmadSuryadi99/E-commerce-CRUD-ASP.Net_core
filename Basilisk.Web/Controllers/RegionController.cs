using Basilisk.DataAccess.Models;
using Basilisk.Providerr;
using Basilisk.ViewModel.Region;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Basilisk.Web.Controllers
{
    [Authorize]
    public class RegionController : BaseController
    {
        private static BasiliskTFContext _context = new BasiliskTFContext();
        public IActionResult Index(string searchName = "")
        {
            SetUsernameRole(User.Claims);
            var model = RegionProvider.GetProvider().GetIndex(searchName);
            return View("Index",model);
        }

        [HttpGet]
        public IActionResult Edit(long idRegion)
        {
           var model = RegionProvider.GetProvider().GetEdit(idRegion);
            return View("UpsertRegion",model);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = new RegionViewModel();
            return View("UpsertRegion", model);
        }

        [HttpPost]
        public IActionResult Save(RegionViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                RegionProvider.GetProvider().PostSave(viewModel);
                return RedirectToAction("Index");
            }
            else
            { 
            return View("UpsertRegion",viewModel);
            }
        }
    }
}
