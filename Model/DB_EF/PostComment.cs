namespace Model.DB_EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PostComment")]
    public partial class PostComment
    {
        public long ID { get; set; }

        public long UserID { get; set; }

        public long PostID { get; set; }

        public long? ParentID { get; set; }

        [StringLength(250)]
        public string CommentImage { get; set; }

        public DateTime? DatePublished { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        [Column(TypeName = "ntext")]
        public string Content { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public bool? Status { get; set; }
    }
}
