﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ModelView
{
    public class ReplyComment
    {
        public long PostID { get; set; }

        public long ParentID { get; set; }

        public string Content { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Website { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool AcceptContact { get; set; }
    }
}
