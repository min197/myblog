using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Areas.Admin.Controllers
{
    public class TagController : BaseController
    {
        // GET: Admin/Tag
        public ActionResult Index(String searchString, int page = 1, int pageSize = 10)
        {
            var model = new TagDao().ListAllTagBySize(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }


        [HttpGet]
        public ActionResult Edit(long id)
        {
            var tag = new TagDao().GetById(id);    // tạo biến lấy ra tag theo id truyền vào
            return View(tag);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Tag tag)
        {

            if (ModelState.IsValid)
            {

                var dao = new TagDao(); // Tạo biến dao ánh xạ đến tầng dao thao tác với CSDL
                var result = dao.Update(tag, LoginController.userLogin);  // tạo biến lưu lại giá trị sau khi thay đổi thông tin
                if (result)  // Kiểm tra chỉnh sửa thành công
                {

                    TempData["Message"] = "Tag " + tag.Title + " cập nhật thành công"; // Trả về thông báo update thành công
                    return RedirectToAction("Index", "Tag"); // Trả về một Controller Tag với phương thức Index

                }
                else
                {
                    TempData["ErrorUpdate"] = "Cập nhật tag thất bại.";
                    ModelState.AddModelError("", "Update tag unsuccessfully!");  // Trả về một thông báo lỗi
                    return View("Index");
                }
            }
            return View("Index"); // Trả về trang index

        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(); // Lấy về view danh sách danh mục
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Tag tag)
        {
            if (ModelState.IsValid)  // Kiểm tra dữ liệu đầu vào  
            {
 
                tag.CreatedDate = DateTime.Now;
                tag.CreatedBy = LoginController.userLogin; // Gán cho đối tượng đang đăng nhập 
                tag.ModifiedBy = LoginController.userLogin;
                tag.ModifiedDate = DateTime.Now;
                var result = new TagDao();
                long id = result.Insert(tag); // Tạo một biến lưu trữ dữ liệu trả về kiểu ID sau khi thực hiện hàm thêm bản ghi
                if (id > 0)  // Kiểm tra id > 0 <=> đã thêm vào thành công
                {
                    TempData["Message"] = "Tag " + tag.Title + " đã được tạo thành công."; // Trả về thông báo tạo Danh mục thành công
                    return RedirectToAction("Index", "Tag"); // Trả về một Controller Tag với phương thức Index

                }
                else
                {
                    TempData["ErrorCreate"] = "Tạo mới tag thất bại";  // tạo một biến lưu trữ thông tin lỗi và gửi đến View 
                    ModelState.AddModelError("", "Add new tag unsuccessfully!");  // Trả về một thông báo lỗi
                    return View("Index");
                }
            }
            return View();
        }

        [HttpDelete]
        public ActionResult Delete(long id)
        {
            new TagDao().Delete(id); // Gọi hàm xóa đối tượng, truyền vào id đối tượng
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {

            var result = new TagDao().ChangeStatus(id); // tạo biến result thay đổi trạng thái của đối tượng
            return Json(new
            {
                status = result // Trả về 1 JSON với trạng thái tương ứng
            });
        }

    }
}