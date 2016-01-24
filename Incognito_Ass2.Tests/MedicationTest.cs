using System;
using System.Text;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ass2.Incognito.Enet.Services;
using Ass2.Incognito.Enet.DAO;
using Ass2.Incognito.Enet.Entities;
using Ass2.Incognito.Enet.Utilities;
using Ass2.Incognito.Enet.DBContext;
using System.Linq;

namespace Incognito_Ass2.Tests
{
    /// <summary>
    /// Summary description for MedicationTest
    /// </summary>
    [TestClass]
    public class MedicationTest
    {
        private Medication testmedi;
        private MedicationService medicationService;
        MedicationDAO medicationDao;
        public MedicationTest()
        {
            medicationDao = new MedicationDAO();
            medicationService = new MedicationService();
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
        public void AddMedicationTest()
        {
            testmedi = new Medication();
            testmedi.Description = "test";
            testmedi.Value = 99999;
            medicationService.AddMedication(testmedi);
            var medi = medicationDao.Medications.SingleOrDefault(Medication => Medication.Description == "test");
            double? testValue = medi.Value;
            int mediID = medi.MedicalID;
            var temp = medicationDao.Medications.SingleOrDefault(Medication => Medication.MedicalID == mediID);
            medicationDao.Medications.Remove(temp);
            medicationDao.SaveChanges();
            Assert.AreEqual(testValue, testmedi.Value);
        }
        [TestMethod]
        public void GetListTest()
        {
            var test = medicationService.GetMedicationList();
            var medilist = from e in medicationDao.Medications
                           select e;
            int i = test.Count();
            int j = medilist.Count();
            Assert.AreEqual(i, j);
        }
        [TestMethod]
        public void GetMedicationTest()
        {
            testmedi = new Medication();
            testmedi.Description = "test";
            testmedi.Value = 99999;
            medicationDao.RegisterMedication(testmedi);
            int mediID = medicationDao.Medications.SingleOrDefault(Medication => Medication.Description == "test").MedicalID;
            var result = medicationDao.GetMedication(mediID).Description;
            var me = medicationDao.Medications.SingleOrDefault(Medication => Medication.Description == "test");
            medicationDao.Medications.Remove(me);
            medicationDao.SaveChanges();
            Assert.AreEqual(result, "test");
        }
        [TestMethod]
        public void GetShellLifeTest()
        {
            testmedi = new Medication();
            testmedi.Description = "test";
            testmedi.Shell_Life = 99;
            medicationDao.RegisterMedication(testmedi);
            int mediID = medicationDao.Medications.SingleOrDefault(Medication => Medication.Description == "test").MedicalID;
            DateTime result = medicationService.GetShellLifeInDays(mediID);
            var me = medicationDao.Medications.SingleOrDefault(Medication => Medication.Description == "test");
            medicationDao.Medications.Remove(me);
            medicationDao.SaveChanges();
            var test = DateTime.Today.AddDays(99);
            Assert.AreEqual(result, test);
        }
    }
}
