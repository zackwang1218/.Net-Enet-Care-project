using Ass2.Incognito.Enet.Utilities;
using Ass2.Incognito.Enet.DBContext;
using Ass2.Incognito.Enet.Entities;
using Ass2.Incognito.Enet.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Ass2.Incognito.Enet.DAO
{
    public class ReportDAO : IncognitoDbContext
    {
        public ICollection<DistributionCenterStockView> DistributionCenterStock(int centerID)
        {
           /*This query is referenced from http://stackoverflow.com/questions/19395849/linq-group-by-on-multiple-table-and-inner-join */
            var result = from t in
                             (
                                 from h in Packages
                                 join u in Medications on h.MedicalID equals u.MedicalID
                                 where h.DistributionCenterID == centerID
                                 select new { h, u }
                                 )
                         group t by new { t.h.MedicalID } into g

                         select new DistributionCenterStockView
                         {
                             ItemID = g.Key.MedicalID,
                             Quantity = g.Sum(k => k.u.Quantity.Value)
                         };
           return result.ToList();
        }
        public ICollection<DistributionCenterStockView> GlobaStock()
        {
            var result = from t in
                             (
                                 from h in Packages
                                 join u in Medications on h.MedicalID equals u.MedicalID
                                 select new { h, u }
                                 )
                         group t by new { t.h.MedicalID } into g

                         select new DistributionCenterStockView
                         {
                             ItemID = g.Key.MedicalID,
                             Quantity = g.Sum(k => k.u.Quantity.Value)
                         };
            return result.ToList();
        }
        public ICollection<Package> PackageTypesList(StockType packageType)
        {
            var result = from p in Packages.Include(c => c.Medication).Include(p => p.DistributionCenter)
                         where p.StockStatus == packageType
                         select p;
            return result.ToList();
        }
        public ICollection<Package> PackageByUser(int userID, StockType packageType)
        {
            var result = from p in Packages.Include(c => c.Medication)
                         where p.UserId == userID
                         && p.StockStatus == packageType
                         select p;
            return result.ToList();
        }
        public ICollection<Package> PackageByCenter(int centerID, StockType packageType)
        {
            var result = from p in Packages.Include(c => c.Medication)
                         where p.DistributionCenterID == centerID
                         && p.StockStatus == packageType
                         select p;
            return result.ToList();
        }

        public IEnumerable<SelectListItem> GetDoctorList()
        {
            IList<SelectListItem> items = new List<SelectListItem>
                {
                    new SelectListItem{Text = "Doctor 1", Value = "4"},
                    new SelectListItem{Text = "Doctor 2", Value = "5"},
                    new SelectListItem{Text = "Doctor 3", Value = "6"}
                };
            return items;
        }
    }
}
