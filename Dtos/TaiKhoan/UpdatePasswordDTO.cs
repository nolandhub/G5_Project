using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi.Dtos.TaiKhoan
{
    public class UpdatePasswordDTO
    {
        public string oldPassword { get; set; }
        public string? newPassword { get; set; }
        public string? rePassword { get; set; }
    }

}