using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;


namespace WebApplication1.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly ApplicationDbContext _Context;

        public DepartmentController(ApplicationDbContext context)
        {
            _Context = context;
        }
        public IActionResult Index()
        {
            var Deptdata = from dept in _Context.Departments select dept;
            return View(Deptdata);
        }

        public ActionResult CreateDepartment()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateDepartment(Department collection)
        {
            try
            {
                _Context.Departments.Add(collection);
                _Context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Delete(int id)
        {
            try
            {
                var Deptdetails = _Context.Departments.Single(x => x.DeptID == id);
                _Context.Departments.Remove(Deptdetails);
                _Context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult Edit(int id)
        {
            var Deptdetails = _Context.Departments.Single(x => x.DeptID == id);

            return View(Deptdetails);

        }

       
        [HttpPost]
        public ActionResult Edit(int id, Department collection)
        {
            try
            {
                Department dpt = _Context.Departments.Single(x => x.DeptID == id);
                dpt.DeptName = collection.DeptName;
                
                _Context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
