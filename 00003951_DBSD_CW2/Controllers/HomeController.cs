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

        //[HttpGet]
        //public ActionResult Apply(int id)
        //{
        //    VacancyManager manager = new VacancyManager();
        //    Vacancy vacancy = manager.GetVacancyById(id);
        //    ViewBag.VacancyTitle = vacancy.VacancyTitle;
        //    ViewBag.VacancyId = vacancy.VacancyId;
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult Apply(int id, Application application)
        //{
        //    try
        //    {
        //        ApplicationManager appManager = new ApplicationManager();
        //        StageManager stageManager = new StageManager();
        //        application.VacancyId = id;

        //        application.StageId = stageManager.GetFirst().StageId;
        //        application.ApplicationCreatedAt = DateTime.Now;
        //        appManager.CreateApplication(application);

        //        return RedirectToAction("Success");
        //    } catch
        //    {
        //        return View();
        //    }
        //}

        //public ActionResult Success()
        //{
        //    return View();
        //}
        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
    }
}