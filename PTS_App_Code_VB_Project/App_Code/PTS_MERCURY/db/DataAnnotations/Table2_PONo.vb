Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations

Namespace PTS_MERCURY.db

    <MetadataType(GetType(Table2_PONoMetadata))>
    Partial Public Class Table2_PONo

    End Class

    Partial Public Class Table2_PONoMetadata

        <Required(ErrorMessage:="Project required")>
        <Range(1, Integer.MaxValue, ErrorMessage:="Project required")>
        Public Property Project_ID As Short

        <Required(ErrorMessage:="Supplier required")>
        Public Property SupplierID As String

        <Required(ErrorMessage:="Description required")>
        Public Property Description As String

        <Required(ErrorMessage:="PO value required")>
        Public Property TotalPrice As Decimal

        <Required(ErrorMessage:="Currency required")>
        Public Property PO_Currency As String

        <Required(ErrorMessage:="VAT required")>
        Public Property VATpercent As Decimal

        <Required(ErrorMessage:="CostCode required")>
        Public Property CostCode As String

        <Required(ErrorMessage:="PO Date required")>
        Public Property PO_Date As Date

    End Class

End Namespace
