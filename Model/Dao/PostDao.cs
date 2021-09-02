using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class PostDao
    {
        MyBlogDbContext db = null;

        public PostDao()  // Tạo Contructor Kịch bản ánh xạ lên cơ sở dữ liệu
        {
            db = new MyBlogDbContext();
        }

        //public IEnumerable<Post> ListNewPost(int page, int pageSize) //Duyệt phần tử dạng Categories từ danh sách, trả về số lượng phần tử theo kích thước cần
        //{
        //    IQueryable<Post> model = db.Posts.Where(x=>x.Status == true); // Tạo một biến với kiểu Query LinQ, biến này có thể thực hiện các truy vấn 
        //    return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize); // Hàm toPagedList của Plugin Pagedlist
        //}

        public IEnumerable<Post> ListAllPost(string searchString, int page, int pageSize)
        {
            IQueryable<Post> model = db.Posts;             // Tạo một danh sách kiểu Post với khả năng có thể dùng truy vấn 
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Title.Contains(searchString) || x.Published.Contains(searchString) || x.AuthorName.Contains(searchString)); // Tìm kiếm những bản ghi với thuộc tính gần giống với yêu cầu
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);         // trả về danh sách giảm dần 
        }

        public IEnumerable<Post> ListPost()
        {
            return db.Posts.Where(x => x.Status == true).OrderByDescending(x => x.CreatedDate).ToList();
        }

        public IEnumerable<Post> ListAllPostByStatus(string searchString, int page, int pageSize)
        {
            IQueryable<Post> model = db.Posts.Where(x=>x.Status==true);             // Tạo một danh sách kiểu Post với khả năng có thể dùng truy vấn 
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Title.Contains(searchString) || x.Published.Contains(searchString) || x.AuthorName.Contains(searchString)); // Tìm kiếm những bản ghi với thuộc tính gần giống với yêu cầu
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);         // trả về danh sách giảm dần 
        }

        public IEnumerable<Post> ListAllPostByCategory(long CatId, string searchString, int page, int pageSize)
        {
            IQueryable<Post> model = db.Posts.Where(x=>x.CategoryID == CatId && x.Status==true);             // Tạo một danh sách kiểu Post với khả năng có thể dùng truy vấn 
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where((x => x.Title.Contains(searchString) || x.Published.Contains(searchString) || x.AuthorName.Contains(searchString))); // Tìm kiếm những bản ghi với thuộc tính gần giống với yêu cầu
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);         // trả về danh sách giảm dần 
        }


        public IEnumerable<Post> GetHotPost(int size)
        {
            return db.Posts.Where(x => x.Status == true).OrderByDescending(x => x.CountRead).Take(size).ToList(); // Lấy ra bản ghi post với giảm dần lượng đọc và số lượng bản ghi size
        }

        public IEnumerable<Post> GetNewPost(int size)
        {
            return db.Posts.Where(x=>x.Status == true).OrderByDescending(x => x.DatePublished).Take(size).ToList(); // Lấy ra bản ghi với số lượng, giảm dần ngày hoàn thành
        }

        public IEnumerable<Post> GetHotPostByUserID(long UserID, int size)
        {
            return db.Posts.Where(x => x.Status == true && x.AuthorID == UserID).OrderByDescending(x => x.CountRead).Take(size).ToList(); // Lấy ra bản ghi post với giảm dần lượng đọc và số lượng bản ghi size
        }

        public IEnumerable<Post> GetRelatedPost(long IdPost, int size)
        {
            var post = db.Posts.Find(IdPost);
            return db.Posts.Where(x => x.Status == true && x.ID != IdPost && x.CategoryID == post.CategoryID).ToList(); // Lấy ra bản ghi với danh mục tương đương
        }

        public Post GetPostAbout(string namePost)
        {
            return db.Posts.SingleOrDefault(x => x.Title == namePost);
        }

        public Post GetById(long id)
        {
            return db.Posts.Find(id);      // Tìm bản ghi post với id truyền vào
        }

        public bool checkNullPost(Post post)
        {
            if (post.Title != null && post.Content != null)          // Kiểm tra các trường của bản ghi
            {
                return true;
            }
            else return false;
        }

        public long? countRead(long postId)   // Hàm đếm số lượng đọc, truyền vào bằng Id post
        {
            Post post = db.Posts.ToList().Find(x => x.ID == postId);
            post.CountRead += 1;
            if(post.CountRead > 10000000)
            {
                post.CountRead = 10000000; // Giới hạn số lượng lượt thích
            }
            db.SaveChanges();
            return post.CountRead;
        }

        public long? coutComment(bool statusCmt, long postId)
        {
            Post post = db.Posts.ToList().Find(x => x.ID == postId);
            if(statusCmt == true)
            {
                post.CountComment += 1;
            }else
            {
                post.CountComment -= 1;
            }

            if (post.CountComment < 0)
            {
                post.CountComment = 0; // Giới hạn số lượng lượt thích
            }
            db.SaveChanges();
            return post.CountComment;
        }

        public string GetFullNameAuthorById(long id)
        {
            var dao = db.Users.Find(id);
            return dao.FirstName +" "+ dao.MidName +" "+ dao.LastName;
        }

        public long Insert(Post entity) // tạo một hàm trả về khi thêm mới một bản ghi post
        {
      
            var result = db.Posts.SingleOrDefault(x => x.Title == entity.Title);
            if (result == null && checkNullPost(entity))
            {
                db.Posts.Add(entity);           // Thêm bản ghi vào bảng Category
                db.SaveChanges();              //Lưu trữ thay đổi
                return entity.ID;     // Trả về ID của bản ghi Post truyền vào
            }
            else
            {
                return 0;
            }
        }

        public bool Update(Post entity, string userName) // Truyền vào một đối tượng Post, tên user quản trị đang đăng nhập
        {
            try
            {
                var post = db.Posts.Find(entity.ID);  // Tìm đến đội tượng ID post truyền vào
                if(post != null)
                {
                    
                    post.CategoryID = entity.CategoryID;
                    post.Title = entity.Title;                // Thay đổi dữ liệu các trường
                    post.Status = entity.Status;
                    post.Published = entity.Published;
                    post.PostImage = entity.PostImage;
                    post.parentID = entity.parentID;
                    post.HistoryModified += "\n Edit by " + userName + " in " + DateTime.Now.ToString();
                    post.ModifiedBy = userName;
                    post.Link = entity.Link;
                    post.MetaTitle = entity.MetaTitle;
                    if (entity.Status == true)
                    {
                        post.DatePublished = DateTime.Now;
                    }
                    post.Content = entity.Content;
                    post.MoreImages = entity.MoreImages;
                    post.ModifiedDate = DateTime.Now;
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
                var post = db.Posts.Find(id);  // Tìm đến bản ghi
                db.Posts.Remove(post); // Di chuyển bản ghi
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
            var post = db.Posts.Find(id); // tìm đến Category với id truyền vào
            post.Status = !post.Status;   // Thay đổi trạng thái của Category
            db.SaveChanges();           // Lưu lại thay đổi
            return !post.Status;          // trả về bool thay đổi 
        }



    }
}
