using Ass2.Incognito.Enet.DBContext;
using Ass2.Incognito.Enet.Entities;
using Ass2.Incognito.Enet.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ass2.Incognito.Enet.DAO
{
    public class PackageDAO : IncognitoDbContext
    {
        public int RegisterPackage(Package pack)
        {
            Packages.Add(pack);
            SaveChanges();
            return pack.Barcode;
        }
        public Package CheckIfPackageExist(int barCode, StockType? stockType)
        {
            var pack = new Package();
            if (stockType != null)
            {
                pack = Packages.SingleOrDefault(Package => Package.Barcode == barCode && Package.StockStatus == stockType);
            }
            else
            {
                pack = Packages.SingleOrDefault(Package => Package.Barcode == barCode);
            }
            return pack;
        }
        /// <summary>
        /// Update the package status
        /// If the value is 0, no update will take place
        /// </summary>
        /// <param name="barCode"></param>
        /// <param name="stockType"></param>
        /// <param name="receivingCenterID"></param>
        /// <param name="distributionCenterID"></param>
        public void UpdatePackageStatus(int userID,int barCode,StockType stockType, int? receivingCenterID, int? distributionCenterID)
        {
            var pack = Packages.SingleOrDefault(Package => Package.Barcode == barCode);
            if (receivingCenterID != 0)
            {
                pack.ReceivingCenterID = receivingCenterID;
            }
            pack.UserId = userID; //Always put the userID of current logged in user
            pack.DistributionCenterID = distributionCenterID; //Always put the distribution Center ID of current user
            pack.StockStatus = stockType;
            SaveChanges();
        }
        public ICollection<Package> PackagesInCenter(int centerID)
        {
            // Reload the distribution center in case the supplied center is not associated with the current context
            var center = DistributionCenters.Find(centerID);

            var packages = from e in center.Packages
                           select e;

            return packages.ToList();
        }
        public void RemovePackage(int barCode)
        {
            var pack = Packages.SingleOrDefault(Package => Package.Barcode == barCode);
            Packages.Remove(pack);
            SaveChanges();
        }
    }
}
