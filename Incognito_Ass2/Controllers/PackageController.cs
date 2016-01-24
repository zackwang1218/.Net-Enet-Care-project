using Ass2.Incognito.Enet.Entities;
using Ass2.Incognito.Enet.Services;
using Ass2.Incognito.Enet.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace Ass2.Incognito.Enet.Controllers
{
    public class PackageController : Controller
    {
        private PackageService packageServies = null;
        private DistributionCenterService distributionCenterService;

        public PackageController()
        {
            packageServies = new PackageService();
            distributionCenterService = new DistributionCenterService();
        }

        [HttpGet]
        public ActionResult RegisterPackage()
        {
            ListMedication();
            return View();
        }
        [HttpPost]
        public ActionResult RegisterPackage(Package package)
        {
            int barcode = 0;
            if(Request.Form["Register"] != null)
            {
                barcode = packageServies.AddPackage(package, WebSecurity.CurrentUserId);
            }
            ListMedication();
            return View(package);
        }
        [HttpGet]
        public ActionResult SendPackage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SendPackage(Package pack)
        {
            ViewBag.DistributionCenters = new DistributionCenterService().GetDistributionCenterList();
            if (Request.Form["SearchBarcode"] != null)
            {
                return View(packageServies.CheckIfBarcodeExist(pack.Barcode, StockType.InStock));
            }
            else if(Request.Form["SendPackageConfirm"] != null)
            {
                packageServies.UpdatePackageStatus(WebSecurity.CurrentUserId, pack.Barcode, StockType.InTransit, pack.ReceivingCenterID.Value);
            }
            return View();
        }
        [HttpGet]
        public ActionResult ReceivePackage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ReceivePackage(Package pack)
        {
            if (Request.Form["SearchBarcode"] != null)
            {
                return View(packageServies.CheckIfBarcodeExist(pack.Barcode, StockType.InTransit));
            }
            else if (Request.Form["ReceivePackageConfirm"] != null)
            {
                packageServies.UpdatePackageStatus(WebSecurity.CurrentUserId, pack.Barcode, StockType.InStock, null);
            }
            return View();
        }
        [HttpGet]
        public ActionResult DistributePackage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DistributePackage(Package pack)
        {
            if (Request.Form["SearchBarcode"] != null)
            {
                return View(packageServies.CheckIfBarcodeExist(pack.Barcode, StockType.InStock));
            }
            else if (Request.Form["DistributePackageConfirm"] != null)
            {
                packageServies.UpdatePackageStatus(WebSecurity.CurrentUserId, pack.Barcode, StockType.Distributed, null);
            }
            return View();
        }

        [HttpGet]
        public ActionResult DiscardPackage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DiscardPackage(Package pack)
        {
            if (Request.Form["SearchBarcode"] != null)
            {
                return View(packageServies.CheckIfBarcodeExist(pack.Barcode, StockType.InStock));
            }
            else if (Request.Form["DiscardPackageConfirm"] != null)
            {
                packageServies.UpdatePackageStatus(WebSecurity.CurrentUserId, pack.Barcode, StockType.Discarded, null);
            }
            return View();
        }

        public ActionResult StockTakingReport()
        {
            return View(packageServies.AllPackagesInCenter(WebSecurity.CurrentUserId));
        }
        [HttpGet]
        public ActionResult AuditPackage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AuditPackage(Package pack, string actionType)
        {
            if (Request.Form["SearchBarcode"] != null)
            {
                return View(packageServies.CheckIfBarcodeExist(pack.Barcode, null));
            }
            else if (Request.Form["AuditFeature"] != null)
            {
                StockType stockType = StockType.InStock;
                if (actionType.CompareTo("InStock") == 0)
                    stockType = StockType.InStock;

                if (actionType.CompareTo("Discard") == 0)
                    stockType = StockType.Discarded;

                if (actionType.CompareTo("Lost") == 0)
                    stockType = StockType.Lost;

                if (actionType.CompareTo("Remove") == 0)
                {
                    packageServies.RemovePackage(pack.Barcode);
                }
                else
                    packageServies.UpdatePackageStatus(WebSecurity.CurrentUserId, pack.Barcode, stockType, null);
            }
            return View();
        }
        public void ListMedication()
        {
            ViewBag.Medications = new MedicationService().GetMedicationList();
        }
        public ActionResult GetShellLife(int id)
        {
            string expDate = new MedicationService().GetShellLifeInDays(id).ToShortDateString();
            return Json(new { FinalDate = expDate });
        }
    }
}
