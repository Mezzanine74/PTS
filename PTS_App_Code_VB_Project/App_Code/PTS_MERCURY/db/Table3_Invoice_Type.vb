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

    Partial Public Class Table3_Invoice_Type
        Public Property Type_id As Integer
        Public Property Type_name As String
    
        Public Overridable Property Table3_Invoice_Type_Junction As ICollection(Of Table3_Invoice_Type_Junction) = New HashSet(Of Table3_Invoice_Type_Junction)
    
    End Class

End Namespace
