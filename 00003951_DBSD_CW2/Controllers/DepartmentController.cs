using _00003951_DBSD_CW2.DataAccess;
using _00003951_DBSD_CW2.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace _00003951_DBSD_CW2.Controllers
{
    [Authorize]
    public class DepartmentController : BaseController
    {
        // GET: Department
        //public ActionResult Index()
        //{
            
        //    DepartmentManager manager = new DepartmentManager();
        //    IList<Department> list = manager.GetDepartments();
        //    return View(list);
        //}

        //// GET: Department/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: Department/Create 
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Department/Create
        //[HttpPost]
        //public ActionResult Create(Department department)
        //{
        //    try
        //    {
        //        DepartmentManager manager = new DepartmentManager();
        //        manager.CreateDepartment(department);
        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Department/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    DepartmentManager manager = new DepartmentManager();
        //    Department department = manager.GetDepartmentById(id);
        //    return View(department);
        //}

        //// POST: Department/Edit/5
        //[HttpPost]
        //public ActionResult Edit(Department department)
        //{
        //    try
        //    {
        //        DepartmentManager manager = new DepartmentManager();
        //        manager.UpdateDepartment(department);

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
