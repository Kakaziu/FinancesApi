using FinancesApi.Models;
using FinancesApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinancesApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;

        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserModel>>> GetAll()
        {
            try
            {
                var users = await _repository.GetAll();

                if (users is null) return NotFound("Não foi possível achar os usuários.");

                return Ok(users);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Houve um problema na sua solicitação");
            }
        }

        [HttpGet("{id}", Name = "GetUser")]
        public async Task<ActionResult<UserModel>> Get(int id)
        {
            try
            {
                var user = await _repository.Get(u => u.Id == id);

                if (user is null) return NotFound("Não foi possível achar o usuário");

                return Ok(user);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Houve um problema na sua solicitação");
            }
        }

        [HttpPost]
        public async Task<ActionResult<UserModel>> Create([FromBody] UserModel user)
        {
            try
            {
                if (user is null) return BadRequest("Dados inválido.");

                user.CreatedDate = DateTime.Now;
                user.LastModifiedDate = DateTime.Now;
                user.SetPasswordHash();

                var newUser = await _repository.Create(user);

                return new CreatedAtRouteResult("GetUser", new { id = newUser.Id }, newUser);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Houve um problema na sua solicitação");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserModel>> Update(int id, [FromBody] UserModel user)
        {
            try
            {
                if (id != user.Id) return BadRequest("Id inválido");

                user.LastModifiedDate = DateTime.Now;

                var updatedUser = await _repository.Update(user);

                if (updatedUser is null) return BadRequest("Não foi possível atualizar o usuário");

                return Ok(updatedUser);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Houve um problema na sua solicitação");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<UserModel>> Delete(int id)
        {
            try
            {
                var user = await _repository.Get(u => u.Id == id);

                if (user is null) return BadRequest("Usuário não encontrado.");

                var deletedUser = await _repository.Delete(user);

                return Ok(deletedUser);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Houve um problema na sua solicitação");
            }
        }
    }
}
