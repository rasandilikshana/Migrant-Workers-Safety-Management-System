using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Test.Models;

namespace Test.Controllers
{
    public class SignUpController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly SqlConnection _connection;
        private readonly SqlCommand _command;
        private SqlDataReader _dataReader;

        public SignUpController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            _command = new SqlCommand();
        }

        public IActionResult Index()
        {
            List<SignUpModel> Users = new List<SignUpModel>();
            _connection.Open();
            _command.Connection = _connection;
            _command.CommandText = "SELECT * FROM users";
            _dataReader = _command.ExecuteReader();

            while (_dataReader.Read())
            {
                var usr = new SignUpModel
                {
                    user_id = _dataReader.GetInt32(0),
                    users_name = _dataReader.GetString(1),
                    users_address = _dataReader.GetString(2),
                    users_gender = _dataReader.GetString(3),
                    users_email = _dataReader.GetString(4),
                    users_contact_number = _dataReader.GetString(5),
                    users_national_id_number = _dataReader.GetString(6),
                    users_passport_number = _dataReader.GetString(7),
                    users_type = _dataReader.GetString(8),
                    users_agency = _dataReader.GetString(9),
                    users_password_hash = _dataReader.GetString(10),
                    country = _dataReader.GetString(11),
                    users_latitude = _dataReader.GetString(12),
                    users_longitude = _dataReader.GetString(13),
                };
                Users.Add(usr);
            }
            ViewBag.Users = Users;
            _connection.Close();
            return View();
        }

        [HttpPost]
        public IActionResult AddNewRecord(string users_name, string users_address, string users_gender, string users_email, string users_contact_number, string users_national_id_number, string users_passport_number, string users_type, string users_agency, string users_password_hash, string country, string users_latitude, string users_longitude)
        {
            try
            {
                _connection.Open();
                _command.Connection = _connection;
                _command.CommandText = "INSERT INTO users VALUES('" + users_name + "','" + users_address + "','" + users_gender + "','" + users_email + "','" + users_contact_number + "','" + users_national_id_number + "','" + users_passport_number + "','" + users_type + "','" + users_agency + "','" + users_password_hash + "','" + country + "','" + users_latitude + "','" + users_longitude + "')";
                _command.ExecuteNonQuery();
                _connection.Close();

                TempData["message"] = "Data Saved Successfully";
                return RedirectToAction("Index", "SignUp");
            }
            catch (Exception ex)
            {
                if (_connection.State == System.Data.ConnectionState.Open)
                {
                    _connection.Close();
                }
                TempData["errormessage"] = "Data Save Failed";
                return RedirectToAction("Index", "SignUp");
            }
        }
    }
}
