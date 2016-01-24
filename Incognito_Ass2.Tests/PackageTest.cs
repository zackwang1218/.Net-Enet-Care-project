using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ass2.Incognito.Enet.DAO;
using Ass2.Incognito.Enet.Entities;
using Ass2.Incognito.Enet.Utilities;
using Ass2.Incognito.Enet.Services;
using Ass2.Incognito.Enet.DBContext;
using System.Linq;

namespace Incognito_Ass2.Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class PackageTest
    {
        private PackageService packageService;
        private PackageDAO packageDao;
        private Package testPack;
        public PackageTest()
        {
            packageService = new PackageService();
            packageDao = new PackageDAO();
            testPack = new Package();
            testPack.UserId = 1;
            testPack.MedicalID = 1;
            testPack.ExpiryDate = DateTime.Today;
            testPack.DistributionCenterID = 1;
            testPack.ReceivingCenterID = null;
            testPack.TransactionDate = DateTime.Today;
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
        public void AddPackageTest()
        {
            int userID = 1;
            int barcode = packageService.AddPackage(testPack, userID);
            var pack = packageDao.Packages.SingleOrDefault(Package => Package.Barcode == barcode);
            int result = pack.Barcode;
            packageDao.Packages.Remove(pack);
            packageDao.SaveChanges();
            Assert.AreEqual(barcode, result);
        }

        [TestMethod]
        public void CheckExistTest()
        {
            int barcode = packageDao.RegisterPackage(testPack);
            var result = packageService.CheckIfBarcodeExist(barcode, null);
            int resultBarcode = result.Barcode;
            var pack = packageDao.Packages.SingleOrDefault(Package => Package.Barcode == barcode);
            packageDao.Packages.Remove(pack);
            packageDao.SaveChanges();
            Assert.AreEqual(barcode, resultBarcode);
        }
        
        [TestMethod]
        public void UpdateStatusTest()
        {
            int userID = 1;
            int barcode = packageDao.RegisterPackage(testPack);
            packageService.UpdatePackageStatus(userID, barcode, StockType.InStock, null);
            var pack = packageDao.Packages.SingleOrDefault(Package => Package.Barcode == barcode);
            var type = pack.StockStatus.ToString();
            packageDao.Packages.Remove(pack);
            packageDao.SaveChanges();
            Assert.AreEqual(type, "InStock");
        }
        [TestMethod]
        public void PackagesInCenterTest()
        {
            int userID = 1;
            var packageList = packageService.AllPackagesInCenter(userID);
            int test = packageList.Count();
            var center = packageDao.DistributionCenters.Find(userID);
            var packages = from e in center.Packages
                           select e;
            int result = packages.Count();
            Assert.AreEqual(test, result);
        }
        [TestMethod]
        public void RemoveTest()
        {
            int barcode = packageDao.RegisterPackage(testPack);
            packageService.RemovePackage(barcode);
            var pack = packageDao.Packages.SingleOrDefault(Package => Package.Barcode == barcode);
            Assert.AreEqual(pack, null);
        }
    }
}
