using System;
using System.Collections.Generic;
using System.Text;

namespace SmartDealer.Models.Models.Customer
{
    public class PhoneNumber : BaseEntity
    {
        public PhoneNumberType NumberType { get; set; }
        public string Digits { get; set; }
        public int FKIdentityProfileId { get; set; }
        public IdentityProfile IdentityProfile { get; set; }
    }
}
