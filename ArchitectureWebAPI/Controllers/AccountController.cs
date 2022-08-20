using Architecture.DataService.IConfiguration;
using ArchitectureWebAPI.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ArchitectureWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        private IConfiguration _configuration;
        public AccountController(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }
        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] LoginRequestDto loginRequest)
        {
            var user = _unitOfWork.appUser.GetUser(loginRequest.Username, loginRequest.Password);

            if(user == null)
            {
                return BadRequest("Authentication Fail");
            }
            else
            {
                var iUser = new IdentityUser
                {
                    UserName = user.Username,
                    Email = user.Email
                };
                var token = GetJwtToken(iUser);
                return Ok(token);
            }
        }

        public string GetJwtToken(IdentityUser user)
        {
            var tokenHandeler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtConfig:secret"]);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("Username", user.UserName),
                new Claim("Email", user.Email)
            };
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials
                (
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandeler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandeler.WriteToken(token);
            return jwtToken;
        }
    }
}
