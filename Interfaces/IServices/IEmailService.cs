using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyWebApi.Dtos;
using MyWebApi.Dtos.TaiKhoan;

namespace MyWebApi.Interfaces.IServices
{
    public interface IEmailService
    {
        public Task<ResultDTO> SendVerification(EmailForm info);

        public Task<ResultDTO> VerifyEmail(string token);
    }
}