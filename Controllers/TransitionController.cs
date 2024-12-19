using FinancesApi.Models;
using FinancesApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinancesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransitionController : ControllerBase
    {
        private readonly ITransitionRepository _repository;

        public TransitionController(ITransitionRepository transitionRepository)
        {
            _repository = transitionRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<TransitionModel>>> GetAll()
        {
            try
            {
                var transitions = await _repository.GetAll();

                if (transitions is null) return NotFound("Não foi possível listar as transições");

                return Ok(transitions);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Houve um problema na sua solicitação.");
            }
        }

        [HttpGet("{id}", Name = "GetTransition")]
        public async Task<ActionResult<TransitionModel>> Get(int id)
        {
            try
            {
                var transition = await _repository.Get(t => t.Id == id);

                if (transition is null) return NotFound("Não foi possível achar a transição.");

                return transition;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Houve um problema na sua solicitação.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<TransitionModel>> Create([FromBody] TransitionModel transition)
        {
            try
            {
                if (transition is null) return BadRequest("Dados inválidos");

                transition.CreatedDate = DateTime.Now;
                transition.LastModifiedDate = DateTime.Now;

                var newTransition = await _repository.Create(transition);

                return new CreatedAtRouteResult("GetTransition", new { id = newTransition.Id }, newTransition);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = ex.InnerException.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TransitionModel>> Update([FromBody] TransitionModel transition, int id)
        {
            try
            {
                if (transition.Id != id) return BadRequest("Id inválido.");

                transition.LastModifiedDate = DateTime.Now;

                var updatedTransition = await _repository.Update(transition);

                if (updatedTransition is null) return BadRequest("Não foi possível atualizar a transição.");

                return Ok(updatedTransition);
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

                transition = await _repository.Delete(transition);

                return Ok(transition);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Houve um problema na sua solicitação.");
            }
        }
    }
}
