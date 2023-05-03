using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Test.Models;

namespace Test.Controllers
{
    public class ComplaineController : Controller
    {
        SqlConnection con = new SqlConnection("Data Source=ITG-DTP-SHM\\SQLEXPRESS;Database=Test;Integrated Security=True");
        SqlCommand com = new SqlCommand();
        SqlDataReader? dr;
        public IActionResult Index()
        {
            List<ComplaineModel> complaine = new List<ComplaineModel>();
            con.Open();
            com.Connection = con;
            com.CommandText = "Select * from complains";
            dr = com.ExecuteReader();

            while(dr.Read())
            {
                var comp = new ComplaineModel
                {
                    id = dr.GetInt32(0),
                    name = dr.GetString(1),
                    phone= dr.GetString(2),
                    country= dr.GetString(3),
                    message= dr.GetString(4),
                    latitude=dr.GetString(5),
                    longitude=dr.GetString(6)
                };
                complaine.Add(comp);
            }
            ViewBag.complaine = complaine;
            return View();
        }

        [HttpPost]
        public IActionResult AddNewRecord(string name,string phone,string country,string message,string latitude,string longitude)
        {
            try
            {
                con.Open();
                com.Connection = con;
                com.CommandText = "Insert into complains values('" + name + "','" + phone + "','" + country + "','" + message + "','" + latitude + "','" + longitude + "')";
                com.ExecuteNonQuery();
                con.Close();

                TempData["message"] = "Data Saved Successfully";
                return RedirectToAction("Index", "Complaine");
            }
            catch(Exception ex)
            {
                if(con.State == System.Data.ConnectionState.Open)
                {
                    con.Close() ;
                }
                TempData["errormessage"] = "Data Save Failed";
                return RedirectToAction("Index", "Complaine");
            }
        }
    }
}
