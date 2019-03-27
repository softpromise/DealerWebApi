using System;
using System.Collections.Generic;
using System.Text;

namespace SmartDealer.Models.Models.Customer
{
    public class IdentityProfile : BaseEntity
    {
        public IdentityProfile()
        {
            PhoneNumbers = new HashSet<PhoneNumber>();
        }
        public Gender Sex { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string EmailAddress { get; set; }
        public string County { get; set; }
        public string Country { get; set; }
        public string Ssn { get; set; }
        public DateTime? BirthDate { get; set; }

        public CustomerName CustomerName { get; set; }
        public ICollection<PhoneNumber> PhoneNumbers { get; set; }


        public int FKCustomerAttributesId { get; set; }
        public CustomerAttributes CustomerAttributes { get; set; }
    }
}
