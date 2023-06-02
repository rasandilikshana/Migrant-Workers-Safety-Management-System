using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Test.Models;

namespace Test.Controllers
{
    public class MinistryDepartmentRegistrationController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly SqlConnection _connection;
        private readonly SqlCommand _command;
        private SqlDataReader _dataReader;

        public MinistryDepartmentRegistrationController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            _command = new SqlCommand();
        }

        public IActionResult Index()
        {
            List<MinistryDepartmentRegistrationModel> department_of_ministry = new List<MinistryDepartmentRegistrationModel>();
            _connection.Open();
            _command.Connection = _connection;
            _command.CommandText = "SELECT * FROM ministry_of_foreign_affairs";
            _dataReader = _command.ExecuteReader();

            while (_dataReader.Read())
            {
                var ministry = new MinistryDepartmentRegistrationModel
                {
                    ministry_of_foreign_affairs_id = _dataReader.GetInt32(0),
                    ministry_of_foreign_affairs_name = _dataReader.GetString(1),
                    ministry_of_foreign_affairs_address = _dataReader.GetString(2),
                    ministry_of_foreign_affairs_email = _dataReader.GetString(3),
                    ministry_of_foreign_affairs_whatsapp_number = _dataReader.GetString(4),
                    ministry_of_foreign_affairs_contact_number = _dataReader.GetString(5),
                    ministry_of_foreign_affairs_divisional_secretariats = _dataReader.GetString(6),
                    ministry_of_foreign_affairs_latitude = _dataReader.GetString(7),
                    ministry_of_foreign_affairs_longitude = _dataReader.GetString(8)
                };
                department_of_ministry.Add(ministry);
            }
            ViewBag.department_of_immigrant = department_of_ministry;
            _connection.Close();
            return View();
        }

        [HttpPost]
        public IActionResult AddNewRecord(string ministry_of_foreign_affairs_name, string ministry_of_foreign_affairs_address, string ministry_of_foreign_affairs_email, string ministry_of_foreign_affairs_whatsapp_number, string ministry_of_foreign_affairs_contact_number, string ministry_of_foreign_affairs_divisional_secretariats, string ministry_of_foreign_affairs_latitude, string ministry_of_foreign_affairs_longitude)
        {
            try
            {
                _connection.Open();
                _command.Connection = _connection;
                _command.CommandText = "INSERT INTO ministry_of_foreign_affairs VALUES('" + ministry_of_foreign_affairs_name + "','" + ministry_of_foreign_affairs_address + "','" + ministry_of_foreign_affairs_email + "','" + ministry_of_foreign_affairs_whatsapp_number + "','" + ministry_of_foreign_affairs_contact_number + "','" + ministry_of_foreign_affairs_divisional_secretariats + "','" + ministry_of_foreign_affairs_latitude + "','" + ministry_of_foreign_affairs_longitude + "')";
                _command.ExecuteNonQuery();
                _connection.Close();

                TempData["message"] = "Data Saved Successfully";
                return RedirectToAction("Index", "MinistryDepartmentRegistration");

            }
            catch (Exception ex)
            {
                if (_connection.State == System.Data.ConnectionState.Open)
                {
                    _connection.Close();
                }
                TempData["errormessage"] = "Data Save Failed";
                return RedirectToAction("Index", "MinistryDepartmentRegistration");
            }
        }
    }
}
