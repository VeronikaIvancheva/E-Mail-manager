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
                Name = "Jik Tak",
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
            
            var Managore = new User
            {
                Id = "2",
                Name = "El Managore",
                UserName = "adminNumbertwo",
                NormalizedUserName = "admin".ToUpper(),
                Email = "banana@abv.bg",
                NormalizedEmail = "banana@abv.bg".ToUpper(),
                EmailConfirmed = true,
                PhoneNumber = "+0895674532",
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                Role = "Manager"
            };

            var NoUser = new User
            {
                Id = "NoUser",
                Name = "Godzilla",
                UserName = "Operator",
                NormalizedUserName = "admin".ToUpper(),
                Email = "cloumba@abv.bg",
                NormalizedEmail = "cloumba@abv.bg".ToUpper(),
                EmailConfirmed = true,
                PhoneNumber = "+0895645254",
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                Role = "Operator"
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
