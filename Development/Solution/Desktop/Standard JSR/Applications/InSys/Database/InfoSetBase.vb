Option Explicit On
Option Strict On


Public Class InfoSetBase
    Public Row As DataRow
    Protected mMenuRow As New MenuRow
    Public mInfoMenuSet As MenuSetClass
    '''''''''''''''''''''''''''''''''''''''''''''''''''------------------------
    Protected Connection As SqlClient.SqlConnection
    Public mDataset As DataSet
    Protected mMenuID As Integer
    Protected mInitID As Integer
    ''' '''''''''''''''''''''''
    Private mTable As GSCOM.SQL.ZDataTable
    Protected Overridable Property Table() As GSCOM.SQL.ZDataTable
        Get
            Return mTable
        End Get
        Set(ByVal value As GSCOM.SQL.ZDataTable)
            mTable = value
        End Set
    End Property
    Private FilterByID As Boolean = True
    Protected ClearThenFillOnLoadInfo As Boolean = True
    Protected UseTransaction As Boolean = True
    Public mSession As UserSession
    Public Sub New(ByVal pSession As UserSession, ByVal pID As Integer)
        mDataset = New DataSet
        mDataset.EnforceConstraints = False 'ROBBIE: 20060919 --DO NOT ENFORCE CONSTRAINTS--ENDEDIT WHEN VALIDATED-----------------\
        Connection = pSession.Connection
        mInitID = pID
        mSession = pSession
    End Sub

    Protected Sub SetHeaderDefaultValues()
        If Me.Table.Columns.Contains("ID_Company") Then
            Me.Table.Columns("ID_Company").DefaultValue = mSession.GetCompanyID()
        End If
        If Me.Table.Columns.Contains("ID_User") Then
            Me.Table.Columns("ID_User").DefaultValue = mSession.GetUserID()
        End If
        If Me.Table.Columns.Contains("ID_CreatedBy") Then
            Me.Table.Columns("ID_CreatedBy").DefaultValue = mSession.Session.Get(Database.Tables.tSession.Field.ID_Employee)
        End If

        If Me.Table.Columns.Contains("ID_ApprovedBy") Then
            Me.Table.Columns("ID_ApprovedBy").DefaultValue = mSession.Session.Get(Database.Tables.tSession.Field.ID_Employee)
        End If
        If Me.Table.Columns.Contains("ID_FilingStatus") Then
            Me.Table.Columns("ID_FilingStatus").DefaultValue = 1
        End If
        Dim dt As DateTime = mSession.GetServerDate()
        Dim d As Date = dt.Date
        If Me.Table.Columns.Contains("DateCreated") Then
            Me.Table.Columns("DateCreated").DefaultValue = d
        End If
        If Me.Table.Columns.Contains("Date") Then
            Me.Table.Columns("Date").DefaultValue = d
        End If
        If Me.Table.Columns.Contains("Time") Then
            Me.Table.Columns("Time").DefaultValue = dt
        End If

        If Me.Table.Columns.Contains("TempTime") Then
            Me.Table.Columns("TempTime").DefaultValue = dt
        End If
        'ADDED:20100910
        If Me.Table.Columns.Contains("DateTime") Then
            Me.Table.Columns("DateTime").DefaultValue = dt
        End If
        'ADDED:20100924
        'If Me.Table.Columns.Contains("ID_Employee") Then
        '    Me.Table.Columns("ID_Employee").DefaultValue = mSession.GetEmployeeID()
        'End If
        If Me.Table.Columns.Contains("ID_Branch") Then
            Me.Table.Columns("ID_Branch").DefaultValue = mSession.GetBranchID()
        End If
    End Sub


    Private Sub SetRelations()
        'Dim s As String
        Dim dt As GSCOM.SQL.ZDataTable
        Dim pdt As DataTable
        Dim pdc As DataColumn
        Dim cdc As DataColumn
        Dim rel As DataRelation
        For Each dr As DataRow In mInfoMenuSet.tMenuDetailTab.Select("TableName <> 'xxx'", "SeqNo,ID")
            Dim mdtr As New MenuDetailTabRow(dr)
            Select Case mdtr.ID_MenuDetailTabType
                Case MenuDetailTabTypeEnum.Grid, MenuDetailTabTypeEnum.TreeView, MenuDetailTabTypeEnum.Form

                    dt = New GSCOM.SQL.ZDataTable(Connection, mdtr.TableName)

                    'If mDataset.Tables(mdtr.TableName) Is Nothing Then
                    mDataset.Tables.Add(dt)
                    If String.IsNullOrEmpty(mdtr.ParentTableName) Or mDataset.Tables(mdtr.ParentTableName) Is Nothing Then
                        pdt = Me.Table
                    Else
                        pdt = Me.mDataset.Tables(mdtr.ParentTableName)
                    End If
                    pdc = pdt.Columns(mdtr.ParentColumn)
                    cdc = dt.Columns(mdtr.ChildColumn)
                    rel = mDataset.Relations.Add(pdc, cdc)
                    rel.RelationName = cdc.Table.TableName
                    SetDetailDefaultValuesFromMenuDetailTabField(mdtr, dt)
                    SetDetailRequiredFieldsFromMenuDetailTabField(mdtr, dt)
            End Select
            'End If
        Next
    End Sub

    Private ReadOnly Property MenuFilter() As String
        Get
            Return "ID_Menu=" & mMenuID.ToString
        End Get
    End Property

    'Public Function SpecificMenu(ByVal pMenu As Integer) As MenuSetClass

    '    Dim dt As men


    '    Return dt

    'End Function
    Public ReadOnly Property MenuID As Integer
        Get
            Return mMenuID
        End Get
    End Property

    Protected Overridable Sub InitControl(ByVal pMenu As Integer)
        SetHeaderDefaultValues()
        Dim s As String
        s = "ID=" & pMenu.ToString
        'Debug.WriteLine("Start: " & Now.TimeOfDay.ToString)
        'mMenuSet = New MenuSet
        mMenuID = pMenu
        'mInfoMenuSet.Menu = MenuTable.Clone
        mInfoMenuSet = New MenuSetClass
        mInfoMenuSet.tMenu = GSCOM.SQL.SelectIntoDataTable(s, mSession.MenuSet.tMenu) ' .ImportRow(MenuTable.Select(s)(0))
        mMenuRow.InnerRow = mInfoMenuSet.tMenu.Rows(0)
        mInfoMenuSet.tMenuTab = GSCOM.SQL.SelectIntoDataTable(MenuFilter, mSession.MenuSet.tMenuTab) 'GSCOM.SQL.TableQuery(s, Connection)
        mInfoMenuSet.tMenuDetailTab = GSCOM.SQL.SelectIntoDataTable(MenuFilter, mSession.MenuSet.tMenuDetailTab) 'GSCOM.SQL.TableQuery(s, Connection)
        ''''''''''''''''''
        s = "MenuTabMenuID=" & pMenu.ToString
        mInfoMenuSet.tMenuTabField = GSCOM.SQL.SelectIntoDataTable(s, mSession.MenuSet.tMenuTabField)
        ''''''''''''''''''
        s = "MenuDetailTabMenuID=" & pMenu.ToString
        mInfoMenuSet.tMenuDetailTabField = GSCOM.SQL.SelectIntoDataTable(s, mSession.MenuSet.tMenuDetailTabField)
        mInfoMenuSet.tMenuDetailTabProperty = GSCOM.SQL.SelectIntoDataTable(s, mSession.MenuSet.tMenuDetailTabProperty)
        mInfoMenuSet.tMenuButton = GSCOM.SQL.SelectIntoDataTable(MenuFilter, mSession.MenuSet.tMenuButton)
        ''''''''''''''''''''''''''''''
        ''''''''''''''''''''''''''''''
        'mtMenu.TableName = "tMenu"
        'mtMenuTab.TableName = "tMenuTab"
        'mtMenuButton.TableName = "tMenuButton"
        'mtMenuDetailTab.TableName = "tMenuDetailTab"
        'mtMenuTabField.TableName = "tMenuTabField"
        'mtMenuDetailTabField.TableName = "tMenuDetailTabField"
        'mtMenuDetailTabGroup.TableName = "tMenuDetailTabGroup"
        'mtMenuDetailTabProperty.TableName = "tMenuDetailTabProperty"

        ' Debug.WriteLine("End: " & Now.TimeOfDay.ToString)

        'mInfoMenuSet = New DataSet
        'With mInfoMenuSet
        '    .Tables.Add(mtMenu)
        '    .Tables.Add(mtMenuTab)
        '    .Tables.Add(mtMenuTabField)
        '    .Tables.Add(mtMenuDetailTab)
        '    .Tables.Add(mtMenuDetailTabField)
        '    .Tables.Add(mtMenuDetailTabProperty)
        'End With
        mInfoMenuSet.AddTables()
        SetHeaderDefaultValuesFromMenuTabField()
        SetHeaderRequiredFieldsFromMenuTabField()
        'SetHeaderRequiredFieldsFromMenuTabField2()
        SetRelations()
        InitExpressions()
        InitControl2(pMenu)
    End Sub

    'Private Sub SetHeaderRequiredFieldsFromMenuTabField2()
    '    Dim mtf As New MenuTabFieldRow
    '    Dim ri As String
    '    For Each dr As DataRow In Me.mInfoMenuSet.tMenuTabField.Select()
    '        ri = dr("RequiredIf").ToString
    '        If ri <> "" Then
    '            ri = Me.PassParameters(ri)
    '            ri = Me.Table.Select(ri).Length.ToString

    '            If CBool(ri) Then
    '                mtf.InnerRow = dr
    '                Me.Table.Columns(mtf.Name).AllowDBNull = False
    '            End If

    '        End If
    '    Next
    'End Sub

    Private Sub SetHeaderDefaultValuesFromMenuTabField()
        Dim mtf As New MenuTabFieldRow
        For Each dr As DataRow In Me.mInfoMenuSet.tMenuTabField.Select("DefaultValue IS NOT NULL")
            mtf.InnerRow = dr
            Me.Table.Columns(mtf.Name).DefaultValue = PassParameters(mtf.DefaultValue).Trim({"'"c})
        Next
    End Sub

    Private Sub SetHeaderRequiredFieldsFromMenuTabField()
        Dim mtf As New MenuTabFieldRow
        For Each dr As DataRow In Me.mInfoMenuSet.tMenuTabField.Select("IsRequired=1")
            mtf.InnerRow = dr
            Me.Table.Columns(mtf.Name).AllowDBNull = False
        Next
    End Sub

    Private Sub SetDetailDefaultValuesFromMenuDetailTabField(ByVal mdt As MenuDetailTabRow, ByVal dt As DataTable)
        Dim mdtf As New MenuDetailTabFieldRow
        For Each dr As DataRow In Me.mInfoMenuSet.tMenuDetailTabField.Select("DefaultValue IS NOT NULL AND ID_MenuDetailTab=" & mdt.ID)
            mdtf.InnerRow = dr
            dt.Columns(mdtf.Name).DefaultValue = PassParameters(mdtf.DefaultValue.ToString()).Trim({"'"c}) ' mdtf.DefaultValue
        Next
    End Sub

    Private Sub SetDetailRequiredFieldsFromMenuDetailTabField(ByVal mdt As MenuDetailTabRow, ByVal dt As DataTable)
        Dim mdtf As New MenuDetailTabFieldRow
        For Each dr As DataRow In Me.mInfoMenuSet.tMenuDetailTabField.Select("IsRequired=1 AND ID_MenuDetailTab=" & mdt.ID)
            mdtf.InnerRow = dr
            dt.Columns(mdtf.Name).AllowDBNull = False
        Next
    End Sub

#Region "InitRow"
    Private Sub InitRow(ByVal pID As Integer)
        Table.Clear()
        If pID = 0 Then
            Me.Row = Table.AddRow() 'ROBBIE NOTE: this would fail if constraints are not met. use newrow() addrow() combination instead
            With Table.Columns("ID")
                .ReadOnly = False
                Me.Row("ID") = 0
                'Me.Row.AcceptChanges()
                'Me.Row.SetAdded()
                .ReadOnly = True
            End With
        Else
            If FilterByID Then
                Table.SetFilter("ID=" & pID.ToString)
            Else
                Table.SetFilter("")
            End If
            Table.Fill()
            Me.Row = Table.Rows(0)
            'Me.Row = Table.Select("ID=" & pID)(0)
        End If
    End Sub

#End Region

#Region "LoadInfo"
    Public Overridable Sub LoadInfo(ByVal Id As Integer)
        Dim s As String
        InitRow(Id)
        SetDefaultValues()
        If ClearThenFillOnLoadInfo Then
            Dim dt As DataTable = Nothing
            For Each dr As DataRelation In Me.mDataset.Relations
                If dr.ParentTable.Rows.Count = 0 Then
                    s = "1=0"
                ElseIf dr.ParentTable.Rows.Count = 1 Then
                    s = dr.ChildColumns(0).ColumnName & "=" & GSCOM.SQL.SQLFormat(dr.ParentTable.Rows(0)(dr.ParentColumns(0).ColumnName))
                Else
                    s = dr.ChildColumns(0).ColumnName & " IN ("
                    For Each dr2 As DataRow In dr.ParentTable.Rows
                        s &= GSCOM.SQL.SQLFormat(dr2(dr.ParentColumns(0).ColumnName))
                        s &= ","
                    Next
                    s = Strings.Left(s, s.Length - 1)
                    s &= ")"
                End If
                Dim dra() As DataRow
                dra = Me.mInfoMenuSet.tMenuDetailTab.Select("TableName=" & GSCOM.SQL.SQLFormat(dr.ChildTable.TableName))
                If dra.Length > 0 Then
                    Dim mdt As New Database.MenuDetailTabRow(dra(0))
                    If mdt.DetailTabFilter IsNot DBNull.Value Then
                        s &= " AND " & Me.PassParameters(mdt.DetailTabFilter.ToString).ToString
                    End If
                End If
                CType(dr.ChildTable, GSCOM.SQL.ZDataTable).ClearThenFill(s)
            Next
        End If


        For Each dt As DataTable In Me.mDataset.Tables

            For Each dr As DataRow In dt.Select("", "", DataViewRowState.Unchanged)
                For Each dc As DataColumn In dt.Columns
                    If dc.ColumnName.StartsWith("ORIGX_") Then
                        dr(dc.ColumnName) = dr(dc.ColumnName.Replace("ORIGX_", ""))
                    End If
                Next
                If dr.RowState = DataRowState.Modified Then
                    dr.AcceptChanges()
                End If
            Next
        Next
    End Sub
#End Region

#Region "SetDefaultValues"
    Protected Overridable Sub SetDefaultValues()
        For Each dr As DataRelation In Me.mDataset.Relations

            For Each dr2 As DataRow In dr.ParentTable.Select()
                ''s = dr.ChildColumns(0).ColumnName & "=" & GSCOM.SQL.SQLFormat(dr.ParentTable.Rows(0)(dr.ParentColumns(0).ColumnName))
                's &= GSCOM.SQL.SQLFormat(dr2(dr.ParentColumns(0).ColumnName))
                's &= ","
                dr.ChildTable.Columns(dr.ChildColumns(0).ColumnName).DefaultValue = dr2.Item(dr.ParentColumns(0).ColumnName)
            Next

        Next

        If Not IsDBNull(mSession.GetEmployeeID()) Then 'execute only if employee mode
            For Each t As DataTable In mDataset.Tables
                If t IsNot Me.Table Then
                    'If t.Columns.Contains("ID_Employee") Then
                    '    With t.Columns("ID_Employee")
                    '        If Not .ReadOnly Then
                    '            .DefaultValue = mSession.GetEmployeeID()
                    '        End If
                    '    End With
                    'End If

                End If
            Next
        End If

        SetDocumentNo()
        SetCodeNo()
    End Sub

#End Region

#Region " Set Document No "
    Protected Sub SetDocumentNo(Optional ByVal vTransaction As SqlClient.SqlTransaction = Nothing)
        'Dim WarehouseCode As String = GSCOM.SQL.SQLFormat(Me.mSession.Session.Get("WarehouseCode").ToString)
        If Me.Row.RowState = DataRowState.Added _
            AndAlso Table.Columns.Contains("DocumentNo") _
            AndAlso Table.Columns.Contains("ID_Company") _
            AndAlso Table.Columns.Contains("SeqNo") Then
            Dim a, s As String
            Dim dra As DataRow()
            dra = mSession.DocumentSeriesTable.Select("ID_Menu=" & mMenuID.ToString)
            If dra.Length > 0 Then
                If Me.Row.Item("ID_Company") Is DBNull.Value Then Exit Sub
                Dim i As Integer
                Dim o As Object
                s = dra(0).Item("Prefix").ToString
                'If WarehouseCode IsNot Nothing Then
                '    s &= Mid(WarehouseCode, 2, WarehouseCode.Length - 2) & "-"
                'End If
                a = "SELECT MAX(SeqNo) FROM " & dra(0).Item("TableName").ToString & " WHERE ID_Company=" & Me.Row.Item("ID_Company").ToString
                If vTransaction IsNot Nothing Then
                    o = GSCOM.SQL.ExecuteScalar(a, vTransaction)
                Else
                    o = GSCOM.SQL.ExecuteScalar(a, Me.Connection)
                End If
                If Not IsDBNull(o) Then
                    i = CInt(o)
                End If
                i += 1
                Me.Row.Item("SeqNo") = i
                If CBool(dra(0).Item("AddDate")) AndAlso Table.Columns.Contains("Date") AndAlso (Not IsDBNull(Me.Row("Date"))) Then
                    s &= Format(CDate(Me.Row("Date")), "yyyyMM") & "-"
                End If
                a = Strings.StrDup(CInt(dra(0)("DigitCount")) - Strings.Len(i.ToString), "0"c)
                s &= a & i.ToString
                Me.Row("DocumentNo") = s
            End If
        End If
    End Sub
#End Region

#Region " Set Code No "
    Protected Sub SetCodeNo(Optional ByVal vTransaction As SqlClient.SqlTransaction = Nothing)
        If Me.Row.RowState = DataRowState.Added _
            AndAlso Table.Columns.Contains("Code") _
            AndAlso Table.Columns.Contains("ID_Company") _
            AndAlso Table.Columns.Contains("SeqNo") Then
            Dim a, s As String
            Dim dra As DataRow()
            dra = mSession.DocumentSeriesTable.Select("ID_Menu=" & mMenuID.ToString)
            If dra.Length > 0 Then
                If Me.Row.Item("ID_Company") Is DBNull.Value Then Exit Sub
                Dim i As Integer
                Dim o As Object
                s = dra(0).Item("Prefix").ToString
                a = "SELECT MAX(SeqNo) FROM " & dra(0).Item("TableName").ToString & " WHERE ID_Company=" & Me.Row.Item("ID_Company").ToString
                If vTransaction IsNot Nothing Then
                    o = GSCOM.SQL.ExecuteScalar(a, vTransaction)
                Else
                    o = GSCOM.SQL.ExecuteScalar(a, Me.Connection)
                End If
                If Not IsDBNull(o) Then
                    i = CInt(o)
                End If
                i += 1
                Me.Row.Item("SeqNo") = i
                If CBool(dra(0).Item("AddDate")) AndAlso Table.Columns.Contains("Date") AndAlso (Not IsDBNull(Me.Row("Date"))) Then
                    s &= Format(CDate(Me.Row("Date")), "yyyyMM")
                End If
                a = Strings.StrDup(CInt(dra(0)("DigitCount")) - Strings.Len(i.ToString), "0"c)
                s &= a & i.ToString
                Me.Row("Code") = s
            End If
        End If
    End Sub
#End Region

#Region "ReloadAfterSave"
    Protected mReloadAfterCommit As Boolean '= True
    Protected Property ReloadAfterCommit() As Boolean
        Get
            Return mReloadAfterCommit
        End Get
        Set(ByVal value As Boolean)
            mReloadAfterCommit = value
        End Set
    End Property

#End Region

#Region "ValidData"
    Protected Overridable Function ValidData() As Boolean
        ''20091017
        'Dim s As String
        'Dim dv As New DataView
        'For Each dc As DataColumn In Table.Columns
        '    If (Not dc.AllowDBNull) Then
        '        dv = New DataView(mInfoMenuSet.tMenuTabField)
        '        dv.RowFilter = "Name=" & GSCOM.SQL.SQLFormat(dc.ColumnName)
        '        If dv.Count > 0 Then
        '            s = dv(0)("EffectiveLabel").ToString
        '        Else
        '            s = dc.Caption
        '        End If
        '        If (Me.Row.Item(dc) Is DBNull.Value) OrElse (Me.Row.Item(dc).ToString = "") Then
        '            Throw New Exception("Please specify " & s)
        '        Else
        '            Select Case dc.DataType.ToString.ToUpper
        '                Case "SYSTEM.INT32", "SYSTEM.DECIMAL"
        '                    If ((Strings.Left(dc.ColumnName, 3) = "ID_") And (CInt(Me.Row.Item(dc)) = 0)) Then
        '                        Throw New Exception("Please select " & s)
        '                    End If
        '            End Select
        '        End If
        '    End If
        'Next
        ''test if the constraints are satisfied
        ''this would raise an exception if constraints are violated
        For Each dt As DataTable In Me.mDataset.Tables
            ValidTable(dt)
        Next
        Try
            If UseTransaction Then
                mDataset.EnforceConstraints = True
                mDataset.EnforceConstraints = False
            End If
        Catch ex As Exception
            'MsgBox(ex.Message)
            Throw New Exception("Please fill up all the required fields")
        End Try
    End Function

    Protected Overridable Function ValidTable(ByVal dt As DataTable) As Boolean

        For Each dr As DataRow In dt.Select
            For Each dc As DataColumn In dt.Columns
                If (Not dc.AllowDBNull) Then
                    If (dr.Item(dc) Is DBNull.Value) OrElse (dr.Item(dc).ToString = "") Then
                        Throw New Exception("Please specify " & dc.Caption.Replace(":", ""))
                    Else
                        Select Case dc.DataType.ToString.ToUpper
                            Case "SYSTEM.INT32", "SYSTEM.DECIMAL"
                                If CInt(dr.Item(dc)) = 0 Then
                                    If ((Strings.Left(dc.ColumnName, 3) = "ID_") AndAlso dt.ParentRelations.Count <> 0) Then
                                        If dr.GetParentRow(dt.TableName) IsNot Nothing AndAlso dr.GetParentRow(dt.TableName).RowState <> DataRowState.Added Then
                                            Throw New Exception("Please select " & dc.Caption)
                                        End If
                                    End If
                                End If
                        End Select
                    End If
                End If
            Next
        Next
    End Function

#End Region

    Protected Sub InitControl2(ByVal pMenu As Integer)
        Try
            Dim dt As GSCOM.SQL.ZDataTable
            For Each dr As DataRow In mInfoMenuSet.tMenuDetailTab.Select
                Dim mdtr As New Database.MenuDetailTabRow(dr)
                dt = CType(mDataset.Tables(mdtr.TableName), SQL.ZDataTable) ' New GSCOM.SQL.ZDataTable(gConnection, s)
                If mdtr.ID_MenuDetailTabType = MenuDetailTabTypeEnum.Grid OrElse mdtr.ID_MenuDetailTabType = MenuDetailTabTypeEnum.Form Then
                    InitDetail(dt, New Database.MenuDetailTabRow(mdtr.InnerRow))
                Else
                    'InitTreeGrid(dt, mdt.InnerRow)
                End If
            Next
        Catch ex As Exception
            MsgBox(pMenu, , "InitControl2")
        End Try
    End Sub
    Public Sub InitDetail(ByVal dt As DataTable, Optional ByVal pMenuDetailTabRow As Database.MenuDetailTabRow = Nothing)
        Dim pMenuDetailTabID As Integer = 0
        If pMenuDetailTabRow IsNot Nothing Then
            pMenuDetailTabID = pMenuDetailTabRow.ID
            Dim s As String
            s = pMenuDetailTabRow.Sort
            If s <> "" Then
                dt.DefaultView.Sort = s
            End If
        End If
        If pMenuDetailTabRow IsNot Nothing Then
            Dim dc As DataColumn
            Dim mdtfr As New MenuDetailTabFieldRow
            For Each drx As DataRow In mInfoMenuSet.tMenuDetailTabField.Select("ID_MenuDetailTab=" & pMenuDetailTabRow.ID, "SeqNo,ID")
                mdtfr.InnerRow = drx
                If Not dt.Columns.Contains(mdtfr.Name) Then
                    MsgBox(mdtfr.Name & " does not exist in " & dt.TableName, MsgBoxStyle.Exclamation)
                Else
                    dc = dt.Columns(mdtfr.Name)
                    dc.Caption = mdtfr.EffectiveLabel
                    'InitColumn(dc, dgv, drx("ID_Menu"))
                End If
            Next
        Else
            'Dim a As DataTable
            'If pDataSource <> "" Then
            '    a = GSCOM.SQL.TableQuery("SELECT TOP 0 * FROM (" & pDataSource & ") a", gConnection)
            'Else
            '    a = GSCOM.SQL.TableQuery("SELECT TOP 0 * FROM v" & Strings.Right(dt.TableName, dt.TableName.Length - 1) & "_List", gConnection)
            'End If
            'For Each dc As DataColumn In a.Columns
            '    InitColumn(dc, dgv, DBNull.Value)
            'Next
        End If
        'AddHandler dgv.DataError, AddressOf dgv_DataError
    End Sub

    Public ReadOnly Property HasUnsavedChanges As Boolean
        Get
            Dim vChanged As Boolean
            For Each dt As DataTable In Me.mDataset.Tables
                If dt.Select("", "", DataViewRowState.Added Or DataViewRowState.Deleted Or DataViewRowState.ModifiedCurrent Or DataViewRowState.ModifiedOriginal).Length > 0 Then
                    vChanged = True
                    Exit For
                End If
            Next
            Return vChanged
        End Get
    End Property

    Public ReadOnly Property RowID As Integer
        Get
            Return CInt(Row("ID"))
        End Get
    End Property

    Private Sub InitExpressions()
        Dim s As String = ""
        s = "Expression IS NOT NULL"
        Dim a As New MenuTabFieldRow
        Dim c As DataColumn
        Dim tn As String = ""
        For Each dr As DataRow In Me.mInfoMenuSet.tMenuTabField.Select(s)
            a.InnerRow = dr
            If tn <> Me.Table.TableName Then
                AddHandler Me.Table.ColumnChanged, AddressOf ColumnChanged
                tn = Me.Table.TableName
            End If
            Dim n As String = dr(Database.Tables.tMenuTabField.Field.Name).ToString
            c = Me.Table.Columns(n)
            c.Table.Columns.Add("EXPRX_" & c.ColumnName, c.Table.Columns(c.ColumnName).DataType, dr("Expression").ToString)
            c.Table.Columns.Add("ORIGX_" & c.ColumnName, c.Table.Columns(c.ColumnName).DataType)
            c.Expression = dr("Expression").ToString
        Next
        Dim b As New MenuDetailTabFieldRow
        For Each dr As DataRow In Me.mInfoMenuSet.tMenuDetailTabField.Select(s)
            b.InnerRow = dr
            Dim vmdt As New MenuDetailTabRow(Me.mInfoMenuSet.tMenuDetailTab.Select("ID=" & b.ID_MenuDetailTab.ToString)(0))
            Dim n As String = dr(Database.Tables.tMenuDetailTabField.Field.Name).ToString
            c = mDataset.Tables(vmdt.TableName.ToString).Columns(n)
            c.Table.Columns.Add("EXPRX_" & c.ColumnName, c.Table.Columns(c.ColumnName).DataType, dr("Expression").ToString)
            c.Table.Columns.Add("ORIGX_" & c.ColumnName, c.Table.Columns(c.ColumnName).DataType)
            c.Expression = dr("Expression").ToString
            If tn <> c.Table.TableName Then
                AddHandler Me.mDataset.Tables(vmdt.TableName).ColumnChanged, AddressOf ColumnChanged
                tn = c.Table.TableName
            End If
        Next

        For Each dt As DataTable In mDataset.Tables
            CType(dt, GSCOM.SQL.ZDataTable).RebuildSelectCommand()
        Next
    End Sub

    Private Sub ColumnChanged(ByVal sender As Object, ByVal e As System.Data.DataColumnChangeEventArgs)
        Static vBusy As Boolean
        If Not vBusy Then
            vBusy = True
            If e.Column.DataType IsNot GetType(Decimal) AndAlso e.Column.DataType IsNot GetType(Int32) Then
                vBusy = False
                Exit Sub
            End If
            If e.Row.RowState = DataRowState.Detached Then
                CType(sender, DataTable).Rows.Add(e.Row)
            End If
            e.Row.EndEdit()
            vBusy = False
            vBusy = False
        End If

    End Sub

    Public Function PassParameters(ByVal s As String, Optional ByVal dtx As DataTable = Nothing, Optional ByVal drx As DataRow = Nothing, Optional ByVal drxp As DataRow = Nothing) As String
        Dim dt As New ParameterDataTable
        Dim dr As DataRow
        For Each dc As DataColumn In Me.Table.Columns
            If Me.Row Is Nothing Then Exit For
            Try
                dr = dt.NewRow
                dr("Parameter") = "$" & dc.ColumnName
                dr("Value") = GSCOM.SQL.SQLFormat(Me.Row(dc))
                If dr.RowState = DataRowState.Detached Then
                    dt.Rows.Add(dr)
                End If
            Catch ex As System.Data.RowNotInTableException
            End Try
        Next
        If dtx IsNot Nothing Then
            If drxp IsNot Nothing Then
                For Each dc As DataColumn In drxp.Table.Columns
                    dr = dt.NewRow

                    dr("Parameter") = "~" & dc.ColumnName
                    If drxp Is Nothing Then
                        dr("Value") = GSCOM.SQL.SQLFormat(DBNull.Value)
                    Else
                        dr("Value") = GSCOM.SQL.SQLFormat(drxp(dc))
                    End If
                    dt.Rows.Add(dr)
                Next
            End If
            For Each dc As DataColumn In dtx.Columns
                dr = dt.NewRow
                dr("Parameter") = "#" & dc.ColumnName
                If drx Is Nothing Then
                    dr("Value") = GSCOM.SQL.SQLFormat(DBNull.Value)
                Else
                    dr("Value") = GSCOM.SQL.SQLFormat(drx(dc))
                End If
                dt.Rows.Add(dr)
            Next
        End If
        s = gPassParameters(mSession.gParameterTable, s)
        s = gPassParameters(dt, s)
        '20101109
        s = s.Replace("@ID_Menu", GSCOM.SQL.SQLFormat(mMenuID))
        If Not Me.Row Is Nothing Then s = s.Replace("@ID", GSCOM.SQL.SQLFormat(Me.Row("ID")))
        Return s
    End Function


    Public Function PassParametersHTML(ByVal s As String) As String
        Dim dt As New ParameterDataTable
        Dim dr As DataRow
        For Each dc As DataColumn In Me.Table.Columns
            dr = dt.NewRow
            dr("Parameter") = "$" & dc.ColumnName
            dr("Value") = "<b>" & Me.Row(dc).ToString & "<b/>"
            dt.Rows.Add(dr)
        Next
        s = gPassParameters(mSession.gParameterTable, s)
        s = gPassParameters(dt, s)
        '20101109
        s = s.Replace("@ID_Menu", GSCOM.SQL.SQLFormat(mMenuID))
        s = s.Replace("@ID", GSCOM.SQL.SQLFormat(Me.Row("ID")))

        Return s
    End Function

    Protected Sub CreateCopy()
        If Me.HasUnsavedChanges Then
            MsgBox("Can't copy an unsaved record", MsgBoxStyle.Exclamation)
        Else
            For Each dt As DataTable In Me.mDataset.Tables
                For Each drv As DataRowView In dt.DefaultView
                    drv.Row.SetAdded()
                Next
            Next
            Me.Table.Columns("ID").ReadOnly = False
            Me.Row("ID") = 0
            Me.Table.Columns("ID").ReadOnly = True
        End If
    End Sub

End Class
