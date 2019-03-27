using SmartDealer.Models.Models.Customer;
using SmartDealer.Repository.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartDealer.Repository.Repositories
{
    public interface ICustomerAttributesRepository : IRepository<CustomerAttributes>
    {
    }
    public class CustomerAttributesRepository : Repository<CustomerAttributes>, ICustomerAttributesRepository
    {
        public CustomerAttributesRepository(DealerContext context) : base(context)
        {

        }

        public DealerContext DealerContext
        { get { return Context as DealerContext; } }
    }
}
