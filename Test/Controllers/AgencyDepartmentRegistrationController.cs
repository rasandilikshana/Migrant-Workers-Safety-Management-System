using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Reflection;
using Test.Models;

namespace Test.Controllers
{
    public class AgencyDepartmentRegistrationController : Controller
    {
        SqlConnection con = new SqlConnection("Data Source=ITG-DTP-SHM\\SQLEXPRESS;Database=Test;Integrated Security=True");
        SqlCommand com = new SqlCommand();
        SqlDataReader? dr;
        public IActionResult Index()
        {
            List<AgencyDepartmentRegistrationModel> department_of_agency = new List<AgencyDepartmentRegistrationModel>();
            con.Open();
            com.Connection = con;
            com.CommandText = "Select * from agency";
            dr = com.ExecuteReader();

            while(dr.Read())
            {
                var agency = new AgencyDepartmentRegistrationModel
                {
                    agency_id = dr.GetInt32(0),
                    agency_name= dr.GetString(1),
                    agency_address= dr.GetString(2),
                    agency_email= dr.GetString(3),
                    agency_whatsapp_number= dr.GetString(4),
                    agency_contact_number= dr.GetString(5),
                    agency_divisional_secretariats= dr.GetString(6),
                    agency_website= dr.GetString(7),
                    agency_latitude=dr.GetString(8),
                    agency_longitude=dr.GetString(9)
                };
                department_of_agency.Add(agency);
            }
            ViewBag.department_of_agency = department_of_agency;
            return View();
        }

        [HttpPost]
        public IActionResult AddNewRecord(string agency_name, string agency_address, string agency_email, string agency_whatsapp_number, string agency_contact_number, string agency_divisional_secretariats, string agency_website, string agency_latitude, string agency_longitude)
        {
            try
            {
                con.Open();
                com.Connection = con;
                com.CommandText = "Insert into agency values('" + agency_name + "','" + agency_address + "','" + agency_email + "','" + agency_whatsapp_number + "','" + agency_contact_number + "','" + agency_divisional_secretariats + "','" + agency_website + "','" + agency_latitude + "','" + agency_longitude + "')";
                com.ExecuteNonQuery();
                con.Close();

                TempData["message"] = "Data Saved Successfully";
                return RedirectToAction("Index", "AgencyDepartmentRegistration");

            }
            catch (Exception ex)
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
                TempData["errormessage"] = "Data Save Failed";
                return RedirectToAction("Index", "AgencyDepartmentRegistration");
            }
        }
    }
}
