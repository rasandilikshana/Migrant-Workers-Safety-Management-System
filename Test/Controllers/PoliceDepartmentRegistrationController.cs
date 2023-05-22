using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Reflection;
using Test.Models;

namespace Test.Controllers
{
    public class PoliceDepartmentRegistrationController : Controller
    {
        SqlConnection con = new SqlConnection("Data Source=ITG-DTP-SHM\\SQLEXPRESS;Database=Test;Integrated Security=True");
        SqlCommand com = new SqlCommand();
        SqlDataReader? dr;
        public IActionResult Index()
        {
            List<PoliceDepartmentRegistrationModel> department_of_police = new List<PoliceDepartmentRegistrationModel>();
            con.Open();
            com.Connection = con;
            com.CommandText = "Select * from police_department";
            dr = com.ExecuteReader();

            while (dr.Read())
            {
                var police = new PoliceDepartmentRegistrationModel
                {
                    police_id = dr.GetInt32(0),
                    police_name = dr.GetString(1),
                    police_address = dr.GetString(2),
                    police_email = dr.GetString(3),
                    police_whatsapp_number = dr.GetString(4),
                    police_contact_number = dr.GetString(5),
                    police_divisional_secretariats = dr.GetString(6),
                    police_latitude = dr.GetString(7),
                    police_longitude = dr.GetString(8)
                };
                department_of_police.Add(police);
            }
            ViewBag.department_of_police = department_of_police;
            return View();
        }

        [HttpPost]
        public IActionResult AddNewRecord(string police_name, string police_address, string police_email, string police_whatsapp_number, string police_contact_number, string police_divisional_secretariats, string police_latitude, string police_longitude)
        {
            try
            {
                con.Open();
                com.Connection = con;
                com.CommandText = "Insert into police_department values('" + police_name + "','" + police_address + "','" + police_email + "','" + police_whatsapp_number + "','" + police_contact_number + "','" + police_divisional_secretariats + "','" + police_latitude + "','" + police_longitude + "')";
                com.ExecuteNonQuery();
                con.Close();

                TempData["message"] = "Data Saved Successfully";
                return RedirectToAction("Index", "PoliceDepartmentRegistration");

            }
            catch (Exception ex)
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
                TempData["errormessage"] = "Data Save Failed";
                return RedirectToAction("Index", "PoliceDepartmentRegistration");
            }
        }
    }
}
