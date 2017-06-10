using _00003951_DBSD_CW2.Models;
using System.Web.Mvc;
using System.Web.Security;
using _00003951_DBSD_CW2.DataAccess;

namespace _00003951_DBSD_CW2.Controllers
{
    [AllowAnonymous]
    public class LoginController : BaseController
    {
        public ActionResult Index()
        {
            return View(new LoginModel());
        }

        [HttpPost]
        public ActionResult Login(LoginModel loginModel, string returnUrl)
        {
            CustomerManager manager = new CustomerManager();
            bool authenticated = manager.Authenticate(loginModel.Email, loginModel.Password);
            if (authenticated)
            {
                FormsAuthentication.SetAuthCookie(loginModel.Email, false);

                if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                    && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Flower");
                }
            }
            else
            {
                ModelState.AddModelError("", "Authentication failed!");
                return View("Index", loginModel);
            }
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Customer customer)
        {
            try
            {
                CustomerManager manager = new CustomerManager();
                manager.CreateCustomer(customer);
                FormsAuthentication.SetAuthCookie(customer.Email, false);

                return RedirectToAction("Index", "Flower");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }

    }
}