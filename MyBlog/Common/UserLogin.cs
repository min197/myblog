using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBlog.Common
{
    [Serializable]  // Tạo thuộc tính sắp xếp theo thứ tự khi lưu trữ
    public class UserLogin
    {
        public long UserID { set; get; }  
        public string UserName { set; get; }
    }
}