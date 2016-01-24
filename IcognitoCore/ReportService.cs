using Ass2.Incognito.Enet.Utilities;
using Ass2.Incognito.Enet.DAO;
using Ass2.Incognito.Enet.Entities;
using Ass2.Incognito.Enet.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Data.Entity;

namespace Ass2.Incognito.Enet.Services
{
    public class ReportService
    {
        private ReportDAO reportDao = null;
        private MedicationDAO medicationDao = null;
        private DistributionCenterDAO distributionCenterDao = null;
        private List<DistributionCenterLossesView> centerLossView = new List<DistributionCenterLossesView>();

        public ReportService()
        {
            reportDao = new ReportDAO();
            medicationDao = new MedicationDAO();
            distributionCenterDao = new DistributionCenterDAO();
        }
        public DistributionCenterStockGUIView GenerateDistributionCenterStock(int centerID)
        {
            ICollection<DistributionCenterStockView> stockReport = reportDao.DistributionCenterStock(centerID);
            DistributionCenterStockGUIView GUIView = new DistributionCenterStockGUIView();

            int totalItems = 0;
            double totalValue = 0;
            foreach (DistributionCenterStockView item in stockReport)
            {
                Medication med = medicationDao.GetMedication(item.ItemID);
                item.ItemName = med.Description;
                item.Total = (item.Quantity * med.Value.Value);

                totalItems += item.Quantity;
                totalValue += item.Total;
            }
            GUIView.Items = stockReport;
            GUIView.TotalQuantity = totalItems;
            GUIView.TotalValue = totalValue;

            return GUIView;
        }
        public DistributionCenterStockGUIView GenerateGlobalStock()
        {
            ICollection<DistributionCenterStockView> stockReport = reportDao.GlobaStock();
            DistributionCenterStockGUIView GUIView = new DistributionCenterStockGUIView();

            int totalItems = 0;
            double totalValue = 0;

            foreach (DistributionCenterStockView item in stockReport)
            {
                Medication med = medicationDao.GetMedication(item.ItemID);
                item.ItemName = med.Description;
                item.Total = (item.Quantity * med.Value.Value);

                totalItems += item.Quantity;
                totalValue += item.Total;
            }
            GUIView.Items = stockReport;
            GUIView.TotalQuantity = totalItems;
            GUIView.TotalValue = totalValue;

            return GUIView;
        }
        public ValueInTransitView TransitRecord()
        {
            ICollection<Package> transitRecord = reportDao.PackageTypesList(StockType.InTransit); //receive all packages in transit
            ValueInTransitView GUIView = new ValueInTransitView();
            int totalItems = 0;
            double totalValue = 0;
            foreach (Package pack in transitRecord)
            {
                totalItems += pack.Medication.Quantity.Value;
                totalValue += pack.Medication.Value.Value * pack.Medication.Quantity.Value;
            }
            GUIView.Items = transitRecord;
            GUIView.TotalQuantity = totalItems;
            GUIView.TotalValue = totalValue;

            return GUIView;
        }
        public ValueInTransitView DoctorActivity(int docID)
        {
            ICollection<Package> doctorRecord = reportDao.PackageByUser(docID, StockType.Distributed);
            ValueInTransitView GUIView = new ValueInTransitView();
            int totalItems = 0;
            double totalValue = 0;
            foreach (Package pack in doctorRecord)
            {
                totalItems += pack.Medication.Quantity.Value;
                totalValue += pack.Medication.Value.Value * pack.Medication.Quantity.Value;
            }
            GUIView.Items = doctorRecord;
            GUIView.TotalQuantity = totalItems;
            GUIView.TotalValue = totalValue;

            return GUIView;
        }
        public ICollection<DistributionCenterLossesView> DisributionCenterLosses()
        {
            //Get all distributionCener
            List<DistributionCenter> distriutionCenters = distributionCenterDao.GetAllDistributionCenter();
            //Add each distribution centers in distributionCenter Loss view item
            foreach (DistributionCenter cent in distriutionCenters)
            {
                DistributionCenterLossesView distLossView = new DistributionCenterLossesView();
                distLossView.Center = cent;

                //Calculate Total Loss
                ICollection<Package> lostPackages = reportDao.PackageByCenter(cent.DistributionCenterID, StockType.Lost);
                foreach (Package pack in lostPackages)
                {
                    distLossView.LossPackageValue += pack.Medication.Quantity.Value * pack.Medication.Value.Value;
                }
                //Calculate Total Discarded
                ICollection<Package> discardedPackages = reportDao.PackageByCenter(cent.DistributionCenterID, StockType.Discarded);
                foreach (Package pack in discardedPackages)
                {
                    distLossView.DiscardedPackageValue += pack.Medication.Quantity.Value * pack.Medication.Value.Value;
                }
                //Calculate Total Distributed
                ICollection<Package> distributedPackages = reportDao.PackageByCenter(cent.DistributionCenterID, StockType.Distributed);
                foreach (Package pack in distributedPackages)
                {
                    distLossView.DistributedPackageValue += pack.Medication.Quantity.Value * pack.Medication.Value.Value;
                }

                //Calcuate Loss Ratio (#Lost + #Discarded) / (#Distributed + #Lost + #Discarded)
                double num = (distLossView.LossPackageValue + distLossView.DiscardedPackageValue);
                double den = distLossView.DistributedPackageValue + distLossView.LossPackageValue + distLossView.DiscardedPackageValue;
                if(den != 0 )
                {
                    distLossView.LossRatio = num/den * 100;
                }

                //total value of lost/discarded
                if(distLossView.DiscardedPackageValue != 0)
                {
                    distLossView.LossPerDiscarded = distLossView.LossPackageValue / distLossView.DiscardedPackageValue;
                }
                centerLossView.Add(distLossView);
            }
            return centerLossView;
        }
        public IEnumerable<SelectListItem> GetDoctorList()
        {
            return reportDao.GetDoctorList();
        }
    }
}