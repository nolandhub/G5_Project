using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi.Dtos.TaiKhoan
{
    public class TaiKhoanDTO
    {
        public string? TenTK { get; set; }
        public string? HinhAnh { get; set; }
        public string? TenHienThi { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public bool? IsVerified { get; set; }

        public DateTime CreateAt { get; set; }

    }
}