using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Areas.Admin.Controllers
{
    public class ContactController : BaseController
    {
        // GET: Admin/Contact
        public ActionResult Index()
        {
            var model = new ContactDao().GetListAllContact();
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var ct = new ContactDao().GetContactById(id);    // tạo biến lấy ra contact theo id truyền vào
            return View(ct);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Contact ct)
        {

            if (ModelState.IsValid)
            {

                var dao = new ContactDao(); // Tạo biến dao ánh xạ đến tầng dao thao tác với CSDL
                var result = dao.Update(ct);  // tạo biến lưu lại giá trị sau khi thay đổi thông tin
                if (result)  // Kiểm tra chỉnh sửa thành công
                {

                    TempData["Message"] = "Thông tin liên hệ " + ct.Name + " cập nhật thành công"; // Trả về thông báo update thành công
                    return RedirectToAction("Index", "Contact"); // Trả về một Controller Contact với phương thức Index

                }
                else
                {
                    TempData["ErrorUpdate"] = "Cập nhật thông tin liên hệ thất bại";
                    ModelState.AddModelError("", "Cập nhật thông tin liên hệ thất bại");  // Trả về một thông báo lỗi
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
        public ActionResult Create(Contact ct)
        {
            if (ModelState.IsValid)  // Kiểm tra dữ liệu đầu vào  
            {
                var result = new ContactDao();
                int id = result.Insert(ct); // Tạo một biến lưu trữ dữ liệu trả về kiểu ID sau khi thực hiện hàm thêm bản ghi
                if (id > 0)  // Kiểm tra id > 0 <=> đã thêm vào thành công
                {
                    TempData["Message"] = "Thông tin liên hệ: " + ct.Name + " đã được tạo thành công"; // Trả về thông báo tạo Contact thành công
                    return RedirectToAction("Index", "Contact"); // Trả về một Controller Tag với phương thức Index
                }
                else
                {
                    TempData["ErrorCreate"] = "Tạo mới Contact không thành công";  // tạo một biến lưu trữ thông tin lỗi và gửi đến View 
                    ModelState.AddModelError("", "Tạo mới Contact không thành công");  // Trả về một thông báo lỗi
                    return View("Index");
                }
            }
            return View();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new ContactDao().Delete(id); // Gọi hàm xóa đối tượng, truyền vào id đối tượng
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult ChangeStatus(int id)
        {

            var result = new ContactDao().ChangeStatus(id); // tạo biến result thay đổi trạng thái của đối tượng
            return Json(new
            {
                status = result // Trả về 1 JSON với trạng thái tương ứng
            });
        }
    }
}