Imports Microsoft.VisualBasic
Imports System.Net.Mail

Namespace PTS_MERCURY.helper

    Public Class View_CheckTolerance

        Shared db As PTS_MERCURY.db.SQL2008_794282_mercuryEntities = New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

        Shared Function CountRow() As Integer

            Dim _return As Integer = 0

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                _return = Aggregate C In db.View_CheckTolerance Into Count()

            End Using

            Return _return

        End Function

        Shared Function GetRows(_id As Integer) As PTS_MERCURY.db.View_CheckTolerance

            Dim _return As PTS_MERCURY.db.View_CheckTolerance

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                _return = (From C In db.View_CheckTolerance).ToList()(_id)

            End Using

            Return _return

        End Function

        Shared Function CountItems() As Integer

            Dim _return As Integer = 0

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                _return = Aggregate C In db.View_CheckTolerance Into Count()

            End Using

            Return _return

        End Function


        Shared Function GetRowByPrimaryKey(_id As Guid) As PTS_MERCURY.db.View_CheckTolerance

            Dim _return As PTS_MERCURY.db.View_CheckTolerance

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                If CountItems() > 0 Then

                    _return = (From C In db.View_CheckTolerance Where C.Id = _id).ToList()(0)

                Else

                    _return = Nothing

                End If

            End Using

            Return _return

        End Function

        Shared Function GetUsersAutoApprovedByPTS(_contractId As Integer, _addendumId As Integer) As String

            Dim _return As String = ""
            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities
                Dim _count As Integer = Aggregate C In db.View_CheckTolerance Where C.ContractID = _contractId And C.AddendumId = _addendumId Into Count()
                If _count > 0 Then
                    Dim _username As String = ""
                    Dim _toleratedDay As String = ""
                    For i = 0 To _count - 1
                        _username = (From C In db.View_CheckTolerance Where C.ContractID = _contractId And C.AddendumId = _addendumId).ToList()(i).UserName
                        _toleratedDay = (From C In db.View_CheckTolerance Where C.ContractID = _contractId And C.AddendumId = _addendumId).ToList()(i).ToleratedDay.ToString()
                        _username = PTS_MERCURY.helper.View_GetFullUserNameFromUserName.GetUserName(_username)
                        _return = _return + "<b>" + _username + "</b> <i>(exceeds " + _toleratedDay + " day limit)</i><br/>"
                    Next
                End If
            End Using
            Return _return
        End Function

        Shared Function GetUsersEmailAutoApprovedByPTS(_contractId As Integer, _addendumId As Integer, _mm As MailMessage) As MailMessage

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities
                Dim _count As Integer = Aggregate C In db.View_CheckTolerance Where C.ContractID = _contractId And C.AddendumId = _addendumId Into Count()
                If _count > 0 Then
                    Dim _username As String = ""
                    Dim _email As String = ""
                    For i = 0 To _count - 1
                        _username = (From C In db.View_CheckTolerance Where C.ContractID = _contractId And C.AddendumId = _addendumId).ToList()(i).UserName
                        _email = PTS_MERCURY.helper.aspnet_Membership.GetEmailFromUserName(_username)
                        _mm.To.Add(_email)
                        'If _return = "" Then
                        '    _return = _email
                        'Else
                        '    _return = _return + ", " + _email
                        'End If
                    Next
                End If
            End Using
            Return _mm
        End Function

        Shared Function GetUsersEmailNotApproved(_contractid As Integer, _addendumid As Integer, _mm As MailMessage) As MailMessage

            Dim _return As String = ""

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                Dim MissingAddendumApprovals = From C In db.AddendumMissingApprovalBreakdowns Join D In db.Table_Addendums On C.AddendumID Equals D.AddendumID Where D.Scenario <> 0 And C.ContractID = _contractid And C.AddendumID = _addendumid Select New With {.ContractID = C.ContractID, .AddendumId = C.AddendumID, .UserName = C.UserName}

                Dim MissingContractApprovals = From C In db.ContractMissingApprovalBreakdowns Join D In db.Table_Contracts On C.ContractID Equals D.ContractID Where D.Scenario <> 0 And C.ContractID = _contractid Select New With {.ContractID = C.ContractID, .AddendumId = 0, .UserName = C.UserName}

                Dim Union = MissingContractApprovals.Concat(MissingAddendumApprovals).ToList()

                Dim _username As String = ""
                Dim _email As String = ""

                If Union.Count() > 0 Then
                    For i = 0 To Union.Count() - 1
                        _username = Union(i).UserName
                        _email = PTS_MERCURY.helper.aspnet_Membership.GetEmailFromUserName(_username)
                        _mm.To.Add(_email)

                    Next

                End If

            End Using

            Return _mm

        End Function


        Public Class View_CheckToleranceGroupBy
            Private _ContractId As Integer
            Public Property ContractId() As Integer
                Get
                    Return _ContractId
                End Get
                Set(ByVal value As Integer)
                    _ContractId = value
                End Set
            End Property

            Private _AddendumId As Integer
            Public Property AddendumId() As Integer
                Get
                    Return _AddendumId
                End Get
                Set(ByVal value As Integer)
                    _AddendumId = value
                End Set
            End Property
        End Class

        Shared Function GetView_CheckToleranceGroupBy() As IQueryable(Of View_CheckToleranceGroupBy)

            Return ( _
                From C In db.View_CheckTolerance
                Group By C.ContractID, C.AddendumId
                Into g = Group Select ContractID, AddendumId).Select(Function(e) New View_CheckToleranceGroupBy With {.ContractId = e.ContractID, .AddendumId = e.AddendumId})

        End Function

    End Class

End Namespace

