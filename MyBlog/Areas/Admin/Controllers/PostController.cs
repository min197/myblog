using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Areas.Admin.Controllers
{
    public class PostController : BaseController
    {
        // GET: Admin/Post
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            var dao = new PostDao();
            var model = dao.ListAllPost(searchString, page, pageSize);   // Tạo biến lưu trữ danh sách các phần tử phù hợp với yêu cầu
            ViewBag.SearchString = searchString;
            return View(model);
        }

        public ActionResult Edit(long id)
        {
            ViewBag.ListPost = new PostDao().ListPost();
            ViewBag.ListCategoryPost = new CategoryDao().ListCategoryPost();
            var post = new PostDao().GetById(id);    // tạo biến lấy ra Danh mục theo id truyền vào
            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Post post)
        {
            ViewBag.ListPost = new PostDao().ListPost();
            ViewBag.ListCategoryPost = new CategoryDao().ListCategoryPost();
            if (ModelState.IsValid)
            {

                var dao = new PostDao(); // Tạo biến dao ánh xạ đến tầng dao thao tác với CSDL
                var result = dao.Update(post, LoginController.userLogin);  // tạo biến lưu lại giá trị sau khi thay đổi thông tin
                if (post.Status == true)
                {
                    post.DatePublished = DateTime.Now;
                }
                if (result)  // Kiểm tra chỉnh sửa thành công
                {
                   
                    TempData["Message"] = "Post " + post.Title + " update successfully"; // Trả về thông báo update thành công
                    return RedirectToAction("Index", "Post"); // Trả về một Controller Post với phương thức Index

                }
                else
                {
                    TempData["ErrorUpdate"] = "Update Post unsuccessfully";
                    ModelState.AddModelError("", "Update Post unsuccessfully!");  // Trả về một thông báo lỗi
                    return View("Index");
                }
            }

            return View("Index"); // Trả về trang index

        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.ListPost = new PostDao().ListPost();
            ViewBag.ListCategoryPost = new CategoryDao().ListCategoryPost();
            return View(); // Lấy về view danh sách danh mục
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Post newPost)
        {
            ViewBag.ListPost = new PostDao().ListPost();
            ViewBag.ListCategoryPost = new CategoryDao().ListCategoryPost();
            if (ModelState.IsValid)  // Kiểm tra dữ liệu đầu vào  
            {
                
                var post = new PostDao();
                newPost.AuthorID = LoginController.userId;
                newPost.AuthorName = post.GetFullNameAuthorById(LoginController.userId);    // Gán cho đối tượng đang đăng nhập 
                newPost.CreatedDate = DateTime.Now;
                newPost.CreatedBy = LoginController.userLogin;
                newPost.ModifiedBy = LoginController.userLogin;
                newPost.ModifiedDate = DateTime.Now;
                newPost.CountRead = 0;
                newPost.HistoryModified = "Create by " + LoginController.userLogin + " " + DateTime.Now.ToString();
                if(newPost.Status == true)
                {
                    newPost.DatePublished = DateTime.Now;
                }
                var result = new PostDao();
                long id = result.Insert(newPost); // Tạo một biến lưu trữ dữ liệu trả về kiểu ID sau khi thực hiện hàm thêm bản ghi
                if (id > 0)  // Kiểm tra id > 0 <=> đã thêm vào thành công
                {
                    TempData["Message"] = "Post " + newPost.Title + " created successfully"; // Trả về thông báo tạo Danh mục thành công
                    return RedirectToAction("Index", "Post"); // Trả về một Controller Post với phương thức Index

                }
                else
                {
                    TempData["ErrorCreate"] = "Create new post unsuccessfully";  // tạo một biến lưu trữ thông tin lỗi và gửi đến View 
                    ModelState.AddModelError("", "Add new post unsuccessfully!");  // Trả về một thông báo lỗi
                    return View("Index");
                }
            }
            return View();
        }

        [HttpDelete]
        public ActionResult Delete(long id)
        {
            new PostDao().Delete(id); // Gọi hàm xóa đối tượng, truyền vào id đối tượng
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            
            var result = new PostDao().ChangeStatus(id); // tạo biến result thay đổi trạng thái của đối tượng
            return Json(new
            {
                status = result // Trả về 1 JSON với trạng thái tương ứng
            });
        }

       
    }
}