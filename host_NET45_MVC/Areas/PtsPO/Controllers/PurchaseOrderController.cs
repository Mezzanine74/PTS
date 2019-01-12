using DevExpress.Web.Mvc;
using PTS_App_Code_VB_Project.PTS_MERCURY.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace host_NET45_MVC.Areas.PtsPO.Controllers
{
    public class PurchaseOrderController : host_NET45_MVC.Controllers.BaseController
    {
        private PTS_App_Code_VB_Project.PTS_MERCURY.db.SQL2008_794282_mercuryEntities db = new PTS_App_Code_VB_Project.PTS_MERCURY.db.SQL2008_794282_mercuryEntities();

        // View Models
        #region ViewModels
        public class Table6_SupplierViewModel
        {
            [Required(ErrorMessage = "Supplier INN required")]
            public string SupplierIDEnter { get; set; }

            [Required(ErrorMessage = "Supplier Name required")]
            public string SupplierName { get; set; }
        }

        public class POModel
        {
            [Required(ErrorMessage="Project required")]
            [Range(1, int.MaxValue, ErrorMessage="Project required")]
            public Int16 Project_ID { get; set; }

            public string ProjectIdName { get; set; }

            [Required(ErrorMessage="Supplier required")]
            public string SupplierID { get; set; }

            public string SupplierIDname { get; set; }

            [Required(ErrorMessage="Description required")]
            public string Description { get; set; }

            [Required(ErrorMessage="PO value required")]
            public decimal TotalPrice { get; set; }

            [Required(ErrorMessage="Currency required")]
            public string PO_Currency { get; set; }

            [Required(ErrorMessage="VAT required")]
            public decimal VATpercent { get; set; }

            [Required(ErrorMessage="CostCode required")]
            public string CostCode { get; set; }

            public string CostCodeDescription { get; set; }

            [Required(ErrorMessage="PO Date required")]
            public DateTime PO_Date { get; set; }

            public string WhoRequested { get; set; }

        }

        public class InvoiceModel
        {
            [Required(ErrorMessage = "Project required")]
            public int ProjectId { get; set; }

            [Required(ErrorMessage = "PO required")]
            public string PO { get; set; }

            public int InvoiceType { get; set; }

            [Required(ErrorMessage = "Invoice No required")]
            public string InvoiceNo { get; set; }

            [Required(ErrorMessage = "Invoice Date required")]
            public DateTime InvoiceDate { get; set; }

            [Required(ErrorMessage = "Invoice Value required")]
            public decimal InvoiceValueExcVAT { get; set; }

            [Required(ErrorMessage = "Invoice Description required")]
            public string InvoiceDescription { get; set; }

            public decimal POTotalExcVAT { get; set; }
            public decimal TotalInvoiceExcVAT { get; set; }
            public decimal OutstandingExcVAT { get; set; }
            public string InvoicePath { get; set; }
        }

        public class PaymentRequestModel
        {
            [Required(ErrorMessage = "Project required")]
            public int ProjectId { get; set; }

            [Required(ErrorMessage = "Invoice required")]
            public int InvoiceId { get; set; }

            [Required(ErrorMessage = "SiteReqNo required")]
            public string SiteRecordNo { get; set; }

            [Required(ErrorMessage = "Date required")]
            public DateTime PayReqDate { get; set; }

            public string RequestPDF { get; set; }
        }

        #endregion

        public ActionResult POCreate()
        {
            ViewBag.PrjDll = db.Table1_Project.Select(c => new { c.ProjectID, c.ProjectName }).ToList();
            ViewBag.SupplierDll = db.Table6_Supplier.Select(c => new { c.SupplierID, c.SupplierName }).ToList();

            Helper.DDL_CurrencyHelper dDL_CurrencyHelper = new Helper.DDL_CurrencyHelper();

            ViewBag.CurrencyDll = dDL_CurrencyHelper.getDDL_Currency_List();
            return View(new POModel());

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult POCreate(POModel poModel)
        {
            return View(poModel);
        }

        [HttpGet]
        public PartialViewResult _EnterNewSupplierPartial()
        {
            return PartialView(new Table6_SupplierViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _EnterNewSupplierPartial(Table6_SupplierViewModel table6_SupplierViewModel)
        {
            // validate SupplierId
            if (!table6_SupplierViewModel.SupplierIDEnter.All(char.IsDigit))
            {
                ViewBag.ValidationMessageINN = "Supplier ID must contain only numeric characters!";
                return PartialView("__EnterNewSupplierPartial", table6_SupplierViewModel);
            }

            if (!(table6_SupplierViewModel.SupplierIDEnter.Trim().Length == 10 || table6_SupplierViewModel.SupplierIDEnter.Trim().Length == 12))
            {
                ViewBag.ValidationMessageINN = "Supplier ID must be 10 or 12 digit!";
                return PartialView("__EnterNewSupplierPartial", table6_SupplierViewModel);
            }
            string _supplierid = table6_SupplierViewModel.SupplierIDEnter;
            if (db.Table6_Supplier.Where(c=>c.SupplierID == _supplierid).Count() > 0)
            {
                ViewBag.ValidationMessageINN = "Supplier ID already exist!";
                return PartialView("__EnterNewSupplierPartial", table6_SupplierViewModel);
            }

                if (ModelState.IsValid)
            {
                PTS_App_Code_VB_Project.PTS_MERCURY.db.Table6_Supplier table6_Supplier = new PTS_App_Code_VB_Project.PTS_MERCURY.db.Table6_Supplier();
                table6_Supplier.SupplierID = table6_SupplierViewModel.SupplierIDEnter;
                table6_Supplier.SupplierName = table6_SupplierViewModel.SupplierName;
                db.Table6_Supplier.Add(table6_Supplier);
                db.SaveChanges();
                ViewBag.Success = "1";
                ViewBag.SupplierID = table6_SupplierViewModel.SupplierIDEnter;
                ViewBag.SupplierName = table6_SupplierViewModel.SupplierName;

                return PartialView("__EnterNewSupplierPartial", table6_SupplierViewModel);
            }

            return PartialView("__EnterNewSupplierPartial", table6_SupplierViewModel);
        }

        [HttpGet]
        public PartialViewResult __EnterNewSupplierPartial()
        {
            return PartialView(new Table6_SupplierViewModel());
        }

        public  ActionResult InvoiceCreate()
        {
            return View(new InvoiceModel());
        }

        public ActionResult PaymentRequestCreate()
        {
            return View(new PaymentRequestModel());
        }

        public ActionResult PayLogCreate()
        {

            return View();
        }

        public ActionResult Pay(string financeNo, DateTime paymentDate, decimal amount, string currency, decimal rubbleDollar, decimal rubbleEuro)
        {
            //PTS_App_Code_VB_Project.PTS_MERCURY.db.Table5_PayLog table5_PayLog = new PTS_App_Code_VB_Project.PTS_MERCURY.db.Table5_PayLog
            //{
            //    FinanceNo = financeNo,
            //    PaymentDate = paymentDate,
            //    Amount = amount,
            //    Currency = currency,
            //    RubbleDollar = rubbleDollar,
            //    RubbleEuro = rubbleEuro
            //};

            //db.Table5_PayLog.Attach(table5_PayLog);
            //db.Table5_PayLog.Add(table5_PayLog);

            try
            {
                //db.SaveChanges();
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            //return Json(true, JsonRequestBehavior.AllowGet);

            return Json(new { financeNo = financeNo, paymentDate = paymentDate }, JsonRequestBehavior.AllowGet);


        }
        
        public ActionResult Main()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartialPayLog(string date = "01/01/1900", int? prjid = 0)
        {
            DateTime _day = DateTime.ParseExact(date, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            //DateTime _day = DateTime.ParseExact("2017-02-08", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            var model = db.View_WhatToBePaidRevised.Where(c => c.ProjectID == prjid && c.Date == _day);
            return PartialView("_GridViewPartialPayLog", model.ToList());
        }
    }
}