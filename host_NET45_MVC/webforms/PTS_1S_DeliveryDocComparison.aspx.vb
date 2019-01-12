Imports System.Data.SqlClient
Imports System.Data

Partial Class PTS_1S_DeliveryDocComparison
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " SELECT TOP 1 [TheLatestUpdate] FROM Db_1S_DeliverySource.[dbo].[Table_TheLatestUpdate] "
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text

            'syntax for parameter adding
            Dim dr As SqlDataReader = cmd.ExecuteReader
            While dr.Read
                LabelTheLAtestUpdate.Text = "The latest update : " + dr(0).ToString
            End While

            con.Close()
            dr.Close()
        End Using

        ' This is not needed anymore, because new design allow us to run query fairly quick
        'If User.IsInRole("DeliveryComparison") Then
        '    ButtonUpdateFromPTSManually.Visible = True
        'Else
        '    ButtonUpdateFromPTSManually.Visible = False
        'End If


    End Sub

    ' This is not needed anymore, because new design allow us to run query fairly quick
    'Protected Sub ButtonUpdateFromPTSManually_Click(sender As Object, e As EventArgs) Handles ButtonUpdateFromPTSManually.Click

    '    Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
    '        con.Open()
    '        Dim sqlstring As String = "SP_Delivery_UpdateIndexesFromPTS"
    '        Dim cmd As New SqlCommand(sqlstring, con)
    '        cmd.CommandType =System.Data.CommandType.StoredProcedure

    '        cmd.ExecuteNonQuery()

    '        con.Close()
    '        con.Dispose()

    '    End Using

    'End Sub

    Protected Sub LnkBtn1_Click(sender As Object, e As EventArgs)

        If ddlProject.SelectedValue <> 0 Then
            RenderReport.Render("excel", ReportViewerPO, "PTS_1S_DeliveryDocComparisonFinalRev", "ProjectID", ddlProject.SelectedValue, "GenerateMissingReport", True, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        Else
            _GiveNotification.Gritter_Error(Page, "Error", "Select Project", "center")
        End If

    End Sub

    Protected Sub LnkBtn2_Click(sender As Object, e As EventArgs)

        If ddlProject.SelectedValue <> 0 Then
            RenderReport.Render("excel", ReportViewerPO, "_Nakl_FollowUpReportBySupplierWithVAT", "ProjectID", ddlProject.SelectedValue, "GenerateMissingReport", True, "HideLink", True, "Currency", "Ruble", Nothing, Nothing, Nothing, Nothing)
        Else
            _GiveNotification.Gritter_Error(Page, "Error", "Select Project", "center")
        End If

    End Sub

    Protected Sub LnkBtn0_Click(sender As Object, e As EventArgs)

        If ddlProject.SelectedValue <> 0 Then
            RenderReport.Render("excel", ReportViewerPO, "PTS_1S_DeliveryDocComparisonFinalRev", "ProjectID", ddlProject.SelectedValue, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        Else
            _GiveNotification.Gritter_Error(Page, "Error", "Select Project", "center")
        End If

    End Sub
End Class
