Namespace VisitorsLog.helper
    Public Class Table_VisitorLogs

        Shared Sub Insert(username As String, pagename As String, visittime As DateTime, ipadress As String, browsertype As String, browserplatform As String, country As String)

            Using db As New VisitorsLog.db.VisitorsLogEntities

                Dim A As New VisitorsLog.db.Table_VisitorLogs
                A.UserName = username
                A.PageName = pagename
                A.VisitTime = visittime
                A.IpAdress = ipadress
                A.BrowserType = browsertype
                A.BrowserPlatform = browserplatform
                A.Country = country

                db.Table_VisitorLogs.Attach(A)
                db.Table_VisitorLogs.Add(A)
                db.SaveChanges()

            End Using

        End Sub

    End Class
End Namespace
