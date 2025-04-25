using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi.Dtos.TaiKhoan
{
    public class TaiKhoanShortInfo
    {
        public string? TenHienThi { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public DateTime CreateAt { get; set; }
    }
}