Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Namespace PTS.CoreTables

  Public Class Table_Contracts

    Public Sub New(ContractID As Int32)

            Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain

                Dim cmd As SqlCommand
                Dim _sql As String = "GetPTSCoreTable_Table_Contract"
                cmd = New SqlCommand(_sql, con)
                cmd.CommandType = CommandType.StoredProcedure

                cmd.Parameters.AddWithValue("@ContractID", ContractID)

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

        Public Property ProjectID() As Nullable(Of Int16)
            Get
                Return _ProjectID
            End Get
            Set(value As Nullable(Of Int16))
                _ProjectID = value
            End Set
        End Property
        Private _ProjectID As Int16 = 0

        Public Property PO_No() As String
            Get
                Return _PO_No
            End Get
            Set(value As String)
                _PO_No = value
            End Set
        End Property
        Private _PO_No As String = String.Empty

        Public Property ContractNo() As String
            Get
                Return _ContractNo
            End Get
            Set(value As String)
                _ContractNo = value
            End Set
        End Property
        Private _ContractNo As String = String.Empty

        Public Property ContractDate() As Nullable(Of DateTime)
            Get
                Return _ContractDate
            End Get
            Set(value As Nullable(Of DateTime))
                _ContractDate = value
            End Set
        End Property
        Private _ContractDate As DateTime = Nothing

        Public Property ContractValue_woVAT() As Nullable(Of Decimal)
            Get
                Return _ContractValue_woVAT
            End Get
            Set(value As Nullable(Of Decimal))
                _ContractValue_woVAT = value
            End Set
        End Property
        Private _ContractValue_woVAT As Decimal = 0

        Public Property ContractValue_withVAT() As Nullable(Of Decimal)
            Get
                Return _ContractValue_withVAT
            End Get
            Set(value As Nullable(Of Decimal))
                _ContractValue_withVAT = value
            End Set
        End Property
        Private _ContractValue_withVAT As Decimal = 0

        Public Property VATpercent() As Nullable(Of Int16)
            Get
                Return _VATpercent
            End Get
            Set(value As Nullable(Of Int16))
                _VATpercent = value
            End Set
        End Property
        Private _VATpercent As Int16 = 0

        Public Property CostCode() As String
            Get
                Return _CostCode
            End Get
            Set(value As String)
                _CostCode = value
            End Set
        End Property
        Private _CostCode As String = String.Empty

        Public Property ContractCurrency() As String
            Get
                Return _ContractCurrency
            End Get
            Set(value As String)
                _ContractCurrency = value
            End Set
        End Property
        Private _ContractCurrency As String = String.Empty

        Public Property SupplierID() As String
            Get
                Return _SupplierID
            End Get
            Set(value As String)
                _SupplierID = value
            End Set
        End Property
        Private _SupplierID As String = String.Empty

        Public Property ContractDescription() As String
            Get
                Return _ContractDescription
            End Get
            Set(value As String)
                _ContractDescription = value
            End Set
        End Property
        Private _ContractDescription As String = String.Empty

        Public Property ContractType() As String
            Get
                Return _ContractType
            End Get
            Set(value As String)
                _ContractType = value
            End Set
        End Property
        Private _ContractType As String = String.Empty

        Public Property LinkToTemplatefile_DOC() As String
            Get
                Return _LinkToTemplatefile_DOC
            End Get
            Set(value As String)
                _LinkToTemplatefile_DOC = value
            End Set
        End Property
        Private _LinkToTemplatefile_DOC As String = String.Empty

        Public Property SentToSupplier() As Nullable(Of Boolean)
            Get
                Return _SentToSupplier
            End Get
            Set(value As Nullable(Of Boolean))
                _SentToSupplier = value
            End Set
        End Property
        Private _SentToSupplier As Boolean = False

        Public Property SignBySupplier() As Nullable(Of Boolean)
            Get
                Return _SignBySupplier
            End Get
            Set(value As Nullable(Of Boolean))
                _SignBySupplier = value
            End Set
        End Property
        Private _SignBySupplier As Boolean = False

        Public Property SignByMercury() As Nullable(Of Boolean)
            Get
                Return _SignByMercury
            End Get
            Set(value As Nullable(Of Boolean))
                _SignByMercury = value
            End Set
        End Property
        Private _SignByMercury As Boolean = False

        Public Property CollectionBySupplier() As Nullable(Of Boolean)
            Get
                Return _CollectionBySupplier
            End Get
            Set(value As Nullable(Of Boolean))
                _CollectionBySupplier = value
            End Set
        End Property
        Private _CollectionBySupplier As Boolean = False

        Public Property ContractGivenTo() As String
            Get
                Return _ContractGivenTo
            End Get
            Set(value As String)
                _ContractGivenTo = value
            End Set
        End Property
        Private _ContractGivenTo As String = String.Empty

        Public Property LinkToPDFcopy() As String
            Get
                Return _LinkToPDFcopy
            End Get
            Set(value As String)
                _LinkToPDFcopy = value
            End Set
        End Property
        Private _LinkToPDFcopy As String = String.Empty

        Public Property ArchivedByMercury() As Nullable(Of Boolean)
            Get
                Return _ArchivedByMercury
            End Get
            Set(value As Nullable(Of Boolean))
                _ArchivedByMercury = value
            End Set
        End Property
        Private _ArchivedByMercury As Boolean = False

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

        Public Property RequestedBy() As String
            Get
                Return _RequestedBy
            End Get
            Set(value As String)
                _RequestedBy = value
            End Set
        End Property
        Private _RequestedBy As String = String.Empty

        Public Property Retention() As Nullable(Of Decimal)
            Get
                Return _Retention
            End Get
            Set(value As Nullable(Of Decimal))
                _Retention = value
            End Set
        End Property
        Private _Retention As Decimal = 0

        Public Property Note() As String
            Get
                Return _Note
            End Get
            Set(value As String)
                _Note = value
            End Set
        End Property
        Private _Note As String = String.Empty

        Public Property AttachmentExist() As Nullable(Of Boolean)
            Get
                Return _AttachmentExist
            End Get
            Set(value As Nullable(Of Boolean))
                _AttachmentExist = value
            End Set
        End Property
        Private _AttachmentExist As Boolean = False

        Public Property Scenario() As Nullable(Of Int16)
            Get
                Return _Scenario
            End Get
            Set(value As Nullable(Of Int16))
                _Scenario = value
            End Set
        End Property
        Private _Scenario As Int16 = 0

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

        Public Property Nominated() As Nullable(Of Boolean)
            Get
                Return _Nominated
            End Get
            Set(value As Nullable(Of Boolean))
                _Nominated = value
            End Set
        End Property
        Private _Nominated As Boolean = False

        Public Property FrameContract() As Nullable(Of Boolean)
            Get
                Return _FrameContract
            End Get
            Set(value As Nullable(Of Boolean))
                _FrameContract = value
            End Set
        End Property
        Private _FrameContract As Boolean = False

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

        Public Property NominatedApprovedByESTM() As Nullable(Of Integer)
            Get
                Return _NominatedApprovedByESTM
            End Get
            Set(value As Nullable(Of Integer))
                _NominatedApprovedByESTM = value
            End Set
        End Property
        Private _NominatedApprovedByESTM As Integer = Nothing

#End Region

#Region "methods"

        Private Sub SetObjectData(dr As SqlDataReader)

            If dr(dr.GetOrdinal("ProjectID")) Is DBNull.Value Then
                Me._ProjectID = Nothing
            Else
                Me._ProjectID = dr(dr.GetOrdinal("ProjectID"))
            End If

            If dr(dr.GetOrdinal("PO_No")) Is DBNull.Value Then
                Me._PO_No = Nothing
            Else
                Me._PO_No = dr(dr.GetOrdinal("PO_No"))
            End If

            If dr(dr.GetOrdinal("ContractNo")) Is DBNull.Value Then
                Me._ContractNo = Nothing
            Else
                Me._ContractNo = dr(dr.GetOrdinal("ContractNo"))
            End If

            If dr(dr.GetOrdinal("ContractDate")) Is DBNull.Value Then
                Me._ContractDate = Nothing
            Else
                Me._ContractDate = dr(dr.GetOrdinal("ContractDate"))
            End If

            If dr(dr.GetOrdinal("ContractValue_woVAT")) Is DBNull.Value Then
                Me._ContractValue_woVAT = Nothing
            Else
                Me._ContractValue_woVAT = dr(dr.GetOrdinal("ContractValue_woVAT"))
            End If

            If dr(dr.GetOrdinal("ContractValue_withVAT")) Is DBNull.Value Then
                Me._ContractValue_withVAT = Nothing
            Else
                Me._ContractValue_withVAT = dr(dr.GetOrdinal("ContractValue_withVAT"))
            End If

            If dr(dr.GetOrdinal("VATpercent")) Is DBNull.Value Then
                Me._VATpercent = Nothing
            Else
                Me._VATpercent = dr(dr.GetOrdinal("VATpercent"))
            End If

            If dr(dr.GetOrdinal("CostCode")) Is DBNull.Value Then
                Me._CostCode = Nothing
            Else
                Me._CostCode = dr(dr.GetOrdinal("CostCode"))
            End If

            If dr(dr.GetOrdinal("ContractCurrency")) Is DBNull.Value Then
                Me._ContractCurrency = Nothing
            Else
                Me._ContractCurrency = dr(dr.GetOrdinal("ContractCurrency"))
            End If

            If dr(dr.GetOrdinal("SupplierID")) Is DBNull.Value Then
                Me._SupplierID = Nothing
            Else
                Me._SupplierID = dr(dr.GetOrdinal("SupplierID"))
            End If

            If dr(dr.GetOrdinal("ContractDescription")) Is DBNull.Value Then
                Me._ContractDescription = Nothing
            Else
                Me._ContractDescription = dr(dr.GetOrdinal("ContractDescription"))
            End If

            If dr(dr.GetOrdinal("ContractType")) Is DBNull.Value Then
                Me._ContractType = Nothing
            Else
                Me._ContractType = dr(dr.GetOrdinal("ContractType"))
            End If

            If dr(dr.GetOrdinal("LinkToTemplatefile_DOC")) Is DBNull.Value Then
                Me._LinkToTemplatefile_DOC = Nothing
            Else
                Me._LinkToTemplatefile_DOC = dr(dr.GetOrdinal("LinkToTemplatefile_DOC"))
            End If

            If dr(dr.GetOrdinal("SentToSupplier")) Is DBNull.Value Then
                Me._SentToSupplier = Nothing
            Else
                Me._SentToSupplier = dr(dr.GetOrdinal("SentToSupplier"))
            End If

            If dr(dr.GetOrdinal("SignBySupplier")) Is DBNull.Value Then
                Me._SignBySupplier = Nothing
            Else
                Me._SignBySupplier = dr(dr.GetOrdinal("SignBySupplier"))
            End If

            If dr(dr.GetOrdinal("SignByMercury")) Is DBNull.Value Then
                Me._SignByMercury = Nothing
            Else
                Me._SignByMercury = dr(dr.GetOrdinal("SignByMercury"))
            End If

            If dr(dr.GetOrdinal("CollectionBySupplier")) Is DBNull.Value Then
                Me._CollectionBySupplier = Nothing
            Else
                Me._CollectionBySupplier = dr(dr.GetOrdinal("CollectionBySupplier"))
            End If

            If dr(dr.GetOrdinal("ContractGivenTo")) Is DBNull.Value Then
                Me._ContractGivenTo = Nothing
            Else
                Me._ContractGivenTo = dr(dr.GetOrdinal("ContractGivenTo"))
            End If

            If dr(dr.GetOrdinal("LinkToPDFcopy")) Is DBNull.Value Then
                Me._LinkToPDFcopy = Nothing
            Else
                Me._LinkToPDFcopy = dr(dr.GetOrdinal("LinkToPDFcopy"))
            End If

            If dr(dr.GetOrdinal("ArchivedByMercury")) Is DBNull.Value Then
                Me._ArchivedByMercury = Nothing
            Else
                Me._ArchivedByMercury = dr(dr.GetOrdinal("ArchivedByMercury"))
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

            If dr(dr.GetOrdinal("RequestedBy")) Is DBNull.Value Then
                Me._RequestedBy = Nothing
            Else
                Me._RequestedBy = dr(dr.GetOrdinal("RequestedBy"))
            End If

            If dr(dr.GetOrdinal("Retention")) Is DBNull.Value Then
                Me._Retention = Nothing
            Else
                Me._Retention = dr(dr.GetOrdinal("Retention"))
            End If

            If dr(dr.GetOrdinal("Note")) Is DBNull.Value Then
                Me._Note = Nothing
            Else
                Me._Note = dr(dr.GetOrdinal("Note"))
            End If

            If dr(dr.GetOrdinal("AttachmentExist")) Is DBNull.Value Then
                Me._AttachmentExist = Nothing
            Else
                Me._AttachmentExist = dr(dr.GetOrdinal("AttachmentExist"))
            End If

            If dr(dr.GetOrdinal("Scenario")) Is DBNull.Value Then
                Me._Scenario = Nothing
            Else
                Me._Scenario = dr(dr.GetOrdinal("Scenario"))
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

            If dr(dr.GetOrdinal("Nominated")) Is DBNull.Value Then
                Me._Nominated = Nothing
            Else
                Me._Nominated = dr(dr.GetOrdinal("Nominated"))
            End If

            If dr(dr.GetOrdinal("FrameContract")) Is DBNull.Value Then
                Me._FrameContract = Nothing
            Else
                Me._FrameContract = dr(dr.GetOrdinal("FrameContract"))
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

            If dr(dr.GetOrdinal("NominatedApprovedByESTM")) Is DBNull.Value Then
                Me._NominatedApprovedByESTM = Nothing
            Else
                Me._NominatedApprovedByESTM = dr(dr.GetOrdinal("NominatedApprovedByESTM"))
            End If

        End Sub

#End Region

  End Class

End Namespace
