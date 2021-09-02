using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.getCategory = new CategoryDao().ListCategoryByParentId(0);  // Lấy ra tất cả danh mục blog hiện có
            var getPost = new PostDao();
            ViewBag.NewPost = getPost.GetNewPost(3);    // Lấy ra 3 bản ghi mới nhất
            ViewBag.HotPost = getPost.GetHotPost(3);   // Lấy ra 3 bản ghi hot nhất
           // ViewBag.Author = new PostDao().getAuthor()
            return View();
        }

        [ChildActionOnly]
        public ActionResult Slide()
        {
            var model = new SlideDao().ListAll();    // Lấy ra danh sách slide
            ViewBag.countSlide = model.Count();  // truyền xuống View số lượng slide trong danh sách
            return PartialView(model);   //truyền xuống view danh sách slide
        }

        //[ChildActionOnly]
        //public ActionResult GetNewPost(int size = 3)
        //{
        //    var model = new PostDao().GetNewPost(size);   // Lấy ra 3 post 
        //    return PartialView(model);
        //}


        [ChildActionOnly]   // Thuộc tính chỉ dành cho những hành động con
        public ActionResult MainMenu() 
        {
            ViewBag.SubMenu = new CategoryDao().ListCategoryByParentId(0);  // lấy ra menu con là danh mục blog
            var model = new MenuDao().ListByTypeId(1);  // tạo biến lấy ra dữ liệu dạng danh sách menu
            return PartialView(model);   // Trả về một phần của View truyền vào danh sách menu
        }

        [ChildActionOnly]   // Thuộc tính chỉ dành cho những hành động con
        public ActionResult Footer()
        {
            var model = new FooterDao().GetFootertById(1); // tạo biến lấy ra dữ liệu footer
            return PartialView(model);   // Trả về một phần của View truyền vào footer
            
        }


    }
}