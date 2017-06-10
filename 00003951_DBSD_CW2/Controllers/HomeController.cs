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
        //public ActionResult Index(string fTitle, string fDescription)
        //{
        //    VacancyManager manager = new VacancyManager();
        //    IList<Vacancy> list = manager.GetAllVacancies(fTitle, fDescription);
        //    DepartmentManager depManager = new DepartmentManager();
        //    IList<Department> departments = depManager.GetDepartments();
        //    foreach(Vacancy item in list)
        //    {
        //        item.Department = departments.First(d => d.DepartmentId == item.DepartmentId);
        //    }

        //    return View(list);
        //}

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