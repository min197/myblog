using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Areas.Admin.Controllers
{
    public class MenuController : BaseController
    {
        // GET: Admin/Menu
        public ActionResult Index(int page = 1, int pageSize = 10)   // Tạo controller với tham số đầu vào là page và kích thước
        {
            var model = new MenuDao();   // gọi đến MenuDao
            return View(model.PagedListMenu(page, pageSize));  // truyền vào View PagedList
        }

        public ActionResult Edit(int id)
        {
            var menu = new MenuDao().GetById(id);    // tạo biến lấy ra Menu theo id truyền vào
            return View(menu);
        }

        [HttpPost]
        public ActionResult Edit(Menu menu)
        {
            if (ModelState.IsValid)
            {

                var dao = new MenuDao(); // Tạo biến dao ánh xạ đến tầng dao thao tác với CSDL
                var result = dao.Update(menu);  // tạo biến lưu lại giá trị sau khi thay đổi thông tin

                if (result)  // Kiểm tra chỉnh sửa thành công
                {

                    TempData["Message"] = "Menu " + menu.Text + " cập nhật thành công"; // Trả về thông báo update thành công
                    return RedirectToAction("Index", "Menu"); // Trả về một Controller Menu với phương thức Index

                }
                else
                {
                    TempData["ErrorUpdate"] = "Cập nhật Menu thất bại";
                    ModelState.AddModelError("", "Cập nhật Menu thất bại");  // Trả về một thông báo lỗi
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
        public ActionResult Create(Menu newMenu)
        {
            if (ModelState.IsValid)  // Kiểm tra dữ liệu đầu vào  
            {

                var result = new MenuDao();
                int id = result.Insert(newMenu); // Tạo một biến lưu trữ dữ liệu trả về kiểu ID sau khi thực hiện hàm thêm bản ghi

                if (id > 0)  // Kiểm tra id > 0 <=> đã thêm vào thành công
                {
                    TempData["Message"] = "Menu " + newMenu.Text + " được tạo thành công"; // Trả về thông báo tạo Danh mục thành công
                    return RedirectToAction("Index", "Menu"); // Trả về một Controller Menu với phương thức Index

                }
                else
                {
                    TempData["ErrorCreate"] = "Tạo mới Menu thất bại";  // tạo một biến lưu trữ thông tin lỗi và gửi đến View 
                    ModelState.AddModelError("", "Add new Menu unsuccessfully!");  // Trả về một thông báo lỗi
                    return View("Index");
                }
            }
            return View();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new MenuDao().Delete(id); // Gọi hàm xóa đối tượng, truyền vào id đối tượng
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult ChangeStatus(int id)
        {
            var result = new MenuDao().ChangeStatus(id); // tạo biến result thay đổi trạng thái của đối tượng
            return Json(new
            {
                status = result // Trả về 1 JSON với trạng thái tương ứng
            });
        }
    }
}