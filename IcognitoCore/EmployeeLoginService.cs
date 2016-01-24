using Ass2.Incognito.Enet.DAO;
using Ass2.Incognito.Enet.DBContext;
using Ass2.Incognito.Enet.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ass2.Incognito.Enet.Services
{
    public class EmployeeLoginService
    {
        private EmployeeLoginDAO employeeLoginDao;
        public EmployeeLoginService()
        {
            employeeLoginDao = new EmployeeLoginDAO();
        }

        public EmployeeLogin GetEmployeeLoginDetail(int userID)
        {
            return employeeLoginDao.GetEmployeeLoginDetail(userID);
        }

        public EmployeeLogin SaveEditedValue(EmployeeLogin empLogin)
        {
            return employeeLoginDao.SaveEditedValue(empLogin);
        }
    }
}
