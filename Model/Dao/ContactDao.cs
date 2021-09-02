using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ContactDao
    {
        MyBlogDbContext db = null;
        public ContactDao()  // Tạo Contructor Kịch bản ánh xạ lên cơ sở dữ liệu
        {
            db = new MyBlogDbContext();
        }

        public Contact GetActiveContactById(int id)
        {
            return db.Contacts.SingleOrDefault(x => x.ID == id && x.Status == true); // Lấy ra contact với id truyền vào
        }

        public List<Contact> GetListAllContact()
        {
            return db.Contacts.ToList();
        }

        public Contact GetContactById(int id)
        {
            return db.Contacts.Find(id);
        }

        public int Insert(Contact entity) // tạo một hàm trả về khi thêm mới một bản ghi tag
        {

            var result = db.Contacts.SingleOrDefault(x => x.Name == entity.Name);
            if (result == null &&  entity.Content != null)
            {
                db.Contacts.Add(entity);           // Thêm bản ghi vào bảng Contact
                db.SaveChanges();              //Lưu trữ thay đổi
                return entity.ID;     // Trả về ID của bản ghi Contact truyền vào
            }
            else
            {
                return 0;
            }
        }
        public bool Update(Contact entity) // Truyền vào một đối tượng Contact
        {
            try
            {
                var contact = db.Contacts.Find(entity.ID);  // Tìm đến đội tượng ID contact truyền vào
                if (entity.Content != null)
                {
                    contact.Name = entity.Name;
                    contact.Status = entity.Status;
                    contact.Content = entity.Content;
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
                var contact = db.Contacts.Find(id);  // Tìm đến bản ghi
                db.Contacts.Remove(contact); // Di chuyển bản ghi
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
            var ct = db.Contacts.Find(id); // tìm đến Contact với id truyền vào
            ct.Status = !ct.Status;   // Thay đổi trạng thái của Tag
            db.SaveChanges();           // Lưu lại thay đổi
            return !ct.Status;          // trả về bool thay đổi 
        }
    }
}
