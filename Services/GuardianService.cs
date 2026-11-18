using Microsoft.EntityFrameworkCore;
using school.Data;
using school.Models.DTos;
using school.Services.Iservices;

namespace school.Services
{
    public class GuardianService(AppDbContext _dbContext) : IGuardian
    {
        public async Task<(GuardianResponseDto? data, string? errors)> AddGuardianAsync(AddGuardianDto addGuardianDto)
        {
            var guardianExists = await _dbContext.guardians.FirstOrDefaultAsync(g=>g.Email == addGuardianDto.PhoneNumber || 
            g.PhoneNumber == addGuardianDto.PhoneNumber);

            if(guardianExists is not null)
            {
                return (null, $"Guardian with email {addGuardianDto.Email} and phone number {addGuardianDto.PhoneNumber} exists");
            }
        }

        public Task<List<GuardianResponseDto>> GetAllGuardians()
        {
            throw new NotImplementedException();
        }
    }
}
