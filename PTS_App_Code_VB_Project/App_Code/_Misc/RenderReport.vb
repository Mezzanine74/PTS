Imports System.Net
Imports System.Security.Principal
Imports Microsoft.Reporting.WebForms
Imports Microsoft.ReportingServices
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Web


Public Class RenderReport
    Shared Sub Render(ByVal RenderType As String, ByVal _ReportViever As ReportViewer, ByVal _ReportPath As String _
             , Optional ByVal Parameter1 As String = "", Optional ByVal Parameter1Value As String = "" _
             , Optional ByVal Parameter2 As String = "", Optional ByVal Parameter2Value As String = "" _
             , Optional ByVal Parameter3 As String = "", Optional ByVal Parameter3Value As String = "" _
             , Optional ByVal Parameter4 As String = "", Optional ByVal Parameter4Value As String = "" _
             , Optional ByVal Parameter5 As String = "", Optional ByVal Parameter5Value As String = "" _
             , Optional ByVal Parameter6 As String = "", Optional ByVal Parameter6Value As String = "" _
             , Optional ByVal _MapPath As String = "" _
             , Optional ByVal Parameter7 As String = "", Optional ByVal Parameter7Value As String = "" _
             , Optional ByVal Parameter8 As String = "", Optional ByVal Parameter8Value As String = "" _
             , Optional ByVal Parameter9 As String = "", Optional ByVal Parameter9Value As String = "" _
             , Optional ByVal Parameter10 As String = "", Optional ByVal Parameter10Value As String = "" _
             , Optional ByVal Parameter11 As String = "", Optional ByVal Parameter11Value As String = "" _
             , Optional ByVal Parameter12 As String = "", Optional ByVal Parameter12Value As String = "" _
             , Optional ByVal Parameter13 As String = "", Optional ByVal Parameter13Value As String = "" _
             , Optional ByVal Parameter14 As String = "", Optional ByVal Parameter14Value As String = "" _
             , Optional ByVal Parameter15 As String = "", Optional ByVal Parameter15Value As String = "" _
             , Optional ByVal Parameter16 As String = "", Optional ByVal Parameter16Value As String = "" _
             , Optional ByVal Parameter17 As String = "", Optional ByVal Parameter17Value As String = "")

        _ReportViever.Visible = True
        _ReportViever.Reset()
        _ReportViever.ServerReport.ReportPath = ConfigurationManager.AppSettings("ReportPath").ToString + _ReportPath
        _ReportViever.ServerReport.ReportServerUrl = New System.Uri(ConfigurationManager.AppSettings("ReportServerURL").ToString)
        _ReportViever.ServerReport.ReportServerCredentials = New ReportCredentials
        _ReportViever.ServerReport.Timeout = -1

        Dim Parameters As ReportParameter
        Dim Parameter_list As New List(Of ReportParameter)


        If Not String.IsNullOrEmpty(Parameter1) AndAlso Not String.IsNullOrEmpty(Parameter1Value) Then
            Parameters = New ReportParameter(Parameter1, Parameter1Value)
            Parameter_list.Add(Parameters)
            _ReportViever.ServerReport.SetParameters(Parameter_list)
        End If
        If Not String.IsNullOrEmpty(Parameter2) AndAlso Not String.IsNullOrEmpty(Parameter2Value) Then
            Parameters = New ReportParameter(Parameter2, Parameter2Value)
            Parameter_list.Add(Parameters)
            _ReportViever.ServerReport.SetParameters(Parameter_list)
        End If
        If Not String.IsNullOrEmpty(Parameter3) AndAlso Not String.IsNullOrEmpty(Parameter3Value) Then
            Parameters = New ReportParameter(Parameter3, Parameter3Value)
            Parameter_list.Add(Parameters)
            _ReportViever.ServerReport.SetParameters(Parameter_list)
        End If
        If Not String.IsNullOrEmpty(Parameter4) AndAlso Not String.IsNullOrEmpty(Parameter4Value) Then
            Parameters = New ReportParameter(Parameter4, Parameter4Value)
            Parameter_list.Add(Parameters)
            _ReportViever.ServerReport.SetParameters(Parameter_list)
        End If
        If Not String.IsNullOrEmpty(Parameter5) AndAlso Not String.IsNullOrEmpty(Parameter5Value) Then
            Parameters = New ReportParameter(Parameter5, Parameter5Value)
            Parameter_list.Add(Parameters)
            _ReportViever.ServerReport.SetParameters(Parameter_list)
        End If
        If Not String.IsNullOrEmpty(Parameter6) AndAlso Not String.IsNullOrEmpty(Parameter6Value) Then
            Parameters = New ReportParameter(Parameter6, Parameter6Value)
            Parameter_list.Add(Parameters)
            _ReportViever.ServerReport.SetParameters(Parameter_list)
        End If
        If Not String.IsNullOrEmpty(Parameter7) AndAlso Not String.IsNullOrEmpty(Parameter7Value) Then
            Parameters = New ReportParameter(Parameter7, Parameter7Value)
            Parameter_list.Add(Parameters)
            _ReportViever.ServerReport.SetParameters(Parameter_list)
        End If
        If Not String.IsNullOrEmpty(Parameter8) AndAlso Not String.IsNullOrEmpty(Parameter8Value) Then
            Parameters = New ReportParameter(Parameter8, Parameter8Value)
            Parameter_list.Add(Parameters)
            _ReportViever.ServerReport.SetParameters(Parameter_list)
        End If
        If Not String.IsNullOrEmpty(Parameter9) AndAlso Not String.IsNullOrEmpty(Parameter9Value) Then
            Parameters = New ReportParameter(Parameter9, Parameter9Value)
            Parameter_list.Add(Parameters)
            _ReportViever.ServerReport.SetParameters(Parameter_list)
        End If
        If Not String.IsNullOrEmpty(Parameter10) AndAlso Not String.IsNullOrEmpty(Parameter10Value) Then
            Parameters = New ReportParameter(Parameter10, Parameter10Value)
            Parameter_list.Add(Parameters)
            _ReportViever.ServerReport.SetParameters(Parameter_list)
        End If
        If Not String.IsNullOrEmpty(Parameter11) AndAlso Not String.IsNullOrEmpty(Parameter11Value) Then
            Parameters = New ReportParameter(Parameter11, Parameter11Value)
            Parameter_list.Add(Parameters)
            _ReportViever.ServerReport.SetParameters(Parameter_list)
        End If
        If Not String.IsNullOrEmpty(Parameter12) AndAlso Not String.IsNullOrEmpty(Parameter12Value) Then
            Parameters = New ReportParameter(Parameter12, Parameter12Value)
            Parameter_list.Add(Parameters)
            _ReportViever.ServerReport.SetParameters(Parameter_list)
        End If
        If Not String.IsNullOrEmpty(Parameter13) AndAlso Not String.IsNullOrEmpty(Parameter13Value) Then
            Parameters = New ReportParameter(Parameter13, Parameter13Value)
            Parameter_list.Add(Parameters)
            _ReportViever.ServerReport.SetParameters(Parameter_list)
        End If
        If Not String.IsNullOrEmpty(Parameter14) AndAlso Not String.IsNullOrEmpty(Parameter14Value) Then
            Parameters = New ReportParameter(Parameter14, Parameter14Value)
            Parameter_list.Add(Parameters)
            _ReportViever.ServerReport.SetParameters(Parameter_list)
        End If
        If Not String.IsNullOrEmpty(Parameter15) AndAlso Not String.IsNullOrEmpty(Parameter15Value) Then
            Parameters = New ReportParameter(Parameter15, Parameter15Value)
            Parameter_list.Add(Parameters)
            _ReportViever.ServerReport.SetParameters(Parameter_list)
        End If
        If Not String.IsNullOrEmpty(Parameter16) AndAlso Not String.IsNullOrEmpty(Parameter16Value) Then
            Parameters = New ReportParameter(Parameter16, Parameter16Value)
            Parameter_list.Add(Parameters)
            _ReportViever.ServerReport.SetParameters(Parameter_list)
        End If
        If Not String.IsNullOrEmpty(Parameter17) AndAlso Not String.IsNullOrEmpty(Parameter17Value) Then
            Parameters = New ReportParameter(Parameter17, Parameter17Value)
            Parameter_list.Add(Parameters)
            _ReportViever.ServerReport.SetParameters(Parameter_list)
        End If


        If RenderType.ToLower = "html" Then
            _ReportViever.ServerReport.Refresh()
        ElseIf RenderType.ToLower = "excel" Then
            Dim warnings As Warning
            Dim streamIds As String
            Dim mimeType As String = String.Empty
            Dim encoding As String = String.Empty
            Dim extension As String = "xls"
            Dim bytes As Byte() = _ReportViever.ServerReport.Render("Excel", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

            HttpContext.Current.Response.Buffer = True
            HttpContext.Current.Response.Clear()
            HttpContext.Current.Response.ContentType = mimeType
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + _ReportPath + "." + extension)
            HttpContext.Current.Response.BinaryWrite(bytes)
            HttpContext.Current.Response.Flush()
        ElseIf RenderType.ToLower = "disc" Then
            Dim warnings As Warning
            Dim streamIds As String
            Dim mimeType As String = String.Empty
            Dim encoding As String = String.Empty
            Dim extension As String = "xls"
            Dim bytes As Byte() = _ReportViever.ServerReport.Render("Excel", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

            Dim oFileStream As System.IO.FileStream
            Dim strFilePath As String = String.Empty
            strFilePath = HttpContext.Current.Server.MapPath("~/" + _MapPath)

            oFileStream = New System.IO.FileStream(strFilePath, System.IO.FileMode.Create)
            oFileStream.Write(bytes, 0, bytes.Length)
            oFileStream.Close()
        ElseIf RenderType.ToLower = "discpdf" Then
            Dim warnings As Warning
            Dim streamIds As String
            Dim mimeType As String = String.Empty
            Dim encoding As String = String.Empty
            Dim extension As String = "PDF"
            Dim bytes As Byte() = _ReportViever.ServerReport.Render("PDF", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

            Dim oFileStream As System.IO.FileStream
            Dim strFilePath As String = String.Empty
            strFilePath = HttpContext.Current.Server.MapPath("~/" + _MapPath)

            oFileStream = New System.IO.FileStream(strFilePath, System.IO.FileMode.Create)
            oFileStream.Write(bytes, 0, bytes.Length)
            oFileStream.Close()



        End If

        Parameters = Nothing
        Parameter_list = Nothing

    End Sub

End Class
