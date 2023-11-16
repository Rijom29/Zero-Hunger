using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZeroHunger.DTOs
{
    public class AdminDTO
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Email { get; set; }
        public int? RegistrationId { get; internal set; }
     
    }
}