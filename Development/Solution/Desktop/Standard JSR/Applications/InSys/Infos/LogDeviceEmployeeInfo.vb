'Option Explicit On
'Option Strict Off

'Imports nDB = GSCOM.Applications.InSys.Database

'Friend Class LogDeviceEmployeeInfo
'    Inherits InfoSet

'    Private myDT As New nDB.Tables.tEmployee(Connection)
'    Private mControl As New InSys.DataControl
'    Private mtEmployeeLogDevice As GSCOM.SQL.ZDataTable
'    Private mtEmployeeFingerPrint As GSCOM.SQL.ZDataTable
'    Private mtLogDevice As DataTable
'    Private mWeekDayTable As DataTable = GSCOM.SQL.TableQuery("SELECT ID,Name FROM vLogDevice_List", Connection)
'    Private mTVWeekDay As New LogDeviceSelector



'    Public Sub New(ByVal pMenu As Integer, ByVal c As SqlClient.SqlConnection, ByVal pListing As DataTable, ByVal pID As Integer)
'        MyBase.New(c, pListing, pID)
'        With mDataset.Tables
'            .Add(Table)
'        End With
'        InitControl(pMenu)
'        mtEmployeeLogDevice = Me.mDataset.Tables("tEmployeeLogDevice")
'        mtEmployeeFingerPrint = Me.mDataset.Tables("tEmployeeFingerPrint")
'        mtLogDevice = GSCOM.SQL.TableQuery("select * from tlogdevice", c)
'        With mTVWeekDay
'            .DataSource = mWeekDayTable
'            .Go()
'        End With
'        'Me.AddControl(mTVWeekDay, "Log Device")

'        Me.AddButton("Enroll Fingerprint", gMainForm.imgList.Images("_logdevice.png"), AddressOf Enrollment)
'        Me.AddButton("Enroll Card", gMainForm.imgList.Images("_logdevice.png"), AddressOf EnrollCard)
'        Me.AddButton("Update Fingerprint Data to Device", gMainForm.imgList.Images("_logdevice.png"), AddressOf Update)


'        Dim tb As TextBox
'        tb = CType(Me.GetControl("_LogPassword"), TextBox)
'        tb.UseSystemPasswordChar = True



'        AfterNew()

'        'Me.GetControl("_AccessNo").Enabled = False

'        Try
'            Dim mGrid As DataGridView
'            mGrid = Me.GetDataGridView(mtEmployeeFingerPrint)
'            mGrid.ReadOnly = True
'            mGrid.AllowUserToAddRows = False
'            mGrid.AllowUserToDeleteRows = False
'        Catch ex As Exception

'        End Try

'    End Sub

'    Private Sub EnrollCard(ByVal sender As Object, ByVal e As EventArgs)

'        Dim curaccessnum As String = myDT.Get(Database.Tables.tEmployee.Field.AccessNo).ToString
'        If curaccessnum = "" Or IsDBNull(curaccessnum) Then
'            MsgBox("Please input the accesss number first", MsgBoxStyle.OkOnly, Me.Text)
'            Exit Sub
'        End If
'        Dim b As New fsEnrollCard(myDT, mtLogDevice, curaccessnum)
'        b.Port = 4370
'        'b.ComPort = Database.SettingEnum.ComPort
'        'b.BaudRate = Database.SettingEnum.BaudRate
'        'b.UseComPort = Database.SettingEnum.UseComPort
'        b.ShowDialog()
'    End Sub

'    Private Sub Enrollment(ByVal sender As Object, ByVal e As EventArgs)
'        Dim curaccessnum As String = myDT.Get(Database.Tables.tEmployee.Field.AccessNo).ToString
'        If curaccessnum = "" Or IsDBNull(curaccessnum) Then
'            MsgBox("Please input the accesss number first", MsgBoxStyle.OkOnly, Me.Text)
'            Exit Sub
'        End If
'        Dim a As New FSEnrollForm(myDT, mtEmployeeFingerPrint, mtLogDevice, curaccessnum)
'        'a.IP = Me.EnrollDeviceIP
'        a.Port = 4370
'        ''20090904-------------------------------------------------------------\
'        'a.ComPort = Database.SettingEnum.ComPort
'        'a.BaudRate = Database.SettingEnum.BaudRate
'        'a.UseComPort = Database.SettingEnum.UseComPort
'        ''20090904-------------------------------------------------------------/
'        a.ShowDialog()
'    End Sub

'    'Private Sub Download(ByVal sender As Object, ByVal e As EventArgs)
'    '    Me.BeginProcess("Downloading")
'    '    GoDownload()
'    'End Sub

'    Private mEnrollDeviceIP As String
'    'Private ReadOnly Property EnrollDeviceIP() As String

'    '    Get

'    '        Dim s As String = ""
'    '        Dim vDef As String
'    '        vDef = nDB.GetSetting(Database.SettingEnum.LogDevice)

'    '        If mEnrollDeviceIP = "" Then
'    '            's = InputBox("Enter enroll device IP address", "Device IP Address Setup", vDef)
'    '            If s = "" Then
'    '                mEnrollDeviceIP = vDef
'    '            Else
'    '                mEnrollDeviceIP = s
'    '            End If
'    '        End If
'    '        Return mEnrollDeviceIP
'    '    End Get
'    'End Property
'    'Private Sub GoDownload()
'    '    Dim a As New FSDevice.Device
'    '    Dim s As String
'    '    'Dim dra As DataRow()
'    '    a.IP = Me.EnrollDeviceIP '"192.168.0.207" 'dr("IPAddress")
'    '    a.Port = 4370
'    '    Try
'    '        If My.Computer.Network.Ping(a.IP) Then
'    '            If a.Connect Then
'    '                Dim pAccessNo As String
'    '                pAccessNo = InputBox("Enter enroll no.", Me.Text, myDT.Get(Database.Tables.tEmployee.Field.AccessNo).ToString)
'    '                If IsNumeric(pAccessNo) Then
'    '                    pAccessNo = CInt(pAccessNo)
'    '                    Dim u As GSCOM.FSDevice.UserTable
'    '                    u = a.GetUser(pAccessNo)
'    '                    If u.Rows.Count > 0 Then
'    '                        For i As Integer = 1 To 10
'    '                            s = "Processing " & "F" & i.ToString
'    '                            Me.BeginProcess(s)
'    '                            mtEmployeeFingerPrint.Rows(0)("F" & i.ToString) = u.Rows(0).Item("F" & (i - 1).ToString)
'    '                        Next
'    '                        Me.EndProcess("Done")
'    '                    Else
'    '                        Me.EndProcess("User with enroll no " & pAccessNo & " is not found", False)
'    '                    End If
'    '                Else
'    '                    Me.EndProcess("Downloading is canceled", False)
'    '                End If
'    '            Else
'    '                Me.EndProcess("Can not connect to device " & " (" & a.IP & ")", False)
'    '            End If
'    '        Else
'    '            Me.EndProcess("Can not connect to device " & " (" & a.IP & ")", False)
'    '        End If

'    '    Catch ex As Exception
'    '        Me.EndProcess("Can not connect to device " & " (" & a.IP & ")", False)
'    '    End Try


'    'End Sub
'    'Private Sub GoDownload()
'    '    Dim a As New FSDevice.Device
'    '    Dim s As String
'    '    'Dim dra As DataRow()
'    '    a.IP = Me.EnrollDeviceIP '"192.168.0.207" 'dr("IPAddress")
'    '    a.Port = 4370
'    '    If a.Connect Then
'    '        Dim pAccessNo As String
'    '        pAccessNo = InputBox("Enter enroll no.", Me.Text, myDT.Get(Database.Tables.tEmployee.Field.AccessNo).ToString)
'    '        If IsNumeric(pAccessNo) Then
'    '            pAccessNo = CInt(pAccessNo)
'    '            Dim u As GSCOM.FSDevice.UserTable
'    '            u = a.GetUser(pAccessNo)
'    '            If u.Rows.Count > 0 Then
'    '                For i As Integer = 1 To 10
'    '                    s = "Processing " & "F" & i.ToString
'    '                    Me.BeginProcess(s)
'    '                    mtEmployeeFingerPrint.Rows(0)("F" & i.ToString) = u.Rows(0).Item("F" & (i - 1).ToString)
'    '                Next
'    '                Me.EndProcess("Done")
'    '            Else
'    '                Me.EndProcess("User with enroll no " & pAccessNo & " is not found", False)
'    '            End If
'    '        Else
'    '            Me.EndProcess("Downloading is canceled", False)
'    '        End If
'    '    Else
'    '        Me.EndProcess("Can not connect to device " & " (" & a.IP & ")", False)
'    '    End If
'    'End Sub

'    Private Sub Update(ByVal sender As Object, ByVal e As EventArgs)
'        Me.BeginProcess("Updating")
'        Dim AccNo As String
'        AccNo = myDT.Get(Database.Tables.tEmployee.Field.AccessNo).ToString
'        If AccNo = "" Or IsDBNull(AccNo) Then
'            MsgBox("Please input the accesss number first", MsgBoxStyle.OkOnly, Me.Text)
'            Exit Sub
'        End If
'        Upload()
'        DeleteFromOtherDevice()
'    End Sub
'    Private Sub Upload()
'        Dim n, c, p, pr, AccNo As String
'        Dim s As String
'        Dim vDeviceRows As DataRow()
'        Dim dwEnrollNumber As Integer
'        Dim dwBackupNumber As Integer = 11
'        AccNo = myDT.Get(Database.Tables.tEmployee.Field.AccessNo).ToString

'        If IsDBNull(AccNo) Or AccNo = "" Then
'            MsgBox("No AccessNo, cannot upload data to device")
'            EndProcess("Done")
'            Exit Sub
'        Else
'            dwEnrollNumber = CInt(AccNo)
'        End If
'        'dwEnrollNumber = myDT.Get(Database.Tables.tEmployee.Field.AccessNo)
'        vDeviceRows = mtEmployeeLogDevice.Select()
'        n = myDT.Get(Database.Tables.tEmployee.Field.Code).ToString
'        c = myDT.Get(Database.Tables.tEmployee.Field.CardNo).ToString
'        pr = myDT.Get(Database.Tables.tEmployee.Field.ID_DevicePrivilege).ToString
'        If c = "" Then
'            c = 0
'        End If
'        p = myDT.Get(Database.Tables.tEmployee.Field.LogPassword).ToString
'        If vDeviceRows.Length = 0 Then
'            EndProcess("No device selected", False)
'        Else
'            For Each dr As DataRow In vDeviceRows
'                Dim a As New FSDevice.Device
'                Dim dra As DataRow()
'                'Dim con As String
'                'Dim ipadd As Object

'                'con = "SELECT IPAddress FROM tLogDevice e WHERE ID = " & dr("ID_LogDevice").ToString
'                'ipadd = GSCOM.SQL.ExecuteScalar(con, gConnection).ToString
'                a.IP = dr("IPAddress")
'                a.Port = 4370
'                Try
'                    If My.Computer.Network.Ping(a.IP) Then
'                        If a.Connect Then
'                            DevPlat = a.GetPlatform
'                            Dim u As FSDevice.UserTable

'                            If DevPlat.ToUpper.Contains("TFT") Then
'                                u = a.GetUser(dwEnrollNumber)
'                            Else
'                                u = a.GetUser2(dwEnrollNumber)
'                            End If

'                            'If DevPlat = ZEM500 Then
'                            '    u = a.GetUser2(dwEnrollNumber)
'                            'Else
'                            '    u = a.GetUser(dwEnrollNumber)
'                            'End If
'                            'u = a.GetUser(dwEnrollNumber)
'                            dra = u.Select
'                            If dra.Length > 0 Then
'                                a.DeleteAllFingerPrint(dwEnrollNumber, dwBackupNumber)
'                            End If

'                            If DevPlat.ToUpper.Contains("TFT") Then
'                                a.SetUserInfo(dwEnrollNumber, n, p, CInt(pr) - 1, True)
'                            Else
'                                a.SetUserInfoCard(dwEnrollNumber.ToString, n, p, CInt(pr) - 1, True, c)
'                            End If

'                            'If DevPlat = ZEM500 Then
'                            '    a.SetUserInfoCard(dwEnrollNumber.ToString, n, p, CInt(pr) - 1, True, c)
'                            'Else
'                            '    a.SetUserInfo(dwEnrollNumber, n, p, CInt(pr) - 1, True)
'                            'End If

'                            'a.SetUserInfo(dwEnrollNumber, n, p, CInt(pr) - 1, True, CInt(c))

'                            a.RefreshData()
'                            For i As Integer = 1 To 10
'                                Me.BeginProcess("Processing " & "F" & i.ToString)
'                                s = mtEmployeeFingerPrint.Rows(0)("F" & i.ToString).ToString
'                                If s <> "" Then
'                                    Try
'                                        a.SetUserTmpStr(dwEnrollNumber, i - 1, s)
'                                    Catch ex As Exception

'                                    End Try
'                                End If
'                            Next
'                            a.RefreshData()
'                            a.StartIdentify()
'                            Me.EndProcess("Done uploading data to device")
'                        Else
'                            Me.EndProcess("Can not connect to " & dr("LogDevice").ToString & " (" & a.IP & ")", False)
'                        End If
'                    Else
'                        Me.EndProcess("Can not connect to " & dr("LogDevice").ToString & " (" & a.IP & ")", False)
'                    End If
'                Catch ex As Exception
'                    Me.EndProcess("Can not connect to " & dr("LogDevice").ToString & " (" & a.IP & ")", False)
'                End Try

'            Next dr
'        End If
'    End Sub
'    'Private Sub Upload()
'    '    Dim n, c, p, pr, AccNo As String
'    '    Dim s As String
'    '    Dim vDeviceRows As DataRow()
'    '    Dim dwEnrollNumber As Integer
'    '    Dim dwBackupNumber As Integer = 11
'    '    AccNo = myDT.Get(Database.Tables.tEmployee.Field.AccessNo).ToString

'    '    If IsDBNull(AccNo) Or AccNo = "" Then
'    '        MsgBox("No AccessNo, cannot upload data to device")
'    '        EndProcess("Done")
'    '        Exit Sub
'    '    Else
'    '        dwEnrollNumber = CInt(AccNo)
'    '    End If
'    '    'dwEnrollNumber = myDT.Get(Database.Tables.tEmployee.Field.AccessNo)
'    '    vDeviceRows = mtEmployeeLogDevice.Select()
'    '    n = myDT.Get(Database.Tables.tEmployee.Field.Code).ToString
'    '    c = myDT.Get(Database.Tables.tEmployee.Field.CardNo).ToString
'    '    pr = myDT.Get(Database.Tables.tEmployee.Field.ID_DevicePrivilege).ToString
'    '    If c = "" Then
'    '        c = 0
'    '    End If
'    '    If pr = "" Then
'    '        pr = 1
'    '    End If
'    '    p = myDT.Get(Database.Tables.tEmployee.Field.LogPassword).ToString
'    '    If vDeviceRows.Length = 0 Then
'    '        EndProcess("No device selected", False)
'    '    Else
'    '        For Each dr As DataRow In vDeviceRows
'    '            Dim a As New FSDevice.Device
'    '            Dim dra As DataRow()
'    '            'Dim con As String
'    '            'Dim ipadd As Object

'    '            'con = "SELECT IPAddress FROM tLogDevice e WHERE ID = " & dr("ID_LogDevice").ToString
'    '            'ipadd = GSCOM.SQL.ExecuteScalar(con, gConnection).ToString
'    '            a.IP = dr("IPAddress")
'    '            a.Port = 4370
'    '            If a.Connect Then
'    '                DevPlat = a.GetPlatform
'    '                Dim u As FSDevice.UserTable
'    '                If DevPlat = ZEM500 Then
'    '                    u = a.GetUser2(dwEnrollNumber)
'    '                Else
'    '                    u = a.GetUser(dwEnrollNumber)
'    '                End If
'    '                'u = a.GetUser(dwEnrollNumber)
'    '                dra = u.Select
'    '                If dra.Length > 0 Then
'    '                    a.DeleteAllFingerPrint(dwEnrollNumber, dwBackupNumber)
'    '                End If

'    '                If DevPlat = ZEM500 Then
'    '                    a.SetUserInfoCard(dwEnrollNumber.ToString, n, p, CInt(pr) - 1, True, c)
'    '                Else
'    '                    a.SetUserInfo(dwEnrollNumber, n, p, CInt(pr) - 1, True)
'    '                End If

'    '                'a.SetUserInfo(dwEnrollNumber, n, p, CInt(pr) - 1, True, CInt(c))

'    '                a.RefreshData()
'    '                For i As Integer = 1 To 10
'    '                    Me.BeginProcess("Processing " & "F" & i.ToString)
'    '                    s = mtEmployeeFingerPrint.Rows(0)("F" & i.ToString).ToString
'    '                    If s <> "" Then
'    '                        Try
'    '                            a.SetUserTmpStr(dwEnrollNumber, i - 1, s)
'    '                        Catch ex As Exception

'    '                        End Try
'    '                    End If
'    '                Next
'    '                a.RefreshData()
'    '                a.StartIdentify()
'    '                Me.EndProcess("Done uploading data to device")
'    '            Else
'    '                Me.EndProcess("Can not connect to " & dr("LogDevice").ToString & " (" & a.IP & ")", False)
'    '            End If
'    '        Next dr
'    '    End If
'    'End Sub

'    Private Sub DeleteFromOtherDevice()
'        Dim dwEnrollNumber As Integer
'        Dim dwBackupNumber As Integer = 11
'        'Dim n As String
'        'n = myDT.Get(Database.Tables.tEmployee.Field.Code.ToString)
'        Dim s As String
'        Dim dt As DataTable
'        s = "SELECT ID,IPAddress,Name FROM tLogDevice ld WHERE ID NOT IN (SELECT ID_LogDevice FROM tEmployeeLogDevice eld WHERE ID_Employee=" & GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tEmployee.Field.ID)) & ")"
'        dt = GSCOM.SQL.TableQuery(s, gConnection)
'        dwEnrollNumber = myDT.Get(Database.Tables.tEmployee.Field.AccessNo)
'        For Each dr As DataRow In dt.Select
'            Me.BeginProcess("Processing " & dr("IPAddress"))
'            Dim a As New FSDevice.Device
'            Dim dra As DataRow()
'            a.IP = dr("IPAddress")
'            a.Port = 4370
'            Try
'                If My.Computer.Network.Ping(a.IP) Then
'                    If a.Connect Then
'                        Dim u As FSDevice.UserTable
'                        u = a.GetUser(dwEnrollNumber)
'                        dra = u.Select
'                        If dra.Length > 0 Then
'                            a.DeleteAllFingerPrint(dwEnrollNumber, dwBackupNumber)
'                            a.DeleteUser(dwEnrollNumber)
'                            '---------------\
'                            Dim eld As New nDB.Tables.tEmployeeLogDevice(gConnection)
'                            s = "ID_Employee=" & myDT.Get(Database.Tables.tEmployee.Field.ID).ToString  'eid("ID_Employee").ToString
'                            s &= " AND ID_LogDevice=" & dr("ID").ToString
'                            eld.ClearThenFill(s)
'                            If eld.Rows.Count > 0 Then
'                                eld.Rows(0).Delete()
'                                eld.Update()
'                            End If
'                            '-------------------/
'                        End If
'                        a.RefreshData()
'                    Else
'                        Me.EndProcess("Unable to connect to device " & dr("Name").ToString, False)
'                    End If
'                Else
'                    Me.EndProcess("Unable to connect to device " & dr("Name").ToString, False)
'                End If

'            Catch ex As Exception
'                Me.EndProcess("Unable to connect to device " & dr("Name").ToString, False)
'            End Try

'        Next
'        Me.EndProcess("Done")
'    End Sub

'    'Private Sub DeleteFromOtherDevice()
'    '    Dim dwEnrollNumber As Integer
'    '    Dim dwBackupNumber As Integer = 11
'    '    'Dim n As String
'    '    'n = myDT.Get(Database.Tables.tEmployee.Field.Code.ToString)
'    '    Dim s As String
'    '    Dim dt As DataTable
'    '    s = "SELECT ID,IPAddress,Name FROM tLogDevice ld WHERE ID NOT IN (SELECT ID_LogDevice FROM tEmployeeLogDevice eld WHERE ID_Employee=" & GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tEmployee.Field.ID)) & ")"
'    '    dt = GSCOM.SQL.TableQuery(s, gConnection)
'    '    dwEnrollNumber = myDT.Get(Database.Tables.tEmployee.Field.AccessNo)
'    '    For Each dr As DataRow In dt.Select
'    '        Me.BeginProcess("Processing " & dr("IPAddress"))
'    '        Dim a As New FSDevice.Device
'    '        Dim dra As DataRow()
'    '        a.IP = dr("IPAddress")
'    '        a.Port = 4370
'    '        If a.Connect Then
'    '            Dim u As FSDevice.UserTable
'    '            u = a.GetUser(dwEnrollNumber)
'    '            dra = u.Select
'    '            If dra.Length > 0 Then
'    '                a.DeleteAllFingerPrint(dwEnrollNumber, dwBackupNumber)
'    '                a.DeleteUser(dwEnrollNumber)
'    '                '---------------\
'    '                Dim eld As New nDB.Tables.tEmployeeLogDevice(gConnection)
'    '                s = "ID_Employee=" & myDT.Get(Database.Tables.tEmployee.Field.ID).ToString  'eid("ID_Employee").ToString
'    '                s &= " AND ID_LogDevice=" & dr("ID").ToString
'    '                eld.ClearThenFill(s)
'    '                If eld.Rows.Count > 0 Then
'    '                    eld.Rows(0).Delete()
'    '                    eld.Update()
'    '                End If
'    '                '-------------------/
'    '            End If
'    '            a.RefreshData()
'    '        Else
'    '            Me.EndProcess("Unable to connect to device " & dr("Name").ToString, False)
'    '        End If
'    '    Next
'    '    Me.EndProcess("Done")
'    'End Sub

'    Public Overrides Sub LoadInfo(ByVal pID As Integer)
'        MyBase.LoadInfo(pID)
'        Me.EnableExtraButtons(pID > 0)
'        mInitID = pID
'        Dim vAccessNo As Integer
'        If pID = 0 Then
'            Dim dt As DataTable
'            dt = GSCOM.SQL.TableQuery("SELECT AccessNo FROM tEmployee", gConnection)
'            For i As Integer = 1 To 65000
'                If dt.Select("AccessNo=" & i.ToString).Length = 0 Then
'                    vAccessNo = i
'                    Exit For
'                End If
'            Next
'            If vAccessNo <> 0 Then
'                myDT.Set(Database.Tables.tEmployee.Field.AccessNo, vAccessNo)
'                'Else
'                '   MsgBox()
'            End If

'        End If
'        If mtEmployeeFingerPrint.Rows.Count = 0 Then
'            Dim dr As DataRow
'            dr = mtEmployeeFingerPrint.NewRow
'            mtEmployeeFingerPrint.Rows.Add(dr)
'        End If
'        'ExchangeData(False)
'        Try

'            If pID = 0 Then
'                For Each dr As DataRow In mWeekDayTable.Select
'                    Dim drx As DataRow
'                    drx = mtEmployeeLogDevice.NewRow
'                    drx("ID_Employee") = myDT.Get(Database.Tables.tEmployee.Field.ID)
'                    drx("ID_LogDevice") = dr("ID")

'                    mtEmployeeLogDevice.Rows.Add(drx)
'                Next
'            End If
'        Catch ex As Exception

'        End Try


'        'mTVWeekDay.CheckNodes(mtEmployeeLogDevice, Database.Tables.tEmployeeLogDevice.Field.ID_LogDevice.ToString)
'        'mTVWeekDay.ExpandAll()
'    End Sub

'    Protected Overrides Function CanSave() As Boolean
'        Dim i As Integer
'        Dim s As String
'        s = "SELECT COUNT(*) FROM tEmployee WHERE AccessNo=" & GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tEmployee.Field.AccessNo))
'        Try
'            i = GSCOM.SQL.ExecuteScalar(s, gConnection)
'        Catch ex As Exception
'        End Try
'        If mInitID = 0 Then
'            If i >= 1 Then
'                EndProcess("Duplicate Access No", False)
'                Return False
'            End If
'        Else
'            If i >= 2 Then
'                EndProcess("Duplicate Access No", False)
'                Return False
'            End If
'        End If
'        'mTVWeekDay.EndEdit(mtEmployeeLogDevice, Database.Tables.tEmployeeLogDevice.Field.ID_LogDevice.ToString)
'        Me.EnableExtraButtons(True)
'        Return MyBase.CanSave()
'    End Function

'#Region "Overrides"
'    Protected Overrides Property Table() As GSCOM.SQL.ZDataTable
'        Get
'            Return myDT
'        End Get
'        Set(ByVal value As GSCOM.SQL.ZDataTable)
'            myDT = CType(value, nDB.Tables.tEmployee)
'        End Set
'    End Property

'    Protected Property Control() As Control
'        Get
'            Return mControl
'        End Get
'        Set(ByVal value As Control)
'            mControl = CType(value, InSys.DataControl)
'        End Set
'    End Property

'#End Region

'    Private Sub LogDeviceEmployeeInfo_Commited(ByVal sender As Object, ByVal e As InfoSet.CommitedEventArgs) Handles Me.Commited
'        mInitID = myDT.Get(Database.Tables.tEmployee.Field.ID)
'    End Sub

'End Class
