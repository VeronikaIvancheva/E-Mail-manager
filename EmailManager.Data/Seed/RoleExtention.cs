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

            var adminUser = SeedAdmin();
            var manager = SeedManager();
            var noUser = SeedNoUser();

            modelBuilder.Entity<User>()
                .HasData(adminUser);

            modelBuilder.Entity<IdentityUserRole<string>>()
                .HasData(new IdentityUserRole<string>
                {
                    RoleId = "1",
                    UserId = adminUser.Id
                });           

            modelBuilder.Entity<User>()
                .HasData(manager);

            modelBuilder.Entity<IdentityUserRole<string>>()
                .HasData(new IdentityUserRole<string>
                {
                    RoleId = "1",
                    UserId = manager.Id
                });


            modelBuilder.Entity<User>()
                .HasData(noUser);

            modelBuilder.Entity<IdentityUserRole<string>>()
                .HasData(new IdentityUserRole<string>
                {
                    RoleId = "2",
                    UserId = noUser.Id
                });
        }

        private static User SeedAdmin()
        {
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

            var hashePass = new PasswordHasher<User>().HashPassword(adminUser, "manager");
            adminUser.PasswordHash = hashePass;

            return adminUser;
        }

        private static User SeedNoUser()
        {
            var noUser = new User
            {
                Id = "NoUser",
                Name = "Godzilla",
                UserName = "Operator",
                NormalizedUserName = "godzilla".ToUpper(),
                Email = "cloumba@abv.bg",
                NormalizedEmail = "cloumba@abv.bg".ToUpper(),
                EmailConfirmed = true,
                PhoneNumber = "+0895645254",
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                Role = "Operator"
            };

            var hashePass = new PasswordHasher<User>().HashPassword(noUser, "nouser");
            noUser.PasswordHash = hashePass;

            return noUser;
        }

        private static User SeedManager()
        {
            var manager = new User
            {
                Id = "2",
                Name = "El Managore",
                UserName = "adminNumbertwo",
                NormalizedUserName = "elmanagore".ToUpper(),
                Email = "banana@abv.bg",
                NormalizedEmail = "banana@abv.bg".ToUpper(),
                EmailConfirmed = true,
                PhoneNumber = "+0895674532",
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                Role = "Manager"
            };

            var hashePass = new PasswordHasher<User>().HashPassword(manager, "banana");
            manager.PasswordHash = hashePass;

            return manager;
        }
    }
}
