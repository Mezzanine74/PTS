Imports System
Imports System.IO
Imports System.Net
Imports System.Data.SqlClient
Imports System.Net.Mail

Partial Class addendumenter_2
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ' it transfer previous page onto here...
        If Not IsPostBack Then
            If Not PreviousPage Is Nothing Then
                Dim previousPageURL As String = Request.UrlReferrer.AbsolutePath.ToString
                Dim PageFileInfo As New System.IO.FileInfo(previousPageURL)
                If PageFileInfo.Name = "Contractview.aspx" OrElse PageFileInfo.Name = "contractview.aspx" Then
                    Dim ContPlaceHold As ContentPlaceHolder = PreviousPage.Master.FindControl("MainContent")
                    Dim LabelProjectName As Label = ContPlaceHold.FindControl("LabelProjectName")
                    Dim LabelPOno As Label = ContPlaceHold.FindControl("LabelPOno")
                    Dim LabelSupplierName As Label = ContPlaceHold.FindControl("LabelSupplierNameV")
                    Dim LabelContractNo As Label = ContPlaceHold.FindControl("LabelContractNo")
                    Dim LabelContractDate As Label = ContPlaceHold.FindControl("LabelContractDate")
                    Dim LabelContractValue As Label = ContPlaceHold.FindControl("LabelContractValue")
                    Dim LabelContractCurrency As Label = ContPlaceHold.FindControl("LabelContractCurrencyV")
                    Dim LabelContractType As Label = ContPlaceHold.FindControl("LabelContractType")
                    Dim LabelContractDescription As Label = ContPlaceHold.FindControl("LabelContractDescription")
                    Dim LabelContractID As Label = ContPlaceHold.FindControl("LabelContractID")
                    Dim LabelGridViewPagingStatusOnContractView As Label = ContPlaceHold.FindControl("LabelGridViewPagingStatus")
                    Dim LabelGridViewPageSizeOnContractView As Label = ContPlaceHold.FindControl("LabelGridViewPageSize")
                    Dim LabelGridViewPageNumberOnContractView As Label = ContPlaceHold.FindControl("LabelGridViewPageNumber")

                    Dim LabelProjectNameInFormView As Label = FormViewAddendums.FindControl("LabelProjectName")
                    Dim LabelPOnoInFormView As Label = FormViewAddendums.FindControl("LabelPOno")
                    Dim LabelSupplierNameInFormView As Label = FormViewAddendums.FindControl("LabelSupplierName")
                    Dim LabelContractNameInFormView As Label = FormViewAddendums.FindControl("LabelContractName")
                    Dim LabelContractDateInFormView As Label = FormViewAddendums.FindControl("LabelContractDate")
                    Dim LabelContractValueInFormView As Label = FormViewAddendums.FindControl("LabelContractValue")
                    Dim LabelCurrencyInFormView As Label = FormViewAddendums.FindControl("LabelCurrency")
                    Dim LabelContractTypeInFormView As Label = FormViewAddendums.FindControl("LabelContractType")
                    Dim LabelContractDescriptionInFormView As Label = FormViewAddendums.FindControl("LabelContractDescription")
                    Dim LabelWarrantyPeriod As Label = FormViewAddendums.FindControl("LabelWarrantyPeriod")
                    Dim TextBoxWarrantyPeriod As TextBox = FormViewAddendums.FindControl("TextBoxWarrantyPeriod")

                    LabelProjectNameInFormView.Text = LabelProjectName.Text
                    LabelPOnoInFormView.Text = LabelPOno.Text
                    LabelSupplierNameInFormView.Text = LabelSupplierName.Text
                    LabelContractNameInFormView.Text = LabelContractNo.Text
                    LabelContractDateInFormView.Text = LabelContractDate.Text
                    LabelContractValueInFormView.Text = LabelContractValue.Text
                    LabelCurrencyInFormView.Text = LabelContractCurrency.Text
                    LabelContractTypeInFormView.Text = LabelContractType.Text
                    LabelContractDescriptionInFormView.Text = LabelContractDescription.Text
                    LabelContractIDonAddendum.Text = LabelContractID.Text
                    LabelGridViewPagingStatusOnAddendum.Text = LabelGridViewPagingStatusOnContractView.Text
                    LabelGridViewPageSizeOnAddendum.Text = LabelGridViewPageSizeOnContractView.Text
                    LabelGridViewPageNumberOnAddendum.Text = LabelGridViewPageNumberOnContractView.Text

                    If LabelProjectNameInFormView.Text = "_CLIENTS" Then
                        LabelWarrantyPeriod.Visible = True
                        TextBoxWarrantyPeriod.Visible = True
                    End If

                    Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities
                        Dim panelClientAddData As Panel = FormViewAddendums.FindControl("panelClientAddData")
                        If (From C In db.Table_Contracts Where C.ContractID = LabelContractID.Text Select C.ProjectID).ToList()(0) = 999 Then
                            panelClientAddData.Visible = True
                        Else
                            panelClientAddData.Visible = False
                        End If
                        db.Dispose()
                    End Using

                End If
            End If
        End If

    End Sub

    Protected Sub FormViewAddendums_ItemInserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewInsertedEventArgs) Handles FormViewAddendums.ItemInserted

        ' send notification
        Dim SignByMercuryCheckBox As CheckBox = FormViewAddendums.FindControl("SignByMercuryCheckBox")
        Dim LinkToPDFcopyTextBox As TextBox = FormViewAddendums.FindControl("LinkToPDFcopyTextBox")
        Dim LabelProjectName As Label = FormViewAddendums.FindControl("LabelProjectName")
        Dim ContractValue_woVATTextBox As TextBox = FormViewAddendums.FindControl("ContractValue_woVATTextBox")
        Dim LabelShowSupplier As Label = FormViewAddendums.FindControl("LabelSupplierName")
        Dim ContractNoTextBox As TextBox = FormViewAddendums.FindControl("ContractNoTextBox")
        Dim LabelContractName As Label = FormViewAddendums.FindControl("LabelContractName")
        Dim LabelCurrency As Label = FormViewAddendums.FindControl("LabelCurrency")

        Dim Notification As String = ""
        Dim SendNotification As New MyCommonTasks

        Dim contractValue As String = ContractValue_woVATTextBox.Text
        If contractValue = "" Then
            contractValue = "0.0"
        End If

        Dim DetailedInfo As String = "| Project: " + LabelProjectName.Text + _
      " |SupplierName= " + GetSupplierName(LabelContractIDonAddendum.Text) + _
    " |AddendumNo= " + ContractNoTextBox.Text + _
    " |ContractNo= " + LabelContractName.Text + _
    " |AddendumValue= " + String.Format("{0:#,##0.00}", Convert.ToDecimal(contractValue)) + _
    LabelCurrency.Text.ToString

        If SignByMercuryCheckBox.Checked = False AndAlso Len(LinkToPDFcopyTextBox.Text) = 0 Then
            'New Addendum> Not Signed> No Attachment
            Notification = "New Addendum> Not Signed> No Attachment" + DetailedInfo
            SendNotification.SendNotification(Notification, GetProjectID(LabelProjectName.Text), 3)
            SendNotification.SendEmailForContract(GetProjectID(LabelProjectName.Text), Notification)
        ElseIf SignByMercuryCheckBox.Checked = True AndAlso Len(LinkToPDFcopyTextBox.Text) = 0 Then
            'New Addendum> Signed> No Attachment
            Notification = "New Addendum> Signed> No Attachment" + DetailedInfo
            SendNotification.SendNotification(Notification, GetProjectID(LabelProjectName.Text), 3)
            SendNotification.SendEmailForContract(GetProjectID(LabelProjectName.Text), Notification)
        ElseIf SignByMercuryCheckBox.Checked = True AndAlso Len(LinkToPDFcopyTextBox.Text) > 1 Then
            'New Addendum> Signed> Attachment added
            Notification = "New Addendum> Signed> " + "<a href=" + """" + Replace(LinkToPDFcopyTextBox.Text, "~", "") + """" + " target=" + """" + "_blank" + """" + ">Attachment</a>" + " added" + DetailedInfo
            SendNotification.SendNotification(Notification, GetProjectID(LabelProjectName.Text), 3)
            SendNotification.SendEmailForContract(GetProjectID(LabelProjectName.Text), Notification)
        ElseIf SignByMercuryCheckBox.Checked = False AndAlso Len(LinkToPDFcopyTextBox.Text) > 1 Then
            'New Addendum> Not Signed> Attachment added
            Notification = "New Addendum> Not Signed> " + "<a href=" + """" + Replace(LinkToPDFcopyTextBox.Text, "~", "") + """" + " target=" + """" + "_blank" + """" + ">Attachment</a>" + " added" + DetailedInfo
            SendNotification.SendNotification(Notification, GetProjectID(LabelProjectName.Text), 3)
            SendNotification.SendEmailForContract(GetProjectID(LabelProjectName.Text), Notification)
        End If


    End Sub

    Protected Sub FormViewAddendums_ItemInserting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewInsertEventArgs) Handles FormViewAddendums.ItemInserting

        Dim ContractValue_woVATTextBox As TextBox = FormViewAddendums.FindControl("ContractValue_woVATTextBox")
        Dim DropDownListRetention As DropDownList = FormViewAddendums.FindControl("DropDownListRetention")
        Dim ContractDateTextBox As TextBox = FormViewAddendums.FindControl("ContractDateTextBox")

        ' update AttachmentExist column
        Dim TextLink As TextBox = FormViewAddendums.FindControl("LinkToPDFcopyTextBox")
        If Len(TextLink.Text) > 0 OrElse Not String.IsNullOrEmpty(TextLink.Text) Then
            e.Values("AttachmentExist") = True
        Else
            e.Values("AttachmentExist") = False
        End If


        If ContractValue_woVATTextBox.Text <> "" Then
            e.Values("AddendumValue_woVAT") = Convert.ToDecimal(ContractValue_woVATTextBox.Text)
        ElseIf ContractValue_woVATTextBox.Text = "" Then
            e.Values("AddendumValue_woVAT") = Nothing
        End If

        If ContractDateTextBox.Text <> "" Then
            e.Values("AddendumDate") = Convert.ToDateTime(Mid(ContractDateTextBox.Text.ToString, 1, 2).ToString + "/" + Mid(ContractDateTextBox.Text.ToString, 4, 2).ToString + "/" + Mid(ContractDateTextBox.Text.ToString, 7, 4).ToString)
        ElseIf ContractDateTextBox.Text = "" Then
            e.Values("AddendumDate") = Nothing
        End If

        e.Values("AddendumRetention") = DropDownListRetention.SelectedValue

        e.Values("ContractID") = Convert.ToInt32(LabelContractIDonAddendum.Text)

        Dim zoneId As String = "Russian Standard Time"
        Dim tzi As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(zoneId)
        Dim result As DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi)

        e.Values("CreatedBy") = result
        e.Values("PersonCreated") = Page.User.Identity.Name.ToString

    End Sub

    Protected Sub ButtonUploadDOC_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim FileToUpload As FileUpload = FormViewAddendums.FindControl("FileUploadDOC")
        Dim LabelProjectName As Label = FormViewAddendums.FindControl("LabelProjectName")
        Dim LabelInfo As Label = FormViewAddendums.FindControl("LabelInfoDOC")
        Dim TextLink As TextBox = FormViewAddendums.FindControl("LinkToTemplatefile_DOCTextBox")

        If LabelProjectName.Text.ToString = "Select Project" Then
            LabelInfo.ForeColor = System.Drawing.Color.Red
            LabelInfo.Text = "Please select Project Name"
            TextLink.Text = ""
        Else
            If FileToUpload.HasFile Then
                If System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName) = ".doc" OrElse System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName) = ".docx" Then
                    If FileToUpload.PostedFile.ContentLength / 1000 > 20000 Then
                        LabelInfo.ForeColor = System.Drawing.Color.Red
                        LabelInfo.Text = "PDF file size must be less than 5MB"
                        TextLink.Text = ""
                    Else
                        If Directory.Exists(Server.MapPath("~/CONTRACT/") + LabelProjectName.Text.ToString + "/") Then
                            Dim UniqueString11 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
                            Dim UniqueString12 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
                            FileToUpload.SaveAs(MapPath("~/CONTRACT/" + LabelProjectName.Text.ToString + "/" + UniqueString11 + UniqueString12 + System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName)))
                            TextLink.Text = "~/CONTRACT/" + LabelProjectName.Text.ToString + "/" + UniqueString11 + UniqueString12 + System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName)
                            LabelInfo.ForeColor = System.Drawing.Color.DarkGreen
                            LabelInfo.Text = FileToUpload.PostedFile.FileName + " has been loaded successfully"
                        Else
                            Dim UniqueString21 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
                            Dim UniqueString22 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
                            Directory.CreateDirectory(Server.MapPath("~/CONTRACT/") + LabelProjectName.Text.ToString)
                            FileToUpload.SaveAs(MapPath("~/CONTRACT/" + LabelProjectName.Text.ToString + "/" + UniqueString21 + UniqueString22 + System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName)))
                            TextLink.Text = "~/CONTRACT/" + LabelProjectName.Text.ToString + "/" + UniqueString21 + UniqueString22 + System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName)
                            LabelInfo.ForeColor = System.Drawing.Color.DarkGreen
                            LabelInfo.Text = FileToUpload.PostedFile.FileName + " has been loaded successfully"
                        End If
                    End If
                Else
                    LabelInfo.ForeColor = System.Drawing.Color.Red
                    LabelInfo.Text = "Please select MS Word format file"
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
        Dim FileToUpload As FileUpload = FormViewAddendums.FindControl("FileUploadPDF")
        Dim LabelProjectName As Label = FormViewAddendums.FindControl("LabelProjectName")
        Dim LabelInfo As Label = FormViewAddendums.FindControl("LabelInfoPDF")
        Dim TextLink As TextBox = FormViewAddendums.FindControl("LinkToPDFcopyTextBox")

        If LabelProjectName.Text.ToString = "Select Project" Then
            LabelInfo.ForeColor = System.Drawing.Color.Red
            LabelInfo.Text = "Please select Project Name"
            TextLink.Text = ""
        Else
            If FileToUpload.HasFile Then
                If System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName) = ".pdf" _
                  OrElse System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName) = ".doc" _
                  OrElse System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName) = ".docx" Then
                    If FileToUpload.PostedFile.ContentLength / 1000 > 20000 Then
                        LabelInfo.ForeColor = System.Drawing.Color.Red
                        LabelInfo.Text = "PDF file size must be less than 5MB"
                        TextLink.Text = ""
                    Else
                        If Directory.Exists(Server.MapPath("~/CONTRACT/") + LabelProjectName.Text.ToString + "/") Then
                            Dim UniqueString11 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
                            Dim UniqueString12 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
                            FileToUpload.SaveAs(MapPath("~/CONTRACT/" + LabelProjectName.Text.ToString + "/" + UniqueString11 + UniqueString12 + System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName)))
                            TextLink.Text = "~/CONTRACT/" + LabelProjectName.Text.ToString + "/" + UniqueString11 + UniqueString12 + System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName)
                            LabelInfo.ForeColor = System.Drawing.Color.DarkGreen
                            LabelInfo.Text = FileToUpload.PostedFile.FileName + " has been loaded successfully"
                        Else
                            Dim UniqueString21 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
                            Dim UniqueString22 As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
                            Directory.CreateDirectory(Server.MapPath("~/CONTRACT/") + LabelProjectName.Text.ToString)
                            FileToUpload.SaveAs(MapPath("~/CONTRACT/" + LabelProjectName.Text.ToString + "/" + UniqueString21 + UniqueString22 + System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName)))
                            TextLink.Text = "~/CONTRACT/" + LabelProjectName.Text.ToString + "/" + UniqueString21 + UniqueString22 + System.IO.Path.GetExtension(FileToUpload.PostedFile.FileName)
                            LabelInfo.ForeColor = System.Drawing.Color.DarkGreen
                            LabelInfo.Text = FileToUpload.PostedFile.FileName + " has been loaded successfully"
                        End If
                    End If
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

    Protected Sub FormViewContract_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles FormViewAddendums.Load
        If IsPostBack Or Not IsPostBack Then
            ' it cause button to postback. 
            Dim ButtonUploadDOC As Button = FormViewAddendums.FindControl("ButtonUploadDOC")
            Dim ButtonUploadPDF As Button = FormViewAddendums.FindControl("ButtonUploadPDF")

        End If
    End Sub

    Protected Function GetProjectID(ByVal ProjectName As String) As String
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = "SELECT     ProjectID FROM  dbo.Table1_Project WHERE  (ProjectName = N'" + ProjectName + "')"
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text
            Dim dr As SqlDataReader = cmd.ExecuteReader
            Dim ProjectID As String = ""
            While dr.Read
                ProjectID = dr(0).ToString
            End While
            con.Close()
            dr.Close()
            con.Dispose()
            Return ProjectID

        End Using
    End Function

    Protected Function GetSupplierName(ByVal ContractID As String) As String
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " SELECT     RTRIM(dbo.Table6_Supplier.SupplierName) AS SupplierName " + _
        " FROM         dbo.Table6_Supplier INNER JOIN " + _
        "                       dbo.Table_Contracts ON dbo.Table6_Supplier.SupplierID = dbo.Table_Contracts.SupplierID " + _
        " WHERE     (dbo.Table_Contracts.ContractID = " + ContractID + ") "

            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text
            Dim dr As SqlDataReader = cmd.ExecuteReader
            Dim SupplierName As String = ""
            While dr.Read
                SupplierName = dr(0).ToString
            End While
            con.Close()
            dr.Close()
            con.Dispose()
            Return SupplierName

        End Using
    End Function

    Protected Sub CheckBoxPenaltyToMercury_CheckedChanged(sender As Object, e As EventArgs)

        Dim cbx As CheckBox = sender

        Dim RequiredFieldValidatorPenaltyMercuryNote As RequiredFieldValidator = FormViewAddendums.FindControl("RequiredFieldValidatorPenaltyMercuryNote")
        If cbx.Checked = True Then
            RequiredFieldValidatorPenaltyMercuryNote.Enabled = True
        Else
            RequiredFieldValidatorPenaltyMercuryNote.Enabled = False
        End If

    End Sub
End Class
