using _00003951_DBSD_CW2.DataAccess;
using _00003951_DBSD_CW2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _00003951_DBSD_CW2.Controllers
{
    public class FlowerController : BaseController
    {
        // GET: Vacancy
        public ActionResult Index(int? fCategory, string fName, string fDescription)
        {
            FlowerManager manager = new FlowerManager();
            FlowerCategoryManager categoryManager = new FlowerCategoryManager();
            IList<Flower> list = manager.FilterFlowers(fCategory, fName, fDescription);
            IList<FlowerCategory> categories = categoryManager.GetFlowerCategories();
            foreach(Flower item in list)
            {
                item.FlowerCategory = categories.First(d => d.Id == item.FlowerCategoryId);
            }
            ViewBag.fTitle = fName;
            ViewBag.fDescription = fDescription;
            ViewBag.fCategory = fCategory;
            ViewBag.categories = categories;
            return View(list);
        }
    }
}
