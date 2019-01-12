
Partial Class ProgressMeasurementMonitor
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        showReport()

    End Sub

    Protected Sub ButtonSeeAgainstCost_Click(sender As Object, e As System.EventArgs) Handles ButtonSeeAgainstCost.Click

        LabelCount.Text = Convert.ToString(Convert.ToInt32(LabelCount.Text) + 1)

        showReport()

    End Sub

    Protected Sub showReport()

        If Convert.ToInt32(LabelCount.Text) Mod 2 = 0 Then
            RenderReport.Render("HTML", ReportViewerSubPoList, "ProgressMeasurementMonitor", "ProjectID", Request.QueryString("ProjectID"))
            ButtonSeeAgainstCost.Text = "See Against Cost"
        Else
            RenderReport.Render("HTML", ReportViewerSubPoList, "ProgressMeasurementMonitorCost", "ProjectID", Request.QueryString("ProjectID"))
            ButtonSeeAgainstCost.Text = "See Against Validated BOQ"
        End If
    End Sub

End Class
