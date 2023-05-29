using Basilisk.ViewModel.Category;
using Basilisk.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Basilisk.ViewModel.IndexCategory;
using Basilisk.ViewModel.ProdSupCat;
using Basilisk.Providerr;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Basilisk.Web.Controllers
{
    [Authorize(Roles = "Administrator,Salesman,Customer")]
    public class CategoryController : BaseController
    {
        private static BasiliskTFContext _context = new BasiliskTFContext();

        public IActionResult Index(string searchName = "", int page = 1)
        {
            SetUsernameRole(User.Claims);
            searchName = string.IsNullOrEmpty(searchName) ? "" : searchName;
            var model = CategoryProvider.GetProvider().GetIndex(page, searchName);
            return View(model);
        }
        [HttpGet]
        public IActionResult Detail(int id)
        {
            var query = CategoryProvider.GetProvider().GetDetail(id);

            return View("DetailProduct", query);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = new CreateUpdateViewModel();

            return View(model);
        }
        [HttpGet]
        public IActionResult AddModal()
        {
            var model = new CreateUpdateViewModel();

            return View(model);
        }

        [HttpPost]
        public IActionResult Add(CreateUpdateViewModel model)
        {
            CategoryProvider.GetProvider().AddCategory(model);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(long id)
        {
            var model = CategoryProvider.GetProvider().GetDataEditCategory(id);
            return View("Edit", model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit(CreateUpdateViewModel model)
        {
            CategoryProvider.GetProvider().PostDataEditCategory(model);
            return RedirectToAction("Index");
        }

        [AcceptVerbs("POST", "GET")]
        public IActionResult Hapus(long id)
        {
            var checkProducts = _context.Products.Any(p => p.CategoryId == id);
            if (checkProducts)
            {
                return View("FailDeleted");
            }
            else
            {
                CategoryProvider.GetProvider().DeleteCategory(id);

            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult DetailPopUp(int id)
        {
            SetUsernameRole(User.Claims);
            var model = CategoryProvider.GetProvider().GetDetailCategory(id);
            return Json(model);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult SavePopUp([FromBody] CreateUpdateViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    if (CategoryProvider.GetProvider().SaveBtnModal(model))
                    {

                        return Json(new { success = true, message = "Inssert berhasil" });
                    }
                    else
                    {
                        return Json(new { success = false });

                    }
                }
                var valMsg = GetValidationViewModels(ModelState);
                return Json(new { success = false, validation = valMsg });
            }
            catch
            {
                return Json(new { success = false });
            }
        }

        [HttpGet]
        public IActionResult GetUpdate(long id)
        {
            try
            {

                var model = CategoryProvider.GetProvider().GetEditCategory(id);
                return Json(model);
            }
            catch
            {
                return Json(new { success = false });
            }

        }
        [AcceptVerbs("POST", "GET")]
        public IActionResult DeletePopUp(long id)
        {
            if (!CategoryProvider.GetProvider().DeleteCategory(id))
            {
                return Json(new { success = false,  message ="u cant delete this category" });
            }
            else
            {
                return Json(new { success = true,  message ="delete success" });

            }
        }
    }
}

