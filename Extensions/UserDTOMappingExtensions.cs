using FinancesApi.DTOs;
using FinancesApi.Models;

namespace FinancesApi.Extensions
{
    public static class UserDTOMappingExtensions
    {
        public static UserDTO? ToUserDTO(this UserModel user)
        {
            if (user == null) return null;

            return new UserDTO()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
            };
        }

        public static UserModel? ToUser(this UserDTO userDTO)
        {
            if (userDTO == null) return null;

            return new UserModel()
            {
                Id = userDTO.Id,
                Name = userDTO.Name,
                Email = userDTO.Email,
                Password = userDTO.Password,
            };
        }

        public static List<UserDTO> ToUserDTOList(this List<UserModel> users)
        {
            if (users is null || !users.Any()) return new List<UserDTO>();

            return users.Select(user => new UserDTO()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
            }).ToList();
        }
    }
}
