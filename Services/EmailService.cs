using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyWebApi.Dtos;
using MyWebApi.Dtos.TaiKhoan;
using MyWebApi.Helpers;
using MyWebApi.Interfaces.IServices;
using MyWebApi.Model;
using SendGrid;
using SendGrid.Helpers.Mail;
using webAPI.Data;


namespace MyWebApi.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;

        private readonly IWebHostEnvironment _environment;
        public EmailService(AppDbContext context, IConfiguration configuration, IWebHostEnvironment environment, JwtAuthToken jwtauthtoken)
        {
            _configuration = configuration;
            _context = context;
            _environment = environment;

        }

        public async Task<ResultDTO> SendVerification(EmailForm info)
        {

            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY") ?? _configuration["SendGrid:ApiKey"];
            var senderEmail = _configuration["SendGrid:FromEmail"];
            var senderName = _configuration["SendGrid:FromName"];

            try
            {

                string templatePath = Path.Combine(_environment.ContentRootPath, "Templates", "authEmail.html");
                string htmlTemplate = await File.ReadAllTextAsync(templatePath);

                htmlTemplate = htmlTemplate.Replace("{{UserName}}", info.ToName);
                htmlTemplate = htmlTemplate.Replace("{{VerificationLink}}", info.VerificationLink);



                var client = new SendGridClient(apiKey);

                var from = new EmailAddress(senderEmail, senderName);
                var to = new EmailAddress(info.ToEmail, info.ToName);
                var subject = info.Subject;
                var plainText = info.PlainText;
                var htmlContent = htmlTemplate;


                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainText, htmlContent);
                var response = await client.SendEmailAsync(msg);

                if (response.IsSuccessStatusCode)
                {
                    return new ResultDTO
                    {
                        Success = true,
                        Message = "Gửi thành công, hãy nhấp vào link để xác thực email của bạn.",
                        Data = null,
                        StatusCode = 200
                    };
                }
                else
                {
                    return new ResultDTO
                    {
                        Success = false,
                        Message = $"Gửi thất bại. Mã trạng thái: {response.StatusCode}",
                        Data = null,
                        StatusCode = (int)response.StatusCode
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResultDTO
                {
                    Success = false,
                    Message = $"Lỗi khi gửi email: {ex.Message}",
                    Data = null,
                    StatusCode = 500
                };
            }
        }

        public async Task<ResultDTO> VerifyEmail(string token)
        {

            var issuer = _configuration["JwtConfig:Issuer"];
            var audience = _configuration["JwtConfig:Audience"];
            var key = _configuration["JwtConfig:Key"];



            var tokenHandler = new JwtSecurityTokenHandler();
            var secret = Encoding.UTF8.GetBytes(key);

            try
            {
                // Xác thực token và lấy principal
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(secret),
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                // Lấy email từ token
                var email = principal.FindFirst(ClaimTypes.Email)?.Value
                    ?? principal.FindFirst(JwtRegisteredClaimNames.Email)?.Value;


                var user = await _context.TaiKhoans.SingleOrDefaultAsync(u => u.Email == email);

                if (user.IsVerified == true)
                {
                    return new ResultDTO
                    {
                        Success = true,
                        Message = "Email đã được xác thực",
                        Data = null,
                        StatusCode = 200
                    };
                }


                user.IsVerified = true;
                await _context.SaveChangesAsync();

                return new ResultDTO
                {
                    Success = true,
                    Message = "Xác thực email thành công, đăng nhập để tiếp tục.",
                    Data = null,
                    StatusCode = 200
                };

            }
            catch (SecurityTokenExpiredException)
            {
                return new ResultDTO
                {
                    Success = false,
                    Message = "Token đã hết hạn",
                    Data = null,
                    StatusCode = 401,
                };

            }
            catch (SecurityTokenInvalidSignatureException)
            {
                return new ResultDTO
                {
                    Success = false,
                    Message = "Chữ ký token không hợp lệ",
                    StatusCode = 401,
                    Data = null,
                };
            }
            catch (SecurityTokenException ex)
            {
                return new ResultDTO
                {
                    Success = false,
                    Message = "Token không hợp lệ: " + ex.Message,
                    StatusCode = 401,
                    Data = null,
                };
            }
            catch (Exception ex)
            {
                return new ResultDTO
                {
                    // Log lỗi ở đây nếu cần
                    Success = false,
                    Message = "Đã xảy ra lỗi: " + ex.Message,
                    StatusCode = 500,
                    Data = null
                };
            }

        }
    }
}
