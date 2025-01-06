using FinancesApi.DTOs;
using FinancesApi.Extensions;
using FinancesApi.Models;
using FinancesApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace FinancesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransitionController : ControllerBase
    {
        private readonly ITransitionRepository _repository;

        public TransitionController(ITransitionRepository transitionRepository)
        {
            _repository = transitionRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<TransitionDTO>>> GetAll()
        {
            try
            {
                var transitions = await _repository.GetAll();

                if (transitions is null) return NotFound("Não foi possível listar as transições");

                if (!Request.Headers.ContainsKey("Authorization")) return BadRequest("Não autorizado.");

                var token = Request.Headers["Authorization"];
                var handler = new JwtSecurityTokenHandler();

                var formatedToken = token.ToString().Split(" ");

                token = formatedToken[1];

                var jwtToken = handler.ReadJwtToken(token);

                var claims = jwtToken.Claims.ToDictionary(c => c.Type, c => c.Value);

                var userTransitions = transitions.Where(t => t.UserId.ToString() == claims["id"]);

                return Ok(userTransitions.ToList().ToListTransitionDTO());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = ex.Message });
            }
        }

        [HttpGet("{id}", Name = "GetTransition")]
        public async Task<ActionResult<TransitionDTO>> Get(int id)
        {
            try
            {
                var transition = await _repository.Get(t => t.Id == id);

                if (transition is null) return NotFound("Não foi possível achar a transição.");

                return Ok(transition.ToTransitionDTO());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Houve um problema na sua solicitação.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<TransitionDTO>> Create([FromBody] TransitionDTO transitionDTO)
        {
            try
            {
                if (transitionDTO is null) return BadRequest("Dados inválidos");

                var transition = transitionDTO.ToTransitionModel();

                transition.CreatedDate = DateTime.Now;
                transition.LastModifiedDate = DateTime.Now;

                var newTransition = await _repository.Create(transition);

                var newTransitionDTO = newTransition.ToTransitionDTO();

                return new CreatedAtRouteResult("GetTransition", new { id = newTransitionDTO.Id }, newTransitionDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Houve um problema na sua solicitação.");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TransitionDTO>> Update([FromBody] TransitionDTO transitionDTO, int id)
        {
            try
            {
                if (transitionDTO.Id != id) return BadRequest("Id inválido.");

                var transition = transitionDTO.ToTransitionModel();

                transition.LastModifiedDate = DateTime.Now;

                var updatedTransition = await _repository.Update(transition);

                if (updatedTransition is null) return BadRequest("Não foi possível atualizar a transição.");

                var updatedTransitionDTO = updatedTransition.ToTransitionDTO();

                return Ok(updatedTransitionDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Houve um problema na sua solicitação.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TransitionModel>> Delete(int id)
        {
            try
            {
                var transition = await _repository.Get(t => t.Id == id);

                if (transition is null) return NotFound("Não foi possível achar a transição");

                var deletedTransition = await _repository.Delete(transition);

                return Ok(deletedTransition.ToTransitionDTO());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Houve um problema na sua solicitação.");
            }
        }
    }
}
