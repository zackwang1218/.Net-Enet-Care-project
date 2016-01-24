namespace Ass2.Incognito.Enet.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EmployeeLogin")]
    public partial class EmployeeLogin
    {
        [Key]
        public int EmpLoginID { get; set; }

        public int UserId { get; set; }

        public int DistributionCenterID { get; set; }

        [Required]
        [StringLength(50)]
        public string Full_Name { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "Email address is not valid")]
        public string Email { get; set; }

        [ForeignKey("DistributionCenterID")]
        public virtual DistributionCenter DistributionCenter { get; set; }
    }
}
