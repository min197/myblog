using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Areas.Admin.Controllers
{
    public class SlideController : BaseController
    {
        // GET: Admin/Slide
        public ActionResult Index(int page = 1, int pageSize = 10)   // Tạo controller với tham số đầu vào là page và kích thước
        {
            var model = new SlideDao();   // gọi đến SlideDao
            return View(model.PagedListSlide(page, pageSize));  // truyền vào View PagedList
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var slide = new SlideDao().GetById(id);    // tạo biến lấy ra Slide theo id truyền vào
            return View(slide);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Slide slide)
        {
            if (ModelState.IsValid)
            {

                var dao = new SlideDao(); // Tạo biến dao ánh xạ đến tầng dao thao tác với CSDL

                var result = dao.Update(slide, LoginController.userLogin);  // tạo biến lưu lại giá trị sau khi thay đổi thông tin

                if (result)  // Kiểm tra chỉnh sửa thành công
                {

                    TempData["Message"] = "Slide " + slide.Title + " cập nhật thành công"; // Trả về thông báo update thành công
                    return RedirectToAction("Index", "Slide"); // Trả về một Controller Slide với phương thức Index

                }
                else
                {
                    TempData["ErrorUpdate"] = "Cập nhật slide thất bại";
                    ModelState.AddModelError("", "Update Slide unsuccessfully!");  // Trả về một thông báo lỗi
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
        public ActionResult Create(Slide newSlide)
        {
            if (ModelState.IsValid)  // Kiểm tra dữ liệu đầu vào  
            {

                var result = new SlideDao();

                int id = result.Insert(newSlide, LoginController.userLogin); // Tạo một biến lưu trữ dữ liệu trả về kiểu ID sau khi thực hiện hàm thêm bản ghi

                if (id > 0)  // Kiểm tra id > 0 <=> đã thêm vào thành công
                {
                    TempData["Message"] = "Slide " + newSlide.Title + " được tạo thành công."; // Trả về thông báo tạo Danh mục thành công
                    return RedirectToAction("Index", "Slide"); // Trả về một Controller Slide với phương thức Index

                }
                else
                {
                    TempData["ErrorCreate"] = "Tạo mới slide thất bại.";  // tạo một biến lưu trữ thông tin lỗi và gửi đến View 
                    ModelState.AddModelError("", "Add new Slide unsuccessfully!");  // Trả về một thông báo lỗi
                    return View("Index");
                }
            }
            return View();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new SlideDao().Delete(id); // Gọi hàm xóa đối tượng, truyền vào id đối tượng
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult ChangeStatus(int id)
        {

            var result = new SlideDao().ChangeStatus(id); // tạo biến result thay đổi trạng thái của đối tượng
            return Json(new
            {
                status = result // Trả về 1 JSON với trạng thái tương ứng
            });
        }
    }
}