Imports System.Web.Services

Partial Class Pages_PTS_PageMethods_Table_Contracts_IgnoreSearchFeed
    Inherits System.Web.UI.Page

    <WebMethod> _
    Public Shared Sub Delete(ByVal id As Integer)



        'Using db As New AsterosOrders.model.AsterosOrdersEntities

        '    Dim a = (From C In db.Table_Persons Where C.PersonId = id).ToList()(0)

        '    db.Table_Persons.Attach(a)
        '    db.Table_Persons.Remove(a)

        '    db.SaveChanges()

        '    db.Dispose()

        'End Using

    End Sub

    <WebMethod> _
    Public Shared Sub InsertTable_Contracts_IgnoreSearchFeed(ByVal Table_Contracts_IgnoreSearchFeed As PTS_MERCURY.db.Table_Contracts_IgnoreSearchFeed)

        Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

            Dim a As New PTS_MERCURY.db.Table_Contracts_IgnoreSearchFeed

            a.ContractID = Table_Contracts_IgnoreSearchFeed.ContractID

            db.Table_Contracts_IgnoreSearchFeed.Attach(a)
            db.Table_Contracts_IgnoreSearchFeed.Add(a)

            db.SaveChanges()

            db.Dispose()

        End Using

    End Sub

End Class
