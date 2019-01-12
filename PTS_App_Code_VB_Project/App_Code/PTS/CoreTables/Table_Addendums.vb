Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Namespace PTS.CoreTables

    Public Class Table_Addendums

        Public Sub New(AddendumID As Int32)

            Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain

                Dim cmd As SqlCommand
                Dim _sql As String = "GetPTSCoreTable_Table_Addendums"
                cmd = New SqlCommand(_sql, con)
                cmd.CommandType = CommandType.StoredProcedure

                cmd.Parameters.AddWithValue("@AddendumID", AddendumID)

                con.Open()
                Dim dr As SqlDataReader = cmd.ExecuteReader()

                While dr.Read()
                    SetObjectData(dr)
                End While

                dr.Close()
                con.Close()
                con.Dispose()

            End Using

        End Sub

#Region "properties"

        Public Property ContractID() As Nullable(Of Int32)
            Get
                Return _ContractID
            End Get
            Set(value As Nullable(Of Int32))
                _ContractID = value
            End Set
        End Property
        Private _ContractID As Int32 = 0

        Public Property PO_No() As String
            Get
                Return _PO_No
            End Get
            Set(value As String)
                _PO_No = value
            End Set
        End Property
        Private _PO_No As String = String.Empty

        Public Property AddendumNo() As String
            Get
                Return _AddendumNo
            End Get
            Set(value As String)
                _AddendumNo = value
            End Set
        End Property
        Private _AddendumNo As String = String.Empty

        Public Property AddendumDate() As Nullable(Of DateTime)
            Get
                Return _AddendumDate
            End Get
            Set(value As Nullable(Of DateTime))
                _AddendumDate = value
            End Set
        End Property
        Private _AddendumDate As DateTime = Nothing

        Public Property AddendumValue_woVAT() As Nullable(Of Decimal)
            Get
                Return _AddendumValue_woVAT
            End Get
            Set(value As Nullable(Of Decimal))
                _AddendumValue_woVAT = value
            End Set
        End Property
        Private _AddendumValue_woVAT As Decimal = 0

        Public Property AddendumValue_WithVAT() As Nullable(Of Decimal)
            Get
                Return _AddendumValue_WithVAT
            End Get
            Set(value As Nullable(Of Decimal))
                _AddendumValue_WithVAT = value
            End Set
        End Property
        Private _AddendumValue_WithVAT As Decimal = 0

        Public Property VATpercent() As Nullable(Of Int16)
            Get
                Return _VATpercent
            End Get
            Set(value As Nullable(Of Int16))
                _VATpercent = value
            End Set
        End Property
        Private _VATpercent As Int16 = 0

        Public Property AddendumDescription() As String
            Get
                Return _AddendumDescription
            End Get
            Set(value As String)
                _AddendumDescription = value
            End Set
        End Property
        Private _AddendumDescription As String = String.Empty

        Public Property CostCode() As String
            Get
                Return _CostCode
            End Get
            Set(value As String)
                _CostCode = value
            End Set
        End Property
        Private _CostCode As String = String.Empty

        Public Property AddendumLinkToTemplatefile_DOC() As String
            Get
                Return _AddendumLinkToTemplatefile_DOC
            End Get
            Set(value As String)
                _AddendumLinkToTemplatefile_DOC = value
            End Set
        End Property
        Private _AddendumLinkToTemplatefile_DOC As String = String.Empty

        Public Property AddendumSignBySupplier() As Nullable(Of Boolean)
            Get
                Return _AddendumSignBySupplier
            End Get
            Set(value As Nullable(Of Boolean))
                _AddendumSignBySupplier = value
            End Set
        End Property
        Private _AddendumSignBySupplier As Boolean = False

        Public Property AddendumSignByMercury() As Nullable(Of Boolean)
            Get
                Return _AddendumSignByMercury
            End Get
            Set(value As Nullable(Of Boolean))
                _AddendumSignByMercury = value
            End Set
        End Property
        Private _AddendumSignByMercury As Boolean = False

        Public Property AddendumCollectionBySupplier() As Nullable(Of Boolean)
            Get
                Return _AddendumCollectionBySupplier
            End Get
            Set(value As Nullable(Of Boolean))
                _AddendumCollectionBySupplier = value
            End Set
        End Property
        Private _AddendumCollectionBySupplier As Boolean = False

        Public Property AddendumGivenTo() As String
            Get
                Return _AddendumGivenTo
            End Get
            Set(value As String)
                _AddendumGivenTo = value
            End Set
        End Property
        Private _AddendumGivenTo As String = String.Empty

        Public Property AddendumLinkToPDFcopy() As String
            Get
                Return _AddendumLinkToPDFcopy
            End Get
            Set(value As String)
                _AddendumLinkToPDFcopy = value
            End Set
        End Property
        Private _AddendumLinkToPDFcopy As String = String.Empty

        Public Property AddendumArchivedByMercury() As Nullable(Of Boolean)
            Get
                Return _AddendumArchivedByMercury
            End Get
            Set(value As Nullable(Of Boolean))
                _AddendumArchivedByMercury = value
            End Set
        End Property
        Private _AddendumArchivedByMercury As Boolean = False

        Public Property AddendumRetention() As Nullable(Of Decimal)
            Get
                Return _AddendumRetention
            End Get
            Set(value As Nullable(Of Decimal))
                _AddendumRetention = value
            End Set
        End Property
        Private _AddendumRetention As Decimal = 0

        Public Property AddendumNote() As String
            Get
                Return _AddendumNote
            End Get
            Set(value As String)
                _AddendumNote = value
            End Set
        End Property
        Private _AddendumNote As String = String.Empty

        Public Property PersonCreated() As String
            Get
                Return _PersonCreated
            End Get
            Set(value As String)
                _PersonCreated = value
            End Set
        End Property
        Private _PersonCreated As String = String.Empty

        Public Property PersonUpdated() As String
            Get
                Return _PersonUpdated
            End Get
            Set(value As String)
                _PersonUpdated = value
            End Set
        End Property
        Private _PersonUpdated As String = String.Empty

        Public Property PersonDeleted() As String
            Get
                Return _PersonDeleted
            End Get
            Set(value As String)
                _PersonDeleted = value
            End Set
        End Property
        Private _PersonDeleted As String = String.Empty

        Public Property AttachmentExist() As Nullable(Of Boolean)
            Get
                Return _AttachmentExist
            End Get
            Set(value As Nullable(Of Boolean))
                _AttachmentExist = value
            End Set
        End Property
        Private _AttachmentExist As Boolean = False

        Public Property NewGeneration() As Nullable(Of Boolean)
            Get
                Return _NewGeneration
            End Get
            Set(value As Nullable(Of Boolean))
                _NewGeneration = value
            End Set
        End Property
        Private _NewGeneration As Boolean = False

        Public Property RequestedBy() As String
            Get
                Return _RequestedBy
            End Get
            Set(value As String)
                _RequestedBy = value
            End Set
        End Property
        Private _RequestedBy As String = String.Empty

        Public Property Penalties() As Nullable(Of Boolean)
            Get
                Return _Penalties
            End Get
            Set(value As Nullable(Of Boolean))
                _Penalties = value
            End Set
        End Property
        Private _Penalties As Boolean = False

        Public Property Advance() As Nullable(Of Decimal)
            Get
                Return _Advance
            End Get
            Set(value As Nullable(Of Decimal))
                _Advance = value
            End Set
        End Property
        Private _Advance As Decimal = 0

        Public Property StartDate() As Nullable(Of DateTime)
            Get
                Return _StartDate
            End Get
            Set(value As Nullable(Of DateTime))
                _StartDate = value
            End Set
        End Property
        Private _StartDate As DateTime = Nothing

        Public Property FinishDate() As Nullable(Of DateTime)
            Get
                Return _FinishDate
            End Get
            Set(value As Nullable(Of DateTime))
                _FinishDate = value
            End Set
        End Property
        Private _FinishDate As DateTime = Nothing

        Public Property DeliveryTerms() As String
            Get
                Return _DeliveryTerms
            End Get
            Set(value As String)
                _DeliveryTerms = value
            End Set
        End Property
        Private _DeliveryTerms As String = String.Empty

        Public Property GuaranteePeriod() As String
            Get
                Return _GuaranteePeriod
            End Get
            Set(value As String)
                _GuaranteePeriod = value
            End Set
        End Property
        Private _GuaranteePeriod As String = String.Empty

        Public Property AddendumTypes() As Nullable(Of Int32)
            Get
                Return _AddendumTypes
            End Get
            Set(value As Nullable(Of Int32))
                _AddendumTypes = value
            End Set
        End Property
        Private _AddendumTypes As Int32 = 0

        Public Property Scenario() As Nullable(Of Int32)
            Get
                Return _Scenario
            End Get
            Set(value As Nullable(Of Int32))
                _Scenario = value
            End Set
        End Property
        Private _Scenario As Int32 = 0

        Public Property POexecuted() As Nullable(Of Boolean)
            Get
                Return _POexecuted
            End Get
            Set(value As Nullable(Of Boolean))
                _POexecuted = value
            End Set
        End Property
        Private _POexecuted As Boolean = False

        Public Property Exceptional() As Nullable(Of Boolean)
            Get
                Return _Exceptional
            End Get
            Set(value As Nullable(Of Boolean))
                _Exceptional = value
            End Set
        End Property
        Private _Exceptional As Boolean = False

        Public Property CreatedBy() As Nullable(Of DateTime)
            Get
                Return _CreatedBy
            End Get
            Set(value As Nullable(Of DateTime))
                _CreatedBy = value
            End Set
        End Property
        Private _CreatedBy As DateTime = Nothing

#End Region

#Region "methods"

        Private Sub SetObjectData(dr As SqlDataReader)

            If dr(dr.GetOrdinal("ContractID")) Is DBNull.Value Then
                Me._ContractID = Nothing
            Else
                Me._ContractID = dr(dr.GetOrdinal("ContractID"))
            End If

            If dr(dr.GetOrdinal("PO_No")) Is DBNull.Value Then
                Me._PO_No = Nothing
            Else
                Me._PO_No = dr(dr.GetOrdinal("PO_No"))
            End If

            If dr(dr.GetOrdinal("AddendumNo")) Is DBNull.Value Then
                Me._AddendumNo = Nothing
            Else
                Me._AddendumNo = dr(dr.GetOrdinal("AddendumNo"))
            End If

            If dr(dr.GetOrdinal("AddendumDate")) Is DBNull.Value Then
                Me._AddendumDate = Nothing
            Else
                Me._AddendumDate = dr(dr.GetOrdinal("AddendumDate"))
            End If

            If dr(dr.GetOrdinal("AddendumValue_woVAT")) Is DBNull.Value Then
                Me._AddendumValue_woVAT = Nothing
            Else
                Me._AddendumValue_woVAT = dr(dr.GetOrdinal("AddendumValue_woVAT"))
            End If

            If dr(dr.GetOrdinal("AddendumValue_WithVAT")) Is DBNull.Value Then
                Me._AddendumValue_WithVAT = Nothing
            Else
                Me._AddendumValue_WithVAT = dr(dr.GetOrdinal("AddendumValue_WithVAT"))
            End If

            If dr(dr.GetOrdinal("VATpercent")) Is DBNull.Value Then
                Me._VATpercent = Nothing
            Else
                Me._VATpercent = dr(dr.GetOrdinal("VATpercent"))
            End If

            If dr(dr.GetOrdinal("AddendumDescription")) Is DBNull.Value Then
                Me._AddendumDescription = Nothing
            Else
                Me._AddendumDescription = dr(dr.GetOrdinal("AddendumDescription"))
            End If

            If dr(dr.GetOrdinal("CostCode")) Is DBNull.Value Then
                Me._CostCode = Nothing
            Else
                Me._CostCode = dr(dr.GetOrdinal("CostCode"))
            End If

            If dr(dr.GetOrdinal("AddendumLinkToTemplatefile_DOC")) Is DBNull.Value Then
                Me._AddendumLinkToTemplatefile_DOC = Nothing
            Else
                Me._AddendumLinkToTemplatefile_DOC = dr(dr.GetOrdinal("AddendumLinkToTemplatefile_DOC"))
            End If

            If dr(dr.GetOrdinal("AddendumSignBySupplier")) Is DBNull.Value Then
                Me._AddendumSignBySupplier = Nothing
            Else
                Me._AddendumSignBySupplier = dr(dr.GetOrdinal("AddendumSignBySupplier"))
            End If

            If dr(dr.GetOrdinal("AddendumSignByMercury")) Is DBNull.Value Then
                Me._AddendumSignByMercury = Nothing
            Else
                Me._AddendumSignByMercury = dr(dr.GetOrdinal("AddendumSignByMercury"))
            End If

            If dr(dr.GetOrdinal("AddendumCollectionBySupplier")) Is DBNull.Value Then
                Me._AddendumCollectionBySupplier = Nothing
            Else
                Me._AddendumCollectionBySupplier = dr(dr.GetOrdinal("AddendumCollectionBySupplier"))
            End If

            If dr(dr.GetOrdinal("AddendumGivenTo")) Is DBNull.Value Then
                Me._AddendumGivenTo = Nothing
            Else
                Me._AddendumGivenTo = dr(dr.GetOrdinal("AddendumGivenTo"))
            End If

            If dr(dr.GetOrdinal("AddendumLinkToPDFcopy")) Is DBNull.Value Then
                Me._AddendumLinkToPDFcopy = Nothing
            Else
                Me._AddendumLinkToPDFcopy = dr(dr.GetOrdinal("AddendumLinkToPDFcopy"))
            End If

            If dr(dr.GetOrdinal("AddendumArchivedByMercury")) Is DBNull.Value Then
                Me._AddendumArchivedByMercury = Nothing
            Else
                Me._AddendumArchivedByMercury = dr(dr.GetOrdinal("AddendumArchivedByMercury"))
            End If

            If dr(dr.GetOrdinal("AddendumRetention")) Is DBNull.Value Then
                Me._AddendumRetention = Nothing
            Else
                Me._AddendumRetention = dr(dr.GetOrdinal("AddendumRetention"))
            End If

            If dr(dr.GetOrdinal("AddendumNote")) Is DBNull.Value Then
                Me._AddendumNote = Nothing
            Else
                Me._AddendumNote = dr(dr.GetOrdinal("AddendumNote"))
            End If

            If dr(dr.GetOrdinal("PersonCreated")) Is DBNull.Value Then
                Me._PersonCreated = Nothing
            Else
                Me._PersonCreated = dr(dr.GetOrdinal("PersonCreated"))
            End If

            If dr(dr.GetOrdinal("PersonUpdated")) Is DBNull.Value Then
                Me._PersonUpdated = Nothing
            Else
                Me._PersonUpdated = dr(dr.GetOrdinal("PersonUpdated"))
            End If

            If dr(dr.GetOrdinal("PersonDeleted")) Is DBNull.Value Then
                Me._PersonDeleted = Nothing
            Else
                Me._PersonDeleted = dr(dr.GetOrdinal("PersonDeleted"))
            End If

            If dr(dr.GetOrdinal("AttachmentExist")) Is DBNull.Value Then
                Me._AttachmentExist = Nothing
            Else
                Me._AttachmentExist = dr(dr.GetOrdinal("AttachmentExist"))
            End If

            If dr(dr.GetOrdinal("NewGeneration")) Is DBNull.Value Then
                Me._NewGeneration = Nothing
            Else
                Me._NewGeneration = dr(dr.GetOrdinal("NewGeneration"))
            End If

            If dr(dr.GetOrdinal("RequestedBy")) Is DBNull.Value Then
                Me._RequestedBy = Nothing
            Else
                Me._RequestedBy = dr(dr.GetOrdinal("RequestedBy"))
            End If

            If dr(dr.GetOrdinal("Penalties")) Is DBNull.Value Then
                Me._Penalties = Nothing
            Else
                Me._Penalties = dr(dr.GetOrdinal("Penalties"))
            End If

            If dr(dr.GetOrdinal("Advance")) Is DBNull.Value Then
                Me._Advance = Nothing
            Else
                Me._Advance = dr(dr.GetOrdinal("Advance"))
            End If

            If dr(dr.GetOrdinal("StartDate")) Is DBNull.Value Then
                Me._StartDate = Nothing
            Else
                Me._StartDate = dr(dr.GetOrdinal("StartDate"))
            End If

            If dr(dr.GetOrdinal("FinishDate")) Is DBNull.Value Then
                Me._FinishDate = Nothing
            Else
                Me._FinishDate = dr(dr.GetOrdinal("FinishDate"))
            End If

            If dr(dr.GetOrdinal("DeliveryTerms")) Is DBNull.Value Then
                Me._DeliveryTerms = Nothing
            Else
                Me._DeliveryTerms = dr(dr.GetOrdinal("DeliveryTerms"))
            End If

            If dr(dr.GetOrdinal("GuaranteePeriod")) Is DBNull.Value Then
                Me._GuaranteePeriod = Nothing
            Else
                Me._GuaranteePeriod = dr(dr.GetOrdinal("GuaranteePeriod"))
            End If

            If dr(dr.GetOrdinal("AddendumTypes")) Is DBNull.Value Then
                Me._AddendumTypes = Nothing
            Else
                Me._AddendumTypes = dr(dr.GetOrdinal("AddendumTypes"))
            End If

            If dr(dr.GetOrdinal("Scenario")) Is DBNull.Value Then
                Me._Scenario = Nothing
            Else
                Me._Scenario = dr(dr.GetOrdinal("Scenario"))
            End If

            If dr(dr.GetOrdinal("POexecuted")) Is DBNull.Value Then
                Me._POexecuted = Nothing
            Else
                Me._POexecuted = dr(dr.GetOrdinal("POexecuted"))
            End If

            If dr(dr.GetOrdinal("Exceptional")) Is DBNull.Value Then
                Me._Exceptional = Nothing
            Else
                Me._Exceptional = dr(dr.GetOrdinal("Exceptional"))
            End If

            If dr(dr.GetOrdinal("CreatedBy")) Is DBNull.Value Then
                Me._CreatedBy = Nothing
            Else
                Me._CreatedBy = dr(dr.GetOrdinal("CreatedBy"))
            End If

        End Sub

#End Region

    End Class

End Namespace
