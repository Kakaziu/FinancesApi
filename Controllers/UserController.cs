using FinancesApi.DTOs;
using FinancesApi.Models;
using FinancesApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FinancesApi.Extensions;

namespace FinancesApi.Controllers
{
    //[Authorize]
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
        public async Task<ActionResult<List<UserDTO>>> GetAll()
        {
            try
            {
                var users = await _repository.GetAll();

                if (users is null) return NotFound("Não foi possível achar os usuários.");

                return Ok(users.ToList().ToUserDTOList());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Houve um problema na sua solicitação");
            }
        }

        [HttpGet("{id}", Name = "GetUser")]
        public async Task<ActionResult<UserDTO>> Get(int id)
        {
            try
            {
                var user = await _repository.Get(u => u.Id == id);

                if (user is null) return NotFound("Não foi possível achar o usuário");

                return Ok(user.ToUserDTO());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Houve um problema na sua solicitação");
            }
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> Create([FromBody] UserDTO userDTO)
        {
            try
            {
                if (userDTO is null) return BadRequest("Dados inválido.");

                var user = userDTO.ToUser();

                user.CreatedDate = DateTime.Now;
                user.LastModifiedDate = DateTime.Now;
                user.SetPasswordHash();

                var newUser = await _repository.Create(user);

                var newUserDTO = newUser.ToUserDTO();

                return new CreatedAtRouteResult("GetUser", new { id = newUserDTO.Id }, newUserDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = ex.InnerException.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserDTO>> Update(int id, [FromBody] UserDTO userDTO)
        {
            try
            {
                if (id != userDTO.Id) return BadRequest("Id inválido");

                var user = userDTO.ToUser();

                user.LastModifiedDate = DateTime.Now;
                user.SetPasswordHash();

                var updatedUser = await _repository.Update(user);

                var updatedUserDTO = updatedUser.ToUserDTO();

                if (updatedUserDTO is null) return BadRequest("Não foi possível atualizar o usuário");

                return Ok(updatedUserDTO);
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

                return Ok(deletedUser.ToUserDTO());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Houve um problema na sua solicitação");
            }
        }
    }
}
