using Microsoft.EntityFrameworkCore;
using SmartDealer.Models.Models.Customer;
using SmartDealer.Repository.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartDealer.Repository.Repositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        IEnumerable<Customer> GetCustomersByState(int pageIndex, int pageSize);
    }
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public DealerContext DealerContext   { get { return Context as DealerContext; } }

        public CustomerRepository(DealerContext context) : base(context)
        {
        }

        public IEnumerable<Customer> GetCustomersByState(int pageIndex, int pageSize)
        {
            // Eager loading multi level
            return DealerContext.Customers
                .Include(c => c.CustomerAttributes)
                .ThenInclude(d => d.IdentityProfile)
                .ThenInclude(e => e.CustomerName)
                .OrderBy(c => c.CustomerAttributes.IdentityProfile.CustomerName.FirstName)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }
    }
}
