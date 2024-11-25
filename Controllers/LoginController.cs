using FinancesApi.Models;
using FinancesApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FinancesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public LoginController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Entry([FromBody] LoginModel login)
        {
            var user = await _userRepository.FindByEmail(login.Login);

            if (login.Login == user.Email && user.ValidPassword(login.Password))
            {
                var token = await GenerateJWT(login);

                return Ok(new { token });
            }

            return BadRequest("Credenciais inválidas.");
        }

        private async Task<string> GenerateJWT(LoginModel login)
        {
            string secret = "04417169-88c0-4c3c-87dc-da761696050d";

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var user = await _userRepository.FindByEmail(login.Login);

            if (user == null) return null;

            var claims = new[]
            {
                new Claim("email", user.Email),
                new Claim("id", user.Id.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: "Finances",
                audience: "FinancesApi",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credential
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
