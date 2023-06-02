using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Test.Models;

namespace Test.Controllers
{
    public class ForeignDepartmentRegistrationController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly SqlConnection _connection;
        private readonly SqlCommand _command;
        private SqlDataReader _dataReader;

        public ForeignDepartmentRegistrationController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            _command = new SqlCommand();
        }

        public IActionResult Index()
        {
            List<ForeignDepartmentRegistrationModel> department_of_foreign = new List<ForeignDepartmentRegistrationModel>();
            _connection.Open();
            _command.Connection = _connection;
            _command.CommandText = "SELECT * FROM foreign_employment_bureau";
            _dataReader = _command.ExecuteReader();

            while (_dataReader.Read())
            {
                var foreign = new ForeignDepartmentRegistrationModel
                {
                    foreign_employment_bureau_id = _dataReader.GetInt32(0),
                    foreign_employment_bureau_name = _dataReader.GetString(1),
                    foreign_employment_bureau_address = _dataReader.GetString(2),
                    foreign_employment_bureau_email = _dataReader.GetString(3),
                    foreign_employment_bureau_whatsapp_number = _dataReader.GetString(4),
                    foreign_employment_bureau_contact_number = _dataReader.GetString(5),
                    foreign_employment_bureau_divisional_secretariats = _dataReader.GetString(6),
                    foreign_employment_bureau_latitude = _dataReader.GetString(7),
                    foreign_employment_bureau_longitude = _dataReader.GetString(8)
                };
                department_of_foreign.Add(foreign);
            }
            ViewBag.department_of_foreign = department_of_foreign;
            _connection.Close();
            return View();
        }

        [HttpPost]
        public IActionResult AddNewRecord(string foreign_employment_bureau_name, string foreign_employment_bureau_address, string foreign_employment_bureau_email, string foreign_employment_bureau_whatsapp_number, string foreign_employment_bureau_contact_number, string foreign_employment_bureau_divisional_secretariats, string foreign_employment_bureau_latitude, string foreign_employment_bureau_longitude)
        {
            try
            {
                _connection.Open();
                _command.Connection = _connection;
                _command.CommandText = "INSERT INTO foreign_employment_bureau VALUES('" + foreign_employment_bureau_name + "','" + foreign_employment_bureau_address + "','" + foreign_employment_bureau_email + "','" + foreign_employment_bureau_whatsapp_number + "','" + foreign_employment_bureau_contact_number + "','" + foreign_employment_bureau_divisional_secretariats + "','" + foreign_employment_bureau_latitude + "','" + foreign_employment_bureau_longitude + "')";
                _command.ExecuteNonQuery();
                _connection.Close();

                TempData["message"] = "Data Saved Successfully";
                return RedirectToAction("Index", "ForeignDepartmentRegistration");

            }
            catch (Exception ex)
            {
                if (_connection.State == System.Data.ConnectionState.Open)
                {
                    _connection.Close();
                }
                TempData["errormessage"] = "Data Save Failed";
                return RedirectToAction("Index", "ForeignDepartmentRegistration");
            }
        }
    }
}
