Imports PTS_App_Code_VB_Project

Partial Class WebUserControl_AddendumEmailBody
    Inherits System.Web.UI.UserControl

    Dim _addendumid As Integer = 0

    Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init

        _addendumid = PTS_MERCURY.helper.Garbage.GetQueryString(PTS_MERCURY.helper.Garbage.QueryStringParameter.AddendumId)

    End Sub

    Protected Sub FormViewAddendumsEmailBody_DataBound(sender As Object, e As EventArgs) Handles FormViewAddendumsEmailBody.DataBound

        Try

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                Dim AddendumID = Convert.ToInt32(FormViewAddendumsEmailBody.DataKey.Value)
                Dim ContractID = PTS_MERCURY.helper.Table_Addendums.GetRowByPrimaryKey(AddendumID).ContractID

                If (From C In db.Table_Addendums Join D In db.Table_Contracts On C.ContractID Equals D.ContractID Where C.AddendumID = AddendumID Select New With {D.ProjectID}).ToList()(0).ProjectID = 999 Then

                    Dim _trClientAdditional As System.Web.UI.HtmlControls.HtmlTableRow = FormViewAddendumsEmailBody.FindControl("ClientAdditionalData")
                    _trClientAdditional.Visible = True
                    Dim TextBoxProjectName As Label = FormViewAddendumsEmailBody.FindControl("TextBoxProjectName")
                    TextBoxProjectName.Text = (From C In db.Table_Addendums Join F In db.Table_Contract_ProjectIDforClient On C.ContractID Equals F.ContractID Join G In db.Table1_Project On F.ProjectID Equals G.ProjectID Where C.ContractID = ContractID Select New With {G.ProjectName}).ToList()(0).ProjectName

                    Dim LabelSupplier As Label = FormViewAddendumsEmailBody.FindControl("LabelSupplier")
                    LabelSupplier.Text = "Client Name"

                    Dim LabelCompletionDate As Label = FormViewAddendumsEmailBody.FindControl("LabelCompletionDate")
                    Dim LabelAktOfWork As Label = FormViewAddendumsEmailBody.FindControl("LabelAktOfWork")

                    LabelCompletionDate.Text = If((From c In db.Table_Addendums_ClientAdditional Where c.AddendumID = AddendumID).ToList()(0).CompletionDate.HasValue, (From c In db.Table_Addendums_ClientAdditional Where c.AddendumID = AddendumID).ToList()(0).CompletionDate, Nothing)
                    LabelAktOfWork.Text = If((From c In db.Table_Addendums_ClientAdditional Where c.AddendumID = AddendumID).ToList()(0).AktOfWork = True, "YES", "NO")

                End If

            End Using

        Catch ex As Exception

        End Try

        Try
            Dim contractId_sqlsource As Integer = 0
            contractId_sqlsource = SqlDataSourceAddendumsEmailBody.SelectParameters(PTS_MERCURY.helper.Garbage.QueryStringParameter.AddendumId).DefaultValue

            If PTS.CoreTables.CreateDataReader.Create_Table1_Project(PTS.CoreTables.CreateDataReader.Create_Table_Contract(PTS.CoreTables.CreateDataReader.Create_Table_Addendums(contractId_sqlsource).ContractID).ProjectID).NewGeneration = False Then

                Dim LabelAddendumValue_WithVAT As Label = FormViewAddendumsEmailBody.FindControl("LabelAddendumValue_WithVAT")
                Dim TextBoxAddendumValue_withVAT As Label = FormViewAddendumsEmailBody.FindControl("TextBoxAddendumValue_withVAT")
                Dim TextBoxVAT As Label = FormViewAddendumsEmailBody.FindControl("TextBoxVAT")

                If LabelAddendumValue_WithVAT IsNot Nothing Then
                    LabelAddendumValue_WithVAT.Text = "Addendum Value Exc. VAT"
                    TextBoxAddendumValue_withVAT.Text = String.Format("{0:###,###,###.00}", PTS.CoreTables.CreateDataReader.Create_Table_Addendums(contractId_sqlsource).AddendumValue_woVAT)
                    TextBoxVAT.Text = "18"

                    Dim LabelInsert22 As Label = FormViewAddendumsEmailBody.FindControl("LabelInsert22")
                    Dim LabelContractValue As Label = FormViewAddendumsEmailBody.FindControl("LabelContractValue")

                    LabelInsert22.Text = "Contract Value Exc. VAT"
                    LabelContractValue.Text = String.Format("{0:###,###,###.00}", PTS.CoreTables.CreateDataReader.Create_Table_Contract(PTS.CoreTables.CreateDataReader.Create_Table_Addendums(contractId_sqlsource).ContractID).ContractValue_woVAT)
                End If

            End If

        Catch ex As Exception

        End Try

        Dim _frmview As FormView = sender

        Dim ImageButtonBudgetPDF As ImageButton = _frmview.FindControl("ImageButtonBudgetPDF")

        If ImageButtonBudgetPDF IsNot Nothing Then

            If System.IO.File.Exists(Server.MapPath(DataBinder.Eval(_frmview.DataItem, "BudgetLinkToPDF").ToString)) Then
                If Right(DataBinder.Eval(_frmview.DataItem, "BudgetLinkToPDF").ToString, 3).ToString = "pdf" Then
                    ImageButtonBudgetPDF.ImageUrl = "~/Images/pdf.bmp"
                ElseIf Right(DataBinder.Eval(_frmview.DataItem, "BudgetLinkToPDF").ToString, 3).ToString = "doc" Then
                    ImageButtonBudgetPDF.ImageUrl = "~/Images/Word.jpg"
                ElseIf Right(DataBinder.Eval(_frmview.DataItem, "BudgetLinkToPDF").ToString, 4).ToString = "docx" Then
                    ImageButtonBudgetPDF.ImageUrl = "~/Images/Word.jpg"
                End If
            Else
                ImageButtonBudgetPDF.Visible = False

            End If

        End If

    End Sub

    Protected Sub FormViewAddendumsEmailBody_ItemCommand(sender As Object, e As FormViewCommandEventArgs) Handles FormViewAddendumsEmailBody.ItemCommand

        If (e.CommandName = "OpenBudgetPDF") Then
            Dim LinkToFile As String = e.CommandArgument.ToString
            Dim openpdf As New MyCommonTasks
            openpdf.OpenPDF(LinkToFile)
        End If

    End Sub
End Class
