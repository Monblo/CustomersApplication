using CustomersApplication.Database;
using CustomersApplication.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace CustomersApplication.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private DatabaseContext _context;

        public AuthController(DatabaseContext context)
        {
            _context= context;
        }

        [HttpPost, Route("login")]
        public IActionResult Login([FromBody] LoginModel user)
        {
            if (user == null)
            {
                return BadRequest("Invalid client request");
            }
            var users = _context.Accounts.ToList();

            var loginUser = users.FirstOrDefault(x => x.Login.Equals(user.Login) && x.Password.Equals(user.Password));
            if (loginUser != null)
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokenOptions = new JwtSecurityToken(
                   issuer: "https://localhost:7001",
                   audience: "https://localhost:7001",
                   claims: new List<Claim>(),
                   expires: DateTime.Now.AddHours(1),
                   signingCredentials: signingCredentials
                   );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return Ok(new { Account = loginUser, Token = tokenString});
            }
            return Unauthorized();
        }
    }
}
