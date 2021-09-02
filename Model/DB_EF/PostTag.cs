namespace Model.DB_EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PostTag")]
    public partial class PostTag
    {
        public long ID { get; set; }

        public long? PostID { get; set; }

        public long? TagID { get; set; }

        [StringLength(250)]
        public string PostTitle { get; set; }

        [StringLength(50)]
        public string TagTitle { get; set; }

        public bool? Status { get; set; }
    }
}
