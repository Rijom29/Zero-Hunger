using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZeroHunger.Models
{
    public class EmployeeClass
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Phone { get; set; }
        public string Email { get; set; }

        public int RegistrationId { get; set; }
        public virtual RegistrationClass Registration { get; set; }
    }
}