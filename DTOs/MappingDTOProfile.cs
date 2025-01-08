using AutoMapper;
using FinancesApi.Models;

namespace FinancesApi.DTOs
{
    public class MappingDTOProfile : Profile
    {
        public MappingDTOProfile() 
        {
            CreateMap<UserModel, UserDTO>().ReverseMap();
            CreateMap<TransitionModel, TransitionDTO>().ReverseMap();
            CreateMap<UserModel, UserDTOUpdateRequest>().ReverseMap();
            CreateMap<UserModel, UserDTOUpdateResponse>().ReverseMap();
        }
    }
}
