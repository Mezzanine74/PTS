<%@ WebHandler Language="VB" Class="Handler" %>

Imports System
Imports System.Web
Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports System.Web.Script.Serialization
 
Public Class Handler : Implements IHttpHandler
   
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim callback As String = context.Request.QueryString("callback")
        Dim query As String = ""
        
        If context.Request.QueryString("query") IsNot Nothing Then
            query = context.Request.QueryString("query")
        End If

        Dim json As String = Me.GetCustomersJSON(query)
        If Not String.IsNullOrEmpty(callback) Then
            json = String.Format("{0}({1});", callback, json)
        End If
 
        context.Response.ContentType = "text/json"
        context.Response.Write(json)
    End Sub
    
    Private Function GetCustomersJSON(_text As String) As String
        
        Dim customers As New List(Of Object)()
                
        Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities
            
            Dim A = db.Table_SupplierType.Where(Function(e) e.SupplierType.Contains(_text))
            
            For i = 0 To A.ToList().Count() - 1
                customers.Add(New With { _
                  .SupplierType = A.ToList()(i).SupplierType.Trim(), _
                  .SupplierTypeId = A.ToList()(i).SupplierTypeId
                })

            Next
            Return (New JavaScriptSerializer().Serialize(customers))
        End Using

    End Function
   
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class