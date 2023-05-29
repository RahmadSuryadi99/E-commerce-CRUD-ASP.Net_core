using Basilisk.Providerr;
using Basilisk.ViewModel;
using Basilisk.ViewModel.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Basilisk.Web.Controllers
{
    public class AccountController : BaseController
    {
        [HttpGet]
        public IActionResult Login(string? returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;//gmna dia bisa ngisi url
            var model = new LoginViewModel();
            return View(model);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Login(string? returnUrl, LoginViewModel model)
        {
            if (ModelState.IsValid)
            {

                if (AccountProvider.GetProvider().IsAuthentication(model))
                {
                    var namaRole = AccountProvider.GetProvider().GetRoleName(model.Username);
                    var claims = new List<Claim>
                    {
                    new Claim("username",model.Username),
                    new Claim(ClaimTypes.NameIdentifier,model.Username),
                    new Claim(ClaimTypes.Role,namaRole)
                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);//ini buat apa?
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(principal);

                    if (returnUrl == null)
                    {
                        if (namaRole=="Customer")
                        {
                            return RedirectToAction("CheckCart", "Customer");
                        }
                        else if(namaRole == "Salesman")
                        {
                            return RedirectToAction("Index", "Order");
                        }
                        else
                        {

                        return RedirectToAction("Index", "Home");
                        }
                    }
                    else Redirect(returnUrl);
                }
                else
                {
                    /* jika auth gagal*/
                    return RedirectToAction("LoginFailed");
                }
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
        [HttpGet]
        public IActionResult LoginFailed()
        {

            return View();
        }
        [Authorize(Roles = "Administrator")]
        public IActionResult Add()
        {
            ViewBag.Password = "Indocyber";

            var model = new GridAccountViewModel();
            return View("Registration",model);
        }
        public IActionResult Save(GridAccountViewModel model)
        {
            AccountProvider.GetProvider().SaveAccount(model);
            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            SetUsernameRole(User.Claims);
            var model = AccountProvider.GetProvider().GetIndex();
            return View(model);
        }
    }
}
