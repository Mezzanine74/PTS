using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace host_NET45_MVC.Areas.PtsPO.Controllers
{
    [Authorize]
    public class JSONController : host_NET45_MVC.Controllers.BaseController
    {
        private PTS_App_Code_VB_Project.PTS_MERCURY.db.SQL2008_794282_mercuryEntities db = new PTS_App_Code_VB_Project.PTS_MERCURY.db.SQL2008_794282_mercuryEntities();

        #region ModelClasses
        public class DDLSupplierPOCreateClass
        {
            public string SupplierID { get; set; }
            public string SupplierName { get; set; }
        }
        #endregion

        #region JsonResults
        public JsonResult DDLProjectsPOCreate(string id)
        {
            var _result = db.sp_DDLProjectsPOCreate(id).ToList();

            return Json(_result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DDLRequestedByPOCreate(int id)
        {
            var _result = db.sp_DDLRequestedByPOCreate(id).ToList();

            return Json(_result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DDLSupplierPOCreate()
        {
            var _result = db.Table6_Supplier.Select(c => new DDLSupplierPOCreateClass { SupplierID = c.SupplierID.Trim(), SupplierName = c.SupplierName.Trim() + " - " + c.SupplierID.Trim() }).ToList();

            return Json(_result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DDLCostCodePOCreate(int prjid, string username)
        {
            var _result = db.sp_DDLCostCodePOCreate(prjid, username).ToList();
            return Json(_result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ValidateFinanceNo(string no, int year)
        {

            // empty Finance No invalid
            if (no.Trim().Length == 0) { return Json(false, JsonRequestBehavior.AllowGet); }

            int count = db.Table5_PayLog.Where(c => c.FinanceNo == no && c.PaymentDate.Year == year).Count();

            if (count > 0) return Json(false, JsonRequestBehavior.AllowGet);
            if (count == 0) return Json(true, JsonRequestBehavior.AllowGet);

            return Json(false, JsonRequestBehavior.AllowGet);
        }



        #endregion

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