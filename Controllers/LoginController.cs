using FinancesApi.Models;
using FinancesApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace FinancesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public LoginController (IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Entry(LoginModel login)
        {
            try
            {
                var user = await _userRepository.GetByEmail(login.LoginProvider);

                if (user == null) return Unauthorized("Credenciais inválidas.");

                if (!user.ComparePasswordHash(login.Password)) return Unauthorized("Senha incorreta.");

                var token = await GenerateJWT(login);

                Response.Headers.Append("Authorization", JsonSerializer.Serialize(token));

                return Ok(new { token = token });
            } catch (Exception)
            {
                return Unauthorized("Usuário não autorizado.");
            }
        }

        private async Task<string> GenerateJWT(LoginModel login)
        {
            string secret = "3b2c2d6b-bec0-48f8-a93f-c4e41f18e103";

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var user = await _userRepository.GetByEmail(login.LoginProvider);

            var claims = new[]
            {
                new Claim("email", user.Email),
                new Claim("id", user.Id.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: "finances_op",
                audience: "finances_app",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
