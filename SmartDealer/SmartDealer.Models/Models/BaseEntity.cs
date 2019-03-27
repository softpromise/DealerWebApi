using System;
using System.Collections.Generic;
using System.Text;

namespace SmartDealer.Models.Models
{
    public class BaseEntity
    {
        public int Id { get;  set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int ModifiedBy { get; set; }
    }
}
