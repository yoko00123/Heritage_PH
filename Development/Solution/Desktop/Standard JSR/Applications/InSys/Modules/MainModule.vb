Option Strict Off
Option Explicit On


Imports System
Imports System.Security.Cryptography
Imports System.Text
Imports InteropExcelReport.Methods_Excel

#Const UseNewConnection = 1


Module MainModule

    Sub Main(ByVal pSplashForm As Form)
        pSplashForm.Hide()
        Dim vLogInForm As LoginForm
        Dim vFirstTime As Boolean = True
L1:
        vLogInForm = New LoginForm
        If vFirstTime Then
            If Environment.GetCommandLineArgs.Length > 1 Then
                'vLogInForm.Visible = True
                AddHandler vLogInForm.Inited, AddressOf LogInForm_Inited
            End If
        End If
        vFirstTime = False
        If vLogInForm.ShowDialog() = vbOK Then
            gMainForm.Text = GSCOM.SQL.ExecuteScalar("Select Name From tSystemVersion Where IsActive = 1", gConnection).ToString & " - " & nDB.CompanyName ' gCompanyName
            'gMainForm.Text = My.Application.Info.Title & " - " & nDB.CompanyName ' gCompanyName
            If gMainForm.ShowDialog() = DialogResult.Retry Then
                gLogOffOnly = True
                GoTo L1
            Else
                gLogOffOnly = False
            End If
        End If

        pSplashForm.Close()
        'Application.Exit()

    End Sub

    Public IsSharedContents As Boolean = False
    Public lg As InSysNetworkProfile.Login = New InSysNetworkProfile.Login
    Public CustomSetting As String = getClient()
    Friend gLogOffOnly As Boolean

    Friend CONST_SERVERSETTINGFILE As String = IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData + CustomSetting, "serversetting.insys")
    Friend CONST_ACTIVATIONFILE As String = IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData, "Giraffe.lic")
    Friend nDB As Database.UserSession

    'Private Enum SortOrder As Integer
    '    Ascending
    '    Descending
    'End Enum

#Region "Declarations"
    Friend gIniFile As String = IO.Path.Combine(Application.StartupPath, "InSys.insysini")
    Friend gIconFile As String = IO.Path.Combine(Application.StartupPath, "icon.ico")
    Public gImageList As ImageList
    Friend CONST_REMEMBERMEFILE As String = IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData + CustomSetting, "rememberme.insys")
    Friend gDBUser As String
    Friend gDBPassword As String '= "sa"
    Friend gDBDataSource As String '= "."
    Friend gDBInitialCatalog As String '= "InSys"

    Friend DevPlat As String = ""
    Friend Const ZEM500 As String = "ZEM500"
    Friend gMainForm As MainForm
    Friend gUser As Integer = 1
    Friend gEmployee As Object
    Friend gUserEmployee As String
    Friend gIcon As Icon
    Friend gLogInForm As LoginForm
    Friend ReadOnly Property gInfoSize As Size
        Get
            Dim sz As New Size
            With sz
                '    .Width = My.Computer.Screen.WorkingArea.Width - 128
                '    .Height = My.Computer.Screen.WorkingArea.Height - 128
                '    If .Width < 800 Then .Width = 800
                '    If .Height < 600 Then .Height = 600
                If My.Computer.Screen.WorkingArea.Width > 1024 Then
                    .Width = 1024
                Else
                    .Width = 800
                End If
                If My.Computer.Screen.WorkingArea.Height > 768 Then
                    .Height = 768
                Else
                    .Height = 600
                End If
            End With
            Return sz
        End Get
    End Property

#End Region

    'EMIL
    Private Function getClient() As String
        Dim rslt As String = Nothing
        If IO.File.Exists(Application.StartupPath + "\Setting.ini") Then
            Dim ClientS As New ClientSelector
            If ClientS.ShowDialog() = DialogResult.OK Then
                rslt = ClientS.ComboBox1.SelectedItem
            End If
        End If
        If rslt Is Nothing Then
            Return Nothing
        Else
            Return "\" + rslt
        End If
    End Function

#Region "GetInfoSet"
    Friend Function GetInfoSet(ByVal pKey As Database.Menu) As InfoSet
        If gMainForm.gInfoSetCollection.Contains(pKey.ToString) Then
            Return CType(gMainForm.gInfoSetCollection.Item(pKey.ToString), InfoSet)
        Else
            Return Nothing
        End If
    End Function

#End Region

#Region "AddInfoSet"
    Friend Sub AddInfoSet(ByVal pInfoSet As InfoSet, ByVal pKey As Database.Menu)
        If Not gMainForm.gInfoSetCollection.Contains(pKey.ToString()) Then
            gMainForm.gInfoSetCollection.Add(pInfoSet, pKey.ToString)
        End If
    End Sub

#End Region

#Region "gConnection"
    Friend Function gConnection() As SqlClient.SqlConnection
        Return nDB.Connection
    End Function

#End Region

#Region "gInitLookUp"
    Friend Sub gInitLookUp(ByVal pBox As DataGridViewComboBoxColumn, Optional ByVal xFilter As String = Nothing)
        Dim dt As DataTable
        Dim s As String
        With pBox
            'ROBBIE: use the view not the table be some name is in another table eg. Employee-Persona
            s = "v"
            s &= Strings.Right(pBox.Name, pBox.Name.Length - ("ID_").Length)
            dt = nDB.GetLookUp(s)

            If xFilter IsNot Nothing Then dt.DefaultView.RowFilter = xFilter

            .DataSource = dt
            .ValueMember = "ID"
            .DisplayMember = "Name"
        End With
    End Sub

#End Region

#Region "gInitLookUp"
    Friend Sub gInitLookUp(ByVal pBox As GSCOM.UI.DataLookUp.DataGridViewLookUpColumn)
        Dim dt As DataTable
        Dim s As String
        With pBox
            'ROBBIE: use the view not the table be some name is in another table eg. Employee-Persona
            s = "v"
            s &= Strings.Right(pBox.Name, pBox.Name.Length - ("ID_").Length)
            dt = nDB.GetLookUp(s)


            '.DataSource = dt
            '.ValueMember = "ID"
            '.DisplayMember = "Name"
        End With
    End Sub

#End Region

#Region "gInit"



#End Region

#Region "gRefreshSettings"
    Public Sub gRefreshSettings()

        Dim s As String
        'Dim c As Color
        s = nDB.GetSetting(Database.SettingEnum.RequiredFieldBackColor)
        If s <> "" Then
            'c = Color.FromName(s)
            'If c.A <> 0 Then
            ' GSCOM.Common.DefaultRequiredFieldBackColor = c
            GSCOM.Common.DefaultRequiredFieldBackColor = GSCOM.Grafix.ColorFromRGB(s)
            GSCOM.Common.DefaultRequiredFieldOddBackColor = GSCOM.Common.DefaultRequiredFieldBackColor
        End If

        s = nDB.GetSetting(Database.SettingEnum.ReadOnlyFieldBackColor)
        If s <> "" Then
            GSCOM.Common.DefaultReadOnlyFieldBackColor = GSCOM.Grafix.ColorFromRGB(s)
            GSCOM.Common.DefaultReadOnlyFieldOddBackColor = GSCOM.Common.DefaultReadOnlyFieldBackColor
        End If



        s = nDB.GetSetting(Database.SettingEnum.DateFormat)
        If s <> "" Then
            GSCOM.Common.DefaultDateFormat = s
        End If
        s = nDB.GetSetting(Database.SettingEnum.TimeFormat)
        If s <> "" Then
            GSCOM.Common.DefaultTimeFormat = s
        End If
        s = nDB.GetSetting(Database.SettingEnum.DateTimeFormat)
        If s <> "" Then
            GSCOM.Common.DefaultDateTimeFormat = s
        End If

        s = nDB.GetSetting(Database.SettingEnum.SearchMode)
        If s <> "" Then
            GSCOM.Common.DefaultSearchMode = s
        End If
    End Sub

#End Region

#Region "SelectStandardMenu"
    'ROBBIE: InfoSet Must Be Passed ByRef because it would be pointed to another instance
    'Friend Sub SelectStandardMenu(ByVal pListing As DataTable, ByVal e As GSCOM.Interfaces.ZIDataList.ButtonClickEventArgs, ByVal pTabPage As GSCOM.Interfaces.ZIDataTabPageList)
    '    Dim i As Integer
    '    Dim k As Database.Menu
    '    i = CInt(pTabPage.Row.Item("ID"))
    '    k = CType(i, Database.Menu)
    '    Dim pInfoSet As InfoSet
    '    pInfoSet = GetInfoSet(k)
    '    Select Case e.ButtonText
    '        Case "New"
    '            If pInfoSet Is Nothing Then
    '                pInfoSet = NewInfo(k, pListing, 0)
    '            Else
    '                pInfoSet.LoadInfo(0)
    '            End If
    '            Application.DoEvents()
    '            If pInfoSet IsNot Nothing Then
    '                pInfoSet.ShowDialog()
    '            End If

    '        Case "Open"
    '            If e.SelectedID <> 0 Then
    '                If pInfoSet Is Nothing Then
    '                    pInfoSet = NewInfo(k, pListing, e.SelectedID)
    '                Else
    '                    pInfoSet.LoadInfo(e.SelectedID)
    '                End If
    '                Application.DoEvents()
    '                If pInfoSet IsNot Nothing Then
    '                    pInfoSet.ShowDialog()
    '                End If
    '            End If
    '        Case "Delete"
    '            If MsgBox("Are you sure you want to delete this record?", MsgBoxStyle.Question Or MsgBoxStyle.YesNoCancel Or MsgBoxStyle.DefaultButton2, "") = MsgBoxResult.Yes Then
    '                GSCOM.SQL.TableQuery("DELETE FROM " & pListing.TableName & " WHERE ID=" & e.SelectedID, gConnection)
    '            End If
    '    End Select
    'End Sub

    Friend Sub SelectStandardMenu(ByVal sender As GSCOM.Interfaces.ZIDataTabPageList, ByVal pListing As DataTable, ByVal e As GSCOM.Interfaces.ZIDataList.ButtonClickEventArgs, ByVal pTabPage As GSCOM.Interfaces.ZIDataTabPageList)
        Dim i As Integer
        Dim k As Database.Menu
        i = CInt(pTabPage.Row.Item("ID"))
        k = CType(i, Database.Menu)
        SelectStandardMenu(sender, k, pListing, e)

    End Sub


    Friend Sub SelectStandardMenu(ByVal sender As Object, ByVal pMenu As Database.Menu, ByVal pListing As DataTable, ByVal e As GSCOM.Interfaces.ZIDataList.ButtonClickEventArgs)
        Dim pInfoSet As InfoSet
        pInfoSet = GetInfoSet(pMenu)
        Select Case e.ButtonText
            Case "New"
                If pInfoSet Is Nothing Then
                    pInfoSet = NewInfo(pMenu, pListing, 0)
                Else
                    pInfoSet.LoadInfo(0)
                End If
                Application.DoEvents()

                Dim o As GSCOM.UI.DataLookUp.DataLookUp
                o = TryCast(sender, GSCOM.UI.DataLookUp.DataLookUp)
                If o IsNot Nothing Then
                    If o.Worker.ParentLookUp IsNot Nothing Then
                        pInfoSet.mDataset.Tables(0).Rows(0).Item(o.Worker.ChildColumnName) = o.Worker.ParentLookUp.SelectedValue  'INITIALIZE THE COLUMN. SET TO PARENT
                    End If
                End If



                If pInfoSet IsNot Nothing Then
                    pInfoSet.ShowDialog()
                End If

            Case "Open", "Open Info"
                If e.SelectedID <> 0 Then
                    If pInfoSet Is Nothing Then
                        pInfoSet = NewInfo(pMenu, pListing, e.SelectedID)
                    Else
                        pInfoSet.LoadInfo(e.SelectedID)
                    End If
                    Application.DoEvents()
                    If pInfoSet IsNot Nothing Then

                        pInfoSet.ShowDialog()
                    End If
                End If
            Case "Print"
                Dim s, tmp As String
                Dim f As New BrowserForm
                f.Size = New Size(1024, 600)
                f.StartPosition = FormStartPosition.CenterScreen ' MainForm.StartPosition
                'f.WindowState = FormWindowState.Maximized
                Dim a As New Html.HtmlList()
                a.ImageFile = nDB.GetMenuValue(pMenu, Database.Tables.tMenu.Field.ImageFile).ToString
                a.StyleSheet = nDB.nGlobal.StyleSheetPath 'nDB.GetSetting(Database.SettingEnum.StyleSheetPath).ToString & "main.css"
                a.ResourcePath = nDB.nGlobal.ResourcePath 'nDB.GetSetting(Database.SettingEnum.ResourcePath).ToString
                'a.SchemaTable = CType(DirectCast(e.DataListBase, GSCOM.UI.DataList.DataListBase).MainView, GSCOM.UI.GSDataGridView.GSDataGridView).SchemaTable
                a.SchemaTable = DirectCast(e.DataListBase, GSCOM.UI.DataList.DataListBase).MainView.SchemaTable
                a.Name = nDB.GetMenuValue(pMenu, Database.Tables.tMenu.Field.Name).ToString
                a.TabHeaderColor = nDB.GetMenuValue(pMenu, Database.Tables.tMenu.Field.DarkColorRGB).ToString
                a.CaptionBackColor = nDB.GetMenuValue(pMenu, Database.Tables.tMenu.Field.ColorRGB).ToString
                'a.MenuID = CType(sender, GSCOM.Interfaces.ZIDataTabPageList).Row("ID")
                a.MenuID = pMenu
                'a.TabHeaderColor = nDB.GetMenuValue(pMenu, Database.Tables.tMenu.Field.DataSou).ToString
                s = a.GetHtml(pListing)
                tmp = IO.Path.GetTempFileName
                '--\ use streamwriter to control encoding. weird: IO.File.WriteAllText(tmp, s, System.Text.Encoding.Unicode) prompts save --\
                Dim tf As New IO.StreamWriter(tmp, False, System.Text.Encoding.UTF8)
                tf.Write(s)
                tf.Flush()
                '-- use streamwriter to control encoding. weird: IO.File.WriteAllText(tmp, s, System.Text.Encoding.Unicode) prompts save -/
                Try
                    f.MainBrowser.Navigate(tmp)
                    f.ShowDialog()
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            Case "Delete"
                Dim s As String
                If e.SelectedID = 0 Then
                    s = "Can not perform delete function. There was no record specified."
                    MsgBox(s, MsgBoxStyle.Information)
                Else
                    If CBool(nDB.GetMenuValue(pMenu, Database.Tables.tMenu.Field.AllowDelete)) Then
                        s = "Are you sure you want to delete this record?"
                        If MsgBox(s, MsgBoxStyle.Question Or MsgBoxStyle.YesNoCancel Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                            s = nDB.GetMenuValue(pMenu, Database.Tables.tMenu.Field.TableName).ToString
                            If Not String.IsNullOrEmpty(s) Then
                                Try
                                    GSCOM.SQL.TableQuery("DELETE FROM " & s & " WHERE ID=" & e.SelectedID, gConnection)
                                    Dim r As DataRow
                                    r = pListing.Select("ID = " & e.SelectedID)(0)
                                    pListing.Rows.Remove(r)
                                Catch ex As Exception
                                    s = getSystemMessage(ex.Message)
                                    MsgBox(s, MsgBoxStyle.Information)
                                End Try
                            Else
                                MsgBox("Table name is not specified", MsgBoxStyle.Exclamation)
                            End If
                        End If
                    Else
                        s = "Can not perform delete function. Delete function is not supported for this listing"
                        MsgBox(s, MsgBoxStyle.Information)
                    End If
                End If
            Case "Columns"
                ShowColumnsDialog(sender, pMenu)
        End Select
    End Sub

    Private Sub ShowColumnsDialog(ByVal sender As Object, ByVal pMenu As Database.Menu)
        'Dim vDisplayColumnsTable As GSCOM.SQL.ZDataTable = nDB.GetDisplayColumnsTable(pMenu) 'vSettings.GetSetting(UI.DataList.Settings.KeyEnum.SelectString, "* ")
        Dim lf As GSCOM.Interfaces.ZIDataTabPageList
        Dim dlb As GSCOM.UI.DataList.DataListBase
        lf = TryCast(sender, GSCOM.Interfaces.ZIDataTabPageList)
        If lf IsNot Nothing Then
            dlb = TryCast(lf.MainList, GSCOM.UI.DataList.DataListBase)
            'dlb.ColumnTable = vDisplayColumnsTable
            Dim a As New ColumnsDialog(pMenu, gConnection, dlb.ColumnTable)
            a.Icon = MainForm.Icon
            If a.ShowDialog() = DialogResult.OK Then
                'dlb.ColumnTable = vDisplayColumnsTable
                dlb.SelectString = dlb.GetSelectStringFromTable
                dlb.MainView.ClearColumns()  'NOTE: MUST CLEAR THE COLUMNS FOR RECREATION
                dlb.FillDatabase(False, True)   'NOTE: 
                ' dlb.RenameHeaders()
                dlb.RefreshFilters()
            End If
        End If

    End Sub
#End Region

#Region "ButtonClicked"

    Friend Sub ButtonClick(ByVal sender As Object, ByVal e As GSCOM.Interfaces.ZIDataList.ButtonClickEventArgs)
        Dim dt As DataTable
        Dim pTabPage As GSCOM.Interfaces.ZIDataTabPageList = CType(sender, GSCOM.Interfaces.ZIDataTabPageList)
        Try
            If e.ButtonText = "Close" Then
                gMainForm.CloseTabPage(gMainForm.tcMain.SelectedTab)
                Exit Sub
            End If

            dt = CType(pTabPage.MainList.DataSource, DataTable)
            SelectStandardMenu(sender, dt, e, pTabPage)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
        End Try
    End Sub

    Friend Sub LookUpButtonClick(ByVal sender As Object, ByVal e As GSCOM.Interfaces.ZIDataList.ButtonClickEventArgs)
        Dim o As Control 'either datalookup or lookupedittingcontrol
        o = CType(sender, Control)
        Dim i As Integer
        Dim k As Database.Menu
        k = CType(i, Database.Menu)
        Dim pMenu As Database.Menu = CType(o.Tag, Database.Menu)
        SelectStandardMenu(sender, pMenu, Nothing, e)
    End Sub

    Public Function DecimalToHoursAndMinutes(ByVal d As Decimal) As String
        Dim s As String
        Dim dp As Decimal
        Dim i As Integer
        i = CInt(Math.Floor(d))
        dp = (d - i) * 60
        s = Format(i, "00") & ":" & Format(dp, "00")
        Return s
    End Function

    Public Function HoursAndMinutesToDecimal(ByVal s As String) As Decimal
        Dim d As Decimal
        Dim intHours As Integer
        Dim intMins As Decimal
        Dim a As String
        Dim p As Integer
        p = InStr(1, s, ":")
        a = Strings.Left(s, p - 1)
        intHours = CInt(a)
        a = Strings.Right(s, Len(s) - p)
        'intMins = CInt(Mid(s, p + 1, Len(s) - (Len(CStr(intHours)) + 1)))
        intMins = CInt(a)
        intMins = intMins / 60
        d = intHours + intMins
        Return d
    End Function



#End Region

#Region "COLORS"

    Friend Function GetHexColor(ByVal colorObj As System.Drawing.Color) As String
        Dim s As String = System.Drawing.ColorTranslator.ToHtml(colorObj)
        If Not s.StartsWith("#") Then
            Dim c As System.Drawing.Color = Color.FromName(s)
            Return c.ToArgb().ToString("X").Remove(0, 2)
        Else
            Return System.Drawing.ColorTranslator.ToHtml(colorObj).Replace(CChar("#"), "")
        End If
    End Function

    Friend Function getColor(ByVal settingName As String) As Color
        Dim s As Object = Trim(GSCOM.SQL.ExecuteScalar("Select dbo.fGetSetting('" + settingName + "')", gConnection).ToString().Replace(CChar("#"), ""))
        Dim colorValue As Color = Nothing
        If s <> "" Then
            Return System.Drawing.ColorTranslator.FromHtml("#" + s)
        Else
            Return Nothing
        End If
    End Function

    Friend Function getColorValue(ByVal HexValue As String) As Color
        Return System.Drawing.ColorTranslator.FromHtml("#" + HexValue)
    End Function

#End Region

#Region "Payroll Adjustments"
    Public Function CanAdjust(ByVal dateValue As DateTime) As Boolean
        Try
            Dim dateNow As DateTime
            Dim Months As Integer
            Dim UserGroupID As Integer = 0
            Dim strSql As String

            strSql = "SELECT ID_UserGroup FROM tUser WHERE ID=" & nDB.GetUserID.ToString
            UserGroupID = CInt(GSCOM.SQL.ExecuteScalar(strSql, gConnection))

            dateNow = Date.Now.Date
            Months = CInt(DateDiff(DateInterval.Month, dateValue, dateNow))

            If Months = 0 Or UserGroupID = 2 Or UserGroupID = 1 Then
                Return True
            End If

            Return False
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try

    End Function
#End Region

#Region "gDestroy"
    Friend Sub gDestroy()
        'gConnection = Nothing
        nDB.Connection = Nothing
        gMainForm = Nothing
        nDB.SettingTable = Nothing
        'oPersona = Nothing
        'oEmployee = Nothing
        'oUser = Nothing
        'oSetting = Nothing
        'oDesignation = Nothing
        'oDepartment = Nothing
        'oMealSched = Nothing
    End Sub

#End Region

#Region "SystemMessage"

    Public Function getSystemMessage(ByVal systemMessage As String) As String

        'this is to check constraints-------------------------\
        Dim s As String
        'to make sure the rows are ordered by name use this: 
        '.Select("", "Name"). but this is already done in query
        For Each dr As DataRow In nDB.SystemMessageTable.Rows
            s = dr("Name").ToString
            If InStr(systemMessage, s, CompareMethod.Text) > 0 Then
                s = dr("Description").ToString
                Return s
            End If
        Next
        s = systemMessage
        'this is to check constraints-------------------------/
        Return s
    End Function

#End Region


#Region "SELECT Statements"
    Public Function GetSingleValue(ByVal strSql As String) As String
        Dim dtr As New DataTable
        Dim Result As String = ""
        dtr = GSCOM.SQL.TableQuery(strSql, gConnection)
        If dtr.Rows.Count > 0 Then
            Result = dtr.Rows(0).Item(0).ToString
        End If
        Return Result
    End Function
#End Region


#Region "ConnectionSetting"

    Friend Function RSADecrypt(ByVal DataToDecrypt() As Byte, ByVal RSAKeyInfo As RSAParameters, ByVal DoOAEPPadding As Boolean) As Byte()
        Try
            'Create a new instance of RSACryptoServiceProvider.
            Dim RSA As New RSACryptoServiceProvider()

            'Import the RSA Key information. This needs
            'to include the private key information.
            RSA.ImportParameters(RSAKeyInfo)

            'Decrypt the passed byte array and specify OAEP padding.  
            'OAEP padding is only available on Microsoft Windows XP or
            'later.  
            Return RSA.Decrypt(DataToDecrypt, DoOAEPPadding)
            'Catch and display a CryptographicException  
            'to the console.
        Catch e As CryptographicException
            Console.WriteLine(e.ToString())

            Return Nothing
        End Try
    End Function




#End Region


    Public Sub InitLookUp(ByVal pLookUp As GSCOM.UI.DataLookUp.DataLookUp, ByVal pMenu As Database.Menu)
        'Dim dr As DataRow
        Dim dra() As DataRow
        Dim s As String
        Dim mr As New Database.MenuRow
        s = GSCOM.SQL.SQLFormat(CInt(pMenu))
        dra = nDB.MenuTable.Select("ID=" & s)
        If dra.Length > 0 Then
            If dra.Length > 1 Then
                MsgBox("Warning! " & pLookUp.Text & " is ambiguous.")
            End If
            mr.InnerRow = dra(0)
            InitLookUp(pLookUp, mr)
        Else
            Dim sa() As String
            sa = pMenu.ToString.Split("_"c)
            If (sa IsNot Nothing) AndAlso sa.Length > 0 Then
                Throw New Exception("You must have the rights to view " & sa(sa.Length - 1) & " List.")
            Else
                Throw New Exception("You must have the rights to view " & pMenu.ToString & " List.")
            End If

        End If
    End Sub


    Public Sub InitLookUp(ByVal pLookUp As GSCOM.UI.DataLookUp.DataLookUp, ByVal mr As Database.MenuRow)
        pLookUp.Text = mr.Name
        pLookUp.Worker.OddBackColor = GSCOM.Grafix.ColorFromRGB(mr.ColorRGB)
        If gImageList IsNot Nothing Then
            pLookUp.Image = gImageList.Images(mr.ImageFile)
        End If
        With pLookUp.Worker
            .Connection = nDB.Connection
            .SetDataSource(mr.DataSource, mr.BaseDataSource, Nothing)
            .ValueMember = "ID"
            .DisplayMember = "Name"
            pLookUp.Tag = mr.ID
            .DataListSelectString = nDB.GetDisplayColumnsTable(mr.ID)
            .Sort = mr.Sort
            '20070428-------------------\

            'Try
            '    Dim dt As DataTable
            '    s = "SELECT TOP 2 " & .ValueMember & ", " & .DisplayMember & " FROM " & s
            '    dt = GSCOM.SQL.TableQuery(s, Connection)
            '    If dt.Rows.Count = 1 Then
            '        Dim dr As DataRow
            '        dr = dt.Rows(0)
            '        .DefaultValue = dr(.ValueMember)
            '        .DefaultDisplay = dr(.DisplayMember).ToString
            '    End If
            'Catch ex As Exception

            'End Try

            '20070428-------------------/
            AddHandler pLookUp.ButtonClick, LookUpButtonClickEventHandler '20061204
        End With
    End Sub

    Public LookUpButtonClickEventHandler As GSCOM.UI.DataList.DataListBase.ButtonClickEventHandler

#Region "InitLookUp"
    Public Sub InitLookUp(ByVal pLookUp As GSCOM.UI.DataLookUp.DataGridViewLookUpColumn, ByVal pMenu As Menu)
        'Dim dr As DataRow
        'Dim dra() As DataRow
        'Dim s As String
        's = GSCOM.SQL.SQLFormat(CInt(pMenu))
        'dra = MenuTable.Select("ID=" & s)
        'If dra.Length > 0 Then
        '    If dra.Length > 1 Then
        '        'review
        '        MsgBox("Warning! " & pLookUp.DataPropertyName & " is ambiguous.")
        '    End If
        '    dr = dra(0)
        '    'pLookUp.DataSource = dr.Item("DataSource").ToString
        '    'pLookUp.OddBackColor = Drawing.Color.FromName(dr.Item("Color").ToString)
        '    InitLookUp(pLookUp, dr)
        'Else
        '    'review
        '    'Throw New Exception("You must have the rights to view " & pLookUp.DataPropertyName & " List.")
        '    Throw New Exception("You must have the rights to view " & pLookUp.HeaderText & " List.")
        'End If
        InitLookUp(pLookUp, pMenu)
    End Sub

    Public Sub InitLookUp(ByVal pLookUp As GSCOM.UI.DataLookUp.DataGridViewLookUpColumn, ByVal pMenu As Integer)
        'Dim dr As DataRow
        Dim dra() As DataRow
        Dim s As String
        s = GSCOM.SQL.SQLFormat(pMenu)
        dra = nDB.MenuTable.Select("ID=" & s)
        Dim mr As New Database.MenuRow
        If dra.Length > 0 Then
            If dra.Length > 1 Then
                'review
                MsgBox("Warning! " & pLookUp.DataPropertyName & " is ambiguous.")
            End If
            mr.InnerRow = dra(0)
            'pLookUp.DataSource = dr.Item("DataSource").ToString
            'pLookUp.OddBackColor = Drawing.Color.FromName(dr.Item("Color").ToString)
            InitLookUp(pLookUp, mr)
        Else
            'review
            'Throw New Exception("You must have the rights to view " & pLookUp.DataPropertyName & " List.")
            Throw New Exception("You must have the rights to view " & pLookUp.HeaderText & " List.")
        End If
    End Sub
#End Region

#Region "InitLookUp"
    Public Sub InitLookUp(ByVal pLookUp As GSCOM.UI.DataLookUp.DataGridViewLookUpColumn, ByVal mr As Database.MenuRow)
        'Dim s As String
        'Dim c As System.Drawing.Color
        's = pRow.Item("DataSource").ToString
        'c = Drawing.Color.FromName(pRow.Item("Color").ToString)
        With pLookUp
            'ROBBIE 20100316
            .Text = mr.Name
            '.HeaderText = .Text''must come from tmenudetailtabfield label
            .OddBackColor = GSCOM.Grafix.ColorFromRGB(mr.ColorRGB)
            If gImageList IsNot Nothing Then
                .Image = gImageList.Images(mr.ImageFile)
            End If
            .Connection = gConnection()
            .DataSource = mr.DataSource
            .ValueMember = "ID"
            .DisplayMember = "Name"
            .Tag = mr.ID  '20061204
            .DataListSelectString = nDB.GetDisplayColumnsTable(mr.ID)
            AddHandler pLookUp.ButtonClick, LookUpButtonClickEventHandler '20061204
        End With
    End Sub



#End Region

#Region "InitControl"
    Public Sub InitControl(ByVal pControl As Control, Optional ByVal pMenu As Integer = Nothing)
        Dim cc() As System.Windows.Forms.Control = Nothing
        Dim p As Integer

        'pControl.Width = 500
        GSCOM.UI.AllControls(cc, pControl)
        If cc IsNot Nothing Then

            For Each c As Control In cc
                If TypeOf c Is ComboBox Then

                    Dim cbo As ComboBox
                    cbo = CType(c, ComboBox)
                    MainModule.InitLookUp(cbo)
                    cbo.DropDownStyle = ComboBoxStyle.DropDownList
                    'cbo.FlatStyle = FlatStyle.Flat

                    cbo.DropDownWidth *= 2
                    cbo.MaxDropDownItems = 32
                ElseIf TypeOf c Is GSCOM.UI.DataLookUp.DataLookUp Then
                    'Dim cbo As GSCOM.UI.DataLookUp.DataLookUp
                    'cbo = CType(c, GSCOM.UI.DataLookUp.DataLookUp)
                    'InitLookUp(cbo)


                ElseIf TypeOf c Is GSCOM.UI.DataImage.DataImage Then
                    Dim cbo As GSCOM.UI.DataImage.DataImage
                    cbo = CType(c, GSCOM.UI.DataImage.DataImage)
                ElseIf TypeOf c Is TableLayoutPanel Then
                    For Each tc As Control In c.Controls
                        p = CType(c, TableLayoutPanel).GetRow(tc)
                        If p <> -1 Then
                            tc.TabIndex = p
                        End If
                    Next
                End If
            Next
        End If

    End Sub

    'Public Function GetAttendanceTemplate() As Byte()
    '    Return My.Resources.AttendanceTemplate
    'End Function

    'Public Function Getpc_tran() As Byte()
    '    Return My.Resources.pc_tran
    'End Function
#End Region

#Region "InitLookUp"
    Public Sub InitLookUp(ByVal pBox As ComboBox, Optional ByVal xFilter As String = Nothing)
        Dim dt As DataTable
        Dim s As String
        With pBox
            'ROBBIE: use the List not the table because sometimes name is in another table eg. Employee-Persona
            s = "v"
            s &= Strings.Right(pBox.Name, pBox.Name.Length - ("_ID_").Length)
            'rOBBIE 20060910
            's &= "_List"
            dt = nDB.GetLookUp(s)
            If xFilter IsNot Nothing Then dt.DefaultView.RowFilter = Trim(xFilter)

            .DataSource = dt
            .ValueMember = "ID"
            .DisplayMember = "Name"
        End With
    End Sub

#End Region

    Friend Sub InitDataListBase(ByVal lf As GSCOM.Interfaces.ZIDataTabPageList, ByVal mr As Database.MenuRow)
        Dim a As GSCOM.UI.DataList.DataListBase
        a = CType(lf.MainList, UI.DataList.DataListBase)
        a.ImageList = gImageList
        a.GetStripButton("New").Enabled = mr.AllowNew
        a.GetStripButton("Delete").Enabled = mr.AllowDelete
        a.GetStripButton("Open").Enabled = mr.AllowOpen
    End Sub

    Friend Sub InitZIDataTabPageList(ByVal lf As GSCOM.Interfaces.ZIDataTabPageList, ByVal mr As Database.MenuRow)
        Dim pOddBackColor As Color
        If mr.ColorRGB <> "" Then
            pOddBackColor = GSCOM.Grafix.ColorFromRGB(mr.ColorRGB)
        Else
            pOddBackColor = Color.Gainsboro
        End If
        lf.Row = mr.InnerRow
        lf.MainList.OddBackColor = pOddBackColor
        lf.Text = mr.Name
        lf.ImageIndex = gImageList.Images.IndexOfKey(mr.ImageFile) 'ROBBIE: IMAGEKEY IS NOT SUPPORTED????? lf.ImageKey = vImageKey
        lf.MainList.Text = mr.Name
        lf.MainList.ReportPath = mr.ReportFile
    End Sub

    Public Function NewReport(ByVal mMenu As Integer) As InfoSet
        Dim pM As Database.Menu = CType(577, Database.Menu)
        Dim pInfoSet As InfoSet = GetInfoSet(pM)

        Dim s As String = "SELECT ID FROM tCompanyMenu cm WHERE cm.ID_Company=" & GSCOM.SQL.SQLFormat(nDB.GetCompanyID) & " AND cm.ID_Menu=" & mMenu.ToString
        Dim vID As Object = GSCOM.SQL.ExecuteScalar(s, gConnection)

        If IsDBNull(vID) Then
            vID = 0
        End If

        If pInfoSet Is Nothing Then
            pInfoSet = ActiveModule.NewInfo(pM, Nothing, CInt(vID))
        Else
            pInfoSet.LoadInfo(CInt(vID))
        End If
        If CInt(vID) = 0 Then
            Dim dt As DataTable = pInfoSet.mDataset.Tables(0)
            dt.Rows(0).Item("ID_Menu") = mMenu
        End If
        Application.DoEvents()
        pInfoSet.ShowDialog()

        Return pInfoSet
    End Function
    Public Function DocumentProperties(ByVal mMenu As Integer, ByVal pID As Integer) As InfoSet
        Dim pM As Database.Menu = CType(630, Database.Menu)
        Dim pInfoSet As InfoSet = GetInfoSet(pM)

        Dim s As String = "SELECT ID FROM tDocumentProperties cm WHERE cm.ID_Company=" & GSCOM.SQL.SQLFormat(nDB.GetCompanyID) & " AND cm.ID_Menu=" & mMenu.ToString & " AND ID_Original = " & pID.ToString
        Dim vID As Object = GSCOM.SQL.ExecuteScalar(s, gConnection)

        If IsDBNull(vID) Then
            vID = 0
        End If

        If pInfoSet Is Nothing Then
            pInfoSet = ActiveModule.NewInfo(pM, Nothing, CInt(vID))
        Else
            pInfoSet.LoadInfo(CInt(vID))
        End If
        If CInt(vID) = 0 Then
            Dim dt As DataTable = pInfoSet.mDataset.Tables(0)
            dt.Rows(0).Item("ID_Menu") = mMenu
        End If
        Application.DoEvents()
        pInfoSet.ShowDialog()
        Return pInfoSet
    End Function
    Friend Function MsgBox(ByVal Prompt As Object, Optional ByVal Buttons As Microsoft.VisualBasic.MsgBoxStyle = vbOKOnly, Optional ByVal Title As String = "") As Microsoft.VisualBasic.MsgBoxResult
        If Title = "" Then Title = My.Application.Info.Title
        Return Microsoft.VisualBasic.Interaction.MsgBox(Prompt, Buttons, Title)
    End Function

    'Friend Sub CreateServerSettingFile(ByVal pServer As String, ByVal pDatabase As String, ByVal pUserName As String, ByVal pPassword As String)
    '    Try
    '        Dim s As String
    '        Dim encryptedData() As Byte
    '        Dim ByteConverter As New System.Text.UnicodeEncoding()
    '        Dim RSA As New System.Security.Cryptography.RSACryptoServiceProvider
    '        Dim xmlKey As String
    '        Dim fn As Integer = FreeFile()


    '        s = pServer
    '        s &= CChar(ChrW(0)) & pDatabase
    '        s &= CChar(ChrW(0)) & pUserName
    '        s &= CChar(ChrW(0)) & pPassword

    '        Dim dataToEncrypt As Byte() = ByteConverter.GetBytes(s)

    '        xmlKey = My.Resources.RSAKeyValue
    '        RSA.FromXmlString(xmlKey)

    '        encryptedData = RSAEncrypt(dataToEncrypt, RSA.ExportParameters(False), False)



    '        FileOpen(fn, CONST_SERVERSETTINGFILE, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)
    '        FilePutObject(fn, encryptedData)
    '        FileSystem.FileClose(fn)
    '        MsgBox("Setting Saved", MsgBoxStyle.Information)


    '    Catch ex As Exception
    '        MsgBox(ex.Message, MsgBoxStyle.Critical)
    '    End Try
    'End Sub
    'Friend Sub CreateServerSettingFile(ByVal pServer As String, ByVal pDatabase As String, ByVal pUserName As String, ByVal pPassword As String)
    '    Try
    '        Dim s As String
    '        Dim encryptedData() As Byte
    '        Dim ByteConverter As New System.Text.UnicodeEncoding()
    '        Dim RSA As New System.Security.Cryptography.RSACryptoServiceProvider(2048)
    '        Dim xmlKey As String
    '        Dim fn As Integer = FreeFile()


    '        s = pServer
    '        s &= CChar(ChrW(0)) & pDatabase
    '        s &= CChar(ChrW(0)) & pUserName
    '        s &= CChar(ChrW(0)) & pPassword

    '        Dim dataToEncrypt As Byte() = ByteConverter.GetBytes(s)
    '        MsgBox(RSA.KeySize)
    '        xmlKey = My.Resources.RSAKeyValue
    '        RSA.FromXmlString(xmlKey)
    '        MsgBox(RSA.KeySize)
    '        encryptedData = RSAEncrypt(dataToEncrypt, RSA.ExportParameters(False), False)



    '        FileOpen(fn, CONST_SERVERSETTINGFILE, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)
    '        FilePutObject(fn, encryptedData)
    '        FileSystem.FileClose(fn)
    '        MsgBox("Setting Saved", MsgBoxStyle.Information)

    '    Catch ex As Exception
    '        MsgBox(ex.Message, MsgBoxStyle.Critical)
    '    End Try
    'End Sub

    Friend Sub CreateServerSettingFile(ByVal pServer As String, ByVal pDatabase As String, ByVal pUserName As String, ByVal pPassword As String)
        Try
            Dim s As String
            'Dim encryptedData() As Byte
            Dim ByteConverter As New System.Text.UnicodeEncoding()
            Dim RSA As New System.Security.Cryptography.RSACryptoServiceProvider
            'Dim xmlKey As String
            Dim fn As Integer = FreeFile()


            s = pServer
            s &= CChar(ChrW(0)) & pDatabase
            s &= CChar(ChrW(0)) & pUserName
            s &= CChar(ChrW(0)) & pPassword

            Dim dataToEncrypt As Byte() = ByteConverter.GetBytes(s)

            'xmlKey = My.Resources.RSAKeyValue
            'RSA.FromXmlString(xmlKey)
            s = GSCOM.Common.EncryptA(s, 41)
            'encryptedData = RSAEncrypt(dataToEncrypt, RSA.ExportParameters(False), False)



            'FileOpen(fn, CONST_SERVERSETTINGFILE, OpenMode.Output, OpenAccess.Write, OpenShare.Shared)
            'FilePutObject(fn, s)
            'FileSystem.FileClose(fn)

            If Not IO.Directory.Exists(IO.Path.GetDirectoryName(CONST_SERVERSETTINGFILE)) Then
                IO.Directory.CreateDirectory(IO.Path.GetDirectoryName(CONST_SERVERSETTINGFILE))
                SetPermission(IO.Path.GetDirectoryName(CONST_SERVERSETTINGFILE))
            End If

            IO.File.WriteAllText(CONST_SERVERSETTINGFILE, s)
            MsgBox("Setting Saved", MsgBoxStyle.Information)

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub SetPermission(ByVal FolderPath As String)
        Dim UserAccount As String = My.User.Name
        Dim FolderInfo As IO.DirectoryInfo = New IO.DirectoryInfo(FolderPath)
        Dim FolderAcl As New System.Security.AccessControl.DirectorySecurity
        Try
            FolderAcl.AddAccessRule(New Security.AccessControl.FileSystemAccessRule(UserAccount, Security.AccessControl.FileSystemRights.FullControl, Security.AccessControl.InheritanceFlags.ContainerInherit Or Security.AccessControl.InheritanceFlags.ObjectInherit, Security.AccessControl.PropagationFlags.None, Security.AccessControl.AccessControlType.Allow))
            'FolderAcl.SetAccessRuleProtection(True, False) 'uncomment to remove existing permissions
            FolderInfo.SetAccessControl(FolderAcl)
        Catch
        End Try
    End Sub

    Friend Function RSAEncrypt(ByVal DataToEncrypt() As Byte, ByVal RSAKeyInfo As System.Security.Cryptography.RSAParameters, ByVal DoOAEPPadding As Boolean) As Byte()
        Try
            Dim RSA As New System.Security.Cryptography.RSACryptoServiceProvider()
            RSA.ImportParameters(RSAKeyInfo)
            Return RSA.Encrypt(DataToEncrypt, DoOAEPPadding)
        Catch e As System.Security.Cryptography.CryptographicException
            Console.WriteLine(e.Message)
            Return Nothing
        End Try
    End Function



    Friend gFromAlert As Boolean

    Private Sub LogInForm_Inited(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lf As LoginForm = sender

        Dim sa As String() = Environment.GetCommandLineArgs
        '-N -W -C -M -S -U -P -d -A
        Dim N As String = ""
        Dim W As String = ""
        Dim S As String = ""
        Dim U As String = ""
        Dim P As String = ""
        Dim d As String = ""
        Dim M As String = ""
        Dim C As String = ""


        For Each ss As String In sa
            If Left(ss, 2) = "-N" Then
                N = Mid(ss, 3)
            End If
            If Left(ss, 2) = "-W" Then
                W = Mid(ss, 3)
            End If
            If Left(ss, 2) = "-C" Then
                C = Mid(ss, 3)
            End If
            If Left(ss, 2) = "-S" Then
                S = Mid(ss, 3)
            End If
            If Left(ss, 2) = "-U" Then
                U = Mid(ss, 3)
            End If
            If Left(ss, 2) = "-P" Then
                P = Mid(ss, 3)
            End If
            If Left(ss, 2) = "-d" Then
                d = Mid(ss, 3)
            End If
            If Left(ss, 2) = "-M" Then
                M = Mid(ss, 3)
            End If
            If Left(ss, 2) = "-A" Then
                gFromAlert = CBool(Mid(ss, 3))
            End If
        Next
        With lf

            .txtUserName.Text = N
            .txtPassword.Text = W
            .ServerBox.Text = S
            .DBBox.Text = d
            .DBUserNameBox.Text = U
            .DBPasswordBox.Text = P

            Dim mCompanyTable As DataTable = ._ID_Company.DataSource
            Dim xs As String
            If C = "" Then xs = "ID" Else xs = "ID=" & C
            'If mCompanyTable.Select(xs).Length = 0 Then
            Dim dr As DataRow
            dr = mCompanyTable.NewRow
            .mCompanyID = C
            If .mCompanyID.ToString = "" Then .mCompanyID = DBNull.Value
            dr("ID") = .mCompanyID
            dr("Name") = M
            mCompanyTable.Rows.Add(dr)
            'End If

            If C = "" Then
                ._ID_Company.SelectedValue = DBNull.Value
            Else
                ._ID_Company.SelectedValue = C
            End If
            .UpdateServerVariables()
            .LogOn()

        End With
    End Sub

    Public Sub SychronizedTime()
        '        Dim currentTime As System.DateTime = System.DateTime.Now
        '        Dim year As Integer = currentTime.Year
        '        Dim month As Integer = currentTime.Month
        '        Dim day As Integer = currentTime.Day
        '        Dim hour As Integer = currentTime.Hour
        '        Dim minute As Integer = currentTime.Minute
        '        Dim secod As Integer = currentTime.Second

        '        Dim dt As DataTable = GSCOM.SQL.TableQuery("SELECT *FROM tLogDevice", gConnection)

        '        Dim a As New FSDevice.Device
        '        Dim s As String

        '        For Each dr As DataRow In dt.Rows
        '            a.IP = dr("IPAddress").ToString
        '            a.Port = 4370
        '            s = dr("IPAddress").ToString

        'ConnectAgain:
        '            If a.Connect() Then
        '                a.SetTime(minute, secod, hour, day, year, month)
        '                MsgBox("Sychronized Time" & vbCrLf & dr("IPAddress").ToString, vbOKOnly, "Successfully")

        '            Else
        '                Dim x As MsgBoxResult = MsgBox("Unable to Connect Device : " & dr("IPAddress").ToString, MsgBoxStyle.AbortRetryIgnore, "Connecting to device...")

        '                If x = MsgBoxResult.Retry Then
        '                    GoTo ConnectAgain
        '                ElseIf x = MsgBoxResult.Abort Then
        '                    Exit Sub
        '                ElseIf x = MsgBoxResult.Ignore Then
        '                    Continue For
        '                End If
        '            End If
        '        Next
    End Sub
    Public Sub ExportToExcel(ByVal pExcelTemplate As String, ByVal pFileName As String, ByVal pParameters() As String, ByVal pParametersVal() As String, ByVal pDataSource As DataSet, Optional ByVal IsPrintPreview As Boolean = False)
        CreateExcelDocument_V3(pExcelTemplate, pParameters, pParametersVal, pDataSource, IsPrintPreview, False, pFileName)
    End Sub
    Public Function EncryptPassword(ByVal pPassword As String) As String
        If pPassword.Length > 0 Then
            Dim s As String
            Dim RSA As New System.Security.Cryptography.RSACryptoServiceProvider
            Dim ByteConverter As New System.Text.UnicodeEncoding()

            Dim sb() As Byte = ByteConverter.GetBytes(pPassword)

            s = ""

            s = ByteConverter.GetString(RSAEncrypt(sb, RSA.ExportParameters(False), False)) & "_BJTGLR"
            Return s
        End If
        Return ""
    End Function
    Public Function DecryptPassword(ByVal pPassword As String) As String
        If pPassword.Length > 0 Then
            Dim s As String
            Dim RSA As New System.Security.Cryptography.RSACryptoServiceProvider
            Dim ByteConverter As New System.Text.UnicodeEncoding()
            Dim sb() As Byte = ByteConverter.GetBytes(pPassword)

            sb = RSADecrypt(sb, RSA.ExportParameters(True), False)
            s = ByteConverter.GetString(sb)
            Return s
        End If
        Return ""
    End Function

    Public Sub GenFolder(pth As String)
        If Not System.IO.Directory.Exists(pth) Then System.IO.Directory.CreateDirectory(pth)
    End Sub
End Module

'Private Function LastFilter() As GSCOM.UI.UIControls.FilterControl
'    Dim c As Control
'    Dim f As GSCOM.UI.UIControls.FilterControl
'    For Each c In LeftPanel.Controls
'        If TypeOf c Is GSCOM.UI.UIControls.FilterControl Then
'            f = c
'        End If
'    Next
'    LastFilter = f
'End Function
