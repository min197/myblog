using MyBlog.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MyBlog.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        // GET: Admin/Base
        protected override void OnActionExecuting(ActionExecutingContext filterContext) // Tạo phương thức ghi đè hàm kiểm tra session, có thể kế thừa
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION]; // tạo một biến ép kiểu về class UserLogin
            if(session == null) // kiểm tra tiến trình đăng nhập trả về
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(new { controller = "Login", action = "Index",Area = "Admin"})); // Kiểm tra nếu session sai thì sẽ trả về dường dẫn khác
            }
            base.OnActionExecuting(filterContext); 
        }
    }
}