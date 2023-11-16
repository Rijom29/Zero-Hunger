using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZeroHunger.Models
{
    public class RestaurantClass
    {
        public int Id { get; set; }

        [Required]
        public string SupplierName { get; set; }

        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }

        public int RegistrationId { get; set; }
        public virtual RegistrationClass Registration { get; set; }

        public virtual ICollection<CollectRequestClass> CollectRequests { get; set; }
    }
}