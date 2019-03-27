using SmartDealer.Models.Models.Customer;
using SmartDealer.Repository.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartDealer.Repository.Repositories
{
    public interface ICustomerNameRepository : IRepository<CustomerName>
    {
    }
    public class CustomerNameRepository : Repository<CustomerName>, ICustomerNameRepository
    {
        public DealerContext DealerContext  { get { return Context as DealerContext; } }

        public CustomerNameRepository(DealerContext context) : base(context)
        {

        }

    }
}
