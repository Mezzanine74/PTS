using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace host_NET45_MVC.App_Code
{
    public class WebFormCommon
    {

        public void processFileManager(System.Web.UI.Page page, Panel panel, string rootFolder, int enableUploadandDelete)
        {
            ASPxFileManager aSPxFileManagerContractAddendums = new ASPxFileManager();
            aSPxFileManagerContractAddendums.ID = "yarrak";
            aSPxFileManagerContractAddendums.Width = 800;
            aSPxFileManagerContractAddendums.Settings.RootFolder = rootFolder;
            aSPxFileManagerContractAddendums.Settings.EnableMultiSelect = true;
            aSPxFileManagerContractAddendums.SettingsFolders.Visible = false;
            aSPxFileManagerContractAddendums.SettingsFileList.View = FileListView.Details;
            aSPxFileManagerContractAddendums.SettingsEditing.AllowDownload = true;
            aSPxFileManagerContractAddendums.SettingsEditing.AllowCopy = true;

            if (enableUploadandDelete == 1)
            {
                aSPxFileManagerContractAddendums.SettingsUpload.Enabled = true;
                aSPxFileManagerContractAddendums.SettingsEditing.AllowDelete = true;
            }
            else
            {
                aSPxFileManagerContractAddendums.SettingsUpload.Enabled = false;
                aSPxFileManagerContractAddendums.SettingsEditing.AllowDelete = false;
            }

            panel.Controls.Add(aSPxFileManagerContractAddendums);
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(page, typeof(System.Web.UI.Page), "alert", "$(function () { $('#ModalFileManager').modal('show') });", true);

        }

    }
}