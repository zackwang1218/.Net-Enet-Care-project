using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ass2.Incognito.Enet.DAO;
using Ass2.Incognito.Enet.Entities;
using Ass2.Incognito.Enet.Utilities;

namespace Ass2.Incognito.Enet.Services
{
    public class PackageService
    {
        private PackageDAO packageDao;
        private EmployeeLoginDAO employeeLoginDao;
        public PackageService()
        {
            packageDao = new PackageDAO();
            employeeLoginDao = new EmployeeLoginDAO();
        }
        public int AddPackage(Package pack, int userID)
        {
            //Form Proper package types
            pack.UserId = userID;
            pack.DistributionCenterID = employeeLoginDao.GetEmployeeLoginDetail(userID).DistributionCenterID;
            pack.ReceivingCenterID = null;
            pack.StockStatus = StockType.InStock;
            pack.TransactionDate = DateTime.Today;

            return packageDao.RegisterPackage(pack);
        }
        /// <summary>
        /// Search package with given barcode and the given stock type
        /// </summary>
        /// <param name="barCode">Barcode of Package to be found</param>
        /// <param name="stockType">Package should have given status</param>
        /// <returns></returns>
        public Package CheckIfBarcodeExist(int barCode, StockType? stockType)
        {
            return packageDao.CheckIfPackageExist(barCode,stockType);
        }

        public void UpdatePackageStatus(int userID, int barCode, StockType stockType, int? receivingCenterID)
        {
            int currentCenterID = employeeLoginDao.GetEmployeeLoginDetail(userID).DistributionCenterID;
            packageDao.UpdatePackageStatus(userID, barCode, stockType, receivingCenterID, currentCenterID);
        }

        public ICollection<Package> AllPackagesInCenter(int userID)
        {
            int currentCenterID = employeeLoginDao.GetEmployeeLoginDetail(userID).DistributionCenterID;
            return packageDao.PackagesInCenter(currentCenterID);
        }
        public void RemovePackage(int barCode)
        {
            packageDao.RemovePackage(barCode);
        }
    }
}
