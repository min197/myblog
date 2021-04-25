using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;
using PagedList;

namespace Model.Dao
{
    public class UserDao
    {
        MyBlogDbContext db = null;
        public UserDao()  // Tạo Contructor Kịch bản ánh xạ lên cơ sở dữ liệu
        {
            db = new MyBlogDbContext();  
        }
        public long Insert(User entity) // tạo một hàm trả về khi thêm mới một bản ghi User
        {
            var result = db.Users.SingleOrDefault(x => x.UserName == entity.UserName);
            if (result == null && entity.UserName != null && entity.PasswordHash != null)
            {
                db.Users.Add(entity);           // Thêm bản ghi vào bảng User
                db.SaveChanges();              //Lưu trữ thay đổi
                return entity.ID;     // Trả về ID của bản ghi truyền vào
            }
            else
            {
                return 0;
            }
        }

        public IEnumerable<User> GetListTypeUser(int typeId)
        {
            return db.Users.Where(x => x.Status == true && x.TypeUserId == typeId).ToList();
        }

        public User GetByID(long id)
        {
            return db.Users.Find(id);
        }

        public bool Update(User entity, string userName) // Truyền vào một đối tượng User, tên nick đang đăng nhập
        {
            try
            {
                var user = db.Users.Find(entity.ID);  // Tìm đến đội tượng User truyền vào
                if(user != null)
                {
                    user.FirstName = entity.FirstName;   // Ghi dữ liệu cập nhật
                    if (!string.IsNullOrEmpty(entity.PasswordHash)) // Kiểm tra mật khẩu nhập vào, nếu không null và khác rỗng
                    {
                        user.PasswordHash = entity.PasswordHash;
                    }
                    user.About = entity.About;
                    user.JobTitle = entity.JobTitle;
                    user.TypeUserId = entity.TypeUserId;
                    user.LinkToFacebook = entity.LinkToFacebook;
                    user.LinkToInstagram = entity.LinkToInstagram;
                    user.LinkToTwitter = entity.LinkToTwitter;
                    user.LinkToYoutube = entity.LinkToYoutube;
                    user.Avatar = entity.Avatar;
                    user.MidName = entity.MidName;
                    user.LastName = entity.LastName;
                    user.Adress = entity.Adress;
                    user.Mobile = entity.Mobile;
                    user.Email = entity.Email;
                    user.ModifiedDate = DateTime.Now;
                }
                db.SaveChanges();    // Lưu các thay đổi sau  khi ghi đầy đủ dữ liệu
                return true;            // trả về 
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public IEnumerable<User> ListAllPaging(string searchString, int page, int pageSize) //Duyệt phần tử dạng User từ danh sách, trả về số lượng phần tử theo kích thước cần
        {
            IQueryable<User> model = db.Users; // Tạo một biến với kiểu Query LinQ, biến này có thể thực hiện các truy vấn 
            //var model = db.Users.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize); // Tạo một đối tượng lưu trữ danh sách user với số lượng pageSize
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x =>x.UserName.Contains(searchString) || x.LastName.Contains(searchString) || x.FirstName.Contains(searchString)); // Tìm kiếm những bản ghi với thuộc tính gần giống với yêu cầu
            }
            //Nếu thực hiện thêm tìm kiếm thì có thể thêm điều kiện vào và dùng if, else if
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize); // Hàm toPagedList của Plugin Pagedlist
        }

        public User GetByName(string userName)   // Tạo hàm lấy ra bản ghi có UserName trùng khớp
        {
            return db.Users.SingleOrDefault(x => x.UserName == userName);  //  Lấy ra bản ghi đơn hoặc mặc định khi có UserName trùng khớp
        }

        public long UserID;

        public int Login(string userName, string password)  // Tạo hàm đăng nhập 
        {
            var result = db.Users.SingleOrDefault(x => x.UserName == userName);  // Tạo biến result tìm bản ghi có thông tin Username trùng khớp
            if (result == null)
            {
                return 0; // Nếu null -> bản ghi với Username không đúng
            }
            else
            {
                if(result.Status == false)
                {
                    return -1; // Kiểm tra trạng thái kích hoạt của tài khoản, nếu false -> chưa kích hoạt
                }
                else
                {
                    if(result.PasswordHash == password) 
                    {
                        UserID = result.ID; // Lấy ra Id người đăng nhập
                        result.LastLogin = DateTime.Now; // Trả về ngày đăng nhập gần nhất;
                        db.SaveChanges();    //Lưu ngày đăng nhập sau cùng
                        return 1; // password và username đúng -> trả về 1
                    }
                    else
                    {
                        return -2; // password không đúng
                    }
                }
            }
        }

        public bool Delete(long id)  //Tạo một hàm xóa bản ghi thông qua id truyền vào
        {
            try
            {
                var user = db.Users.Find(id);  // Tìm đến bản ghi
                db.Users.Remove(user); // Di chuyển bản ghi
                db.SaveChanges();     //Lưu thay đổi tại CSDL
                return true;            // trả về biến true khi xóa thành công
            }catch(Exception)             // Nếu bắt gặp một ngoại lệ
            {
                return false;           // trả về false
            }
        }

        public bool ChangeStatus(long id)  // Truyền vào hàm một biến ID
        {
            var user = db.Users.Find(id);  // Tìm đến phần tử với id trùng khớp
            user.Status = !user.Status;  // Thay đổi trạng thái
            db.SaveChanges();   // Lưu lại thay đổi
            return !user.Status;  // trả về trạng thái đã thay đổi
        }
    }
}
