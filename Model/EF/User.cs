namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        public long ID { get; set; }

        public int? TypeUserId { get; set; }

        [StringLength(250)]
        public string Avatar { get; set; }

        [StringLength(50)]
        public string UserName { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string MidName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(50)]
        public string Mobile { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(32)]
        public string PasswordHash { get; set; }

        [StringLength(250)]
        public string Adress { get; set; }

        [StringLength(250)]
        public string LinkToFacebook { get; set; }

        [StringLength(250)]
        public string LinkToInstagram { get; set; }

        [StringLength(250)]
        public string LinkToYoutube { get; set; }

        [StringLength(250)]
        public string LinkToTwitter{ get; set; }

        [StringLength(250)]
        public string JobTitle { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? LastLogin { get; set; }

        [StringLength(250)]
        public string About { get; set; }

        [Column(TypeName = "ntext")]
        public string Profile { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        public bool Status { get; set; }
    }
}
