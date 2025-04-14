using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi.Dtos.TaiKhoan
{
    public class LoginResponeDTO
    {
        public string TenHienThi { get; set; }
        public string? token { get; set; }
        public int expireIn { get; set; }

    }
}