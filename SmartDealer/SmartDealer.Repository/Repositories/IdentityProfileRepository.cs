using SmartDealer.Models.Models.Customer;
using SmartDealer.Repository.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartDealer.Repository.Repositories
{
    public interface IIdentityProfileRepository : IRepository<IdentityProfile>
    {
    }
    public class IdentityProfileRepository : Repository<IdentityProfile>, IIdentityProfileRepository
    {
        public IdentityProfileRepository(DealerContext context) : base(context)
        {

        }

        public DealerContext DealerContext
        { get { return Context as DealerContext; } }
    }
}
