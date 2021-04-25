namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Post")]
    public partial class Post
    {
        public long ID { get; set; }

        public long? AuthorID { get; set; }

        [StringLength(250)]
        public string AuthorName { get; set; }

        [Column(TypeName = "ntext")]
        public string Published { get; set; }

        public long? parentID { get; set; }

        public long? CategoryID { get; set; }

        [StringLength(250)]
        public string Title { get; set; }

        [StringLength(250)]
        public string MetaTitle { get; set; }

        [StringLength(250)]
        public string Link { get; set; }

        public DateTime? DatePublished { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        public long? CountRead { get; set; }

        [StringLength(250)]
        public string PostImage { get; set; }


        [Column(TypeName = "xml")]
        public string MoreImages { get; set; }

        [Column(TypeName = "ntext")]
        public string Content { get; set; }

        [Column(TypeName = "text")]
        public string HistoryModified { get; set; }

        public bool Status { get; set; }
    }
}
