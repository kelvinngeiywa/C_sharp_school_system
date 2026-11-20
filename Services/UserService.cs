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

        public async Task<(UserResponseDto? data, string? errors)> CreateUserCredentials(CreateUserCredentialsDTO userCredentialsDTO)
        {
            var user = new User();
            var accountType = userCredentialsDTO.AccountType;
            //var passwordHasher = new PasswordHasher<User>();

            switch (accountType)
            {
                
                case AccountTypeEnum.TEACHER:
                    var teacherRole = await _dbContext.Set<Role>().FirstOrDefaultAsync(rl => rl.Name == RoleEnum.TEACHER);
                    var teacher = await _dbContext.teachers.FirstOrDefaultAsync(tr=>tr.Email == userCredentialsDTO.Email);
                    if(teacher is null)
                    {
                        return (null, "Teacher does not exists!!");
                    }
                    var teacherAccountExists = await _dbContext.users.FirstOrDefaultAsync(ta=>ta.Email == userCredentialsDTO.Email);

                    if (teacherAccountExists is not null)
                    {
                        return (null, "Account for teacher exists");
                    }

                    var passwordHasher = new PasswordHasher<User>();

                    var teacherAccount = new User
                    {
                        Fullname=teacher.Name,
                        Email=teacher.Email,
                        PhoneNumber=teacher.PhoneNumber,
                        DOB=teacher.DOB,
                        Roles = new HashSet<Role> { teacherRole },
                        CreatedDate = DateOnly.FromDateTime(DateTime.UtcNow)
                    };


                    teacherAccount.Password = passwordHasher.HashPassword(teacherAccount,userCredentialsDTO.Password);
                    _dbContext.users.Add(teacherAccount);
                    await _dbContext.SaveChangesAsync();

                    var teacherResponse = toUserResponseDto(teacherAccount);

                    return (teacherResponse, null);

                    break;
                case AccountTypeEnum.PARENT:
                    var parentRole = await _dbContext.Set<Role>().FirstOrDefaultAsync(rl => rl.Name == RoleEnum.PARENT);
                    var parent = await _dbContext.guardians
                        .Include(sg => sg.StudentGuardian)
                        .ThenInclude(st => st.student)
                        .FirstOrDefaultAsync(pr => pr.Email == userCredentialsDTO.Email);

                    if (parent is null)
                    {
                        return (null, "Guardian does not exists!!");
                    }

                 
                    var doesParentHaveStudent = parent.StudentGuardian.Any(sg=>sg.student != null);
                    if (!doesParentHaveStudent)
                    {
                        return (null, "Guardian is not linked to any student");
                    }

                    var parentAccountExists = await _dbContext.users.FirstOrDefaultAsync(pa=>pa.Email == userCredentialsDTO.Email);

                    if (parentAccountExists is not null)
                    {
                        return (null, "Account for guardian exists");
                    }
                    var passwordHasher1 = new PasswordHasher<User>();
                    var parentAccount = new User
                    {
                        Fullname = parent.Name,
                        Email = parent.Email,
                        PhoneNumber = parent.PhoneNumber,
                        
                        Roles = new HashSet<Role> { parentRole },
                        CreatedDate = DateOnly.FromDateTime(DateTime.UtcNow)
                    };

                    parentAccount.Password = passwordHasher1.HashPassword(parentAccount, userCredentialsDTO.Password);
                    _dbContext.users.Add(parentAccount);
                    await _dbContext.SaveChangesAsync();

                    var parentResponse = toUserResponseDto(parentAccount);

                    return (parentResponse, null);

                    break;
                case AccountTypeEnum.STAFF:
                    return (null, null);
                    break;
                default:
                    return (null, null);
                    break;

            }

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

        private UserResponseDto toUserResponseDto(User user)
        {
            return new UserResponseDto(user.Fullname,user.Email,user.PhoneNumber,user.DOB,user.CreatedDate,user.Roles
                .Select(r => new RoleResponseDto(r.Id, r.Name, r.Description, r.CreatedDate)).ToList()
                );
        }
    }
}
