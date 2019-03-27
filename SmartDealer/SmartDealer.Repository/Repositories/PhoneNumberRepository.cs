using SmartDealer.Models.Models.Customer;
using SmartDealer.Repository.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartDealer.Repository.Repositories
{
    
    public interface IPhoneNumberRepository : IRepository<PhoneNumber>
    {
    }
    public class PhoneNumberRepository : Repository<PhoneNumber>, IPhoneNumberRepository
    {
        public PhoneNumberRepository(DealerContext context) : base(context)
        {

        }

        public DealerContext DealerContext
        { get { return Context as DealerContext; } }
    }
}
