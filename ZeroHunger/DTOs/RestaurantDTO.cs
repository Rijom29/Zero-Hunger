using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZeroHunger.EF;

namespace ZeroHunger.DTOs
{
    public class RestaurantDTO
    {
        internal string SupplierName;
        internal string ContactNumber;
        internal string Location;
     

        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

       
    }
}