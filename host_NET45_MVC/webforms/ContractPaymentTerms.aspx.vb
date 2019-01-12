Imports System.Data.SqlClient
Partial Class ContractPaymentTerms
    Inherits System.Web.UI.Page

  Protected Sub LinkButtonInsert_Click(sender As Object, e As System.EventArgs) Handles LinkButtonInsert.Click
    Dim con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
    con.Open()
    Dim sqlstring As String = " INSERT INTO [Table_ContractsTerms] " + _
"            ([ContractID] " + _
"            ,[Advance] " + _
"            ,[ProgressPayment] " + _
"            ,[PaymentAfterDelivery] " + _
"            ,[PaymentTermDay] " + _
"            ,[CreditRubWthVAT] " + _
"            ,[PenaltiesForLatePayment] " + _
"            ,[PenaltiesLimit]) " + _
"      VALUES " + _
"            (@ContractID " + _
"            ,@Advance " + _
"            ,@ProgressPayment " + _
"            ,@PaymentAfterDelivery " + _
"            ,@PaymentTermDay " + _
"            ,@CreditRubWthVAT " + _
"            ,@PenaltiesForLatePayment " + _
"            ,@PenaltiesLimit) "

    Dim cmd As New SqlCommand(sqlstring, con)
        cmd.CommandType = System.Data.CommandType.Text

        'syntax for parameter adding
        Dim ContractID As SqlParameter = cmd.Parameters.Add("@ContractID", System.Data.SqlDbType.Int)
        ContractID.Value = Convert.ToInt32(Request.QueryString("ID"))

        Dim Advance As SqlParameter = cmd.Parameters.Add("@Advance", System.Data.SqlDbType.Int)
        If String.IsNullOrEmpty(TextBoxAdvance.Text) Then
            Advance.Value = Nothing
        Else
            Advance.Value = Convert.ToInt32(TextBoxAdvance.Text)
        End If

        Dim ProgressPayment As SqlParameter = cmd.Parameters.Add("@ProgressPayment", System.Data.SqlDbType.Int)
        If String.IsNullOrEmpty(TextBoxProgressPayment.Text) Then
            ProgressPayment.Value = Nothing
        Else
            ProgressPayment.Value = Convert.ToInt32(TextBoxProgressPayment.Text)
        End If

        Dim PaymentAfterDelivery As SqlParameter = cmd.Parameters.Add("@PaymentAfterDelivery", System.Data.SqlDbType.Int)
        If String.IsNullOrEmpty(TextBoxPaymentAfterDelivery.Text) Then
            PaymentAfterDelivery.Value = Nothing
        Else
            PaymentAfterDelivery.Value = Convert.ToInt32(TextBoxPaymentAfterDelivery.Text)
        End If

        Dim PaymentTermDay As SqlParameter = cmd.Parameters.Add("@PaymentTermDay", System.Data.SqlDbType.Int)
        If String.IsNullOrEmpty(TextBoxPaymentTermDays.Text) Then
            PaymentTermDay.Value = Nothing
        Else
            PaymentTermDay.Value = Convert.ToInt32(TextBoxPaymentTermDays.Text)
        End If

        Dim CreditRubWthVAT As SqlParameter = cmd.Parameters.Add("@CreditRubWthVAT", System.Data.SqlDbType.Decimal)
        If String.IsNullOrEmpty(TextBoxCreditAmount.Text) Then
            CreditRubWthVAT.Value = Nothing
        Else
            CreditRubWthVAT.Value = Convert.ToDecimal(TextBoxCreditAmount.Text)
        End If

        Dim PenaltiesForLatePayment As SqlParameter = cmd.Parameters.Add("@PenaltiesForLatePayment", System.Data.SqlDbType.NVarChar)
        If String.IsNullOrEmpty(TextBoxPenaltiesForLatePayment.Text) Then
            PenaltiesForLatePayment.Value = Nothing
        Else
            PenaltiesForLatePayment.Value = TextBoxPenaltiesForLatePayment.Text
        End If

        Dim PenaltiesLimit As SqlParameter = cmd.Parameters.Add("@PenaltiesLimit", System.Data.SqlDbType.NVarChar)
        If String.IsNullOrEmpty(TextBoxPenaltiesLimit.Text) Then
      PenaltiesLimit.Value = Nothing
    Else
      PenaltiesLimit.Value = TextBoxPenaltiesLimit.Text
    End If

    Dim dr As SqlDataReader = cmd.ExecuteReader
    con.Close()
    dr.Close()
  End Sub
End Class
