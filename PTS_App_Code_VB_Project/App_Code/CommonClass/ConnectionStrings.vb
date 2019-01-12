Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Configuration

Public Class ConnectionStringsPTS

  Shared Function GetConnectionStringMain() As SqlConnection
        Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("MERCURYConnectionString").ConnectionString)
    Return con
  End Function

End Class
