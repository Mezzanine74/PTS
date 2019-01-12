using PTS_App_Code_VB_Project.PTS_MERCURY.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace host_NET45_MVC.Controllers
{
    public class ReportsViewController : BaseController
    {
        private SQL2008_794282_mercuryEntities db = new SQL2008_794282_mercuryEntities();

        #region View_FrameContractsBudgetStatus
        // GET: Reports
        public ActionResult View_FrameContractsBudgetStatus(string projectid = "")
        {

            short _projectid = 0;
            try { _projectid = short.Parse(projectid); }
            catch (Exception) { }

            if (projectid.Trim().Length == 0)
            {
                ViewBag.ProjectName = "No Project Selected";
                return View((from C in db.View_FrameContractsBudgetStatus where C.ProjectID == _projectid select C).Take(0).ToList());
            }
            else 
            {
                string projectname = "";
                projectname = PTS_App_Code_VB_Project.PTS_MERCURY.helper.Table1_Project.GetRowByPrimaryKey(_projectid).ProjectName;
                ViewBag.ProjectName = projectname;

                return View((from C in db.View_FrameContractsBudgetStatus where C.ProjectID == _projectid select C).ToList());

            }

        }
        #endregion

    }
}