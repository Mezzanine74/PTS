Imports Microsoft.VisualBasic

Namespace PTS_MERCURY.helper

    Public Class Table_Contracts

        Shared Function CountItems() As Integer

            Dim _return As Integer = 0

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                _return = Aggregate C In db.Table_Contracts Into Count()

            End Using

            Return _return

        End Function

        Shared Function CountItemsByContractId(_contractid As Integer) As Integer

            Dim _return As Integer = 0

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                _return = Aggregate C In db.Table_Contracts Where C.ContractID = _contractid Into Count()

            End Using

            Return _return

        End Function

        Shared Function GetRowByPrimaryKey(_id As Integer) As PTS_MERCURY.db.Table_Contracts

            Dim _return As PTS_MERCURY.db.Table_Contracts

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                If CountItemsByContractId(_id) > 0 Then

                    _return = (From C In db.Table_Contracts Where C.ContractID = _id).ToList()(0)

                Else

                    _return = Nothing

                End If

            End Using

            Return _return

        End Function

        Shared Function GetSupplierName(_contractId As Integer) As String

            Dim _return As String = ""

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                Try
                    _return = (From C In db.Table_Contracts Where C.ContractID = _contractId Join D In db.Table6_Supplier On C.SupplierID Equals D.SupplierID Select New With {D.SupplierName}).ToList()(0).SupplierName.Trim()

                Catch ex As Exception

                End Try

            End Using

            Return _return

        End Function

        Shared Function GetProjectName(_contractId As Integer) As String

            Dim _return As String = ""

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                Try
                    _return = (From C In db.Table_Contracts Where C.ContractID = _contractId Join D In db.Table1_Project On C.ProjectID Equals D.ProjectID Select New With {D.ProjectID}).ToList()(0).ProjectID

                Catch ex As Exception

                End Try

            End Using

            Return _return

        End Function

        Structure BudgetTolerancePercentage
            Const TolerancePercent As Decimal = 90.0
        End Structure

    End Class

End Namespace
