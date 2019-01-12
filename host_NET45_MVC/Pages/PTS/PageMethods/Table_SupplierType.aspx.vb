Imports System.Web.Services

Partial Class Pages_PTS_PageMethods_Table_SupplierType
    Inherits System.Web.UI.Page

    <WebMethod> _
    Public Shared Sub Delete(_text As String)

        Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

            Dim a = (From C In db.Table_SupplierType Where C.SupplierType = _text).ToList()(0)

            db.Table_SupplierType.Attach(a)
            db.Table_SupplierType.Remove(a)

            db.SaveChanges()

            db.Dispose()

        End Using

    End Sub

    <WebMethod> _
    Public Shared Sub Insert(_item As PTS_MERCURY.db.Table_SupplierType)

        If PTS_MERCURY.helper.Table_SupplierType.CountItemsBySupplierType(_item.SupplierType) > 0 Then
            Exit Sub
        End If

        Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

            Dim a As New PTS_MERCURY.db.Table_SupplierType

            a.SupplierType = _item.SupplierType
            a.SupplierDiscipline = "-"

            db.Table_SupplierType.Attach(a)
            db.Table_SupplierType.Add(a)

            db.SaveChanges()

            db.Dispose()

        End Using

    End Sub

End Class
