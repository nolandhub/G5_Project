using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWebApi.Dtos.TaiKhoan;
using MyWebApi.Interfaces.IServices;
using webAPI.Data;

namespace MyWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IAuthService _authservice;
        public AuthController(AppDbContext context, IAuthService authservice)
        {
            _context = context;
            _authservice = authservice;
        }


        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromForm] RegisterDTO regis)
        {
            var result = await _authservice.Register(regis);
            return StatusCode(result.StatusCode, result);
        }


        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromForm] LoginDTO log)
        {

            if (string.IsNullOrEmpty(log.TenTK) || string.IsNullOrEmpty(log.MatKhau))
            {
                return BadRequest(new { message = "Tên đăng nhập và mật khẩu không được để trống" });
            }

            var result = await _authservice.Login(log);
            return StatusCode(result.StatusCode, result);
        }



        [HttpPost("Test")]
        [Authorize]
        public async Task<IActionResult> Test()
        {

            return Ok("Api Work");
        }

    }
}