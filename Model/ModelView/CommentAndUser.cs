using Model.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ModelView
{
    public class CommentAndUser
    {
        public long CommentID { get; set; }

        public long? parentID { get; set; }

        public string Avatar { get; set; }

        [Display(Name = "Nội dung bình luận")]
        [Required(ErrorMessage ="Hãy nhập nội dung bình luận")]
        [StringLength(1000, MinimumLength = 10, ErrorMessage = "Bình luận lớn hơn 10 ký tự và nhỏ hơn 500")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Hãy nhập tên")]
        [Display(Name = "Tên của bạn")]
        [RegularExpression("([a-zA-Z0-9 .&'-]+)", ErrorMessage = "Tên chỉ có thể từ chữ cái và số")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Hãy nhập Email")]
        [Display(Name = "Email của bạn")]
        [EmailAddress(ErrorMessage = "Hãy điền đúng Email")]
        [RegularExpression(@"^([A-Za-z0-9][^'!&\\#*$%^?<>()+=:;`~\[\]{}|/,₹€@ ][a-zA-z0-9-._][^!&\\#*$%^?<>()+=:;`~\[\]{}|/,₹€@ ]*\@[a-zA-Z0-9][^!&@\\#*$%^?<>()+=':;~`.\[\]{}|/,₹€ ]*\.[a-zA-Z]{2,6})$", ErrorMessage = "Hãy điền đúng Email")]
        public string Email { get; set; }

        [Display(Name = "Website (nếu có)")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Địa chỉ trang web quá ngắn")]
        [RegularExpression(@"((?:https?\:\/\/|\/.)(?:[-a-z0-9]+\.)*[-a-z0-9]+.*)", ErrorMessage = "Hãy điền đúng Website")]
        public string Website { get; set; }

        public DateTime CreatedDate { get; set; }

        [Display(Name = "Đồng ý nhận thông báo qua Email?")]
        public bool AcceptContact { get; set; }
    }
}
