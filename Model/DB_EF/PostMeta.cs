namespace Model.DB_EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PostMeta")]
    public partial class PostMeta
    {
        public long ID { get; set; }

        public long? PostID { get; set; }

        [StringLength(50)]
        public string Key { get; set; }

        [Column(TypeName = "text")]
        public string Content { get; set; }
    }
}
