Option Explicit On
Option Strict On

Imports nDB = GSCOM.Applications.InSys.Database

Friend Class BatchFingerDataTransferInfo
    Inherits InfoSet
    Const DA800P As String = "ZEM500"
    Private myDT As New nDB.Tables.tBatchFingerDataTransfer(Connection)
    Private mtBatchFingerDataTransfer_Detail As GSCOM.SQL.ZDataTable
    Private mControl As New InSys.DataControl

    Private mtBatchEmployeeFingerPrint As GSCOM.SQL.ZDataTable

    Private mAddEmployeeButton As ToolStripButton

    Private mtBatchEmployeeLogDevice As GSCOM.SQL.ZDataTable

    Public Sub New(ByVal pMenu As Integer, ByVal c As SqlClient.SqlConnection, ByVal pListing As DataTable, ByVal pID As Integer)
        MyBase.New(c, pListing, pID)
        With mDataset.Tables
            .Add(Table)

        End With

        InitControl(pMenu)



        mtBatchEmployeeLogDevice = CType(Me.mDataset.Tables("tBatchEmployeeLogDevice"), GSCOM.SQL.ZDataTable)
        mtBatchEmployeeFingerPrint = CType(Me.mDataset.Tables("tBatchFingerDataTransfer_Detail"), GSCOM.SQL.ZDataTable)

        Me.AddButton("Download Fingerprint Data from Device", gMainForm.imgList.Images("_logdevice.png"), AddressOf Import)
        Me.AddButton("Update Fingerprint Data to Device", gMainForm.imgList.Images("_logdevice.png"), AddressOf Update)
        'Me.AddButton("Sychronized Time", gMainForm.imgList.Images("_logdevice.png"), AddressOf SychronizedTime)


        AfterNew()


        mtBatchFingerDataTransfer_Detail = CType(Me.mDataset.Tables("tBatchFingerDataTransfer_Detail"), GSCOM.SQL.ZDataTable)



        Try


        Catch ex As Exception

        End Try

    End Sub

    'gLen.code

    Private Sub SychronizedTime(ByVal sender As Object, ByVal e As EventArgs)
        Me.BeginProcess("Sychronizing Time")
        MainModule.SychronizedTime()
        Me.EndProcess("Done sychronizing Time")
    End Sub

    'Private Sub SychronizedTime(ByVal sender As Object, ByVal e As EventArgs)
    '    Me.BeginProcess("Sychronizing Time")
    '    getTime()
    '    Me.EndProcess("Done sychronizing Time")

    'End Sub

    'Private Sub getTime()
    '    Dim currentTime As System.DateTime = System.DateTime.Now
    '    Dim year As Integer = currentTime.Year
    '    Dim month As Integer = currentTime.Month
    '    Dim day As Integer = currentTime.Day
    '    Dim hour As Integer = currentTime.Hour
    '    Dim minute As Integer = currentTime.Minute
    '    Dim secod As Integer = currentTime.Second
    '    Dim vDeviceRows As DataRow()
    '    vDeviceRows = mtBatchEmployeeLogDevice.Select()
    '    Dim a As New FSDevice.Device
    '    Dim s As String
    '    For Each dr As DataRow In vDeviceRows
    '        a.IP = dr("IPAddress").ToString
    '        a.Port = 4370
    '        s = dr("IPAddress").ToString
    '        Me.BeginProcess(s)
    '        If a.Connect() Then
    '            a.SetTime(minute, secod, hour, day, year, month)
    '        End If
    '    Next
    'End Sub

    Private Sub Update(ByVal sender As Object, ByVal e As EventArgs)
        Me.BeginProcess("Updating")
        Upload()
        DeleteFromOtherDevice()
    End Sub
    Private Sub Import(ByVal sender As Object, ByVal e As EventArgs)
        Me.BeginProcess("Downloading")
        'mtEmployeeLogDeviceDownload = Me.Dataset.Tables("Select top 1 * from tBatchEmployeeLogDevice where id =" & GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tBatchFingerDataTransfer.Field.ID)) & ")")
        Download()
        ''DeleteFromOtherDevice()
    End Sub

#Region "Download"
    Private Sub Download()


        'Dim o As Object
        'Dim vDeviceRows As DataRow()
        'Dim dwEnrollNumber As Integer


        'Dim dwBackupNumber As Integer = 11
        'Dim vEmployeeID As DataRow()
        'vEmployeeID = mtBatchEmployeeFingerPrint.Select()

        'vDeviceRows = mtBatchEmployeeLogDevice.Select()

        'If vDeviceRows.Length = 0 Then
        '    EndProcess("No device selected", False)
        'Else
        '    Dim u As FSDevice.UserTable

        '    For Each dr As DataRow In vDeviceRows
        '        Dim a As New FSDevice.Device
        '        Dim dra As DataRow()
        '        a.IP = dr("IPAddress").ToString
        '        a.Port = 4370

        '        Try
        '            If My.Computer.Network.Ping(a.IP) Then
        '                If a.Connect Then
        '                    DevPlat = a.GetPlatform
        '                    For Each eid As DataRow In vEmployeeID
        '                        Dim v As String

        '                        Dim bio As String

        '                        v = "SELECT AccessNo FROM tEmployee e WHERE ID = " & eid("ID_Employee").ToString
        '                        o = GSCOM.SQL.ExecuteScalar(v, gConnection)

        '                        v = "SELECT Name FROM vEmployee l WHERE ID = " & eid("ID_Employee").ToString
        '                        bio = GSCOM.SQL.ExecuteScalar(v, gConnection).ToString

        '                        If IsDBNull(o) Then
        '                            dwEnrollNumber = 0
        '                        Else
        '                            dwEnrollNumber = CInt(o)
        '                        End If
        '                        If DevPlat = ZEM500 Then
        '                            u = a.GetUser2(dwEnrollNumber)
        '                        Else
        '                            u = a.GetUser(dwEnrollNumber)
        '                        End If


        '                        dra = u.Select("EnrollNumber=" & dwEnrollNumber.ToString)
        '                        If dra.Length > 0 Then
        '                            Dim f As New nDB.Tables.tEmployeeFingerPrint(gConnection)

        '                            Dim eld As New nDB.Tables.tEmployeeLogDevice(gConnection)
        '                            Dim s As String
        '                            s = "ID_Employee=" & eid("ID_Employee").ToString
        '                            f.ClearThenFill(s)
        '                            s &= " AND ID_LogDevice=" & dr("ID_LogDevice").ToString
        '                            eld.ClearThenFill(s)
        '                            Dim fdr As DataRow
        '                            If f.Rows.Count = 0 Then
        '                                fdr = f.NewRow
        '                                fdr("ID_Employee") = eid("ID_Employee")
        '                                f.Rows.Add(fdr)
        '                            Else
        '                                fdr = f.Rows(0)
        '                            End If
        '                            For i As Integer = 1 To 10
        '                                s = "Processing " & "F" & i.ToString & " of " & bio.ToString
        '                                Me.BeginProcess(s)
        '                                fdr("F" & i.ToString) = dra(0).Item("F" & (i - 1).ToString)
        '                            Next
        '                            'Dim pw As Integer
        '                            'pw = CInt(dra(0).Item(FSDevice.UserTable.Fields.Password).ToString)

        '                            'If Not IsDBNull(pw) Then
        '                            '    GSCOM.SQL.ExecuteNonQuery("EXEC pEmployeeLogPassword " & eid("ID_Employee").ToString & "," & pw.ToString, Connection)
        '                            'End If

        '                            If eld.Rows.Count = 0 Then
        '                                Dim eldr As DataRow
        '                                eldr = eld.NewRow
        '                                eldr("ID_Employee") = eid("ID_Employee")
        '                                eldr("ID_LogDevice") = dr("ID_LogDevice")
        '                                eld.Rows.Add(eldr)
        '                            End If
        '                            f.Update()
        '                            eld.Update()
        '                            '--------------------------------------------------------
        '                            Dim emp As New nDB.Tables.tEmployee(gConnection)
        '                            s = "ID=" & eid("ID_Employee").ToString
        '                            emp.ClearThenFill(s)
        '                            If emp.Rows.Count > 0 Then
        '                                emp.Set(Database.Tables.tEmployee.Field.CardNo, dra(0).Item(FSDevice.UserTable.Fields.CardNumber.ToString))
        '                                emp.Set(Database.Tables.tEmployee.Field.Password, dra(0).Item(FSDevice.UserTable.Fields.Password.ToString))
        '                                emp.Set(Database.Tables.tEmployee.Field.AccessCode, dra(0).Item(FSDevice.UserTable.Fields.Privelege.ToString))
        '                                emp.Set(Database.Tables.tEmployee.Field.ID_DevicePrivilege, CStr(CInt(dra(0).Item(FSDevice.UserTable.Fields.Privelege)) + 1))
        '                                emp.Update()
        '                            End If

        '                            Me.EndProcess("Done")
        '                        End If
        '                        Me.EndProcess("Done downloading data to device")
        '                    Next
        '                Else
        '                    Me.EndProcess("Can not connect to " & dr("LogDevice").ToString & " (" & a.IP & ")", False)
        '                End If
        '            Else
        '                Me.EndProcess("Can not connect to " & dr("LogDevice").ToString & " (" & a.IP & ")", False)
        '            End If
        '        Catch ex As Exception
        '            Me.EndProcess("Can not connect to " & dr("LogDevice").ToString & " (" & a.IP & ")", False)
        '        End Try



        '    Next dr
        'End If
    End Sub
   
#End Region

#Region "Upload"

    Private Sub Upload()
        '        Dim n As String
        '        Dim s As String
        '        Dim emp As String
        '        emp = ""
        '        Dim cntup As Integer = 0
        '        Dim vCNo As String
        '        Dim vDeviceRows As DataRow()
        '        Dim dwEnrollNumber As Integer
        '        Dim dwBackupNumber As Integer = 11
        '        Dim vEmployeeID As DataRow()
        '        vEmployeeID = mtBatchEmployeeFingerPrint.Select()

        '        vDeviceRows = mtBatchEmployeeLogDevice.Select()

        '        If vDeviceRows.Length = 0 Then
        '            EndProcess("No device selected", False)
        '        Else
        '            'v = "delete from tEmployeeLogDevice where id_employee = " & eid("ID_Employee").ToString
        '            'GSCOM.SQL.ExecuteNonQuery(v, gConnection)
        '            'GSCOM.SQL.ExecuteNonQuery(v, gConnection)
        '            For Each dr As DataRow In vDeviceRows
        '                Dim a As New FSDevice.Device
        '                Dim dra As DataRow()
        '                a.IP = dr("IPAddress").ToString
        '                a.Port = 4370
        '                If a.Connect Then
        '                    cntup = 0
        '                    DevPlat = a.GetPlatform
        '                    For Each eid As DataRow In vEmployeeID

        '                        Dim v As String
        '                        Dim x As Object



        '                        Dim c As String
        '                        Dim d As String
        '                        Dim p As String
        '                        Dim k As String
        '                        Dim pr As String
        '                        Dim bio As String

        '                        Dim m As Object



        '                        v = "SELECT AccessNo FROM tEmployee e WHERE ID = " & eid("ID_Employee").ToString
        '                        x = GSCOM.SQL.ExecuteScalar(v, gConnection).ToString
        '                        v = "SELECT id_deviceprivilege FROM tEmployee e WHERE ID = " & eid("ID_Employee").ToString

        '                        pr = GSCOM.SQL.ExecuteScalar(v, gConnection).ToString

        '                        v = "SELECT Code FROM tEmployee e WHERE ID = " & eid("ID_Employee").ToString
        '                        n = GSCOM.SQL.ExecuteScalar(v, gConnection).ToString

        '                        v = "SELECT Password FROM tEmployee e WHERE ID = " & eid("ID_Employee").ToString
        '                        p = GSCOM.SQL.ExecuteScalar(v, gConnection).ToString

        '                        v = "SELECT LogPassword FROM tEmployee e WHERE ID = " & eid("ID_Employee").ToString
        '                        c = GSCOM.SQL.ExecuteScalar(v, gConnection).ToString

        '                        v = "SELECT WithCard FROM tLogDevice l WHERE ID = " & dr.Item("ID_LogDevice").ToString
        '                        k = GSCOM.SQL.ExecuteScalar(v, gConnection).ToString

        '                        v = "SELECT CardNo FROM tEmployee e WHERE ID = " & eid("ID_Employee").ToString
        '                        m = GSCOM.SQL.ExecuteScalar(v, gConnection)

        '                        v = "SELECT Name FROM vEmployee l WHERE ID = " & eid("ID_Employee").ToString
        '                        bio = GSCOM.SQL.ExecuteScalar(v, gConnection).ToString





        '                        If IsDBNull(x) Or x.ToString = "" Then
        '                            Continue For
        '                        Else
        '                            dwEnrollNumber = CInt(x)

        '                        End If

        '                        If IsDBNull(c) Or c = "" Then
        '                            d = "0"
        '                        Else

        '                            d = c
        '                        End If








        '                        vCNo = ""
        '                        If CBool(k) And (Not IsDBNull(m)) Then
        '                            vCNo = m.ToString
        '                        End If




        '                        Dim u As FSDevice.UserTable
        '                        Dim cntr As Integer = 0
        '                        Try
        'hell:
        '                            cntr = cntr + 1
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

        '                        Catch

        '                            'MsgBox(CStr(cntr).ToString & " Tries")
        '                            If cntr <= 100 Then
        '                                GoTo hell
        '                            Else
        '                                Continue For
        '                            End If


        '                        End Try
        '                        dra = u.Select
        '                        If dra.Length > 0 Then
        '                            a.DeleteAllFingerPrint(dwEnrollNumber, dwBackupNumber)
        '                        End If

        '                        Try
        '                            'If a.GetPlatform = DA800P Then
        '                            '    a.SetUserInfoCard(dwEnrollNumber.ToString, n, p, CInt(pr) - 1, True, "")
        '                            'Else
        '                            '    a.SetUserInfo(dwEnrollNumber, n, p, CInt(pr) - 1, True)
        '                            'End If
        '                            If DevPlat.ToUpper.Contains("TFT") Then
        '                                a.SetUserInfo(dwEnrollNumber.ToString, n, p, CInt(pr) - 1, True, m.ToString)
        '                            Else
        '                                a.SetUserInfoCard(dwEnrollNumber.ToString, n, p, CInt(pr) - 1, True, m.ToString)
        '                            End If
        '                            'If DevPlat = ZEM500 Then
        '                            '    a.SetUserInfoCard(dwEnrollNumber.ToString, n, p, CInt(pr) - 1,  True, m.ToString)
        '                            'Else
        '                            '    a.SetUserInfo(dwEnrollNumber.ToString, n, p, CInt(pr) - 1, True, m.ToString)
        '                            'End If

        '                        Catch ex As Exception

        '                        End Try

        '                        cntup = cntup + 1 'updated counter
        '                        a.RefreshData()
        '                        For i As Integer = 1 To 10
        '                            Me.BeginProcess("Processing " & "F" & i.ToString & " of " & bio.ToString)
        '                            's = mtBatchEmployeeFingerPrint.Rows(0)("F" & i.ToString).ToString
        '                            s = eid("F" & i.ToString).ToString
        '                            If s <> "" Then
        '                                Try
        '                                    a.SetUserTmpStr(dwEnrollNumber, i - 1, s)
        '                                Catch ex As Exception

        '                                End Try
        '                            End If
        '                        Next

        '                        a.RefreshData()

        '                        a.StartIdentify()


        '                        Me.EndProcess("Done uploading data to device")
        '                    Next

        '                    For Each eid As DataRow In vEmployeeID
        '                        GSCOM.SQL.ExecuteNonQuery("delete from tEmployeeLogDevice where id_employee = " & eid("ID_Employee").ToString, gConnection)
        '                        Dim x As String = ""
        '                        x = GSCOM.SQL.ExecuteScalar("SELECT AccessNo FROM tEmployee e WHERE ID = " & eid("ID_Employee").ToString, gConnection).ToString
        '                        If x = "" Then
        '                            Continue For
        '                        End If
        '                        For Each drow As DataRow In vDeviceRows
        '                            Dim IDLDevice As String = ""
        '                            IDLDevice = GSCOM.SQL.ExecuteScalar("select id from tlogdevice where ipaddress = '" & drow("IPAddress").ToString & "'", gConnection).ToString
        '                            GSCOM.SQL.ExecuteNonQuery("insert into tEmployeeLogDevice (ID_Employee,ID_LogDevice) values (" & eid("ID_Employee").ToString & ", " & IDLDevice & ")", gConnection)
        '                        Next
        '                    Next
        '                    a.StartIdentify()
        '                Else
        '                    Me.EndProcess("Can not connect to " & dr("LogDevice").ToString & " (" & a.IP & ")", False)
        '                End If

        '            Next dr
        '        End If
        '        MsgBox(CStr(cntup) & " Users updated to device/s")
    End Sub
 
#End Region

 
    Private Sub DeleteFromOtherDevice()
        'Dim dwEnrollNumber As Integer
        'Dim dwBackupNumber As Integer = 11
        'Dim vEmployeeID As DataRow()
        'vEmployeeID = mtBatchEmployeeFingerPrint.Select()
        ''Dim n As String
        ''n = myDT.Get(Database.Tables.tEmployee.Field.Code.ToString)
        'Dim s As String
        'Dim dt As DataTable
        's = "SELECT ID,IPAddress,Name FROM tLogDevice ld WHERE ID NOT IN (SELECT ID_LogDevice FROM tBatchEmployeeLogDevice eld WHERE ID_BatchFingerDataTransfer=" & GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tBatchFingerDataTransfer.Field.ID)) & ")"
        'dt = GSCOM.SQL.TableQuery(s, gConnection)

        'For Each dr As DataRow In dt.Select
        '    Me.BeginProcess("Processing " & dr("IPAddress").ToString)
        '    Dim a As New FSDevice.Device
        '    Dim dra As DataRow()
        '    a.IP = dr("IPAddress").ToString
        '    a.Port = 4370
        '    Try
        '        If My.Computer.Network.Ping(a.IP) Then
        '            If a.Connect Then
        '                For Each eid As DataRow In vEmployeeID
        '                    Dim v As String
        '                    Dim x As Object
        '                    v = "SELECT AccessNo FROM tEmployee e WHERE ID = " & eid("ID_Employee").ToString
        '                    x = GSCOM.SQL.ExecuteScalar(v, gConnection).ToString
        '                    If IsDBNull(x) Then
        '                        dwEnrollNumber = 0
        '                    Else
        '                        dwEnrollNumber = CInt(x)
        '                    End If
        '                    Dim u As FSDevice.UserTable
        '                    u = a.GetUser(dwEnrollNumber)
        '                    dra = u.Select
        '                    If dra.Length > 0 Then
        '                        a.DeleteAllFingerPrint(dwEnrollNumber, dwBackupNumber)
        '                        a.DeleteUser(dwEnrollNumber)

        '                        '---------------\
        '                        Dim eld As New nDB.Tables.tEmployeeLogDevice(gConnection)
        '                        s = "ID_Employee=" & eid("ID_Employee").ToString
        '                        s &= " AND ID_LogDevice=" & dr("ID").ToString
        '                        eld.ClearThenFill(s)
        '                        If eld.Rows.Count > 0 Then
        '                            eld.Rows(0).Delete()
        '                            eld.Update()
        '                        End If
        '                        '-------------------/
        '                    End If
        '                    a.RefreshData()
        '                Next
        '            Else
        '                Me.EndProcess("Unable to connect to device " & dr("Name").ToString, False)
        '            End If
        '        Else
        '            Me.EndProcess("Unable to connect to device " & dr("Name").ToString, False)
        '        End If

        '    Catch ex As Exception
        '        Me.EndProcess("Unable to connect to device " & dr("Name").ToString, False)
        '    End Try


        'Next

        'Me.EndProcess("Done")
    End Sub
   
    Public Overrides Sub LoadInfo(ByVal pID As Integer)
        MyBase.LoadInfo(pID)
        Me.EnableExtraButtons(pID > 0)
        mInitID = pID
    End Sub

    Protected Overrides Function CanSave() As Boolean
        Me.EnableExtraButtons(True)
        Return MyBase.CanSave()
    End Function

#Region "Overrides"
    Protected Overrides Property Table() As GSCOM.SQL.ZDataTable
        Get
            Return myDT
        End Get
        Set(ByVal value As GSCOM.SQL.ZDataTable)
            myDT = CType(value, nDB.Tables.tBatchFingerDataTransfer)
        End Set
    End Property

    Protected Property Control() As Control
        Get
            Return mControl
        End Get
        Set(ByVal value As Control)
            mControl = CType(value, InSys.DataControl)
        End Set
    End Property

#End Region

    Private Sub LogDeviceEmployeeInfo_Commited(ByVal sender As Object, ByVal e As InfoSet.CommitedEventArgs) Handles Me.Commited
        mInitID = CInt(myDT.Get(Database.Tables.tBatchFingerDataTransfer.Field.ID))
    End Sub

#Region "AddEmployee"
    'Private Sub AddEmployee(ByVal sender As Object, ByVal e As EventArgs)
    '    Dim frm As New BrowserDataListForm(Database.Menu.HUMANRESOURCE_Employee, , True)

    '    frm.StartPosition = FormStartPosition.CenterScreen
    '    frm.Size = New Size(800, 600)
    '    'frm.SelectionMode = True
    '    frm.ShowDialog()

    '    Dim a As Integer()
    '    a = frm.CheckedRowID

    '    If a IsNot Nothing Then
    '        Dim dr As DataRow
    '        For Each i As Integer In a
    '            If mtBatchFingerDataTransfer_Detail.Select("ID_Employee=" & i).Length = 0 Then
    '                dr = mtBatchFingerDataTransfer_Detail.NewRow
    '                dr.Item("ID_Employee") = i
    '                dr.Item("ID_BatchFingerDataTransfer") = mInitID
    '                mtBatchFingerDataTransfer_Detail.Rows.Add(dr)
    '            End If
    '        Next
    '    End If

    '    Me.SaveButton.PerformClick()
    'End Sub
    'Private Sub AddEmployee(ByVal sender As Object, ByVal e As EventArgs)
    '    Dim frm As New GSCOM.UI.GSForms.DataSelectForm
    '    With frm.MainSelector
    '        .ImageList = gImageList
    '        .ImageKey = nDB.GetMenuValue(Database.Menu.FingerprintEnrollment_Employee, Database.Tables.tMenu.Field.ImageFile).ToString
    '        .GroupImageKey = nDB.GetSetting(Database.SettingEnum.FolderImageFile).ToString
    '        '.ImageKeys.Add(Database.GetMenuValue(Database.Menu.MasterFile_Company, Database.Tables.tMenu.Field.ImageKey).ToString, "Company")
    '        '.ImageKeys.Add(Database.GetMenuValue(Database.Menu.MasterFile_Branch, Database.Tables.tMenu.Field.ImageKey).ToString, "Branch")
    '        '.ImageKeys.Add(Database.GetMenuValue(Database.Menu.MasterFile_Department, Database.Tables.tMenu.Field.ImageKey).ToString, "Department")
    '        '.ImageKeys.Add(Database.GetMenuValue(Database.Menu.MasterFile_Designation, Database.Tables.tMenu.Field.ImageKey).ToString, "Designation")
    '        .GroupCount = 3
    '    End With
    '    frm.Size = New Size(800, 600)
    '    frm.Init(nDB.GetMenuDataSourceValue(Database.Menu.FingerprintEnrollment_Employee).ToString, Connection, )
    '    'frm.CheckNodes(mtBatchFingerDataTransfer_Detail, "ID_Employee")
    '    If frm.ShowDialog() = DialogResult.OK Then
    '        Dim dr As DataRow
    '        For Each i As Integer In frm.GetSelectedIDs
    '            If mtBatchFingerDataTransfer_Detail.Select("ID_Employee=" & i).Length = 0 Then
    '                dr = mtBatchFingerDataTransfer_Detail.NewRow
    '                dr.Item("ID_Employee") = i
    '                dr.Item("ID_BatchFingerDataTransfer") = mInitID
    '                mtBatchFingerDataTransfer_Detail.Rows.Add(dr)
    '            End If
    '        Next
    '        Me.SaveButton.PerformClick()
    '        'Me.GetDataGridView(mtBatchFingerDataTransfer_Detail).Refresh()
    '    End If
    'End Sub

    'Private Sub ApplyFile(ByVal sender As Object, ByVal e As EventArgs)
    '    If MsgBox("Do you want to apply the file?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
    '        GSCOM.SQL.ExecuteNonQuery("EXEC pScheduleAssignment " & myDT.Get(nDB.Tables.tScheduleAssignment.Field.ID).ToString, Connection)
    '        MsgBox("Finished applying the file.", MsgBoxStyle.Information)
    '    End If
    'End Sub

#End Region

End Class
