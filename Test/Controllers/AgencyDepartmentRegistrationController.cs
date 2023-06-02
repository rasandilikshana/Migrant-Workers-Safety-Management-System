using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Test.Models;

namespace Test.Controllers
{
    public class AgencyDepartmentRegistrationController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly SqlConnection _connection;
        private readonly SqlCommand _command;
        private SqlDataReader _dataReader;

        public AgencyDepartmentRegistrationController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            _command = new SqlCommand();
        }

        public IActionResult Index()
        {
            List<AgencyDepartmentRegistrationModel> department_of_agency = new List<AgencyDepartmentRegistrationModel>();
            _connection.Open();
            _command.Connection = _connection;
            _command.CommandText = "SELECT * FROM agency";
            _dataReader = _command.ExecuteReader();

            while (_dataReader.Read())
            {
                var agency = new AgencyDepartmentRegistrationModel
                {
                    agency_id = _dataReader.GetInt32(0),
                    agency_name = _dataReader.GetString(1),
                    agency_address = _dataReader.GetString(2),
                    agency_email = _dataReader.GetString(3),
                    agency_whatsapp_number = _dataReader.GetString(4),
                    agency_contact_number = _dataReader.GetString(5),
                    agency_divisional_secretariats = _dataReader.GetString(6),
                    agency_website = _dataReader.GetString(7),
                    agency_latitude = _dataReader.GetString(8),
                    agency_longitude = _dataReader.GetString(9)
                };
                department_of_agency.Add(agency);
            }
            ViewBag.department_of_agency = department_of_agency;

            _connection.Close();

            return View();
        }

        [HttpPost]
        public IActionResult AddNewRecord(string agency_name, string agency_address, string agency_email, string agency_whatsapp_number, string agency_contact_number, string agency_divisional_secretariats, string agency_website, string agency_latitude, string agency_longitude)
        {
            try
            {
                _connection.Open();
                _command.Connection = _connection;
                _command.CommandText = "INSERT INTO agency VALUES('" + agency_name + "','" + agency_address + "','" + agency_email + "','" + agency_whatsapp_number + "','" + agency_contact_number + "','" + agency_divisional_secretariats + "','" + agency_website + "','" + agency_latitude + "','" + agency_longitude + "')";
                _command.ExecuteNonQuery();
                _connection.Close();

                TempData["message"] = "Data Saved Successfully";
                return RedirectToAction("Index", "AgencyDepartmentRegistration");

            }
            catch (Exception ex)
            {
                if (_connection.State == System.Data.ConnectionState.Open)
                {
                    _connection.Close();
                }
                TempData["errormessage"] = "Data Save Failed";
                return RedirectToAction("Index", "AgencyDepartmentRegistration");
            }
        }
    }
}
