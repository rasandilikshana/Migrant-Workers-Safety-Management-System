using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Reflection;
using Test.Models;
namespace Test.Controllers
{
    public class MinistryDepartmentRegistrationController : Controller
    {
        SqlConnection con = new SqlConnection("Data Source=ITG-DTP-SHM\\SQLEXPRESS;Database=Test;Integrated Security=True");
        SqlCommand com = new SqlCommand();
        SqlDataReader? dr;
        public IActionResult Index()
        {
            List<MinistryDepartmentRegistrationModel> department_of_ministry = new List<MinistryDepartmentRegistrationModel>();
            con.Open();
            com.Connection = con;
            com.CommandText = "Select * from ministry_of_foreign_affairs";
            dr = com.ExecuteReader();

            while (dr.Read())
            {
                var ministry = new MinistryDepartmentRegistrationModel
                {
                    ministry_of_foreign_affairs_id = dr.GetInt32(0),
                    ministry_of_foreign_affairs_name = dr.GetString(1),
                    ministry_of_foreign_affairs_address = dr.GetString(2),
                    ministry_of_foreign_affairs_email = dr.GetString(3),
                    ministry_of_foreign_affairs_whatsapp_number = dr.GetString(4),
                    ministry_of_foreign_affairs_contact_number = dr.GetString(5),
                    ministry_of_foreign_affairs_divisional_secretariats = dr.GetString(6),
                    ministry_of_foreign_affairs_latitude = dr.GetString(7),
                    ministry_of_foreign_affairs_longitude = dr.GetString(8)
                };
                department_of_ministry.Add(ministry);
            }
            ViewBag.department_of_immigrant = department_of_ministry;
            return View();
        }

        [HttpPost]
        public IActionResult AddNewRecord(string ministry_of_foreign_affairs_name, string ministry_of_foreign_affairs_address, string ministry_of_foreign_affairs_email, string ministry_of_foreign_affairs_whatsapp_number, string ministry_of_foreign_affairs_contact_number, string ministry_of_foreign_affairs_divisional_secretariats, string ministry_of_foreign_affairs_latitude, string ministry_of_foreign_affairs_longitude)
        {
            try
            {
                con.Open();
                com.Connection = con;
                com.CommandText = "Insert into ministry_of_foreign_affairs values('" + ministry_of_foreign_affairs_name + "','" + ministry_of_foreign_affairs_address + "','" + ministry_of_foreign_affairs_email + "','" + ministry_of_foreign_affairs_whatsapp_number + "','" + ministry_of_foreign_affairs_contact_number + "','" + ministry_of_foreign_affairs_divisional_secretariats + "','" + ministry_of_foreign_affairs_latitude + "','" + ministry_of_foreign_affairs_longitude + "')";
                com.ExecuteNonQuery();
                con.Close();

                TempData["message"] = "Data Saved Successfully";
                return RedirectToAction("Index", "MinistryDepartmentRegistration");

            }
            catch (Exception ex)
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
                TempData["errormessage"] = "Data Save Failed";
                return RedirectToAction("Index", "MinistryDepartmentRegistration");
            }
        }
    }
}
