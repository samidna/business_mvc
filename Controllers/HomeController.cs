using Business.Helpers;
using Business.Models;
using Business.Services.Implements;
using Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Business.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<Employee> employees = new List<Employee>();
            DataTable dt = await SqlHelper.SelectAsync("Select * from Employees");
            foreach (DataRow item in dt.Rows)
            {
                employees.Add(new()
                {
                    Id = (int)item[0],
                    Name = (string)item[1],
                    Surname = ((string)item[2]),
                    FatherName = (string)item[3],
                    Salary = (decimal)item[4],
                    PositionId = (int)item[5],
                    BranchId = (int)item[6]
                });
            }
            return View(employees);
        }
        [HttpPost]

        public async Task<IActionResult> Employee(string name, string surname,string FatherName,int Salary,int PositionId,int BranchId)
        {
            IEmployeeService service = new EmployeeService();
            await service.AddAsync(new Employee
            {
               Name = name,
               Surname=surname,
               FatherName=FatherName,
               Salary=Salary,
               PositionId=PositionId,
               BranchId=BranchId
            });
            return RedirectToAction(nameof(EmployeeGetAll));
        }
        public async Task<IActionResult> EmployeeGetAll()
        {
            IEmployeeService service = new EmployeeService();
            return Json(await service.GetAllAsync());
        }
        public async Task<IActionResult> EmployeeGetById(int id)
        {
            IEmployeeService service = new EmployeeService();
            return Json(await service.GetByIdAsync(id));
        }
        public async Task<IActionResult> EmployeeDelete(int id)
        {
            IEmployeeService service = new EmployeeService();
            if (await service.Delete(id) > 0)
            {
                return Ok();
            }
            return NotFound();
        }

    }
}
