using _00003951_DBSD_CW2.DataAccess;
using _00003951_DBSD_CW2.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace _00003951_DBSD_CW2.Controllers
{
    [Authorize]
    public class OrderController : BaseController
    {
        // GET: Order
        public ActionResult Index()
        {

            Customer user = this.getCustomer();
            FlowerOrderManager manager = new FlowerOrderManager();
            IList<FlowerOrder> list = manager.GetFlowerOrdersByCustomer(user.Id);
            return View(list);
        }

        // GET: Order/Details/5
        public ActionResult Details(int id)
        {
            FlowerOrderManager manager = new FlowerOrderManager();
            FlowerOrder model = manager.GetFlowerOrderById(id);
            return View(model);
        }

        // POST: Order/Create
        [HttpPost]
        public ActionResult Create()
        {
            try
            {
                //DepartmentManager manager = new DepartmentManager();
                //manager.CreateDepartment(department);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
