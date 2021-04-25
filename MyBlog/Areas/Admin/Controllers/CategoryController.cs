using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        // GET: Admin/Category
        public ActionResult Index(int page = 1, int pageSize = 20)  // truyền vào tham số trang và kích thước 
        {
            var dao = new CategoryDao();
            var model = dao.ListAllCategoryPage(page, pageSize);  // tạo biến gọi ra hàm và truyền vào tham số
            return View(model);
        }

        public ActionResult Edit(long id)
        {
            var cat = new CategoryDao().GetById(id);    // tạo biến lấy ra Danh mục theo id truyền vào
            return View(cat);
        }

        [HttpPost]
        public ActionResult Edit(Category cat)
        {
            if (ModelState.IsValid)
            {
                
                var dao = new CategoryDao(); // Tạo biến dao ánh xạ đến tầng dao thao tác với CSDL
                var result = dao.Update(cat, LoginController.userLogin);  // tạo biến lưu lại giá trị sau khi thay đổi thông tin
                if (result)  // Kiểm tra chỉnh sửa thành công
                {
                    TempData["Message"] = "Category " + cat.Title + " update successfully"; // Trả về thông báo update thành công
                    return RedirectToAction("Index", "Category"); // Trả về một Controller User với phương thức Index

                }
                else
                {
                    TempData["ErrorUpdate"] = "Update Category unsuccessfully";
                    ModelState.AddModelError("", "Update Category unsuccessfully!");  // Trả về một thông báo lỗi
                    return View("Index");
                }
            }
            else return View("Index"); // Trả về trang index

        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(); // Lấy về view danh sách danh mục
        }

        [HttpPost]
        public ActionResult Create(Category newCategory)
        {
            if (ModelState.IsValid)  // Kiểm tra dữ liệu đầu vào  
            {
                newCategory.CreatedBy = LoginController.userLogin;    // Gán cho đối tượng đang đăng nhập 
                newCategory.CreatedDate = DateTime.Now;
                newCategory.ModifiedBy = LoginController.userLogin;
                newCategory.ModifiedDate = DateTime.Now;
                var result = new CategoryDao();

                long id = result.Insert(newCategory); // Tạo một biến lưu trữ dữ liệu trả về kiểu ID sau khi thực hiện hàm thêm bản ghi


                if (id > 0)  // Kiểm tra id > 0 <=> đã thêm vào thành công
                {
                    TempData["Message"] = "Category " + newCategory.Title + " created successfully"; // Trả về thông báo tạo Danh mục thành công
                    return RedirectToAction("Index", "Category"); // Trả về một Controller Category với phương thức Index

                }
                else
                {
                    TempData["ErrorCreate"] = "Create new category unsuccessfully";  // tạo một biến lưu trữ thông tin lỗi và gửi đến View 
                    ModelState.AddModelError("", "Add new category unsuccessfully!");  // Trả về một thông báo lỗi
                    return View("Index");
                }
            }
            return View();  
        }

        [HttpDelete]
        public ActionResult Delete(long id)
        {
            new CategoryDao().Delete(id); // Gọi hàm xóa đối tượng, truyền vào id đối tượng
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new CategoryDao().ChangeStatus(id); // tạo biến result thay đổi trạng thái của đối tượng
            return Json(new
            {
                status = result // Trả về 1 JSON với trạng thái tương ứng
            });
        }

        [HttpPost]
        public JsonResult ChangeShowOnHome(long id)
        {
            var result = new CategoryDao().ChangeShowOnHome(id); // tạo biến result thay đổi trạng thái của đối tượng
            return Json(new
            {
                status = result // Trả về 1 JSON với trạng thái tương ứng
            });
        }
    }
}