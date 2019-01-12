Imports Microsoft.VisualBasic
Imports System.Web.ModelBinding
Imports System.Web.UI.WebControls

Namespace PTS_MERCURY.BL

    Public Class BL
        Implements IDisposable

        Private db As PTS_MERCURY.db.SQL2008_794282_mercuryEntities = New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

#Region "Table_Budget"


        Public Function FormviewTable_Budget_GetItem() As PTS_MERCURY.db.Table_Budget
            Return Nothing
        End Function

        Public Sub FormviewTable_Budget_InsertItem(context As ModelMethodContext)
            Dim item = New PTS_MERCURY.db.Table_Budget
            context.TryUpdateModel(item)
            If context.ModelState.IsValid Then
                db.Table_Budget.Attach(item)
                db.Table_Budget.Add(item)
                db.SaveChanges()
            End If
        End Sub

        Public Function GridviewTable_Budget_GetData(<Control> ByVal ProjectId As Nullable(Of Integer)) As IQueryable(Of PTS_MERCURY.helper.Table_Budget.Table_Budget_ToGrid)

            If ProjectId IsNot Nothing Then
                Return (From C In db.Table_Budget Where C.ProjectID = ProjectId).Select(Function(e) New PTS_MERCURY.helper.Table_Budget.Table_Budget_ToGrid With _
                                                      {.BudgetID = e.BudgetID,
                                                       .ProjectID = e.ProjectID,
                                                       .CostCode = e.CostCode,
                                                       .Budget = e.Budget,
                                                       .PlannedToSpend = e.PlannedToSpend,
                                                       .PlannedToSpendCO = e.PlannedToSpendCO,
                                                       .Currency = e.Currency.Trim(),
                                                       .UpdatedPlannedRevenue = e.UpdatedPlannedRevenue,
                                                       .OriginalBOQ = e.OriginalBOQ,
                                                       .VCO = e.VCO,
                                                       .CostToGoBOQ = e.CostToGoBOQ,
                                                       .CostToGoVCO = e.CostToGoVCO})

            Else
                Return Nothing
            End If

        End Function

        Public Sub GridviewTable_Budget_UpdateItem(ByVal BudgetID As Int32, context As ModelMethodContext)
            Dim item As PTS_MERCURY.db.Table_Budget = Nothing
            item = (From C In db.Table_Budget Where C.BudgetID = BudgetID).ToList()(0)
            If item Is Nothing Then
                context.ModelState.AddModelError("", String.Format("Item with id {0} was not found", BudgetID))
                Return
            End If
            context.TryUpdateModel(item)
            If context.ModelState.IsValid Then
                db.SaveChanges()
            End If
        End Sub

        Public Sub GridviewTable_Budget_DeleteItem(ByVal BudgetID As Int32)
            Dim item = (From C In db.Table_Budget Where C.BudgetID = BudgetID).ToList()(0)
            db.Table_Budget.Attach(item)
            db.Table_Budget.Remove(item)
            db.SaveChanges()
        End Sub

#End Region

#Region "Table1_Project"


        Public Function FormviewTable1_Project_GetItem() As PTS_MERCURY.db.Table1_Project
            Return Nothing
        End Function

        Public Sub FormviewTable1_Project_InsertItem(context As ModelMethodContext)
            Dim item = New PTS_MERCURY.db.Table1_Project
            context.TryUpdateModel(item)
            If context.ModelState.IsValid Then
                db.Table1_Project.Attach(item)
                db.Table1_Project.Add(item)
                db.SaveChanges()
            End If
        End Sub

        Public Function GridviewTable1_Project_GetData() As IQueryable(Of PTS_MERCURY.db.Table1_Project)
            Return From C In db.Table1_Project
        End Function

        Public Sub GridviewTable1_Project_UpdateItem(ByVal ProjectID As Int16, context As ModelMethodContext)
            Dim item As PTS_MERCURY.db.Table1_Project = Nothing
            item = (From C In db.Table1_Project Where C.ProjectID = ProjectID).ToList()(0)
            If item Is Nothing Then
                context.ModelState.AddModelError("", String.Format("Item with id {0} was not found", ProjectID))
                Return
            End If
            context.TryUpdateModel(item)
            If context.ModelState.IsValid Then
                db.SaveChanges()
            End If
        End Sub

        Public Sub GridviewTable1_Project_DeleteItem(ByVal ProjectID As Int16)
            Dim item = (From C In db.Table1_Project Where C.ProjectID = ProjectID).ToList()(0)
            db.Table1_Project.Attach(item)
            db.Table1_Project.Remove(item)
            db.SaveChanges()
        End Sub

#End Region

#Region "Table7_CostCode"


        Public Function FormviewTable7_CostCode_GetItem() As PTS_MERCURY.db.Table7_CostCode
            Return Nothing
        End Function

        Public Sub FormviewTable7_CostCode_InsertItem(context As ModelMethodContext)
            Dim item = New PTS_MERCURY.db.Table7_CostCode
            context.TryUpdateModel(item)
            If context.ModelState.IsValid Then
                db.Table7_CostCode.Attach(item)
                db.Table7_CostCode.Add(item)
                db.SaveChanges()
            End If
        End Sub

        Public Function GridviewTable7_CostCode_GetData() As IQueryable(Of PTS_MERCURY.db.Table7_CostCode)
            Return From C In db.Table7_CostCode
        End Function

        Public Sub GridviewTable7_CostCode_UpdateItem(ByVal CostCode As String, context As ModelMethodContext)
            Dim item As New PTS_MERCURY.db.Table7_CostCode
            item = (From C In db.Table7_CostCode Where C.CostCode = CostCode).ToList()(0)
            If item Is Nothing Then
                context.ModelState.AddModelError("", String.Format("Item with id {0} was not found", CostCode))
                Return
            End If
            context.TryUpdateModel(item)
            If context.ModelState.IsValid Then
                db.SaveChanges()
            End If
        End Sub

        Public Sub GridviewTable7_CostCode_DeleteItem(ByVal CostCode As String)
            Dim item = (From C In db.Table7_CostCode Where C.CostCode = CostCode).ToList()(0)
            db.Table7_CostCode.Attach(item)
            db.Table7_CostCode.Remove(item)
            db.SaveChanges()
        End Sub

#End Region

#Region "Table7_CostDivision"


        Public Function FormviewTable7_CostDivision_GetItem() As PTS_MERCURY.db.Table7_CostDivision
            Return Nothing
        End Function

        Public Sub FormviewTable7_CostDivision_InsertItem(context As ModelMethodContext)
            Dim item = New PTS_MERCURY.db.Table7_CostDivision
            context.TryUpdateModel(item)
            If context.ModelState.IsValid Then
                db.Table7_CostDivision.Attach(item)
                db.Table7_CostDivision.Add(item)
                db.SaveChanges()
            End If
        End Sub

        Public Function GridviewTable7_CostDivision_GetData() As IQueryable(Of PTS_MERCURY.db.Table7_CostDivision)
            Return From C In db.Table7_CostDivision
        End Function

        Public Sub GridviewTable7_CostDivision_UpdateItem(ByVal CostVidisionID As String, context As ModelMethodContext)
            Dim item As PTS_MERCURY.db.Table7_CostDivision = Nothing
            item = (From C In db.Table7_CostDivision Where C.CostVidisionID = CostVidisionID).ToList()(0)
            If item Is Nothing Then
                context.ModelState.AddModelError("", String.Format("Item with id {0} was not found", CostVidisionID))
                Return
            End If
            context.TryUpdateModel(item)
            If context.ModelState.IsValid Then
                db.SaveChanges()
            End If
        End Sub

        Public Sub GridviewTable7_CostDivision_DeleteItem(ByVal CostVidisionID As String)
            Dim item = (From C In db.Table7_CostDivision Where C.CostVidisionID = CostVidisionID).ToList()(0)
            db.Table7_CostDivision.Attach(item)
            db.Table7_CostDivision.Remove(item)
            db.SaveChanges()
        End Sub

#End Region

#Region "Table7_CostDivision2"


        Public Function FormviewTable7_CostDivision2_GetItem() As PTS_MERCURY.db.Table7_CostDivision2
            Return Nothing
        End Function

        Public Sub FormviewTable7_CostDivision2_InsertItem(context As ModelMethodContext)
            Dim item = New PTS_MERCURY.db.Table7_CostDivision2
            context.TryUpdateModel(item)
            If context.ModelState.IsValid Then
                db.Table7_CostDivision2.Attach(item)
                db.Table7_CostDivision2.Add(item)
                db.SaveChanges()
            End If
        End Sub

        Public Function GridviewTable7_CostDivision2_GetData() As IQueryable(Of PTS_MERCURY.db.Table7_CostDivision2)
            Return From C In db.Table7_CostDivision2
        End Function

        Public Sub GridviewTable7_CostDivision2_UpdateItem(ByVal CostDivision2ID As String, context As ModelMethodContext)
            Dim item As PTS_MERCURY.db.Table7_CostDivision2 = Nothing
            item = (From C In db.Table7_CostDivision2 Where C.CostDivision2ID = CostDivision2ID).ToList()(0)
            If item Is Nothing Then
                context.ModelState.AddModelError("", String.Format("Item with id {0} was not found", CostDivision2ID))
                Return
            End If
            context.TryUpdateModel(item)
            If context.ModelState.IsValid Then
                db.SaveChanges()
            End If
        End Sub

        Public Sub GridviewTable7_CostDivision2_DeleteItem(ByVal CostDivision2ID As String)
            Dim item = (From C In db.Table7_CostDivision2 Where C.CostDivision2ID = CostDivision2ID).ToList()(0)
            db.Table7_CostDivision2.Attach(item)
            db.Table7_CostDivision2.Remove(item)
            db.SaveChanges()
        End Sub

#End Region

#Region "View_AddendumDetails"

        Public Function FormviewAddendumDetails_GetItem(<Control> ByVal addendumid As Integer) As PTS_MERCURY.db.View_AddendumDetails

            Try
                Return (From C In db.View_AddendumDetails Where C.AddendumID = addendumid).ToList()(0)
            Catch ex As Exception
                Return Nothing
            End Try

        End Function

#End Region

#Region "View_contractDetails"

        Public Function FormviewcontractDetails_GetItem(<Control> ByVal contractid As Integer) As PTS_MERCURY.db.View_ContractDetails

            Try
                Return (From C In db.View_ContractDetails Where C.ContractID = contractid).ToList()(0)
            Catch ex As Exception
                Return Nothing
            End Try

        End Function

#End Region

#Region "Table3_Invoice_AdditionalApprovalProjectUserMatrix"


        Public Function FormviewTable3_Invoice_AdditionalApprovalProjectUserMatrix_GetItem() As PTS_MERCURY.db.Table3_Invoice_AdditionalApprovalProjectUserMatrix
            Return Nothing
        End Function

        Public Sub FormviewTable3_Invoice_AdditionalApprovalProjectUserMatrix_InsertItem(context As ModelMethodContext)
            Dim item = New PTS_MERCURY.db.Table3_Invoice_AdditionalApprovalProjectUserMatrix
            context.TryUpdateModel(item)
            If context.ModelState.IsValid Then
                db.Table3_Invoice_AdditionalApprovalProjectUserMatrix.Attach(item)
                db.Table3_Invoice_AdditionalApprovalProjectUserMatrix.Add(item)
                db.SaveChanges()
            End If
        End Sub

        Public Function GridviewTable3_Invoice_AdditionalApprovalProjectUserMatrix_GetData() As IQueryable(Of PTS_MERCURY.db.Table3_Invoice_AdditionalApprovalProjectUserMatrix)
            Return From C In db.Table3_Invoice_AdditionalApprovalProjectUserMatrix
        End Function

        Public Sub GridviewTable3_Invoice_AdditionalApprovalProjectUserMatrix_UpdateItem(ByVal id As Int32, context As ModelMethodContext)
            Dim item As PTS_MERCURY.db.Table3_Invoice_AdditionalApprovalProjectUserMatrix = Nothing
            item = (From C In db.Table3_Invoice_AdditionalApprovalProjectUserMatrix Where C.id = id).ToList()(0)
            If item Is Nothing Then
                context.ModelState.AddModelError("", String.Format("Item with id {0} was not found", id))
                Return
            End If
            context.TryUpdateModel(item)
            If context.ModelState.IsValid Then
                db.SaveChanges()
            End If
        End Sub

        Public Sub GridviewTable3_Invoice_AdditionalApprovalProjectUserMatrix_DeleteItem(ByVal id As Int32)
            Dim item = (From C In db.Table3_Invoice_AdditionalApprovalProjectUserMatrix Where C.id = id).ToList()(0)
            db.Table3_Invoice_AdditionalApprovalProjectUserMatrix.Attach(item)
            db.Table3_Invoice_AdditionalApprovalProjectUserMatrix.Remove(item)
            db.SaveChanges()
        End Sub

#End Region

#Region "Table3_Invoice_AdditionalUserApprovals"


        Public Function FormviewTable3_Invoice_AdditionalUserApprovals_GetItem() As PTS_MERCURY.db.Table3_Invoice_AdditionalUserApprovals
            Return Nothing
        End Function

        Public Sub FormviewTable3_Invoice_AdditionalUserApprovals_InsertItem(context As ModelMethodContext)
            Dim item = New PTS_MERCURY.db.Table3_Invoice_AdditionalUserApprovals
            context.TryUpdateModel(item)
            If context.ModelState.IsValid Then
                db.Table3_Invoice_AdditionalUserApprovals.Attach(item)
                db.Table3_Invoice_AdditionalUserApprovals.Add(item)
                db.SaveChanges()
            End If
        End Sub

        Public Function GridviewTable3_Invoice_AdditionalUserApprovals_GetData() As IQueryable(Of PTS_MERCURY.db.Table3_Invoice_AdditionalUserApprovals)
            Return From C In db.Table3_Invoice_AdditionalUserApprovals
        End Function

        Public Sub GridviewTable3_Invoice_AdditionalUserApprovals_UpdateItem(ByVal id As Int32, context As ModelMethodContext)
            Dim item As PTS_MERCURY.db.Table3_Invoice_AdditionalUserApprovals = Nothing
            item = (From C In db.Table3_Invoice_AdditionalUserApprovals Where C.id = id).ToList()(0)
            If item Is Nothing Then
                context.ModelState.AddModelError("", String.Format("Item with id {0} was not found", id))
                Return
            End If
            context.TryUpdateModel(item)
            If context.ModelState.IsValid Then
                db.SaveChanges()
            End If
        End Sub

        Public Sub GridviewTable3_Invoice_AdditionalUserApprovals_DeleteItem(ByVal id As Int32)
            Dim item = (From C In db.Table3_Invoice_AdditionalUserApprovals Where C.id = id).ToList()(0)
            db.Table3_Invoice_AdditionalUserApprovals.Attach(item)
            db.Table3_Invoice_AdditionalUserApprovals.Remove(item)
            db.SaveChanges()
        End Sub

#End Region

#Region "Table_Addendum_UsersApprv_IgnoreUser"

        Public Function FormviewTable_Addendum_UsersApprv_IgnoreUser_GetItem() As PTS_MERCURY.db.Table_Addendum_UsersApprv_IgnoreUser
            Return Nothing
        End Function

        Public Sub FormviewTable_Addendum_UsersApprv_IgnoreUser_InsertItem(context As ModelMethodContext)
            Dim item = New PTS_MERCURY.db.Table_Addendum_UsersApprv_IgnoreUser
            context.TryUpdateModel(item)
            If context.ModelState.IsValid Then
                db.Table_Addendum_UsersApprv_IgnoreUser.Attach(item)
                db.Table_Addendum_UsersApprv_IgnoreUser.Add(item)
                db.SaveChanges()
            End If
        End Sub

        Public Function GridviewTable_Addendum_UsersApprv_IgnoreUser_GetData() As IQueryable(Of PTS_MERCURY.db.Table_Addendum_UsersApprv_IgnoreUser)
            Return From C In db.Table_Addendum_UsersApprv_IgnoreUser
        End Function

        Public Sub GridviewTable_Addendum_UsersApprv_IgnoreUser_UpdateItem(ByVal id As Int32, context As ModelMethodContext)
            Dim item As PTS_MERCURY.db.Table_Addendum_UsersApprv_IgnoreUser = Nothing
            item = (From C In db.Table_Addendum_UsersApprv_IgnoreUser Where C.id = id).ToList()(0)
            If item Is Nothing Then
                context.ModelState.AddModelError("", String.Format("Item with id {0} was not found", id))
                Return
            End If
            context.TryUpdateModel(item)
            If context.ModelState.IsValid Then
                db.SaveChanges()
            End If
        End Sub

        Public Sub GridviewTable_Addendum_UsersApprv_IgnoreUser_DeleteItem(ByVal id As Int32)
            Dim item = (From C In db.Table_Addendum_UsersApprv_IgnoreUser Where C.id = id).ToList()(0)
            db.Table_Addendum_UsersApprv_IgnoreUser.Attach(item)
            db.Table_Addendum_UsersApprv_IgnoreUser.Remove(item)
            db.SaveChanges()
        End Sub

#End Region

#Region "Table_Contract_UsersApprv_IgnoreUser"


        Public Function FormviewTable_Contract_UsersApprv_IgnoreUser_GetItem() As PTS_MERCURY.db.Table_Contract_UsersApprv_IgnoreUser
            Return Nothing
        End Function

        Public Sub FormviewTable_Contract_UsersApprv_IgnoreUser_InsertItem(context As ModelMethodContext)
            Dim item = New PTS_MERCURY.db.Table_Contract_UsersApprv_IgnoreUser
            context.TryUpdateModel(item)
            If context.ModelState.IsValid Then
                db.Table_Contract_UsersApprv_IgnoreUser.Attach(item)
                db.Table_Contract_UsersApprv_IgnoreUser.Add(item)
                db.SaveChanges()
            End If
        End Sub

        Public Function GridviewTable_Contract_UsersApprv_IgnoreUser_GetData() As IQueryable(Of PTS_MERCURY.db.Table_Contract_UsersApprv_IgnoreUser)
            Return From C In db.Table_Contract_UsersApprv_IgnoreUser
        End Function

        Public Sub GridviewTable_Contract_UsersApprv_IgnoreUser_UpdateItem(ByVal id As Int32, context As ModelMethodContext)
            Dim item As PTS_MERCURY.db.Table_Contract_UsersApprv_IgnoreUser = Nothing
            item = (From C In db.Table_Contract_UsersApprv_IgnoreUser Where C.id = id).ToList()(0)
            If item Is Nothing Then
                context.ModelState.AddModelError("", String.Format("Item with id {0} was not found", id))
                Return
            End If
            context.TryUpdateModel(item)
            If context.ModelState.IsValid Then
                db.SaveChanges()
            End If
        End Sub

        Public Sub GridviewTable_Contract_UsersApprv_IgnoreUser_DeleteItem(ByVal id As Int32)
            Dim item = (From C In db.Table_Contract_UsersApprv_IgnoreUser Where C.id = id).ToList()(0)
            db.Table_Contract_UsersApprv_IgnoreUser.Attach(item)
            db.Table_Contract_UsersApprv_IgnoreUser.Remove(item)
            db.SaveChanges()
        End Sub

#End Region

#Region "Dispose"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    db.Dispose()
                End If
            End If
            Me.disposedValue = True
        End Sub

        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class

End Namespace

