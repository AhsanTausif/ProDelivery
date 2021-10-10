//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProDelivery.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Dealership
    {
        public int Dealership_Id { get; set; }
        public string Sender_Name { get; set; }
        public string Branch_Address { get; set; }
        public Nullable<System.DateTime> Date_Of_Apply { get; set; }
        public decimal Money_Deposited { get; set; }
        public int Sender_Id { get; set; }
        public int Admin_Id { get; set; }
    
        public virtual Admin Admin { get; set; }
        public virtual Sender Sender { get; set; }
    }
}