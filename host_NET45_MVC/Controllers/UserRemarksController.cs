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
    public class UserRemarksController : Controller
    {
        private SQL2008_794282_mercuryEntities db = new SQL2008_794282_mercuryEntities();

        public class Table_Contracts_UserRemarksSelect
        {
            public int id { get; set; }
            public int ContractID { get; set; }
            public string UserName { get; set; }
            public string PageUserName { get; set; }
            public string Remark { get; set; }
            public string LastUpdate { get; set; }
            public Boolean ShowOrHide { get; set; }
        }

        // GET: Table_Contracts_UserRemarks
        public ActionResult GetByContractId(int id)
        {
            var table_Contracts_UserRemarks = db.Table_Contracts_UserRemarks
                .Select(c => new Table_Contracts_UserRemarksSelect 
                { id = c.id, ContractID = c.ContractID, UserName = c.UserName, Remark = c.Remark, LastUpdate = c.LastUpdate.ToString()}).Where(c => c.ContractID == id);

            List<Table_Contracts_UserRemarksSelect> _return = new List<Table_Contracts_UserRemarksSelect>();

            foreach (var item in table_Contracts_UserRemarks)
            {
                _return.Add(new Table_Contracts_UserRemarksSelect
                {
                    id = item.id,
                    ContractID = item.ContractID,
                    UserName = item.UserName,
                    PageUserName = User.Identity.Name.ToLower(),
                    Remark = item.Remark,
                    LastUpdate = item.LastUpdate,
                    ShowOrHide = ShowOrHideRemarkEntry(item.UserName, User.Identity.Name.ToLower())
                });
            }

        // Insert unread comments to table for this user

            string ForWho = System.Web.Security.Roles.IsUserInRole("ContractLeadGirls") == true ? "lawyers" : User.Identity.Name.ToString().ToLower();

                db.Database.ExecuteSqlCommand( 
                " INSERT INTO [dbo].[Table_Contract_CommentsRead] " + 
                "            ([C_Or_A] " + 
                "            ,[id_comment] " + 
                "            ,[FromWho] " + 
                "            ,[ReadWho] " + 
                "            ,[WhenRead]) " + 
                " Select [C_or_A] " + 
                "       ,[id_comment] " + 
                "       ,[FromWho] " + 
                "       ,[ForWho] " + 
                "       ,{0} " + 
                "   FROM [ApprMx].[Notf_ContractCommentsUnRead] " + 
                " WHERE C_or_A = N'C' and C_or_A_id = {1} and ForWho = {2}",
                DateTime.Now, id, ForWho);

            return Json(_return, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult CreateContract(Table_Contracts_UserRemarks table_Contracts_UserRemarks)
        {
            table_Contracts_UserRemarks.LastUpdate = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.Table_Contracts_UserRemarks.Add(table_Contracts_UserRemarks);
                db.SaveChanges();
                return Json(
                    new Table_Contracts_UserRemarksSelect
                    {
                        id = table_Contracts_UserRemarks.id,
                        ContractID = table_Contracts_UserRemarks.ContractID,
                        UserName = table_Contracts_UserRemarks.UserName,
                        PageUserName = User.Identity.Name.ToLower(),
                        Remark = table_Contracts_UserRemarks.Remark,
                        LastUpdate = DateTime.Now.ToString(),
                        ShowOrHide = ShowOrHideRemarkEntry(table_Contracts_UserRemarks.UserName, User.Identity.Name.ToLower())
                    }
                    , JsonRequestBehavior.AllowGet);
            }
            return Json(new { data = false }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult EditContract(int id, string Remark)
        {
            Table_Contracts_UserRemarks table_Contracts_UserRemarks = db.Table_Contracts_UserRemarks.Find(id);
            table_Contracts_UserRemarks.Remark = Remark;
            table_Contracts_UserRemarks.LastUpdate = DateTime.Now;
            db.SaveChanges();

            return Json(
                new Table_Contracts_UserRemarksSelect
                {
                    id = table_Contracts_UserRemarks.id,
                    ContractID = table_Contracts_UserRemarks.ContractID,
                    UserName = table_Contracts_UserRemarks.UserName,
                    PageUserName = User.Identity.Name.ToLower(),
                    Remark = table_Contracts_UserRemarks.Remark,
                    LastUpdate = DateTime.Now.ToString(),
                    ShowOrHide = ShowOrHideRemarkEntry(table_Contracts_UserRemarks.UserName, User.Identity.Name.ToLower())
                }
                , JsonRequestBehavior.AllowGet);

        }

        //[ValidateAntiForgeryToken]
        public ActionResult DeleteContract(int id)
        {
            Table_Contracts_UserRemarks table_Contracts_UserRemarks = db.Table_Contracts_UserRemarks.Find(id);
            db.Table_Contracts_UserRemarks.Remove(table_Contracts_UserRemarks);
            db.SaveChanges();
            return Json(new { data = true }, JsonRequestBehavior.AllowGet);
        }


        public class Table_Addendums_UserRemarksSelect
        {
            public int id { get; set; }
            public int AddendumID { get; set; }
            public string UserName { get; set; }
            public string PageUserName { get; set; }
            public string Remark { get; set; }
            public string LastUpdate { get; set; }
            public Boolean ShowOrHide { get; set; }
        }

        // GET: Table_Addendums_UserRemarks
        public ActionResult GetByAddendumId(int id)
        {
            var table_Addendums_UserRemarks = db.Table_Addendum_UserRemarks
                .Select(c => new Table_Addendums_UserRemarksSelect { id = c.id, AddendumID = c.AddendumID, UserName = c.UserName, Remark = c.Remark, LastUpdate = c.LastUpdate.ToString() }).Where(c => c.AddendumID == id);

            List<Table_Addendums_UserRemarksSelect> _return = new List<Table_Addendums_UserRemarksSelect>();

            foreach (var item in table_Addendums_UserRemarks)
            {
                _return.Add(new Table_Addendums_UserRemarksSelect
                {
                    id = item.id,
                    AddendumID = item.AddendumID,
                    UserName = item.UserName,
                    PageUserName = User.Identity.Name.ToLower(),
                    Remark = item.Remark,
                    LastUpdate = item.LastUpdate,
                    ShowOrHide = ShowOrHideRemarkEntry(item.UserName, User.Identity.Name.ToLower())
                });
            }

            // Insert unread comments to table for this user

            string ForWho = System.Web.Security.Roles.IsUserInRole("ContractLeadGirls") == true ? "lawyers" : User.Identity.Name.ToString().ToLower();

            db.Database.ExecuteSqlCommand(
                " INSERT INTO [dbo].[Table_Contract_CommentsRead] " + 
                "            ([C_Or_A] " + 
                "            ,[id_comment] " + 
                "            ,[FromWho] " + 
                "            ,[ReadWho] " + 
                "            ,[WhenRead]) " + 
                " Select [C_or_A] " + 
                "       ,[id_comment] " + 
                "       ,[FromWho] " + 
                "       ,[ForWho] " + 
                "       ,{0} " + 
                "   FROM [ApprMx].[Notf_AddendumCommentsUnRead] " + 
                " WHERE C_or_A = N'A' and C_or_A_id = {1} and ForWho = {2} ", 
            DateTime.Now, id, ForWho);

            return Json(_return, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult CreateAddendum(Table_Addendum_UserRemarks table_Addendums_UserRemarks)
        {
            table_Addendums_UserRemarks.LastUpdate = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.Table_Addendum_UserRemarks.Add(table_Addendums_UserRemarks);
                db.SaveChanges();
                return Json(
                    new Table_Addendums_UserRemarksSelect
                    {
                        id = table_Addendums_UserRemarks.id,
                        AddendumID = table_Addendums_UserRemarks.AddendumID,
                        UserName = table_Addendums_UserRemarks.UserName,
                        PageUserName = User.Identity.Name.ToLower(),
                        Remark = table_Addendums_UserRemarks.Remark,
                        LastUpdate = DateTime.Now.ToString(),
                        ShowOrHide = ShowOrHideRemarkEntry(table_Addendums_UserRemarks.UserName, User.Identity.Name.ToLower())
                    }
                    , JsonRequestBehavior.AllowGet);
            }
            return Json(new { data = false }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult EditAddendum(int id, string Remark)
        {
            Table_Addendum_UserRemarks table_Addendums_UserRemarks = db.Table_Addendum_UserRemarks.Find(id);
            table_Addendums_UserRemarks.Remark = Remark;
            table_Addendums_UserRemarks.LastUpdate = DateTime.Now;
            db.SaveChanges();

            return Json(
                new Table_Addendums_UserRemarksSelect
                {
                    id = table_Addendums_UserRemarks.id,
                    AddendumID = table_Addendums_UserRemarks.AddendumID,
                    UserName = table_Addendums_UserRemarks.UserName,
                    PageUserName = User.Identity.Name.ToLower(),
                    Remark = table_Addendums_UserRemarks.Remark,
                    LastUpdate = DateTime.Now.ToString(),
                    ShowOrHide = ShowOrHideRemarkEntry(table_Addendums_UserRemarks.UserName, User.Identity.Name.ToLower())
                }
                , JsonRequestBehavior.AllowGet);

        }

        //[ValidateAntiForgeryToken]
        public ActionResult DeleteAddendum(int id)
        {
            Table_Addendum_UserRemarks table_Addendums_UserRemarks = db.Table_Addendum_UserRemarks.Find(id);
            db.Table_Addendum_UserRemarks.Remove(table_Addendums_UserRemarks);
            db.SaveChanges();
            return Json(new { data = true }, JsonRequestBehavior.AllowGet);
        }
        
        static Boolean ShowOrHideRemarkEntry(string userName, string pageUserName)
        {
            if (userName == pageUserName)
            {
                return true;
            }

            if (userName == "lawyers" && System.Web.Security.Roles.IsUserInRole("ContractLeadGirls") == true)
            {
                return true;
            }
            return false;
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
