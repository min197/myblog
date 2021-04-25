using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Areas.Admin.Controllers
{
    public class CommentController : BaseController
    {
        // GET: Admin/Comment
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var model = new CommentDao().ListAllCommentBySearchAndSize(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var cmt = new CommentDao().GetById(id);    // tạo biến lấy ra tag theo id truyền vào
            return View(cmt);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(PostComment cmt)
        {

            if (ModelState.IsValid)
            {

                var dao = new CommentDao(); // Tạo biến dao ánh xạ đến tầng dao thao tác với CSDL
                var result = dao.Update(cmt, LoginController.userLogin);  // tạo biến lưu lại giá trị sau khi thay đổi thông tin
                if (result)  // Kiểm tra chỉnh sửa thành công
                {

                    TempData["Message"] = "Comment update successfully"; // Trả về thông báo update thành công
                    return RedirectToAction("Index", "Comment"); // Trả về một Controller Post với phương thức Index

                }
                else
                {
                    TempData["ErrorUpdate"] = "Update comment unsuccessfully";
                    ModelState.AddModelError("", "Update Comment unsuccessfully!");  // Trả về một thông báo lỗi
                    return View("Index");
                }
            }
            return View("Index"); // Trả về trang index

        }


        [HttpDelete]
        public ActionResult Delete(long id)
        {
            new CommentDao().Delete(id); // Gọi hàm xóa đối tượng, truyền vào id đối tượng
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {

            var result = new CommentDao().ChangeStatus(id); // tạo biến result thay đổi trạng thái của đối tượng
            return Json(new
            {
                status = result // Trả về 1 JSON với trạng thái tương ứng
            });
        }
    }
}