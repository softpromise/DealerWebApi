using System;
using System.Collections.Generic;
using System.Text;

namespace SmartDealer.Models.Models.Customer
{
    public class CustomerName : BaseEntity
    {
        public CustomerName()
        {
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string SingularName { get; set; }
        public SalutationType Title { get; set; }
        public string Suffix { get; set; }
        public int FKIdentityProfileId { get; set; }
        public IdentityProfile IdentityProfile { get; set; }

    }
}
