using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Test.Models;

namespace Test.Controllers
{
    public class UComplaineController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly SqlConnection _connection;
        private readonly SqlCommand _command;
        private SqlDataReader _dataReader;

        public UComplaineController(IConfiguration configuration)
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
            _command.CommandText = "SELECT * FROM complains";
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
            return View();
        }

        [HttpPost]
        public IActionResult AddNewRecord(string name, string phone, string country, string message, string latitude, string longitude)
        {
            try
            {
                _connection.Open();
                _command.Connection = _connection;
                _command.CommandText = "INSERT INTO complains VALUES('" + name + "','" + phone + "','" + country + "','" + message + "','" + latitude + "','" + longitude + "')";
                _command.ExecuteNonQuery();
                _connection.Close();

                TempData["message"] = "Data Saved Successfully";
                return RedirectToAction("Index", "UComplaine");
            }
            catch (Exception ex)
            {
                if (_connection.State == System.Data.ConnectionState.Open)
                {
                    _connection.Close();
                }
                TempData["errormessage"] = "Data Save Failed";
                return RedirectToAction("Index", "UComplaine");
            }
        }
    }
}
