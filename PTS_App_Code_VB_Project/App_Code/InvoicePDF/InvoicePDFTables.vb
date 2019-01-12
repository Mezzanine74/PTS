
Imports Microsoft.VisualBasic
Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Public Class InvoicePDFTables

    Shared Function Table3_Invoice_PDF_Row_ByInvoiceID(ByVal _id As Integer) As InvoicePDF.Table3_Invoice_PDFRow

        Using adapter As New InvoicePDFTableAdapters.Table3_Invoice_PDFTableAdapter
            Dim Table As InvoicePDF.Table3_Invoice_PDFDataTable
            Table = adapter.GetDataByInvoiceID(_id)
            For Each _row As InvoicePDF.Table3_Invoice_PDFRow In Table
                Return _row
                _row = Nothing
            Next
            Table.Dispose()
            adapter.Dispose()
        End Using

    End Function

    Shared Function PayReqEmailBodyByInvoiceID(ByVal _id As Integer) As InvoicePDF.PayReqEmailBodyRow

        Using adapter As New InvoicePDFTableAdapters.PayReqEmailBodyTableAdapter
            Dim Table As InvoicePDF.PayReqEmailBodyDataTable
            Table = adapter.GetDataByInvoiceId(_id)
            For Each _row As InvoicePDF.PayReqEmailBodyRow In Table
                Return _row
                _row = Nothing
            Next
            Table.Dispose()
            adapter.Dispose()
        End Using

    End Function


    Shared Function GetCountOfInvoicePDF(ByVal _id As Integer) As Integer

        Using adapter As New InvoicePDFTableAdapters.Table3_Invoice_PDFTableAdapter
            Return adapter.GetCountOfInvoice(_id)
            adapter.Dispose()

        End Using

    End Function

    Shared Function Table3_Invoice_PRrequestToPM_Row_ByInvoiceID(ByVal _id As Integer) As Mercury.Table3_Invoice_PRrequestToPMRow

        Using adapter As New MercuryTableAdapters.Table3_Invoice_PRrequestToPMTableAdapter
            Dim Table As Mercury.Table3_Invoice_PRrequestToPMDataTable
            Table = adapter.GetDataByInvoiceID(_id)
            For Each _row As Mercury.Table3_Invoice_PRrequestToPMRow In Table
                Return _row
                _row = Nothing
            Next
            Table.Dispose()
            adapter.Dispose()
        End Using

    End Function

    Public Shared Sub AddTextToPdf(inputPdfPath As String, outputPdfPath As String, textToAdd As String, point As Drawing.Point)
        'variables
        Dim pathin As String = inputPdfPath
        Dim pathout As String = outputPdfPath

        'create PdfReader object to read from the existing document
        Using reader As New PdfReader(pathin)
            'create PdfStamper object to write to get the pages from reader 
            Using stamper As New PdfStamper(reader, New FileStream(pathout, FileMode.Create))
                'select two pages from the original document
                reader.SelectPages("1-2")

                'gettins the page size in order to substract from the iTextSharp coordinates
                Dim pageSize = reader.GetPageSize(1)

                ' PdfContentByte from stamper to add content to the pages over the original content
                Dim pbover As PdfContentByte = stamper.GetOverContent(1)

                'add content to the page using ColumnText
                Dim font As New Font(font.BOLD)
                font.Size = 24
                font.Color = iTextSharp.text.BaseColor.RED

                'setting up the X and Y coordinates of the document
                Dim x As Integer = point.X
                Dim y As Integer = point.Y

                x += 113
                y = CInt(pageSize.Height - y)

                ColumnText.ShowTextAligned(pbover, Element.ALIGN_CENTER, New Phrase(textToAdd, font), x, y, 0)
            End Using
        End Using
    End Sub


    Public Shared Sub MergeFiles(destinationFile As String, sourceFiles As String())
        Try
            Dim f As Integer = 0
            ' we create a reader for a certain document
            Dim reader As New PdfReader(sourceFiles(f))
            ' we retrieve the total number of pages
            Dim n As Integer = reader.NumberOfPages
            'Console.WriteLine("There are " + n + " pages in the original file.");
            ' step 1: creation of a document-object
            Dim document As New Document(reader.GetPageSizeWithRotation(1))
            ' step 2: we create a writer that listens to the document
            Dim writer As PdfWriter = PdfWriter.GetInstance(document, New FileStream(destinationFile, FileMode.Create))
            ' step 3: we open the document
            document.Open()
            Dim cb As PdfContentByte = writer.DirectContent
            Dim page As PdfImportedPage
            Dim rotation As Integer
            ' step 4: we add content
            While f < sourceFiles.Length
                Dim i As Integer = 0
                While i < n
                    i += 1
                    document.SetPageSize(reader.GetPageSizeWithRotation(i))
                    document.NewPage()
                    page = writer.GetImportedPage(reader, i)
                    rotation = reader.GetPageRotation(i)
                    If rotation = 90 OrElse rotation = 270 Then
                        cb.AddTemplate(page, 0, -1.0F, 1.0F, 0, 0, _
                            reader.GetPageSizeWithRotation(i).Height)
                    Else
                        cb.AddTemplate(page, 1.0F, 0, 0, 1.0F, 0, _
                            0)
                        'Console.WriteLine("Processed page " + i);
                    End If
                End While
                f += 1
                If f < sourceFiles.Length Then
                    reader = New PdfReader(sourceFiles(f))
                    ' we retrieve the total number of pages
                    'Console.WriteLine("There are " + n + " pages in the original file.");
                    n = reader.NumberOfPages
                End If
            End While
            ' step 5: we close the document
            document.Close()
        Catch e As Exception
            Dim strOb As String = e.Message
        End Try
    End Sub

    Public Function CountPageNo(strFileName As String) As Integer
        ' we create a reader for a certain document
        Dim reader As New PdfReader(strFileName)
        ' we retrieve the total number of pages
        Return reader.NumberOfPages
    End Function

End Class
