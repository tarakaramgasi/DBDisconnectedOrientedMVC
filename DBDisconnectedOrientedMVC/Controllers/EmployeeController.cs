using DBDisconnectedOrientedMVC.Models;
using DBDisconnectedOrientedMVC.Helper;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;

namespace DBDisconnectedOrientedMVC.Controllers
{
    public class EmployeeController : Controller
    {
        internal string _dbContext = DBConnectionHelper.GetConnectionString();
        // GET: Employee
        [HttpGet]
        public ActionResult Index()
        {
            DBConnectionHelper obj = new DBConnectionHelper();
            List<Employee> employees = obj.SelectEmployees();
            return View(employees);
        }
        [HttpPost]
        public ActionResult Index(Employee employee)
        {
            
            return View();
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            DBConnectionHelper obj = new DBConnectionHelper();
            if (obj.InsertData(employee) == 1)
            {
                Console.WriteLine("Row inserted successfully");
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit() 
        {
            return View();
        }
        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
            DBConnectionHelper obj = new DBConnectionHelper();
            obj.EditEmployees(employee);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Details(object id)
        {
            Employee emp = new DBConnectionHelper().SelectEmployeesById(Convert.ToInt32(id));
            return View(emp);
        }
        [HttpGet]
        public ActionResult Delete(object id)
        {
            new DBConnectionHelper().DeleteByID(Convert.ToInt32(id));
            return RedirectToAction("Index");
        }
    }
}