Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Net.Mail
Imports System.Web
Imports System.Net
Imports PTS_App_Code_VB_Project.PTS_MERCURY.helper.EmailGenerator

Public Class SendEmailApprovalMatrix

    Shared Sub Send(ByVal _ContractOrAddendumID As Integer, _
                    ByVal _TypeOfAction As Integer, _
                    Optional ByVal _PO_no As String = Nothing, _
                    Optional ByVal _InsertOrUpdate As String = Nothing, _
                    Optional ByVal _EmailBody As String = Nothing)
        ' --------Type Of Action---------
        ' 1- Contract Entered 
        ' 2- Addendum Entered
        ' 3- PO entered or updated
        ' 4- Frame contract approved
        ' 5- PO raised after Addendum approval of Frame Contract
        ' 6- It sends rejection comments on CONTRACTS to approval users
        ' 7- It sends rejection comments on ADDENDUMS to approval users
        ' 8- It sends rejection comments on CONTRACTS to approval users after updating of comment
        ' 9- It sends rejection comments on ADDENDUMS to approval users after updating of comment
        ' 10- It sends removal of rejection comments on CONTRACTS to approval users
        ' 11- It sends removal of rejection comments on ADDENDUMS to approval users
        ' 12- Raisal of new PO against contract. It is new logic in PTS
        ' 13- Raisal of new PO against addendum. It is new logic in PTS
        ' 14- Update of existing PO which has been raised against contract before. It is new logic in PTS
        ' 15- Zero Value Addendum executed

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = ""

            If _TypeOfAction = 1 Then
                sqlstring = "ApprovalContractSendEmail"
            ElseIf _TypeOfAction = 2 Then
                sqlstring = "ApprovalAddendumSendEmail"
            ElseIf _TypeOfAction = 3 Then
                sqlstring = "ApprovalPOInsertUpdateSendEmail"
            ElseIf _TypeOfAction = 4 Then
                sqlstring = "ApprovalPOInsertUpdateSendEmail"
            ElseIf _TypeOfAction = 5 Then
                sqlstring = "ApprovalAddendumSendEmailPo"
            ElseIf _TypeOfAction = 6 Then
                sqlstring = "RejectionCommentsContractSendEmail"
            ElseIf _TypeOfAction = 7 Then
                sqlstring = "RejectionCommentsAddendumSendEmail"
            ElseIf _TypeOfAction = 8 Then
                sqlstring = "RejectionCommentsContractSendEmail"
            ElseIf _TypeOfAction = 9 Then
                sqlstring = "RejectionCommentsAddendumSendEmail"
            ElseIf _TypeOfAction = 10 Then
                sqlstring = "RejectionCommentsContractSendEmail"
            ElseIf _TypeOfAction = 11 Then
                sqlstring = "RejectionCommentsAddendumSendEmail"
            ElseIf _TypeOfAction = 12 Then
                sqlstring = "ApprovalPOInsertUpdateSendEmail"
            ElseIf _TypeOfAction = 13 Then
                sqlstring = "ApprovalAddendumSendEmailPo"
            ElseIf _TypeOfAction = 14 Then
                sqlstring = "ApprovalAddendumSendEmailPo"
            ElseIf _TypeOfAction = 15 Then
                sqlstring = "ApprovalAddendumSendEmailPo"
            End If

            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = Data.CommandType.StoredProcedure

            Dim _ProjectID As Integer = 0

            If _TypeOfAction = 1 Then
                Dim ContractID As SqlParameter = cmd.Parameters.Add("@ContractID", Data.SqlDbType.Int)
                ContractID.Value = _ContractOrAddendumID
                _ProjectID = PTS.CoreTables.CreateDataReader.Create_Table_Contract(_ContractOrAddendumID).ProjectID
            ElseIf _TypeOfAction = 2 Then
                Dim AddendumID As SqlParameter = cmd.Parameters.Add("@AddendumID", Data.SqlDbType.Int)
                AddendumID.Value = _ContractOrAddendumID
                _ProjectID = PTS.CoreTables.CreateDataReader.Create_Table_Contract(PTS.CoreTables.CreateDataReader.Create_Table_Addendums(_ContractOrAddendumID).ContractID).ProjectID
            ElseIf _TypeOfAction = 3 Then
                Dim ContractID As SqlParameter = cmd.Parameters.Add("@ContractID", Data.SqlDbType.Int)
                ContractID.Value = _ContractOrAddendumID
                _ProjectID = PTS.CoreTables.CreateDataReader.Create_Table_Contract(_ContractOrAddendumID).ProjectID
            ElseIf _TypeOfAction = 4 Then
                Dim ContractID As SqlParameter = cmd.Parameters.Add("@ContractID", Data.SqlDbType.Int)
                ContractID.Value = _ContractOrAddendumID
                _ProjectID = PTS.CoreTables.CreateDataReader.Create_Table_Contract(_ContractOrAddendumID).ProjectID
            ElseIf _TypeOfAction = 5 Then
                Dim AddendumID As SqlParameter = cmd.Parameters.Add("@AddendumID", Data.SqlDbType.Int)
                AddendumID.Value = _ContractOrAddendumID
                _ProjectID = PTS.CoreTables.CreateDataReader.Create_Table_Contract(PTS.CoreTables.CreateDataReader.Create_Table_Addendums(_ContractOrAddendumID).ContractID).ProjectID
            ElseIf _TypeOfAction = 6 Then
                Dim ContractID As SqlParameter = cmd.Parameters.Add("@ContractID", Data.SqlDbType.Int)
                ContractID.Value = _ContractOrAddendumID
                _ProjectID = PTS.CoreTables.CreateDataReader.Create_Table_Contract(_ContractOrAddendumID).ProjectID
            ElseIf _TypeOfAction = 7 Then
                Dim AddendumID As SqlParameter = cmd.Parameters.Add("@AddendumID", Data.SqlDbType.Int)
                AddendumID.Value = _ContractOrAddendumID
                _ProjectID = PTS.CoreTables.CreateDataReader.Create_Table_Contract(PTS.CoreTables.CreateDataReader.Create_Table_Addendums(_ContractOrAddendumID).ContractID).ProjectID
            ElseIf _TypeOfAction = 8 Then
                Dim ContractID As SqlParameter = cmd.Parameters.Add("@ContractID", Data.SqlDbType.Int)
                ContractID.Value = _ContractOrAddendumID
                _ProjectID = PTS.CoreTables.CreateDataReader.Create_Table_Contract(_ContractOrAddendumID).ProjectID
            ElseIf _TypeOfAction = 9 Then
                Dim AddendumID As SqlParameter = cmd.Parameters.Add("@AddendumID", Data.SqlDbType.Int)
                AddendumID.Value = _ContractOrAddendumID
                _ProjectID = PTS.CoreTables.CreateDataReader.Create_Table_Contract(PTS.CoreTables.CreateDataReader.Create_Table_Addendums(_ContractOrAddendumID).ContractID).ProjectID
            ElseIf _TypeOfAction = 10 Then
                Dim ContractID As SqlParameter = cmd.Parameters.Add("@ContractID", Data.SqlDbType.Int)
                ContractID.Value = _ContractOrAddendumID
                _ProjectID = PTS.CoreTables.CreateDataReader.Create_Table_Contract(_ContractOrAddendumID).ProjectID
            ElseIf _TypeOfAction = 11 Then
                Dim AddendumID As SqlParameter = cmd.Parameters.Add("@AddendumID", Data.SqlDbType.Int)
                AddendumID.Value = _ContractOrAddendumID
                _ProjectID = PTS.CoreTables.CreateDataReader.Create_Table_Contract(PTS.CoreTables.CreateDataReader.Create_Table_Addendums(_ContractOrAddendumID).ContractID).ProjectID
            ElseIf _TypeOfAction = 12 Then
                Dim ContractID As SqlParameter = cmd.Parameters.Add("@ContractID", Data.SqlDbType.Int)
                ContractID.Value = _ContractOrAddendumID
                _ProjectID = PTS.CoreTables.CreateDataReader.Create_Table_Contract(_ContractOrAddendumID).ProjectID
            ElseIf _TypeOfAction = 13 Then
                Dim AddendumID As SqlParameter = cmd.Parameters.Add("@AddendumID", Data.SqlDbType.Int)
                AddendumID.Value = _ContractOrAddendumID
                _ProjectID = PTS.CoreTables.CreateDataReader.Create_Table_Contract(PTS.CoreTables.CreateDataReader.Create_Table_Addendums(_ContractOrAddendumID).ContractID).ProjectID
            ElseIf _TypeOfAction = 14 Then
                Dim AddendumID As SqlParameter = cmd.Parameters.Add("@AddendumID", Data.SqlDbType.Int)
                AddendumID.Value = _ContractOrAddendumID
                _ProjectID = PTS.CoreTables.CreateDataReader.Create_Table_Contract(PTS.CoreTables.CreateDataReader.Create_Table_Addendums(_ContractOrAddendumID).ContractID).ProjectID
            ElseIf _TypeOfAction = 15 Then
                Dim AddendumID As SqlParameter = cmd.Parameters.Add("@AddendumID", Data.SqlDbType.Int)
                AddendumID.Value = _ContractOrAddendumID
                _ProjectID = PTS.CoreTables.CreateDataReader.Create_Table_Contract(PTS.CoreTables.CreateDataReader.Create_Table_Addendums(_ContractOrAddendumID).ContractID).ProjectID
            End If

            Dim dr As SqlDataReader = cmd.ExecuteReader

            Dim _fromText As String = ""

            If _TypeOfAction = 1 Then
                _fromText = "Запрос на согласование контракта" ' Contract Approval Request

            ElseIf _TypeOfAction = 2 Then
                _fromText = "Запрос дополнительного соглашения" ' Addendum Approval Request

            ElseIf _TypeOfAction = 3 And _InsertOrUpdate.Trim = "insert" Then
                _fromText = "Завершен заказ на поставку" ' PO has been raised

            ElseIf _TypeOfAction = 3 And _InsertOrUpdate.Trim = "update" Then
                _fromText = "Заказ на поставку обновлен" ' PO has been updated

            ElseIf _TypeOfAction = 4 Then

                _fromText = "Утвержден рамочный договор" ' Frame contract approved

            ElseIf _TypeOfAction = 5 Then
                _fromText = "Завершен заказ на поставку" ' PO has been raised

            ElseIf _TypeOfAction = 6 Then
                _fromText = "Несогласие по контракту" ' Disagreement on Contract

            ElseIf _TypeOfAction = 7 Then
                _fromText = "Разногласия по дополнительному соглашению" ' Disagreement on Addendum

            ElseIf _TypeOfAction = 8 Then
                _fromText = "Несогласие по контракту" ' Disagreement on Contract

            ElseIf _TypeOfAction = 9 Then
                _fromText = "Разногласия по дополнительному соглашению" ' Disagreement on Addendum

            ElseIf _TypeOfAction = 10 Then
                _fromText = "Несогласие по контракту" ' Disagreement on Contract

            ElseIf _TypeOfAction = 11 Then
                _fromText = "Разногласия по дополнительному соглашению" ' Disagreement on Addendum

            ElseIf _TypeOfAction = 12 Then
                _fromText = "Завершен заказ на поставку" ' PO has been raised

            ElseIf _TypeOfAction = 13 Then
                _fromText = "Завершен заказ на поставку" ' PO has been raised

            ElseIf _TypeOfAction = 14 Then
                _fromText = "Заказ на поставку обновлен" ' PO has been updated

            ElseIf _TypeOfAction = 15 Then
                _fromText = "Дополнительное соглашение с нулевой стоимостью" ' Zero value addendum
            End If

            Dim MailFrom As New MailAddress("procurement@mercuryeng.ru", _fromText)

            Dim mm1 As New MailMessage()
            mm1.BodyEncoding = System.Text.Encoding.UTF8

            mm1.From = MailFrom
            mm1.Body = ""

            If _ProjectID = 197 Or _ProjectID = 195 Then ' Nevskaya Ratusha Projects
                mm1.To.Add("arno.grah@mercuryeng.ru")
            End If

            While dr.Read
                If dr(0).ToString.Length <> 0 Then
                    If HttpContext.Current.User.Identity.Name.ToString.ToLower = "savas" Then
                        mm1.Body = mm1.Body + dr(0).ToString + Environment.NewLine
                    Else
                        If (dr(0).ToString.ToLower <> "evgeniya.emelianova@mercuryeng.ru") Then
                            mm1.To.Add(dr(0).ToString)
                        End If
                    End If
                End If
            End While

            ' Remove this after implementing general notification matrix
            'If _TypeOfAction = 1 Then
            '    'sqlstring = "ApprovalContractSendEmail"
            '    SetEmailToExceptionalPerson(_ContractOrAddendumID, "C", mm1)

            'ElseIf _TypeOfAction = 2 Then
            '    'sqlstring = "ApprovalAddendumSendEmail"
            '    SetEmailToExceptionalPerson(_ContractOrAddendumID, "A", mm1)
            'ElseIf _TypeOfAction = 3 Then
            '    'sqlstring = "ApprovalPOInsertUpdateSendEmail"
            '    SetEmailToExceptionalPerson(_ContractOrAddendumID, "C", mm1)
            'ElseIf _TypeOfAction = 4 Then
            '    'sqlstring = "ApprovalPOInsertUpdateSendEmail"
            '    SetEmailToExceptionalPerson(_ContractOrAddendumID, "C", mm1)
            'ElseIf _TypeOfAction = 5 Then
            '    'sqlstring = "ApprovalAddendumSendEmailPo"
            '    SetEmailToExceptionalPerson(_ContractOrAddendumID, "A", mm1)
            'ElseIf _TypeOfAction = 6 Then
            '    'sqlstring = "RejectionCommentsContractSendEmail"
            '    SetEmailToExceptionalPerson(_ContractOrAddendumID, "C", mm1)
            'ElseIf _TypeOfAction = 7 Then
            '    'sqlstring = "RejectionCommentsAddendumSendEmail"
            '    SetEmailToExceptionalPerson(_ContractOrAddendumID, "A", mm1)
            'ElseIf _TypeOfAction = 8 Then
            '    'sqlstring = "RejectionCommentsContractSendEmail"
            '    SetEmailToExceptionalPerson(_ContractOrAddendumID, "C", mm1)
            'ElseIf _TypeOfAction = 9 Then
            '    'sqlstring = "RejectionCommentsAddendumSendEmail"
            '    SetEmailToExceptionalPerson(_ContractOrAddendumID, "A", mm1)
            'ElseIf _TypeOfAction = 10 Then
            '    'sqlstring = "RejectionCommentsContractSendEmail"
            '    SetEmailToExceptionalPerson(_ContractOrAddendumID, "C", mm1)
            'ElseIf _TypeOfAction = 11 Then
            '    'sqlstring = "RejectionCommentsAddendumSendEmail"
            '    SetEmailToExceptionalPerson(_ContractOrAddendumID, "A", mm1)
            'ElseIf _TypeOfAction = 12 Then
            '    'sqlstring = "ApprovalPOInsertUpdateSendEmail"
            '    SetEmailToExceptionalPerson(_ContractOrAddendumID, "C", mm1)
            'ElseIf _TypeOfAction = 13 Then
            '    'sqlstring = "ApprovalAddendumSendEmailPo"
            '    SetEmailToExceptionalPerson(_ContractOrAddendumID, "A", mm1)
            'ElseIf _TypeOfAction = 14 Then
            '    'sqlstring = "ApprovalAddendumSendEmailPo"
            '    SetEmailToExceptionalPerson(_ContractOrAddendumID, "A", mm1)
            'ElseIf _TypeOfAction = 15 Then
            '    'sqlstring = "ApprovalAddendumSendEmailPo"
            '    SetEmailToExceptionalPerson(_ContractOrAddendumID, "A", mm1)
            'End If
            ' Remove this after implementing general notification matrix

            mm1.To.Add("sk@mercuryeng.ru")

            dr.Close()

            If _TypeOfAction = 1 Then
                mm1.Subject = "Введен проект договора. Требуется одобрение." ' Draft Contract entered. Approval required.

            ElseIf _TypeOfAction = 2 Then
                mm1.Subject = "Введено дополнительное соглашение. Требуется одобрение." ' Draft Addendum entered. Approval required.

            ElseIf _TypeOfAction = 3 AndAlso _InsertOrUpdate.Trim = "insert" Then
                mm1.Subject = "Запуск заказа" ' PO raised

            ElseIf _TypeOfAction = 3 AndAlso _InsertOrUpdate.Trim = "update" Then
                mm1.Subject = "Обновление заказа на поставку" ' PO updated

            ElseIf _TypeOfAction = 4 Then

                Using adapter As New MercuryTableAdapters.Table6_SupplierTableAdapter
                    Dim Table As New Mercury.Table6_SupplierDataTable
                    Table = adapter.GetDataBySupplierID(PTS.CoreTables.CreateDataReader.Create_Table_Contract(_ContractOrAddendumID).SupplierID)
                    For Each _row In Table
                        mm1.Subject = "Утвержден рамочный договор (" + _row.SupplierName + ")" ' Frame contract approved
                    Next
                    adapter.Dispose()
                End Using

            ElseIf _TypeOfAction = 5 Then
                mm1.Subject = "Запуск заказа" ' PO raised

            ElseIf _TypeOfAction = 6 Then
                mm1.Subject = NameOfLeadLawyer.GetNameFromFunction.ToUpper + " Не согласен с договором" ' does not agree with contract

            ElseIf _TypeOfAction = 7 Then
                mm1.Subject = NameOfLeadLawyer.GetNameFromFunction.ToUpper + " Не согласен с дополнительным соглашением" ' does not agree with addendum

            ElseIf _TypeOfAction = 8 Then
                mm1.Subject = NameOfLeadLawyer.GetNameFromFunction.ToUpper + " Обновленный существующий комментарий к несогласованному контракту " ' updated existing comment on the disagreed contract 

            ElseIf _TypeOfAction = 9 Then
                mm1.Subject = NameOfLeadLawyer.GetNameFromFunction.ToUpper + " Обновить существующий комментарий по несогласованному дополнительному соглашению " ' updated existing comment on the disagreed addendum

            ElseIf _TypeOfAction = 10 Then
                mm1.Subject = NameOfLeadLawyer.GetNameFromFunction.ToUpper + " Устранено несогласие с контрактом " ' removed disagreement on contract

            ElseIf _TypeOfAction = 11 Then
                mm1.Subject = NameOfLeadLawyer.GetNameFromFunction.ToUpper + " Устранено несогласие с дополнительным соглашением " ' removed disagreement on addendum

            ElseIf _TypeOfAction = 12 Then
                mm1.Subject = "Заказа на поставку начаты для контракта" ' PO raised against the contract

            ElseIf _TypeOfAction = 13 Then
                mm1.Subject = "Заказ на поставку начался для дополнительного соглашения" ' PO raised against the addendum

            ElseIf _TypeOfAction = 14 Then
                mm1.Subject = "Заказ на поставку обновлен после замены дополнительного соглашения" ' PO updated after replace addendum

            ElseIf _TypeOfAction = 15 Then
                mm1.Subject = "Дополнительное соглашение с нулевой стоимостью одобрено" ' Zero Value Addendum approved
            End If

            '1	GO TO APPROVAL PAGE
            '2	GO TO APPROVAL PAGE
            '3	LINK TO DEFINE INVOICE
            '4	This frame contract 
            '5	LINK TO DEFINE INVOICE
            '6	 doesnt agree on this contract because of the reasons mentioned below. You can click this link to go to approval page
            '7	 doesnt agree on this addendum because of the reasons mentioned below. You can click this link to go to approval page
            '8	 updated existing comment on the disagreed contract as mentioned below. You can click this link to go to approval page
            '9	 updated existing comment on the disagreed addendum as mentioned below. You can click this link to go to approval page
            '10	 removed disagreement on this contract. You can click this link to go to approval page
            '11	 removed disagreement on this addendum. You can click this link to go to approval page
            '12	LINK TO DEFINE INVOICE
            '13	LINK TO DEFINE INVOICE
            '14	LINK TO DEFINE INVOICE
            '15	 This ZERO VALUE addendum has been approved. You can see addendum details on this link.

            Dim emailGeneratorHelper = New EmailGeneratorHelper()

            If _TypeOfAction = 1 Then
                mm1.Body = emailGeneratorHelper.getContractApprovalRequestEmailBody(_ContractOrAddendumID)
                'mm1.Body = mm1.Body + Environment.NewLine + _
                '" <a href=" + """" + "http://pts.mercuryeng.ru/contractdetails.aspx?ContractID=" + _ContractOrAddendumID.ToString + """" + " target=" + """" + "_blank" + """" + ">Перейти на страницу ОФИЦИАЛЬНОГО УТВЕРЖДЕНИЯ</a> " + _
                '  Environment.NewLine + _
                '  _EmailBody

            ElseIf _TypeOfAction = 2 Then
                mm1.Body = emailGeneratorHelper.getAddendumApprovalRequestEmailBody(_ContractOrAddendumID)
                'mm1.Body = mm1.Body + Environment.NewLine + _
                '" <a href=" + """" + "http://pts.mercuryeng.ru/addendumdetails.aspx?AddendumID=" + _ContractOrAddendumID.ToString + """" + " target=" + """" + "_blank" + """" + ">Перейти на страницу ОФИЦИАЛЬНОГО УТВЕРЖДЕНИЯ</a> " + _
                '  Environment.NewLine + _
                '  _EmailBody

            ElseIf _TypeOfAction = 3 Then
                mm1.Body = emailGeneratorHelper.getPOHasBeenRaisedEmailBody(_PO_no)
                'mm1.Body = mm1.Body + Environment.NewLine + _
                '" <a href=" + """" + "http://pts.mercuryeng.ru/invoicedefine.aspx?PoNo=" + _PO_no + "&ProjectID=" + getProjectID(_PO_no) + "&PO_link=" + """" + " target=" + """" + "_blank" + """" + ">ССЫЛКА ДЛЯ ОПРЕДЕЛЕНИЯ СЧЕТА</a> " + _
                '  Environment.NewLine + _
                '  _EmailBody

            ElseIf _TypeOfAction = 4 Then
                mm1.Body = emailGeneratorHelper.getFrameContractApprovedEmailBody(_ContractOrAddendumID)
                'mm1.Body = mm1.Body + Environment.NewLine + _
                '" <a href=" + """" + "http://pts.mercuryeng.ru/contractdetails.aspx?ContractID=" + _ContractOrAddendumID.ToString + """" + " target=" + """" + "_blank" + """" + ">Этот рамочный контракт</a> " + _
                '  _EmailBody

            ElseIf _TypeOfAction = 5 Then
                mm1.Body = emailGeneratorHelper.getPOHasBeenRaisedForAddendumEmailBody(_PO_no)
                'mm1.Body = mm1.Body + Environment.NewLine + _
                '" <a href=" + """" + "http://pts.mercuryeng.ru/invoicedefine.aspx?PoNo=" + _PO_no + "&ProjectID=" + getProjectID(_PO_no) + "&PO_link=" + """" + " target=" + """" + "_blank" + """" + ">ССЫЛКА ДЛЯ ОПРЕДЕЛЕНИЯ СЧЕТА</a> " + _
                '  Environment.NewLine + _
                '  _EmailBody

            ElseIf _TypeOfAction = 6 Then
                mm1.Body = emailGeneratorHelper.getDisagreementOnContractEmailBody(_ContractOrAddendumID)
                'mm1.Body = mm1.Body + Environment.NewLine + _
                '" <a href=" + """" + "http://pts.mercuryeng.ru/contractdetails.aspx?ContractID=" + _ContractOrAddendumID.ToString + """" + " target=" + """" + "_blank" + """" + ">" + NameOfLeadLawyer.GetNameFromFunction.ToUpper + " не согласен с этим договором. Причина указана ниже. Вы можете нажать эту ссылку, чтобы перейти на страницу утверждения</a> " + _
                '  Environment.NewLine + _
                '  _EmailBody

            ElseIf _TypeOfAction = 7 Then
                mm1.Body = emailGeneratorHelper.getDisagreementOnAddendumEmailBody(_ContractOrAddendumID)
                'mm1.Body = mm1.Body + Environment.NewLine + _
                '" <a href=" + """" + "http://pts.mercuryeng.ru/addendumdetails.aspx?AddendumID=" + _ContractOrAddendumID.ToString + """" + " target=" + """" + "_blank" + """" + ">" + NameOfLeadLawyer.GetNameFromFunction.ToUpper + " не согласен с этим дополнительным соглашением. Причина указана ниже. Вы можете нажать эту ссылку, чтобы перейти на страницу утверждения</a> " + _
                '  Environment.NewLine + _
                '  _EmailBody

            ElseIf _TypeOfAction = 8 Then
                mm1.Body = mm1.Body + Environment.NewLine + _
                " <a href=" + """" + "http://pts.mercuryeng.ru/contractdetails.aspx?ContractID=" + _ContractOrAddendumID.ToString + """" + " target=" + """" + "_blank" + """" + ">" + NameOfLeadLawyer.GetNameFromFunction.ToUpper + " обновленный существующий комментарий к несогласованному контракту, как указано ниже. Вы можете нажать эту ссылку, чтобы перейти на страницу утверждения</a> " + _
                  Environment.NewLine + _
                  _EmailBody

            ElseIf _TypeOfAction = 9 Then
                mm1.Body = mm1.Body + Environment.NewLine + _
                " <a href=" + """" + "http://pts.mercuryeng.ru/addendumdetails.aspx?AddendumID=" + _ContractOrAddendumID.ToString + """" + " target=" + """" + "_blank" + """" + ">" + NameOfLeadLawyer.GetNameFromFunction.ToUpper + " обновленный существующий комментарий к несогласованному дополнительному соглашению, как указано ниже. Вы можете нажать эту ссылку, чтобы перейти на страницу утверждения</a> " + _
                  Environment.NewLine + _
                  _EmailBody

            ElseIf _TypeOfAction = 10 Then
                mm1.Body = emailGeneratorHelper.getDisagreementOnContractRemovedEmailBody(_ContractOrAddendumID)
                'mm1.Body = mm1.Body + Environment.NewLine + _
                '" <a href=" + """" + "http://pts.mercuryeng.ru/contractdetails.aspx?ContractID=" + _ContractOrAddendumID.ToString + """" + " target=" + """" + "_blank" + """" + ">" + NameOfLeadLawyer.GetNameFromFunction.ToUpper + " устранены разногласия по этому контракту. Вы можете нажать эту ссылку, чтобы перейти на страницу утверждения</a> " + _
                '  Environment.NewLine + _
                '  _EmailBody

            ElseIf _TypeOfAction = 11 Then
                mm1.Body = emailGeneratorHelper.getDisagreementOnAddendumRemovedEmailBody(_ContractOrAddendumID)
                'mm1.Body = mm1.Body + Environment.NewLine + _
                '" <a href=" + """" + "http://pts.mercuryeng.ru/addendumdetails.aspx?AddendumID=" + _ContractOrAddendumID.ToString + """" + " target=" + """" + "_blank" + """" + ">" + NameOfLeadLawyer.GetNameFromFunction.ToUpper + " устранены разногласия по этому дополнительному соглашению. Вы можете нажать эту ссылку, чтобы перейти на страницу утверждения</a> " + _
                '  Environment.NewLine + _
                '  _EmailBody

            ElseIf _TypeOfAction = 12 Then
                mm1.Body = emailGeneratorHelper.getPOHasBeenRaisedEmailBody(_PO_no)
                'mm1.Body = mm1.Body + Environment.NewLine + _
                '" <a href=" + """" + "http://pts.mercuryeng.ru/invoicedefine.aspx?PoNo=" + _PO_no + "&ProjectID=" + getProjectID(_PO_no) + "&PO_link=" + """" + " target=" + """" + "_blank" + """" + ">ССЫЛКА ДЛЯ ОПРЕДЕЛЕНИЯ СЧЕТА</a> " + _
                '  Environment.NewLine + _
                '  _EmailBody

            ElseIf _TypeOfAction = 13 Then
                mm1.Body = emailGeneratorHelper.getPOHasBeenRaisedForAddendumEmailBody(_PO_no)
                'mm1.Body = mm1.Body + Environment.NewLine + _
                '" <a href=" + """" + "http://pts.mercuryeng.ru/invoicedefine.aspx?PoNo=" + _PO_no + "&ProjectID=" + getProjectID(_PO_no) + "&PO_link=" + """" + " target=" + """" + "_blank" + """" + ">ССЫЛКА ДЛЯ ОПРЕДЕЛЕНИЯ СЧЕТА</a> " + _
                '  Environment.NewLine + _
                '  _EmailBody

            ElseIf _TypeOfAction = 14 Then
                mm1.Body = emailGeneratorHelper.getPOHasBeenUpdatedEmailBody(_ContractOrAddendumID)
                'mm1.Body = mm1.Body + Environment.NewLine + _
                '" <a href=" + """" + "http://pts.mercuryeng.ru/invoicedefine.aspx?PoNo=" + _PO_no + "&ProjectID=" + getProjectID(_PO_no) + "&PO_link=" + """" + " target=" + """" + "_blank" + """" + ">ССЫЛКА ДЛЯ ОПРЕДЕЛЕНИЯ СЧЕТА</a> " + _
                '  Environment.NewLine + _
                '  _EmailBody

            ElseIf _TypeOfAction = 15 Then
                mm1.Body = emailGeneratorHelper.getZeroValueAddendumEmailBody(_ContractOrAddendumID)
                'mm1.Body = mm1.Body + Environment.NewLine + _
                '" <a href=" + """" + "http://pts.mercuryeng.ru/addendumdetails.aspx?AddendumID=" + _ContractOrAddendumID.ToString + """" + " target=" + """" + "_blank" + """" + ">" + " Без изменения стоимости договора утвержден.Вы можете увидеть дополнительную информацию об этой ссылке.</a> "
            End If

            If _TypeOfAction = 1 Then
                ' add attachment if exist
                Dim path As String = HttpContext.Current.Server.MapPath(GetDOCattachmentContract(_ContractOrAddendumID))
                Dim file As System.IO.FileInfo = New System.IO.FileInfo(path)
                If file.Exists Then
                    mm1.Attachments.Add(New Attachment(path))
                End If

            ElseIf _TypeOfAction = 2 Then
                ' add attachment if exist
                Dim path As String = HttpContext.Current.Server.MapPath(GetDOCattachmentAddendum(_ContractOrAddendumID))
                Dim file As System.IO.FileInfo = New System.IO.FileInfo(path)
                If file.Exists Then
                    mm1.Attachments.Add(New Attachment(path))
                End If

            ElseIf _TypeOfAction = 3 Then
                ' No attachment required

            ElseIf _TypeOfAction = 4 Then
                ' No attachment required

            ElseIf _TypeOfAction = 5 Then
                ' No attachment required

            ElseIf _TypeOfAction = 6 Then
                ' No attachment required

            ElseIf _TypeOfAction = 7 Then
                ' No attachment required

            ElseIf _TypeOfAction = 8 Then
                ' No attachment required

            ElseIf _TypeOfAction = 9 Then
                ' No attachment required

            ElseIf _TypeOfAction = 10 Then
                ' No attachment required

            ElseIf _TypeOfAction = 11 Then
                ' No attachment required

            ElseIf _TypeOfAction = 12 Then
                ' No attachment required

            ElseIf _TypeOfAction = 13 Then
                ' No attachment required

            ElseIf _TypeOfAction = 14 Then
                ' No attachment required

            ElseIf _TypeOfAction = 15 Then
                ' No attachment required
            End If

            mm1.IsBodyHtml = True

            Dim smtp As New SmtpClient_RussianEncoded
            'Try
            smtp.Send(mm1)
            'Catch ex As Exception
            'End Try
            con.Close()
            con.Dispose()
        End Using
    End Sub

    'Shared Sub SetEmailToExceptionalPerson(ByVal _id As Integer, ByVal _type As String, ByVal _mm As MailMessage)

    '    Using context As New PTS_MERCURY.db.SQL2008_794282_mercuryEntities

    '        Dim Project_ID As Int16
    '        If _type = "C" Then
    '            Dim _C = (From C In context.Table_Contracts Where C.ContractID = _id Select C).ToList()(0)
    '            Project_ID = _C.ProjectID

    '        ElseIf _type = "A" Then
    '            Dim _A = (From C In context.Table_Contracts Join A In context.Table_Addendums On C.ContractID Equals A.ContractID Where A.AddendumID = _id Select C).ToList()(0)
    '            Project_ID = _A.ProjectID
    '        End If

    '        If Project_ID = 197 Or Project_ID = 195 Then
    '            _mm.To.Add("anton.mokrov@mercuryeng.ru")
    '        End If

    '    End Using

    'End Sub

    Shared Function getProjectID(ByVal _Po_Po As String) As String

        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = " SELECT [Project_ID] FROM [Table2_PONo] WHERE PO_No = @PO_No "

            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = Data.CommandType.Text

            Dim PO_No As SqlParameter = cmd.Parameters.Add("@PO_No", Data.SqlDbType.NVarChar)
            PO_No.Value = _Po_Po

            Dim dr As SqlDataReader = cmd.ExecuteReader

            Dim _return As String = ""

            While dr.Read
                _return = dr(0).ToString
            End While

            dr.Close()
            con.Close()
            con.Dispose()

            Return _return

        End Using

    End Function

    Shared Function GetDOCattachmentContract(ByVal _ContractID As Integer) As String
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = "ApprovalGetContractDOC"

            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = Data.CommandType.StoredProcedure

            Dim ContractID As SqlParameter = cmd.Parameters.Add("@ContractID", Data.SqlDbType.Int)

            ContractID.Value = _ContractID
            Dim dr As SqlDataReader = cmd.ExecuteReader

            Dim _return As String = ""

            While dr.Read
                _return = dr(0).ToString
            End While

            dr.Close()
            con.Close()
            con.Dispose()

            Return _return

        End Using

    End Function

    Shared Function GetDOCattachmentAddendum(ByVal _AddendumID As Integer) As String
        Using con As SqlConnection = ConnectionStringsPTS.GetConnectionStringMain
            con.Open()
            Dim sqlstring As String = "ApprovalGetAddendumDOC"

            Dim cmd As New SqlCommand(sqlstring, con)
            cmd.CommandType = Data.CommandType.StoredProcedure

            Dim AddendumID As SqlParameter = cmd.Parameters.Add("@AddendumID", Data.SqlDbType.Int)

            AddendumID.Value = _AddendumID
            Dim dr As SqlDataReader = cmd.ExecuteReader

            Dim _return As String = ""

            While dr.Read
                _return = dr(0).ToString
            End While

            dr.Close()
            con.Close()
            con.Dispose()

            Return _return

        End Using

    End Function
End Class


Public Class EmailGeneratorHelper
    Private localHost As String = PTS_App_Code_VB_Project.PTS_MERCURY.helper.Garbage.MagicString.LocalHostAdress

    Public Function getEverybodyApprovedContractEmailBody(id As Integer) As String
        Dim myClient As New WebClient()
        myClient.Encoding = System.Text.Encoding.UTF8
        Try
            Return myClient.DownloadString((localHost & Convert.ToString("/PtsWorker/EmailGenerator/EverybodyApprovedContract/")) + id.ToString())
        Catch generatedExceptionName As Exception
            Return "Error"
        End Try
    End Function

    Public Function getEverybodyApprovedAddendumEmailBody(id As Integer) As String
        Dim myClient As New WebClient()
        myClient.Encoding = System.Text.Encoding.UTF8

        Try
            Return myClient.DownloadString((localHost & Convert.ToString("/PtsWorker/EmailGenerator/EverybodyApprovedAddendum/")) + id.ToString())
        Catch generatedExceptionName As Exception
            Return "error"
        End Try
    End Function

    Public Function getLawyersApprovedContractEmailBody(id As Integer) As String
        Dim myClient As New WebClient()
        myClient.Encoding = System.Text.Encoding.UTF8
        Try
            Return myClient.DownloadString((localHost & Convert.ToString("/PtsWorker/EmailGenerator/LawyersApprovedContract/")) + id.ToString())
        Catch generatedExceptionName As Exception
            Return "error"
        End Try
    End Function

    Public Function getLawyersApprovedAddendumEmailBody(id As Integer) As String
        Dim myClient As New WebClient()
        myClient.Encoding = System.Text.Encoding.UTF8
        Try
            Return myClient.DownloadString((localHost & Convert.ToString("/PtsWorker/EmailGenerator/LawyersApprovedAddendum/")) + id.ToString())
        Catch generatedExceptionName As Exception
            Return "error"
        End Try
    End Function

    Public Function getContractApprovalRequestEmailBody(id As Integer) As String
        Dim myClient As New WebClient()
        myClient.Encoding = System.Text.Encoding.UTF8
        Try
            Return myClient.DownloadString((localHost & Convert.ToString("/PtsWorker/EmailGenerator/ContractApprovalRequest/")) + id.ToString())
        Catch generatedExceptionName As Exception
            Return "error"
        End Try
    End Function

    Public Function getAddendumApprovalRequestEmailBody(id As Integer) As String
        Dim myClient As New WebClient()
        myClient.Encoding = System.Text.Encoding.UTF8

        Try
            Return myClient.DownloadString((localHost & Convert.ToString("/PtsWorker/EmailGenerator/AddendumApprovalRequest/")) + id.ToString())
        Catch generatedExceptionName As Exception
            Return "error"
        End Try
    End Function

    Public Function getPOHasBeenRaisedEmailBody(id As String) As String
        Dim myClient As New WebClient()
        myClient.Encoding = System.Text.Encoding.UTF8
        Try
            Return myClient.DownloadString((localHost & Convert.ToString("/PtsWorker/EmailGenerator/POHasBeenRaised/")) + id.ToString())
        Catch generatedExceptionName As Exception
            Return "error"
        End Try
    End Function

    Public Function getPOHasBeenRaisedForAddendumEmailBody(id As String) As String
        Dim myClient As New WebClient()
        myClient.Encoding = System.Text.Encoding.UTF8
        Try
            Return myClient.DownloadString((localHost & Convert.ToString("/PtsWorker/EmailGenerator/POHasBeenRaisedForAddendum/")) + id.ToString())
        Catch generatedExceptionName As Exception
            Return "error"
        End Try
    End Function

    Public Function getPOHasBeenUpdatedEmailBody(id As String) As String
        Dim myClient As New WebClient()
        myClient.Encoding = System.Text.Encoding.UTF8
        Try
            Return myClient.DownloadString((localHost & Convert.ToString("/PtsWorker/EmailGenerator/POHasBeenUpdated/")) + id.ToString())
        Catch generatedExceptionName As Exception
            Return "error"
        End Try
    End Function

    Public Function getFrameContractApprovedEmailBody(id As Integer) As String
        Dim myClient As New WebClient()
        myClient.Encoding = System.Text.Encoding.UTF8
        Try
            Return myClient.DownloadString((localHost & Convert.ToString("/PtsWorker/EmailGenerator/FrameContractApproved/")) + id.ToString())
        Catch generatedExceptionName As Exception
            Return "error"
        End Try
    End Function

    Public Function getDisagreementOnContractEmailBody(id As Integer) As String
        Dim myClient As New WebClient()
        myClient.Encoding = System.Text.Encoding.UTF8
        Try
            Return myClient.DownloadString((localHost & Convert.ToString("/PtsWorker/EmailGenerator/DisagreementOnContract/")) + id.ToString())
        Catch generatedExceptionName As Exception
            Return "error"
        End Try
    End Function

    Public Function getDisagreementOnContractRemovedEmailBody(id As Integer) As String
        Dim myClient As New WebClient()
        myClient.Encoding = System.Text.Encoding.UTF8
        Try
            Return myClient.DownloadString((localHost & Convert.ToString("/PtsWorker/EmailGenerator/DisagreementOnContractRemoved/")) + id.ToString())
        Catch generatedExceptionName As Exception
            Return "error"
        End Try
    End Function

    Public Function getDisagreementOnAddendumEmailBody(id As Integer) As String
        Dim myClient As New WebClient()
        myClient.Encoding = System.Text.Encoding.UTF8
        Try
            Return myClient.DownloadString((localHost & Convert.ToString("/PtsWorker/EmailGenerator/DisagreementOnAddendum/")) + id.ToString())
        Catch generatedExceptionName As Exception
            Return "error"
        End Try
    End Function

    Public Function getDisagreementOnAddendumRemovedEmailBody(id As Integer) As String
        Dim myClient As New WebClient()
        myClient.Encoding = System.Text.Encoding.UTF8
        Try
            Return myClient.DownloadString((localHost & Convert.ToString("/PtsWorker/EmailGenerator/DisagreementOnAddendumRemoved/")) + id.ToString())
        Catch generatedExceptionName As Exception
            Return "error"
        End Try
    End Function

    Public Function getZeroValueAddendumEmailBody(id As Integer) As String
        Dim myClient As New WebClient()
        myClient.Encoding = System.Text.Encoding.UTF8
        Try
            Return myClient.DownloadString((localHost & Convert.ToString("/PtsWorker/EmailGenerator/ZeroValueAddendum/")) + id.ToString())
        Catch generatedExceptionName As Exception
            Return "error"
        End Try
    End Function

End Class
