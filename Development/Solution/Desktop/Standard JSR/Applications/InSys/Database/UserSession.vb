Public Class UserSession

#Region "Declarations"

    Public MenuSet As MenuSetClass
    Public LookUpSet As New DataSet
    Public Connection As SqlClient.SqlConnection
    Public Session As Tables.tSession
    Public SettingTable As DataTable
    Public SystemMessageTable As DataTable
    Public SystemDataLookUpTable As DataTable
    Public SystemQueryParameterTable As DataTable
    Public DocumentSeriesTable As DataTable
    Public UserFavMenuTable As GSCOM.SQL.ZDataTable
    Public AuditTrail As AuditTrail
    Public nGlobal As NetGlobal

#End Region

    Public ReadOnly Property MenuTable() As DataTable
        Get
            Return MenuSet.tMenu
        End Get
    End Property

    Public Sub New(con As SqlClient.SqlConnection)
        Connection = con
        Me.nGlobal = New NetGlobal(Me, Connection)
    End Sub

    Private Sub InitSystemTables()
        'important to order by name because the system will loop later for string search. 
        Dim s As String
        s = "SELECT * FROM tSystemMessage ORDER BY [Name] DESC" 'must be descending. sample: IX_tEmployeeDailyScheduleView, IX_tEmployeeDailyScheduleView_1. the former will always be the one returned if ascending
        SystemMessageTable = GSCOM.SQL.TableQuery(s, Connection)
        s = "SELECT * FROM tSystemQueryParameter ORDER BY [SeqNo],[ID]" 'must be descending. sample: IX_tEmployeeDailyScheduleView, IX_tEmployeeDailyScheduleView_1. the former will always be the one returned if ascending
        SystemQueryParameterTable = GSCOM.SQL.TableQuery(s, Connection)
        s = "SELECT * FROM tSystemDataLookUp WHERE IsActive=1"
        SystemDataLookUpTable = GSCOM.SQL.TableQuery(s, Connection)
        s = "SELECT * FROM vDocumentSeries WHERE IsActive=1"
        DocumentSeriesTable = GSCOM.SQL.TableQuery(s, Connection)
        s = "SELECT Name,Value FROM tSetting WHERE (Active=1)"
        SettingTable = GSCOM.SQL.TableQuery(s, Connection)
        's = "SELECT * FROM vUserFavMenu WHERE ID_User=" & GetUserID.ToString
        'UserFavMenuTable =  GSCOM.SQL.TableQuery(s, Connection)
        UserFavMenuTable = New GSCOM.SQL.ZDataTable(Connection, "tUserFavMenu") ' GSCOM.SQL.TableQuery(s, Connection)
        UserFavMenuTable.ClearThenFill("ID_User=" & GetUserID.ToString)
        UserFavMenuTable.DefaultView.Sort = "SeqNo,ID"
    End Sub

#Region "StartSession"
    Public Sub StartSession(ByVal pID_User As Integer, ByVal pID_Company As Object, ByVal pID_Employee As Object)
        Session = New Tables.tSession(Connection)
        'ROBBIE: 20060919 --DO NOT ENFORCE CONSTRAINTS--ENDEDIT WHEN VALIDATED-----------------\
        Dim ds As New DataSet
        ds.EnforceConstraints = False
        ds.Tables.Add(Session)
        'ROBBIE: 20060919 --DO NOT ENFORCE CONSTRAINTS--ENDEDIT WHEN VALIDATED-----------------/
        Dim dr As DataRow
        dr = Session.AddRow()
        With Session
            .Set(Tables.tSession.Field.ID_User, pID_User)
            .Set(Tables.tSession.Field.StartDateTime, GetServerDate)
            .Set(Tables.tSession.Field.ID_Company, pID_Company)
            .Set(Tables.tSession.Field.ID_Employee, pID_Employee)
            .Update()
        End With
        gParameterTable = New ParameterDataTable
        For Each dc As DataColumn In Me.Session.Columns
            If dc.ColumnName = "ID" Then
                gParameterTable.Rows.Add("@ID_Session", GSCOM.SQL.SQLFormat(Session.Get(dc.ColumnName)))
            Else
                'If dc.ColumnName.StartsWith("ID_") Then
                gParameterTable.Rows.Add("@" & dc.ColumnName, GSCOM.SQL.SQLFormat(Session.Get(dc.ColumnName)))
                'End If
            End If
        Next

        LoadTables()

        Me.AuditTrail = New Database.AuditTrail(Me)
        Me.AuditTrail.LogSessionStart()
    End Sub

    Public Sub LoadTables()
        InitSystemTables()
        InitMenu()
    End Sub

    'Public Sub DarkColor(ByRef r As Integer, ByRef g As Integer, ByRef b As Integer) ' As String
    '    r = CInt(r / 1.75)
    '    g = CInt(g / 1.75)
    '    b = CInt(b / 1.75)
    '    'Dim rs, gs, bs As String
    '    'rs = Hex(r)
    '    'gs = Hex(g)
    '    'bs = Hex(b)
    '    'Return rs & gs & bs 'rs.PadLeft(2 - rs.Length, "0"c) & gs.PadLeft(2 - gs.Length, "0"c) & bs.PadLeft(2 - bs.Length, "0"c)
    '    'ret()
    'End Sub

    Public Function DarkColor(ByVal rgb As String) As String
        Dim r, g, b As Byte
        r = Convert.ToByte(Left(rgb, 2), 16)
        g = Convert.ToByte(Mid(rgb, 3, 2), 16)
        b = Convert.ToByte(Right(rgb, 2), 16)
        r = CByte(r / 1.75)
        g = CByte(g / 1.75)
        b = CByte(b / 1.75)
        Dim rs, gs, bs As String
        rs = Hex(r)
        gs = Hex(g)
        bs = Hex(b)
        Return rs & gs & bs 'rs.PadLeft(2 - rs.Length, "0"c) & gs.PadLeft(2 - gs.Length, "0"c) & bs.PadLeft(2 - bs.Length, "0"c)
    End Function

    Private Sub InitMenu()
        Dim s As String
        MenuSet = New MenuSetClass(Me, GetSessionID)
        Dim mr As New MenuRow()
        'Dim c As Color
        For Each dr As DataRow In MenuTable.Rows
            mr.InnerRow = dr
            s = mr.DataSource
            s = gPassParameters(gParameterTable, s)
            mr.DataSource = s

            s = mr.ListSource
            s = gPassParameters(gParameterTable, s)
            mr.ListSource = s



            If CBool(GetSetting(Database.SettingEnum.UseMonochrome)) Then
                s = GetSetting(Database.SettingEnum.Color)
                'c = Color.FromName(s)
                'mr.ColorRGB = String.Format("{0},{1},{2}", c.R, c.G, c.B)
                mr.ColorRGB = s '"e0e0e0"
                mr.DarkColorRGB = Me.DarkColor(s) '"808080"
            End If
        Next


        'For Each dr As DataRow In MenuSet.tMenuButton.Select
        '    s = dr("EnabledIf").ToString
        '    s = gPassParameters(gParameterTable, s)
        '    dr("EnabledIf") = s

        '    s = dr("CommandText").ToString
        '    s = gPassParameters(gParameterTable, s)
        '    dr("CommandText") = s

        'Next

    End Sub

#End Region

#Region "GetSessionID"
    Public Function GetSessionID() As Integer
        Return CInt(Session.Get(Tables.tSession.Field.ID))
    End Function

#End Region

#Region "GetCompanyID"
    'must return Object type because it can be dbnull.value
    Public Function GetCompanyID() As Object
        Return (Session.Get(Tables.tSession.Field.ID_Company))
    End Function
    Public ReadOnly Property CompanyName() As String
        Get
            'Dim o As String
            'o = Session.Get("Company").ToString
            'If o = "" Then
            '    o = "All Companies"
            'End If
            'Return o
            Return Session.Get("Company").ToString
        End Get
    End Property

    Public ReadOnly Property UserName() As String
        Get
            Return Session.Get("User").ToString
        End Get
    End Property

    Public ReadOnly Property BranchName() As String
        Get
            Return Session.Get("Branch").ToString
        End Get
    End Property
#End Region

#Region "GetCompanyID"
    'must return Object type because it can be dbnull.value
    Public Function GetUserID() As Integer
        Return CInt((Session.Get(Tables.tSession.Field.ID_User)))
    End Function

    Public Function GetEmployeeID() As Object
        Return (Session.Get(Tables.tSession.Field.ID_Employee))
    End Function

    Public Function GetBranchID() As Object
        Return (Session.Get("ID_Branch"))
    End Function
#End Region

    Public Function GetServerDate() As Date
        Try
            Dim dt As DataTable
            dt = GSCOM.SQL.TableQuery("SELECT GETDATE()", Connection)
            Return CDate(dt.Rows(0).Item(0))
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#Region "EndSession"
    Public Sub EndSession()
        Session.Set(0, Tables.tSession.Field.EndDateTime, GetServerDate)
        Try
            Session.Update()
        Catch ex As Exception
            'no message
        End Try
    End Sub

#End Region

    '#Region "SettingEnum"
    '    Public Enum SettingEnum
    '        CompanyName
    '        IconWidth
    '        IconHeight
    '        ReportPath
    '        ResourcePath
    '        PhotoPath
    '        StyleSheetPath
    '    End Enum

    '#End Region


#Region "gGetSetting"
    Public Function GetSetting(ByVal pSetting As SettingEnum, Optional ByVal pDefaultValue As String = "") As String
        Try
            Return SettingTable.Select(Tables.tSetting.Field.Name.ToString & "=" & GSCOM.SQL.SQLFormat(pSetting.ToString))(0).Item(Tables.tSetting.Field.Value.ToString).ToString
        Catch ex As Exception
            Return pDefaultValue
            'ROBBIE: Consider : do we have to notify the user that the setting is not found?
            'MsgBox("Setting " & pSetting.ToString & " is not found.", MsgBoxStyle.Exclamation)
        End Try
    End Function

    Public Function GetSetting(ByVal pSetting As String, Optional ByVal pDefaultValue As String = "") As String
        Try
            Return SettingTable.Select(Tables.tSetting.Field.Name.ToString & "=" & GSCOM.SQL.SQLFormat(pSetting.ToString))(0).Item(Tables.tSetting.Field.Value.ToString).ToString
        Catch ex As Exception
            Return pDefaultValue
            'ROBBIE: Consider : do we have to notify the user that the setting is not found?
            'MsgBox("Setting " & pSetting.ToString & " is not found.", MsgBoxStyle.Exclamation)
        End Try
    End Function
#End Region

    Private Function GetMenuRow(ByVal pMenu As Menu) As DataRow
        Dim dra() As DataRow
        dra = MenuTable.Select(Tables.tMenu.Field.ID.ToString & "=" & pMenu)
        If (dra Is Nothing) OrElse (dra.Length = 0) Then
            Return Nothing
        Else
            Return dra(0)
        End If
    End Function

    Public Function GetMenuValue(ByVal pMenu As Menu, ByVal pColumn As Tables.tMenu.Field) As Object
        Return GetMenuValue(pMenu, pColumn.ToString)
    End Function

    Public Function GetMenuValue(ByVal pMenu As Menu, ByVal pColumn As String) As Object
        Dim dr As DataRow
        dr = GetMenuRow(pMenu)
        If (dr Is Nothing) Then
            Throw New Exception(pMenu.ToString & " is not found")
        Else
            Return dr.Item(pColumn)
        End If
    End Function

    Public Function GetMenuDataSourceValue(ByVal pMenu As Menu) As String
        Return GetMenuValue(pMenu, Tables.tMenu.Field.DataSource).ToString
    End Function

    Public Sub RefreshLookUp()
        For Each dt As DataTable In LookUpSet.Tables
            dt.Clear() 'DO NOT DESTROY THE TABLES. JUST UPDATE THE REFERENCED TABLES
            GSCOM.SQL.FillTable(dt, "SELECT * FROM " & dt.TableName, Connection, True) 'Use * for filtering eg. tPayrollItem.id_payrollitemcategory
            GSCOM.UI.AddNullItem(dt)
        Next
    End Sub

    Public Function ImagePath(ByVal pImageFile As String) As String
        Dim l As Integer = IO.Path.GetFileNameWithoutExtension(pImageFile).Length
        If l = 36 And pImageFile.Contains("-") Then
            Return nGlobal.PhotosPath & pImageFile 'GetSetting(SettingEnum.PhotoPath) & pImageFile
        ElseIf l = 38 And Left(pImageFile, 2) = "F-" Then
            Return nGlobal.FilesPath & pImageFile 'GetSetting(SettingEnum.FilePath) & pImageFile
        Else
            Return nGlobal.ResourcePath & pImageFile 'GetSetting(SettingEnum.ResourcePath) & pImageFile
        End If
    End Function

#Region "GetLookup"
    Public Function GetLookUp(ByVal pTableName As String, Optional ByVal pCreateIfNothing As Boolean = True) As DataTable
        Dim dt As DataTable
        Try
            LookUpSet.EnforceConstraints = False 'Must place this code somewhere so it wont called again and again
            If LookUpSet.Tables.Contains(pTableName) Then
                Return LookUpSet.Tables.Item(pTableName)
            Else
                If pCreateIfNothing Then
                    dt = GSCOM.SQL.TableQuery("SELECT * FROM " & pTableName, Connection, True) 'Use * for filtering eg. tPayrollItem.id_payrollitemcategory
                    dt.TableName = pTableName
                    LookUpSet.Tables.Add(dt) 'Add the table to the dataset that does not enforce constraints before adding the null item
                    'If pAddNullItem Then
                    GSCOM.UI.AddNullItem(dt)
                    'End If
                    'ROBBIE: The Sort Property of the DefaultView 
                    'is reset to empty string when the table is added to a dataset
                    '...or its the entire defaultview that is changed?
                    'The solution: set the sort property after adding the table to the dataset (",)
                    'use seqno if available
                    If dt.Columns.Contains("SeqNo") Then
                        dt.DefaultView.Sort = "SeqNo,Name"
                    Else
                        dt.DefaultView.Sort = "Name"
                    End If
                    Return dt
                Else
                    Return Nothing
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

    '#Region "InitLookUp"
    '    Public Sub InitLookUp(ByVal pLookUp As GSCOM.UI.DataLookUp.DataLookUp)
    '        Dim dr As DataRow
    '        Dim dra() As DataRow
    '        dra = MenuTable.Select("Name=" & GSCOM.SQL.SQLFormat(pLookUp.Text))
    '        If dra.Length > 0 Then
    '            If dra.Length > 1 Then
    '                MsgBox("Warning! " & pLookUp.Text & " is ambiguous.")
    '            End If
    '            dr = dra(0)
    '            InitLookUp(pLookUp, dr)
    '        Else
    '            Throw New Exception("You must have the rights to view " & pLookUp.Text & " List.")
    '        End If
    '    End Sub

    '#End Region

#Region "InitLookUp (DataLookUp)"


#Region "InitLookUp"




#End Region

#End Region
    

    Public gParameterTable As ParameterDataTable

 

    Private Function GetSelectStringCore(ByVal pMenu As Integer) As DataTable
        'Private Function GetSelectStringCore(ByVal pMenu As Integer) As GSCOM.SQL.ZDataTable
        Dim sb As New System.Text.StringBuilder()
        Dim dt As New GSCOM.SQL.ZDataTable(Connection, "tUserMenuTabField")
        sb.AppendLine("(MenuTabMenuID=" & pMenu.ToString)
        sb.AppendLine("AND ID_User=" & GetUserID.ToString)
        sb.AppendLine("AND IsActive=1")
        sb.AppendLine("AND MenuTabIsActive=1")
        sb.AppendLine("AND ShowInList=1")
        sb.AppendLine("AND ((NOT EXISTS (SELECT ID FROM tSystemApplicationMenuTab samt WHERE samt.ID_MenuTab=vUserMenuTabField.ID_MenuTab)) OR (EXISTS (SELECT ID FROM vSystemApplicationMenuTab samt WHERE samt.IsActive=1 AND samt.ID_MenuTab=vUserMenuTabField.ID_MenuTab)))")
        sb.AppendLine(")")
        dt.ClearThenFill(sb.ToString, "SeqNo,MenuTabSeqNo,MenuTabMenuID,Panel,ID")
        Return dt
        '''''''''''
        'Dim sb As New System.Text.StringBuilder()
        'Dim dt As DataTable
        'sb.AppendLine("SELECT * FROM vUserMenuTabField WHERE ")
        'sb.AppendLine("(MenuTabMenuID=" & pMenu.ToString)
        'sb.AppendLine("AND ID_User=" & GetUserID.ToString)
        'sb.AppendLine("AND IsActive=1")
        'sb.AppendLine("AND MenuTabIsActive=1")
        'sb.AppendLine("AND ShowInList=1")
        'sb.AppendLine("AND ((NOT EXISTS (SELECT ID FROM tSystemApplicationMenuTab samt WHERE samt.ID_MenuTab=vUserMenuTabField.ID_MenuTab)) OR (EXISTS (SELECT ID FROM vSystemApplicationMenuTab samt WHERE samt.IsActive=1 AND samt.ID_MenuTab=vUserMenuTabField.ID_MenuTab)))")
        'sb.AppendLine(")")
        'sb.AppendLine(" ORDER BY SeqNo,MenuTabSeqNo,MenuTabMenuID,Panel,ID")
        'dt = GSCOM.SQL.TableQuery(sb.ToString, Connection)
        'Return dt
    End Function

    Public Function GetSelectStringFromTable(ByVal pMenu As Integer) As String ''HAS DUPLICATE IN DATALISTBASE
        Dim mColumnTable As DataTable = GetDisplayColumnsTable(pMenu)
        Dim s As String = ""
        If mColumnTable IsNot Nothing Then
            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder
            For Each dr As DataRow In mColumnTable.Select("", "SeqNo,MenuTabSeqNo,ID_MenuTab,Panel,ID")
                s = dr("Name").ToString
                If Strings.Left(s, 3) = "ID_" Then
                    s = Strings.Right(s, s.Length - 3)
                End If
                s = "[" & s & "]"
                sb.Append(s & ", ")
            Next
            s = sb.ToString
            s = Strings.Left(s, s.Length - 2) & " "
        Else
            's = mSelectString & " "
            s = "No selecttable"
            MsgBox(s)
            s = "* "
        End If
        Return s
    End Function

    Public Function GetDisplayColumnsTable(ByVal pMenu As Integer) As DataTable
        'Dim dt As GSCOM.SQL.ZDataTable
        Dim dt As DataTable
        dt = GetSelectStringCore(pMenu)
        If dt.Rows.Count = 0 Then
            Dim sb As New System.Text.StringBuilder()
            sb = New System.Text.StringBuilder
            sb.Append("EXEC pUserMenuTabField " & GetUserID() & "," & pMenu)
            GSCOM.SQL.ExecuteNonQuery(sb.ToString, Connection)
            dt = GetSelectStringCore(pMenu)
        End If
        Return dt
    End Function


    '''''''''''''''''''''''''''''''''''''''''''''''''''
    '''''''''''''''''''''''''''''''''''''''''''''''''''


    'Public Function fUser(ByVal pName As Object, ByVal pPassword As Object, ByVal pEmployeeCode As Object) As DataTable
    '    Dim s As String
    '    s = "SELECT * FROM dbo.fUser (" & GSCOM.SQL.SQLFormat(pName) & ", " & GSCOM.SQL.SQLFormat(pPassword) & ", " & GSCOM.SQL.SQLFormat(pEmployeeCode) & ")"
    '    Return GSCOM.SQL.TableQuery(s, Connection)
    'End Function


   



End Class
