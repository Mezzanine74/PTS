using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PTS_App_Code_VB_Project.PTS_MERCURY.db;
using System.IO;

namespace host_NET45_MVC.Controllers
{
    public class Table_BudgetChangeLogController : BaseController
    {
        private SQL2008_794282_mercuryEntities db = new SQL2008_794282_mercuryEntities();

        // GET: Table_BudgetChangeLog
        public ActionResult Index()
        {
            return View(db.Table_BudgetChangeLog.ToList());
        }

        // GET: Table_BudgetChangeLog/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Table_BudgetChangeLog table_BudgetChangeLog = db.Table_BudgetChangeLog.Find(id);
            if (table_BudgetChangeLog == null)
            {
                return HttpNotFound();
            }
            return View(table_BudgetChangeLog);
        }

        // GET: Table_BudgetChangeLog/Create
        public ActionResult Create()
        {
            ViewBag.ProjectID = new SelectList(db.Table1_Project.OrderBy(x => x.ProjectName), "ProjectID", "ProjectName");
            ViewBag.CostCode = new SelectList(db.Table7_CostCode.OrderBy(x => x.CostCode), "CostCode", "CostCode");
            return View();
        }

        // POST: Table_BudgetChangeLog/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,ProjectId,CostCode,OldValue,NewValue,Explanation,Attachment,Email")] Table_BudgetChangeLog table_BudgetChangeLog)
        {
            if (ModelState.IsValid)
            {

                foreach (string _file in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[_file];
                    //Save file here
                    if (file != null && file.ContentLength > 0)

                        try
                        {
                            string _folder = "~/Views/Table_BudgetChangeLog/Files";
                            string path = Path.Combine(Server.MapPath(_folder), Path.GetFileName(file.FileName));
                            file.SaveAs(path);
                            table_BudgetChangeLog.Attachment = _folder + "/" + Path.GetFileName(file.FileName);
                            TempData["FileUploadMessage"] = "File uploaded successfully";

                        }
                        catch (Exception ex)
                        {
                            TempData["FileUploadMessage"] = "ERROR:" + ex.Message.ToString();
                        }
                    else
                    {
                        TempData["FileUploadMessage"] = "You have not specified a file.";
                    }

                }

                table_BudgetChangeLog.LogTime = DateTime.Now;
                db.Table_BudgetChangeLog.Add(table_BudgetChangeLog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(table_BudgetChangeLog);
        }

        // GET: Table_BudgetChangeLog/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Table_BudgetChangeLog table_BudgetChangeLog = db.Table_BudgetChangeLog.Find(id);
            if (table_BudgetChangeLog == null)
            {
                return HttpNotFound();
            }
            return View(table_BudgetChangeLog);
        }

        // POST: Table_BudgetChangeLog/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,ProjectId,CostCode,OldValue,NewValue,Explanation,Attachment,Email")] Table_BudgetChangeLog table_BudgetChangeLog)
        {
            if (ModelState.IsValid)
            {

                foreach (string _file in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[_file];
                    //Save file here
                    if (file != null && file.ContentLength > 0)

                        try
                        {
                            string _folder = "~/Views/Table_BudgetChangeLog/Files";
                            string path = Path.Combine(Server.MapPath(_folder), Path.GetFileName(file.FileName));
                            file.SaveAs(path);
                            table_BudgetChangeLog.Attachment = _folder + "/" + Path.GetFileName(file.FileName);
                            TempData["FileUploadMessage"] = "File uploaded successfully";

                        }
                        catch (Exception ex)
                        {
                            TempData["FileUploadMessage"] = "ERROR:" + ex.Message.ToString();
                        }
                    else
                    {
                        TempData["FileUploadMessage"] = "You have not specified a file.";
                    }

                }

                db.Entry(table_BudgetChangeLog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(table_BudgetChangeLog);
        }

        // GET: Table_BudgetChangeLog/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Table_BudgetChangeLog table_BudgetChangeLog = db.Table_BudgetChangeLog.Find(id);
            if (table_BudgetChangeLog == null)
            {
                return HttpNotFound();
            }
            return View(table_BudgetChangeLog);
        }

        // POST: Table_BudgetChangeLog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Table_BudgetChangeLog table_BudgetChangeLog = db.Table_BudgetChangeLog.Find(id);
            db.Table_BudgetChangeLog.Remove(table_BudgetChangeLog);
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
