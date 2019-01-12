using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace host_NET45_MVC.Areas.PtsContractual.Controllers
{
    public class _BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //////////// Translation kontrol kismi

            // bunu buradan sonra kaldir, hata verdigi icin burada
            Session["Translation"] = PTS_App_Code_VB_Project.PTS_MERCURY.helper.BodyTexts.GetTranslation();


            if (Session["ddl_lang"] == null)
            {
                // DEFAULT language ENG
                Session["ddl_lang"] = PTS_App_Code_VB_Project.PTS_MERCURY.helper.BodyTexts.Lang.rus;
            }

            // this should be placed here later. DDL should switch
            //If Not IsPostBack Then
            //    If Session("ddl_lang") IsNot Nothing Then
            //        ddl_lang.SelectedValue = Session("ddl_lang").ToString()
            //    End If
            //End If


            if (Session["CountBodyText"] == null)
            {
                Session["CountBodyText"] = PTS_App_Code_VB_Project.PTS_MERCURY.helper.BodyTexts.CountText();
                IEnumerable<PTS_App_Code_VB_Project.PTS_MERCURY.db.BodyText> Translation = PTS_App_Code_VB_Project.PTS_MERCURY.helper.BodyTexts.GetTranslation();
                Session["Translation"] = Translation;
            }

            if (Session["CountBodyText"] != null)
            {
                try
                {
                    if (PTS_App_Code_VB_Project.PTS_MERCURY.helper.BodyTexts.CountText() > Convert.ToInt32(Session["CountBodyText"]))
                    {
                        IEnumerable<PTS_App_Code_VB_Project.PTS_MERCURY.db.BodyText> Translation = PTS_App_Code_VB_Project.PTS_MERCURY.helper.BodyTexts.GetTranslation();
                        Session["Translation"] = Translation;
                    }

                }
                catch (Exception ex)
                {

                }
            }

            ///////////////// _________________Translation kontrol kismi

        }
    }
}