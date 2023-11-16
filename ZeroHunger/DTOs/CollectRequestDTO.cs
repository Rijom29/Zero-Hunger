using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZeroHunger.DTOs
{
    public class CollectRequestDTO
    {
       
        [Required(ErrorMessage = "Food Type is required")]
        public string FoodType { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Max Preservation Time is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Max Preservation Time must be greater than 0")]
        public int MaxPreservationTime { get; set; }

        public int Id { get; set; }
    
        public string CollectionStatus { get; set; }
        public DateTime? CollectionTime { get; set; }
        public int? CollectionEmployeeId { get; set; }
        public int RestaurantId { get; set; }

        
    }

    

}