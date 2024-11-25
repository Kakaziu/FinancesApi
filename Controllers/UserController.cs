using FinancesApi.Models;
using FinancesApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinancesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserModel>>> Index()
        {
            var users = await _userRepository.FindAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> Show(int id)
        {
            var user = await _userRepository.FindById(id);

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<UserModel>> Create([FromBody] UserModel user)
        {
            var newUser = await _userRepository.Insert(user);

            return Ok(newUser);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserModel>> Update([FromBody] UserModel user, int id)
        {
            user.Id = id;
            var updatedUser = await _userRepository.Update(id, user);
            return Ok(updatedUser);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<UserModel>> Delete(int id)
        {
            var user = await _userRepository.Delete(id);
            return Ok(user);
        }
    }
}
