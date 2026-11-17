using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using school.Data;
using school.Models;
using school.Models.DTos;
using school.Models.Enums;
using school.Services.Iservices;

namespace school.Services
{
    public class UserService(AppDbContext _dbContext) : IUser
    {
        public async Task<UserResponseDto> CreateAdmin(CreateUserDto createUserDto)
        {
            var adminRole = await _dbContext.Set<Role>().FirstOrDefaultAsync(rl => rl.Name == RoleEnum.ADMIN);
            var emailAndPhoneExists = await _dbContext.users.FirstOrDefaultAsync(user => user.Email == createUserDto.Email && user.PhoneNumber ==
            createUserDto.PhoneNumber);

            if(adminRole is null || emailAndPhoneExists is not null)
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
                new HashSet<Role> { adminRole }
            );

        }

        public async Task<List<UserResponseDto>> getAllUserAsync()
        {
            return await _dbContext.users.Select(
                user=>new UserResponseDto(
                    user.Fullname,user.Email,user.PhoneNumber,user.DOB,user.CreatedDate,user.Roles.ToHashSet()
                    )
                ).ToListAsync();
            
        }
    }
}
