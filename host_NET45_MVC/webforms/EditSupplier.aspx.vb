Imports System.Data.SqlClient

Partial Class EditSupplier
    Inherits System.Web.UI.Page

    Protected Sub GridViewEditSupplier_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridViewEditSupplier.RowDataBound

        Dim RegularExpressionValidatorSupplierEnter As RegularExpressionValidator = DirectCast(e.Row.FindControl("RegularExpressionValidatorSupplierEnter"), RegularExpressionValidator)

        If RegularExpressionValidatorSupplierEnter IsNot Nothing Then
            ' it is EDIT mode
            Dim CheckBoxPersonSupplier As CheckBox = DirectCast(e.Row.FindControl("CheckBoxPersonSupplier"), CheckBox)
            Dim TextBoxSupplierID As TextBox = DirectCast(e.Row.FindControl("TextBoxSupplierID"), TextBox)

            If Len(TextBoxSupplierID.Text.Trim) = 12 Then
                CheckBoxPersonSupplier.Checked = True
            End If

            If CheckBoxPersonSupplier.Checked Then
                RegularExpressionValidatorSupplierEnter.ValidationExpression = "\d{12}"
                RegularExpressionValidatorSupplierEnter.ErrorMessage = "INN number to be 12 digit!"
            Else
                RegularExpressionValidatorSupplierEnter.ValidationExpression = "\d{10}"
                RegularExpressionValidatorSupplierEnter.ErrorMessage = "INN number to be 10 digit!"
            End If

        End If

        If e.Row.RowType = DataControlRowType.DataRow Then

            If Not Page.User.IsInRole("EnterPurchaseOrder") Then
                GridViewEditSupplier.Columns(0).Visible = False
                'e.Row.Cells(0).Enabled = False
            End If


        End If

    End Sub

    Protected Sub GridViewEditSupplier_RowUpdated(sender As Object, e As GridViewUpdatedEventArgs) Handles GridViewEditSupplier.RowUpdated

        Dim row As GridViewRow = sender.rows(sender.editIndex)
        Dim TextBoxSupplierID As TextBox = DirectCast(row.FindControl("TextBoxSupplierID"), TextBox)

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = "UPDATE Table6_Supplier SET SupplierID = @SupplierIDNew WHERE SupplierID = @SupplierIDOld"
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text

            'syntax for parameter adding
            Dim SupplierIDNew As SqlParameter = cmd.Parameters.Add("@SupplierIDNew", System.Data.SqlDbType.NVarChar, 12)
            SupplierIDNew.Value = TextBoxSupplierID.Text.Trim

            Dim SupplierIDOld As SqlParameter = cmd.Parameters.Add("@SupplierIDOld", System.Data.SqlDbType.NVarChar, 12)
            SupplierIDOld.Value = Mid(TextBoxSupplierIDOuter.Text.TrimEnd, 1, TextBoxSupplierIDOuter.Text.TrimEnd.IndexOf(" "))

            Dim dr As SqlDataReader = cmd.ExecuteReader
            con.Close()
            dr.Close()
        End Using


    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            TextBoxSupplierIDOuter.Focus()
        End If

    End Sub

    Protected Sub CheckBoxPersonSupplier_CheckedChanged(sender As Object, e As EventArgs)

        Dim cBx As CheckBox = sender
        Dim row As GridViewRow = cBx.NamingContainer

        Dim TextBoxSupplierID As TextBox = DirectCast(row.FindControl("TextBoxSupplierID"), TextBox)
        Dim CheckBoxPersonSupplier As CheckBox = DirectCast(row.FindControl("CheckBoxPersonSupplier"), CheckBox)
        Dim RegularExpressionValidatorSupplierEnter As RegularExpressionValidator = DirectCast(row.FindControl("RegularExpressionValidatorSupplierEnter"), RegularExpressionValidator)

        If CheckBoxPersonSupplier.Checked Then
            RegularExpressionValidatorSupplierEnter.ValidationExpression = "\d{12}"
            RegularExpressionValidatorSupplierEnter.ErrorMessage = "INN number to be 12 digit!"
        Else
            RegularExpressionValidatorSupplierEnter.ValidationExpression = "\d{10}"
            RegularExpressionValidatorSupplierEnter.ErrorMessage = "INN number to be 10 digit!"
        End If

    End Sub

End Class
