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

    Partial Public Class Table7_CostDivision
        Public Property CostVidisionID As String
        Public Property CostDivisionDescription As String
        Public Property CostDivision2ID As String
    
        Public Overridable Property Table7_CostCode As ICollection(Of Table7_CostCode) = New HashSet(Of Table7_CostCode)
    
    End Class

End Namespace