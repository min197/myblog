using Common;
using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            var model = new ContactDao().GetActiveContactById(1); // Lấy ra thông tin liên hệ với id = 1
            return View(model);
        }


        // Thêm mới feedback
        [HttpPost]
        public JsonResult AddFeedback(string name, string email, string phone, string message)
        {
            // Tạo biến feedback lưu dữ liệu dạng string truyền vào từ ajax
            var feedback = new Feedback();
            feedback.Name = name;
            feedback.Email = email;
            feedback.Phone = phone;
            feedback.Message = message;
            feedback.CreatedDate = DateTime.Now;

            long result = new FeedbackDao().InsertFeedback(feedback);
            if (result > 0)
            {
                // Tạo biến string lưu trữ thông tin form gửi về email
                string contentToAdmin = System.IO.File.ReadAllText(Server.MapPath("~/assets/client/Template/TemplateEmailFeedbackToAdmin.html"));
                contentToAdmin = contentToAdmin.Replace("{{Name}}", feedback.Name);
                contentToAdmin = contentToAdmin.Replace("{{Email}}", feedback.Email);
                contentToAdmin = contentToAdmin.Replace("{{Phone}}", feedback.Phone);
                contentToAdmin = contentToAdmin.Replace("{{Message}}", feedback.Message);
                // Gửi thông báo về email quản trị
                new MailHelper().SendMailToAdmin("Tin nhắn từ " + feedback.Name + " đến Blog", contentToAdmin);

                // Gửi thông báo về email người dùng
                string contentToUser = System.IO.File.ReadAllText(Server.MapPath("~/assets/client/Template/TemplateEmailFeedback.html"));
                contentToUser = contentToUser.Replace("{{Name}}", feedback.Name);
                contentToUser = contentToUser.Replace("{{Message}}", feedback.Message);
                new MailHelper().SendMail(feedback.Email, "Đóng góp của bạn đã được gửi thành công", contentToUser);


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