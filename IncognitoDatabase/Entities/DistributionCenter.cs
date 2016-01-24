namespace Ass2.Incognito.Enet.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DistributionCenter")]
    public partial class DistributionCenter
    {
        [Key]
        public int DistributionCenterID { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Address { get; set; }

        [StringLength(50)]
        public string PhoneNumber { get; set; }

        //[InverseProperty("DistributionCenter")]
        public virtual ICollection<Package> Packages { get; set; }

       // [InverseProperty("DistributionCenter")]
        public virtual ICollection<Package> ReceivingPackages { get; set; }

        [InverseProperty("DistributionCenter")]
        public virtual ICollection<EmployeeLogin> EmployeeLogins { get; set; }
    }
}
