using HospitalManagement.Api.Entities;
using Microsoft.EntityFrameworkCore;
using HospitalManagement.Api.Data;

namespace HospitalManagement.Api.Data
{
    public static class DbSeeder
    {
        public static void Seed(ApplicationDbContext db)
        {
            if (!db.Roles.Any())
            {
                db.Roles.AddRange(
                    new Role
                    {
                        RoleId = 1,
                        RoleName = "Admin"
                    },
                    new Role
                    {
                        RoleId = 2,
                        RoleName = "Super Admin"
                    }
                );

                db.SaveChanges();
            }

            if (!db.Admins.Any())
            {
                var adminPassword = Environment.GetEnvironmentVariable("SEED_ADMIN_PASSWORD");

                if (string.IsNullOrWhiteSpace(adminPassword))
                {
                    throw new InvalidOperationException(
                        "SEED_ADMIN_PASSWORD is not configured."
                    );
                }

                db.Admins.Add(
                    new Admin
                    {
                        FullName = "Hospital Administrator",
                        Email = "admin@medicareai.com",
                        MobileNumber = "9876500001",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword(adminPassword),
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow,
                        RoleId = 1
                    }
                );

                db.SaveChanges();
            }
        }
    }
}