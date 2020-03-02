Option Strict On
Option Explicit On



Public Class AuditTrail

    Private Session As UserSession
    Private mMenu As Database.Menu

    Public Property CurrentParentID As Object
    Public Property LastSavedID As Integer
    Public Property AuditTrailSessionID As Integer

    Public Property vID_User As Integer
    Public Property vID_Company As Integer
    Public Property vID_Branch As Integer
    Public Property DocumentID As Integer
    Public Sub New(ByVal pSession As UserSession)
        Me.Session = pSession
        vID_User = Me.Session.GetUserID()
        If Me.Session.GetCompanyID IsNot DBNull.Value Then
            vID_Company = CInt(Me.Session.GetCompanyID())
        End If
        If Me.Session.GetBranchID IsNot DBNull.Value Then
            vID_Branch = CInt(Me.Session.GetBranchID())
        End If
    End Sub

    Public Sub LogSessionStart()
        Me.WriteLog(DBNull.Value, AuditTrailTypeEnum.Session, Me.Session.UserName)
    End Sub

    Public Sub LogSessionEnd()
        Me.WriteLog(AuditTrailSessionID, AuditTrailTypeEnum.Session, "End")
    End Sub

    Public Sub LogInfoSave(ByVal pInfoSetBase As InfoSetBase, ByVal pTransaction As SqlClient.SqlTransaction)
        Dim dv As DataView
        Dim n As String = pInfoSetBase.mInfoMenuSet.tMenu.Rows(0)("Name").ToString

        Dim img As Object = pInfoSetBase.mInfoMenuSet.tMenu.Rows(0)("ImageFile").ToString

        Dim vSaveID As Integer = Me.WriteLog(Me.AuditTrailSessionID, AuditTrailTypeEnum.Save, n, , , , img, pTransaction)
        Dim vTableID As Integer
        Dim vRowID As Integer

        Dim odv As DataView
        Dim odrv As DataRowView


        For Each dt As DataTable In pInfoSetBase.mDataset.Tables
            dv = New DataView(dt)
            odv = New DataView(dt)

            dv.RowStateFilter = _
                   DataViewRowState.Added _
                Or DataViewRowState.ModifiedCurrent _
                Or DataViewRowState.Deleted
            If dv.Count > 0 Then
                vTableID = Me.WriteLog(vSaveID, AuditTrailTypeEnum.Table, dt.TableName, , , , "_menutab.png", pTransaction)
            End If

            dv.RowStateFilter = DataViewRowState.Added
            For Each drv As DataRowView In dv
                If dt.Columns.Contains("Name") Then
                    n = drv("Name").ToString
                Else
                    n = drv("ID").ToString
                End If
                If dt.Columns.Contains("ImageFile") Then
                    img = drv("ImageFile").ToString
                Else
                    img = DBNull.Value
                End If
                vRowID = Me.WriteLog(vTableID, AuditTrailTypeEnum.AddedRecord, n, , , , img, pTransaction)
                For Each dc As DataColumn In dt.Columns
                    If drv(dc.ColumnName).ToString <> "" Then
                        Me.WriteLog(vRowID, AuditTrailTypeEnum.Field, dc.ColumnName, DBNull.Value, drv(dc.ColumnName), , "_menutabfield.png", pTransaction)
                    End If
                Next
            Next


            dv.RowStateFilter = DataViewRowState.ModifiedCurrent
            odv.RowStateFilter = DataViewRowState.ModifiedOriginal
            For Each drv As DataRowView In dv
                If dt.Columns.Contains("Name") Then
                    n = drv("Name").ToString
                Else
                    n = drv("ID").ToString
                End If
                If dt.Columns.Contains("ImageFile") Then
                    img = drv("ImageFile").ToString
                Else
                    img = DBNull.Value
                End If
                vRowID = Me.WriteLog(vTableID, AuditTrailTypeEnum.ModifiedRecord, n, , , , img, pTransaction)

                odv.RowFilter = "ID=" & drv("ID").ToString
                odrv = odv(0)

                For Each dc As DataColumn In dt.Columns
                    If odrv(dc.ColumnName).ToString <> drv(dc.ColumnName).ToString Then
                        Me.WriteLog(vRowID, AuditTrailTypeEnum.Field, dc.ColumnName, odrv(dc.ColumnName), drv(dc.ColumnName), , "_menutabfield.png", pTransaction)
                    End If
                Next
            Next

            dv.RowStateFilter = DataViewRowState.Deleted
            odv.RowStateFilter = DataViewRowState.OriginalRows
            For Each drv As DataRowView In dv
                If dt.Columns.Contains("Name") Then
                    n = drv("Name").ToString
                Else
                    n = drv("ID").ToString
                End If
                If dt.Columns.Contains("ImageFile") Then
                    img = drv("ImageFile").ToString
                Else
                    img = DBNull.Value
                End If
                vRowID = Me.WriteLog(vTableID, AuditTrailTypeEnum.DeletedRecord, n, , , , img, pTransaction)

                odv.RowFilter = "ID=" & drv("ID").ToString
                odrv = odv(0)

                For Each dc As DataColumn In dt.Columns
                    'If odrv(dc.ColumnName).ToString <> drv(dc.ColumnName).ToString Then
                    If drv(dc.ColumnName).ToString <> "" Then
                        Me.WriteLog(vRowID, AuditTrailTypeEnum.Field, dc.ColumnName, odrv(dc.ColumnName), DBNull.Value, , "_menutabfield.png", pTransaction)

                    End If
                    'End If
                Next
            Next



        Next



    End Sub

    Private Function WriteLog(ByVal pParentID As Object, ByVal pID_AuditTrailType As AuditTrailTypeEnum _
                              , ByVal pName As String _
                              , Optional ByVal pOldValue As Object = Nothing _
                              , Optional ByVal pNewValue As Object = Nothing _
                              , Optional ByVal pDetails As Object = Nothing _
                              , Optional ByVal pImageFile As Object = Nothing _
                              , Optional ByVal pTransaction As SqlClient.SqlTransaction = Nothing) As Integer

        Dim s As String

        Dim Hostname As String = System.Net.Dns.GetHostName
        s = "EXEC p_AuditTrail " & GSCOM.SQL.SQLFormat(pParentID)
        s &= ", " & GSCOM.SQL.SQLFormat(CInt(pID_AuditTrailType))
        s &= ", " & GSCOM.SQL.SQLFormat(pName)
        s &= ", " & GSCOM.SQL.SQLFormat(fGetValueName(GSCOM.SQL.SQLFormat(pName), pID_AuditTrailType, GSCOM.SQL.SQLFormat(pOldValue), pTransaction))
        s &= ", " & GSCOM.SQL.SQLFormat(fGetValueName(GSCOM.SQL.SQLFormat(pName), pID_AuditTrailType, GSCOM.SQL.SQLFormat(pNewValue), pTransaction))
        's &= ", " & GSCOM.SQL.SQLFormat(pOldValue)
        's &= ", " & GSCOM.SQL.SQLFormat(pNewValue)
        s &= ", " & GSCOM.SQL.SQLFormat(pDetails)
        's &= ", " & GSCOM.SQL.SQLFormat(pImageFile)
        s &= ", " & GSCOM.SQL.SQLFormat(Hostname)
        s &= ", " & GSCOM.SQL.SQLFormat(Me.Session.GetSessionID)

        If pTransaction IsNot Nothing Then
            LastSavedID = CInt(GSCOM.SQL.ExecuteScalar(s, pTransaction))
        Else
            LastSavedID = CInt(GSCOM.SQL.ExecuteScalar(s, Me.Session.Connection))
        End If

        If pID_AuditTrailType = 1 AndAlso Me.AuditTrailSessionID = 0 Then
            Me.AuditTrailSessionID = LastSavedID
        End If


        Return LastSavedID


    End Function

    Public Sub UpdateDocumentroperties(ByVal pID As Integer, ByVal pMenuID As Integer, ByVal TransType As String)

        Me.DocumentID = pID
        Me.mMenu = CType(pMenuID, Menu)
        Dim s As String = ""
        s = "EXEC pDocumentProperties "

        If TransType = "1" Then
            s &= 1
        ElseIf TransType = "2" Then
            s &= "2"
        Else
            s &= "3"
        End If

        s &= "," & Session.GetMenuValue(mMenu, Database.Tables.tMenu.Field.TableName).ToString
        s &= "," & CInt(mMenu).ToString
        s &= "," & Me.DocumentID.ToString
        s &= "," & Me.vID_Company.ToString
        s &= "," & Me.vID_Branch.ToString
        s &= "," & Me.vID_User.ToString
        Try
            GSCOM.SQL.ExecuteScalar(s, Session.Connection)
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub

    '*******************************************************************'
    ' KEVIN - EDIT OLD NEW (ID) VALUE 2013-05-15
    '*******************************************************************'

    Public Function fGetValueName(ByVal pName As String, ByVal pID_AuditTrailType As AuditTrailTypeEnum, ByVal Value As String, ByVal pTransaction As SqlClient.SqlTransaction) As String
        Dim ValueName As String = Value
        Try
            If pID_AuditTrailType = 6 And pName Like "*ID_*" Then
                pName = Right(Left(pName, pName.Length - 1), pName.Length - 5)
                pName = "v" & pName
                Dim s As String = "SELECT TOP 1 NAME FROM " & pName & " WHERE ID=" & Value
                Dim dta As DataTable

                If pTransaction IsNot Nothing Then
                    'LastSavedID = CInt(GSCOM.SQL.ExecuteScalar(s, pTransaction))
                    dta = GSCOM.SQL.TableQuery(s, pTransaction)
                Else
                    'LastSavedID = CInt(GSCOM.SQL.ExecuteScalar(s, Me.Session.Connection))
                    dta = GSCOM.SQL.TableQuery(s, Me.Session.Connection)
                End If

                'dta = GSCOM.SQL.TableQuery(s, pTransaction)
                If dta.Select.Length > 0 Then
                    ValueName = dta.Rows(0).Item("Name").ToString
                    ValueName = "'" & ValueName & "'"
                End If
            End If
        Catch ex As Exception
            ValueName = ValueName
        End Try
        Return ValueName
    End Function

End Class
