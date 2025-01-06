using FinancesApi.Models;
using FinancesApi.DTOs

namespace FinancesApi.Extensions
{
    public static class TransitionDTOMappingExtensions
    {
        public static TransitionDTO? ToTransitionDTO(TransitionModel transition)
        {
            if (transition == null) return null;

            return new TransitionDTO()
            {
                Id = transition.Id,
                Description = transition.Description,
                Value = transition.Value,
                TransitionType = transition.TransitionType,
                UserId = transition.UserId,
            };
        }

        public static TransitionModel? ToTransitionModel(TransitionDTO transitionDTO)
        {
            if (transitionDTO is null) return null;

            return new TransitionModel()
            {
                Id = transitionDTO.Id,
                Description = transitionDTO.Description,
                Value = transitionDTO.Value,
                TransitionType = transitionDTO.TransitionType,
                UserId = transitionDTO.UserId,
            };
        }

        public static List<TransitionDTO>? ToListTransitionDTO(List<TransitionModel> transitions)
        {
            if (transitions is null || !transitions.Any()) return null;

            return transitions.Select(t => new TransitionDTO()
            {
                Id = t.Id,
                Description = t.Description,
                Value = t.Value,
                TransitionType = t.TransitionType,
                UserId = t.UserId,
            }).ToList();
        }
    }
}
