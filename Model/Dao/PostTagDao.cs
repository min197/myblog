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
    public class PostTagDao
    {
        MyBlogDbContext db = null;
        public PostTagDao()  // Tạo Contructor Kịch bản ánh xạ lên cơ sở dữ liệu
        {
            db = new MyBlogDbContext();
        }

        /* public IPagedList<PostTagViewModel> ListAllPostTag(string searchString, int page, int pageSize)
        {
            
                   var model = from a in db.PostTags
                        join b in db.Posts on a.PostID equals b.ID
                        join c in db.Tags on a.TagID equals c.ID
                        select new PostTagViewModel()
                        {
                            ID = a.ID,
                            PostID = b.ID,
                            TagID = c.ID,
                            TitlePost = b.Title,
                            TitleTag = c.Title,
                            Status = a.Status
                        };
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.TitlePost.Contains(searchString) || x.TitleTag.Contains(searchString)); // Tìm kiếm những bản ghi với thuộc tính gần giống với yêu cầu
            }
            return model.OrderBy(x=>x.ID).ToPagedList(page, pageSize);
        }*/


        public IEnumerable<PostTag> ListAllPostTag(String searchString, int page, int pageSize)
        {

            IQueryable<PostTag> model = db.PostTags;   // Tạo một truy vấn hỗ trợ tải chậm

            if (!string.IsNullOrEmpty(searchString))   // kiểm tra chuỗi tìm kiếm nhập vào
            {
                model = model.Where(x => x.PostTitle.Contains(searchString) || x.TagTitle.Contains(searchString) || x.PostID.Value.ToString() == searchString || x.TagID.Value.ToString() == searchString); // Tìm kiếm những bản ghi với thuộc tính gần giống với yêu cầu
            }
            return model.OrderBy(x => x.ID).ToPagedList(page, pageSize);   // Trả về trang PagedList thứ tự tăng dần ID
        }

        public List<PostTagViewModel> ListPostOnTag(long idTag, string searchString, ref int totalPost, int pageIndex, int pageSize)
        {
            var model = from a in db.PostTags.ToList()  // từ bản ghi trong bảng PostTags 
                        join b in db.Tags.ToList() on a.TagID equals b.ID  // kết hợp với bảng Tags với tương đương ID 2 bảng
                        join c in db.Posts.ToList() on a.PostID equals c.ID  // tương tự bên trên
                        where a.TagID == idTag   // lấy ra bản ghi với ID Tag truyền vào
                        select new PostTagViewModel
                        {
                            Tag = b,    // Gán thuộc tính cho các trường
                            Post = c 
                        };

            if (!string.IsNullOrEmpty(searchString))   // Kiểm tra chuỗi tìm kiếm
            {
                model = model.Where(x => x.Post.Title.Contains(searchString) || x.Post.AuthorName.Contains(searchString) || x.Post.Published.Contains(searchString) || x.Post.Content.Contains(searchString));
            }
           
            model.OrderBy(x => x.Post.ID).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            totalPost = model.Count(); // Đếm tổng số bản ghi đã lấy ra 
            return model.ToList();
        }

        public int CountPostOnTag(long TagId)
        {
            var model = db.PostTags.Where(x => x.TagID == TagId).ToList();   // Lấy ra danh sách các quan hệ với Tag ID truyền vào
            return model.Count(); // Đếm số bản ghi
        }


        public List<TagAndCountPost> ListTagAndCountPost()
        {
            var query = from a in db.Tags.ToList()  // Truy vấn đến bảng Tag 
                         join b in db.PostTags.ToList() on a.ID equals b.TagID   // Truy vấn đến bảng quan hệ post tag
                         where a.ID != 0             // Điều kiện của truy vấn
                         select new TagAndCountPost 
                         {

                             Tag = a,                        // gán giá trị cho biến
                             count = (from c in db.PostTags.ToList()         // Đếm số lần xuất hiện trong bảng của biến
                                      where c.TagID.Equals(a.ID)
                                      select a).Count()
                         };
            
            return query.GroupBy(x=>x.Tag.ID).Select(grp => grp.First()).ToList();    // Sắp xếp theo nhóm và chọn nhóm đầu tiên nếu trùng nhau
        }


        public List<PostTagViewModel> ListTagOnPost(long postId)
        {
            var model = from a in db.PostTags.ToList()    // Thực hiện truy vấn bắt đầu từ bảng PostTags
                        join b in db.Tags.ToList() on a.TagID equals b.ID   
                        join c in db.Posts.ToList() on a.PostID equals c.ID
                        where a.PostID == postId
                        select new PostTagViewModel
                        {
                            Post = c,
                            Tag = b
                        };
            model.OrderBy(x => x.Tag.ID);
            return model.ToList();
        }

        public long Insert(PostTag entity)
        {
            var result = db.PostTags.SingleOrDefault(x => x.PostID == entity.PostID && x.TagID == entity.TagID);  // Kiểm tra dữ liệu nhập vào
            if (result == null)
            {
                var post = new PostDao();
                var tag = new TagDao();
                entity.PostTitle = post.GetById(entity.PostID.Value).Title;   // Gán thuộc tính 
                entity.TagTitle = tag.GetById(entity.TagID.Value).Title;
                db.PostTags.Add(entity);           // Thêm bản ghi vào bảng PostTag
                db.SaveChanges();              //Lưu trữ thay đổi
                return entity.ID;     // Trả về ID của bản ghi PostTag truyền vào
            }
            else
            {
                return 0;
            }
        }

        public bool Update(PostTag entity)// Truyền vào một đối tượng PostTag - quan hệ giữa hai bảng
        { 
            try
            {
                var pt = db.PostTags.Find(entity.ID);  // Tìm đến đội tượng ID posttag truyền vào
                pt.PostID = entity.PostID;
                pt.TagID = entity.TagID;
                pt.PostTitle = entity.PostTitle;
                pt.TagTitle = entity.TagTitle;
                pt.Status = entity.Status;
                db.SaveChanges();    // Lưu các thay đổi sau  khi ghi đầy đủ dữ liệu
                return true;            // trả về đúng khi thành công
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public PostTag GetById(long id)
        {
            return db.PostTags.Find(id);
        }

        public bool Delete(long id)  //Tạo một hàm xóa bản ghi thông qua id truyền vào
        {
            try
            {
                var posttag = db.PostTags.Find(id);  // Tìm đến bản ghi
                db.PostTags.Remove(posttag); // Di chuyển bản ghi
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
            var pt = db.PostTags.Find(id); // tìm đến PostTag với id truyền vào
            pt.Status = !pt.Status;   // Thay đổi trạng thái của quan hệ
            db.SaveChanges();           // Lưu lại thay đổi
            return !pt.Status;          // trả về bool thay đổi 
        }

    }
}
