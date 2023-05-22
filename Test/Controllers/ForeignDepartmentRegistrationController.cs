using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Reflection;
using Test.Models;

namespace Test.Controllers
{
    public class ForeignDepartmentRegistrationController : Controller
    {
        SqlConnection con = new SqlConnection("Data Source=ITG-DTP-SHM\\SQLEXPRESS;Database=Test;Integrated Security=True");
        SqlCommand com = new SqlCommand();
        SqlDataReader? dr;
        public IActionResult Index()
        {
            List<ForeignDepartmentRegistrationModel> department_of_foreign = new List<ForeignDepartmentRegistrationModel>();
            con.Open();
            com.Connection = con;
            com.CommandText = "Select * from foreign_employment_bureau";
            dr = com.ExecuteReader();

            while (dr.Read())
            {
                var foreign = new ForeignDepartmentRegistrationModel
                {
                    foreign_employment_bureau_id = dr.GetInt32(0),
                    foreign_employment_bureau_name = dr.GetString(1),
                    foreign_employment_bureau_address = dr.GetString(2),
                    foreign_employment_bureau_email = dr.GetString(3),
                    foreign_employment_bureau_whatsapp_number = dr.GetString(4),
                    foreign_employment_bureau_contact_number = dr.GetString(5),
                    foreign_employment_bureau_divisional_secretariats = dr.GetString(6),
                    foreign_employment_bureau_latitude = dr.GetString(7),
                    foreign_employment_bureau_longitude = dr.GetString(8)
                };
                department_of_foreign.Add(foreign);
            }
            ViewBag.department_of_foreign = department_of_foreign;
            return View();
        }

        [HttpPost]
        public IActionResult AddNewRecord(string foreign_employment_bureau_name, string foreign_employment_bureau_address, string foreign_employment_bureau_email, string foreign_employment_bureau_whatsapp_number, string foreign_employment_bureau_contact_number, string foreign_employment_bureau_divisional_secretariats, string foreign_employment_bureau_latitude, string foreign_employment_bureau_longitude)
        {
            try
            {
                con.Open();
                com.Connection = con;
                com.CommandText = "Insert into foreign_employment_bureau values('" + foreign_employment_bureau_name + "','" + foreign_employment_bureau_address + "','" + foreign_employment_bureau_email + "','" + foreign_employment_bureau_whatsapp_number + "','" + foreign_employment_bureau_contact_number + "','" + foreign_employment_bureau_divisional_secretariats + "','" + foreign_employment_bureau_latitude + "','" + foreign_employment_bureau_longitude + "')";
                com.ExecuteNonQuery();
                con.Close();

                TempData["message"] = "Data Saved Successfully";
                return RedirectToAction("Index", "ForeignDepartmentRegistration");

            }
            catch (Exception ex)
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
                TempData["errormessage"] = "Data Save Failed";
                return RedirectToAction("Index", "ForeignDepartmentRegistration");
            }
        }
    }
}
