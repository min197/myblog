using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyBlog.Areas.Admin.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Username?")] // Tạo yêu cầu khi hiển thị lên View
        public string UserName { set; get; }

        [Required(ErrorMessage = "Password?")] 
        public string Passsword { set; get; }

        public bool RememberMe { set; get; }
    }
}