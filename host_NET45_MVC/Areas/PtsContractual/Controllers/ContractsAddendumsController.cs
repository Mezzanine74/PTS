using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace host_NET45_MVC.Areas.PtsContractual.Controllers
{
    public class ContractsAddendumsController : _BaseController
    {
        private PTS_App_Code_VB_Project.PTS_MERCURY.db.SQL2008_794282_mercuryEntities db = new PTS_App_Code_VB_Project.PTS_MERCURY.db.SQL2008_794282_mercuryEntities();

        // JSONS
        public JsonResult getProjectsSearch()
        {
            return Json(
                db.Table1_Project
                .Where(o => o.CurrentStatus == true && o.NewGeneration == true)
                .Select(c => new { c.ProjectID, c.ProjectName })
                .OrderBy(c=>c.ProjectName),JsonRequestBehavior.AllowGet);
        }

        public JsonResult getSuppliersSearch()
        {
            return Json(
                db.View_SupplierNamesForContracts
                .Select(c=> new { c.SupplierID, c.SupplierName })
                .OrderBy(c=>c.SupplierName), JsonRequestBehavior.AllowGet);
        }


        // GET: PtsContractual/ContractsAddendums
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ContractCoverPage(int id)
        {
            host_NET45_MVC.Areas.PtsWorker.Controllers.EmailGeneratorController A = new host_NET45_MVC.Areas.PtsWorker.Controllers.EmailGeneratorController();
            return View(A.getContractDetails(id));
        }

        public ActionResult AddendumCoverPage(int id)
        {
            host_NET45_MVC.Areas.PtsWorker.Controllers.EmailGeneratorController A = new host_NET45_MVC.Areas.PtsWorker.Controllers.EmailGeneratorController();
            return View(A.getAddendumDetails(id));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}