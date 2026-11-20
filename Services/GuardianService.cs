using Microsoft.EntityFrameworkCore;
using school.Data;
using school.Models;
using school.Models.DTos;
using school.Services.Iservices;

namespace school.Services
{
    public class GuardianService(AppDbContext _dbContext) : IGuardian
    {
        public async Task<(GuardianResponseDto? data, string? errors)> AddExtraGuardian(string RegNumber,AddGuardianDto addGuardian)
        {
            var studentExists = await _dbContext.students.FirstOrDefaultAsync(st => st.RegNumber == RegNumber);

            if(studentExists is null)
            {
                return (null, "Student does not exists");
            }

            var guardianExists = await _dbContext.guardians.AnyAsync(g => g.Email == addGuardian.Email ||
            g.PhoneNumber == addGuardian.PhoneNumber);

            if (guardianExists)
            {
                return (null, $"Guardian with email {addGuardian.Email} or phone number {addGuardian.PhoneNumber} exists");
            }

            var newGuardian = new Guardian
            {
                Name = addGuardian.Name,
                Email = addGuardian.Email,
                PhoneNumber = addGuardian.PhoneNumber,
                Residence = addGuardian.Residence
            };

            var studentGuardian = new StudentGuardian
            {
                GuardianId = newGuardian.Id,
                StudentRegNumber = studentExists.RegNumber,
                Relationship = "Guardian",
                IsPrimaryContact = true

            };
            newGuardian.StudentGuardian.Add(studentGuardian);

            _dbContext.guardians.Add(newGuardian);
            await _dbContext.SaveChangesAsync();

            var guardianResponse = toGuardianResponseDTO(newGuardian);

            return (guardianResponse, null);

        }

        public async Task<(GuardianResponseDto? data, string? errors)> AddGuardianAsync(AddGuardianDto addGuardianDto)
        {
            var guardianExists = await _dbContext.guardians.AnyAsync(g=>g.Email == addGuardianDto.Email ||
            g.PhoneNumber == addGuardianDto.PhoneNumber);

            if(guardianExists)
            {
                return (null, $"Guardian with email {addGuardianDto.Email} or phone number {addGuardianDto.PhoneNumber} exists");
            }
            var newGuardian = new Guardian
            {
                Name=addGuardianDto.Name,
                Email=addGuardianDto.Email,
                PhoneNumber=addGuardianDto.PhoneNumber,
                Residence=addGuardianDto.Residence
            };

            _dbContext.guardians.Add(newGuardian);
            await _dbContext.SaveChangesAsync();
            var response= new GuardianResponseDto(newGuardian.Name,newGuardian.Email,newGuardian.PhoneNumber, newGuardian.Residence,newGuardian.StudentGuardian.
                Select(pc=>pc.IsPrimaryContact).FirstOrDefault());
            return (response, null);
        }

        public async Task<List<GuardianResponseDto>> GetAllGuardians()
        {
            return await _dbContext.guardians.Select(gd=>new GuardianResponseDto(gd.Name,gd.Email,gd.PhoneNumber,gd.Residence,
                gd.StudentGuardian.
                Select(pc => pc.IsPrimaryContact).FirstOrDefault())
                ).ToListAsync();
        }

        private GuardianResponseDto toGuardianResponseDTO(Guardian guardian)
        {
            return new GuardianResponseDto(guardian.Name, guardian.Email, guardian.PhoneNumber, guardian.Residence, guardian.StudentGuardian.
                Select(pc => pc.IsPrimaryContact).FirstOrDefault());
        }
    }
}
