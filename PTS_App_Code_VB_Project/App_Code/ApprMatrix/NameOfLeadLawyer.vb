Imports Microsoft.VisualBasic
Imports System.Data.SqlClient

Public Class NameOfLeadLawyer

    Shared Function GetNameFromFunction() As String

        ' This function designed to return LARISA name from SQL scalar function, but it is changed temporarily as per LArisa request as shown below.
        ' It is not achiavable in SQL Server as it cause several side affects.
        Return "lawyers"
        Exit Function


        ' This will return Name Of Lead Girl (current: Larisa)
        Dim Adapter As New MercuryTableAdapters.NameOfLeadLawyerTableAdapter
        Dim Table As Mercury.NameOfLeadLawyerDataTable

        Table = Adapter.GetData

        For Each _return In Table
            ' single value returns
            ' TOLOWER is very important. Dont remove
            Return _return.NameOfLeadLawyer.ToLower()
            _return = Nothing
        Next

        Table = Nothing
        Adapter = Nothing

    End Function


End Class
