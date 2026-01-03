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

        private readonly IEmailService _emailService;
        public AuthController(IAuthService authservice, IEmailService emailService)
        {
            _authservice = authservice;
            _emailService = emailService;
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



        [HttpPost("Sendverify")]
        [AllowAnonymous]
        public async Task<IActionResult> SendVerify(EmailForm info)
        {
            var result = await _emailService.SendVerification(info);
            return StatusCode(result.StatusCode, result);
        }


        [HttpGet("VerifyEmail")]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyEmail([FromQuery] string token)
        {
            var result = await _emailService.VerifyEmail(token);

            if (result.Success)
            {
                return Content(
    "<html><head><meta charset='UTF-8'></head><body><h1>Xác thực email thành công!</h1><p>Bây giờ bạn có thể đăng nhập.</p></body></html>",
    "text/html", System.Text.Encoding.UTF8
);

            }
            else
            {
                return Content("<html><head><meta charset='UTF-8'></head><body><h1>Xác thực email thất bại!</h1><p>Hãy thử lại!</p></body></html>",
    "text/html", System.Text.Encoding.UTF8);
            }
        }
    }
}