Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations

Namespace PTS_MERCURY.db

    <MetadataType(GetType(Table_BudgetChangeLogMetadata))> _
    Partial Public Class Table_BudgetChangeLog

    End Class

    Partial Public Class Table_BudgetChangeLogMetadata
        <Required>
        Public Property CostCode As String
    End Class

End Namespace
