using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Threading.Tasks;
using Ass2.Incognito.Enet.Entities;
using Ass2.Incognito.Enet.DBContext;

namespace Ass2.Incognito.Enet.DAO
{
    public class MedicationDAO : IncognitoDbContext
    {
        public bool RegisterMedication(Medication med)
        {
            Medications.Add(med);
            SaveChanges();
            return true;
        }
        public List<SelectListItem> GetMedicationList()
        {
            var med = (from x in Medications
                        select new SelectListItem { Value = x.MedicalID.ToString(), Text = x.Description }).ToList();
            return med;
        }
        public Medication GetMedication(int medicationID)
        {
            var medicine = Medications.SingleOrDefault(Medication => Medication.MedicalID == medicationID);
            return medicine;
        }
        public int GetShellLifeInDays(int medicalID)
        {
            var shellLife = (from x in Medications
                        where x.MedicalID == medicalID
                        select x.Shell_Life).SingleOrDefault().ToString();
            return Int32.Parse(shellLife);
        }
    }
}
