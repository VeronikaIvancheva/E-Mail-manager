using EmailManager.Data.Implementation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace EmailManager.Data.Seed
{
    public static class RoleExtention
    {
        public static void UpdateDatabase(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>()
                .HasData(new IdentityRole { Name = "Manager", Id = "1", NormalizedName = "manager".ToUpper() });

            modelBuilder.Entity<IdentityRole>()
                .HasData(new IdentityRole { Name = "Operator", Id = "2", NormalizedName = "operator".ToUpper() });

            var adminUser = new User
            {
                Id = "1",
                FirstName = "Jik",
                LastName = "Tak",
                UserName = "admin",
                NormalizedUserName = "admin".ToUpper(),
                Email = "admin@abv.bg",
                NormalizedEmail = "admin@abv.bg".ToUpper(),
                EmailConfirmed = true,
                PhoneNumber = "+111111111",
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                Role = "Manager"
            };

            var hashePass = new PasswordHasher<User>().HashPassword(adminUser, "manager");
            adminUser.PasswordHash = hashePass;

            modelBuilder.Entity<User>()
                .HasData(adminUser);

            modelBuilder.Entity<IdentityUserRole<string>>()
                .HasData(new IdentityUserRole<string>
                {
                    RoleId = "1",
                    UserId = adminUser.Id
                });
        }
    }
}
