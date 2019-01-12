Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Namespace PTS.CoreTables

    Public Class aspnet_Users

        Public Sub New(UserName As String)

            Dim connection As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            Dim cmd As SqlCommand
            Dim _sql As String = _
                " SELECT " + _
                " 	Case When ApproveFramePo IS Null Then 0 Else ApproveFramePo END AS ApproveFramePo  " + _
                "  ,Case When AuthorizedForExceptionalApprove IS Null Then 0 Else AuthorizedForExceptionalApprove END AS AuthorizedForExceptionalApprove  " + _
                "  ,IsNull(AuthorizedForPaymentRequest,0) AS AuthorizedForPaymentRequest " + _
                " FROM aspnet_Users  " + _
                " WHERE UserName = @UserName "
            cmd = New SqlCommand(_sql, connection)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.AddWithValue("@UserName", UserName)

            connection.Open()
            Dim objReader As SqlDataReader = cmd.ExecuteReader()

            While objReader.Read()
                SetObjectData(objReader)
            End While

            objReader.Close()
            connection.Close()

        End Sub

#Region "properties"

        Public Property ApproveFramePo() As Boolean
            Get
                Return _ApproveFramePo

            End Get
            Set(value As Boolean)
                    _ApproveFramePo = value

            End Set
        End Property
        Private _ApproveFramePo As Boolean = False

        Public Property AuthorizedForExceptionalApprove() As Boolean
            Get
                Return _AuthorizedForExceptionalApprove

            End Get
            Set(value As Boolean)
                _AuthorizedForExceptionalApprove = value

            End Set
        End Property
        Private _AuthorizedForExceptionalApprove As Boolean = False

        Public Property AuthorizedForPaymentRequest() As Boolean
            Get
                Return _AuthorizedForPaymentRequest

            End Get
            Set(value As Boolean)
                _AuthorizedForPaymentRequest = value

            End Set
        End Property
        Private _AuthorizedForPaymentRequest As Boolean = False

#End Region

#Region "methods"

        Private Sub SetObjectData(theObjReader As SqlDataReader)

            Me._ApproveFramePo = theObjReader("ApproveFramePo")
            Me._AuthorizedForExceptionalApprove = theObjReader("AuthorizedForExceptionalApprove")
            Me._AuthorizedForPaymentRequest = theObjReader("AuthorizedForPaymentRequest")

        End Sub

#End Region

    End Class

End Namespace
