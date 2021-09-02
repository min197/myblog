using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Areas.Admin.Controllers
{
    public class UserCommentController : BaseController
    {
        // GET: Admin/UserComment
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10) 
        {
            // Lấy ra tất cả comment theo searchString truyền vào
            var model = new UserCommentDao().ListAllUserCommentBySearchAndSize(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
    }
}