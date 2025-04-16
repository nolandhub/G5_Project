using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyWebApi.Dtos;
using MyWebApi.Dtos.TaiKhoan;

namespace MyWebApi.Interfaces.IServices
{
    public interface IAuthService
    {

        Task<ResultDTO> Login(LoginDTO log);
        Task<ResultDTO> Register(RegisterDTO regis);



    }
}