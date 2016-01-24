namespace Ass2.Incognito.Enet.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Medication")]
    public partial class Medication
    {
        [Key]
        public int MedicalID { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        public int? Shell_Life { get; set; }

        public double? Value { get; set; }

        [StringLength(100)]
        public string TempSensitivity { get; set; }

        public int? Quantity { get; set; }

        [InverseProperty("Medication")]
        public virtual ICollection<Package> Packages { get; set; }
    }
}
