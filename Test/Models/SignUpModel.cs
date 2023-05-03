using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Models
{
    public class SignUpModel
    {
        public int user_id { get; set;}
        public string? users_name { get; set; }
        public string? users_address { get; set; }
        public string? users_gender { get; set; }
        public string? users_email { get; set; }
        public string? users_contact_number { get; set; }
        public string? users_national_id_number { get; set; }
        public string? users_passport_number { get; set;}
        public string? users_type { get; set; }
        public int users_agency_id { get; set; }
        public string? users_password_hash { get; set; }
        public string? country { get; set; }
        public string? users_latitude { get; set; }
        public string? users_longitude { get; set;}
    }
}
