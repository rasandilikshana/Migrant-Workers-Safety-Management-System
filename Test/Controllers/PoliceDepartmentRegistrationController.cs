using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Test.Models;

namespace Test.Controllers
{
    public class PoliceDepartmentRegistrationController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly SqlConnection _connection;
        private readonly SqlCommand _command;
        private SqlDataReader _dataReader;

        public PoliceDepartmentRegistrationController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            _command = new SqlCommand();
        }

        public IActionResult Index()
        {
            List<PoliceDepartmentRegistrationModel> department_of_police = new List<PoliceDepartmentRegistrationModel>();
            _connection.Open();
            _command.Connection = _connection;
            _command.CommandText = "SELECT * FROM police_department";
            _dataReader = _command.ExecuteReader();

            while (_dataReader.Read())
            {
                var police = new PoliceDepartmentRegistrationModel
                {
                    police_id = _dataReader.GetInt32(0),
                    police_name = _dataReader.GetString(1),
                    police_address = _dataReader.GetString(2),
                    police_email = _dataReader.GetString(3),
                    police_whatsapp_number = _dataReader.GetString(4),
                    police_contact_number = _dataReader.GetString(5),
                    police_divisional_secretariats = _dataReader.GetString(6),
                    police_latitude = _dataReader.GetString(7),
                    police_longitude = _dataReader.GetString(8)
                };
                department_of_police.Add(police);
            }
            ViewBag.department_of_police = department_of_police;
            _connection.Close();
            return View();
        }

        [HttpPost]
        public IActionResult AddNewRecord(string police_name, string police_address, string police_email, string police_whatsapp_number, string police_contact_number, string police_divisional_secretariats, string police_latitude, string police_longitude)
        {
            try
            {
                _connection.Open();
                _command.Connection = _connection;
                _command.CommandText = "INSERT INTO police_department VALUES('" + police_name + "','" + police_address + "','" + police_email + "','" + police_whatsapp_number + "','" + police_contact_number + "','" + police_divisional_secretariats + "','" + police_latitude + "','" + police_longitude + "')";
                _command.ExecuteNonQuery();
                _connection.Close();

                TempData["message"] = "Data Saved Successfully";
                return RedirectToAction("Index", "PoliceDepartmentRegistration");

            }
            catch (Exception ex)
            {
                if (_connection.State == System.Data.ConnectionState.Open)
                {
                    _connection.Close();
                }
                TempData["errormessage"] = "Data Save Failed";
                return RedirectToAction("Index", "PoliceDepartmentRegistration");
            }
        }
    }
}
