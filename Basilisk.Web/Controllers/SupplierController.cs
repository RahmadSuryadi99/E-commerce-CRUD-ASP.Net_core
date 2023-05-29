using Basilisk.DataAccess.Models;
using Basilisk.Providerr;
using Basilisk.ViewModel.Supplier;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Basilisk.Web.Controllers
{
    [Authorize]
    public class SupplierController : BaseController
    {
        private static BasiliskTFContext _context = new BasiliskTFContext();

        public IActionResult Index(int page = 1,string searchName = "")
        {
            SetUsernameRole(User.Claims);
           var model = SupplierProvider.GetProvider().GetIndex(page, searchName);   
          
            return View(model);
        }
        public IActionResult Detail(int id)
        {
           var model = SupplierProvider.GetProvider().GetDetail(id);

            return View("DetailSupplier", model);
        }
        [HttpGet]
        public IActionResult Add()
        {
            var model = new CreateUpdateSupViewModel();
            return View(model);
        }
        [HttpPost]
        public IActionResult Add(CreateUpdateSupViewModel model)
        {
            if (ModelState.IsValid)
            {
                SupplierProvider.GetProvider().PostAdd(model); 
                 return RedirectToAction("Index");
            }
            else
            {
                return View("Add", model);
            }
        }
        [HttpGet]
        public IActionResult Edit(long id) 
        {
            var entity = SupplierProvider.GetProvider().GetEdit(id);
            return View(entity);
        }

        [HttpPost]
        public IActionResult Edit(CreateUpdateSupViewModel model) 
        {
            SupplierProvider.GetProvider().PostEdit(model);
            return RedirectToAction("Index");
        }
    
        [AcceptVerbs("POST", "GET")]
        public IActionResult Delete(long id)
        {
            var checkProducts = _context.Products.Any(p => p.SupplierId == id);
            if (checkProducts)
            {
                return View("FailDeleted");
            }
            else
            {
                var supplier = _context.Suppliers.SingleOrDefault(p => p.Id == id);
                supplier.DeleteDate = DateTime.Now;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult DetailPopUp(int id)
        {
            var model = SupplierProvider.GetProvider().GetDetail(id);
            return Json(model);
        }

    }
}
