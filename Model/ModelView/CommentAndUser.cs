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

        public long PostID { get; set; }

        public long? parentID { get; set; }

        public string Avatar { get; set; }

        public string Content { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Website { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool AcceptContact { get; set; }
    }
}
