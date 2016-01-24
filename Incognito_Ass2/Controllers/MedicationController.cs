using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ass2.Incognito.Enet.Entities;
using Ass2.Incognito.Enet.Services;


namespace Ass2.Incognito.Enet.Controllers
{
    public class MedicationController : Controller
    {
        private MedicationService medicationService = null; 

        public MedicationController()
        {
            medicationService = new MedicationService();
        }

        [HttpGet]
        public ActionResult RegisterMedication()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterMedication(Medication med)
        {
            medicationService.AddMedication(med);
            return View();
        }

    }
}
