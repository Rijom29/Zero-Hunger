//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ZeroHunger.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class CollectRequest
    {
        public int Id { get; set; }
        public string FoodType { get; set; }
        public int Quantity { get; set; }
        public int MaxPreservationTime { get; set; }
        public string CollectionStatus { get; set; }
        public Nullable<System.DateTime> CollectionTime { get; set; }
        public Nullable<int> CollectionEmployeeId { get; set; }
        public Nullable<int> RestaurantId { get; set; }
    
        public virtual Employee Employee { get; set; }
        public virtual Restaurant Restaurant { get; set; }
    }
}
