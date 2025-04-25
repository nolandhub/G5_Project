using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi.Dtos.TaiKhoan
{
    public class CreateFullDTO
    {
        public string TenTK { get; set; }
        public string MatKhau { get; set; }
        public string? HinhAnh { get; set; }
        public string TenHienThi { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public int LoaiTK { get; set; }
    }
}