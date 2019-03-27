using System;
using System.Collections.Generic;
using System.Text;

namespace SmartDealer.Models.Models.Customer
{
    public class CustomerAttributes : BaseEntity
    {
        public bool AllowsPhoneContact { get; set; }
        public bool AllowsMailContact { get; set; }
        public bool AllowsEmailContact { get; set; }
        public bool AllowsSmsContact { get; set; }
        public string Comments { get; set; }
        public string DriverLicenseNumber { get; set; }
        public State DriverLicenseState { get; set; }
        public bool IsMarried { get; set; }
        public bool IsWholesale { get; set; }
        public DateTime? LastContactDate { get; set; }
        public string CustomerNumber { get; set; }
        public string CustomerType { get; set; }

        public IdentityProfile IdentityProfile { get; set; }

        public int FKCustomerId { get; set; }
        public  Customer Customer { get; set; }
    }
}
