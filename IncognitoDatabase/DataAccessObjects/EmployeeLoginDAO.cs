using Ass2.Incognito.Enet.DBContext;
using Ass2.Incognito.Enet.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ass2.Incognito.Enet.DAO
{
    public class EmployeeLoginDAO : IncognitoDbContext
    {
        public EmployeeLogin GetEmployeeLoginDetail(int userID)
        {
            var loginDetail = EmployeeLogins.SingleOrDefault(EmployeeLogin => EmployeeLogin.UserId == userID);
            return loginDetail;
        }
        public EmployeeLogin SaveEditedValue(EmployeeLogin empLogin)
        {
            var loginDetail = EmployeeLogins.SingleOrDefault(EmployeeLogin => EmployeeLogin.EmpLoginID == empLogin.EmpLoginID);
            loginDetail.DistributionCenterID = empLogin.DistributionCenterID;
            loginDetail.Full_Name = empLogin.Full_Name;
            loginDetail.Email = empLogin.Email;
            SaveChanges();
            return loginDetail;
        }
    }
}
