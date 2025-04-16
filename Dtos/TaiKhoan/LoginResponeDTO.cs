using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi.Dtos.TaiKhoan
{
    public class LoginResponeDTO
    {
        public int MaTK { get; set; }
        public string TenHienThi { get; set; }
        public string Token { get; set; }
        public int ExpireIn { get; set; }

    }
}