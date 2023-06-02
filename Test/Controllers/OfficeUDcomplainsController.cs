using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Test.Models;

namespace Test.Controllers
{
    public class OfficeUDcomplainsController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly SqlConnection _connection;
        private readonly SqlCommand _command;
        private SqlDataReader _dataReader;

        public OfficeUDcomplainsController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            _command = new SqlCommand();
        }

        public IActionResult Index()
        {
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

            _command.CommandText = "SELECT COUNT(*) FROM users";
            int countOfUsers = (int)_command.ExecuteScalar();
            ViewBag.CountOfUsers = countOfUsers;
            _connection.Close();
            return View();
        }
    }
}
