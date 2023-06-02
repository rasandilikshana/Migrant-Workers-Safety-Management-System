using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Test.Models;

namespace Test.Controllers
{
    public class ImmigrationDepartmentRegistrationController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly SqlConnection _connection;
        private readonly SqlCommand _command;
        private SqlDataReader _dataReader;

        public ImmigrationDepartmentRegistrationController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            _command = new SqlCommand();
        }

        public IActionResult Index()
        {
            List<ImmigrationDepartmentRegistrationModel> department_of_immigrant = new List<ImmigrationDepartmentRegistrationModel>();
            _connection.Open();
            _command.Connection = _connection;
            _command.CommandText = "SELECT * FROM department_of_immigrant";
            _dataReader = _command.ExecuteReader();

            while (_dataReader.Read())
            {
                var immigrant = new ImmigrationDepartmentRegistrationModel
                {
                    department_of_immigrant_id = _dataReader.GetInt32(0),
                    department_of_immigrant_name = _dataReader.GetString(1),
                    department_of_immigrant_address = _dataReader.GetString(2),
                    department_of_immigrant_email = _dataReader.GetString(3),
                    department_of_immigrant_whatsapp_number = _dataReader.GetString(4),
                    department_of_immigrant_contact_number = _dataReader.GetString(5),
                    department_of_immigrant_secretariats = _dataReader.GetString(6),
                    department_of_immigrant_latitude = _dataReader.GetString(7),
                    department_of_immigrant_longitude = _dataReader.GetString(8)
                };
                department_of_immigrant.Add(immigrant);
            }
            ViewBag.department_of_immigrant = department_of_immigrant;
            _connection.Close();
            return View();
        }

        [HttpPost]
        public IActionResult AddNewRecord(string department_of_immigrant_name, string department_of_immigrant_address, string department_of_immigrant_email, string department_of_immigrant_whatsapp_number, string department_of_immigrant_contact_number, string department_of_immigrant_secretariats, string department_of_immigrant_latitude, string department_of_immigrant_longitude)
        {
            try
            {
                _connection.Open();
                _command.Connection = _connection;
                _command.CommandText = "INSERT INTO department_of_immigrant VALUES('" + department_of_immigrant_name + "','" + department_of_immigrant_address + "','" + department_of_immigrant_email + "','" + department_of_immigrant_whatsapp_number + "','" + department_of_immigrant_contact_number + "','" + department_of_immigrant_secretariats + "','" + department_of_immigrant_latitude + "','" + department_of_immigrant_longitude + "')";
                _command.ExecuteNonQuery();
                _connection.Close();

                TempData["message"] = "Data Saved Successfully";
                return RedirectToAction("Index", "ImmigrationDepartmentRegistration");

            }
            catch (Exception ex)
            {
                if (_connection.State == System.Data.ConnectionState.Open)
                {
                    _connection.Close();
                }
                TempData["errormessage"] = "Data Save Failed";
                return RedirectToAction("Index", "ImmigrationDepartmentRegistration");
            }
        }
    }
}
