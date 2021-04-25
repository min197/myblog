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
            ViewBag.PostAbout = new PostDao().GetPostAbout("About");
            ViewBag.ListMember = new UserDao().GetListTypeUser(1);
            return View();
        }

        public ActionResult AboutUser(long UserID)
        {
            var getPost = new PostDao();
            ViewBag.PostByUserID = getPost.GetHotPostByUserID(UserID, 5);
            ViewBag.NewPost = getPost.GetNewPost(3);
            ViewBag.HotPost = getPost.GetHotPost(3);
            var user = new UserDao().GetByID(UserID);
            return View(user);
        }
    }
}