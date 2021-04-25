using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Areas.Admin.Controllers
{
    public class PostTagController : BaseController
    {
        // GET: Admin/PostTag
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            ViewBag.SearchString = searchString;
            var model = new PostTagDao().ListAllPostTag(searchString, page, pageSize);
            return View(model);
        }

        //[HttpGet]
        //public ActionResult Edit(long id)
        //{
        //    ViewBag.listPost = new PostDao().ListPost();
        //    ViewBag.listTag = new TagDao().ListAllTag();
        //    var pt = new PostTagDao().GetById(id);    // tạo biến lấy ra tag theo id truyền vào
        //    return View(pt);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[ValidateInput(false)]
        //public ActionResult Edit(PostTag tag)
        //{
        //    ViewBag.listPost = new PostDao().ListPost();
        //    ViewBag.listTag = new TagDao().ListAllTag();
        //    if (ModelState.IsValid)
        //    {

        //        var dao = new PostTagDao(); // Tạo biến dao ánh xạ đến tầng dao thao tác với CSDL
        //        var result = dao.Update(tag);  // tạo biến lưu lại giá trị sau khi thay đổi thông tin
        //        if (result)  // Kiểm tra chỉnh sửa thành công
        //        {

        //            TempData["Message"] = "Post-Tag update successfully"; // Trả về thông báo update thành công
        //            return RedirectToAction("Index", "PostTag"); // Trả về một Controller Post với phương thức Index

        //        }
        //        else
        //        {
        //            TempData["ErrorUpdate"] = "Update Post-Tag unsuccessfully";
        //            ModelState.AddModelError("", "Update Post-Tag unsuccessfully!");  // Trả về một thông báo lỗi
        //            return View("Index");
        //        }
        //    }
        //    return View("Index"); // Trả về trang index

        //}


        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.listPost = new PostDao().ListPost();
            ViewBag.listTag = new TagDao().ListAllTag();
            return View();
        }

        [HttpPost]
        public ActionResult Create(PostTag newPostTag)
        {
            ViewBag.listPost = new PostDao().ListPost();
            ViewBag.listTag = new TagDao().ListAllTag();
            if (ModelState.IsValid)  // Kiểm tra dữ liệu đầu vào  
            {

                var result = new PostTagDao();
                long id = result.Insert(newPostTag); // Tạo một biến lưu trữ dữ liệu trả về kiểu ID sau khi thực hiện hàm thêm bản ghi
                if (id > 0)  // Kiểm tra id > 0 <=> đã thêm vào thành công
                {
                    TempData["Message"] = "New Relation created successfully"; // Trả về thông báo tạo Danh mục thành công
                    return RedirectToAction("Index", "PostTag"); // Trả về một Controller Tag với phương thức PostTag

                }
                else
                {
                    TempData["ErrorCreate"] = "Create Relation unsuccessfully";  // tạo một biến lưu trữ thông tin lỗi và gửi đến View 
                    ModelState.AddModelError("", "Add new Relation unsuccessfully!");  // Trả về một thông báo lỗi
                    return View("Index");
                }
            }
            return View();
        }

        [HttpDelete]
        public ActionResult Delete(long id)
        {
            new PostTagDao().Delete(id); // Gọi hàm xóa đối tượng, truyền vào id đối tượng
            return RedirectToAction("PostTag");
        }

        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {

            var result = new PostTagDao().ChangeStatus(id); // tạo biến result thay đổi trạng thái của đối tượng
            return Json(new
            {
                status = result // Trả về 1 JSON với trạng thái tương ứng
            });
        }
    }
}