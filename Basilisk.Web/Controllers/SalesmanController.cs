using Basilisk.DataAccess.Models;
using Basilisk.Providerr;
using Basilisk.ViewModel.Salesman;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Basilisk.Web.Controllers
{
    [Authorize]
    public class SalesmanController : BaseController
    {
        private const int totalDataPerPage = 5;
        public static BasiliskTFContext _context = new BasiliskTFContext();
        public static IQueryable getJumlahRegion(string id)
        {
            var region = SalesmanProvider.GetProvider().GetJumlahRegion(id);

            return region;
        }
        public IActionResult Index(int page = 1)
        {
            SetUsernameRole(User.Claims);
            var model = SalesmanProvider.GetProvider().GetIndex(page);

            return View(model);
        }
     
       
        [HttpGet]
        public IActionResult Add()
        {
           var model = SalesmanProvider.GetProvider().GetAdd();
            return View(model);
        }

        [HttpPost]
        public IActionResult Add(SalesmanViewModel viewModel) 
        {
            if (ModelState.IsValid)
            {
               SalesmanProvider.GetProvider().PostAdd(viewModel);
                return RedirectToAction("Index");
            }
            else
            {
                //viewModel.DropDownSales = GetSuperiorEmployee();

                return View("Add", viewModel);
            }
        }
        [HttpGet]
        public IActionResult Edit(string id) 
        {
            var model = SalesmanProvider.GetProvider().GetEdit(id);
            return View(model);  
        }
        [HttpPost]
        public IActionResult Edit(SalesmanViewModel viewModel) 
        {
            if (ModelState.IsValid)
            {
                SalesmanProvider.GetProvider().PostEdit(viewModel);
                return RedirectToAction("Index");
            }
            else
            {
                viewModel.DropDownSales = SalesmanProvider.GetProvider().GetDataSuperiorEmployee();
                ViewBag.listSales = SalesmanProvider.GetProvider().GetDataSuperiorEmployee();
                return View("Add", viewModel);
            }
        }

        [HttpGet]
        public IActionResult Detail(string id)
        {
            var model = SalesmanProvider.GetProvider().GetDetail(id);
           
            return View(model);
        }
        [AcceptVerbs("POST","GET")]
        public IActionResult Delete(string empNumber)
        {
            SalesmanProvider.GetProvider().PostDelete(empNumber);
            return RedirectToAction("Index");
        }
    }
}
