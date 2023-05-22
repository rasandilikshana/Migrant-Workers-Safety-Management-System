using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Reflection;
using Test.Models;

namespace Test.Controllers
{
    public class ImmigrationDepartmentRegistrationController : Controller
    {
        SqlConnection con = new SqlConnection("Data Source=ITG-DTP-SHM\\SQLEXPRESS;Database=Test;Integrated Security=True");
        SqlCommand com = new SqlCommand();
        SqlDataReader? dr;
        public IActionResult Index()
        {
            List<ImmigrationDepartmentRegistrationModel> department_of_immigrant = new List<ImmigrationDepartmentRegistrationModel>();
            con.Open();
            com.Connection = con;
            com.CommandText = "Select * from department_of_immigrant";
            dr= com.ExecuteReader();

            while(dr.Read())
            {
                var immigrant = new ImmigrationDepartmentRegistrationModel
                {
                    department_of_immigrant_id=dr.GetInt32(0),
                    department_of_immigrant_name=dr.GetString(1),
                    department_of_immigrant_address=dr.GetString(2),
                    department_of_immigrant_email=dr.GetString(3),
                    department_of_immigrant_whatsapp_number=dr.GetString(4),
                    department_of_immigrant_contact_number=dr.GetString(5),
                    department_of_immigrant_secretariats=dr.GetString(6),
                    department_of_immigrant_latitude=dr.GetString(7),
                    department_of_immigrant_longitude=dr.GetString(8)
                };
                department_of_immigrant.Add( immigrant );
            }
            ViewBag.department_of_immigrant= department_of_immigrant;
            return View();
        }

        [HttpPost]
        public IActionResult AddNewRecord(string department_of_immigrant_name,string department_of_immigrant_address,string department_of_immigrant_email,string department_of_immigrant_whatsapp_number,string department_of_immigrant_contact_number,string department_of_immigrant_secretariats,string department_of_immigrant_latitude,string department_of_immigrant_longitude)
        {
            try
            {
                con.Open();
                com.Connection = con;
                com.CommandText = "Insert into department_of_immigrant values('" + department_of_immigrant_name + "','" + department_of_immigrant_address + "','" + department_of_immigrant_email + "','" + department_of_immigrant_whatsapp_number + "','" + department_of_immigrant_contact_number + "','" + department_of_immigrant_secretariats + "','" + department_of_immigrant_latitude + "','" + department_of_immigrant_longitude + "')";
                com.ExecuteNonQuery();
                con.Close();

                TempData["message"] = "Data Saved Successfully";
                return RedirectToAction("Index", "ImmigrationDepartmentRegistration");

            }
            catch(Exception ex)
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close() ;
                }
                TempData["errormessage"] = "Data Save Failed";
                return RedirectToAction("Index", "ImmigrationDepartmentRegistration");
            }
        }
    }
}
