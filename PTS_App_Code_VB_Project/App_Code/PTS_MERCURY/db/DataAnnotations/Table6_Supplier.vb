Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations

Namespace PTS_MERCURY.db

    <MetadataType(GetType(Table6_SupplierMetadata))>
    Partial Public Class Table6_Supplier

    End Class

    Partial Public Class Table6_SupplierMetadata

        <Required(ErrorMessage:="Supplier INN required")>
        Public Property SupplierID As String

        <Required(ErrorMessage:="Supplier Name required")>
        Public Property SupplierName As String

    End Class

End Namespace
