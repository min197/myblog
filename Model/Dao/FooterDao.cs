using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class FooterDao
    {
        MyBlogDbContext db = null;
        public FooterDao()  // Tạo Contructor Kịch bản ánh xạ lên cơ sở dữ liệu
        {
            db = new MyBlogDbContext();
        }

        public Footer GetFooter(string nameFooter)
        {
            return db.Footers.SingleOrDefault(x => x.ID == nameFooter && x.Status == true); // Lấy ra footer với tên trùng với tên footer trong DB
        }
    }
}
