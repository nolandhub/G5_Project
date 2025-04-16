using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi.Dtos
{
    public class ResultDTO
    {
        public bool Success { get; set; }       // Trạng thái thành công
        public string? Message { get; set; }     // Thông báo chi tiết
        public object? Data { get; set; }        // Dữ liệu bổ sung (nếu có)
        public int StatusCode { get; set; }     // Mã trạng thái HTTP
    }
}