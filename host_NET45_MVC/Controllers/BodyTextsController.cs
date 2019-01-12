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
    public class BodyTextsController : BaseController
    {
        private SQL2008_794282_mercuryEntities db = new SQL2008_794282_mercuryEntities();

        public class BodyTextIndexModel
        {
            public int Id { get; set; }
            public string Ref { get; set; }
            public string GenerateCodeASPX { get; set; }
            public string GenerateCodeVB { get; set; }
            public string Eng { get; set; }
            public string Rus { get; set; }
        }

        // GET: BodyTexts
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult IndexJson()
        {
            return Json(
                db.BodyTexts.ToList().OrderByDescending(c => c.id)
                .Select(c => new BodyTextIndexModel
                {
                    Id = c.id,
                    Ref = c.Ref,
                    GenerateCodeASPX = "<%= BodyTexts.Ref(\"" + c.Ref + "\")%>",
                    GenerateCodeVB = "BodyTexts.Ref(\"" + c.Ref + "\")",
                    Eng = c.Eng,
                    Rus = c.Rus
                })
                , JsonRequestBehavior.AllowGet);
        }


        // GET: BodyTexts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BodyText bodyText = db.BodyTexts.Find(id);
            if (bodyText == null)
            {
                return HttpNotFound();
            }
            return View(bodyText);
        }

        // GET: BodyTexts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BodyTexts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Ref,Eng,Rus")] BodyText bodyText)
        {
            string base64Guid = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            bodyText.Ref = base64Guid.Replace("=", "");
            bodyText.Rus = bodyText.Eng;

            if (ModelState.IsValid)
            {
                db.BodyTexts.Add(bodyText);
                db.SaveChanges();
                //return RedirectToAction("Index");
                return Json(
                    db.BodyTexts.Where(a => a.id == bodyText.id).ToList()
                    .Select(c => new BodyTextIndexModel
                    {
                        Id = c.id,
                        Ref = c.Ref,
                        GenerateCodeASPX = "<%= BodyTexts.Ref(\"" + c.Ref + "\")%>",
                        GenerateCodeVB = "BodyTexts.Ref(\"" + c.Ref + "\")",
                        Eng = c.Eng,
                        Rus = c.Rus
                    })
                    , JsonRequestBehavior.AllowGet);
            }

            return View(bodyText);
        }

        // GET: BodyTexts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BodyText bodyText = db.BodyTexts.Find(id);
            if (bodyText == null)
            {
                return HttpNotFound();
            }
            return View(bodyText);
        }

        // POST: BodyTexts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Ref,Eng,Rus")] BodyText bodyText)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bodyText).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = "1"});
            }
            return Json(new { success = "0" });
        }

        // GET: BodyTexts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BodyText bodyText = db.BodyTexts.Find(id);
            if (bodyText == null)
            {
                return HttpNotFound();
            }
            return View(bodyText);
        }

        // POST: BodyTexts/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BodyText bodyText = db.BodyTexts.Find(id);
            db.BodyTexts.Remove(bodyText);
            db.SaveChanges();
            return Json(new { deleted = 1 });
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
