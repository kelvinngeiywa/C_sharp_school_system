using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using school.Data;
using school.Models;
using school.Models.DTos;
using school.Models.Enums;
using school.Services.Iservices;

namespace school.Services
{
    public class UserService(AppDbContext _dbContext, IJwt jwtService) : IUser
    {
        public async Task<UserResponseDto> CreateAdmin(CreateUserDto createUserDto)
        {
            var adminRole = await _dbContext.Set<Role>().FirstOrDefaultAsync(rl => rl.Name == RoleEnum.ADMIN);
            var emailAndPhoneExists = await _dbContext.users.AnyAsync(user => user.Email == createUserDto.Email || user.PhoneNumber ==
            createUserDto.PhoneNumber);

            if(adminRole is null || emailAndPhoneExists)
            {
                return null;
            }
            var passwordHasher = new PasswordHasher<User>();

            var adminUser = new User
            {
                Fullname = createUserDto.Fullname,
                Email=createUserDto.Email,
                PhoneNumber=createUserDto.PhoneNumber,
                DOB=createUserDto.DOB,
                Roles=new HashSet<Role> { adminRole},
                CreatedDate= DateOnly.FromDateTime(DateTime.UtcNow)
            };
            adminUser.Password = passwordHasher.HashPassword(adminUser, createUserDto.Password);

            _dbContext.users.Add(adminUser);
            await _dbContext.SaveChangesAsync();

            return new UserResponseDto
            (
                adminUser.Fullname,
                adminUser.Email,
                adminUser.PhoneNumber,
                adminUser.DOB,
                adminUser.CreatedDate,
                adminUser.Roles.Select(r=>new RoleResponseDto(r.Id,r.Name,r.Description,r.CreatedDate)).ToList()

                
            );

        }

        public async Task<List<UserResponseDto>> GetAllAdminUsers()
        {
            return await _dbContext.users.Where(role => role.Roles.Any(r => r.Name == RoleEnum.ADMIN)).Select(
                user => new UserResponseDto(user.Fullname, user.Email, user.PhoneNumber, user.DOB, user.CreatedDate, user.Roles.Select(
                    rl=>new RoleResponseDto(rl.Id,rl.Name,rl.Description,rl.CreatedDate)).ToList())
                ).ToListAsync();
            //var all = await _dbContext.users.Select(u => new { u.Email, Roles = u.Roles.Select(r => r.Name).ToList() }).ToListAsync();
            //return all;


        }

        public async Task<List<UserResponseDto>> getAllUserAsync()
        {
            return await _dbContext.users.Select(
                user=>new UserResponseDto(
                    user.Fullname,user.Email,user.PhoneNumber,user.DOB,user.CreatedDate,user.Roles.Select(rl=>new RoleResponseDto(
                        rl.Id,rl.Name,rl.Description,rl.CreatedDate)).ToList()
                    )
                ).ToListAsync();
            
        }

        public async Task<LoginResponseDTO> LoginUserAsync(LoginDTO loginDTO)
        {
            var user = await _dbContext.users.Include(user=>user.Roles).FirstOrDefaultAsync(user => user.Email == loginDTO.Email);

            if(user is null)
            {
                return null;
            }

            var confirmPassword = new PasswordHasher<User>().VerifyHashedPassword(user, user.Password, loginDTO.Password);

            if(confirmPassword == PasswordVerificationResult.Failed)
            {
                return null;
            }

            return new LoginResponseDTO(Token:jwtService.CreateToken(user));

        }
    }
}
