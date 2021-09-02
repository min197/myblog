using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class FeedbackDao
    {
        MyBlogDbContext db = null;
        public FeedbackDao()  // Tạo Contructor Kịch bản ánh xạ lên cơ sở dữ liệu
        {
            db = new MyBlogDbContext();
        }

        public long InsertFeedback(Feedback fb)
        {
            db.Feedbacks.Add(fb);
            db.SaveChanges();
            return fb.ID;
        }

        public IEnumerable<Feedback> ListAllFeedbackBySearchAndSize(string searchString, int page, int pageSize)
        {
            IQueryable<Feedback> model = db.Feedbacks;             // Tạo một danh sách kiểu Feedback với khả năng có thể dùng truy vấn 
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.Email.Contains(searchString) || x.Message.Contains(searchString) || x.Phone.ToString().Contains(searchString)); // Tìm kiếm những bản ghi với thuộc tính gần giống với yêu cầu
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);         // trả về danh sách giảm dần 
        }

        public Feedback getFeedbackById(long id)
        {
            return db.Feedbacks.Find(id);
        }

        public bool Delete(long id)  //Tạo một hàm xóa bản ghi thông qua id truyền vào
        {
            try
            {
                var fb = db.Feedbacks.Find(id);  // Tìm đến bản ghi
                db.Feedbacks.Remove(fb); // Di chuyển bản ghi
                db.SaveChanges();     //Lưu thay đổi tại CSDL
                return true;            // trả về biến true khi xóa thành công
            }
            catch (Exception)             // Nếu bắt gặp một ngoại lệ
            {
                return false;           // trả về false
            }
        }

        public bool ChangeStatus(long id)
        {
            var fb = db.Feedbacks.Find(id);
            fb.Status = true;
            db.SaveChanges();
            return fb.Status;
        }
    }
}
