using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MyWebApi.Dtos.TaiKhoan;
using MyWebApi.Model;
using webAPI.Data;

namespace MyWebApi.Helpers
{
    public class JwtAuthToken
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly JwtSecurityTokenHandler _tokenHandler = new JwtSecurityTokenHandler();

        public JwtAuthToken(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public LoginResponeDTO Authenticate(TaiKhoan user)
        {

            var issuer = _configuration["JwtConfig:Issuer"];
            var audience = _configuration["JwtConfig:Audience"];
            var key = _configuration["JwtConfig:Key"];
            var tokenValidityMins = _configuration.GetValue<int>("JwtConfig:TokenValidityMins");
            var tokenExpiryTimeStamp = DateTime.UtcNow.AddMinutes(tokenValidityMins);

            var tokenDescriptor = new SecurityTokenDescriptor
            {

                Subject = new ClaimsIdentity(new Claim[]
                {
                   new Claim(JwtRegisteredClaimNames.NameId, user.MaTK.ToString()),
                   new Claim(JwtRegisteredClaimNames.GivenName, user.TenHienThi ),
                   new Claim(ClaimTypes.Role, user.LoaiTK.ToString())

                }),
                Expires = tokenExpiryTimeStamp,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)), SecurityAlgorithms.HmacSha256Signature),
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(securityToken);

            return new LoginResponeDTO
            {
                MaTK = user.MaTK,
                TenHienThi = user.TenHienThi,
                Token = accessToken,
                ExpireIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.UtcNow).TotalSeconds
            };
        }
    }
}