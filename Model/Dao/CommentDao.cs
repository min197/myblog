using Model.EF;
using Model.ModelView;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class CommentDao
    {
        MyBlogDbContext db = null;

        public CommentDao()  // Tạo Contructor Kịch bản ánh xạ lên cơ sở dữ liệu
        {
            db = new MyBlogDbContext();
        }

        public PostComment GetById(long id) // Lấy postcomment theo id truyền vào
        {
            return db.PostComments.Find(id);
        }

        public List<PostComment> GetListCommentByPostId(long postId) //Lấy ra danh sách comment dựa theo id post
        {
            return db.PostComments.Where(x => x.Status == true && x.PostID == postId).OrderBy(x => x.CreatedDate).ToList();
        }

        public List<PostComment> GetListCommentByParentId(long parentId)  // Lấy ra danh sách comment theo biến id cha
        {
            return db.PostComments.Where(x => x.Status == true && x.ParentID == parentId).OrderBy(x => x.CreatedDate).ToList();
        }

        public IEnumerable<PostComment> ListAllCommentBySearchAndSize(string searchString, int page, int pageSize)
        {
            IQueryable<PostComment> model = db.PostComments;             // Tạo một danh sách kiểu PostComment với khả năng có thể dùng truy vấn 
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Content.Contains(searchString) || x.CreatedBy.Contains(searchString) || x.PostID.ToString().Contains(searchString) || x.ParentID.ToString().Contains(searchString)); // Tìm kiếm những bản ghi với thuộc tính gần giống với yêu cầu
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);         // trả về danh sách giảm dần 
        }


        public int CountCommentByPostId(long postId)
        {
            var cmt = db.PostComments.Where(x => x.Status == true && x.PostID == postId).ToList();
            return cmt.Count();
        }

        //public List<CommentAndUser> GetCommentAndUser()
        //{
        //    var model = from a in db.PostComments.ToList()
        //                join b in db.UserComments.ToList() on a.UserID equals b.ID
        //                where a.Status == true
        //                select new CommentAndUser
        //                {
        //                    Content = a.Content,
        //                    UserName = b.UserName,
                            
        //                };
        //    return model.OrderBy(x => x.comment.ID).ToList();
        //}

        public List<CommentAndUser> GetCommentAndUserByPostId(long postId)  // Lấy ra comment và user comment qua postId
        {
            var model = from a in db.PostComments.ToList()
                        join b in db.UserComments.ToList() on a.UserID equals b.ID
                        where a.Status == true && a.PostID == postId 
                        select new CommentAndUser
                        {
                            CommentID = a.ID,
                            parentID = a.ParentID,
                            Avatar = b.AvatarImage,
                            Content = a.Content,
                            UserName = b.UserName,
                            Email = b.Email,
                            Website = b.Website,
                            CreatedDate = a.CreatedDate
                        };
            return model.OrderBy(x => x.CreatedDate).ToList();
        }

        public long InsertCommentAndUser(PostComment comment, UserComment user) // tạo một hàm trả về khi thêm mới một bản ghi comment
        {
            var userGet = db.UserComments.SingleOrDefault(x => x.Email == user.Email);

            if (userGet == null) // User chưa tồn tại trong db
            {
                // Thêm user mới vào usercomment
                user.CreatedDate = DateTime.Now;
                user.Status = true;
                db.UserComments.Add(user);
                db.SaveChanges();

                if (comment.Content != null)
                {
                    comment.CreatedBy = user.UserName;
                    comment.CreatedDate = comment.CreatedDate;
                    comment.Status = false;  
                    comment.UserID = user.ID;

                    db.PostComments.Add(comment);           // Thêm bản ghi vào bảng Comment
                    db.SaveChanges();

                    return comment.ID;
                }
                else
                {
                    return -1;   // User hợp lệ, comment rỗng
                }

            }
            else   // User đã tồn tại trong db
            {
                if (user.UserName == userGet.UserName && (userGet.Website != null && user.Website != null))
                {
                    // Cập nhật thông tin cho user comment
                    userGet.Website = user.Website;
                    userGet.AcceptContact = user.AcceptContact;
                    db.SaveChanges();

                    // Thêm comment nếu user đã tồn tại
                    if (comment.Content != null)
                    {
                        comment.CreatedBy = userGet.UserName;
                        comment.Status = false;
                        comment.UserID = userGet.ID;

                        db.PostComments.Add(comment);           // Thêm bản ghi vào bảng Comment
                        db.SaveChanges();

                        return comment.ID;
                    }
                    else
                    {
                        return -1;   // User hợp lệ, comment rỗng
                    }

                }
                else // Cập nhật thông tin cho user
                {
                    userGet.UserName = user.UserName;
                    if(user.Website != null)
                    {
                        userGet.Website = user.Website;
                    }
                    db.SaveChanges();  // Lưu thông tin cập nhật
                    // Thêm comment và cập nhật user
                    if (comment.Content != null)
                    {
                        comment.CreatedBy = userGet.UserName;
                        comment.Status = false;
                        comment.UserID = userGet.ID;

                        db.PostComments.Add(comment);           // Thêm bản ghi vào bảng Comment
                        db.SaveChanges();

                        return comment.ID;
                    }
                    else
                    {
                        return -1;   // User hợp lệ, comment rỗng
                    }
                }
            }
        }

        /*
         * public long InsertCommentAndUser(PostComment comment, UserComment user) // tạo một hàm trả về khi thêm mới một bản ghi comment
        {
            var userGet = db.UserComments.SingleOrDefault(x => x.Email == user.Email);
            if(userGet == null)
            {
                // Thêm user mới vào usercomment
                db.UserComments.Add(user);
                db.SaveChanges();


                if (comment.Content != null)
                {
                    comment.CreatedBy = user.UserName;
                    comment.CreatedDate = comment.CreatedDate;
                    comment.Status = true;
                    comment.UserID = user.ID;

                    db.PostComments.Add(comment);           // Thêm bản ghi vào bảng Comment
                    db.SaveChanges();

                    return comment.ID;
                }
                else
                {
                    return -2;   // User hợp lệ, comment rỗng
                }

            }
            else
            {
                if (user.UserName == userGet.UserName && (userGet.Website != null && user.Website != null))
                { 
                    // Cập nhật thông tin cho user comment
                    userGet.Website = user.Website;
                    userGet.AcceptContact = user.AcceptContact;
                    db.SaveChanges();

                    // Thêm comment nếu user đã tồn tại
                    if (comment.Content != null)
                    {
                        comment.CreatedBy = userGet.UserName;
                        comment.CreatedDate = DateTime.Now;
                        comment.Status = true;
                        comment.UserID = userGet.ID;

                        db.PostComments.Add(comment);           // Thêm bản ghi vào bảng Comment
                        db.SaveChanges();

                        return comment.ID;
                    }
                    else
                    {
                        return -2;   // User hợp lệ, comment rỗng
                    }

                }
                else
                {
                    return -1; // Trùng email nhưng khác tên
                }
            }

        }*/




        public bool Update(PostComment entity, string userName) // Truyền vào một đối tượng PostComment, tên user quản trị đang đăng nhập
        {
            try
            {
                var cmt = db.PostComments.Find(entity.ID);  // Tìm đến đội tượng ID postcomment truyền vào
                if (entity.Content != null)
                {
                    cmt.CommentImage = entity.CommentImage;
                    cmt.Content = entity.Content;
                    cmt.ModifiedBy = userName;
                    cmt.Status = entity.Status;
                    cmt.Content = entity.Content;
                    cmt.ModifiedDate = DateTime.Now;
                    if(cmt.Status == true)
                    {
                        cmt.DatePublished = DateTime.Now;
                    }
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
                var cmt = db.PostComments.Find(id);  // Tìm đến bản ghi
                db.PostComments.Remove(cmt); // Di chuyển bản ghi
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
            var cmt = db.PostComments.Find(id); // tìm đến Comment với id truyền vào
            cmt.Status = !cmt.Status;   // Thay đổi trạng thái của Comment
            if (cmt.Status == true)
            {
                cmt.DatePublished = DateTime.Now;
            }

            db.SaveChanges();             // Lưu lại thay đổi

           
            return !cmt.Status;          // trả về bool thay đổi 
        }

    }
}
