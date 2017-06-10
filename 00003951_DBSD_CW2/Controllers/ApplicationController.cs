using _00003951_DBSD_CW2.DataAccess;
using _00003951_DBSD_CW2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _00003951_DBSD_CW2.Controllers
{
    [Authorize]
    public class ApplicationController : BaseController
    {
        // GET: Application
        //[HttpGet]
        //public ActionResult Update(int id)
        //{
        //    ApplicationManager manager = new ApplicationManager();
        //    Application model = manager.GetApplicationById(id);

        //    StageManager stageManager = new StageManager();
        //    ViewBag.stages = stageManager.GetStages();
        //    return View(model);
        //}

        //[HttpPost]
        //public ActionResult Update(int id, Application application)
        //{
            
        //    ApplicationManager manager = new ApplicationManager();
        //    manager.MoveToStage(id, application.StageId);
        //    return RedirectToAction("Details", "Vacancy", new { id = id });
        //}
        //[HttpGet]
        //public ActionResult Disqualify(int id)
        //{
        //    ApplicationManager manager = new ApplicationManager();
        //    manager.Disqualify(id);
        //    return RedirectToAction("Details", "Vacancy", new { id = id });
        //}
    }
}