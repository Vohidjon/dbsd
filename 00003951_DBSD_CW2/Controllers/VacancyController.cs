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
    public class VacancyController : BaseController
    {
        // GET: Vacancy
        public ActionResult Index(string fTitle, string fDescription)
        {
            Recruiter recruiter = this.getRecruiter();
            VacancyManager manager = new VacancyManager();
            DepartmentManager depManager = new DepartmentManager();
            IList<Vacancy> list = manager.FilterVacancies(recruiter.RecruiterId, fTitle, fDescription);
            IList<Department> departments = depManager.GetDepartments();
            foreach(Vacancy item in list)
            {
                item.Department = departments.First(d => d.DepartmentId == item.DepartmentId);
            }
            ViewBag.fTitle = fTitle;
            ViewBag.fDescription = fDescription;
            return View(list);
        }

        // GET: Vacancy/Details/5
        public ActionResult Details(int id)
        {
            VacancyManager vacancyManager = new VacancyManager();
            Vacancy vacancy = vacancyManager.GetVacancyById(id);
            ApplicationManager manager = new ApplicationManager();
            IList<Application> list = manager.GetApplications(id);
            StageManager stageManager = new StageManager();
            foreach(Application item in list)
            {
                item.Stage = stageManager.GetStageById(item.StageId);
            }
            ViewBag.VacancyTitle = vacancy.VacancyTitle;
            return View(list);
        }

        // GET: Vacancy/Create
        public ActionResult Create()
        {
            DepartmentManager manager = new DepartmentManager();
            IList<Department> departments = manager.GetDepartments();
            VacancyWrapper wrapper = new VacancyWrapper();
            wrapper.vacancy = new Vacancy();
            wrapper.departments = departments;
            return View(wrapper);
        }

        // POST: Vacancy/Create
        [HttpPost]
        public ActionResult Create(Vacancy vacancy)
        {
            try
            {
                VacancyManager manager = new VacancyManager();
                vacancy.RecruiterId = this.getRecruiter().RecruiterId;
                manager.CreateVacancy(vacancy);

                return RedirectToAction("Index");
            }
            catch
            {
                DepartmentManager manager = new DepartmentManager();
                IList<Department> departments = manager.GetDepartments();
                VacancyWrapper wrapper = new VacancyWrapper();
                wrapper.vacancy = vacancy;
                wrapper.departments = departments;
                return View(wrapper);
            }
        }

        // GET: Vacancy/Edit/5
        public ActionResult Edit(int id)
        {
            VacancyManager vacancyManager = new VacancyManager();
            DepartmentManager manager = new DepartmentManager();
            IList<Department> departments = manager.GetDepartments();
            VacancyWrapper wrapper = new VacancyWrapper();
            wrapper.vacancy = vacancyManager.GetVacancyById(id);
            wrapper.departments = departments;
            return View(wrapper);
        }

        // POST: Vacancy/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Vacancy vacancy)
        {
            try
            {
                VacancyManager manager = new VacancyManager();
                manager.UpdateVacancy(id, vacancy);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Report(int? vacancyId)
        {
            VacancyManager manager = new VacancyManager();
            Recruiter recruiter = this.getRecruiter();
            IList<Report> list = new List<Report>();
            ViewBag.vacancies = manager.GetVacancies(recruiter.RecruiterId);
            ViewBag.vacancyId = vacancyId;
            if(vacancyId.HasValue)
            {
                list = manager.GenerateReport(vacancyId.Value, recruiter.RecruiterId);
            }

            return View(list);
        }

        // GET: Vacancy/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Vacancy/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
