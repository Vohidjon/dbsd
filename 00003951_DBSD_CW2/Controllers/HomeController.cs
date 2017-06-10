using _00003951_DBSD_CW2.DataAccess;
using _00003951_DBSD_CW2.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace _00003951_DBSD_CW2.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index(string fTitle, string fDescription)
        {
            FlowerCategoryManager categoryManager = new FlowerCategoryManager();
            IList<FlowerCategory> list = categoryManager.GetFlowerCategories();
            return View(list);
        }
    }
}