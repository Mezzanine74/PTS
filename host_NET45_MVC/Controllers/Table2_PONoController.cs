using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PTS_App_Code_VB_Project.PTS_MERCURY.db;

namespace host_NET45_MVC.Controllers
{
    public class Table2_PONoController : BaseController
    {
        private SQL2008_794282_mercuryEntities db = new SQL2008_794282_mercuryEntities();

        // GET: Table2_PONo
        public ActionResult Index()
        {
            var table2_PONo = db.Table2_PONo.Include(t => t.Table6_Supplier).Include(t => t.Table7_CostCode).Include(t => t.Table1_Project);
            return View(table2_PONo.ToList());
        }

        // GET: Table2_PONo/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Table2_PONo table2_PONo = db.Table2_PONo.Find(id);
            if (table2_PONo == null)
            {
                return HttpNotFound();
            }
            return View(table2_PONo);
        }

        // GET: Table2_PONo/Create
        public ActionResult Create()
        {
            ViewBag.SupplierID = new SelectList(db.Table6_Supplier, "SupplierID", "SupplierName");
            ViewBag.CostCode = new SelectList(db.Table7_CostCode, "CostCode", "CostVidisionID");
            ViewBag.Project_ID = new SelectList(db.Table1_Project, "ProjectID", "ProjectName");
            return View();
        }

        // POST: Table2_PONo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PO_No,Project_ID,SupplierID,Description,TotalPrice,PO_Currency,VATpercent,CostCode,Notes,PO_Date,CreatedBy,UpdatedBy,DeletedBy,PersonCreated,PersonUpdated,PersonDeleted,FromAccess,Approved,PersonApproved,RequestedBy,CO,Scenario,FrameContractPO,FrameContractID")] Table2_PONo table2_PONo)
        {
            if (ModelState.IsValid)
            {
                db.Table2_PONo.Add(table2_PONo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SupplierID = new SelectList(db.Table6_Supplier, "SupplierID", "SupplierName", table2_PONo.SupplierID);
            ViewBag.CostCode = new SelectList(db.Table7_CostCode, "CostCode", "CostVidisionID", table2_PONo.CostCode);
            ViewBag.Project_ID = new SelectList(db.Table1_Project, "ProjectID", "ProjectName", table2_PONo.Project_ID);
            return View(table2_PONo);
        }

        // GET: Table2_PONo/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Table2_PONo table2_PONo = db.Table2_PONo.Find(id);
            if (table2_PONo == null)
            {
                return HttpNotFound();
            }
            ViewBag.SupplierID = new SelectList(db.Table6_Supplier, "SupplierID", "SupplierName", table2_PONo.SupplierID);
            ViewBag.CostCode = new SelectList(db.Table7_CostCode, "CostCode", "CostVidisionID", table2_PONo.CostCode);
            ViewBag.Project_ID = new SelectList(db.Table1_Project, "ProjectID", "ProjectName", table2_PONo.Project_ID);
            return View(table2_PONo);
        }

        // POST: Table2_PONo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PO_No,Project_ID,SupplierID,Description,TotalPrice,PO_Currency,VATpercent,CostCode,Notes,PO_Date,CreatedBy,UpdatedBy,DeletedBy,PersonCreated,PersonUpdated,PersonDeleted,FromAccess,Approved,PersonApproved,RequestedBy,CO,Scenario,FrameContractPO,FrameContractID")] Table2_PONo table2_PONo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(table2_PONo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SupplierID = new SelectList(db.Table6_Supplier, "SupplierID", "SupplierName", table2_PONo.SupplierID);
            ViewBag.CostCode = new SelectList(db.Table7_CostCode, "CostCode", "CostVidisionID", table2_PONo.CostCode);
            ViewBag.Project_ID = new SelectList(db.Table1_Project, "ProjectID", "ProjectName", table2_PONo.Project_ID);
            return View(table2_PONo);
        }

        // GET: Table2_PONo/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Table2_PONo table2_PONo = db.Table2_PONo.Find(id);
            if (table2_PONo == null)
            {
                return HttpNotFound();
            }
            return View(table2_PONo);
        }

        // POST: Table2_PONo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Table2_PONo table2_PONo = db.Table2_PONo.Find(id);
            db.Table2_PONo.Remove(table2_PONo);
            db.SaveChanges();
            return RedirectToAction("Index");
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
