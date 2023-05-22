using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Reflection;
using Test.Models;

namespace Test.Controllers
{
    public class EmbassyDepartmentRegistrationController : Controller
    {
        SqlConnection con = new SqlConnection("Data Source=ITG-DTP-SHM\\SQLEXPRESS;Database=Test;Integrated Security=True");
        SqlCommand com = new SqlCommand();
        SqlDataReader? dr;
        public IActionResult Index()
        {
            List<EmbassyDepartmentRegistrationModel> department_of_embassy = new List<EmbassyDepartmentRegistrationModel>();
            con.Open();
            com.Connection = con;
            com.CommandText = "Select * from embassy";
            dr = com.ExecuteReader();

            while (dr.Read())
            {
                var embassy = new EmbassyDepartmentRegistrationModel
                {
                    embassy_id=dr.GetInt32(0),
                    embassy_name=dr.GetString(1),
                    embassy_address=dr.GetString(2),
                    embassy_email=dr.GetString(3),
                    embassy_whatsapp_number=dr.GetString(4),
                    embassy_contact_number=dr.GetString(5),
                    embassy_divisional_secretariats=dr.GetString(6),
                    embassy_website=dr.GetString(7),
                    embassy_validcountry=dr.GetString(8),
                    embassy_latitude=dr.GetString(9),
                    embassy_longitude=dr.GetString(10)
                };
                department_of_embassy.Add(embassy);
            }
            ViewBag.department_of_embassy = department_of_embassy;
            return View();
        }

        [HttpPost]
        public IActionResult AddNewRecord(string embassy_name, string embassy_address, string embassy_email, string embassy_whatsapp_number, string embassy_contact_number, string embassy_divisional_secretariats, string embassy_website, string embassy_validcountry, string embassy_latitude, string embassy_longitude)
        {
            try
            {
                con.Open();
                com.Connection = con;
                com.CommandText = "Insert into embassy values('" + embassy_name + "','" + embassy_address + "','" + embassy_email + "','" + embassy_whatsapp_number + "','" + embassy_contact_number + "','" + embassy_divisional_secretariats + "','" + embassy_website + "','" + embassy_validcountry + "','" + embassy_latitude + "','" + embassy_longitude + "')";
                com.ExecuteNonQuery();
                con.Close();

                TempData["message"] = "Data Saved Successfully";
                return RedirectToAction("Index", "EmbassyDepartmentRegistration");

            }
            catch (Exception ex)
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
                TempData["errormessage"] = "Data Save Failed";
                return RedirectToAction("Index", "EmbassyDepartmentRegistration");
            }
        }
    }
}
