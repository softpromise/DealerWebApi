using Microsoft.EntityFrameworkCore;
using SmartDealer.Models.Models.User;
using SmartDealer.Repository.Contexts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartDealer.Repository.Repositories
{

    public interface IAuthRepository : IRepository<User>
    {
        Task<User> Login(string userName, string password);
        Task<bool> UserExist(string userName);
    }
    public class AuthRepository : Repository<User>, IAuthRepository
    {
        public DealerContext DealerContext  { get { return Context as DealerContext; } }
        public AuthRepository(DealerContext context) : base(context)
        {

        }

        public async Task<User> Login(string userName, string password)
        {
            var user = await DealerContext.Users.FirstOrDefaultAsync(x => x.Username == userName);
            if (user == null)
                return null;
            if(!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }
            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                        return false;
                }
            }
            return true;
        }

        public async Task<bool> UserExist(string userName)
        {
            if (await DealerContext.Users.AnyAsync(x => x.Username == userName))
                return true;
            return false;
        }

        
    }
}
