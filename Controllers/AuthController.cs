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

        private readonly IAuthService _authservice;
        public AuthController(IAuthService authservice)
        {
            _authservice = authservice;
        }


        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterDTO regis)
        {
            var result = await _authservice.Register(regis);
            return StatusCode(result.StatusCode, result);
        }


        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDTO log)
        {

            if (string.IsNullOrEmpty(log.TenTK) || string.IsNullOrEmpty(log.MatKhau))
            {
                return BadRequest(new { message = "Tên đăng nhập và mật khẩu không được để trống" });
            }

            var result = await _authservice.Login(log);
            return StatusCode(result.StatusCode, result);
        }


        [HttpPost("Test")]
        [Authorize(Roles = "1,2")]
        public async Task<IActionResult> Test()
        {
            return Ok("Api Work");
        }

    }
}