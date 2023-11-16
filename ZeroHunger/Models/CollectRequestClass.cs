using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZeroHunger.Models
{
    public class CollectRequestClass
    {
        public int Id { get; set; }

        [Required]
        public string FoodType { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public int MaxPreservationTime { get; set; }

        [Required]
        public string CollectionStatus { get; set; }

        public DateTime CollectionTime { get; set; }

        public int CollectionEmployeeId { get; set; }
        public int RestaurantId { get; set; }

        public virtual EmployeeClass CollectionEmployee { get; set; }
        public virtual RestaurantClass Restaurant { get; set; }
        public virtual AdminClass Admin { get; set; }      
    }
}