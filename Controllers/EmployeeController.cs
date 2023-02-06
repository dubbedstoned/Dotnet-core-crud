using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _Context;

        public EmployeeController(ApplicationDbContext context)
        {
            _Context = context;
        }

        public IActionResult Index()
        {
            var data=_Context.Employees.FromSqlRaw("exec sp_GetAllEmployeetList ").ToList();
            return View(data);
        }

        public IActionResult CreateEmployee()
        {
           
            return View();  
        }

        [HttpPost]
        public IActionResult CreateEmployee(Employee model)
        {

            _Context.Database.ExecuteSqlRaw($"sp_CreateEmployee @name='{model.EmpName}',@address='{model.EmpAddress}',@deptid='{model.DeptID}' ");
            TempData["Success"] = "Record is Successfully inserted";
            return RedirectToAction("Index");

        }
        public IActionResult Delete(int id)
        {
            if (id==0)
            {
                return NotFound();
            }
            var data = _Context.Database.ExecuteSqlRaw($"sp_DeleteEmployee'{id}'");
            TempData["Success"] = "Record is Successfully Deleted";
            return RedirectToAction("Index");

        }

        public IActionResult Edit(int id)
        {
            if (id==0)
            {
                return NotFound();
            }
            else
            {
                var data = _Context.Employees.FromSqlRaw($"sp_GetEmployeeById'{id}'");
                Employee emp=new Employee();  
                foreach (var item in data )
                {
                    emp.EmpID = item.EmpID;
                    emp.EmpName = item.EmpName;
                    emp.EmpAddress = item.EmpAddress;   
                    emp.DeptID = item.DeptID;   
                }
                return View(emp);  
            }
        }
        [HttpPost]
        public IActionResult Edit(Employee model)
        {
            var parameter = $"sp_UpdateEmployee @id='{model.EmpID}',@name='{model.EmpName}',@address='{model.EmpAddress}',@deptid='{model.DeptID}'";
            TempData["Success"] = "Record is Successfully updated";
            return RedirectToAction("Index");
        }
    }
}
