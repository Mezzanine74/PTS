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

    Partial Public Class Table_Addendums
        Public Property AddendumID As Integer
        Public Property ContractID As Integer
        Public Property PO_No As String
        Public Property AddendumNo As String
        Public Property AddendumDate As Nullable(Of Date)
        Public Property AddendumValue_woVAT As Nullable(Of Decimal)
        Public Property AddendumValue_WithVAT As Nullable(Of Decimal)
        Public Property VATpercent As Nullable(Of Short)
        Public Property AddendumDescription As String
        Public Property CostCode As String
        Public Property AddendumLinkToTemplatefile_DOC As String
        Public Property AddendumSignBySupplier As Nullable(Of Boolean)
        Public Property AddendumSignByMercury As Nullable(Of Boolean)
        Public Property AddendumCollectionBySupplier As Nullable(Of Boolean)
        Public Property AddendumGivenTo As String
        Public Property AddendumLinkToPDFcopy As String
        Public Property AddendumArchivedByMercury As Nullable(Of Boolean)
        Public Property AddendumRetention As Nullable(Of Decimal)
        Public Property AddendumNote As String
        Public Property CreatedBy As Nullable(Of Date)
        Public Property UpdatedBy As Nullable(Of Date)
        Public Property DeletedBy As Nullable(Of Date)
        Public Property PersonCreated As String
        Public Property PersonUpdated As String
        Public Property PersonDeleted As String
        Public Property AttachmentExist As Nullable(Of Boolean)
        Public Property NewGeneration As Nullable(Of Boolean)
        Public Property RequestedBy As String
        Public Property Penalties As Boolean
        Public Property PenaltiesNote As String
        Public Property PenaltiesToSupplier As Boolean
        Public Property PenaltiesToSupplierNote As String
        Public Property Budget As Nullable(Of Decimal)
        Public Property BudgetLinkToPDF As String
        Public Property Advance As Nullable(Of Decimal)
        Public Property Interim As Nullable(Of Decimal)
        Public Property Shipment As Nullable(Of Decimal)
        Public Property Delivery As Nullable(Of Decimal)
        Public Property StartDate As Nullable(Of Date)
        Public Property FinishDate As Nullable(Of Date)
        Public Property DeliveryTerms As String
        Public Property GuaranteePeriod As String
        Public Property AddendumTypes As Short
        Public Property Scenario As Short
        Public Property POexecuted As Boolean
        Public Property Exceptional As Nullable(Of Boolean)
        Public Property AddendumLinkToTemplatefile_DOCBackUp As String
        Public Property AddendumLinkToPDFcopyBackUp As String
    
        Public Overridable Property Table_Addendum_UserRemarks As ICollection(Of Table_Addendum_UserRemarks) = New HashSet(Of Table_Addendum_UserRemarks)
        Public Overridable Property Table_Addendums_ClientAdditional As Table_Addendums_ClientAdditional
        Public Overridable Property Table_Contracts As Table_Contracts
        Public Overridable Property Table_Addendum_UsersApprv As ICollection(Of Table_Addendum_UsersApprv) = New HashSet(Of Table_Addendum_UsersApprv)
    
    End Class

End Namespace
