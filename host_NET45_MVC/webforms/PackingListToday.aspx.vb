Imports System.Data.SqlClient
Imports System.Data

Partial Class PackingListToday
  Inherits System.Web.UI.Page

  Protected Sub GridViewPAckingListToday_Load(sender As Object, e As System.EventArgs) Handles GridViewPAckingListToday.Load
    If Not Roles.IsUserInRole("ProcurementStaff") Then
      GridViewPAckingListToday.Columns(4).Visible = False
      GridViewPAckingListToday.Columns(5).Visible = False
    End If
  End Sub

  Protected Sub GridViewPAckingListToday_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridViewPAckingListToday.RowCommand
    If (e.CommandName = "OpenPdf") Then

      Dim LinkToFile As String = e.CommandArgument.ToString
      Dim openpdf As New MyCommonTasks
      Try
        openpdf.OpenPDF(LinkToFile)
      Catch ex As Exception
        Throw ex
      End Try


    End If
  End Sub

    'Protected Sub GridViewPAckingListToday_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewPAckingListToday.RowCreated
    '  If e.Row.RowType = DataControlRowType.DataRow Then

    '    Dim chb As CheckBox = DirectCast(e.Row.FindControl("CheckBoxTransport"), CheckBox)
    '    AddHandler chb.CheckedChanged, AddressOf check_changed

    '    Dim Dll As DropDownList = DirectCast(e.Row.FindControl("DropDownListPersonResponsible"), DropDownList)
    '    AddHandler Dll.SelectedIndexChanged, AddressOf Index_changed

    '  End If
    'End Sub

  Public Sub check_changed(ByVal sender As Object, ByVal e As EventArgs)
    Dim CheckBoxTransport As CheckBox = sender
    Dim PayReqNo As Integer = _
      Me.GridViewPAckingListToday.DataKeys.Item(CType(CType(sender, CheckBox).Parent.Parent, GridViewRow).RowIndex).Value
    ' Update paymetRequest table 
    Dim TransportStatus As String = ""
    If CheckBoxTransport.Checked Then
      TransportStatus = "1"
    Else
      TransportStatus = "0"
    End If
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = "update Table4_PaymentRequest set transport = " + TransportStatus + " where PayReqNo = " + PayReqNo.ToString + ""
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text
            Dim dr As SqlDataReader = cmd.ExecuteReader
            con.Close()
            con.Dispose()
            dr.Close()

        End Using

        GridViewPAckingListToday.DataBind()
    End Sub

    Private Sub Index_changed(sender As Object, e As EventArgs)
        Dim DropDownListResponsiblePerson As DropDownList = sender
        Dim PersonResponsible As String = ""
        Dim PayReqNo As Integer =
      Me.GridViewPAckingListToday.DataKeys.Item(CType(CType(sender, DropDownList).Parent.Parent, GridViewRow).RowIndex).Value
        Dim PersonResponsibleID As String = ""

        ' Find ResponsiblePersonID
        If Len(DropDownListResponsiblePerson.SelectedValue) > 0 Then
            Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
                con.Open()
                Dim sqlstring As String = "SELECT PersonResponsibleID FROM [Table_PersonResponsible] WHERE [PersonResponsible] = N'" + DropDownListResponsiblePerson.SelectedValue + "'"
                Dim cmd As New SqlCommand(sqlstring, con)
                cmd.CommandType = System.Data.CommandType.Text
                Dim dr As SqlDataReader = cmd.ExecuteReader
                While dr.Read
                    PersonResponsibleID = dr(0).ToString
                End While
                con.Close()
                con.Dispose()
                dr.Close()

            End Using
        Else
            PersonResponsibleID = "NULL"
        End If

        'Update Payment Request Table
        Using conX As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            conX.Open()
            Dim sqlstringX As String = "update Table4_PaymentRequest set PersonResponsibleID = " + PersonResponsibleID + " where PayReqNo = " + PayReqNo.ToString + ""
            Dim cmdX As New SqlCommand(sqlstringX, conX)
            cmdX.CommandType = System.Data.CommandType.Text
            Dim drX As SqlDataReader = cmdX.ExecuteReader
            conX.Close()
            conX.Dispose()
            drX.Close()

        End Using

        GridViewPAckingListToday.DataBind()
    End Sub

    Protected Sub GridViewPAckingListToday_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewPAckingListToday.RowDataBound

        Dim MyTask As New MyCommonTasks
        MyTask.HoverEffectOnGridviewCells(sender, e.Row)

        If e.Row.RowType = DataControlRowType.DataRow Then
            'it defines type of PDF image if it exist or not.
            If DataBinder.Eval(e.Row.DataItem, "AttachmentExist") = True Then
                DirectCast(e.Row.FindControl("ImageButton1"), ImageButton).ImageUrl = "~/Images/pdf.bmp"
            Else
                DirectCast(e.Row.FindControl("ImageButton1"), ImageButton).ImageUrl = "~/Images/pdf_bw.bmp"
            End If

        End If

        ' It fixes column width problem
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim Label7 As Label = DirectCast(e.Row.FindControl("Label7"), Label)

            If Label7 IsNot Nothing Then
                Label7.Text = Label7.Text.Replace(",", "," + " ")
            End If
        End If

        'it defines CurrencyImage.
        If DirectCast(e.Row.FindControl("LabelCurrency"), Label) IsNot Nothing Then
            If DirectCast(e.Row.FindControl("LabelCurrency"), Label).Text = "Rub" Then
                DirectCast(e.Row.FindControl("ImageCurrency"), Image).ImageUrl = "~/Images/ruble_icon_.bmp"
            ElseIf DirectCast(e.Row.FindControl("LabelCurrency"), Label).Text = "Dollar" Then
                DirectCast(e.Row.FindControl("ImageCurrency"), Image).ImageUrl = "~/Images/dollar_icon_.bmp"
            ElseIf DirectCast(e.Row.FindControl("LabelCurrency"), Label).Text = "Euro" Then
                DirectCast(e.Row.FindControl("ImageCurrency"), Image).ImageUrl = "~/Images/euro_icon_.bmp"
            End If
        End If

        '' it paints DDL background
        'Dim DDL_ResponsiblePerson As DropDownList = DirectCast(e.Row.FindControl("DropDownListPersonResponsible"), DropDownList)
        'If DDL_ResponsiblePerson IsNot Nothing Then
        '  DDL_ResponsiblePerson.BackColor = System.Drawing.ColorTranslator.FromHtml(GetDDLBackGroundColor(DDL_ResponsiblePerson.SelectedValue.ToString))
        '  If Not DDL_ResponsiblePerson.SelectedValue.ToString.ToLower = "site" Then
        '    DDL_ResponsiblePerson.ForeColor = System.Drawing.Color.White
        '    DDL_ResponsiblePerson.Font.Bold = True
        '  Else
        '    DDL_ResponsiblePerson.ForeColor = System.Drawing.Color.Black
        '  End If
        'End If

    End Sub

    Protected Function GetDDLBackGroundColor(ByVal PersonName As String) As String
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = "select RTRIM(DDLBackColor) as DDLBackColor from Table_PersonResponsible where PersonResponsible = N'" + PersonName + "' "
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text

            Dim dr As SqlDataReader = cmd.ExecuteReader
            Dim BackGroundColor As String = ""
            While dr.Read
                BackGroundColor = dr(0).ToString
            End While
            con.Close()
            con.Dispose()
            dr.Close()
            Return BackGroundColor

        End Using
    End Function


  Protected Sub SqlDataSourcePackingListToday_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceSelectingEventArgs) Handles SqlDataSourcePackingListToday.Selecting
  End Sub

  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    If Not IsPostBack Or IsPostBack Then
      If Roles.IsUserInRole("ProcurementStaff") Then
        ImageButtonDeliveryReport.Visible = True
      Else
        ImageButtonDeliveryReport.Visible = False
      End If
    End If

    If Not IsPostBack Then
      Dim zoneId As String = "Russian Standard Time"
      Dim tzi As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(zoneId)
      Dim result As DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi)

      PaymentDateTextBoxShown.Text = (Mid(result.ToString, 1, 2) + "/" + Mid(result.ToString, 4, 2) + "/" + Mid(result.ToString, 7, 4)).ToString

      SqlDataSourcePackingListToday.SelectParameters("PayReqDate").DefaultValue = PaymentDateTextBoxShown.Text
      SqlDataSourcePackingListToday.SelectParameters("PayReqDate").Type = TypeCode.DateTime
    End If

  End Sub

  Protected Sub PaymentDateTextBoxShown_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles PaymentDateTextBoxShown.TextChanged
    SqlDataSourcePackingListToday.SelectParameters("PayReqDate").DefaultValue = PaymentDateTextBoxShown.Text
    SqlDataSourcePackingListToday.SelectParameters("PayReqDate").Type = TypeCode.DateTime

    GridViewPAckingListToday.DataBind()
  End Sub

End Class
