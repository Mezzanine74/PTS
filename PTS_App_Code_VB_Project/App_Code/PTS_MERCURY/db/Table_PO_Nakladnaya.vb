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

    Partial Public Class Table_PO_Nakladnaya
        Public Property ID_Nak As Integer
        Public Property PO_No As String
        Public Property Inv_ContrNo As String
        Public Property NakNo As String
        Public Property Nak_Date As Date
        Public Property Nak_Rub_WithVAT As Decimal
        Public Property SF As Boolean
        Public Property CreatedBy As Nullable(Of Date)
        Public Property PersonCreated As String
        Public Property payreqno As Nullable(Of Integer)
        Public Property Comment As String
        Public Property PDF As String
    
        Public Overridable Property Table_PO_Akt_To_Nak As ICollection(Of Table_PO_Akt_To_Nak) = New HashSet(Of Table_PO_Akt_To_Nak)
        Public Overridable Property Table2_PONo As Table2_PONo
    
    End Class

End Namespace