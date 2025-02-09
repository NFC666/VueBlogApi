using VueBlog.API.Models;

namespace VueBlog.API.Utility
{
    public static class UserDtoConverter
    {
        public static UserDTO Converter(User user)
        {
            UserDTO userDTO = new UserDTO()
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                AvatarUrl = user.AvatarUrl,
                Role = user.Role,
                CreatedAt = user.CreatedAt,
            };
            return userDTO;
        }
        public static List<UserDTO> Converter(List<User> users)
        {
            var userDTOs = new List<UserDTO>();
            foreach (var item in users)
            {
                userDTOs.Add(Converter(item));
            }
            return userDTOs;
        }
    }
}
