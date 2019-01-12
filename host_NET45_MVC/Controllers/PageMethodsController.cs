using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace host_NET45_MVC.Controllers
{
    public class PageMethodsController : BaseController
    {

        #region Table_Budget_PlannedToSpendConstraints
            public void Table_Budget_PlannedToSpendConstraints_Insert(int BudgetID)
                {
                    PTS_App_Code_VB_Project.PTS_MERCURY.helper.Table_Budget_PlannedToSpendConstraints.Insert(BudgetID);
                }

            public void Table_Budget_PlannedToSpendConstraints_Delete(int BudgetID)
                {
                    PTS_App_Code_VB_Project.PTS_MERCURY.helper.Table_Budget_PlannedToSpendConstraints.Delete(BudgetID);
                }        
        #endregion

            public ActionResult SiteMapHTML()
            {
                return PartialView();
            }

    }
}