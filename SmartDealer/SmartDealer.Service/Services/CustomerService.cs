using SmartDealer.Models.Models.Customer;
using SmartDealer.Repository.Contexts;
using SmartDealer.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartDealer.Service.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetCustomerListAsync();
        Task CreateCustomerAsync(Customer customer);
    }
    public class CustomerService : BaseService, ICustomerService
    {
        private IUnitOfWork unitOfWork;

        public CustomerService(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public async Task<IEnumerable<Customer>> GetCustomerListAsync()
        {
            IEnumerable<Customer> response;
            try
            {
                //using (var uow = unitOfWork)
                //{
                    response = await unitOfWork.Customer.GetAllAsync();
                //}
            }
            catch (Exception ex)
            {
                //response = CreateCustomError(ex);
                throw;
            }
            
            return response;
        }

        public async Task CreateCustomerAsync(Customer customer)
        {
            try
            {
                //using(var uow  = unitOfWork)
                //{
                    await unitOfWork.Customer.AddAsync(customer);
                    await unitOfWork.CompleteAsync();
                //}
            }
            catch (Exception ex)
            {
                //response = CreateCustomError(ex);
                throw;
            }
        }

    }
}
