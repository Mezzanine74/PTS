Imports System
Imports System.IO
Imports System.Net
Imports System.Data.SqlClient


Partial Class contractenter_2IASDFK
    Inherits System.Web.UI.Page

    Dim notification As New _GiveNotification

    Protected Sub SqlDataSourceSupplierEnter_Inserting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceCommandEventArgs)


    End Sub

    Protected Sub DropDownListPrj_DataBound(ByVal sender As Object, ByVal e As System.EventArgs)
        If Not IsPostBack Then
            Dim lst1 As New ListItem("Select Project", "0")
            Dim DropDownListPrj As DropDownList = FormViewContract.FindControl("DropDownListPrj")
            DropDownListPrj.Items.Insert(0, lst1)
        End If
    End Sub

    Protected Sub x_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim DrpSplr As DropDownList = FormViewContract.FindControl("DropLspLr")
        Dim LblShwSplr1 As Label = FormViewContract.FindControl("LabelShowSupplier")
        Dim TextBoxSupplierID As TextBox = FormViewContract.FindControl("x")

        TextBoxSupplierID.Text = Left(TextBoxSupplierID.Text.ToString, 12)

        DrpSplr.DataBind()
        LblShwSplr1.Text = DrpSplr.SelectedValue.ToString

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("DuBakali") = "YES" Then

            notification._GiveNotification(HttpContext.Current.Request.Headers("X-Requested-With"), _
                       Page, "<p style=" + """" + "text-align: center;font-weight: bold" + """" + _
                       ">You entered contract successfully!</p>")
            Session.Remove("DuBakali")
        End If

        Dim SqlDataSourcePrj As SqlDataSource = FormViewContract.FindControl("SqlDataSourcePrj")
        SqlDataSourcePrj.SelectParameters("username").DefaultValue = Page.User.Identity.Name.ToLower

        If Not IsPostBack Or IsPostBack Then
            ' it adds jscript into insert button
            Dim FormViewSupplier As FormView = FormViewContract.FindControl("FormViewSupplier")
            Dim Inst As LinkButton = FormViewSupplier.FindControl("InsertButton")

            'Inst.Attributes.Add("onclick", "return SupplierInsert();")

            'Dim checkBoxClient As CheckBox = FormViewSupplier.FindControl("CheckBoxClient")
            'checkBoxClient.Attributes.Add("onclick", "ClickEnable();")
        End If

        ' This part cancelled.
        'If IsPostBack Or Not IsPostBack Then
        'Dim FormViewSupplier As FormView = FormViewContract.FindControl("FormViewSupplier")
        'Dim TextBoxSaveNextClientID As TextBox = FormViewSupplier.FindControl("TextBoxSaveNextClientID")
        'TextBoxSaveNextClientID.Text = GetTheLastClientID()
        'End If

        If Not IsPostBack Then
            Dim DrpProjec As DropDownList = FormViewContract.FindControl("DropDownListPrj")
            DrpProjec.Focus()
        End If

    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender

        If IsPostBack Then

            Dim FrmViewSupplier As FormView = FormViewContract.FindControl("FormViewSupplier")
            Dim aBtn As Button = FormViewContract.FindControl("ButtonEnterSupplier")

            If FrmViewSupplier.Visible = True Then
                aBtn.Text = "Hide Supplier Window"
            ElseIf FrmViewSupplier.Visible = False Then
                aBtn.Text = "Add New Supplier"
            End If
        End If

        If IsPostBack Then
            Dim controll As New String(Page.Request.Params.Get("__EVENTTARGET"))
            If (Not controll Is Nothing) Or (controll <> "") Then
                If controll = "ctl00$MainContent$FormViewContract$x" Then

                    Dim LblShwSplr As Label = FormViewContract.FindControl("LabelShowSupplier")
                    Dim LabelErr As Label = FormViewContract.FindControl("LabelSplrError")
                    Dim TxtSupplier As TextBox = FormViewContract.FindControl("x")

                    If LblShwSplr.Text = "" Then
                        LabelErr.Text = "INN Number is not 10 digit or Supplier does not exist"
                        TxtSupplier.BackColor = System.Drawing.Color.Yellow
                    ElseIf LblShwSplr.Text <> "" Then
                        LabelErr.Text = ""
                        TxtSupplier.BackColor = System.Drawing.Color.White
                    End If
                End If
            End If
        End If

    End Sub

    Protected Sub ButtonEnterSupplier_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim FrmViewSupplier As FormView = FormViewContract.FindControl("FormViewSupplier")
        Dim Btn As Button = FormViewContract.FindControl("ButtonEnterSupplier")

        If Btn.Text = "Add New Supplier" Then
            FrmViewSupplier.Visible = True
            Dim SupplierIDTextBox As TextBox = FrmViewSupplier.FindControl("SupplierIDTextBox")
            Dim SupplierNameTextBox As TextBox = FrmViewSupplier.FindControl("SupplierNameTextBox")
            'Dim CheckBoxClient As CheckBox = FrmViewSupplier.FindControl("CheckBoxClient")
            Dim VAT_FreeCheckBox2 As CheckBox = FrmViewSupplier.FindControl("VAT_FreeCheckBox2")

            SupplierIDTextBox.Text = ""
            SupplierNameTextBox.Text = ""
            'CheckBoxClient.Checked = False
            VAT_FreeCheckBox2.Checked = False

        End If

        If Btn.Text = "Hide Supplier Window" Then
            FrmViewSupplier.Visible = False
            Btn.Text = "Add New Supplier"
        End If

    End Sub

    Protected Sub SqlDataSourceSupplierEnter_Inserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs)
        Dim command As System.Data.Common.DbCommand = e.Command
        Dim FrmViewSupplier As FormView = FormViewContract.FindControl("FormViewSupplier")
        Dim TextBoxSupplier As TextBox = FormViewContract.FindControl("x")

        Dim DrpSplr As DropDownList = FormViewContract.FindControl("DropLspLr")
        Dim LblShwSplr As Label = FormViewContract.FindControl("LabelShowSupplier")
        Dim LabelSplrError As Label = FormViewContract.FindControl("LabelSplrError")


        FrmViewSupplier.Visible = False
        TextBoxSupplier.Text = command.Parameters("@SupplierID").Value
        TextBoxSupplier.BackColor = System.Drawing.Color.White

        DrpSplr.DataBind()
        LblShwSplr.Text = DrpSplr.SelectedValue
        LabelSplrError.Text = ""

    End Sub

    Protected Sub InsertButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

    Protected Sub FormViewContract_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles FormViewContract.DataBound


    End Sub

    Protected Sub FormViewContract_ItemInserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewInsertedEventArgs) Handles FormViewContract.ItemInserted
        LabelEntryMessage.Visible = True
        LabelProjectSupplierComtability.Visible = False

        'Dim DropDownListPrj As DropDownList = FormViewContract.FindControl("DropDownListPrj")
        'DropDownListPrj.DataBind()

        'FormViewContract.DataBind()

        Session("duBakali") = "YES"

        Response.Redirect("~/webforms/contractenter.aspx")

    End Sub

    Protected Sub FormViewContract_ItemInserting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewInsertEventArgs) Handles FormViewContract.ItemInserting

        Dim LabelSplrError As Label = FormViewContract.FindControl("LabelSplrError")
        If LabelSplrError.Text = "INN Number is not 10 digit or Supplier does not exist" Then
            e.Cancel = True
        Else
            Dim DropDownListPrj As DropDownList = FormViewContract.FindControl("DropDownListPrj")
            Dim TextBoxSupplierID As TextBox = FormViewContract.FindControl("x")

            '/* CAN BE ACTIVATED LATER.
            ' check if client and supplier are compatible
            'If Not ClientAndSupplierIDCompatible(DropDownListPrj.SelectedValue, TextBoxSupplierID.Text) Then
            'LabelProjectSupplierComtability.Visible = True
            'e.Cancel = True
            'Exit Sub
            'End If
            ' ____________________________

            e.Cancel = False

            ' update attachmentExist column
            Dim TextLink As TextBox = FormViewContract.FindControl("LinkToPDFcopyTextBox")
            If Len(TextLink.Text) > 0 OrElse Not String.IsNullOrEmpty(TextLink.Text) Then
                e.Values("AttachmentExist") = True
            Else
                e.Values("AttachmentExist") = False
            End If

            Dim ContractValue_woVATTextBox As TextBox = FormViewContract.FindControl("ContractValue_woVATTextBox")
            Dim DropDownListCurrency As DropDownList = FormViewContract.FindControl("DropDownListCurrency")
            Dim DropDownListContractType As DropDownList = FormViewContract.FindControl("DropDownListContractType")
            Dim DropDownListRetention As DropDownList = FormViewContract.FindControl("DropDownListRetention")
            Dim TextBoxRetention As TextBox = FormViewContract.FindControl("TextBoxRetention")

            Dim ContractDateTextBox As TextBox = FormViewContract.FindControl("ContractDateTextBox")

            e.Values("ProjectID") = DropDownListPrj.SelectedValue.ToString

            If ContractValue_woVATTextBox.Text <> "" Then
                e.Values("ContractValue_woVAT") = Convert.ToDecimal(ContractValue_woVATTextBox.Text)
            ElseIf ContractValue_woVATTextBox.Text = "" Then
                e.Values("ContractValue_woVAT") = Nothing
            End If

            e.Values("ContractCurrency") = DropDownListCurrency.SelectedValue.ToString
            e.Values("ContractType") = DropDownListContractType.SelectedValue.ToString
            If ContractDateTextBox.Text <> "" Then
                e.Values("ContractDate") = Convert.ToDateTime(Mid(ContractDateTextBox.Text.ToString, 1, 2).ToString + "/" + Mid(ContractDateTextBox.Text.ToString, 4, 2).ToString + "/" + Mid(ContractDateTextBox.Text.ToString, 7, 4).ToString)
            ElseIf ContractDateTextBox.Text = "" Then
                e.Values("ContractDate") = Nothing
            End If
            'e.Values("Retention") = DropDownListRetention.SelectedValue
            e.Values("Retention") = TextBoxRetention.Text.Trim

            Dim zoneId As String = "Russian Standard Time"
            Dim tzi As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(zoneId)
            Dim result As DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi)

            e.Values("CreatedBy") = result
            e.Values("PersonCreated") = Page.User.Identity.Name.ToString
        End If
    End Sub

    Protected Function ClientAndSupplierIDCompatible(ByVal ProjectID As Integer, ByVal SupplierID As String) As Boolean
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " SELECT     Client " +
                                                    " FROM         dbo.Table6_Supplier " +
                                                    " WHERE     (SupplierID = @SupplierID) "

            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text

            'syntax for parameter adding
            Dim SupplierIDParameter As SqlParameter = cmd.Parameters.Add("@SupplierID", System.Data.SqlDbType.NVarChar, 12)
            SupplierIDParameter.Value = SupplierID
            Dim dr As SqlDataReader = cmd.ExecuteReader
            Dim IsCompatible As Boolean = False
            While dr.Read
                If SupplierID = "0000000000" Then
                    IsCompatible = True
                Else
                    If dr(0) = True Then
                        If ProjectID = 999 Then
                            IsCompatible = True
                        Else
                            IsCompatible = False
                        End If
                    ElseIf dr(0) = False Then
                        If ProjectID = 999 Then
                            IsCompatible = False
                        Else
                            IsCompatible = True
                        End If
                    End If
                End If
            End While
            con.Close()
            con.Dispose()
            dr.Close()
            Return IsCompatible
        End Using

    End Function

    Protected Sub ButtonUploadDOC_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim FileToUpload As FileUpload = FormViewContract.FindControl("FileUploadDOC")
        Dim DropProject As DropDownList = FormViewContract.FindControl("DropDownListPrj")
        'Dim TxtInvoiceID As TextBox = FormViewContract.FindControl("InvoiceIDTextBox")
        Dim LabelInfo As Label = FormViewContract.FindControl("LabelInfoDOC")
        Dim TextLink As TextBox = FormViewContract.FindControl("LinkToTemplatefile_DOCTextBox")

        If DropProject.SelectedItem.ToString = "Select Project" Then
            LabelInfo.ForeColor = System.Drawing.Color.Red
            LabelInfo.Text = "Please select Project Name"
            TextLink.Text = ""
        Else
            If FileToUpload.HasFile Then
                If System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName) = ".doc" OrElse System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName) = ".docx" _
                    OrElse System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName) = ".zip" Then
                    'If FileToUpload.PostedFile.ContentLength / 1000 > 5000 Then
                    '    LabelInfo.ForeColor = System.Drawing.Color.Red
                    '    LabelInfo.Text = "PDF file size must be less than 5MB"
                    '    TextLink.Text = ""
                    'Else
                    If Directory.Exists(Server.MapPath("~/CONTRACT/") + DropProject.SelectedItem.ToString + "/") Then
                        Dim UniqueString11 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
                        Dim UniqueString12 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
                        FileToUpload.SaveAs(MapPath("~/CONTRACT/" + DropProject.SelectedItem.ToString + "/" + UniqueString11 + UniqueString12 + System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName)))
                        TextLink.Text = "~/CONTRACT/" + DropProject.SelectedItem.ToString + "/" + UniqueString11 + UniqueString12 + System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName)
                        LabelInfo.ForeColor = System.Drawing.Color.DarkGreen
                        LabelInfo.Text = FileToUpload.PostedFile.FileName + " has been loaded successfully"
                    Else
                        Dim UniqueString21 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
                        Dim UniqueString22 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
                        Directory.CreateDirectory(Server.MapPath("~/CONTRACT/") + DropProject.SelectedItem.ToString)
                        FileToUpload.SaveAs(MapPath("~/CONTRACT/" + DropProject.SelectedItem.ToString + "/" + UniqueString21 + UniqueString22 + System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName)))
                        TextLink.Text = "~/CONTRACT/" + DropProject.SelectedItem.ToString + "/" + UniqueString21 + UniqueString22 + System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName)
                        LabelInfo.ForeColor = System.Drawing.Color.DarkGreen
                        LabelInfo.Text = FileToUpload.PostedFile.FileName + " has been loaded successfully"
                    End If
                    'End If
                Else
                    LabelInfo.ForeColor = System.Drawing.Color.Red
                    LabelInfo.Text = "Please select MS Word or ZIP format file"
                    TextLink.Text = ""
                End If
            Else
                TextLink.Text = ""
                LabelInfo.ForeColor = System.Drawing.Color.Red
                LabelInfo.Text = "you did not specify any file"
            End If
        End If
    End Sub

    Protected Sub ButtonUploadPDF_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim FileToUpload As FileUpload = FormViewContract.FindControl("FileUploadPDF")
        Dim DropProject As DropDownList = FormViewContract.FindControl("DropDownListPrj")
        'Dim TxtInvoiceID As TextBox = FormViewContract.FindControl("InvoiceIDTextBox")
        Dim LabelInfo As Label = FormViewContract.FindControl("LabelInfoPDF")
        Dim TextLink As TextBox = FormViewContract.FindControl("LinkToPDFcopyTextBox")

        If DropProject.SelectedItem.ToString = "Select Project" Then
            LabelInfo.ForeColor = System.Drawing.Color.Red
            LabelInfo.Text = "Please select Project Name"
            TextLink.Text = ""
        Else
            If FileToUpload.HasFile Then
                If System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName) = ".pdf" _
                  OrElse System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName) = ".doc" _
                  OrElse System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName) = ".docx" Then
                    'If FileToUpload.PostedFile.ContentLength / 1000 > 5000 Then
                    '    LabelInfo.ForeColor = System.Drawing.Color.Red
                    '    LabelInfo.Text = "PDF file size must be less than 5MB"
                    '    TextLink.Text = ""
                    'Else
                    If Directory.Exists(Server.MapPath("~/CONTRACT/") + DropProject.SelectedItem.ToString + "/") Then
                        Dim UniqueString11 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
                        Dim UniqueString12 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
                        FileToUpload.SaveAs(MapPath("~/CONTRACT/" + DropProject.SelectedItem.ToString + "/" + UniqueString11 + UniqueString12 + System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName)))
                        TextLink.Text = "~/CONTRACT/" + DropProject.SelectedItem.ToString + "/" + UniqueString11 + UniqueString12 + System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName)
                        LabelInfo.ForeColor = System.Drawing.Color.DarkGreen
                        LabelInfo.Text = FileToUpload.PostedFile.FileName + " has been loaded successfully"
                    Else
                        Dim UniqueString21 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
                        Dim UniqueString22 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
                        Directory.CreateDirectory(Server.MapPath("~/CONTRACT/") + DropProject.SelectedItem.ToString)
                        FileToUpload.SaveAs(MapPath("~/CONTRACT/" + DropProject.SelectedItem.ToString + "/" + UniqueString21 + UniqueString22 + System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName)))
                        TextLink.Text = "~/CONTRACT/" + DropProject.SelectedItem.ToString + "/" + UniqueString21 + UniqueString22 + System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName)
                        LabelInfo.ForeColor = System.Drawing.Color.DarkGreen
                        LabelInfo.Text = FileToUpload.PostedFile.FileName + " has been loaded successfully"
                    End If
                    'End If
                Else
                    LabelInfo.ForeColor = System.Drawing.Color.Red
                    LabelInfo.Text = "Please select PDF, DOC or DOCX format file"
                    TextLink.Text = ""
                End If
            Else
                TextLink.Text = ""
                LabelInfo.ForeColor = System.Drawing.Color.Red
                LabelInfo.Text = "you did not specify any file"
            End If
        End If
    End Sub

    Protected Sub FormViewContract_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles FormViewContract.Load
        If IsPostBack Or Not IsPostBack Then
            ' it cause button to postback. 
            Dim ButtonUploadDOC As Button = FormViewContract.FindControl("ButtonUploadDOC")
            Dim ButtonUploadPDF As Button = FormViewContract.FindControl("ButtonUploadPDF")
            Dim DropDownListPrj As DropDownList = FormViewContract.FindControl("DropDownListPrj")

        End If
    End Sub

    Protected Function GetTheLastClientID() As String
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain

            con.Open()
            Dim sqlstring As String = " SELECT     TOP (1) SupplierID " +
                                                      " FROM         dbo.Table6_Supplier " +
                                                      " WHERE     (Client = 1) " +
                                                      " ORDER BY SupplierID DESC "
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text
            'syntax for parameter adding
            Dim dr As SqlDataReader = cmd.ExecuteReader
            Dim TheLastClientID As String = ""
            While dr.Read
                TheLastClientID = dr(0).ToString
            End While
            con.Close()
            con.Dispose()
            dr.Close()

            Dim TheNextClientID As String = ""
            For i = 1 To Len(TheLastClientID)
                If Mid(TheLastClientID, i, 1) <> "0" Then
                    TheNextClientID = Convert.ToString((Convert.ToInt32(Mid(TheLastClientID, i, Len(TheLastClientID) - i + 1)) + 1))
                    Dim tt As String = ""
                    For j = 1 To i - 1
                        tt = tt + "0"
                    Next
                    TheNextClientID = tt + TheNextClientID
                    Exit For
                End If
            Next
            Return TheNextClientID

        End Using

    End Function

    Protected Sub DropDownListPrj_SelectedIndexChanged(sender As Object, e As System.EventArgs)
        Dim DropDownListPrj As DropDownList = sender
        Dim LabelWarrantyPeriod As Label = FormViewContract.FindControl("LabelWarrantyPeriod")
        Dim TextBoxWarrantyPeriod As TextBox = FormViewContract.FindControl("TextBoxWarrantyPeriod")

        Dim LabelProjectOnPTS As Label = FormViewContract.FindControl("LabelProjectOnPTS")
        Dim DropDownListProjectOnPTS As DropDownList = FormViewContract.FindControl("DropDownListProjectOnPTS")
        Dim CompareValidatorProjectOnPTS As CompareValidator = FormViewContract.FindControl("CompareValidatorProjectOnPTS")

        If DropDownListPrj.SelectedValue = 999 Then
            LabelWarrantyPeriod.Visible = True
            TextBoxWarrantyPeriod.Visible = True

            Dim SqlDataSourceProjectOnPTS As SqlDataSource = FormViewContract.FindControl("SqlDataSourceProjectOnPTS")
            SqlDataSourceProjectOnPTS.SelectParameters("username").DefaultValue = Page.User.Identity.Name.ToLower

            LabelProjectOnPTS.Visible = True
            DropDownListProjectOnPTS.Visible = True
            CompareValidatorProjectOnPTS.Enabled = True

            ' activate Client panelClientAddData
            Dim panelClientAddData As Panel = FormViewContract.FindControl("panelClientAddData")
            panelClientAddData.Visible = True

        Else
            LabelWarrantyPeriod.Visible = False
            TextBoxWarrantyPeriod.Visible = False

            LabelProjectOnPTS.Visible = False
            DropDownListProjectOnPTS.Visible = False
            CompareValidatorProjectOnPTS.Enabled = False

            ' deactivate Client panelClientAddData
            Dim panelClientAddData As Panel = FormViewContract.FindControl("panelClientAddData")
            panelClientAddData.Visible = False

        End If

    End Sub

    Protected Sub SqlDataSourceContract_Inserted(sender As Object, e As SqlDataSourceStatusEventArgs)

        Dim NewID As Integer = e.Command.Parameters("@id").Value
        Dim DropDownListProjectOnPTS As DropDownList = FormViewContract.FindControl("DropDownListProjectOnPTS")
        Dim DropDownListPrj As DropDownList = FormViewContract.FindControl("DropDownListPrj")

        If DropDownListPrj.SelectedValue = 999 Then

            Using Adapter As New CertificationTableAdapters.Table_Contract_ProjectIDforClientTableAdapter

                Dim table As New Certification.Table_Contract_ProjectIDforClientDataTable
                table = Adapter.GetDataByContractID(NewID)

                If table.Rows.Count = 0 Then

                    Adapter.Insert(NewID, Convert.ToInt32(DropDownListProjectOnPTS.SelectedValue))
                Else

                    Adapter.Update(Convert.ToInt32(DropDownListProjectOnPTS.SelectedValue), NewID)
                End If

                table = Nothing

            End Using
        End If

    End Sub

    Protected Sub FormViewSupplier_ItemInserting(sender As Object, e As FormViewInsertEventArgs)

        Dim FormViewSupplier As FormView = FormViewContract.FindControl("FormViewSupplier")
        Dim SupplierIDTextBox As TextBox = FormViewSupplier.FindControl("SupplierIDTextBox")
        Dim LabelSupplierMessage As Label = FormViewSupplier.FindControl("LabelSupplierMessage")
        Dim sqlsupplier As SqlDataSource = FormViewContract.FindControl("SqlDataSourceSupplierEnter")

        Using Adapter As New MercuryTableAdapters.Table6_SupplierTableAdapter
            If Adapter.GetCountBySupplierID(SupplierIDTextBox.Text) > 0 Then
                ' give message that it is dublicating
                LabelSupplierMessage.Visible = True
                e.Cancel = True
            End If

            Adapter.Dispose()

        End Using

    End Sub

    Protected Sub CheckBoxPersonSupplier_CheckedChanged(sender As Object, e As EventArgs)

        Dim FormViewSupplier As FormView = FormViewContract.FindControl("FormViewSupplier")
        Dim RegularExpressionValidatorSupplierEnter As RegularExpressionValidator = FormViewSupplier.FindControl("RegularExpressionValidatorSupplierEnter")

        Dim cbx As CheckBox = sender

        If cbx.Checked Then
            RegularExpressionValidatorSupplierEnter.ValidationExpression = "\d{12}"
            RegularExpressionValidatorSupplierEnter.ErrorMessage = "INN number to be 12 digit!"
        Else
            RegularExpressionValidatorSupplierEnter.ValidationExpression = "\d{10}"
            RegularExpressionValidatorSupplierEnter.ErrorMessage = "INN number to be 10 digit!"
        End If

        RegularExpressionValidatorSupplierEnter.Validate()

    End Sub

    Protected Sub InsertButton_Click1(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim zoneId As String = "Russian Standard Time"
        Dim tzi As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(zoneId)
        Dim result As DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi)

        Dim sqlsupplier As SqlDataSource = FormViewContract.FindControl("SqlDataSourceSupplierEnter")
        sqlsupplier.InsertParameters("CreatedBy").DefaultValue = result.ToString
        sqlsupplier.InsertParameters("CreatedBy").Type = TypeCode.DateTime

        sqlsupplier.InsertParameters("PersonCreated").DefaultValue = Page.User.Identity.Name.ToString
        sqlsupplier.InsertParameters("PersonCreated").Type = TypeCode.String

    End Sub

    Protected Sub CheckBoxPenaltyToMercury_CheckedChanged(sender As Object, e As EventArgs)

        Dim cbx As CheckBox = sender

        Dim RequiredFieldValidatorPenaltyMercuryNote As RequiredFieldValidator = FormViewContract.FindControl("RequiredFieldValidatorPenaltyMercuryNote")
        If cbx.Checked = True Then
            RequiredFieldValidatorPenaltyMercuryNote.Enabled = True
        Else
            RequiredFieldValidatorPenaltyMercuryNote.Enabled = False
        End If

    End Sub
End Class
