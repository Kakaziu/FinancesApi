using FinancesApi.Models;
using FinancesApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
            try
            {
                var user = await _userRepository.FindById(id);

                return Ok(user);
            } catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<ActionResult<UserModel>> Create([FromBody] UserModel user)
        {
            try
            {
                var newUser = await _userRepository.Insert(user);

                return Ok(newUser);
            } catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserModel>> Update([FromBody] UserModel user, int id)
        {
            try
            {
                user.Id = id;
                var updatedUser = await _userRepository.Update(user, id);
                return Ok(updatedUser);
            } catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<UserModel>> Delete(int id)
        {
            try
            {
                var user = await _userRepository.Delete(id);
                return Ok(user);
            } catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
