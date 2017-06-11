using _00003951_DBSD_CW2.DataAccess;
using _00003951_DBSD_CW2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _00003951_DBSD_CW2.Controllers
{
    //[Authorize]
    public class ReportController : BaseController
    {
        // GET: Report/Purchases
       
        public ActionResult Purchases(int? customerId)
        {
            FlowerOrderManager manager = new FlowerOrderManager();
            IList<FlowerPurchaseReport> report = new List<FlowerPurchaseReport>();
            if (customerId.HasValue)
            {
                report = manager.FlowerPurchaseReport(customerId.Value);
            }
            
            ViewBag.customers = new CustomerManager().GetCustomers();
            return View(report);
        }
    }
}