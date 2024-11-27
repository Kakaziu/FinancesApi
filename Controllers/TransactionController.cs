using FinancesApi.Models;
using FinancesApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FinancesApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUserRepository _userRepository;

        public TransactionController(ITransactionRepository transactionRepository, IUserRepository userRepository)
        {
            _transactionRepository = transactionRepository;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<TransactionModel>>> Index()
        {
            var transactions = await _transactionRepository.FindAll();
            return Ok(transactions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionModel>> Show(int id)
        {
            var transaction = await _transactionRepository.FindById(id);

            return Ok(transaction);
        }

        [HttpPost]
        public async Task<ActionResult<TransactionModel>> Create([FromBody] TransactionModel transaction)
        {
            if(Request.Headers.TryGetValue("Authorization", out var token))
            {
                var handler = new JwtSecurityTokenHandler();

                token = token.ToString().Split(" ").Last();

                if(handler.CanReadToken(token))
                {
                    var jwtToken = handler.ReadJwtToken(token);

                    var userEmail = jwtToken.Claims.FirstOrDefault(x => x.Type == "email").Value;

                    var user = await _userRepository.FindByEmail(userEmail);

                    transaction.UserId = user.Id;
                    var newTransaction = await _transactionRepository.Insert(transaction);

                    return Ok(newTransaction);
                }

                return Unauthorized("Token inválido.");
            }

            return Unauthorized("Acess Denied.");  
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TransactionModel>> Update([FromBody] TransactionModel transaction, int id)
        {
            transaction.Id = id;
            var updatedTransaction = await _transactionRepository.Update(transaction, id);
            return Ok(updatedTransaction);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TransactionModel>> Delete(int id)
        {
            var transaction = await _transactionRepository.Delete(id);
            return Ok(transaction);
        }
    }
}
