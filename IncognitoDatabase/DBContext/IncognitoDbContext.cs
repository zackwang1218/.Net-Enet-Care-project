namespace Ass2.Incognito.Enet.DBContext
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Ass2.Incognito.Enet.Entities;

    public partial class IncognitoDbContext : DbContext
    {
        public IncognitoDbContext()
            : base("name=IncognitoDbContext")
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Package>()
                        .HasOptional(m => m.DistributionCenter)
                        .WithMany(t => t.Packages)
                        .HasForeignKey(m => m.DistributionCenterID)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<Package>()
                        .HasOptional(m => m.ReceivingCenter)
                        .WithMany(t => t.ReceivingPackages)
                        .HasForeignKey(m => m.ReceivingCenterID)
                        .WillCascadeOnDelete(false);
        }
        public virtual DbSet<DistributionCenter> DistributionCenters { get; set; }
        public virtual DbSet<EmployeeLogin> EmployeeLogins { get; set; }
        public virtual DbSet<Medication> Medications { get; set; }
        public virtual DbSet<Package> Packages { get; set; }

    }
}
