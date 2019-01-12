Imports System.Web.Services

Partial Class Pages_PTS_PageMethods_Table_SupplierType_Tags
    Inherits System.Web.UI.Page

    Public Class _item2
        Property TagSourceIndex As String
        Property SourceType As String
        Property tag As String
    End Class

    <WebMethod> _
    Public Shared Sub Delete(_item2 As _item2)

        Dim _SupplierTypeId As Integer = PTS_MERCURY.helper.Table_SupplierType.GetTagIdByTag(_item2.tag)

        ' delete tag from tags junction
        Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

            Dim a = (From C In db.Table_SupplierType_Tags Where C.SourceType = _item2.SourceType And C.SupplierTypeId = _SupplierTypeId And C.TagSourceIndex = _item2.TagSourceIndex).ToList()(0)

            db.Table_SupplierType_Tags.Attach(a)
            db.Table_SupplierType_Tags.Remove(a)

            db.SaveChanges()

            db.Dispose()

        End Using

        ' delete this tag from source if it is not used on junction
        Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

            If (From C In db.Table_SupplierType_Tags Where C.SupplierTypeId = _SupplierTypeId).ToList().Count() = 0 Then

                ' delete supplier junction if tag doenst exist anymore
                db.Database.ExecuteSqlCommand("DELETE FROM Table_SupplierType_Junction WHERE SupplierTypeId = {0}", _SupplierTypeId)

                Dim a = (From C In db.Table_SupplierType Where C.SupplierTypeId = _SupplierTypeId).ToList()(0)

                db.Table_SupplierType.Attach(a)
                db.Table_SupplierType.Remove(a)

                db.SaveChanges()

                db.Dispose()

            End If

        End Using

    End Sub

    <WebMethod> _
    Public Shared Sub Insert(_item2 As _item2)

        Dim _SupplierTypeId As Integer = PTS_MERCURY.helper.Table_SupplierType.GetTagIdByTag(_item2.tag)
        If _SupplierTypeId = 0 Then
            Exit Sub
        End If

        Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

            Dim a As New PTS_MERCURY.db.Table_SupplierType_Tags

            a.SourceType = _item2.SourceType
            a.SupplierTypeId = _SupplierTypeId
            a.TagSourceIndex = _item2.TagSourceIndex

            db.Table_SupplierType_Tags.Attach(a)
            db.Table_SupplierType_Tags.Add(a)

            db.SaveChanges()

            db.Dispose()

        End Using

        ' Get SupplierId
        Dim _supplierId As String = ""

        If _item2.SourceType = "c" Then
            _supplierId = PTS_MERCURY.helper.Table_Contracts.GetRowByPrimaryKey(_item2.TagSourceIndex.Trim()).SupplierID.Trim()
        ElseIf _item2.SourceType = "a" Then
            Dim _contractid As String = PTS_MERCURY.helper.Table_Addendums.GetRowByPrimaryKey(_item2.TagSourceIndex.Trim()).ContractID
            _supplierId = PTS_MERCURY.helper.Table_Contracts.GetRowByPrimaryKey(_contractid).SupplierID.Trim()
        ElseIf _item2.SourceType = "p" Then
            _supplierId = PTS_MERCURY.helper.Table2_PONo.GetRowByPrimaryKey(_item2.TagSourceIndex.Trim()).SupplierID.Trim()
        End If

        ' insert to junction
        Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

            Dim a As New PTS_MERCURY.db.Table_SupplierType_Junction

            a.SupplierID = _supplierId
            a.SupplierTypeId = _SupplierTypeId

            db.Table_SupplierType_Junction.Attach(a)
            db.Table_SupplierType_Junction.Add(a)

            Try
                db.SaveChanges()
            Catch ex As Exception

            End Try

            db.Dispose()

        End Using


    End Sub

End Class
