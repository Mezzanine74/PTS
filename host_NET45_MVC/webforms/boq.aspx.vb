Partial Class boq
    Inherits System.Web.UI.Page

    Dim StartPosition As Integer
    Dim EndPosition As Integer
    Dim OccurenceOfSpace As Integer
    Dim sayac As Integer = 0
    Dim DonguBiter As Boolean = False

    Dim StartPosition2 As Integer
    Dim EndPosition2 As Integer
    Dim OccurenceOfSpace2 As Integer
    Dim sayac2 As Integer = 0
    Dim DonguBiter2 As Boolean = False



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load





        Me.Form.DefaultButton = Me.Button1.UniqueID
    End Sub


    Protected Sub GridViewMonitor_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridViewMonitor.Load

        If IsPostBack Then

            SqlDataSourceMonitor.SelectParameters.Clear()
            SqlDataSourceMonitor.SelectCommand = "SELECT * FROM [View_TenderBOQ] WHERE Description LIKE '%' + @"

            While DonguBiter = False

                If sayac = 0 Then
                    StartPosition = 0
                Else
                    StartPosition = OccurenceOfSpace
                End If

                EndPosition = InStr(StartPosition + 1, Trim(TextBoxSearch.Text), " ")
                OccurenceOfSpace = EndPosition
                sayac = sayac + 1

                If EndPosition = 0 Then
                    If sayac = 1 Then
                        SqlDataSourceMonitor.SelectParameters.Add(Mid(Trim(TextBoxSearch.Text), StartPosition + 1, Len(Trim(TextBoxSearch.Text)) - StartPosition).ToString + sayac.ToString, TypeCode.String.ToString())
                        SqlDataSourceMonitor.SelectParameters(Mid(Trim(TextBoxSearch.Text), StartPosition + 1, Len(Trim(TextBoxSearch.Text)) - StartPosition).ToString + sayac.ToString).DefaultValue = Mid(Trim(TextBoxSearch.Text), StartPosition + 1, Len(Trim(TextBoxSearch.Text)) - StartPosition).ToString
                        SqlDataSourceMonitor.SelectParameters(Mid(Trim(TextBoxSearch.Text), StartPosition + 1, Len(Trim(TextBoxSearch.Text)) - StartPosition).ToString + sayac.ToString).Type = TypeCode.String
                        SqlDataSourceMonitor.SelectCommand = SqlDataSourceMonitor.SelectCommand + Mid(Trim(TextBoxSearch.Text), StartPosition + 1, Len(Trim(TextBoxSearch.Text)) - StartPosition).ToString + sayac.ToString + " + '%'"
                    Else
                        SqlDataSourceMonitor.SelectParameters.Add(Mid(Trim(TextBoxSearch.Text), StartPosition + 1, Len(Trim(TextBoxSearch.Text)) - StartPosition).ToString + sayac.ToString, TypeCode.String.ToString())
                        SqlDataSourceMonitor.SelectParameters(Mid(Trim(TextBoxSearch.Text), StartPosition + 1, Len(Trim(TextBoxSearch.Text)) - StartPosition).ToString + sayac.ToString).DefaultValue = Mid(Trim(TextBoxSearch.Text), StartPosition + 1, Len(Trim(TextBoxSearch.Text)) - StartPosition).ToString
                        SqlDataSourceMonitor.SelectParameters(Mid(Trim(TextBoxSearch.Text), StartPosition + 1, Len(Trim(TextBoxSearch.Text)) - StartPosition).ToString + sayac.ToString).Type = TypeCode.String
                        SqlDataSourceMonitor.SelectCommand = SqlDataSourceMonitor.SelectCommand + " AND Description LIKE '%' + @" + Mid(Trim(TextBoxSearch.Text), StartPosition + 1, Len(Trim(TextBoxSearch.Text)) - StartPosition).ToString + sayac.ToString + " + '%'"
                    End If
                    DonguBiter = True
                Else
                    If sayac = 1 Then
                        SqlDataSourceMonitor.SelectParameters.Add(Mid(Trim(TextBoxSearch.Text), StartPosition + 1, EndPosition - StartPosition - 1).ToString + sayac.ToString, TypeCode.String.ToString())
                        SqlDataSourceMonitor.SelectParameters(Mid(Trim(TextBoxSearch.Text), StartPosition + 1, EndPosition - StartPosition - 1).ToString + sayac.ToString).DefaultValue = Mid(Trim(TextBoxSearch.Text), StartPosition + 1, EndPosition - StartPosition - 1).ToString
                        SqlDataSourceMonitor.SelectParameters(Mid(Trim(TextBoxSearch.Text), StartPosition + 1, EndPosition - StartPosition - 1).ToString + sayac.ToString).Type = TypeCode.String
                        SqlDataSourceMonitor.SelectCommand = SqlDataSourceMonitor.SelectCommand + Mid(Trim(TextBoxSearch.Text), StartPosition + 1, EndPosition - StartPosition - 1).ToString + sayac.ToString + " + '%'"
                    Else
                        SqlDataSourceMonitor.SelectParameters.Add(Mid(Trim(TextBoxSearch.Text), StartPosition + 1, EndPosition - StartPosition - 1).ToString + sayac.ToString, TypeCode.String.ToString())
                        SqlDataSourceMonitor.SelectParameters(Mid(Trim(TextBoxSearch.Text), StartPosition + 1, EndPosition - StartPosition - 1).ToString + sayac.ToString).DefaultValue = Mid(Trim(TextBoxSearch.Text), StartPosition + 1, EndPosition - StartPosition - 1).ToString
                        SqlDataSourceMonitor.SelectParameters(Mid(Trim(TextBoxSearch.Text), StartPosition + 1, EndPosition - StartPosition - 1).ToString + sayac.ToString).Type = TypeCode.String
                        SqlDataSourceMonitor.SelectCommand = SqlDataSourceMonitor.SelectCommand + " AND Description LIKE '%' + @" + Mid(Trim(TextBoxSearch.Text), StartPosition + 1, EndPosition - StartPosition - 1).ToString + sayac.ToString + " + '%'"
                    End If
                End If
            End While
        End If

        If Not IsPostBack Then
            GridViewMonitor.Sort("Tender", SortDirection.Descending)
        End If

    End Sub


    Protected Sub GridViewMonitor_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridViewMonitor.RowCommand

    End Sub

    Protected Sub GridViewMonitor_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewMonitor.RowDataBound
        Dim row As GridViewRow
        For Each row In GridViewMonitor.Rows
            'it will be used for currency icons
            'If DirectCast(row.FindControl("LabelLinkInvoice"), Label) IsNot Nothing Then
            'Dim path As String = Server.MapPath(DirectCast(row.FindControl("LabelLinkInvoice"), Label).Text.ToString)
            'Dim file As System.IO.FileInfo = New System.IO.FileInfo(path)
            'If file.Exists Then
            'DirectCast(row.FindControl("ImageButtonPdf"), ImageButton).ImageUrl = "~/Images/pdf.bmp"
            'Else
            'DirectCast(row.FindControl("ImageButtonPdf"), ImageButton).ImageUrl = "~/Images/pdf_bw.bmp"
            'End If
            'End If
        Next

        '************

        '*************
        Dim row2 As GridViewRow
        For Each row2 In GridViewMonitor.Rows
            If DirectCast(row2.FindControl("LabelDescription"), Label) IsNot Nothing Then
                While DonguBiter2 = False

                    If sayac2 = 0 Then
                        StartPosition2 = 0
                    Else
                        StartPosition2 = OccurenceOfSpace2
                    End If

                    EndPosition2 = InStr(StartPosition2 + 1, Trim(TextBoxSearch.Text), " ")
                    OccurenceOfSpace2 = EndPosition2
                    sayac2 = sayac2 + 1

                    If EndPosition2 = 0 Then
                        If sayac2 = 1 Then
                            DirectCast(row2.FindControl("LabelDescription"), Label).Text = Regex.Replace(DirectCast(row2.FindControl("LabelDescription"), Label).Text, Mid(Trim(TextBoxSearch.Text), StartPosition2 + 1, Len(Trim(TextBoxSearch.Text)) - StartPosition2).ToString, "<span style=""background-color: #FFFF00;"">" & Mid(Trim(TextBoxSearch.Text), StartPosition2 + 1, Len(Trim(TextBoxSearch.Text)) - StartPosition2).ToString & "</span>", RegexOptions.IgnoreCase)
                        Else
                            Select Case sayac2
                                Case 2
                                    DirectCast(row2.FindControl("LabelDescription"), Label).Text = Regex.Replace(DirectCast(row2.FindControl("LabelDescription"), Label).Text, Mid(Trim(TextBoxSearch.Text), StartPosition2 + 1, Len(Trim(TextBoxSearch.Text)) - StartPosition2).ToString, "<span style=""background-color: #ADFF2F;"">" & Mid(Trim(TextBoxSearch.Text), StartPosition2 + 1, Len(Trim(TextBoxSearch.Text)) - StartPosition2).ToString & "</span>", RegexOptions.IgnoreCase)
                                Case 3
                                    DirectCast(row2.FindControl("LabelDescription"), Label).Text = Regex.Replace(DirectCast(row2.FindControl("LabelDescription"), Label).Text, Mid(Trim(TextBoxSearch.Text), StartPosition2 + 1, Len(Trim(TextBoxSearch.Text)) - StartPosition2).ToString, "<span style=""background-color: #FFB6C1;"">" & Mid(Trim(TextBoxSearch.Text), StartPosition2 + 1, Len(Trim(TextBoxSearch.Text)) - StartPosition2).ToString & "</span>", RegexOptions.IgnoreCase)
                                Case 4
                                    DirectCast(row2.FindControl("LabelDescription"), Label).Text = Regex.Replace(DirectCast(row2.FindControl("LabelDescription"), Label).Text, Mid(Trim(TextBoxSearch.Text), StartPosition2 + 1, Len(Trim(TextBoxSearch.Text)) - StartPosition2).ToString, "<span style=""background-color: #87CEFA;"">" & Mid(Trim(TextBoxSearch.Text), StartPosition2 + 1, Len(Trim(TextBoxSearch.Text)) - StartPosition2).ToString & "</span>", RegexOptions.IgnoreCase)
                                Case 5
                                    DirectCast(row2.FindControl("LabelDescription"), Label).Text = Regex.Replace(DirectCast(row2.FindControl("LabelDescription"), Label).Text, Mid(Trim(TextBoxSearch.Text), StartPosition2 + 1, Len(Trim(TextBoxSearch.Text)) - StartPosition2).ToString, "<span style=""background-color: #DA70D6;"">" & Mid(Trim(TextBoxSearch.Text), StartPosition2 + 1, Len(Trim(TextBoxSearch.Text)) - StartPosition2).ToString & "</span>", RegexOptions.IgnoreCase)
                                Case 6
                                    DirectCast(row2.FindControl("LabelDescription"), Label).Text = Regex.Replace(DirectCast(row2.FindControl("LabelDescription"), Label).Text, Mid(Trim(TextBoxSearch.Text), StartPosition2 + 1, Len(Trim(TextBoxSearch.Text)) - StartPosition2).ToString, "<span style=""background-color: #FF0000;"">" & Mid(Trim(TextBoxSearch.Text), StartPosition2 + 1, Len(Trim(TextBoxSearch.Text)) - StartPosition2).ToString & "</span>", RegexOptions.IgnoreCase)
                                Case 7
                                    DirectCast(row2.FindControl("LabelDescription"), Label).Text = Regex.Replace(DirectCast(row2.FindControl("LabelDescription"), Label).Text, Mid(Trim(TextBoxSearch.Text), StartPosition2 + 1, Len(Trim(TextBoxSearch.Text)) - StartPosition2).ToString, "<span style=""background-color: #8FBC8B;"">" & Mid(Trim(TextBoxSearch.Text), StartPosition2 + 1, Len(Trim(TextBoxSearch.Text)) - StartPosition2).ToString & "</span>", RegexOptions.IgnoreCase)
                                Case 8
                                    DirectCast(row2.FindControl("LabelDescription"), Label).Text = Regex.Replace(DirectCast(row2.FindControl("LabelDescription"), Label).Text, Mid(Trim(TextBoxSearch.Text), StartPosition2 + 1, Len(Trim(TextBoxSearch.Text)) - StartPosition2).ToString, "<span style=""background-color: #1E90FF;"">" & Mid(Trim(TextBoxSearch.Text), StartPosition2 + 1, Len(Trim(TextBoxSearch.Text)) - StartPosition2).ToString & "</span>", RegexOptions.IgnoreCase)
                                Case 9
                                    DirectCast(row2.FindControl("LabelDescription"), Label).Text = Regex.Replace(DirectCast(row2.FindControl("LabelDescription"), Label).Text, Mid(Trim(TextBoxSearch.Text), StartPosition2 + 1, Len(Trim(TextBoxSearch.Text)) - StartPosition2).ToString, "<span style=""background-color: #FF8C00;"">" & Mid(Trim(TextBoxSearch.Text), StartPosition2 + 1, Len(Trim(TextBoxSearch.Text)) - StartPosition2).ToString & "</span>", RegexOptions.IgnoreCase)
                                Case 10
                                    DirectCast(row2.FindControl("LabelDescription"), Label).Text = Regex.Replace(DirectCast(row2.FindControl("LabelDescription"), Label).Text, Mid(Trim(TextBoxSearch.Text), StartPosition2 + 1, Len(Trim(TextBoxSearch.Text)) - StartPosition2).ToString, "<span style=""background-color: #FFD700;"">" & Mid(Trim(TextBoxSearch.Text), StartPosition2 + 1, Len(Trim(TextBoxSearch.Text)) - StartPosition2).ToString & "</span>", RegexOptions.IgnoreCase)
                            End Select
                        End If
                        DonguBiter2 = True
                    Else
                        If sayac2 = 1 Then
                            DirectCast(row2.FindControl("LabelDescription"), Label).Text = Regex.Replace(DirectCast(row2.FindControl("LabelDescription"), Label).Text, Mid(Trim(TextBoxSearch.Text), StartPosition2 + 1, EndPosition2 - StartPosition2 - 1).ToString, "<span style=""background-color: #FFFF00;"">" & Mid(Trim(TextBoxSearch.Text), StartPosition2 + 1, EndPosition2 - StartPosition2 - 1).ToString & "</span>", RegexOptions.IgnoreCase)
                        Else
                            Select Case sayac2
                                Case 2
                                    DirectCast(row2.FindControl("LabelDescription"), Label).Text = Regex.Replace(DirectCast(row2.FindControl("LabelDescription"), Label).Text, Mid(Trim(TextBoxSearch.Text), StartPosition2 + 1, EndPosition2 - StartPosition2 - 1).ToString, "<span style=""background-color: #ADFF2F;"">" & Mid(Trim(TextBoxSearch.Text), StartPosition2 + 1, EndPosition2 - StartPosition2 - 1).ToString & "</span>", RegexOptions.IgnoreCase)
                                Case 3
                                    DirectCast(row2.FindControl("LabelDescription"), Label).Text = Regex.Replace(DirectCast(row2.FindControl("LabelDescription"), Label).Text, Mid(Trim(TextBoxSearch.Text), StartPosition2 + 1, EndPosition2 - StartPosition2 - 1).ToString, "<span style=""background-color: #FFB6C1;"">" & Mid(Trim(TextBoxSearch.Text), StartPosition2 + 1, EndPosition2 - StartPosition2 - 1).ToString & "</span>", RegexOptions.IgnoreCase)
                                Case 4
                                    DirectCast(row2.FindControl("LabelDescription"), Label).Text = Regex.Replace(DirectCast(row2.FindControl("LabelDescription"), Label).Text, Mid(Trim(TextBoxSearch.Text), StartPosition2 + 1, EndPosition2 - StartPosition2 - 1).ToString, "<span style=""background-color: #87CEFA;"">" & Mid(Trim(TextBoxSearch.Text), StartPosition2 + 1, EndPosition2 - StartPosition2 - 1).ToString & "</span>", RegexOptions.IgnoreCase)
                                Case 5
                                    DirectCast(row2.FindControl("LabelDescription"), Label).Text = Regex.Replace(DirectCast(row2.FindControl("LabelDescription"), Label).Text, Mid(Trim(TextBoxSearch.Text), StartPosition2 + 1, EndPosition2 - StartPosition2 - 1).ToString, "<span style=""background-color: #DA70D6;"">" & Mid(Trim(TextBoxSearch.Text), StartPosition2 + 1, EndPosition2 - StartPosition2 - 1).ToString & "</span>", RegexOptions.IgnoreCase)
                                Case 6
                                    DirectCast(row2.FindControl("LabelDescription"), Label).Text = Regex.Replace(DirectCast(row2.FindControl("LabelDescription"), Label).Text, Mid(Trim(TextBoxSearch.Text), StartPosition2 + 1, EndPosition2 - StartPosition2 - 1).ToString, "<span style=""background-color: #FF0000;"">" & Mid(Trim(TextBoxSearch.Text), StartPosition2 + 1, EndPosition2 - StartPosition2 - 1).ToString & "</span>", RegexOptions.IgnoreCase)
                                Case 7
                                    DirectCast(row2.FindControl("LabelDescription"), Label).Text = Regex.Replace(DirectCast(row2.FindControl("LabelDescription"), Label).Text, Mid(Trim(TextBoxSearch.Text), StartPosition2 + 1, EndPosition2 - StartPosition2 - 1).ToString, "<span style=""background-color: #8FBC8B;"">" & Mid(Trim(TextBoxSearch.Text), StartPosition2 + 1, EndPosition2 - StartPosition2 - 1).ToString & "</span>", RegexOptions.IgnoreCase)
                                Case 8
                                    DirectCast(row2.FindControl("LabelDescription"), Label).Text = Regex.Replace(DirectCast(row2.FindControl("LabelDescription"), Label).Text, Mid(Trim(TextBoxSearch.Text), StartPosition2 + 1, EndPosition2 - StartPosition2 - 1).ToString, "<span style=""background-color: #1E90FF;"">" & Mid(Trim(TextBoxSearch.Text), StartPosition2 + 1, EndPosition2 - StartPosition2 - 1).ToString & "</span>", RegexOptions.IgnoreCase)
                                Case 9
                                    DirectCast(row2.FindControl("LabelDescription"), Label).Text = Regex.Replace(DirectCast(row2.FindControl("LabelDescription"), Label).Text, Mid(Trim(TextBoxSearch.Text), StartPosition2 + 1, EndPosition2 - StartPosition2 - 1).ToString, "<span style=""background-color: #FF8C00;"">" & Mid(Trim(TextBoxSearch.Text), StartPosition2 + 1, EndPosition2 - StartPosition2 - 1).ToString & "</span>", RegexOptions.IgnoreCase)
                                Case 10
                                    DirectCast(row2.FindControl("LabelDescription"), Label).Text = Regex.Replace(DirectCast(row2.FindControl("LabelDescription"), Label).Text, Mid(Trim(TextBoxSearch.Text), StartPosition2 + 1, EndPosition2 - StartPosition2 - 1).ToString, "<span style=""background-color: #FFD700;"">" & Mid(Trim(TextBoxSearch.Text), StartPosition2 + 1, EndPosition2 - StartPosition2 - 1).ToString & "</span>", RegexOptions.IgnoreCase)
                            End Select
                        End If
                    End If
                End While
                sayac2 = 0
                DonguBiter2 = False
            End If
        Next

        ' It fixes column width problem
        Dim row1 As GridViewRow
        For Each row1 In GridViewMonitor.Rows
            Dim Label1 As Label = DirectCast(row1.FindControl("LabelDescription"), Label)

            If Label1 IsNot Nothing Then
                Label1.Text = Label1.Text.Replace(",", "," + " ")
            End If
        Next

        'it defines CurrencyImage.
        If DirectCast(e.Row.FindControl("LabelCurrency1"), Label) IsNot Nothing Then
            If DirectCast(e.Row.FindControl("LabelCurrency1"), Label).Text = "Rub" Then
                DirectCast(e.Row.FindControl("ImageCurrency1"), Image).ImageUrl = "~/Images/ruble_icon_.bmp"
            ElseIf DirectCast(e.Row.FindControl("LabelCurrency1"), Label).Text = "Dollar" Then
                DirectCast(e.Row.FindControl("ImageCurrency1"), Image).ImageUrl = "~/Images/dollar_icon_.bmp"
            ElseIf DirectCast(e.Row.FindControl("LabelCurrency1"), Label).Text = "Euro" Then
                DirectCast(e.Row.FindControl("ImageCurrency1"), Image).ImageUrl = "~/Images/euro_icon_.bmp"
            End If
            e.Row.Cells(5).BackColor = System.Drawing.Color.Bisque
            e.Row.Cells(7).BackColor = System.Drawing.Color.LightSkyBlue
        End If

        If DirectCast(e.Row.FindControl("LabelCurrency2"), Label) IsNot Nothing Then
            If DirectCast(e.Row.FindControl("LabelCurrency2"), Label).Text = "Rub" Then
                DirectCast(e.Row.FindControl("ImageCurrency2"), Image).ImageUrl = "~/Images/ruble_icon_.bmp"
            ElseIf DirectCast(e.Row.FindControl("LabelCurrency2"), Label).Text = "Dollar" Then
                DirectCast(e.Row.FindControl("ImageCurrency2"), Image).ImageUrl = "~/Images/dollar_icon_.bmp"
            ElseIf DirectCast(e.Row.FindControl("LabelCurrency2"), Label).Text = "Euro" Then
                DirectCast(e.Row.FindControl("ImageCurrency2"), Image).ImageUrl = "~/Images/euro_icon_.bmp"
            End If
        End If

    End Sub

    Protected Sub SqlDataSourceMonitor_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceSelectingEventArgs) Handles SqlDataSourceMonitor.Selecting
        e.Command.CommandTimeout = 120
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click

    End Sub

    Protected Sub SqlDataSourceMonitor_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs) Handles SqlDataSourceMonitor.Selected

    End Sub
End Class
