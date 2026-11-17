using Microsoft.EntityFrameworkCore;
using school.Models;
using school.Models.Enums;

namespace school.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options):DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>()
                .Property(r => r.Name)
                .HasConversion<string>()
                .HasMaxLength(50);


            var seedDate = new DateOnly(2026, 11, 17);
            modelBuilder.Entity<Role>().HasData
            (
                new Role { Id= Guid.Parse("01933b4a-1000-7000-8000-000000000001"), Name=RoleEnum.SUPER_ADMIN,Description="Super Admin role", CreatedDate= seedDate },
                new Role { Id = Guid.Parse("01933b4a-2000-7000-8000-000000000002"), Name = RoleEnum.ADMIN, Description = "Admin role", CreatedDate = seedDate },
                new Role { Id = Guid.Parse("01933b4a-3000-7000-8000-000000000003"), Name = RoleEnum.PRINCIPAL, Description = "Principal role", CreatedDate = seedDate },
                new Role { Id = Guid.Parse("01933b4a-4000-7000-8000-000000000004"), Name = RoleEnum.TEACHER, Description = "Teacher role", CreatedDate = seedDate },
                new Role { Id = Guid.Parse("01933b4a-5000-7000-8000-000000000005"), Name = RoleEnum.PARENT, Description = "Parent role", CreatedDate = seedDate },
                new Role { Id = Guid.Parse("01933b4a-6000-7000-8000-000000000006"), Name = RoleEnum.STAFF, Description = "Support staff role", CreatedDate = seedDate }

            );
        }

        public DbSet<User> users { get; set; }
        public DbSet<Role> roles { get; set; }
        public DbSet<Teacher> teachers { get; set; }
        public DbSet<Subject> subjects { get; set; }
        
    }
}
