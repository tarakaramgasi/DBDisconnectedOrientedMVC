using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using DBDisconnectedOrientedMVC.Models;
using System.Collections.Generic;
using System;
using System.Linq;



namespace DBDisconnectedOrientedMVC.Helper
{
    internal class DBConnectionHelper
    {
        
        internal static string GetConnectionString()
        {
            var connection = new ConfigurationBuilder()
                .SetBasePath("C:\\Users\\tarak\\Desktop\\Csharp\\Inheritance\\DBDisconnectedOrientedMVC\\DBDisconnectedOrientedMVC")
                .AddJsonFile("appsettings.json").Build();
            string conStr=connection.GetConnectionString("sqlconnection");
            return conStr;
        }

        internal int InsertData(Employee employee)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(GetConnectionString()))
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from employee", con);
                da.Fill(dt);
            }
            
            

            //DataSet ds = new DataSet();
            //da.Fill(ds, "employee");

            var newRow = dt.NewRow();
            newRow["empID"]= employee.EmpID;
            newRow["empName"] = employee.EmpName;
            newRow["empEmail"] = employee.EmpEmail;
            newRow["empGender"] = employee.EmpGender;
            newRow["empDOB"] = employee.EmpDOB;
            newRow["empAge"] = employee.EmpAge;
            newRow["empDept"] = employee.EmpDept;
            newRow["empRole"] = employee.EmpRole;
            newRow["empSalary"] = employee.EmpSalary;
            dt.Rows.Add(newRow);

            using(SqlConnection con = new SqlConnection(GetConnectionString()))
            {
                con.Open();
                using(SqlCommandBuilder bldr = new SqlCommandBuilder())
                {
                    bldr.DataAdapter = new SqlDataAdapter("select * from employee", con);
                    SqlDataAdapter da = bldr.DataAdapter as SqlDataAdapter;
                    da.Update(dt);
                }
            }

            return 1; 
            
        }
        internal List<Employee> SelectEmployees()
        {
            List<Employee> list = new List<Employee>();
            
            DataTable dt = new DataTable();
            using(SqlConnection con = new SqlConnection(GetConnectionString()))
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from employee",con);
                da.Fill(dt);
            }
            foreach(DataRow row in dt.Rows)
            {
                Employee emp = new Employee();
                emp.EmpID=Convert.ToInt32(row[0]);
                emp.EmpName = Convert.ToString(row[1]);
                emp.EmpEmail = Convert.ToString(row[2]);
                emp.EmpGender = Convert.ToString(row[3]);
                emp.EmpDOB = Convert.ToDateTime(row[4]);
                emp.EmpAge = Convert.ToInt32(row[5]);
                emp.EmpDept = Convert.ToString(row[6]);
                emp.EmpRole = Convert.ToString(row[7]);
                emp.EmpSalary = Convert.ToInt32(row[8]);
                list.Add(emp);
                

            }
            return list;
        }
        internal Employee SelectEmployeesById(int empId)
        {
            List<Employee> list = new List<Employee>();

            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(GetConnectionString()))
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from employee", con);
                da.Fill(dt);
            }
            int rowNumber = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int id = Convert.ToInt32(dt.Rows[i].ItemArray[0]);
                if (id == empId)
                {
                    rowNumber = i;
                }
            }

            DataRow rowToGet = dt.Rows[rowNumber];
            Employee emp = new Employee();
            emp.EmpID = Convert.ToInt32(rowToGet[0]);
            emp.EmpName = Convert.ToString(rowToGet[1]);
            emp.EmpEmail = Convert.ToString(rowToGet[2]);
            emp.EmpGender = Convert.ToString(rowToGet[3]);
            emp.EmpDOB = Convert.ToDateTime(rowToGet[4]);
            emp.EmpAge = Convert.ToInt32(rowToGet[5]);
            emp.EmpDept = Convert.ToString(rowToGet[6]);
            emp.EmpRole = Convert.ToString(rowToGet[7]);
            emp.EmpSalary = Convert.ToInt32(rowToGet[8]);
            


            
            return emp;
        }

        internal void EditEmployees(Employee employee)
        {
            DataTable dt = new DataTable();
            using(SqlConnection con = new SqlConnection(GetConnectionString()))
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from employee",con);
                da.Fill(dt);
            }
            int rowNumber = 0;
            for (int i= 0;i<dt.Rows.Count;i++)
            {
                int id =Convert.ToInt32(dt.Rows[i].ItemArray[0]);
                if (id == employee.EmpID)
                {
                    rowNumber = i;
                }
            }
            
            DataRow rowToUpdate = dt.Rows[rowNumber];
            rowToUpdate["empName"] = employee.EmpName;
            rowToUpdate["empEmail"] = employee.EmpEmail;
            rowToUpdate["empGender"] = employee.EmpGender;
            rowToUpdate["empDOB"] = employee.EmpDOB;
            rowToUpdate["empAge"] = employee.EmpAge;
            rowToUpdate["empDept"] = employee.EmpDept;
            rowToUpdate["empRole"] = employee.EmpRole;
            rowToUpdate["empSalary"] = employee.EmpSalary;
                
        
            using(SqlConnection con = new SqlConnection(GetConnectionString()))
            {
                using(SqlCommandBuilder bldr = new SqlCommandBuilder())
                {
                    bldr.DataAdapter = new SqlDataAdapter("select * from employee", con);
                    SqlDataAdapter da = bldr.DataAdapter as SqlDataAdapter;
                    da.Update(dt);
                }
            }
  
        }

        internal void DeleteByID(int empId)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(GetConnectionString()))
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from employee", con);
                da.Fill(dt);
            }
            int rowNumber = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int id = Convert.ToInt32(dt.Rows[i].ItemArray[0]);
                if (id == empId)
                {
                    rowNumber = i;
                }
            }

            DataRow rowToDelete = dt.Rows[rowNumber];
            rowToDelete.Delete();

            using (SqlConnection con = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommandBuilder bldr = new SqlCommandBuilder())
                {
                    bldr.DataAdapter = new SqlDataAdapter("select * from employee", con);
                    SqlDataAdapter da = bldr.DataAdapter as SqlDataAdapter;
                    da.Update(dt);
                }
            }


            
        }
    }
}