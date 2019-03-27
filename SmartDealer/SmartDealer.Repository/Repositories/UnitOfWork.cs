using SmartDealer.Repository.Contexts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartDealer.Repository.Repositories
{
    public interface IUnitOfWork//: IDisposable
    {
        IAuthRepository User { get; }

        ICustomerRepository Customer { get; }

        ICustomerAttributesRepository CustomerAttributes { get; }

        IIdentityProfileRepository IdentityProfile { get; }

        ICustomerNameRepository CustomerName { get; }

        IPhoneNumberRepository PhoneNumber { get; }

        Task<int> CompleteAsync();

    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly DealerContext dealerContext;
        public UnitOfWork(DealerContext _dealerContext)
        {
            dealerContext = _dealerContext;
            User = new AuthRepository(dealerContext);
            Customer = new CustomerRepository(dealerContext);
            CustomerAttributes = new CustomerAttributesRepository(dealerContext);
            IdentityProfile = new IdentityProfileRepository(dealerContext);
            CustomerName = new CustomerNameRepository(dealerContext);
            PhoneNumber = new PhoneNumberRepository(dealerContext);
        }
        public IAuthRepository User { get; private set; }
        public ICustomerRepository Customer { get; private set; }

        public ICustomerAttributesRepository CustomerAttributes { get; private set; }
        public IIdentityProfileRepository IdentityProfile { get; private set; }

        public ICustomerNameRepository CustomerName { get; private set; }

        public IPhoneNumberRepository PhoneNumber { get; private set; }

        public async Task<int> CompleteAsync()
        {
            return await dealerContext.SaveChangesAsync();
        }
        //public void Dispose()
        //{
        //    dealerContext.Dispose();
        //
        //}

    }
}
