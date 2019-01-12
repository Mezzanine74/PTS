Imports System.Data.SqlClient

Partial Class POs_To_Check
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'ScriptManager1.SupportsPartialRendering = False
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not (Page.User.Identity.Name.ToLower = "tamas.simon" Or Page.User.Identity.Name.ToLower = "savas") Then

            Response.Redirect("~/webforms/accessdenied.aspx")

        End If


        GridViewMonitor.DataBind()
    End Sub


    Protected Sub GridViewMonitor_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridViewMonitor.RowCommand
        If (e.CommandName = "good") Then

            Dim PayReqNo As Integer = e.CommandArgument

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                Dim _entry As New PTS_MERCURY.db.Table_PaymentRequest_OverHeadCheck

                If (Aggregate C In db.Table_PaymentRequest_OverHeadCheck Where C.PayReqNo = PayReqNo Into Count()) = 0 Then
                    _entry.PayReqNo = PayReqNo
                    _entry.WhenChecked = LocalTime.GetTime
                    _entry.WhoChecked = Page.User.Identity.Name.ToLower
                    _entry.Status = "good"

                    db.Table_PaymentRequest_OverHeadCheck.Add(_entry)

                    db.SaveChanges()

                    GridViewMonitor.DataBind()

                    _GiveNotification.Gritter_Error(Page, "Marked as Good", "You will not see this item again")

                End If

                db.Dispose()
            End Using

        End If

        If (e.CommandName = "move") Then

            Dim PayReqNo As Integer = e.CommandArgument

            If IsNumeric(PayReqNo) Then

                Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                    Dim _entry = (From C In db.Table2_PONo
                                 Join I In db.Table3_Invoice On C.PO_No Equals I.PO_No
                                 Join P In db.Table4_PaymentRequest On I.InvoiceID Equals P.InvoiceID
                                 Where P.PayReqNo = PayReqNo Select New With {C.PO_No}).ToList()(0)

                    If (Aggregate C In db.Table2_PONo Where C.PO_No = _entry.PO_No Into Count()) = 1 Then

                        LabelPayReqNoModal.Text = PayReqNo.ToString
                        Session("payreqno") = PayReqNo.ToString

                        LabelPoOnModal.Text = _entry.PO_No
                        Session("pomove") = LabelPoOnModal.Text

                        ButtonMoveAction.Enabled = True

                    Else

                        LabelPoOnModal.Text = "This PO cannot be moved. Several invoices under that."

                        ButtonMoveAction.Enabled = False

                    End If


                End Using

            End If

            ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "alert", "$(function () { $('#" + "ModalMove" + "').modal({}) });", True)


        End If

    End Sub

    Protected Sub GridViewMonitor_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewMonitor.RowDataBound

        Dim MyTask As New MyCommonTasks
        MyTask.HoverEffectOnGridviewCells(sender, e.Row)

        If e.Row.RowType = DataControlRowType.DataRow Then
            'it defines type of PDF image if it exist or not.
            If DataBinder.Eval(e.Row.DataItem, "AttachmentExist") = True Then
                DirectCast(e.Row.FindControl("ImageButtonPdf"), HyperLink).ImageUrl = "~/Images/pdf.bmp"
                DirectCast(e.Row.FindControl("ImageButtonPdf"), HyperLink).NavigateUrl = _
                  "~/webforms/showFile.aspx?link=" + Replace(DataBinder.Eval(e.Row.DataItem, "LinkToInvoice").ToString, "~", "")
            Else
                DirectCast(e.Row.FindControl("ImageButtonPdf"), HyperLink).ImageUrl = "~/Images/pdf_bw.bmp"
            End If
        End If

        ' It fixes column width problem
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim Label1 As Label = DirectCast(e.Row.FindControl("Label2"), Label)

            If Label1 IsNot Nothing Then
                Label1.Text = Label1.Text.Replace(",", "," + " ")
            End If
        End If

        ' it provide hyperlinkURL for POsub
        If DirectCast(e.Row.FindControl("LabelPONo"), Label) IsNot Nothing Then

            ' highlight row Color if it is sub PO
            If InStr(DataBinder.Eval(e.Row.DataItem, "PO_No").ToString, "/") > 0 Then
                e.Row.BackColor = System.Drawing.Color.Yellow
            End If

        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            If IsNotApproved(DataBinder.Eval(e.Row.DataItem, "PO_No").ToString) Then
                e.Row.BackColor = System.Drawing.Color.Black
                e.Row.ForeColor = System.Drawing.Color.White
                e.Row.Enabled = False
                e.Row.Cells(11).Text = "NOT APPROVED PO"
                e.Row.Cells(11).BackColor = System.Drawing.Color.Red
            End If
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim LabelSupplierID As Label = DirectCast(e.Row.FindControl("LabelSupplierID"), Label)
            Dim pono As String = ""
            If Request.QueryString("POno") IsNot Nothing Then
                pono = Request.QueryString("POno")

                Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                    LabelSupplierID.Text = (From C In db.Table2_PONo Where C.PO_No = pono).ToList()(0).SupplierID

                End Using

            End If


        End If

    End Sub

    Protected Function IsNotApproved(ByVal POno As String) As Boolean
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " SELECT CASE WHEN Approved IS NULL THEN 1 ELSE  Approved END AS Approved FROM Table2_POno WHERE PO_No = @PO_No "
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text
            'syntax for parameter adding
            Dim poNo_ As SqlParameter = cmd.Parameters.Add("@PO_No", System.Data.SqlDbType.NVarChar, 15)
            poNo_.Value = POno
            Dim dr As SqlDataReader = cmd.ExecuteReader
            Dim returnValue As Boolean
            While dr.Read
                If dr(0) = False Then
                    returnValue = True
                Else
                    returnValue = False
                End If
            End While
            Return returnValue
            con.Close()
            dr.Close()
            con.Dispose()

        End Using
    End Function

    Protected Sub ButtonMoveAction_Click(sender As Object, e As EventArgs)

        Dim _Podestination As String = ""
        Dim _po As String = Session("pomove")

        Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

            Dim _entity = (From C In db.Table2_PONo Where C.PO_No = _po).ToList()(0)

            _Podestination = db.GetPoNoFromMakerNewGeneration(DropDownListProject.SelectedValue).ToList()(0)

            db.Database.ExecuteSqlCommand("update Table2_PONo set Project_ID = {0} , PO_No = {1} where PO_No = {2} ", _
                                          DropDownListProject.SelectedValue, _
                                          db.GetPoNoFromMakerNewGeneration(DropDownListProject.SelectedValue).ToList()(0), _
                                          _po
                                          )

            Session.Remove("pomove")

            db.Dispose()

        End Using


        Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

            Dim _entry As New PTS_MERCURY.db.Table_PaymentRequest_OverHeadCheck

            Dim _payreqno As String = Session("payreqno")

            If (Aggregate C In db.Table_PaymentRequest_OverHeadCheck Where C.PayReqNo = _payreqno Into Count()) = 0 Then
                _entry.PayReqNo = _payreqno
                _entry.WhenChecked = LocalTime.GetTime
                _entry.WhoChecked = Page.User.Identity.Name.ToLower
                _entry.Status = "moved to " + _Podestination

                db.Table_PaymentRequest_OverHeadCheck.Add(_entry)

                db.SaveChanges()

                GridViewMonitor.DataBind()

                _GiveNotification.Gritter_Error(Page, "Moved to " + _Podestination, "You will not see this item again")

                Session.Remove("payreqno")

            End If

            db.Dispose()
        End Using





    End Sub
End Class
