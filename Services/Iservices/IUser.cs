using school.Models.DTos;

namespace school.Services.Iservices
{
    public interface IUser
    {
        Task<UserResponseDto> CreateAdmin(CreateUserDto createUserDto);
        Task<List<UserResponseDto>> GetAllAdminUsers();
        Task<List<UserResponseDto>> getAllUserAsync();
        Task<LoginResponseDTO> LoginUserAsync(LoginDTO loginDTO);
    }
}
