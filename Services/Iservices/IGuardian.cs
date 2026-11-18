using school.Models.DTos;

namespace school.Services.Iservices
{
    public interface IGuardian
    {
        Task<(GuardianResponseDto? data,string? errors)> AddGuardianAsync(AddGuardianDto addGuardianDto);
        Task<List<GuardianResponseDto>> GetAllGuardians();
    }
}
