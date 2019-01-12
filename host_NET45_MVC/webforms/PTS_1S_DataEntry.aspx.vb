Imports System.IO
Imports System.Data.OleDb
Imports System.Data
Imports System.Data.SqlClient

Partial Class PTS_1S_DataEntry
    Inherits System.Web.UI.Page

    Dim Date_From As String = ""
    Dim Date_To As String = ""
    Dim PaymentAmount_1S As Decimal = 0.0
    Dim PaymentAmount_PTS As Decimal = 0.0
    Dim SupposedToBePaid As Decimal = 0.0
    Dim DifferenceInRealPayments As Decimal = 0.0

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If FileUpload1.HasFile Then
            Dim path__1 As String = FileUpload1.PostedFile.FileName

            FileUpload1.PostedFile.SaveAs(Server.MapPath("~/UploadsFrom1S/") & path__1)

            SqlDataSource1.SelectCommand = "SELECT * FROM OPENDATASOURCE('Microsoft.ACE.OLEDB.12.0','Data Source=" _
                                          + Server.MapPath("~/UploadsFrom1S/") & path__1 + ";Extended Properties=''Excel 12.0;HDR=No''')...[TDSheet$]"

            GridView1.DataBind()

        End If



        ' validating FROM and TO date
        For Each gvr As GridViewRow In GridView1.Rows
            If gvr.RowType = DataControlRowType.DataRow Then
                If gvr.RowIndex = 3 Then

                    ' Get the date FROM
                    Date_From = Mid(gvr.Cells(1).Text, Len(gvr.Cells(1).Text.TrimEnd) - 24, 11)

                    ' Get the date TO
                    Date_To = Mid(gvr.Cells(1).Text, Len(gvr.Cells(1).Text.TrimEnd) - 10, 11)

                    '' validate date FROM
                    'If IsDate(Date_From) Then
                    '    gvr.Cells(1).BackColor = System.Drawing.Color.Green
                    '    gvr.Cells(1).Text = Convert.ToDateTime(Date_From)
                    'Else
                    '    gvr.Cells(1).BackColor = System.Drawing.Color.Red
                    '    gvr.Cells(1).Text = "Not Valid Date"
                    'End If

                    '' validate date TO
                    'If IsDate(Date_To) Then
                    '    gvr.Cells(2).BackColor = System.Drawing.Color.Green
                    '    gvr.Cells(2).Text = Convert.ToDateTime(Date_To)
                    'Else
                    '    gvr.Cells(2).BackColor = System.Drawing.Color.Red
                    '    gvr.Cells(2).Text = "Not Valid Date"
                    'End If

                    'If Date_From = Date_To Then
                    '    gvr.BackColor = System.Drawing.Color.Green
                    'Else
                    '    gvr.BackColor = System.Drawing.Color.Red
                    'End If

                End If
            End If
        Next

        If IsDate(Date_From) And IsDate(Date_To) And (Date_From = Date_To) Then
            ' delete existing data to overwrite
            EnterPaymentsFrom1S.Delete(Date_From)
        Else
            ' Give some warning to user that it is NOT VALID 
            Response.Write("DateFrom and DateTo must be equal to each other. This is at the top.")

        End If

        ' overwrite the data
        GridView1.DataBind()

        For Each gvr2 As GridViewRow In GridView1.Rows
            If gvr2.RowType = DataControlRowType.Header Then
                gvr2.BackColor = System.Drawing.Color.Red
            ElseIf gvr2.RowType = DataControlRowType.DataRow Then
                gvr2.BackColor = System.Drawing.Color.Gray
            ElseIf gvr2.RowType = DataControlRowType.Footer Then
                gvr2.BackColor = System.Drawing.Color.Blue
            End If

            If gvr2.RowType = DataControlRowType.DataRow Then

            End If

            If gvr2.RowType = DataControlRowType.DataRow Then

                If IsNumeric(gvr2.Cells(1).Text) Then
                    ' assume that you can start entering 
                    gvr2.BackColor = System.Drawing.Color.GreenYellow
                    If (IsDate(Date_From) And IsDate(Date_To)) And (Date_From = Date_To) Then
                        ' insert data
                        'Try
                        EnterPaymentsFrom1S.Insert(Convert.ToString(Mid(gvr2.Cells(2).Text, 21, 11)), _
                                                   Convert.ToDateTime(Date_From), _
                                                   Convert.ToInt32(gvr2.Cells(1).Text), _
                                                   Convert.ToString(gvr2.Cells(2).Text), _
                                                   Convert.ToString(gvr2.Cells(4).Text), _
                                                   Convert.ToDecimal(gvr2.Cells(6).Text.Replace(",", "")))
                        'Catch ex As Exception

                        'End Try
                    End If
                End If

            End If
        Next

        If IsDate(Date_From) And IsDate(Date_To) And (Date_From = Date_To) Then
            Dim ObjSource As ObjectDataSource = WebUserControl_PTS1Scomparison.FindControl("ObjectDataSourceComparison")
            ObjSource.SelectParameters("CreatedBy").DefaultValue = Convert.ToDateTime(Date_From)
        End If

    End Sub

    Protected Sub PaymentDateTextBox_TextChanged(sender As Object, e As EventArgs) Handles PaymentDateTextBox.TextChanged

        Dim ObjSource As ObjectDataSource = WebUserControl_PTS1Scomparison.FindControl("ObjectDataSourceComparison")
        ObjSource.SelectParameters("CreatedBy").DefaultValue = Convert.ToDateTime(PaymentDateTextBox.Text)

    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not (Page.User.Identity.Name.ToLower() = "dzera" OrElse Page.User.Identity.Name.ToLower() = "inna" OrElse Page.User.Identity.Name.ToLower() = "savas" OrElse Page.User.Identity.Name.ToLower() = "elmira.shabaeva" _
            OrElse Page.User.Identity.Name.ToLower() = "mariya.podobueva" OrElse Page.User.Identity.Name.ToLower() = "natalia.larionova") Then
            Response.Redirect("~/webforms/AccessDenied.aspx")
        End If

    End Sub
End Class
