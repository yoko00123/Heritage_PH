Option Explicit On
Option Strict Off

Imports System.Security.Cryptography
Imports System.IO
Imports InSysNetworkProfile
Imports System.Threading

Public Class LoginForm

#Region "Declarations"
    Friend mCompanyID As Object
    Event Inited As EventHandler
    Dim vPanelHeight As Integer
    Dim vFormHeight As Integer
    Dim curCompanyName As String = ""
    Private mServerControlsChanged As Boolean
    Private mUiChanged As Boolean = False
#End Region

#Region "New"

    Public Sub New()
        InitializeComponent()
        vPanelHeight = Panel1.Height
        vFormHeight = Me.Height
        '''''''''''''''''''''''''''''''''''''''''''''''''''GET ICON''''''''''''''''''''''''''''''''''''''''''''''''''
        If IO.File.Exists(Application.StartupPath + "\Icon.ico") Then Me.Icon = _
            Drawing.Icon.ExtractAssociatedIcon(Application.StartupPath + "\Icon.ico")
        '''''''''''''''''''''''''''''''''''''''''''''''''''CHECK IF Setting.ini exists'''''''''''''''''''''''''''''''
        ''''''''''''''''''''''''''''''''''''''''''''''USED FOR CLIENT SELECTOR'''''''''''''''''''''''''''''''''''''''
        If Not CustomSetting Is Nothing Then
            chkSelect.Checked = True
        End If
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    End Sub

#End Region

#Region "Events"

    Private Sub LoginForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Visible = False
        Me.ControlBox = False
        OK.Enabled = False
        Cancel.Enabled = False
        gLogInForm = Me

        Me.Text = Application.ProductName & " - Checking Connection"
        Application.DoEvents()
        gInit()

        Cursor = Cursors.WaitCursor
        'LogStatus("Checking Connection")

        Me.EnableControl(False)
        Dim thrd As New Thread(Sub() InitConnection(AddressOf Init)) 'CheckConnection(ServerBox.Text, DBBox.Text, DBUserNameBox.Text, DBPasswordBox.Text, AddressOf Init)
        thrd.IsBackground = True
        thrd.Start()

        Me.Visible = True
        'Me.HelpButton = True
    End Sub

    Public Sub ChangeUI()
        '-----------------20130416 -EMIL----------------------------------------|
        If Not lg.IsUserCancel And IsSharedContents Then
            lg.DName.Text = GSCOM.SQL.ExecuteScalar("SELECT dbo.fGetSetting('ServerName')", gConnection).ToString
            lg.UName.Text = GSCOM.SQL.ExecuteScalar("SELECT dbo.fGetSetting('UserName')", gConnection).ToString
            lg.PWord.Text = GSCOM.Common.EncryptA(GSCOM.SQL.ExecuteScalar("SELECT dbo.fGetSetting('Password')", gConnection).ToString, 41)

            lg.ReportPath.Text = GSCOM.SQL.ExecuteScalar("SELECT dbo.fGetSetting('ReportPath')", gConnection).ToString
            lg.ResourcesPath.Text = GSCOM.SQL.ExecuteScalar("SELECT dbo.fGetSetting('ResourcePath')", gConnection).ToString
            lg.PhotosPath.Text = GSCOM.SQL.ExecuteScalar("SELECT dbo.fGetSetting('PhotoPath')", gConnection).ToString
            lg.StyleSheetsPath.Text = GSCOM.SQL.ExecuteScalar("SELECT dbo.fGetSetting('StyleSheetPath')", gConnection).ToString
            lg.TemplatesPath.Text = GSCOM.SQL.ExecuteScalar("SELECT dbo.fGetSetting('ExcelTemplatePath')", gConnection).ToString
            lg.FilesPath.Text = GSCOM.SQL.ExecuteScalar("SELECT dbo.fGetSetting('FilePath')", gConnection).ToString

            lg.GO()

            If lg.Connected Then
                GSCOM.SQL.ExecuteNonQuery("EXEC dbo.pUpdateContentsPath '" & lg.DName.Text & "', '" & lg.UName.Text & "', '" & GSCOM.Common.EncryptA(lg.PWord.Text, 41) & "', '" & lg.ReportPath.Text & "', '" & _
                lg.ResourcesPath.Text & "', '" & lg.PhotosPath.Text & "', '" & lg.StyleSheetsPath.Text & "', '" & lg.TemplatesPath.Text & "', '" & lg.FilesPath.Text & "'", gConnection)
            End If
        End If
        '-----------------20130416 -EMIL-----------------------------------------

        If mUiChanged Then Exit Sub
        Dim loginBG As String = IO.Path.Combine(nDB.nGlobal.ResourcePath + "BG\", GSCOM.SQL.ExecuteScalar("Select dbo.fGetSetting('LoginBG')", gConnection).ToString)
        If IO.File.Exists(loginBG) Then
            Dim b As Bitmap
            b = New Bitmap(loginBG)
            Me.LogoPictureBox.Dock = DockStyle.Top
            Me.LogoPictureBox.Image = b
        Else
            Me.LogoPictureBox.Image = Nothing
        End If

        'COLORS'
        Me.BackColor = getColor("LoginBGColor")
        OK.BackColor = getColor("LoginBGColor")
        OK.ForeColor = getColor("LoginButtonForeColor")
        Cancel.BackColor = getColor("LoginBGColor")
        Cancel.ForeColor = getColor("LoginButtonForeColor")
        StatusStrip1.BackColor = getColor("LoginStatusBGColor")
        MainStatus.ForeColor = getColor("LoginStatusForeColor")
        ServerGroupBox.BackColor = getColor("LoginBGColor")
        GroupBox1.BackColor = getColor("LoginBGColor")
        GroupBox1.ForeColor = getColor("LoginForeColor")
        ServerGroupBox.ForeColor = getColor("LoginForeColor")
        Panel1.BackColor = getColor("LoginBGColor")

        Try
            Me.Text = GSCOM.SQL.ExecuteScalar("Select Name From tSystemVersion Where IsActive = 1", gConnection).ToString
        Catch
        End Try
        mUiChanged = True
        'COLORS'
    End Sub

    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        Cursor = Cursors.WaitCursor
        LogStatus("Connecting, please wait...")
        Application.DoEvents()

        Dim mserver As String = ServerBox.Text
        Dim mdb As String = DBBox.Text
        Dim muser As String = DBUserNameBox.Text
        Dim mpass As String = DBPasswordBox.Text

        Dim thrd As New Thread(Sub() CheckConnection(mserver, mdb, muser, mpass, AddressOf LoginGo))
        thrd.IsBackground = True
        thrd.Start()
    End Sub

    Delegate Sub dgLoginGo(result As Boolean)
    Sub LoginGo(result As Boolean)

        If Me.InvokeRequired Then
            Me.Invoke(New dgLoginGo(AddressOf LoginGo), result)
        Else
            Cursor = Cursors.Default
            If result Then

                Dim msg As Integer
                If IO.File.Exists(CONST_SERVERSETTINGFILE) = False Then
                    msg = MsgBox("Do you want to save new setting?", vbYesNo)
                    If msg = vbYes Then
                        CreateServerSettingFile(ServerBox.Text, DBBox.Text, DBUserNameBox.Text, DBPasswordBox.Text)
                    End If
                Else
                    If (ServerBox.Text + DBBox.Text + DBUserNameBox.Text + DBPasswordBox.Text <> gDBDataSource + gDBInitialCatalog + gDBUser + gDBPassword) Then
                        msg = MsgBox("Do you want to overwrite current setting?", vbYesNo)
                        If msg = vbYes Then
                            IO.File.Delete(CONST_SERVERSETTINGFILE)
                            CreateServerSettingFile(ServerBox.Text, DBBox.Text, DBUserNameBox.Text, DBPasswordBox.Text)
                        End If
                    End If
                End If

                UpdateServerVariables()
                InitConnection(Nothing)
                mServerControlsChanged = False
                ServerGroupBox.Visible = False
                Me.Height = 404

                Dim islicensingon As Object = GSCOM.SQL.ExecuteScalar("SELECT dbo.fGetSetting('Licensing')", gConnection)
                islicensingon = IIf(islicensingon.ToString = "1", True, False)

                If islicensingon Then
                    'hahahaha enable mo to para walang makalogin trolololololol
                    Dim pkey As Object = GSCOM.SQL.ExecuteScalar("SELECT dbo.fGetSetting('ProductKey')", gConnection)
                    If IsDBNull(pkey) Then
                        MsgBox("Invalid Product")
                        Exit Sub
                    ElseIf pkey.ToString.Trim = "" Then
                        MsgBox("Invalid Product")
                        Exit Sub
                    Else
                        'RegistrationForm.ValidateLicenseKey(CONST_ACTIVATIONFILE, pkey.ToString)
                        If Not ManualActivationForm.ac.VerifyRegistration(GSCOM.SQL.ExecuteScalar("SELECT dbo.fGetSetting('ActivationKey')", gConnection).ToString) Then
                            If ManualActivationForm.ShowDialog() = Windows.Forms.DialogResult.OK Then
                            Else
                                Exit Sub
                            End If
                        End If
                    End If


                End If

                If chkRemember.Checked Then
                    RememberMe(txtUserName.Text, txtPassword.Text)
                Else
                    If IO.File.Exists(CONST_REMEMBERMEFILE) Then
                        IO.File.Delete(CONST_REMEMBERMEFILE)
                    End If
                End If


                If chkSelect.Enabled Then
                    'CheckIfRetail()
                    LogOn()
                Else
                    'CheckIfRetail()
                    LogOn2()
                End If
            Else
                MsgBox("Connection failed.", vbCritical)
                ServerBox.Text = IIf(gDBDataSource = "", ServerBox.Text, gDBDataSource)
                DBBox.Text = IIf(gDBInitialCatalog = "", DBBox.Text, gDBInitialCatalog)
                DBUserNameBox.Text = IIf(gDBUser = "", DBUserNameBox.Text, gDBUser)
                DBPasswordBox.Text = IIf(gDBPassword = "", DBPasswordBox.Text, gDBPassword)
                MainStatus.Text = "Connection failed."
            End If

        End If
    End Sub

    Delegate Sub dgLogStatus(msg As String)
    Sub LogStatus(msg As String)
        If Me.InvokeRequired Then
            Me.Invoke(New dgLogStatus(AddressOf LogStatus), msg)
        Else
            Me.MainStatus.Text = msg
            Application.DoEvents()
        End If
    End Sub


    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub LoginForm_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        ReLog()
    End Sub

    Private Sub ServerSaveButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ServerSaveButton.Click
        Cursor = Cursors.WaitCursor
        LogStatus("Verifying connection, please wait...")
        Dim thrd As New Thread(Sub() BeginInvoke(New MethodInvoker(Sub() CheckConnection(ServerBox.Text, DBBox.Text, DBUserNameBox.Text, DBPasswordBox.Text, AddressOf ServerSaveInfo))))
        thrd.IsBackground = True
        thrd.Start()
    End Sub

    Delegate Sub dgServerSaveInfo(result As Boolean)
    Sub ServerSaveInfo(result As Boolean)
        If Me.InvokeRequired Then
            Me.Invoke(New dgServerSaveInfo(AddressOf ServerSaveInfo), result)
        Else
            Cursor = Cursors.Default
            If result = True Then
                MainStatus.Text = "Connection successful."
                CreateServerSettingFile(ServerBox.Text, DBBox.Text, DBUserNameBox.Text, DBPasswordBox.Text)
                UpdateServerVariables()
                InitConnection(Nothing)
                mServerControlsChanged = False

                CheckIfRetail()
            Else
                MsgBox("Connection failed.", vbCritical)
                ServerBox.Text = IIf(gDBDataSource = "", ServerBox.Text, gDBDataSource)
                DBBox.Text = IIf(gDBInitialCatalog = "", DBBox.Text, gDBInitialCatalog)
                DBUserNameBox.Text = IIf(gDBUser = "", DBUserNameBox.Text, gDBUser)
                DBPasswordBox.Text = IIf(gDBPassword = "", DBPasswordBox.Text, gDBPassword)
                MainStatus.Text = "Connection failed."
            End If
        End If
    End Sub

    Private Sub CheckConnection(ByVal pServer As String, ByVal pDatabase As String, ByVal pUserName As String, ByVal pPassword As String, action As Action(Of Boolean)) 'As Boolean

        Dim s As String
        s = "Password=" & pPassword & ""
        s &= ";Persist Security Info=True"
        s &= ";User ID=" & pUserName & ";"
        s &= "Initial Catalog=" & pDatabase & ";"
        s &= "Data Source=" & pServer
        Dim sql As New SqlClient.SqlConnection(s)
        Try
            sql.Open()
            sql.Close()
            LogStatus("Connected")
            If action IsNot Nothing Then action(True) 'Return True
        Catch ex As Exception
            LogStatus(ex.Message)
            If action IsNot Nothing Then action(False) 'Return False
        End Try

    End Sub

    Private Sub CheckIfRetail()

        Dim vInitServer As Boolean
        Dim mCompanyTable As New DataTable
        Dim dr As DataRow

        'mCompanyTable.PrimaryKey = New DataColumn() {mCompanyTable.Columns.Add("ID", GetType(Integer))}
        mCompanyTable.Columns.Add("ID", GetType(Integer))
        mCompanyTable.Columns.Add("Name", GetType(String))
        dr = mCompanyTable.NewRow

        'InitConnection()
        Dim s As String = "SELECT dbo.fGetSetting('SingleCompanyOnly')"
        If GSCOM.SQL.ExecuteScalar(s, gConnection) = 1 Then
            mCompanyID = GSCOM.SQL.ExecuteScalar("SELECT dbo.fGetSetting('DefaultCompanyID')", gConnection)
            dr("ID") = GSCOM.SQL.ExecuteScalar("SELECT dbo.fGetSetting('DefaultCompanyID')", gConnection)
            dr("Name") = GSCOM.SQL.ExecuteScalar("SELECT Name FROM dbo.tCompany WHERE ID = (SELECT dbo.fGetSetting('DefaultCompanyID'))", gConnection)
            chkSelect.Visible = False
            'chkRemember.Top = chkSelect.Top
        Else
            mCompanyID = System.Windows.Forms.Application.UserAppDataRegistry.GetValue("companyid", DBNull.Value) 'vCompanyID
            If mCompanyID.ToString = "" Then mCompanyID = DBNull.Value
            dr("ID") = mCompanyID
            dr("Name") = System.Windows.Forms.Application.UserAppDataRegistry.GetValue("company", "All Companies").ToString 'vCompany

        End If

        mCompanyTable.Rows.Add(dr)
        With _ID_Company
            .ValueMember = "ID"
            .DisplayMember = "Name"
            .DataSource = mCompanyTable
            .SelectedValue = mCompanyID
        End With
        InitServerControls(vInitServer)
        RaiseEvent Inited(Me, EventArgs.Empty)

    End Sub

#End Region

#Region "InitConnection"

    Private Sub InitConnection(action As Action(Of Boolean))
        Try
            Dim s As String
            s = "Password=" & gDBPassword & ""
            s &= ";Persist Security Info=True"
            s &= ";User ID=" & gDBUser & ";"
            s &= "Initial Catalog=" & gDBInitialCatalog & ";"
            s &= "Data Source=" & gDBDataSource
            nDB = New Database.UserSession(New SqlClient.SqlConnection(s))
            GSCOM.Applications.InSys.Html.nDB = nDB

            LogStatus("Connected")
            If action IsNot Nothing Then action(True)
        Catch ex As Exception
            LogStatus(ex.Message)
            If action IsNot Nothing Then action(False)
        End Try
    End Sub

#End Region

#Region "Init"

    Friend Function gInit() As Boolean

        If System.IO.File.Exists(gIconFile) Then
            gIcon = New Icon(gIconFile)
        End If

        If Not IO.File.Exists(CONST_SERVERSETTINGFILE) Then
            'Return False

            Using iss As New InitialServerSettingForm(CONST_SERVERSETTINGFILE)
                Dim k As DialogResult = iss.ShowDialog()
                If k = Windows.Forms.DialogResult.OK Then
                    GetUserNameAndPassword(gDBDataSource, gDBInitialCatalog, gDBUser, gDBPassword)
                    Return True
                ElseIf k = Windows.Forms.DialogResult.Cancel Then
                    End ' lets end this
                End If
            End Using
        Else
            GetUserNameAndPassword(gDBDataSource, gDBInitialCatalog, gDBUser, gDBPassword)
            Return True
        End If

    End Function

    Public Sub GetUserNameAndPassword(ByRef pServer As String, ByRef pDatabase As String, ByRef pUserName As String, ByRef pPassword As String)
        Try
            Dim ByteConverter As New System.Text.UnicodeEncoding()
            Dim s As String
            'Dim encryptedData() As Byte
            'Dim decryptedData() As Byte
            Dim encryptedData As String
            Dim decryptedData As String
            Dim RSA As New System.Security.Cryptography.RSACryptoServiceProvider
            Dim xmlKey As String
            Dim fn As Integer = FreeFile()
            xmlKey = My.Resources.RSAKeyValue
            'ReDim encryptedData(CInt(Microsoft.VisualBasic.FileLen(CONST_SERVERSETTINGFILE)) - 1)
            RSA.FromXmlString(xmlKey)
            'FileOpen(fn, CONST_SERVERSETTINGFILE, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)
            'FileGetObject(fn, encryptedData)
            'FileClose(fn)
            'FileOpen(fn, CONST_SERVERSETTINGFILE, OpenMode.Input, OpenAccess.Read, OpenShare.Shared)
            'FileGetObject(fn, encryptedData)
            'FileClose(fn)
            encryptedData = IO.File.ReadAllText(CONST_SERVERSETTINGFILE)
            decryptedData = GSCOM.Common.EncryptA(encryptedData, 41)

            'encryptedData = IO.File.ReadAllBytes(CONST_SERVERSETTINGFILE)
            'decryptedData = RSADecrypt(encryptedData, RSA.ExportParameters(True), False)
            s = decryptedData 'ByteConverter.GetString(decryptedData)
            Dim sa As String()
            sa = s.Split(CChar(ChrW(0)))
            'if row = 0 then username, row = 1 then password
            pServer = sa(0)
            pDatabase = sa(1)
            pUserName = sa(2)
            pPassword = sa(3)
        Catch ex As Exception
            'Catch this exception in case the encryption did
            'not succeed.
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Public Sub GetLoginInfo(ByRef pUsername As String, ByRef pPassword As String)
        Try
            Dim ByteConverter As New System.Text.UnicodeEncoding()
            Dim s As String
            Dim encryptedData() As Byte
            Dim decryptedData() As Byte
            Dim RSA As New System.Security.Cryptography.RSACryptoServiceProvider
            Dim xmlKey As String
            Dim fn As Integer = FreeFile()
            xmlKey = My.Resources.RSAKeyValue
            ReDim encryptedData(CInt(Microsoft.VisualBasic.FileLen(CONST_REMEMBERMEFILE)) - 1)
            RSA.FromXmlString(xmlKey)
            FileOpen(fn, CONST_REMEMBERMEFILE, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)
            FileGetObject(fn, encryptedData)
            FileClose(fn)
            decryptedData = RSADecrypt(encryptedData, RSA.ExportParameters(True), False)
            s = ByteConverter.GetString(decryptedData)
            Dim sa As String()
            sa = s.Split(CChar(ChrW(0)))
            'if row = 0 then username, row = 1 then password
            pUsername = sa(0)
            pPassword = sa(1)
        Catch ex As Exception
            'Catch this exception in case the encryption did
            'not succeed.
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Delegate Sub dgInit(result As Boolean)
    Private Sub Init(result As Boolean)
        Try

            If Me.InvokeRequired Then
                Me.Invoke(New dgInit(AddressOf Init), result)
            Else
                Me.Text = Application.ProductName
                Me.EnableControl(True)
                Cursor = Cursors.Default
                If Not result Then Throw New Exception("Connection Failed")

                'InitConnection()
                'CheckConnection(ServerBox.Text, DBBox.Text, DBUserNameBox.Text, DBPasswordBox.Text, Nothing)

                If IO.File.Exists(CONST_REMEMBERMEFILE) Then
                    GetLoginInfo(txtUserName.Text, txtPassword.Text)
                    chkRemember.Checked = True
                End If
                If Command.Trim <> "" Then
                    gIniFile = Command.Trim
                End If

                Dim mCompanyTable As New DataTable
                Dim vInitServer As Boolean
                Me.Text = Application.ProductName
                vInitServer = gInit()
                Me.Icon = gIcon
                txtUserName.Text = System.Windows.Forms.Application.UserAppDataRegistry.GetValue("User", "").ToString

                Dim dr As DataRow

                If IO.File.Exists(CONST_SERVERSETTINGFILE) Then

                    'nDB.Connection = New SqlClient.SqlConnection(s) LJ load to initializer
                    'CHANGE UI-BACKGROUND IMAGE'
                    ChangeUI()

                    'mCompanyTable.PrimaryKey = New DataColumn() {mCompanyTable.Columns.Add("ID", GetType(Integer))}
                    mCompanyTable.Columns.Add("ID", GetType(Integer))
                    mCompanyTable.Columns.Add("Name", GetType(String))
                    dr = mCompanyTable.NewRow

                    Dim s As String = "SELECT dbo.fGetSetting('SingleCompanyOnly')"
                    If GSCOM.SQL.ExecuteScalar(s, gConnection) = 1 Then
                        mCompanyID = GSCOM.SQL.ExecuteScalar("SELECT dbo.fGetSetting('DefaultCompanyID')", gConnection)
                        dr("ID") = GSCOM.SQL.ExecuteScalar("SELECT dbo.fGetSetting('DefaultCompanyID')", gConnection)
                        dr("Name") = GSCOM.SQL.ExecuteScalar("SELECT Name FROM dbo.tCompany WHERE ID = (SELECT dbo.fGetSetting('DefaultCompanyID'))", gConnection)
                        chkSelect.Visible = False
                        'chkRemember.Top = chkSelect.Top
                        Me.ServerSettingButton.Enabled = False
                    Else
                        mCompanyID = System.Windows.Forms.Application.UserAppDataRegistry.GetValue("companyid", DBNull.Value) 'vCompanyID
                        If mCompanyID.ToString = "" Then mCompanyID = DBNull.Value
                        dr("ID") = mCompanyID
                        dr("Name") = System.Windows.Forms.Application.UserAppDataRegistry.GetValue("company", "All Companies").ToString 'vCompany
                        Me.ServerSettingButton.Enabled = True
                    End If

                    'Dim dr As DataRow

                    'mCompanyTable.PrimaryKey = New DataColumn() {mCompanyTable.Columns.Add("ID", GetType(Integer))}
                    'mCompanyTable.Columns.Add("ID", GetType(Integer))
                    'mCompanyTable.Columns.Add("Name", GetType(String))
                    dr = mCompanyTable.NewRow

                    mCompanyID = System.Windows.Forms.Application.UserAppDataRegistry.GetValue("companyid", DBNull.Value) 'vCompanyID
                    If mCompanyID.ToString = "" Then mCompanyID = DBNull.Value
                    dr("ID") = mCompanyID
                    dr("Name") = System.Windows.Forms.Application.UserAppDataRegistry.GetValue("company", "All Companies").ToString 'vCompany



                    mCompanyTable.Rows.Add(dr)
                    If mCompanyTable.Rows.Count > 0 Then
                        curCompanyName = mCompanyTable.Rows(0)("Name")
                    End If
                    With _ID_Company
                        .ValueMember = "ID"
                        .DisplayMember = "Name"
                        .DataSource = mCompanyTable
                        .SelectedValue = mCompanyID
                    End With
                    'InitConnection()
                    InitServerControls(vInitServer)
                    RaiseEvent Inited(Me, EventArgs.Empty)
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

#End Region

#Region "LogOn2"
    Private Sub LogOn2()
        Panel1.Enabled = False
        'Me.ControlBox = False
        'Me.ShowIcon = True
        'gCompanyName = _ID_Company.Text
        Windows.Forms.Application.UserAppDataRegistry.SetValue("user", txtUserName.Text)
        Windows.Forms.Application.UserAppDataRegistry.SetValue("companyid", _ID_Company.SelectedValue)
        Windows.Forms.Application.UserAppDataRegistry.SetValue("company", _ID_Company.Text)
        Windows.Forms.Application.UserAppDataRegistry.SetValue("directorypath", My.Application.Info.DirectoryPath)

        nDB.StartSession(gUser, _ID_Company.SelectedValue, gEmployee)
        gRefreshSettings()
        With MainProgressBar
            .Maximum = 6
            .Value = 0
            .Style = ProgressBarStyle.Continuous
        End With
        gMainForm = New MainForm
        Me.DialogResult = Windows.Forms.DialogResult.OK

    End Sub
#End Region

#Region "LogOn"

    Public Sub UpdateServerVariables()
        gDBDataSource = ServerBox.Text
        gDBInitialCatalog = DBBox.Text
        gDBUser = DBUserNameBox.Text
        gDBPassword = DBPasswordBox.Text
    End Sub

    Friend Sub LogOn()
        Dim dt As DataTable
        Dim bstr As String
        Dim bbool As Boolean
        Dim fpstr As String
        Dim fpbool As Boolean
        Dim ilstr As String
        Dim ilbool As Boolean
        Dim pestr As String
        Dim pecon As String
        Try

            If ServerGroupBox.Visible Then
                UpdateServerVariables()
                InitConnection(Nothing)
                mServerControlsChanged = False
            Else
                InitConnection(Nothing)
            End If
            Dim s As String
            s = "SELECT isnull(dbo.fGetSetting('IsEncrypted'),0)"
            If GSCOM.SQL.ExecuteScalar(s, gConnection) IsNot DBNull.Value Then
                If CBool(GSCOM.SQL.ExecuteScalar(s, gConnection)) = 0 Then
                    Dim udt As DataTable = GSCOM.SQL.TableQuery("SELECT ID,Password FROM tUSER where Password is not NULL", gConnection)
                    For Each udr As DataRow In udt.Rows
                        GSCOM.SQL.ExecuteNonQuery("UPDATE tUser SET Password = '" & GSCOM.Common.EncryptA(udr("Password").ToString, 41) & "_BJTGLR" & "' WHERE ID = " & udr("ID").ToString, gConnection)
                    Next
                    GSCOM.SQL.ExecuteNonQuery("UPDATE tSetting SET Value = 1 WHERE Name = 'IsEncrypted'", gConnection)
                End If
            End If
            If txtUserName.Text.Trim = "" Then
                MsgBox("Please enter username", MsgBoxStyle.Information)
                txtUserName.Focus()
                Exit Sub
            End If

            MainStatus.Text = "Validating"
            Application.DoEvents()
            Dim vUserName As String
            Dim vPassword As Object = DBNull.Value
            vUserName = txtUserName.Text
            If (txtPassword.Text <> "") Then
                vPassword = GSCOM.Common.EncryptA(txtPassword.Text, 41) & "_BJTGLR"
            End If
            Dim textPassword As String
            If txtPassword.Text = "" Then
                textPassword = "NULL"
            Else
                textPassword = txtPassword.Text
            End If
            dt = Database.fUser(vUserName, vPassword, gConnection)
            MainStatus.Text = ""
            Application.DoEvents()
            If dt.Rows.Count = 0 Then
                MsgBox("Invalid", MsgBoxStyle.Exclamation)
                GSCOM.SQL.ExecuteNonQuery("EXEC pInvalidLogCountApply " & GSCOM.SQL.SQLFormat(txtUserName.Text), gConnection)

                GSCOM.SQL.ExecuteNonQuery("EXEC pFailedLogAttempt " & GSCOM.SQL.SQLFormat(txtUserName.Text) & "," & GSCOM.SQL.SQLFormat(textPassword) & ",'Invalid'", gConnection)
            Else
                bstr = "SELECT dbo.fUserisBlocked('" & txtUserName.Text & "')"
                bbool = CBool(GSCOM.SQL.ExecuteScalar(bstr, gConnection))
                If bbool Then
                    MsgBox("User is blocked, Please contact Admin to unblocked", MsgBoxStyle.Exclamation)
                    GSCOM.SQL.ExecuteNonQuery("EXEC pFailedLogAttempt " & txtUserName.Text & "," & textPassword.ToString & ",'Blocked'", gConnection)

                    Exit Sub
                End If
                ilstr = "SELECT dbo.fForceChangePassword_FirstLog('" & txtUserName.Text & "')"
                ilbool = CBool(GSCOM.SQL.ExecuteScalar(ilstr, gConnection))
                If ilbool Then
                    MsgBox("This is your initial log, Please Change your password to log in", MsgBoxStyle.Exclamation)
                    Dim f As New ChangePasswordForm
                    f.initlogbool = True
                    f.uID = CStr(dt.Rows(0).Item(Database.Tables.tUser.Field.ID.ToString))
                    f.ShowDialog()

                    Exit Sub
                End If
                fpstr = "SELECT dbo.fForceChangePassword_Expired('" & txtUserName.Text & "')"
                fpbool = CBool(GSCOM.SQL.ExecuteScalar(fpstr, gConnection))
                pestr = "SELECT dbo.fGetSetting('PasswordExpirationDays')"
                pecon = CStr(GSCOM.SQL.ExecuteScalar(pestr, gConnection))
                If fpbool Then
                    MsgBox("Your password meets " & pecon.ToString & " days expiration, Please Change Your Password to log in", MsgBoxStyle.Exclamation)
                    Dim f As New ChangePasswordForm
                    f.passexpiredbool = True
                    f.uID = CStr(dt.Rows(0).Item(Database.Tables.tUser.Field.ID.ToString))
                    f.ShowDialog()

                    Exit Sub
                End If

                Dim p As String
                Dim mCompanyTable As New DataTable
                mCompanyTable.Columns.Add("ID", GetType(Integer))
                mCompanyTable.Columns.Add("Name", GetType(String))

                p = dt.Rows(0).Item(Database.Tables.tUser.Field.Password.ToString).ToString
                If p <> "" Then
                    p = Strings.Left(p, p.Length - 7)
                End If
                p = GSCOM.Common.EncryptA(p, 41).ToString

                If String.Compare(txtPassword.Text, p, False) = 0 Then
                    GSCOM.SQL.ExecuteNonQuery("EXEC pResetInvalidLogCount " & CInt(dt.Rows(0).Item(Database.Tables.tUser.Field.ID.ToString)), gConnection)
                    gUser = CInt(dt.Rows(0).Item(Database.Tables.tUser.Field.ID.ToString))
                    gEmployee = dt.Rows(0).Item("ID_Employee")
                    Dim dr As DataRow
                    If IsDBNull(gEmployee) Then
                        mCompanyTable = Database.LogInCompanyTable(gUser, gConnection)
                        dr = mCompanyTable.NewRow
                        dr("ID") = DBNull.Value
                        dr("Name") = "All Companies"
                    Else
                        dr = mCompanyTable.NewRow
                        dr("ID") = dt.Rows(0).Item("ID_Company")
                        dr("Name") = dt.Rows(0).Item("Company")
                        mCompanyID = dr("ID")
                    End If
                    mCompanyTable.Rows.Add(dr)

                    mCompanyTable.DefaultView.Sort = "Name"

                    Dim dra As DataRow()
                    dra = mCompanyTable.Select("ID=" & GSCOM.SQL.SQLFormat(mCompanyID))
                    If dra.Length = 0 Then
                        mCompanyID = DBNull.Value
                    End If

                    With _ID_Company
                        .ValueMember = "ID"
                        .DisplayMember = "Name"
                        .DataSource = mCompanyTable
                        .SelectedValue = mCompanyID
                    End With

                    'BRYAN:20130218
                    Try
                        ID_Company_ = If(IsDBNull(mCompanyID), 0, mCompanyID)
                    Catch
                        ID_Company_ = 0
                    End Try


                    ' ID_Company_ = _ID_Company.SelectedIndex 'EMIL:20130216

                    If chkSelect.Checked And chkSelect.Enabled = True Then
                        txtPassword.Enabled = False
                        txtUserName.Enabled = False
                        chkSelect.Enabled = False
                        _ID_Company.Enabled = True
                        _ID_Company.DroppedDown = True
                        _ID_Company.Focus()
                    Else
                        LogOn2()
                    End If
                Else
                    MsgBox("Invalid", MsgBoxStyle.Exclamation)
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

#End Region

#Region "ShowProgress"

    Delegate Sub dgShowProgress(pText As String)
    Friend Sub ShowProgress(Optional ByVal pText As String = "")
        If Me.InvokeRequired Then
            Me.Invoke(New dgShowProgress(AddressOf ShowProgress), pText)
        Else
            If MainProgressBar.Value < MainProgressBar.Maximum Then
                MainProgressBar.Value += 1
                If pText <> "" Then
                    MainStatus.Text = pText
                End If
            End If
            Application.DoEvents()
        End If
    End Sub

#End Region

#Region "UpdateServerControls"

    Private Sub UpdateServerControls()
        ServerBox.Text = gDBDataSource
        DBBox.Text = gDBInitialCatalog
        DBUserNameBox.Text = gDBUser
        DBPasswordBox.Text = gDBPassword
    End Sub

#End Region

    Friend Sub RememberMe(pUserName As String, pPassword As String)
        Try
            Dim s As String
            Dim encryptedData() As Byte
            Dim ByteConverter As New System.Text.UnicodeEncoding()
            Dim RSA As New System.Security.Cryptography.RSACryptoServiceProvider
            Dim xmlKey As String
            Dim fn As Integer = FreeFile()

            s = pUserName
            s &= CChar(ChrW(0)) & pPassword

            Dim dataToEncrypt As Byte() = ByteConverter.GetBytes(s)

            xmlKey = My.Resources.RSAKeyValue
            RSA.FromXmlString(xmlKey)

            encryptedData = RSAEncrypt(dataToEncrypt, RSA.ExportParameters(False), False)

            FileOpen(fn, CONST_REMEMBERMEFILE, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)
            FilePutObject(fn, encryptedData)
            FileSystem.FileClose(fn)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub InitServerControls(ByVal pInitServer As Boolean)
        If pInitServer Then
            ServerGroupBox.Visible = False
            ServerSaveButton.Image = GSCOM.UI.SaveButtonImage
            Me.Height = 404
            UpdateServerControls()
        Else
            ServerSettingButton.Enabled = False
        End If
        AddHandler ServerBox.TextChanged, AddressOf ServerControlsChanged
        AddHandler DBBox.TextChanged, AddressOf ServerControlsChanged
        AddHandler DBUserNameBox.TextChanged, AddressOf ServerControlsChanged
        AddHandler DBPasswordBox.TextChanged, AddressOf ServerControlsChanged

    End Sub

    Private Sub ServerControlsChanged(ByVal sender As Object, ByVal e As EventArgs)
        mServerControlsChanged = True
    End Sub

    Public Sub ReLog()
        OK.Enabled = True
        Cancel.Enabled = True
        Panel1.Enabled = True
        Panel2.Enabled = True
        MainStatus.Text = ""
        MainProgressBar.Value = 0
        Me.Show()
        Application.DoEvents()
        If gLogOffOnly Then
            txtUserName.SelectAll()
            txtUserName.Focus()
        Else
            If txtUserName.Text = "" Then
                txtUserName.Focus()
            Else
                txtPassword.Focus()
            End If
        End If
    End Sub

    Private Sub ServerSettingButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ServerSettingButton.Click
        If vPanelHeight = 0 Then
            vPanelHeight = Panel1.Height
            vFormHeight = Me.Height
        End If

        If Not ServerGroupBox.Visible Then
            Panel1.Height = vPanelHeight
            Me.Height = vFormHeight
            ServerGroupBox.Visible = True

            UpdateServerControls()
        Else
            ServerGroupBox.Visible = False
            If mServerControlsChanged Then
                If MsgBox("Hiding the server setting will undo your recent changes, Are you sure you want to hide the server settings?", MsgBoxStyle.YesNoCancel) <> vbYes Then
                    Exit Sub
                End If
                mServerControlsChanged = False
            End If
            Me.Height = 404

        End If
    End Sub

    Sub EnableControl(Optional enable As Boolean = True)
        For Each c As Control In Me.Controls
            c.Enabled = enable
        Next
    End Sub

    Private Sub DBBox_DropDown(sender As Object, e As EventArgs) Handles DBBox.DropDown
        DBBox.Items.Clear()
        DBBox.Items.Add("<Fetching Database>")
        ServerGroupBox.Enabled = False
        Tasks.Task.Factory.StartNew(Sub() LoadDropdown(ServerBox.Text, DBUserNameBox.Text, DBPasswordBox.Text))
    End Sub

    Sub LoadDropdown(server As String, user As String, pass As String)
        Try
            Dim conargs As String = String.Format("Data Source={0};User ID={1};Password={2};Persist Security Info=True;", server, user, pass)
            Using con As New SqlClient.SqlConnection(conargs)
                Using dt As DataTable = GSCOM.SQL.TableQuery("SELECT name FROM sys.databases WHERE database_id > 6 ORDER BY name", con)
                    BeginInvoke(New MethodInvoker(Sub() loadDropItems(dt)))
                End Using
            End Using
        Catch ex As Exception
            BeginInvoke(New MethodInvoker(Sub() DBBox.Items.Clear()))
            MsgBox(ex.Message)
        Finally
            BeginInvoke(New MethodInvoker(Sub() ServerGroupBox.Enabled = True))
        End Try
    End Sub

    Sub loadDropItems(dt As DataTable)
        DBBox.Items.Clear()
        For Each c As DataRow In dt.Rows
            DBBox.Items.Add(c("name").ToString())
        Next
    End Sub


End Class

#Region "Comments"

' TODO: Insert code to perform custom authentication using the provided username and password 
' (See http://go.microsoft.com/fwlink/?LinkId=35339).  
' The custom principal can then be attached to the current thread's principal as follows: 
'     My.User.CurrentPrincipal = CustomPrincipal
' where CustomPrincipal is the IPrincipal implementation used to perform authentication. 
' Subsequently, My.User will return identity information encapsulated in the CustomPrincipal object
' such as the username, display name, etc.

#End Region
