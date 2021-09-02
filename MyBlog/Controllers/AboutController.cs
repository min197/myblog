using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Controllers
{
    public class AboutController : Controller
    {
        // GET: About
        public ActionResult Index()
        {
            ViewBag.PostAbout = new PostDao().GetPostAbout("About"); //Lấy ra bài post với tên truyền vào "About"
            
            ViewBag.ListMember = new UserDao().GetListTypeUser(1); // Lấy ra danh sách người dùng với phân loại kiểu 1 - hiển thị trên trang Thông tin
            return View();
        }

        // Thông tin về user
        public ActionResult AboutUser(long UserID) 
        {
            var getPost = new PostDao();
            ViewBag.PostByUserID = getPost.GetHotPostByUserID(UserID, 5); // Lấy ra post theo user Id truyền vào, số lượng 5 posts
            ViewBag.NewPost = getPost.GetNewPost(3);  
            ViewBag.HotPost = getPost.GetHotPost(3);
            var user = new UserDao().GetByID(UserID);
            return View(user);
        }
    }
}