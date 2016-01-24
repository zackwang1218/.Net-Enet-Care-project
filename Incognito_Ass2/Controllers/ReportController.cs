using Ass2.Incognito.Enet.Entities;
using Ass2.Incognito.Enet.Services;
using Ass2.Incognito.Enet.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Incognito_Ass2.Controllers.Report
{
    public class ReportController : Controller
    {
        private ReportService reportService = null;
        private int centerID;

        public ReportController()
        {
            reportService = new ReportService();
        }

        [HttpGet]
        public ActionResult DistributionCenterStock()
        {
            ListDistributionCenters();
            return View();
        }

        [HttpPost]
        public ActionResult DistributionCenterStock(DistributionCenterStockGUIView center)
        {
            centerID = center.CenterID;
            ListDistributionCenters();
            return View(reportService.GenerateDistributionCenterStock(centerID));
        }

        public ActionResult GlobalStock()
        {
            return View(reportService.GenerateGlobalStock());
        }

        public ActionResult DistributionCenterLoss()
        {
            return View();
        }

        public ActionResult ValueInTransit()
        {
            return View(reportService.TransitRecord());
        }

        public ActionResult DistributionCenterLosses()
        {
            return View(reportService.DisributionCenterLosses());
        }

        [HttpGet]
        public ActionResult DoctorActivity()
        {
            ListDoctors();
            return View();
        }

        [HttpPost]
        public ActionResult DoctorActivity(ValueInTransitView doc)
        {
            ListDoctors();
            return View(reportService.DoctorActivity(doc.DoctorID));
        }

        private void ListDistributionCenters()
        {
            ViewBag.DistributionCenters = new DistributionCenterService().GetDistributionCenterList();
        }

        public void ListDoctors()
        {
            ViewBag.DoctorList = new ReportService().GetDoctorList();
        }
    }
}