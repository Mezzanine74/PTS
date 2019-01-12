Imports System.Data.SqlClient
Partial Class questionnaire_EIM2
  Inherits System.Web.UI.Page

  Protected Sub GridViewQstGrid_1_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridViewQstGrid_1.RowCommand
    Dim imagebuttonSender As ImageButton = e.CommandSource
    Dim row As GridViewRow = CType(imagebuttonSender.NamingContainer, GridViewRow)
    Do_AllRowCommandForGridview(row, e.CommandName, e.CommandArgument.ToString)
  End Sub

  Protected Sub GridViewQstGrid_1_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewQstGrid_1.RowDataBound
    Dim row As GridViewRow = e.Row
    Do_AllRowDataRowBoundForGridview(row)
  End Sub

  Protected Sub GridViewQstGrid_2_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridViewQstGrid_2.RowCommand
    Dim imagebuttonSender As ImageButton = e.CommandSource
    Dim row As GridViewRow = CType(imagebuttonSender.NamingContainer, GridViewRow)
    Do_AllRowCommandForGridview(row, e.CommandName, e.CommandArgument.ToString)
  End Sub

  Protected Sub GridViewQstGrid_2_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewQstGrid_2.RowDataBound
    Dim row As GridViewRow = e.Row
    Do_AllRowDataRowBoundForGridview(row)
  End Sub

  Protected Sub GridViewQstGrid_3_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridViewQstGrid_3.RowCommand
    Dim imagebuttonSender As ImageButton = e.CommandSource
    Dim row As GridViewRow = CType(imagebuttonSender.NamingContainer, GridViewRow)
    Do_AllRowCommandForGridview(row, e.CommandName, e.CommandArgument.ToString)
  End Sub

  Protected Sub GridViewQstGrid_3_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewQstGrid_3.RowDataBound
    Dim row As GridViewRow = e.Row
    Do_AllRowDataRowBoundForGridview(row)
  End Sub

  Protected Sub GridViewQstGrid_4_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridViewQstGrid_4.RowCommand
    Dim imagebuttonSender As ImageButton = e.CommandSource
    Dim row As GridViewRow = CType(imagebuttonSender.NamingContainer, GridViewRow)
    Do_AllRowCommandForGridview(row, e.CommandName, e.CommandArgument.ToString)
  End Sub

  Protected Sub GridViewQstGrid_4_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewQstGrid_4.RowDataBound
    Dim row As GridViewRow = e.Row
    Do_AllRowDataRowBoundForGridview(row)
  End Sub


  Protected Function getAnswerID(ByVal questionID As Integer, ByVal userName As String) As Integer
    Dim con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
    con.Open()
    Dim sqlstring As String = " SELECT CASE WHEN (SELECT     Answer AS AnswerId " + _
" FROM         dbo.Table_QuestionnaireAnswers " + _
" WHERE     (Id = " + questionID.ToString + ") AND (UserName = N'" + userName + "')) is null then 0 else (SELECT     Answer AS AnswerId " + _
" FROM         dbo.Table_QuestionnaireAnswers " + _
" WHERE     (Id = " + questionID.ToString + ") AND (UserName = N'" + userName + "')) end "

    Dim cmd As New SqlCommand(sqlstring, con)
        cmd.CommandType = System.Data.CommandType.Text
        Dim dr As SqlDataReader = cmd.ExecuteReader
        Dim AnswerID As Integer = 0
        While dr.Read
            AnswerID = Convert.ToInt32(dr(0).ToString())
        End While
        con.Close()
        dr.Close()
        Return AnswerID
    End Function

    Protected Sub updateAnswer(ByVal QuestionID As String, ByVal UserName As String, ByVal AnswerID As String)
        ' insert Answer if doesnt exist
        Dim con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
        con.Open()
        Dim sqlstring As String = " SELECT     COUNT(Answer) AS Expr1 " +
" FROM         dbo.Table_QuestionnaireAnswers " +
" WHERE     (Id = " + QuestionID + ") AND (UserName = N'" + UserName + "') "

        Dim cmd As New SqlCommand(sqlstring, con)
        cmd.CommandType = System.Data.CommandType.Text
        Dim dr As SqlDataReader = cmd.ExecuteReader
        Dim AnswerExist As Boolean = False
        While dr.Read
            If dr(0) = 0 Then
                AnswerExist = False
            Else
                AnswerExist = True
            End If
        End While
        con.Close()
        dr.Close()

        If Not AnswerExist Then
            Dim con2 As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con2.Open()
            Dim sqlstring2 As String = " INSERT INTO [Table_QuestionnaireAnswers] " +
                                              "            ([Id] " +
                                              "            ,[UserName] " +
                                              "            ,[Answer]) " +
                                              "      VALUES " +
                                              "            ( " + QuestionID + "" +
                                              "            , " + "N'" + UserName + "'" + "" +
                                              "            ," + AnswerID + ") "

            Dim cmd2 As New SqlCommand(sqlstring2, con2)
            cmd2.CommandType = System.Data.CommandType.Text
            Dim dr2 As SqlDataReader = cmd2.ExecuteReader
            con2.Close()
            dr2.Close()
        Else
            'update Answer if already exist
            Dim con2 As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con2.Open()
            Dim sqlstring2 As String = " UPDATE [Table_QuestionnaireAnswers] " +
                                                  "    SET [Answer] = " + AnswerID + "  " +
                                                  "  WHERE  Id = " + QuestionID + " AND UserName = " + "N'" + UserName + "'"
            Dim cmd2 As New SqlCommand(sqlstring2, con2)
            cmd2.CommandType = System.Data.CommandType.Text
            Dim dr2 As SqlDataReader = cmd2.ExecuteReader
            con2.Close()
            dr2.Close()
        End If
    End Sub

    Protected Sub Do_AllRowCommandForGridview(ByVal row As GridViewRow, ByVal CommandName As String, ByVal CommandArgument As String)
        Dim ImageButtonNone As ImageButton = DirectCast(row.FindControl("ImageButtonNone"), ImageButton)
        Dim ImageButtonMust As ImageButton = DirectCast(row.FindControl("ImageButtonMust"), ImageButton)
        Dim ImageButtonNice As ImageButton = DirectCast(row.FindControl("ImageButtonNice"), ImageButton)
        Dim ImageButtonNot As ImageButton = DirectCast(row.FindControl("ImageButtonNot"), ImageButton)

        If (CommandName = "None") Then
            updateAnswer(CommandArgument.ToString, "tatiana", "0")
            ImageButtonNone.ImageUrl = "~/images/RadioNone.png"
            ImageButtonMust.ImageUrl = "~/images/RadioUnChecked.png"
            ImageButtonNice.ImageUrl = "~/images/RadioUnChecked.png"
            ImageButtonNot.ImageUrl = "~/images/RadioUnChecked.png"
        End If
        If (CommandName = "Must") Then
            updateAnswer(CommandArgument.ToString, "tatiana", "1")
            ImageButtonNone.ImageUrl = "~/images/RadioUnChecked.png"
            ImageButtonMust.ImageUrl = "~/images/RadioChecked.png"
            ImageButtonNice.ImageUrl = "~/images/RadioUnChecked.png"
            ImageButtonNot.ImageUrl = "~/images/RadioUnChecked.png"
        End If
        If (CommandName = "Nice") Then
            updateAnswer(CommandArgument.ToString, "tatiana", "2")
            ImageButtonNone.ImageUrl = "~/images/RadioUnChecked.png"
            ImageButtonMust.ImageUrl = "~/images/RadioUnChecked.png"
            ImageButtonNice.ImageUrl = "~/images/RadioChecked.png"
            ImageButtonNot.ImageUrl = "~/images/RadioUnChecked.png"
        End If
        If (CommandName = "Not") Then
            updateAnswer(CommandArgument.ToString, "tatiana", "3")
            ImageButtonNone.ImageUrl = "~/images/RadioUnChecked.png"
            ImageButtonMust.ImageUrl = "~/images/RadioUnChecked.png"
            ImageButtonNice.ImageUrl = "~/images/RadioUnChecked.png"
            ImageButtonNot.ImageUrl = "~/images/RadioChecked.png"
        End If
    End Sub

    Protected Sub Do_AllRowDataRowBoundForGridview(ByVal row As GridViewRow)
        If Not IsPostBack Then
            If row.RowType = DataControlRowType.DataRow Then
                Dim ImageButtonNone As ImageButton = DirectCast(row.FindControl("ImageButtonNone"), ImageButton)
                Dim ImageButtonMust As ImageButton = DirectCast(row.FindControl("ImageButtonMust"), ImageButton)
                Dim ImageButtonNice As ImageButton = DirectCast(row.FindControl("ImageButtonNice"), ImageButton)
                Dim ImageButtonNot As ImageButton = DirectCast(row.FindControl("ImageButtonNot"), ImageButton)
                Dim application As Label = DirectCast(row.FindControl("application"), Label)
                Dim WhatItOffers As Label = DirectCast(row.FindControl("WhatItOffers"), Label)

                application.Text = Replace(Mid(application.Text, 1, InStr(application.Text, "-")) + "<br /><br />" + "<i>" + Mid(application.Text, InStr(application.Text, "-") + 1, Len(application.Text) - InStr(application.Text, "-")), "-", "") + "</i>"
                WhatItOffers.Text = Replace(Mid(WhatItOffers.Text, 1, InStr(WhatItOffers.Text, "-")) + "<br /><br />" + "<i>" + Mid(WhatItOffers.Text, InStr(WhatItOffers.Text, "-") + 1, Len(WhatItOffers.Text) - InStr(WhatItOffers.Text, "-")), "-", "") + "</i>"

                If getAnswerID(DataBinder.Eval(row.DataItem, "ID"), "tatiana") = 0 Then
                    ImageButtonNone.ImageUrl = "~/images/RadioNone.png"
                    ImageButtonMust.ImageUrl = "~/images/RadioUnChecked.png"
                    ImageButtonNice.ImageUrl = "~/images/RadioUnChecked.png"
                    ImageButtonNot.ImageUrl = "~/images/RadioUnChecked.png"
                ElseIf getAnswerID(DataBinder.Eval(row.DataItem, "ID"), "tatiana") = 1 Then
                    ImageButtonNone.ImageUrl = "~/images/RadioUnChecked.png"
                    ImageButtonMust.ImageUrl = "~/images/RadioChecked.png"
                    ImageButtonNice.ImageUrl = "~/images/RadioUnChecked.png"
                    ImageButtonNot.ImageUrl = "~/images/RadioUnChecked.png"
                ElseIf getAnswerID(DataBinder.Eval(row.DataItem, "ID"), "tatiana") = 2 Then
                    ImageButtonNone.ImageUrl = "~/images/RadioUnChecked.png"
                    ImageButtonMust.ImageUrl = "~/images/RadioUnChecked.png"
                    ImageButtonNice.ImageUrl = "~/images/RadioChecked.png"
                    ImageButtonNot.ImageUrl = "~/images/RadioUnChecked.png"
                ElseIf getAnswerID(DataBinder.Eval(row.DataItem, "ID"), "tatiana") = 3 Then
                    ImageButtonNone.ImageUrl = "~/images/RadioUnChecked.png"
                    ImageButtonMust.ImageUrl = "~/images/RadioUnChecked.png"
                    ImageButtonNice.ImageUrl = "~/images/RadioUnChecked.png"
                    ImageButtonNot.ImageUrl = "~/images/RadioChecked.png"
                End If

                If row.RowType = DataControlRowType.DataRow Then
                    If InStr(application.Text, "Report  development") > 0 Then
                        row.Visible = False
                    End If
                End If


            End If
        End If
    End Sub

    Protected Sub Page_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        ' ScriptManager1.SupportsPartialRendering = False
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Or IsPostBack Then
            SqlDataSourceYourBusinessNeeds2.SelectParameters("UserName").DefaultValue = "tatiana"
        End If

        ' do a loop to provide titles for each gridview
        For i = 1 To 4
            Dim con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " SELECT RTRIM(GroupName) AS GroupName FROM dbo.Table_QuestionnaireGroupName  WHERE (GroupNameId = " + i.ToString + ")"
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = System.Data.CommandType.Text
            Dim dr As SqlDataReader = cmd.ExecuteReader
            Dim Title As String = ""
            While dr.Read
                Title = dr(0).ToString
            End While
            con.Close()
            dr.Close()

            If i = 1 Then
                labelTitle1.Text = Title
            ElseIf i = 2 Then
                labelTitle2.Text = Title
            ElseIf i = 3 Then
                labelTitle3.Text = Title
            ElseIf i = 4 Then
                labelTitle4.Text = Title
            End If


        Next
    End Sub

    Protected Sub ButtonSaveYourBusinessNeeds_Click(sender As Object, e As System.EventArgs) Handles ButtonSaveYourBusinessNeeds.Click
        For Each row As GridViewRow In GridViewYourBusinessNeeds.Rows
            If row.RowType = DataControlRowType.DataRow Then
                Dim TextBoxDescription As TextBox = DirectCast(row.FindControl("TextBoxDescription"), TextBox)
                Dim TextBoxSolution As TextBox = DirectCast(row.FindControl("TextBoxSolution"), TextBox)
                Dim TextBoxBenefit As TextBox = DirectCast(row.FindControl("TextBoxBenefit"), TextBox)
                Dim TextBoxBenefitInExpence As TextBox = DirectCast(row.FindControl("TextBoxBenefitInExpence"), TextBox)
                Dim labelID As Label = DirectCast(row.FindControl("labelID"), Label)
                Dim labelWarningDescription As Label = DirectCast(row.FindControl("labelWarningDescription"), Label)
                Dim labelWarningSolution As Label = DirectCast(row.FindControl("labelWarningSolution"), Label)

                If Not String.IsNullOrEmpty(TextBoxDescription.Text) _
          OrElse Not String.IsNullOrEmpty(TextBoxSolution.Text) _
          OrElse Not String.IsNullOrEmpty(TextBoxBenefit.Text) _
          OrElse Not String.IsNullOrEmpty(TextBoxBenefitInExpence.Text) Then

                    If String.IsNullOrEmpty(TextBoxDescription.Text) Then
                        labelWarningDescription.Visible = True
                    Else
                        labelWarningDescription.Visible = False
                    End If

                    If String.IsNullOrEmpty(TextBoxSolution.Text) Then
                        labelWarningSolution.Visible = True
                    Else
                        labelWarningSolution.Visible = False
                    End If

                    If Not String.IsNullOrEmpty(TextBoxDescription.Text) AndAlso Not String.IsNullOrEmpty(TextBoxSolution.Text) Then
                        ' ready to INSERT ot UPDATE
                        If Convert.ToInt32(labelID.Text) = 0 Then
                            ' INSERT
                            InsertYourBusinessNeed(TextBoxDescription.Text, TextBoxSolution.Text, TextBoxBenefit.Text, TextBoxBenefitInExpence.Text)
                        Else
                            ' UPDATE
                            UpdateYourBusinessNeed(labelID.Text, TextBoxDescription.Text, TextBoxSolution.Text, TextBoxBenefit.Text, TextBoxBenefitInExpence.Text)
                        End If
                    End If

                ElseIf String.IsNullOrEmpty(TextBoxDescription.Text) _
          AndAlso String.IsNullOrEmpty(TextBoxSolution.Text) _
          AndAlso String.IsNullOrEmpty(TextBoxBenefit.Text) _
          AndAlso String.IsNullOrEmpty(TextBoxBenefitInExpence.Text) Then

                    If Convert.ToInt32(labelID.Text) <> 0 Then
                        'DELETE comment
                        DeleteYourBusinessNeed(labelID.Text)
                    End If
                End If
            End If
        Next

    End Sub

    Protected Sub InsertYourBusinessNeed(ByVal Description As String _
                                       , ByVal solution As String _
                                       , ByVal benefit As String _
                                       , ByVal BenefitInExpence As String)
        Dim con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
        con.Open()
        Dim sqlstring As String = " INSERT INTO [Table_QuestionnaireYourBusinessNeeds] " +
                                            "            ([UserName] " +
                                            "            ,[Description] " +
                                            "            ,[Solution] " +
                                            "            ,[Benefit] " +
                                            "            ,[BenefitInExpence]) " +
                                            "      VALUES " +
                                            "            ( N'" + "tatiana" + "'" +
                                            "            , N'" + Description + "' " +
                                            "            , N'" + solution + "' " +
                                            "            , N'" + benefit + "' " +
                                            "            , N'" + BenefitInExpence + "') "

        Dim cmd As New SqlCommand(sqlstring, con)
        cmd.CommandType = System.Data.CommandType.Text
        Dim dr As SqlDataReader = cmd.ExecuteReader
        con.Close()
        dr.Close()
    End Sub

    Protected Sub UpdateYourBusinessNeed(ByVal ID As String _
                                       , ByVal Description As String _
                                     , ByVal solution As String _
                                     , ByVal benefit As String _
                                     , ByVal BenefitInExpence As String)
        Dim con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
        con.Open()
        Dim sqlstring As String = " UPDATE [Table_QuestionnaireYourBusinessNeeds] " +
                                    "    SET [UserName] =  " + "N'" + "tatiana" + "'" +
                                    "       ,[Description] =  " + "N'" + Description + "'" +
                                    "       ,[Solution] =  " + "N'" + solution + "'" +
                                    "       ,[Benefit] =  " + "N'" + benefit + "'" +
                                    "       ,[BenefitInExpence] =  " + "N'" + BenefitInExpence + "'" +
                                    "  WHERE ID =  " + ID

        Dim cmd As New SqlCommand(sqlstring, con)
        cmd.CommandType = System.Data.CommandType.Text
        Dim dr As SqlDataReader = cmd.ExecuteReader
        con.Close()
        dr.Close()
    End Sub

    Protected Sub DeleteYourBusinessNeed(ByVal ID As String)
        Dim con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
        con.Open()
        Dim sqlstring As String = " DELETE FROM [Table_QuestionnaireYourBusinessNeeds]  WHERE ID =  " + ID
        Dim cmd As New SqlCommand(sqlstring, con)
        cmd.CommandType = System.Data.CommandType.Text
        Dim dr As SqlDataReader = cmd.ExecuteReader
    con.Close()
    dr.Close()
  End Sub


End Class
