using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.EF;
using Model.Dao;
using MyBlog.Common;

namespace MyBlog.Areas.Admin.Controllers
{
    public class UserController : BaseController    // Phải trải qua lớp Base để kiểm tra xem có thể vào trang không
    {
        // GET: Admin/User
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5) //Số lượng trang và số lượng phần tử trong một trang
        {
            var dao = new UserDao(); 
            var model = dao.ListAllPaging(searchString, page, pageSize);   // Tạo biến lưu trữ danh sách các phần tử phù hợp với yêu cầu
            ViewBag.SearchString = searchString;
            return View(model); // Trả về một view truyền vào biến
        }

        public ActionResult Logout()
        {
            Session[CommonConstants.USER_SESSION] = null;
            return Redirect("/Admin/Login");
        }
        public ActionResult ErrorPage()
        {
            return View();
        }


        public ActionResult Edit(long id)
        {
            var user = new UserDao().GetByID(id);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid) // Kiểm tra dữ liệu vào có hợp lệ
            {
                var dao = new UserDao();   // Tạo một biến lớp UserDao, ánh xạ đến cơ sở dữ liệu 
                if (!string.IsNullOrEmpty(user.PasswordHash)) // nếu mật khẩu truyền vào khác rỗng
                {
                    var encryptMd5Pass = Encryptor.MD5Hash(user.PasswordHash); // Tạo một biến chuyển đổi mật khẩu chuyền vào sang dạng mã háo MD5
                    user.PasswordHash = encryptMd5Pass; // gán mật khẩu mới truyền vào cho user mới
                }

                //user.ModifiedBy = LoginController.userLogin; // Lưu tên người quản trị sửa

                var result = dao.Update(user, LoginController.userLogin); // Tạo một biến lưu trữ dữ liệu trả về kiểu bool sau khi thực hiện cập nhật

               
                if (result)  // Kiểm tra chỉnh sửa thành công
                {
                    TempData["Message"] = "User " + user.UserName + " update successfully"; // Trả về thông báo update thành công
                    return RedirectToAction("Index", "User"); // Trả về một Controller User với phương thức Index
                  
                }
                else
                {
                   TempData["ErrorUpdate"] = "Update user unsuccessfully";
                   ModelState.AddModelError("", "Add new user unsuccessfully!");  // Trả về một thông báo lỗi
                   return View("Index");
                }
            }

            return View("Index"); // Trả về trang index
        }



        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid) // Kiểm tra dữ liệu vào có hợp lệ
            {
                var dao = new UserDao();   // Tạo một biến lớp UserDao, ánh xạ đến cơ sở dữ liệu 
                if (!string.IsNullOrEmpty(user.PasswordHash)) // nếu mật khẩu truyền vào khác rỗng
                {
                    var encryptMd5Pass = Encryptor.MD5Hash(user.PasswordHash); // Tạo một biến chuyển đổi mật khẩu chuyền vào sang dạng mã háo MD5
                    user.PasswordHash = encryptMd5Pass; // gán mật khẩu mới truyền vào cho user mới
                }
                    
                    user.CreatedBy = LoginController.userLogin;  // Lưu tên người quản trị tạo
                    user.ModifiedBy = LoginController.userLogin; // Lưu tên người quản trị sửa
                    user.ModifiedDate = DateTime.Now;   // Lưu thời gian chỉnh sửa
                    user.CreatedDate = DateTime.Now;

                    long id = dao.Insert(user); // Tạo một biến lưu trữ dữ liệu trả về kiểu ID sau khi thực hiện hàm thêm bản ghi


                    if (id > 0)  // Kiểm tra id > 0 <=> đã thêm vào thành công
                    {
                        TempData["Message"] = "User " + user.UserName + " created successfully"; // Trả về thông báo tạo user thành công
                        return RedirectToAction("Index", "User"); // Trả về một Controller User với phương thức Index
                        
                    }
                    else
                    {
                        TempData["ErrorCreate"] = "Create new user unsuccessfully";
                        ModelState.AddModelError("", "Add new user unsuccessfully!");  // Trả về một thông báo lỗi
                        return View("Index");
                    }

            }
            return View("Index"); // Trả về trang index
        }

        [HttpDelete]
        public ActionResult Delete(long id)
        {
            new UserDao().Delete(id); // Gọi hàm xóa đối tượng, truyền vào id đối tượng
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new UserDao().ChangeStatus(id); // tạo biến result thay đổi trạng thái của đối tượng
            return Json(new
            {
                status = result // Trả về 1 JSON với trạng thái tương ứng
            });
        }
        
    }
}