Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Web.UI.WebControls

Public Class VacationTables

    Shared Function Table_EmployeeJunction_Row_ById(ByVal _id As Integer) As Vacation.Table_EmployeeJunctionRow

        Using adapter As New VacationTableAdapters.Table_EmployeeJunctionTableAdapter
            Dim Table As Vacation.Table_EmployeeJunctionDataTable
            Table = adapter.GetDataById(_id)
            For Each _row As Vacation.Table_EmployeeJunctionRow In Table
                Return _row
                _row = Nothing
            Next
            Table.Dispose()
            adapter.Dispose()
        End Using

    End Function

    Shared Function Table_Department_Row_ById(ByVal _id As Integer) As Vacation.Table_DepartmentRow

        Using adapter As New VacationTableAdapters.Table_DepartmentTableAdapter
            Dim Table As Vacation.Table_DepartmentDataTable
            Table = adapter.GetDataById(_id)
            For Each _row As Vacation.Table_DepartmentRow In Table
                Return _row
                _row = Nothing
            Next
            Table.Dispose()
            adapter.Dispose()
        End Using

    End Function

    Shared Function Table_Department_Row_ByName(ByVal _Name As String) As Vacation.Table_DepartmentRow

        Using adapter As New VacationTableAdapters.Table_DepartmentTableAdapter
            Dim Table As Vacation.Table_DepartmentDataTable
            Table = adapter.GetDataByName(_Name)
            For Each _row As Vacation.Table_DepartmentRow In Table
                Return _row
                _row = Nothing
            Next
            Table.Dispose()
            adapter.Dispose()
        End Using

    End Function

    Shared Function Table_Division_Row_ById(ByVal _id As Integer) As Vacation.Table_DivisionRow

        Using adapter As New VacationTableAdapters.Table_DivisionTableAdapter
            Dim Table As Vacation.Table_DivisionDataTable
            Table = adapter.GetDataById(_id)
            For Each _row As Vacation.Table_DivisionRow In Table
                Return _row
                _row = Nothing
            Next
            Table.Dispose()
            adapter.Dispose()
        End Using

    End Function

    Shared Function Table_Division_Row_ByName(ByVal _Name As String) As Vacation.Table_DivisionRow

        Using adapter As New VacationTableAdapters.Table_DivisionTableAdapter
            Dim Table As Vacation.Table_DivisionDataTable
            Table = adapter.GetDataByName(_Name)
            For Each _row As Vacation.Table_DivisionRow In Table
                Return _row
                _row = Nothing
            Next
            Table.Dispose()
            adapter.Dispose()
        End Using

    End Function

    Shared Function Table_EmplyRank_Row_ById(ByVal _id As Integer) As Vacation.Table_EmplyRankRow

        Using adapter As New VacationTableAdapters.Table_EmplyRankTableAdapter
            Dim Table As Vacation.Table_EmplyRankDataTable
            Table = adapter.GetDataById(_id)
            For Each _row As Vacation.Table_EmplyRankRow In Table
                Return _row
                _row = Nothing
            Next
            Table.Dispose()
            adapter.Dispose()
        End Using

    End Function

    Shared Function Table_EmplyRank_Row_ByRank(ByVal _Rank As Integer) As Vacation.Table_EmplyRankRow

        Using adapter As New VacationTableAdapters.Table_EmplyRankTableAdapter
            Dim Table As Vacation.Table_EmplyRankDataTable
            Table = adapter.GetDataByRank(_Rank)
            For Each _row As Vacation.Table_EmplyRankRow In Table
                Return _row
                _row = Nothing
            Next
            Table.Dispose()
            adapter.Dispose()
        End Using

    End Function

    Shared Function Table_Position_Row_ById(ByVal _id As Integer) As Vacation.Table_PositionRow

        Using adapter As New VacationTableAdapters.Table_PositionTableAdapter
            Dim Table As Vacation.Table_PositionDataTable
            Table = adapter.GetDataById(_id)
            For Each _row As Vacation.Table_PositionRow In Table
                Return _row
                _row = Nothing
            Next
            Table.Dispose()
            adapter.Dispose()
        End Using

    End Function

    Shared Function Table_Position_Row_ByName(ByVal _Name As String) As Vacation.Table_PositionRow

        Using adapter As New VacationTableAdapters.Table_PositionTableAdapter
            Dim Table As Vacation.Table_PositionDataTable
            Table = adapter.GetDataByName(_Name)
            For Each _row As Vacation.Table_PositionRow In Table
                Return _row
                _row = Nothing
            Next
            Table.Dispose()
            adapter.Dispose()
        End Using

    End Function

    Shared Function aspnet_Membership_Row_ByLoweredEmail(ByVal _email As String) As Vacation.aspnet_MembershipRow

        Using adapter As New VacationTableAdapters.aspnet_MembershipTableAdapter
            Dim Table As Vacation.aspnet_MembershipDataTable
            Table = adapter.GetDataByLoweredEmail(_email)
            For Each _row As Vacation.aspnet_MembershipRow In Table
                Return _row
                _row = Nothing
            Next
            Table.Dispose()
            adapter.Dispose()
        End Using

    End Function

    Shared Function aspnet_Users_Row_ByUserId(ByVal _userId As Guid) As Vacation.aspnet_UsersRow

        Using adapter As New VacationTableAdapters.aspnet_UsersTableAdapter
            Dim Table As Vacation.aspnet_UsersDataTable
            Table = adapter.GetDataByUserId(_userId)
            For Each _row As Vacation.aspnet_UsersRow In Table
                Return _row
                _row = Nothing
            Next
            Table.Dispose()
            adapter.Dispose()
        End Using

    End Function

    Shared Function GetUserNameFromEmailAdress(ByVal _username As String) As String

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = "vac.SP_GetUserNameSurnameFromEmail"
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = Data.CommandType.StoredProcedure

            'syntax for parameter adding
            Dim UserParm As SqlParameter = cmd.Parameters.Add("@UserName", Data.SqlDbType.NVarChar, 256)
            UserParm.Value = _username
            Dim dr As SqlDataReader = cmd.ExecuteReader
            While dr.Read
                Return dr(0)
            End While

            con.Close()
            dr.Close()
        End Using

    End Function

    Shared Function GetCountOfNonApprovedRequest(ByVal _RequestId As Integer) As Integer

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = "vac.SP_GetCountOfNonApprovedRequest"
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = Data.CommandType.StoredProcedure

            'syntax for parameter adding
            Dim RequestId As SqlParameter = cmd.Parameters.Add("@RequestId", Data.SqlDbType.Int)
            RequestId.Value = _RequestId
            Dim dr As SqlDataReader = cmd.ExecuteReader
            While dr.Read
                Return dr(0)
            End While

            con.Close()
            dr.Close()
        End Using

    End Function

    Shared Sub ProcessComments(ByVal _Type As String, ByVal _Comment As String, ByVal _id As Integer)

        If _Type = "Employee" Then

            If Not String.IsNullOrEmpty(_Comment) Then

                ' there is user comment. insert if doestnn exist, update if exist
                Using Adapter As New VacationTableAdapters.Table_EmplyCommentTableAdapter

                    If Adapter.GetCountOfComment(_id) = 0 Then
                        ' INSERT
                        Adapter.Insert(Adapter.GetMaxId + 1, _id, _Comment, Date.Now)
                    Else
                        ' UPDATE
                        Adapter.Update(_Comment, Date.Now, _id)
                    End If

                    Adapter.Dispose()

                End Using

            Else
                ' DELETE
                Using Adapter As New VacationTableAdapters.Table_EmplyCommentTableAdapter

                    Adapter.Delete(_id)

                    Adapter.Dispose()

                End Using

            End If

        End If

        If _Type = "HR" Then

            If Not String.IsNullOrEmpty(_Comment) Then

                ' there is user comment. insert if doestnn exist, update if exist
                Using Adapter As New VacationTableAdapters.Table_HRCommentTableAdapter

                    If Adapter.GetCountOfComment(_id) = 0 Then
                        ' INSERT
                        Adapter.Insert(Adapter.GetMaxId + 1, _id, _Comment, Date.Now)

                    Else
                        ' UPDATE
                        Adapter.Update(_Comment, Date.Now, _id)

                    End If

                    Adapter.Dispose()

                End Using

            Else
                ' DELETE
                Using Adapter As New VacationTableAdapters.Table_HRCommentTableAdapter

                    Adapter.Delete(_id)

                    Adapter.Dispose()

                End Using

            End If

        End If

    End Sub

    Shared Sub DisableDDLWithoutDisable(ByVal _ddl As DropDownList)

        If _ddl IsNot Nothing Then
            _ddl.ForeColor = System.Drawing.Color.Red
            If _ddl.SelectedItem IsNot Nothing AndAlso _ddl.SelectedValue IsNot Nothing Then
                Dim li As New ListItem(_ddl.SelectedItem.Text, _ddl.SelectedValue, True)
                _ddl.Items.Clear()
                _ddl.Items.Add(li)
                li = Nothing
            End If
        End If

    End Sub

    Shared Function Table_EmplyCommentByRequestedID(ByVal _id As Integer) As Vacation.Table_EmplyCommentRow

        Using adapter As New VacationTableAdapters.Table_EmplyCommentTableAdapter
            Dim Table As Vacation.Table_EmplyCommentDataTable
            Table = adapter.GetDataByRequestId(_id)
            For Each _row As Vacation.Table_EmplyCommentRow In Table
                Return _row
                _row = Nothing
            Next
            Table.Dispose()
            adapter.Dispose()
        End Using

    End Function

    Shared Function Table_EmplyCommentGetCountOfComment(ByVal _id As Integer) As Integer

        Using Adapter As New VacationTableAdapters.Table_EmplyCommentTableAdapter

            Return Adapter.GetCountOfComment(_id)
            Adapter.Dispose()

        End Using

    End Function

    Shared Function Table_HRCommentByRequestedID(ByVal _id As Integer) As Vacation.Table_HRCommentRow

        Using adapter As New VacationTableAdapters.Table_HRCommentTableAdapter
            Dim Table As Vacation.Table_HRCommentDataTable
            Table = adapter.GetDataByRequestId(_id)
            For Each _row As Vacation.Table_HRCommentRow In Table
                Return _row
                _row = Nothing
            Next
            Table.Dispose()
            adapter.Dispose()
        End Using

    End Function

    Shared Function Table_HRCommentGetCountOfComment(ByVal _id As Integer) As Integer

        Using Adapter As New VacationTableAdapters.Table_HRCommentTableAdapter

            Return Adapter.GetCountOfComment(_id)
            Adapter.Dispose()

        End Using

    End Function

    Shared Function Table_Table_RequestByID(ByVal _id As Integer) As Vacation.Table_RequestRow

        Using adapter As New VacationTableAdapters.Table_RequestTableAdapter
            Dim Table As Vacation.Table_RequestDataTable
            Table = adapter.GetDataById(_id)
            For Each _row As Vacation.Table_RequestRow In Table
                Return _row
                _row = Nothing
            Next
            Table.Dispose()
            adapter.Dispose()
        End Using

    End Function

    Shared Function Table_Table_RequestGetCountOfId(ByVal _id As Integer) As Integer

        Using Adapter As New VacationTableAdapters.Table_RequestTableAdapter

            Return Adapter.GetCountOfId(_id)
            Adapter.Dispose()

        End Using

    End Function

    Shared Function Table_Table_ApprvMx_Row_ById(ByVal _id As Integer) As Vacation.Table_ApprvMxRow

        Using adapter As New VacationTableAdapters.Table_ApprvMxTableAdapter
            Dim Table As Vacation.Table_ApprvMxDataTable
            Table = adapter.GetDataById(_id)
            For Each _row As Vacation.Table_ApprvMxRow In Table
                Return _row
                _row = Nothing
            Next
            Table.Dispose()
            adapter.Dispose()
        End Using

    End Function

    Shared Sub Execute_SP_InsertApprovalMatrixFromVacationRequest(ByVal _RequestId As Integer)

        Using adapter As New VacationTableAdapters.CommonQueriesTableAdapter
            adapter.SP_InsertApprovalMatrixFromVacationRequest(_RequestId)
            adapter.Dispose()
        End Using

    End Sub

    Shared Sub AddSelect(ByVal _ddl As DropDownList)

        Dim lst As New ListItem("Select", "0")
        _ddl.Items.Insert(0, lst)

    End Sub

    Shared Sub FormViewRequestDefaultModeReadOnlyProcessItems(ByVal _Formview As FormView, ByVal _LabelKeepRequestId As Label)

        Dim DropDownListType As DropDownList = _Formview.FindControl("DropDownListType")
        Dim DropDownListPaidOrUnpaid As DropDownList = _Formview.FindControl("DropDownListPaidOrUnpaid")
        Dim DropDownListPositionDetails As DropDownList = _Formview.FindControl("DropDownListPositionDetails")
        Dim ObjectDataSourcePositionDetails As ObjectDataSource = _Formview.FindControl("ObjectDataSourcePositionDetails")
        Dim ObjectDataSourceApprMx As ObjectDataSource = _Formview.FindControl("ObjectDataSourceApprMx")

        If VacationTables.Table_Table_RequestGetCountOfId(_LabelKeepRequestId.Text) > 0 Then
            ' data exists

            ' show only selected item on DDL
            VacationTables.DisableDDLWithoutDisable(DropDownListType)
            VacationTables.DisableDDLWithoutDisable(DropDownListPaidOrUnpaid)

            ' populate position details ddl
            Dim _DepartmentName As String = VacationTables.Table_Table_RequestByID(_LabelKeepRequestId.Text).DepartmentName
            Dim _DivisionName As String = VacationTables.Table_Table_RequestByID(_LabelKeepRequestId.Text).DivisionName
            Dim _PositionName As String = VacationTables.Table_Table_RequestByID(_LabelKeepRequestId.Text).PositionName
            Dim lst As New ListItem(_DepartmentName + " / " + _DivisionName + " / " + _PositionName, "0")
            DropDownListPositionDetails.Items.Insert(0, lst)

            Dim DropDownListUsersFromVacationJunction As DropDownList = _Formview.FindControl("DropDownListUsersFromVacationJunction")
            ' Show only selected DDL DropDownListUsersFromVacationJunction
            VacationTables.DisableDDLWithoutDisable(DropDownListUsersFromVacationJunction)

            ' Calendar Days
            Dim LabelCalendarDayItem As Label = _Formview.FindControl("LabelCalendarDayItem")

            Try
                If IsDBNull(VacationTables.Table_Table_RequestByID(_LabelKeepRequestId.Text).CalendarDays) Then

                End If
            Catch ex As Exception
                LabelCalendarDayItem.Text = "Not provided by HR yet.<br />You will be notified by another email after completion."

            End Try

            ' Show employee comment if exist
            Dim LabelEmployeeCommentItem As Label = _Formview.FindControl("LabelEmployeeCommentItem")

            If VacationTables.Table_EmplyCommentGetCountOfComment(_LabelKeepRequestId.Text) > 0 Then
                LabelEmployeeCommentItem.Text = VacationTables.Table_EmplyCommentByRequestedID(_LabelKeepRequestId.Text).Comment
            Else
                LabelEmployeeCommentItem.Text = "No comment"
            End If

            ' Show review status from HR
            Dim LabelReviewStatusFromHR As Label = _Formview.FindControl("LabelReviewStatusFromHR")

            If VacationTables.Table_Table_RequestByID(_LabelKeepRequestId.Text).ReviewedByHR = True Then
                LabelReviewStatusFromHR.Text = "Reviewed"
            Else
                LabelReviewStatusFromHR.Text = "Not reviewed by HR yet.<br />You will be notified by another email after completion."
            End If

            ' Show comment from HR
            Dim LabelHRCommentItem As Label = _Formview.FindControl("LabelHRCommentItem")

            If VacationTables.Table_HRCommentGetCountOfComment(_LabelKeepRequestId.Text) > 0 Then
                LabelHRCommentItem.Text = VacationTables.Table_HRCommentByRequestedID(_LabelKeepRequestId.Text).Comment
            Else
                LabelHRCommentItem.Text = "No comment"
            End If

            ' Show Approval Matrix 
            ' this is provided by queryString on ObjectDataSource
            ObjectDataSourceApprMx.SelectParameters("RequestId").DefaultValue = _LabelKeepRequestId.Text

            ' Show Approved Or Not
            Dim LabelApprovedOrNot As Label = _Formview.FindControl("LabelApprovedOrNot")
            If VacationTables.Table_Table_RequestByID(_LabelKeepRequestId.Text).ApprvOrNot = True Then
                LabelApprovedOrNot.Text = "Approved"
            Else
                LabelApprovedOrNot.Text = "Not approved yet.<br />You will be notified by another email after approval."
            End If

            ' Show Approval Time
            Dim LabelApprovalTime As Label = _Formview.FindControl("LabelApprovalTime")

            Try
                If IsDBNull(VacationTables.Table_Table_RequestByID(_LabelKeepRequestId.Text).FullApprovalTime) Then

                End If
            Catch ex As Exception
                LabelApprovalTime.Text = "Not approved yet.<br />You will be notified by another email after approval."

            End Try

        End If

    End Sub

    Shared Function ValidateVacationPeriod(ByVal _EmplyName As String, ByVal _Start As DateTime, ByVal _Finish As DateTime) As String

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = "Vac.SP_ValidateVacationPeriod"
            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = Data.CommandType.StoredProcedure

            'syntax for parameter adding
            Dim EmplyName As SqlParameter = cmd.Parameters.Add("@EmplyName", Data.SqlDbType.NVarChar, 256)
            EmplyName.Value = _EmplyName
            Dim Start As SqlParameter = cmd.Parameters.Add("@Start", Data.SqlDbType.SmallDateTime)
            Start.Value = _Start
            Dim Finish As SqlParameter = cmd.Parameters.Add("@Finish", Data.SqlDbType.SmallDateTime)
            Finish.Value = _Finish

            Dim dr As SqlDataReader = cmd.ExecuteReader
            Dim JunctionDates As String = String.Empty

            While dr.Read

                ' This will concatneate superposed dates
                JunctionDates = JunctionDates + Mid(dr(0).ToString, 1, 10) + "<br/>"

            End While

            Return JunctionDates

            con.Close()
            dr.Close()
        End Using


    End Function

End Class
