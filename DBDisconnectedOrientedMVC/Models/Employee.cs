using System;

namespace DBDisconnectedOrientedMVC.Models
{
    public class Employee
    {
        public int EmpID { get; set; }
        public string EmpName { get; set; }
        public string EmpEmail { get; set; }
        public string EmpGender { get; set; }
        public DateTime EmpDOB { get; set; }
        public int EmpAge { get; set; }
        public string EmpDept { get; set; }
        public string EmpRole { get; set; }
        public int EmpSalary { get; set; }
    }
}