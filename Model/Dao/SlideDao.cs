using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class SlideDao
    {
        MyBlogDbContext db = null;
        public SlideDao()  // Tạo Contructor Kịch bản ánh xạ lên cơ sở dữ liệu
        {
            db = new MyBlogDbContext();
        }

        public List<Slide> ListAll()
        {
            return db.Slides.Where(x => x.Status == true).OrderBy(x => x.DisplayOrder).ToList(); // Gọi ra tất cả các slide hiện có, với sắp xếp tăng dần 
        }

        public IEnumerable<Slide> PagedListSlide(int page, int pageSize) 
        {
            IQueryable<Slide> model = db.Slides; // Tạo biến dạng IQueryable tham chiếu đến bảng Slide
            return model.OrderBy(x => x.ID).OrderBy(x => x.DisplayOrder).ToPagedList(page, pageSize); // Lấy ra biến theo sắp xếp theo thứ tự tăng dần
        }


        public Slide GetById(int id)
        {
            return db.Slides.Find(id);
        }


        public bool checkNullSlide(Slide slide)
        {
            if (slide.Title != null && slide.Link != null)   // Kiểm tra các trường không null cuả slide truyền vào
            {
                return true;
            }
            else return false;
        }

        public int Insert(Slide entity, string user) // tạo một hàm trả về khi thêm mới một bản ghi menu
        {
            var result = db.Slides.SingleOrDefault(e=>e.Title == entity.Title);
            if (result == null && checkNullSlide(entity))
            {
                entity.CreatedDate = DateTime.Now;
                entity.CreatedBy = user;
                db.Slides.Add(entity);           // Thêm bản ghi vào bảng Menu
                db.SaveChanges();              //Lưu trữ thay đổi
                return entity.ID;     // Trả về ID của bản ghi Menu truyền vào
            }
            else
            {
                return 0;
            }

        }

        public bool Update(Slide entity, string user) // Truyền vào một đối tượng Menu, tên user quản trị đang đăng nhập
        {
            try
            {
                var dao = db.Slides.Find(entity.ID);  // Tìm đến đội tượng ID menu truyền vào
                if (dao != null)
                {
                    dao.ModifiedBy = user;
                    dao.Title = entity.Title;                // Thay đổi dữ liệu các trường
                    dao.Status = entity.Status;
                    dao.Link = entity.Link;
                    dao.Image = entity.Image;
                    dao.ModifiedDate = DateTime.Now;
                    dao.Descriptions = entity.Descriptions;
                    dao.DisplayOrder = entity.DisplayOrder;
                }
                db.SaveChanges();    // Lưu các thay đổi sau  khi ghi đầy đủ dữ liệu
                return true;            // trả về đúng khi thành công
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int id)  //Tạo một hàm xóa bản ghi thông qua id truyền vào
        {
            try
            {
                var slide = db.Slides.Find(id);  // Tìm đến bản ghi
                db.Slides.Remove(slide); // Di chuyển bản ghi
                db.SaveChanges();     //Lưu thay đổi tại CSDL
                return true;            // trả về biến true khi xóa thành công
            }
            catch (Exception)             // Nếu bắt gặp một ngoại lệ
            {
                return false;           // trả về false
            }
        }

        public bool ChangeStatus(int id)
        {
            var slide = db.Slides.Find(id); // tìm đến Category với id truyền vào
            slide.Status = !slide.Status;   // Thay đổi trạng thái của Category
           
            db.SaveChanges();           // Lưu lại thay đổi
            return !slide.Status;          // trả về bool thay đổi 
        }
    }
}
