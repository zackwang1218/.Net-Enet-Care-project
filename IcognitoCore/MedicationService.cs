using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ass2.Incognito.Enet.DAO;
using Ass2.Incognito.Enet.Entities;
using System.Web.Mvc;

namespace Ass2.Incognito.Enet.Services
{
    public class MedicationService
    {
        private MedicationDAO medicationDao;
        public MedicationService()
        {
            medicationDao = new MedicationDAO();
        }
        public bool AddMedication(Medication med)
        {
            medicationDao.RegisterMedication(med);
            return true;
        }
        public List<SelectListItem> GetMedicationList()
        {
            return medicationDao.GetMedicationList();
        }
        public DateTime GetShellLifeInDays(int medicationID)
        {
            return DateTime.Today.AddDays(medicationDao.GetShellLifeInDays(medicationID));
        }
    }
}
