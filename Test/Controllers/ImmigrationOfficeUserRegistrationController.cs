using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Test.Models;

namespace Test.Controllers
{
    public class ImmigrationOfficeUserRegistrationController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly SqlConnection _connection;
        private readonly SqlCommand _command;
        private SqlDataReader _dataReader;

        public ImmigrationOfficeUserRegistrationController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            _command = new SqlCommand();
        }

        public IActionResult Index()
        {
            List<ImmigrationOfficeUserRegistrationModel> office_user = new List<ImmigrationOfficeUserRegistrationModel>();
            _connection.Open();
            _command.Connection = _connection;
            _command.CommandText = "SELECT * FROM office_user";
            _dataReader = _command.ExecuteReader();

            while (_dataReader.Read())
            {
                var immigrant_office_user = new ImmigrationOfficeUserRegistrationModel
                {
                    office_user_id = _dataReader.GetInt32(0),
                    office_user_name = _dataReader.GetString(1),
                    office_user_address = _dataReader.GetString(2),
                    office_user_national_id_number = _dataReader.GetString(3),
                    office_user_phone_number = _dataReader.GetString(4),
                    office_user_gmail = _dataReader.GetString(5),
                    office_user_type = _dataReader.GetString(6),
                    office_user_password_hash = _dataReader.GetString(7),
                    office_user_latitude = _dataReader.GetString(8),
                    office_user_longitude = _dataReader.GetString(9)
                };
                office_user.Add(immigrant_office_user);
            }
            ViewBag.office_user = office_user;
            _connection.Close();
            return View();
        }

        [HttpPost]
        public IActionResult AddNewRecord(string office_user_name, string office_user_address, string office_user_national_id_number, string office_user_phone_number, string office_user_gmail, string office_user_type, string office_user_password_hash, string office_user_latitude, string office_user_longitude)
        {
            try
            {
                _connection.Open();
                _command.Connection = _connection;
                _command.CommandText = "INSERT INTO office_user VALUES('" + office_user_name + "','" + office_user_address + "','" + office_user_national_id_number + "','" + office_user_phone_number + "','" + office_user_gmail + "','" + office_user_type + "','" + office_user_password_hash + "','" + office_user_latitude + "','" + office_user_longitude + "')";
                _command.ExecuteNonQuery();
                _connection.Close();

                TempData["message"] = "Data Saved Successfully";
                return RedirectToAction("Index", "ImmigrationOfficeUserRegistration");

            }
            catch (Exception ex)
            {
                if (_connection.State == System.Data.ConnectionState.Open)
                {
                    _connection.Close();
                }
                TempData["errormessage"] = "Data Save Failed";
                return RedirectToAction("Index", "ImmigrationOfficeUserRegistration");
            }
        }
    }
}
