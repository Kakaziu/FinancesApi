using FinancesApi.DTOs;
using FinancesApi.Models;
using FinancesApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FinancesApi.Extensions;
using Microsoft.AspNetCore.JsonPatch;
using AutoMapper;
using FinancesApi.Data.Map;

namespace FinancesApi.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDTO>>> GetAll()
        {
            try
            {
                var users = await _repository.GetAll();

                if (users is null) return NotFound("Não foi possível achar os usuários.");

                return Ok(_mapper.Map<List<UserDTO>>(users));
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

                return Ok(_mapper.Map<UserDTO>(user));
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

                var user = _mapper.Map<UserModel>(userDTO);

                user.CreatedDate = DateTime.Now;
                user.LastModifiedDate = DateTime.Now;
                user.SetPasswordHash();

                var newUser = await _repository.Create(user);

                var newUserDTO = _mapper.Map<UserDTO>(newUser);

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

                var user = _mapper.Map<UserModel>(userDTO);

                user.LastModifiedDate = DateTime.Now;
                user.SetPasswordHash();

                var updatedUser = await _repository.Update(user);

                var updatedUserDTO = _mapper.Map<UserDTO>(updatedUser);

                if (updatedUserDTO is null) return BadRequest("Não foi possível atualizar o usuário");

                return Ok(updatedUserDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Houve um problema na sua solicitação");
            }
        }

        [HttpPatch("{id}/updatePassword")]
        public async Task<ActionResult<UserDTOUpdateResponse>> UpdatePassword(int id, 
            JsonPatchDocument<UserDTOUpdateRequest> jsonPatch)
        {
            try
            {
                if (jsonPatch is null || id <= 0) return BadRequest("Dados inválidos");

                var user = await _repository.Get(u => u.Id == id);

                if (user is null) return NotFound();

                var userDTOUpdateRequest = _mapper.Map<UserDTOUpdateRequest>(user);

                jsonPatch.ApplyTo(userDTOUpdateRequest, ModelState);

                if (!ModelState.IsValid) return BadRequest(ModelState);

                _mapper.Map(userDTOUpdateRequest, user);

                user.SetPasswordHash();
                await _repository.Update(user);

                return Ok(_mapper.Map<UserDTOUpdateResponse>(user));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<UserDTO>> Delete(int id)
        {
            try
            {
                var user = await _repository.Get(u => u.Id == id);

                if (user is null) return BadRequest("Usuário não encontrado.");

                var deletedUser = await _repository.Delete(user);

                return Ok(_mapper.Map<UserDTO>(deletedUser));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Houve um problema na sua solicitação");
            }
        }
    }
}
