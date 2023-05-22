using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Diagnostics;
using Test.Models;
using System.Reflection;

namespace Test.Controllers
{
    public class SignUpController : Controller
    {
        SqlConnection con = new SqlConnection("Data Source=ITG-DTP-SHM\\SQLEXPRESS;Database=Test;Integrated Security=True");
        SqlCommand com = new SqlCommand();
        SqlDataReader? dr;

        public IActionResult Index()
        {
            List<SignUpModel> Users = new List<SignUpModel>();
            con.Open();
            com.Connection = con;
            com.CommandText = "Select * from users";
            dr = com.ExecuteReader();
            
            while (dr.Read())
            {
                var usr = new SignUpModel
                {
                    user_id = dr.GetInt32(0),
                    users_name = dr.GetString(1),
                    users_address = dr.GetString(2),
                    users_gender = dr.GetString(3),
                    users_email = dr.GetString(4),
                    users_contact_number = dr.GetString(5),
                    users_national_id_number = dr.GetString(6),
                    users_passport_number = dr.GetString(7),
                    users_type = dr.GetString(8),
                    users_agency = dr.GetString(9),
                    users_password_hash= dr.GetString(10),
                    country=dr.GetString(11),
                    users_latitude=dr.GetString(12),
                    users_longitude=dr.GetString(13),
                };
                Users.Add(usr);
            }
            ViewBag.Users = Users;

            return View();
        }

        [HttpPost]
        public IActionResult AddNewRecord(string users_name,string users_address, string users_gender,string users_email,string users_contact_number,string users_national_id_number,string users_passport_number,string users_type,string users_agency, string users_password_hash,string country,string users_latitude,string users_longitude)
        {
            try
            {
                con.Open();
                com.Connection = con;
                com.CommandText = "Insert into users values('" + users_name + "','" + users_address + "','" + users_gender + "','" + users_email + "','" + users_contact_number + "','" + users_national_id_number + "','" + users_passport_number + "','" + users_type + "','" + users_agency + "','" + users_password_hash + "','" + country + "','" + users_latitude + "','" + users_longitude + "')";
                com.ExecuteNonQuery();
                con.Close();

                TempData["message"] = "Data Saved Successfully";
                return RedirectToAction("Index", "SignUp");
            }catch(Exception ex)
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
                TempData["errormessage"] = "Data Save Failed";
                return RedirectToAction("Index", "SignUp");
            }
        }
    }
}
