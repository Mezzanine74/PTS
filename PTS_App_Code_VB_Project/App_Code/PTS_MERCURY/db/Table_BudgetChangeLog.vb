'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated from a template.
'
'     Manual changes to this file may cause unexpected behavior in your application.
'     Manual changes to this file will be overwritten if the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Imports System
Imports System.Collections.Generic

Namespace PTS_MERCURY.db

    Partial Public Class Table_BudgetChangeLog
        Public Property id As Integer
        Public Property ProjectId As Short
        Public Property CostCode As String
        Public Property OldValue As Decimal
        Public Property NewValue As Decimal
        Public Property Explanation As String
        Public Property Attachment As String
        Public Property Email As String
        Public Property LogTime As Nullable(Of Date)
    
    End Class

End Namespace
