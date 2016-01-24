using System;
using System.Text;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ass2.Incognito.Enet.DAO;
using Ass2.Incognito.Enet.Entities;
using Ass2.Incognito.Enet.Utilities;
using Ass2.Incognito.Enet.DBContext;
using Ass2.Incognito.Enet.Services;
using System.Linq;
namespace Incognito_Ass2.Tests
{
    /// <summary>
    /// Summary description for ReportTest
    /// </summary>
    [TestClass]
    public class ReportTest
    {
        private ReportDAO reportDao;
        private ReportService reportService;
        public ReportTest()
        {
            reportDao = new ReportDAO();
            reportService = new ReportService();
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void GlobalStockServiceTest()
        {
            var globalValueResult = reportService.GenerateGlobalStock().TotalValue;
            double totalValueTest = 0;
            var centerList = from c in reportDao.DistributionCenters
                             select c.DistributionCenterID;
            foreach (var centerID in centerList)
            {
                var centerStockValue = reportService.GenerateDistributionCenterStock(centerID).TotalValue;
                totalValueTest += centerStockValue;
            }
            Assert.AreEqual(globalValueResult, totalValueTest);
        }
        [TestMethod]
        public void CenterStockServiceTest()
        {
            int centerID = 1;
            var stockView = reportService.GenerateDistributionCenterStock(centerID);
            var stockReport = reportDao.DistributionCenterStock(centerID);
            int numOfMediTest = stockView.Items.Count();
            int numOfMediResult = stockReport.Count();
            Assert.AreEqual(numOfMediTest, numOfMediResult);
        }
        [TestMethod]
        public void TransitRecordTest()
        {
            var RecordReport = reportService.TransitRecord();
            var packageList = reportDao.PackageTypesList(StockType.InTransit);
            int totalPackages = 0;
            double totalValue = 0;
            foreach (Package pack in packageList)
            {
                totalPackages += pack.Medication.Quantity.Value;
                totalValue += pack.Medication.Value.Value * pack.Medication.Quantity.Value;
            }
            Assert.AreEqual(RecordReport.TotalQuantity, totalPackages);
            Assert.AreEqual(RecordReport.TotalValue, totalValue);
        }
        [TestMethod]
        public void DoctorActivityTest()
        {
            int DoctorID = 1;
            var doctor1Result = reportService.DoctorActivity(DoctorID);
            var doctorRecord = reportDao.PackageByUser(DoctorID, StockType.Distributed);
            int totalItems = 0;
            double totalValue = 0;
            foreach (Package pack in doctorRecord)
            {
                totalItems += pack.Medication.Quantity.Value;
                totalValue += pack.Medication.Value.Value * pack.Medication.Quantity.Value;
            }
            Assert.AreEqual(doctor1Result.TotalQuantity, totalItems);
            Assert.AreEqual(doctor1Result.TotalValue, totalValue);
        }
        [TestMethod]
        public void DistributionCenterLossTest()
        {
            var losseslist = reportService.DisributionCenterLosses();
            Assert.IsNotNull(losseslist);
            int numOfReportCenter = losseslist.Count();
            var centers = from c in reportDao.DistributionCenters
                      select c;
            int sum = centers.Count();
            Assert.AreEqual(numOfReportCenter, sum);
        }
        [TestMethod]
        public void CenterStockTest()
        {
            int centerID = 1;
            int mediID = 1;
            var stockList = reportDao.DistributionCenterStock(centerID);
            var reportView = from m in stockList
                               where m.ItemID == mediID
                               select m;
            int mediQuantity = reportView.First().Quantity;
            var selectedMediList = from m in reportDao.Packages
                              where (m.MedicalID == mediID && m.DistributionCenterID==centerID)
                              select m;
            var totalValue = selectedMediList.Count() * 12;
            Assert.AreEqual(mediQuantity, totalValue);
        }
        [TestMethod]
        public void GlobalStockTest()
        {
            int mediID = 2;
            var stockList = reportDao.GlobaStock();
            var reportView = from m in stockList
                             where m.ItemID == mediID
                             select m;
            int mediQuantity = reportView.First().Quantity;
            var selectedMediList = from m in reportDao.Packages
                                   where m.MedicalID == mediID
                                   select m;
            var totalValue = selectedMediList.Count() * 100;
            Assert.AreEqual(mediQuantity, totalValue);
        }
        [TestMethod]
        public void PackageTypeListTest()
        {
            var InStockList = reportDao.PackageTypesList(StockType.InStock);
            var list = from p in reportDao.Packages
                      where p.StockStatus == StockType.InStock
                      select p;
            int sum = list.Count();
            int result = InStockList.Count();
            Assert.AreEqual(sum, result);
        }
        [TestMethod]
        public void PackageByUserTest()
        {
            int userID = 1;
            var packageList = reportDao.PackageByUser(userID, StockType.InStock);
            var test = from p in reportDao.Packages
                       where p.UserId == userID && p.StockStatus == StockType.InStock
                       select p;
            Assert.AreEqual(packageList.Count(), test.Count());
        }
        [TestMethod]
        public void PackageByCenterTest()
        {
            int centerID = 1;
            var packageList = reportDao.PackageByCenter(centerID, StockType.InTransit);
            var test = from p in reportDao.Packages
                       where p.DistributionCenterID == centerID && p.StockStatus == StockType.InTransit
                       select p;
            Assert.AreEqual(packageList.Count(), test.Count());
        }
    }
}
