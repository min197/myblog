using BotDetect.Web.Mvc;
using Model.Dao;
using Model.EF;
using Model.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Controllers
{
    public class BlogController : Controller
    {
        // GET: Blog

       // public long postID;

        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)  // Truyền vào controller từ khóa tìm kiếm và số trang, kích cỡ trang
        {
            
            ViewBag.ListAllTag = new TagDao().ListAllTag();  // Lấy ra tất cả các tag hiện có với trạng thái true
            ViewBag.getCategory = new CategoryDao().ListAllCategory(); // Tạo biến viewbag lấy ra tất cả danh mục hiện có 
            var getPost = new PostDao();      
            ViewBag.NewPost = getPost.GetNewPost(3);    // viewbag lưu 3 bản ghi mới nhất
            ViewBag.HotPost = getPost.GetHotPost(3);   // viewbag lưu 3 bản ghi hot nhất
            ViewBag.SearchString = searchString;    //viewbag lưu lại từ khóa tìm kiếm
            var model = new PostDao().ListAllPostByStatus(searchString, page, pageSize);   // tạo biến lưu trữ danh sách bản ghi post 
            if (model.Count() != 0 && searchString != null)
            {
                TempData["MessageSuccess"] = "Đã tìm thấy blog liên quan:";  // truyền xuống View tin nhắn
                return View(model);
            }
            else if(model.Count() == 0 && searchString != null)
            {
                TempData["MessageError"] = "Không tìm thấy blog nào có liên quan đến từ khóa, hãy thử tìm lại ^^";
                return View(model);
            }
            else
            {
                return View(model);
            }
        }

        public ActionResult Detail(long PostId)  // Hàm hiển thị bản ghi với biến Id truyền vào
        {
          //  postID = PostId;
            
            ViewBag.TagOnPost = new PostTagDao().ListTagOnPost(PostId);   // Lấy ra tất cả các tag liên quan đến bản ghi
            ViewBag.getCategory = new CategoryDao().ListAllCategory();   // Tương tự như trên Index
            var post = new PostDao().GetById(PostId);    // Lấy ra bản ghi
            if(post != null)
            {
                var countRead = new PostDao().countRead(PostId);  // Tăng biến đếm của bản ghi
            }
            ViewBag.Category = new CategoryDao().GetById(post.CategoryID.Value);    
            var getPost = new PostDao();
            ViewBag.RelatedPost = getPost.GetRelatedPost(PostId, 5);
            ViewBag.NewPost = getPost.GetNewPost(3);
            ViewBag.HotPost = getPost.GetHotPost(3);
            return View(post);
        }

        [ChildActionOnly]
        public ActionResult TagAndCount(long TagId)
        {
            ViewBag.CountPostOnTag = new PostTagDao().CountPostOnTag(TagId);
            return View();
        }

        public ActionResult BlogTag(long TagId,string searchString, int pageIndex = 1, int pageSize = 5 )  // Hàm hiển thị các blog thuộc tag
        {

            ViewBag.getCategory = new CategoryDao().ListAllCategory(); // Tạo biến viewbag lấy ra tất cả danh mục hiện có 
            var getPost = new PostDao();
            ViewBag.NewPost = getPost.GetNewPost(3);    // viewbag lưu 3 bản ghi mới nhất
            ViewBag.HotPost = getPost.GetHotPost(3);   // viewbag lưu 3 bản ghi hot nhất

            ViewBag.Tag = new TagDao().GetById(TagId);  // Lấy ra tag theo biến ID truyền vào

            int totalPost = 0;   // số lượng bản ghi mặc định = 0
            ViewBag.SearchString = searchString;   // chuỗi tìm kiếm được gán
            ViewBag.PostOnTag = new PostTagDao().ListPostOnTag(TagId, searchString, ref totalPost, pageIndex, pageSize);
            ViewBag.Total = totalPost;

            ViewBag.Page = pageIndex;  // Trang hiển thị hiện tại
            int maxPage = 5;
            int totalPage = 0;
            totalPage = (int)Math.Ceiling((double)(totalPost / pageSize));  // số trang sẽ bằng số bản ghi chia kích thước trang, làm tròn chẵn
            ViewBag.TotalPage = totalPage;

            ViewBag.Maxpage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = pageIndex + 1;   
            ViewBag.Prev = pageIndex - 1;

            //ViewBag.ListAllTag = new TagDao().ListAllTag();
            ViewBag.ListAllTag = new PostTagDao().ListTagAndCountPost();

            if (totalPost!= 0 && searchString != null)
            {
                TempData["MessageSuccess"] = "Đã tìm thấy bài viết liên quan:";  // truyền xuống View tin nhắn
                return View();
            }
            else if (totalPost == 0 && searchString != null)
            {
                TempData["MessageError"] = "Không tìm thấy bài viết nào có liên quan đến từ khóa, hãy thử tìm lại ^^";
                return View();
            }
            else
            {
                return View();
            }
           
        }

        public ActionResult BlogCategory(long CatId, string searchString, int page = 1, int pageSize = 10)
        {
            ViewBag.getAllCategory = new CategoryDao().ListAllCategory();    // Tương tự như trên Index
            var post = new PostDao().ListAllPostByCategory(CatId, searchString, page, pageSize);
            ViewBag.Category = new CategoryDao().GetById(CatId);
            var getPost = new PostDao();
            ViewBag.NewPost = getPost.GetNewPost(3);
            ViewBag.HotPost = getPost.GetHotPost(3);
            if (post.Count() != 0 && searchString != null)     // Kiểm tra kết quả tìm kiếm và từ khóa tìm kiếm
            {
                TempData["MessageSuccess"] = "Đã tìm thấy blog liên quan:";
                return View(post);
            }
            else if (post.Count() == 0 && searchString != null)
            {
                TempData["MessageError"] = "Không tìm thấy blog nào có liên quan đến từ khóa, hãy thử tìm lại ^^";
                return View(post);
            }
            else
            {
                return View(post);
            }
        }

        public PartialViewResult GetComments(long postId)
        {
            //var model = new CommentDao().GetListCommentByPostId(postId);
            var model = new CommentDao().GetCommentAndUserByPostId(postId);
            ViewBag.PostId = postId;
            return PartialView(model);
        }

        [HttpGet]
        public ActionResult AddComment(long postId)
        {
            ViewBag.PostID = postId;
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CaptchaValidationActionFilter("CaptchaCode", "CommentCaptcha", "Bạn đã nhập sai mã Captcha!")]
        public ActionResult AddComment(CommentAndUser newComment, long postId)
        {
            if (ModelState.IsValid)
            {
                var user = new UserComment();
                var cmt = new PostComment();

                user.UserName = newComment.UserName;
                user.Email = newComment.Email;
                user.Website = newComment.Website;
                user.AcceptContact = newComment.AcceptContact;


                cmt.Content = newComment.Content;
                cmt.PostID = postId;

                long resultAddComment = new CommentDao().InsertCommentAndUser(cmt, user);

                if (resultAddComment > 0)
                {
                    TempData["SuccessMessage"] = "Bình luận sẽ được kiểm duyệt, cảm ơn bạn đã để lại bình luận^^";
                }
                else if (resultAddComment == -1)
                {
                    TempData["ErrorMessage1"] = "Bình luận không thành công, bạn đã bình luận với một tên khác!";
                }
                else if(resultAddComment == -2)
                {
                    TempData["ErrorMessage2"] = "Bình luận không thành công, hãy nhập đúng thông tin!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Bình luận không thành công, đã xảy ra lỗi gì đó, xin vui lòng thử lại";
                }
            }
            else
            {
                MvcCaptcha.ResetCaptcha("CommentCaptcha");
            }
            return RedirectToAction("Detail", "Blog", new { postId = postId });
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[ValidateInput(false)]
        //public ActionResult AddSubComment(CommentAndUser newComment, long ComId)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        var user = new UserComment();
        //        var cmt = new PostComment();

        //        user.UserName = newComment.UserName;
        //        user.Email = newComment.Email;
        //        user.Website = newComment.Website;
        //        user.AcceptContact = newComment.AcceptContact;


        //        cmt.Content = newComment.Content;
        //        cmt.ParentID = ComId;
        //        cmt.PostID = new CommentDao().GetById(ComId).PostID;

        //        long resultAddComment = new CommentDao().InsertCommentAndUser(cmt, user);

        //        if (resultAddComment > 0)
        //        {
        //            TempData["SuccessMessage"] = "Bình luận sẽ được kiểm duyệt, cảm ơn bạn đã để lại bình luận^^";
        //        }
        //        else if (resultAddComment == -1)
        //        {
        //            TempData["ErrorMessage1"] = "Bình luận không thành công, bạn đã bình luận với một tên khác!";
        //        }
        //        else if (resultAddComment == -2)
        //        {
        //            TempData["ErrorMessage2"] = "Bình luận không thành công, hãy nhập đúng thông tin!";
        //        }
        //        else
        //        {
        //            TempData["ErrorMessage"] = "Bình luận không thành công, đã xảy ra lỗi gì đó, xin vui lòng thử lại";
        //        }

        //    }
        //    return RedirectToAction("Detail", "Blog", new { postId = new CommentDao().GetById(ComId).PostID });
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[ValidateInput(false)]
        //public PartialViewResult AddSubComment(CommentAndUser newComment)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = new UserComment();
        //        var cmt = new PostComment();

        //        user.UserName = newComment.UserName;
        //        user.Email = newComment.Email;
        //        user.Website = newComment.Website;
        //        user.AcceptContact = newComment.AcceptContact;


        //        cmt.Content = newComment.Content;

        //        long resultAddComment = new CommentDao().InsertCommentAndUser(cmt, user);
        //        if (resultAddComment != 0)
        //        {
        //            TempData["SuccessMessage"] = "Bình luận sẽ sẽ được kiểm duyệt, cảm ơn bạn đã để lại bình luận^^";
        //        }
        //        else
        //        {
        //            TempData["ErrorMessage"] = "Bình luận không thành công, hãy nhập đúng thông tin!";
        //        }

        //    }
        //    return PartialView();
        //}


        /* 
         * public ActionResult BlogsCategory(string searchString, int page = 1, int pageSize = 10)
         {
             ViewBag.getCategory = new CategoryDao().ListAll();
             var getPost = new PostDao();
             ViewBag.NewPost = getPost.GetNewPost(3);
             ViewBag.HotPost = getPost.GetHotPost(3);
             ViewBag.SearchString = searchString;
             var model = new PostDao().ListAllPostByCategory(searchString, page, pageSize);
             return View(model);
         }
         */

    }
}