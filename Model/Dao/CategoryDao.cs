using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class CategoryDao
    {
        MyBlogDbContext db = null;
        public CategoryDao()  // Tạo Contructor Kịch bản ánh xạ lên cơ sở dữ liệu
        {
            db = new MyBlogDbContext();
        }

        public IEnumerable<Category> ListAllCategoryPage(int page, int pageSize) //Duyệt phần tử dạng Categories từ danh sách, trả về số lượng phần tử theo kích thước cần
        {
            IQueryable<Category> model = db.Categories; // Tạo một biến với kiểu Query LinQ, biến này có thể thực hiện các truy vấn 

            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize); // Hàm toPagedList của Plugin Pagedlist
        }

        public List<Category> ListAllCategory() // Lấy danh sách tất cả các danh mục
        {
            return db.Categories.Where(x => x.Status == true && x.ShowOnHome == true).OrderBy(x => x.ID).ToList();
        }

        public List<Category> ListCategoryByParentId(long parentId) // Lấy danh sách danh mục con theo danh mục cha
        {
            return db.Categories.Where(x => x.Status == true && x.ShowOnHome == true && x.ParentID == parentId).OrderBy(x => x.ID).ToList();
        }

        public IEnumerable<Category> ListCategory()
        {
            return db.Categories.Where(x => x.Status == true).OrderByDescending(x => x.CreatedDate).ToList();
        }


        public IEnumerable<Category> ListCategoryPost()
        {
            return db.Categories.Where(x => x.Status == true && x.ShowOnHome == true).OrderBy(x => x.ID).ToList();
        }

        public bool checkNullCategory(Category cat) // Kiểm tra danh mục hợp lệ hay không
        {
            if (cat.Title != null && cat.MetaTitle != null )
            {
                return true;
            }
            else return false;
        }

        public long Insert(Category entity) // tạo một hàm trả về khi thêm mới một bản ghi Category
        {
            var result = db.Categories.SingleOrDefault(x => x.Title == entity.Title);
            if (result == null && checkNullCategory(entity))
            {
                db.Categories.Add(entity);           // Thêm bản ghi vào bảng Category
                db.SaveChanges();              //Lưu trữ thay đổi
                return entity.ID;     // Trả về ID của bản ghi Category truyền vào
            }
            else
            {
                return 0;
            }

        }

        public bool Update(Category entity, string userName) // Truyền vào một đối tượng Category, tên user quản trị đang đăng nhập
        {
            try
            {
                var cat = db.Categories.Find(entity.ID);  // Tìm đến đội tượng ID Category truyền vào
                cat.Title = entity.Title;                // Thay đổi dữ liệu các trường
                cat.Status = entity.Status;
                cat.ShowOnHome = entity.ShowOnHome;
                cat.ParentID = entity.ParentID;
                cat.ModifiedBy = userName;
                cat.MetaTitle = entity.MetaTitle;
                cat.ModifiedDate = DateTime.Now;
                db.SaveChanges();    // Lưu các thay đổi sau  khi ghi đầy đủ dữ liệu
                return true;            // trả về đúng khi thành công
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Category GetById(long id)
        {
            return db.Categories.Find(id);

        }

        public bool Delete(long id)  //Tạo một hàm xóa bản ghi thông qua id truyền vào
        {
            try
            {
                var cat = db.Categories.Find(id);  // Tìm đến bản ghi
                db.Categories.Remove(cat); // Di chuyển bản ghi
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
            var cat = db.Categories.Find(id); // tìm đến Category với id truyền vào
            cat.Status = !cat.Status;   // Thay đổi trạng thái của Category
            db.SaveChanges();           // Lưu lại thay đổi
            return !cat.Status;          // trả về bool thay đổi 
        }

        public bool ChangeShowOnHome(long id)   // Chỉnh sửa trạng thái hiển thị tại trang Home
        {
            var cat = db.Categories.Find(id);     
            cat.ShowOnHome = !cat.ShowOnHome;
            db.SaveChanges();  
            return !cat.ShowOnHome;
        }
    }
}
