Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Namespace PTS.CoreTables

    Public Class Table1_Project

        Public Sub New(ProjectID As Int16)

            Dim connection As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            Dim cmd As SqlCommand
            Dim _sql As String = " SELECT [ProjectName] ,[NewGeneration], (CASE WHEN RTRIM(Logo) IS NULL THEN N'_' ELSE RTRIM(Logo) END) AS Logo, PoMakerNewGeneration, NewPoPolicy, PR_Coverpage_Approval_Allowed, RTRIM(PM_SignaturesLink) AS PM_SignaturesLink, PR_Coverpage_Approval_Compulsory FROM [Table1_Project] WHERE ProjectID = @ProjectID "
            cmd = New SqlCommand(_sql, connection)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.AddWithValue("@ProjectID", ProjectID)

            connection.Open()
            Dim objReader As SqlDataReader = cmd.ExecuteReader()

            While objReader.Read()
                SetObjectData(objReader)
            End While

            objReader.Close()
            connection.Close()

        End Sub

#Region "properties"

        Public Property ProjectName() As String
            Get
                Return _ProjectName
            End Get
            Set(value As String)
                _ProjectName = value
            End Set
        End Property
        Private _ProjectName As String = String.Empty

        Public Property NewGeneration() As Boolean
            Get
                Return _NewGeneration
            End Get
            Set(value As Boolean)
                _NewGeneration = value
            End Set
        End Property
        Private _NewGeneration As Boolean = False

        Public Property Logo() As String
            Get
                Return _Logo
            End Get
            Set(value As String)
                _Logo = value
            End Set
        End Property
        Private _Logo As String = String.Empty

        Public Property PoMakerNewGeneration() As Boolean
            Get
                Return _PoMakerNewGeneration
            End Get
            Set(value As Boolean)
                _PoMakerNewGeneration = value
            End Set
        End Property
        Private _PoMakerNewGeneration As Boolean = False

        Public Property NewPoPolicy() As Boolean
            Get
                Return _NewPoPolicy
            End Get
            Set(value As Boolean)
                _NewPoPolicy = value
            End Set
        End Property
        Private _NewPoPolicy As Boolean = False

        Public Property PR_Coverpage_Approval_Allowed() As Boolean
            Get
                Return _PR_Coverpage_Approval_Allowed
            End Get
            Set(value As Boolean)
                _PR_Coverpage_Approval_Allowed = value
            End Set
        End Property
        Private _PR_Coverpage_Approval_Allowed As Boolean = False

        Public Property PM_SignaturesLink() As String
            Get
                Return _PM_SignaturesLink
            End Get
            Set(value As String)
                _PM_SignaturesLink = value
            End Set
        End Property
        Private _PM_SignaturesLink As String = String.Empty

        Public Property PR_Coverpage_Approval_Compulsory() As Boolean
            Get
                Return _PR_Coverpage_Approval_Compulsory
            End Get
            Set(value As Boolean)
                _PR_Coverpage_Approval_Compulsory = value
            End Set
        End Property
        Private _PR_Coverpage_Approval_Compulsory As Boolean = False

#End Region

#Region "methods"

        Private Sub SetObjectData(theObjReader As SqlDataReader)

            Me._ProjectName = theObjReader("ProjectName")
            Me._NewGeneration = theObjReader("NewGeneration")
            Me._Logo = theObjReader("Logo")
            Me._PoMakerNewGeneration = theObjReader("PoMakerNewGeneration")
            Me._NewPoPolicy = theObjReader("NewPoPolicy")
            Me._PR_Coverpage_Approval_Allowed = theObjReader("PR_Coverpage_Approval_Allowed")
            Me._PM_SignaturesLink = theObjReader("PM_SignaturesLink")
            Me._PR_Coverpage_Approval_Compulsory = theObjReader("PR_Coverpage_Approval_Compulsory")

        End Sub

#End Region

    End Class

End Namespace
