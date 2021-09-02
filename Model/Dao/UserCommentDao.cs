using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;

namespace Model.Dao
{
    public class UserCommentDao
    {
        MyBlogDbContext db = null;

        public UserCommentDao()  // Tạo Contructor Kịch bản ánh xạ lên cơ sở dữ liệu
        {
            db = new MyBlogDbContext();
        }

        public IEnumerable<UserComment> ListAllUserCommentBySearchAndSize(string searchString, int page, int pageSize)
        {
            IQueryable<UserComment> model = db.UserComments;             // Tạo một danh sách kiểu PostComment với khả năng có thể dùng truy vấn 
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.UserName.Contains(searchString) || x.Email.Contains(searchString) || x.ID.ToString().Contains(searchString) || x.Website.Contains(searchString)); // Tìm kiếm những bản ghi với thuộc tính gần giống với yêu cầu
            }
            return model.OrderByDescending(x => x.ID).ToPagedList(page, pageSize);         // trả về danh sách giảm dần 
        }



    }
}
