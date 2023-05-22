using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Reflection;
using Test.Models;

namespace Test.Controllers
{
    public class EmbassyOfficeUserRegistrationController : Controller
    {
        SqlConnection con = new SqlConnection("Data Source=ITG-DTP-SHM\\SQLEXPRESS;Database=Test;Integrated Security=True");
        SqlCommand com = new SqlCommand();
        SqlDataReader? dr;
        public IActionResult Index()
        {
            List<EmbassyOfficeUserRegistrationModel> office_user = new List<EmbassyOfficeUserRegistrationModel>();
            con.Open();
            com.Connection = con;
            com.CommandText = "Select * from office_user";
            dr = com.ExecuteReader();

            while (dr.Read())
            {
                var embassy_office_user = new EmbassyOfficeUserRegistrationModel()
                {
                    office_user_id = dr.GetInt32(0),
                    office_user_name = dr.GetString(1),
                    office_user_address = dr.GetString(2),
                    office_user_national_id_number = dr.GetString(3),
                    office_user_phone_number = dr.GetString(4),
                    office_user_gmail = dr.GetString(5),
                    office_user_type = dr.GetString(6),
                    office_user_password_hash = dr.GetString(7),
                    office_user_latitude = dr.GetString(8),
                    office_user_longitude = dr.GetString(9)
                };
                office_user.Add(embassy_office_user);
            }
            ViewBag.office_user = office_user;
            return View();
        }

        [HttpPost]
        public IActionResult AddNewRecord(string office_user_name, string office_user_address, string office_user_national_id_number, string office_user_phone_number, string office_user_gmail, string office_user_type, string office_user_password_hash, string office_user_latitude, string office_user_longitude)
        {
            try
            {
                con.Open();
                com.Connection = con;
                com.CommandText = "Insert into office_user values('" + office_user_name + "','" + office_user_address + "','" + office_user_national_id_number + "','" + office_user_phone_number + "','" + office_user_gmail + "','" + office_user_type + "','" + office_user_password_hash + "','" + office_user_latitude + "','" + office_user_longitude + "')";
                com.ExecuteNonQuery();
                con.Close();

                TempData["message"] = "Data Saved Successfully";
                return RedirectToAction("Index", "EmbassyOfficeUserRegistration");

            }
            catch (Exception ex)
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
                TempData["errormessage"] = "Data Save Failed";
                return RedirectToAction("Index", "EmbassyOfficeUserRegistration");
            }
        }
    }
}
