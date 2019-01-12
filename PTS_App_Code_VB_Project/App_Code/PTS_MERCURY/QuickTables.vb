Imports Microsoft.VisualBasic

Namespace PTS_MERCURY.db

    Public Class QuickTables

        Shared Function Table1_Project(ByVal _ProjectID As Int16) As PTS_MERCURY.db.Table1_Project

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                Dim table = (From C In db.Table1_Project Where C.ProjectID = _ProjectID).ToList()(0)

                Return table

                db.Dispose()

            End Using

        End Function

        Shared Function Table_Contracts(ByVal _ContractID As Int32) As PTS_MERCURY.db.Table_Contracts

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                Dim table = (From C In db.Table_Contracts Where C.ContractID = _ContractID).ToList()(0)

                Return table

                db.Dispose()

            End Using

        End Function

        Shared Function Table_Addendums(ByVal _AddendumID As Int32) As PTS_MERCURY.db.Table_Addendums

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                Dim table = (From C In db.Table_Addendums Where C.AddendumID = _AddendumID).ToList()(0)

                Return table

                db.Dispose()

            End Using

        End Function


        Shared Function Table_Contracts_ClientAdditional(ByVal _ContractID As Int32) As PTS_MERCURY.db.Table_Contracts_ClientAdditional

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                Dim table = (From C In db.Table_Contracts_ClientAdditional Where C.ContractID = _ContractID).ToList()(0)

                Return table

                db.Dispose()

            End Using

        End Function

        Shared Function Table_Addendums_ClientAdditional(ByVal _AddendumID As Int32) As PTS_MERCURY.db.Table_Addendums_ClientAdditional

            Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                Dim table = (From C In db.Table_Addendums_ClientAdditional Where C.AddendumID = _AddendumID).ToList()(0)

                Return table

                db.Dispose()

            End Using

        End Function

        Shared Function aspnet_Users(ByVal _UserName As String) As PTS_MERCURY.db.aspnet_Users

            Try
                Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                    Dim table = (From C In db.aspnet_Users Where C.UserName = _UserName).ToList()(0)

                    Return table

                    db.Dispose()

                End Using

            Catch ex As Exception

            End Try

        End Function

        Shared Function aspnet_Membership(ByVal _UserID As System.Guid) As PTS_MERCURY.db.aspnet_Membership

            Try
                Using db As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

                    Dim table = (From C In db.aspnet_Membership Where C.UserId = _UserID).ToList()(0)

                    Return table

                    db.Dispose()

                End Using

            Catch ex As Exception

            End Try

        End Function

    End Class

End Namespace

