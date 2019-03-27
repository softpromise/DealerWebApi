using SmartDealer.Models.Models.User;
using SmartDealer.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartDealer.Service.Services
{
    public interface IAuthService
    {
        Task<User> Register(User user, string password);
        Task<User> Login(string userName, string password);
        Task<bool> IsUserExist(string userName);
        Task<IEnumerable<User>> GetUsersList();
    }
    public class AuthService : BaseService, IAuthService
    {
        private IUnitOfWork unitOfWork;

        public AuthService(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            try
            {
                
                    await unitOfWork.User.AddAsync(user);
                    await unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                //response = CreateCustomError(ex);
                throw;
            }
            return user;
        }

        public async Task<User> Login(string userName, string password)
        {
            User response = new User();
            try
            {
                //using (var uow = unitOfWork)
                //{
                    response = await unitOfWork.User.Login(userName, password);
                //}
            }
            catch (Exception ex)
            {
                //response = CreateCustomError(ex);
                throw;
            }

            return response;
        }

        public async Task<IEnumerable<User>> GetUsersList()
        {
            IEnumerable<User> response;
            try
            {
                //using (var uow = unitOfWork)
                //{
                response = await unitOfWork.User.GetAllAsync();
                //}
            }
            catch (Exception ex)
            {
                //response = CreateCustomError(ex);
                throw;
            }

            return response;
        }
        public async Task<bool> IsUserExist(string userName)
        {
            bool response = false;
            try
            {
                //using (var uow = unitOfWork)
                //{
                    response = await unitOfWork.User.UserExist(userName);
                //}
            }
            catch (Exception ex)
            {
                //response = CreateCustomError(ex);
                throw;
            }

            return response;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
