using Ass2.Incognito.Enet.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ass2.Incognito.Enet.ViewModels
{
    public class DistributionCenterStockView
    {
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public double Total { get; set; }
    }
    public class DistributionCenterStockGUIView
    {
        public int CenterID { get; set; }
        public ICollection<DistributionCenterStockView> Items { get; set; }
        public int TotalQuantity { get; set; }
        public double TotalValue { get; set; }
    }

    //Used for doctor activity, Value in Transit
    public class ValueInTransitView
    {
        public int DoctorID { get; set; }
        public ICollection<Package> Items { get; set; }
        public int TotalQuantity { get; set; }
        public double TotalValue { get; set; }
    }
    public class DistributionCenterLossesView
    {
        public DistributionCenter Center { get; set; }
        public double LossPackageValue { get; set; }
        public double DiscardedPackageValue { get; set; }
        public double DistributedPackageValue { get; set; }
        public double LossRatio { get; set; }
        public double LossPerDiscarded { get; set; }
    }
}
