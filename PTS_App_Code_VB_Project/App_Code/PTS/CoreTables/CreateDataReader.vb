Imports Microsoft.VisualBasic

Namespace PTS.CoreTables

  Public Class CreateDataReader

    Public Shared Function Create_Table_Contract(ByVal _ContractID As Integer) As Table_Contracts

      Dim _return As New Table_Contracts(_ContractID)
      Return _return

      _return = Nothing

    End Function


    Public Shared Function Create_Table_Addendums(ByVal _AddendumID As Integer) As Table_Addendums

      Dim _return As New Table_Addendums(_AddendumID)
      Return _return

      _return = Nothing

    End Function

    Public Shared Function Create_Table1_Project(ByVal _ProjectID As Integer) As Table1_Project

      Dim _return As New Table1_Project(_ProjectID)
      Return _return

      _return = Nothing

    End Function

        Public Shared Function Create_aspnet_Users(ByVal _UserName As String) As aspnet_Users

            Dim _return As New aspnet_Users(_UserName)
            Return _return

            _return = Nothing

        End Function

  End Class

End Namespace

