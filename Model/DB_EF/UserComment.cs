namespace Model.DB_EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserComment")]
    public partial class UserComment
    {
        public long ID { get; set; }

        [StringLength(50)]
        public string UserName { get; set; }

        [StringLength(250)]
        public string AvatarImage { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string Website { get; set; }

        public bool? AcceptContact { get; set; }

        public bool? Status { get; set; }
    }
}
