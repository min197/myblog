using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using Model.ModelView;

namespace Model.Dao
{
    public class TagDao
    {
        MyBlogDbContext db = null;
        public TagDao()  // Tạo Contructor Kịch bản ánh xạ lên cơ sở dữ liệu
        {
            db = new MyBlogDbContext();
        }

        public List<Tag> ListAllTag()  // Hàm lấy ra tất cả các tag
        {
            return db.Tags.Where(x => x.Status == true).OrderBy(x => x.CreatedDate).ToList();   // lấy ra những tag với trạng thái true, ngày tạo
        }

   

        public IEnumerable<Tag> ListAllTagBySize(string searchString, int page, int pageSize)  // Lấy ra các tag với từ khóa, trang hiện tại và kích thước
        {
            IQueryable<Tag> model = db.Tags;   // Tạo biến truy vấn hỗ trợ tải chậm
            if (!string.IsNullOrEmpty(searchString))    // Kiểm tra chuỗi tìm kiếm nhập vào
            {
                model = model.Where(x => x.Title.Contains(searchString) || x.Content.Contains(searchString)); // Tìm kiếm những bản ghi với thuộc tính gần giống với yêu cầu
            }
            return db.Tags.OrderBy(x => x.CreatedDate).ToPagedList(page, pageSize);
        }

        public Tag GetById(long id)
        {
            return db.Tags.Find(id);      // Tìm bản tag với id truyền vào
        }

        public bool checkNullTag(Tag tag)
        {
            if (tag.Title != null && tag.Content != null)          // Kiểm tra các trường của bản ghi
            {
                return true;
            }
            else return false;
        }

        public long Insert(Tag entity) // tạo một hàm trả về khi thêm mới một bản ghi tag
        {

            var result = db.Tags.SingleOrDefault(x => x.Title == entity.Title);
            if (result == null && checkNullTag(entity))
            {
                db.Tags.Add(entity);           // Thêm bản ghi vào bảng Tag
                db.SaveChanges();              //Lưu trữ thay đổi
                return entity.ID;     // Trả về ID của bản ghi Tag truyền vào
            }
            else
            {
                return 0;
            }
        }
        public bool Update(Tag entity, string userName) // Truyền vào một đối tượng Post, tên user quản trị đang đăng nhập
        {
            try
            {
                var tag = db.Tags.Find(entity.ID);  // Tìm đến đội tượng ID post truyền vào
                if (checkNullTag(entity))
                {
                    tag.Title = entity.Title;
                    tag.MetaTitle = entity.MetaTitle;
                    tag.ModifiedBy = userName;
                    tag.Status = entity.Status;
                    tag.Content = entity.Content;
                    tag.ModifiedDate = DateTime.Now;
                }
                db.SaveChanges();    // Lưu các thay đổi sau  khi ghi đầy đủ dữ liệu
                return true;            // trả về đúng khi thành công
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(long id)  //Tạo một hàm xóa bản ghi thông qua id truyền vào
        {
            try
            {
                var tag = db.Tags.Find(id);  // Tìm đến bản ghi
                db.Tags.Remove(tag); // Di chuyển bản ghi
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
            var tag = db.Tags.Find(id); // tìm đến Tag với id truyền vào
            tag.Status = !tag.Status;   // Thay đổi trạng thái của Tag
            db.SaveChanges();           // Lưu lại thay đổi
            return !tag.Status;          // trả về bool thay đổi 
        }

    }
}
