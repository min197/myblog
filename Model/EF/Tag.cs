namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Tag")]
    public partial class Tag
    {
        public long ID { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(50)]
        public string MetaTitle { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        [Column(TypeName = "ntext")]
        public string Content { get; set; }

        public bool Status { get; set; }
    }
}
