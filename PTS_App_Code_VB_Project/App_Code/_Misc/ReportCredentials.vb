Imports System.Net
Imports Microsoft.Reporting.WebForms
Imports System.Security.Principal
Imports System.Configuration

<Serializable()> _
Public NotInheritable Class ReportCredentials
    Implements IReportServerCredentials


    Public ReadOnly Property ImpersonationUser() As WindowsIdentity _
    Implements IReportServerCredentials.ImpersonationUser
        Get
            'Use the default windows user. Credentials will be
            'provided by the NetworkCredentials property.
            Return Nothing


        End Get
    End Property


    Public ReadOnly Property NetworkCredentials() As ICredentials _
    Implements IReportServerCredentials.NetworkCredentials
        Get

            Dim ReportUser As String = ConfigurationManager.AppSettings("ReportUser").ToString
            Dim ReportUserPassword As String = ConfigurationManager.AppSettings("ReportUserPassword").ToString
            Dim ReportServerName As String = ConfigurationManager.AppSettings("ReportServerName").ToString
            Return New NetworkCredential(ReportUser, ReportUserPassword, ReportServerName)

        End Get
    End Property


    Public Function GetFormsCredentials(ByRef authCookie As Cookie, ByRef userName As String, ByRef password As String, ByRef authority As String) As Boolean _
     Implements IReportServerCredentials.GetFormsCredentials


        authCookie = Nothing
        userName = Nothing
        password = Nothing
        authority = Nothing


        'Not using form credentials
        Return False
    End Function


End Class