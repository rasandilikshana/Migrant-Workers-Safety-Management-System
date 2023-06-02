using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Test.Models;

namespace Test.Controllers
{
    public class OfficeUserDashboardController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly SqlConnection _connection;
        private readonly SqlCommand _command;
        private SqlDataReader _dataReader;
        public OfficeUserDashboardController(IConfiguration configuration)
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
            _command.CommandText = "Select * from users ORDER BY user_id DESC";
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
            _dataReader.Close();

            _command.CommandText = "SELECT COUNT(*) FROM users";
            int countOfUsers = (int)_command.ExecuteScalar();
            ViewBag.CountOfUsers = countOfUsers;

            _connection.Close();

            List<ComplaineModel> complaine = new List<ComplaineModel>();
            _connection.Open();
            _command.Connection = _connection;
            _command.CommandText = "Select * from complains ORDER BY id DESC";
            _dataReader = _command.ExecuteReader();

            while (_dataReader.Read())
            {
                var comp = new ComplaineModel
                {
                    id = _dataReader.GetInt32(0),
                    name = _dataReader.GetString(1),
                    phone = _dataReader.GetString(2),
                    country = _dataReader.GetString(3),
                    message = _dataReader.GetString(4),
                    latitude = _dataReader.GetString(5),
                    longitude = _dataReader.GetString(6)
                };
                complaine.Add(comp);
            }
            ViewBag.complaine = complaine;
            _dataReader.Close();

            _command.CommandText = "SELECT COUNT(*) FROM complains";
            int countOfComplaines = (int)_command.ExecuteScalar();
            ViewBag.countOfComplaines = countOfComplaines;

            _connection.Close();
            return View();
        }
    }
}
