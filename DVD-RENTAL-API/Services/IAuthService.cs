using DVD_RENTAL_API.DTOs;

namespace DVD_RENTAL_API.Services
{
    public interface IAuthService
    {
        Task<UserResponseDTO> RegisterAsync(UserDTO userDTO);
        Task<string> LoginAsync(LoginDTO loginDTO);
    }
}
