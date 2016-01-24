using Ass2.Incognito.Enet.DBContext;
using Ass2.Incognito.Enet.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Ass2.Incognito.Enet.DAO
{
    public class DistributionCenterDAO : IncognitoDbContext
    {
        public List<SelectListItem> GetDistributionCenterList()
        {
            var distCtr = (from x in DistributionCenters
                       select new SelectListItem { Value = x.DistributionCenterID.ToString(), Text = x.Name }).ToList();
            return distCtr;
        }
        public List<DistributionCenter> GetAllDistributionCenter()
        {
            var result = from x in DistributionCenters
                         select x;
            return result.ToList();
        }
        public DistributionCenter GetDistributionCenter(int centerID)
        {
            var center = DistributionCenters.SingleOrDefault(DistributionCenter => DistributionCenter.DistributionCenterID == centerID);
            return center; 
        }
    }
}
