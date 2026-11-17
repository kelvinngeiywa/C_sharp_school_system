using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using school.Models;
using school.Models.Enums;

namespace school.bootstrap
{
    public static class SuperAdminSeeder
    {
        public static void SeedSuperAdmin(DbContext dbContext, IConfiguration configuration)
        {
            if (dbContext.Set<User>().Any())
            {
                return;
            }
            var superAdminRole = dbContext.Set<Role>().FirstOrDefault(rl => rl.Name == RoleEnum.SUPER_ADMIN);

            if (superAdminRole is not null)
            {
                var superAdmin = CreateSuperAdminUser(superAdminRole, configuration);
                dbContext.Set<User>().Add(superAdmin);
                dbContext.SaveChanges();

            }



        }
        public static async Task SeedSuperAdminAsync(DbContext dbContext, IConfiguration configuration,
            CancellationToken cancellationToken=default)
        {
            if (await dbContext.Set<User>().AnyAsync())
            {
                return;
            }
            var superAdminRole = await dbContext.Set<Role>().FirstOrDefaultAsync(rl => rl.Name == RoleEnum.SUPER_ADMIN);

            if (superAdminRole is not null)
            {
                var superAdmin = CreateSuperAdminUser(superAdminRole, configuration);
                dbContext.Set<User>().Add(superAdmin);
                await dbContext.SaveChangesAsync();

            }

        }

        private static User CreateSuperAdminUser(Role superAdminRole, IConfiguration configuration)
        {
            var hashedPassword = new PasswordHasher<User>();

            var superAdmin = new User
            {
                Id = Guid.CreateVersion7(),
                Fullname = "Super Admin",
                Email = configuration["superAdmin:Email"],
                PhoneNumber = configuration["superAdmin:PhoneNumber"],
                Roles = new HashSet<Role> { superAdminRole },
                DOB = new DateOnly(1999, 02, 27),
                CreatedDate = DateOnly.FromDateTime(DateTime.UtcNow)
            };

            var rawPassword = configuration["superAdmin:Password"];

            superAdmin.Password = hashedPassword.HashPassword(superAdmin, rawPassword);

            return superAdmin;


        }
    }
}
