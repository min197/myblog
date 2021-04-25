namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Category")]
    public partial class Category
    {
        
        public long ID { get; set; }

        public long? ParentID { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(50)]
        public string MetaTitle { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public bool Status { get; set; }

        public bool ShowOnHome { get; set; }
    }
}
