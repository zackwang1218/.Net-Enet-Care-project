namespace Ass2.Incognito.Enet.Entities
{
    using Ass2.Incognito.Enet.Utilities;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Package")]
    public partial class Package
    {
        [Key]
        public int Barcode { get; set; }

        public int MedicalID { get; set; }

        public int? DistributionCenterID { get; set; }

        public int? ReceivingCenterID { get; set; }

        public int UserId { get; set; }

        [Column(TypeName = "date")]
        public DateTime ExpiryDate { get; set; }

        public StockType StockStatus { get; set; }

        [Column(TypeName = "date")]
        public DateTime TransactionDate { get; set; }

        public virtual DistributionCenter DistributionCenter { get; set; }

        public virtual DistributionCenter ReceivingCenter { get; set; }

        [ForeignKey("MedicalID")]
        public virtual Medication Medication { get; set; }
    }
}
