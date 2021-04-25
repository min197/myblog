using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class MenuDao
    {
        MyBlogDbContext db = null;
        public MenuDao()  // Tạo Contructor Kịch bản ánh xạ lên cơ sở dữ liệu
        {
            db = new MyBlogDbContext();
        }

        public List<Menu> ListByTypeId (int typeId) // Tạo một hàm lấy ra danh sách Menu với biến truyền vào là typeId
        {
            return db.Menus.Where(x => x.TypeID == typeId && x.Status == true).ToList(); // trả về danh sách cần lấy
        }

        public IEnumerable<Menu> PagedListMenu(int page, int pageSize)
        {
            IQueryable<Menu> model = db.Menus;   // Tạo biến IQueryable tham chiếu đến bảng Menu trong db
            return model.OrderBy(x => x.TypeID).OrderBy(x => x.DisplayOrder).ToPagedList(page, pageSize);  // lấy ra danh sách menu theo dạng PagedList
        }

        public Menu GetById(int id)
        {
            return db.Menus.Find(id);       // Lấy ra menu với id truyền vào
        }

        public bool checkNullMenu(Menu menu)
        {
            if (menu.Link != null && menu.Text != null && menu.TypeID > 0)   // Kiểm tra các trường không null  và phù hợp
            {
                return true;            
            }
            else return false;
        }

        public int Insert(Menu entity) // tạo một hàm trả về khi thêm mới một bản ghi menu
        {
            var result = db.Menus.SingleOrDefault(x => x.Text == entity.Text);
            if (result == null && checkNullMenu(entity))
            {
                db.Menus.Add(entity);           // Thêm bản ghi vào bảng Menu
                db.SaveChanges();              //Lưu trữ thay đổi
                return entity.ID;     // Trả về ID của bản ghi Menu truyền vào
            }
            else
            {
                return 0;
            }

        }

        public bool Update(Menu entity) // Truyền vào một đối tượng Menu, tên user quản trị đang đăng nhập
        {
            try
            {
                var menu = db.Menus.Find(entity.ID);  // Tìm đến đội tượng ID menu truyền vào
                if (menu != null)
                {
                    menu.Text = menu.Text;                // Thay đổi dữ liệu các trường
                    menu.Status = entity.Status;
                    menu.Link = entity.Link;
                    menu.Target = entity.Target;
                    menu.TypeID = entity.TypeID;
                    
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
                var menu = db.Menus.Find(id);  // Tìm đến bản ghi
                db.Menus.Remove(menu); // Di chuyển bản ghi
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
            var menu = db.Menus.Find(id); // tìm đến Category với id truyền vào
            menu.Status = !menu.Status;   // Thay đổi trạng thái của Category
            db.SaveChanges();           // Lưu lại thay đổi
            return !menu.Status;          // trả về bool thay đổi 
        }

    }
}
