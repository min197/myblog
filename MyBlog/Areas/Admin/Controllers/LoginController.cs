using Model.Dao;
using MyBlog.Areas.Admin.Models;
using MyBlog.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }

        public static long userId;
        public static string userLogin;

        public ActionResult Login(LoginModel model) // Tạo một hành động trả về, truyền vào một biến lớp LoginModel
        {
            if (ModelState.IsValid)  // Nếu biến truyền vào khác rỗng thì thực hiện lệnh
            {
               
                var dao = new UserDao();  // Tạo một biến dao kiểu UserDao (chứa các hàm kiểm tra đăng nhập)
                var result = dao.Login(model.UserName, Encryptor.MD5Hash(model.Passsword));  // tạo biến kết quả kiểm tra thông tin đăng nhập 
                

                if (result == 1)
                {
                    var user = dao.GetByName(model.UserName); // Tạo một biến user lấy thông tin từ dữ liệu vào là tên người dùng
                   
                    var userSession = new UserLogin(); // Tạo một biến kiểu Đăng nhập người dùng
                    userSession.UserName = user.UserName;   // nạp vào tên người dùng
                    userSession.UserID = user.ID; // nạp vào ID

                    Session.Add(CommonConstants.USER_SESSION, userSession); // Thêm vào phiên làm việc thông tin người dùng hiện tại (ID, Username)
                    userId = dao.UserID;
                    userLogin = model.UserName; // Trả về tên người dùng đang đăng nhập
                    return RedirectToAction("Index", "Home"); // Trả về một View Index theo controller Home
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại"); 
                    
                }
                else if( result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản bị khóa");
                }
                else if(result == -2)
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng");
                }
            }
            else
            {
                ModelState.AddModelError("", "Đăng nhập thất bại.");
            }
            return View("Index"); // Khi không thực hiện gì thì trả về view Index
            
        }
    }
}