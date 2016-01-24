using Ass2.Incognito.Enet.DAO;
using Ass2.Incognito.Enet.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Ass2.Incognito.Enet.Services
{
    public class DistributionCenterService
    {
        private DistributionCenterDAO distributionCenterDao = null;

        public DistributionCenterService()
        {
            distributionCenterDao = new DistributionCenterDAO();
        }
        public List<SelectListItem> GetDistributionCenterList()
        {
            return distributionCenterDao.GetDistributionCenterList();
        }
        public virtual List<DistributionCenter> GetDistributionCenterNames()
        {
            var totalCenter = distributionCenterDao.GetAllDistributionCenter();
            return totalCenter;
        }
        public List<SelectListItem> DistributionCenterStock(DistributionCenter center)
        {
            return null;
        }
    }
}
