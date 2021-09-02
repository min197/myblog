using Common;
using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Areas.Admin.Controllers
{
    public class FeedbackController : BaseController
    {
        // GET: Admin/Feedback
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {

            var model = new FeedbackDao().ListAllFeedbackBySearchAndSize(searchString, page, pageSize);
            return View(model);
        }

        [HttpDelete]
        public ActionResult Delete(long id)
        {
            new FeedbackDao().Delete(id); // Gọi hàm xóa đối tượng, truyền vào id đối tượng
            return RedirectToAction("Index");
        }


        [HttpPost]
        public JsonResult RepFeedback(string id, string message)  // Controller trả lời feedback 
        {
            long idFb = int.Parse(id);  // lấy id truyền vào dạng json và chuyển đổi về kiểu long
            var feedback = new FeedbackDao().getFeedbackById(idFb);

            if (feedback != null)
            {
                // Tạo biến string lưu trữ thông tin form gửi về email
    
                // Gửi thông báo về email người dùng
                string contentToUser = System.IO.File.ReadAllText(Server.MapPath("~/assets/admin/Template/TemplateEmailReplyFeedback.html"));
                contentToUser = contentToUser.Replace("{{Name}}", feedback.Name);
                contentToUser = contentToUser.Replace("{{Message}}", message);
                new MailHelper().SendMail(feedback.Email, "Myblog trả lời tin nhắn của " + feedback.Name + " đã đóng góp vào " + feedback.CreatedDate.ToString(), contentToUser);
                bool statusFb = new FeedbackDao().ChangeStatus(idFb);
                return Json(new
                {
                    status = true,
                });
            }
            else
            {
                return Json(new
                {
                    status = false,
                    message = "Câu trả lời gửi về không thành công"
                });
            }
        }


    }
}