using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Models
{
    public class ComplaineModel 
    {
        public int id { get; set; }
        public string? name { get; set; }
        public string? phone { get; set; }
        public string? country { get; set; }
        public string? message { get; set; }
        public string? latitude { get; set; }
        public string? longitude { get; set; }

    }
}
