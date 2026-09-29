using HospitalManagement.Api.Data;
using HospitalManagement.Api.DTOs;
using HospitalManagement.Api.Entities;
using HospitalManagement.Api.Helpers;
using HospitalManagement.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Api.Services
{
    public class AdminService : IAdminService
    {
        private readonly ApplicationDbContext _context;
        private readonly JwtHelper _jwtHelper;

        public AdminService(
            ApplicationDbContext context,
            JwtHelper jwtHelper)
        {
            _context = context;
            _jwtHelper = jwtHelper;
        }
        public async Task<bool> RegisterAdminAsync(AdminRegisterDto dto)
        {
            // Check email
            var emailExists = await _context.Admins
                .AnyAsync(a => a.Email == dto.Email);

            if (emailExists)
            {
                return false;
            }

            // Check mobile number
            var mobileExists = await _context.Admins
                .AnyAsync(a => a.MobileNumber == dto.MobileNumber);

            if (mobileExists)
            {
                return false;
            }

            // Check role
            var roleExists = await _context.Roles
                .AnyAsync(r => r.RoleId == dto.RoleId);

            if (!roleExists)
            {
                return false;
            }

            // Create admin
            var admin = new Admin
            {
                FullName = dto.FullName,
                Email = dto.Email,
                MobileNumber = dto.MobileNumber,

                PasswordHash = PasswordHelper.HashPassword(
                    dto.Password
                ),

                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                RoleId = dto.RoleId
            };

            _context.Admins.Add(admin);

            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<AdminLoginResponseDto?> LoginAdminAsync(
            AdminLoginDto dto)
        {
            // Find admin and load Role
            var admin = await _context.Admins
                .Include(a => a.Role)
                .FirstOrDefaultAsync(a => a.Email == dto.Email);

            // Admin not found
            if (admin == null)
            {
                return null;
            }

            // Admin inactive
            if (!admin.IsActive)
            {
                return null;
            }

            // Verify password
            var passwordValid = PasswordHelper.VerifyPassword(
                dto.Password,
                admin.PasswordHash
            );

            if (!passwordValid)
            {
                return null;
            }

            // Update last login
            admin.LastLogin = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            // Generate JWT token with role
            var token = _jwtHelper.GenerateToken(
                admin,
                admin.Role.RoleName
            );

            // Return login response
            return new AdminLoginResponseDto
            {
                Token = token,

                Admin = new AdminResponseDto
                {
                    AdminId = admin.AdminId,
                    FullName = admin.FullName,
                    Email = admin.Email,
                    MobileNumber = admin.MobileNumber,
                    IsActive = admin.IsActive,
                    CreatedAt = admin.CreatedAt,
                    LastLogin = admin.LastLogin,

                    // Role information
                    RoleId = admin.RoleId,
                    RoleName = admin.Role.RoleName
                }
            };
        }
    }
}