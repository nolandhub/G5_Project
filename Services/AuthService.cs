using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyWebApi.Dtos;
using MyWebApi.Dtos.TaiKhoan;
using MyWebApi.Helpers;
using MyWebApi.Interfaces.IServices;
using MyWebApi.Model;
using webAPI.Data;

namespace MyWebApi.Services
{
    public class AuthService : IAuthService
    {

        private readonly AppDbContext _context;

        private readonly JwtAuthToken _jwtAuthToken;

        public AuthService(AppDbContext context, JwtAuthToken jwtAuthToken)
        {
            _context = context;
            _jwtAuthToken = jwtAuthToken;
        }




        public async Task<ResultDTO> Login(LoginDTO log)
        {
            try
            {
                var user = await _context.TaiKhoans.FirstOrDefaultAsync(c => c.TenTK == log.TenTK);
                if (user == null || !PasswordHasher.Verify(user.MatKhau, log.MatKhau))
                {
                    return new ResultDTO
                    {
                        Success = false,
                        Message = "Tên Đăng nhập hoặc mật khẩu không chính xác ",
                        StatusCode = 401
                    };
                }

                // loginResponse Chứa id,Tên hiển thị , Token,ExpireIn do bên jwt đã trả về trong "Authenticate"
                var loginResponse = _jwtAuthToken.Authenticate(user);

                return new ResultDTO
                {
                    Success = true,
                    Message = "Đăng nhập thành công",
                    StatusCode = 200,
                    Data = loginResponse
                };
            }
            catch (Exception ex)
            {
                return new ResultDTO
                {
                    Success = false,
                    Message = $"Lỗi khi đăng nhập: {ex.Message}",
                    StatusCode = 500
                };
            }
        }

        public async Task<ResultDTO> Register(RegisterDTO regis)
        {
            try
            {
                var hashPash = PasswordHasher.Hash(regis.MatKhau);

                var existingAccount = await _context.TaiKhoans
                    .Where(t => t.TenTK == regis.TenTK || t.TenHienThi == regis.TenHienThi)
                    .ToListAsync();

                if (existingAccount.Any(t => t.TenTK == regis.TenTK))
                {
                    return new ResultDTO
                    {
                        Success = false,
                        Message = "Tên tài khoản đã tồn tại.",
                        StatusCode = 400  // Bad Request
                    };
                }


                var user = new TaiKhoan
                {
                    TenTK = regis.TenTK,
                    MatKhau = hashPash,
                    TenHienThi = regis.TenHienThi,
                    Email = regis.Email,
                    Phone = regis.Phone,
                    IsVerified = false,
                    Xoa = false,
                    LoaiTK = 3,
                    CreateAt = DateTime.Now,
                };

                await _context.TaiKhoans.AddAsync(user);
                await _context.SaveChangesAsync();

                return new ResultDTO
                {
                    Success = true,
                    Message = "Đăng ký thành công",
                    StatusCode = 201,
                    Data = new { user.TenTK, user.TenHienThi, user.Email, user.Phone },
                };
            }
            catch (Exception ex)
            {
                return new ResultDTO
                {
                    Success = false,
                    Message = $"Lỗi khi đăng ký tài khoản: {ex.Message}",
                    StatusCode = 500
                };
            }
        }
    }
}