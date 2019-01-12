Imports PTS_App_Code_VB_Project

Partial Class WebUserControl_ContractEmailBody
    Inherits System.Web.UI.UserControl

    Dim _contractid As Integer = 0

    Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init

        _contractid = PTS_MERCURY.helper.Garbage.GetQueryString(PTS_MERCURY.helper.Garbage.QueryStringParameter.ContractId)

    End Sub

    Protected Sub FormViewContractEmailBody_DataBound(sender As Object, e As EventArgs) Handles FormViewContractEmailBody.DataBound

        Try

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                Dim ContractID = Convert.ToInt32(FormViewContractEmailBody.DataKey.Value)

                If (From C In db.Table_Contracts Where C.ContractID = ContractID).ToList()(0).ProjectID = 999 Then

                    Dim _trClientAdditional As System.Web.UI.HtmlControls.HtmlTableRow = FormViewContractEmailBody.FindControl("ClientAdditionalData")
                    _trClientAdditional.Visible = True
                    Dim TextBoxProjectName As Label = FormViewContractEmailBody.FindControl("TextBoxProjectName")
                    TextBoxProjectName.Text = (From C In db.Table_Contract_ProjectIDforClient Join D In db.Table1_Project On C.ProjectID Equals D.ProjectID Where C.ContractID = ContractID Select New With {D.ProjectName}).ToList()(0).ProjectName

                    Dim LabelSupplierName As Label = FormViewContractEmailBody.FindControl("LabelSupplierName")
                    LabelSupplierName.Text = "Client Name"

                    Dim LabelCompletionDate As Label = FormViewContractEmailBody.FindControl("LabelCompletionDate")
                    Dim LabelAktOfWork As Label = FormViewContractEmailBody.FindControl("LabelAktOfWork")

                    LabelCompletionDate.Text = If((From c In db.Table_Contracts_ClientAdditional Where c.ContractID = ContractID).ToList()(0).CompletionDate.HasValue, (From c In db.Table_Contracts_ClientAdditional Where c.ContractID = ContractID).ToList()(0).CompletionDate, Nothing)
                    LabelAktOfWork.Text = If((From c In db.Table_Contracts_ClientAdditional Where c.ContractID = ContractID).ToList()(0).AktOfWork = True, "YES", "NO")

                End If

            End Using

        Catch ex As Exception

        End Try

        Try
            Dim contractId_sqlsource As Integer = 0
            contractId_sqlsource = SqlDataSourceContractEmailBody.SelectParameters(PTS_MERCURY.helper.Garbage.QueryStringParameter.ContractId).DefaultValue

            If PTS.CoreTables.CreateDataReader.Create_Table1_Project(PTS.CoreTables.CreateDataReader.Create_Table_Contract(contractId_sqlsource).ProjectID).NewGeneration = False Then

                Dim LabelContractValueTitle As Label = FormViewContractEmailBody.FindControl("LabelInsert6")
                Dim TextBoxContractValue_withVAT As Label = FormViewContractEmailBody.FindControl("TextBoxContractValue_withVAT")
                Dim TextBoxVAT As Label = FormViewContractEmailBody.FindControl("TextBoxVAT")

                LabelContractValueTitle.Text = "Contract Value Exc. VAT"
                TextBoxContractValue_withVAT.Text = String.Format("{0:N2}", PTS.CoreTables.CreateDataReader.Create_Table_Contract(contractId_sqlsource).ContractValue_woVAT)
                TextBoxVAT.Text = "18"

            End If

            Dim SupplierID As String = PTS.CoreTables.CreateDataReader.Create_Table_Contract(contractId_sqlsource).SupplierID
            Dim ProjectID As Integer = PTS.CoreTables.CreateDataReader.Create_Table_Contract(contractId_sqlsource).ProjectID
            Dim panelNewSupplier As Panel = FormViewContractEmailBody.FindControl("panelNewSupplier")

            Using adapter As New ApprovalMatrixTableAdapters.QueriesTableAdapter

                If adapter.GetTotalPoNumberUnderThisSupplier(SupplierID) = 0 And ProjectID <> 999 Then
                    panelNewSupplier.Visible = True
                Else
                    panelNewSupplier.Visible = False
                End If

                adapter.Dispose()

            End Using

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

    Protected Sub FormViewContractEmailBody_ItemCommand(sender As Object, e As FormViewCommandEventArgs) Handles FormViewContractEmailBody.ItemCommand

        If (e.CommandName = "OpenBudgetPDF") Then
            Dim LinkToFile As String = e.CommandArgument.ToString
            Dim openpdf As New MyCommonTasks
            openpdf.OpenPDF(LinkToFile)
        End If


    End Sub
End Class
