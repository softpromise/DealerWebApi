using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SmartDealer.Models.Models.User;
using SmartDealer.Service.Services;
using SmartDealer.WebAPI.Dtos;

namespace SmartDealer.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        private readonly IConfiguration config;
        // 123

        public AuthController(IAuthService _authService, IConfiguration _config)
        {
            authService = _authService;
            config = _config;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType(typeof(IEnumerable<User>), 200)]
        public async Task<IActionResult> Register(UserForRegisterDto RegdUser)
        {
           //if (!ModelState.IsValid) // If [ApiController] attribute not present we should manually check this modelstate
           //    return BadRequest(ModelState);
           //
            RegdUser.UserName = RegdUser.UserName.ToLower();
            if (await authService.IsUserExist(RegdUser.UserName))
                return BadRequest("Username already exist");

            var NewUser = new User
            {
                Username = RegdUser.UserName
            };
            
            var CreatedUser = await authService.Register(NewUser, RegdUser.Password);

            return StatusCode(200);
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(IEnumerable<User>), 200)]
        public async Task<IActionResult> Login(UserForLoginDto Login)
        {
            var userFromRepo = await authService.Login(Login.UserName.ToLower(), Login.Password);

            if (userFromRepo == null)
                return Unauthorized();

            var claims = new[]
                {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.Username)
                };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("AppSettings:Token").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = cred
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new { token = tokenHandler.WriteToken(token) });
        }
        [AllowAnonymous]
        [HttpGet("userList")]
        [ProducesResponseType(typeof(IEnumerable<User>), 200)]
        public async Task<IActionResult> GetAllUsers()
        {
            IEnumerable<User> userListFromRepo = await authService.GetUsersList();

            if (userListFromRepo == null)
                return NoContent();

            var Result = userListFromRepo.Select(x => new User { Id = x.Id, Username = x.Username });

            return Ok(Result);
        }
    }
}