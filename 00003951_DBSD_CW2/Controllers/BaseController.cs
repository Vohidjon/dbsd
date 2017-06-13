using _00003951_DBSD_CW2.DataAccess;
using _00003951_DBSD_CW2.Models;
using System.Web.Mvc;

namespace _00003951_DBSD_CW2.Controllers
{
    public class BaseController : Controller
    {
        // fetches currently logged in customer
        public Customer getCustomer()
        {
            CustomerManager manager = new CustomerManager();
            return manager.GetCustomerByEmail(User.Identity.Name);
        }
    }
}