using _00003951_DBSD_CW2.DataAccess;
using _00003951_DBSD_CW2.Models;
using System.Web.Mvc;

namespace _00003951_DBSD_CW2.Controllers
{
    public class BaseController : Controller
    {
        // fetches currently logged in recruiter
        public Recruiter getRecruiter()
        {
            RecruiterManager manager = new RecruiterManager();
            return manager.GetRecruiterByEmail(User.Identity.Name);
        }
    }
}