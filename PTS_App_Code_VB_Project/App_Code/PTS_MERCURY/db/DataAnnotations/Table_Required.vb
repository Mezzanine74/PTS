Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations

Namespace PTS_MERCURY.db

    <MetadataType(GetType(Table_RequiredMetadata))>
    Partial Public Class Table_Required

    End Class

    Partial Public Class Table_RequiredMetadata

        <Required(ErrorMessage:="RequiredId is required")>
        Public Property RequiredId As Integer

        <Required(ErrorMessage:="RequiredText is required")>
        Public Property RequiredText As String

    End Class

End Namespace
