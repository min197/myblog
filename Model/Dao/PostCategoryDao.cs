using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    class PostCategoryDao
    {
        MyBlogDbContext db = null;

        public PostCategoryDao()  // Tạo Contructor Kịch bản ánh xạ lên cơ sở dữ liệu
        {
            db = new MyBlogDbContext();
        }

        public Category ViewDetail(int id)
        {
            return db.Categories.Find(id); //Tìm đến category với id truyền vào
        }
    }
}
