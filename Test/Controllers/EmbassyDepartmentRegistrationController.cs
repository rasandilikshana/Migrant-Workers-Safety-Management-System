using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Test.Models;

namespace Test.Controllers
{
    public class EmbassyDepartmentRegistrationController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly SqlConnection _connection;
        private readonly SqlCommand _command;
        private SqlDataReader _dataReader;

        public EmbassyDepartmentRegistrationController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            _command = new SqlCommand();
        }

        public IActionResult Index()
        {
            List<EmbassyDepartmentRegistrationModel> department_of_embassy = new List<EmbassyDepartmentRegistrationModel>();
            _connection.Open();
            _command.Connection = _connection;
            _command.CommandText = "SELECT * FROM embassy";
            _dataReader = _command.ExecuteReader();

            while (_dataReader.Read())
            {
                var embassy = new EmbassyDepartmentRegistrationModel
                {
                    embassy_id = _dataReader.GetInt32(0),
                    embassy_name = _dataReader.GetString(1),
                    embassy_address = _dataReader.GetString(2),
                    embassy_email = _dataReader.GetString(3),
                    embassy_whatsapp_number = _dataReader.GetString(4),
                    embassy_contact_number = _dataReader.GetString(5),
                    embassy_divisional_secretariats = _dataReader.GetString(6),
                    embassy_website = _dataReader.GetString(7),
                    embassy_validcountry = _dataReader.GetString(8),
                    embassy_latitude = _dataReader.GetString(9),
                    embassy_longitude = _dataReader.GetString(10)
                };
                department_of_embassy.Add(embassy);
            }
            ViewBag.department_of_embassy = department_of_embassy;
            _connection.Close();
            return View();
        }

        [HttpPost]
        public IActionResult AddNewRecord(string embassy_name, string embassy_address, string embassy_email, string embassy_whatsapp_number, string embassy_contact_number, string embassy_divisional_secretariats, string embassy_website, string embassy_validcountry, string embassy_latitude, string embassy_longitude)
        {
            try
            {
                _connection.Open();
                _command.Connection = _connection;
                _command.CommandText = "INSERT INTO embassy VALUES('" + embassy_name + "','" + embassy_address + "','" + embassy_email + "','" + embassy_whatsapp_number + "','" + embassy_contact_number + "','" + embassy_divisional_secretariats + "','" + embassy_website + "','" + embassy_validcountry + "','" + embassy_latitude + "','" + embassy_longitude + "')";
                _command.ExecuteNonQuery();
                _connection.Close();

                TempData["message"] = "Data Saved Successfully";
                return RedirectToAction("Index", "EmbassyDepartmentRegistration");
            }
            catch (Exception ex)
            {
                if (_connection.State == System.Data.ConnectionState.Open)
                {
                    _connection.Close();
                }
                TempData["errormessage"] = "Data Save Failed";
                return RedirectToAction("Index", "EmbassyDepartmentRegistration");
            }
        }
    }
}
