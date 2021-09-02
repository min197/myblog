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

        public Footer GetActiveFooterById(int id)
        {
            return db.Footers.SingleOrDefault(x => x.ID == id && x.Status == true); // Lấy ra footer với tên trùng với tên footer trong DB
        }

        public List<Footer> GetListAllFooter()
        {
            return db.Footers.ToList();
        }


        public Footer GetFootertById(int id)
        {
            return db.Footers.Find(id);
        }

        public int Insert(Footer entity) // tạo một hàm trả về khi thêm mới một bản ghi tag
        {

            var result = db.Footers.SingleOrDefault(x => x.Name == entity.Name);
            if (result == null && entity.Content != null)
            {
                db.Footers.Add(entity);           // Thêm bản ghi vào bảng Footer
                db.SaveChanges();              //Lưu trữ thay đổi
                return entity.ID;     // Trả về ID của bản ghi Footer truyền vào
            }
            else
            {
                return 0;
            }
        }
        public bool Update(Footer entity) // Truyền vào một đối tượng Footer
        {
            try
            {
                var Footer = db.Footers.Find(entity.ID);  // Tìm đến đội tượng ID Footer truyền vào
                if (entity.Content != null)
                {
                    Footer.Name = entity.Name;
                    Footer.Status = entity.Status;
                    Footer.Content = entity.Content;
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
                var Footer = db.Footers.Find(id);  // Tìm đến bản ghi
                db.Footers.Remove(Footer); // Di chuyển bản ghi
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
            var ft = db.Footers.Find(id); // tìm đến Footer với id truyền vào
            ft.Status = !ft.Status;   // Thay đổi trạng thái của Tag
            db.SaveChanges();           // Lưu lại thay đổi
            return !ft.Status;          // trả về bool thay đổi 
        }

    }
}
