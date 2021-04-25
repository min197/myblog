namespace Model.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MyBlogDbContext : DbContext
    {
        public MyBlogDbContext()
            : base("name=MyBlog")
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<PostComment> PostComments { get; set; }
        public virtual DbSet<PostMeta> PostMetas { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<Footer> Footers { get; set; }
        public virtual DbSet<Slide> Slides { get; set; }
        public virtual DbSet<PostTag> PostTags { get; set; }
        public virtual DbSet<UserComment> UserComments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<PostTag>()
               .Property(e => e.PostTitle)
               .IsUnicode(true);

            modelBuilder.Entity<PostTag>()
               .Property(e => e.TagTitle)
               .IsUnicode(true);

            modelBuilder.Entity<Post>()
                .Property(e => e.AuthorName)
                .IsUnicode(true);


            modelBuilder.Entity<Post>()
               .Property(e => e.Title)
               .IsUnicode(true);

            modelBuilder.Entity<Post>()
               .Property(e => e.MetaTitle)
               .IsUnicode(false);


            modelBuilder.Entity<Post>()
               .Property(e => e.PostImage)
               .IsUnicode(false);

            modelBuilder.Entity<Post>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Post>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Post>()
                .Property(e => e.Content)
                .IsUnicode(true);

            modelBuilder.Entity<Post>()
               .Property(e => e.Link)
               .IsUnicode(false);

            modelBuilder.Entity<PostComment>()
                .Property(e => e.CommentImage)
                .IsUnicode(true);

            modelBuilder.Entity<PostComment>()
                .Property(e => e.Content)
                .IsUnicode(true);

            modelBuilder.Entity<PostComment>()
                .Property(e => e.CreatedBy)
                .IsUnicode(true);

            modelBuilder.Entity<PostComment>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<PostMeta>()
                .Property(e => e.Key)
                .IsUnicode(false);

            modelBuilder.Entity<PostMeta>()
                .Property(e => e.Content)
                .IsUnicode(false);

            modelBuilder.Entity<Tag>()
                .Property(e => e.MetaTitle)
                .IsUnicode(false);

            modelBuilder.Entity<Tag>()
                .Property(e => e.Title)
                .IsUnicode(true);

            modelBuilder.Entity<Tag>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Tag>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Tag>()
                .Property(e => e.Content)
                .IsUnicode(true);


            modelBuilder.Entity<User>()
                .Property(e => e.Avatar)
                .IsUnicode(true);

            modelBuilder.Entity<User>()
                .Property(e => e.FirstName)
                .IsUnicode(true);

            modelBuilder.Entity<User>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.MidName)
                .IsUnicode(true);

            modelBuilder.Entity<User>()
                .Property(e => e.LastName)
                .IsUnicode(true);

            modelBuilder.Entity<User>()
                .Property(e => e.Mobile)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.PasswordHash)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.About)
                .IsUnicode(true);

            modelBuilder.Entity<User>()
                .Property(e => e.Profile)
                .IsUnicode(true);

            modelBuilder.Entity<User>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.LinkToFacebook)
                .IsUnicode(false);


            modelBuilder.Entity<User>()
                .Property(e => e.LinkToInstagram)
                .IsUnicode(false);


            modelBuilder.Entity<User>()
                .Property(e => e.LinkToTwitter)
                .IsUnicode(false);


            modelBuilder.Entity<User>()
                .Property(e => e.LinkToYoutube)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.JobTitle)
                .IsUnicode(true);

            modelBuilder.Entity<User>()
                .Property(e => e.Adress)
                .IsUnicode(true);

            modelBuilder.Entity<Menu>()
               .Property(e => e.Target)
               .IsUnicode(false);

            modelBuilder.Entity<Menu>()
              .Property(e => e.Text)
              .IsUnicode(true);

            modelBuilder.Entity<Footer>()
             .Property(e => e.ID)
             .IsUnicode(false);

            modelBuilder.Entity<Footer>()
           .Property(e => e.Content)
           .IsUnicode(true);

            modelBuilder.Entity<Slide>()
           .Property(e => e.Image)
           .IsUnicode(true);


            modelBuilder.Entity<Slide>()
           .Property(e => e.CreatedBy)
           .IsUnicode(false);

            modelBuilder.Entity<Slide>()
           .Property(e => e.Link)
           .IsUnicode(false);

            modelBuilder.Entity<Slide>()
           .Property(e => e.ModifiedBy)
           .IsUnicode(false);

            modelBuilder.Entity<Slide>()
          .Property(e => e.Title)
          .IsUnicode(true);


            modelBuilder.Entity<UserComment>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<UserComment>()
                .Property(e => e.Website)
                .IsUnicode(false);

            modelBuilder.Entity<UserComment>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<UserComment>()
               .Property(e => e.AvatarImage)
               .IsUnicode(true);
        }

        
    }
}
