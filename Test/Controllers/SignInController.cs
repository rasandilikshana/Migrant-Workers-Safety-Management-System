using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Test.Models;

namespace Test.Controllers
{
    public class SignInController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly SqlConnection _connection;
        private readonly SqlCommand _command;
        private SqlDataReader _dataReader;

        public SignInController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            _command = new SqlCommand();
        }

        public IActionResult Index()
        {
            List<SignInModel> Users = new List<SignInModel>();
            _connection.Open();
            _command.Connection = _connection;
            _command.CommandText = "Select * from users";
            _dataReader = _command.ExecuteReader();

            while (_dataReader.Read())
            {
                var usr = new SignInModel
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
                    users_longitude = _dataReader.GetString(13)
                };
                Users.Add(usr);
            }
            ViewBag.Users = Users;
            _connection.Close();
            return View();
        }

        [HttpPost]
        public IActionResult SignIn(SignInModel signInModel)
        {
            // Validate the email and password against the database
            bool isValidUser = ValidateUser(signInModel.users_email, signInModel.users_password_hash);

            if (isValidUser)
            {
                // Redirect to the authenticated user's home page
                return RedirectToAction("Index", "UHome");
            }
            else
            {
                // Display an error message if authentication fails
                TempData["ErrorMessage"] = "Invalid email or password !";
                return RedirectToAction("Index");
            }
        }

        private bool ValidateUser(string users_email, string users_password_hash)
        {
            // Perform the database validation here using the provided email and password
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                con.Open();
                SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM users WHERE users_email = @Email AND users_password_hash = @Password", con);
                command.Parameters.AddWithValue("@Email", users_email);
                command.Parameters.AddWithValue("@Password", users_password_hash);
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }
    }
}

