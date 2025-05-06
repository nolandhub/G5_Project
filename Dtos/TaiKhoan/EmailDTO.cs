using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi.Dtos.TaiKhoan
{
    public class EmailForm
    {
        public string ToEmail { get; set; }
        public string ToName { get; set; }
        public string Subject { get; set; }
        public string PlainText { get; set; }
        public string HtmlContent { get; set; }
        public string VerificationLink { get; set; }

    }
}