Option Explicit On
Option Strict On

Imports nDB = GSCOM.Applications.InSys.Database

Friend Class LogDeviceInfo
    Inherits InfoSet

    Private myDT As New nDB.Tables.tLogDevice(Connection)
    Private mtLogDeviceUser As GSCOM.SQL.ZDataTable
    Private mControl As New InSys.DataControl
    Private mReportViewer As CrystalDecisions.Windows.Forms.CrystalReportViewer
    'Private mtBatchEmployeeFingerPrint As GSCOM.SQL.ZDataTable
    Dim PrevIP As String = ""
    Private mAddEmployeeButton As ToolStripButton
    Private mRestartDeviceButton As ToolStripButton
    Private mPowerOffDeviceButton As ToolStripButton
    Private mSyncTime As ToolStripButton
    'Private mtEmployeeLogDevice As GSCOM.SQL.ZDataTable
    'Private mtEmployeeLogDeviceDownload As GSCOM.SQL.ZDataTable
    'Private mWeekDayTable As DataTable = GSCOM.SQL.TableQuery("SELECT ID,Name FROM vLogDevice_List", Connection)
    'Private mTVWeekDay As New LogDeviceSelector

    'Private mInitID As Integer

    Public Sub New(ByVal pMenu As Integer, ByVal c As SqlClient.SqlConnection, ByVal pListing As DataTable, ByVal pID As Integer)
        MyBase.New(c, pListing, pID)
        With mDataset.Tables
            .Add(Table)
        End With


        InitControl(pMenu)
        'mReportViewer = AddReportViewer("Report Summary")
        'mReportViewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
        'mRestartDeviceButton = Me.GetStripButton("Restart Device")
        'AddHandler mRestartDeviceButton.Click, AddressOf RestartDevice
        'mPowerOffDeviceButton = Me.GetStripButton("Power off Device")
        'AddHandler mPowerOffDeviceButton.Click, AddressOf PowerOffDevice
        'mSyncTime = Me.GetStripButton("Sync Time")
        'AddHandler mSyncTime.Click, AddressOf SyncTime

        'mtEmployeeLogDevice = CType(Me.Dataset.Tables("tBatchEmployeeLogDevice"), GSCOM.SQL.ZDataTable)
        'mtBatchEmployeeFingerPrint = CType(Me.Dataset.Tables("tBatchFingerDataTransfer_Detail"), GSCOM.SQL.ZDataTable)
        'With mTVWeekDay
        '    .DataSource = mWeekDayTable
        '    .Go()
        'End With
        'Me.AddControl(mTVWeekDay, "Batch Log Device")


        'mAddEmployeeButton = Me.GetStripButton("Generate")
        'AddHandler mAddEmployeeButton.Click, AddressOf AddEmployee



        Me.ReloadAfterCommit = True

        AfterNew()


        'mtLogDeviceUser = CType(Me.mDataset.Tables("tLogDeviceUser"), GSCOM.SQL.ZDataTable)



        'Try


        'Catch ex As Exception

        'End Try

    End Sub
    Private Sub SyncTime(ByVal sender As Object, ByVal e As EventArgs)

        'If MsgBox("Are you sure you want to sync this pc's time with the device?", MsgBoxStyle.YesNo Or MsgBoxStyle.Question) = MsgBoxResult.No Then
        '    Exit Sub
        'End If

        'Dim devST As New FSDevice.Device
        'BeginProcess("Syncing Time")
        'devST.IP = myDT.Get(Database.Tables.tLogDevice.Field.IPAddress).ToString
        'devST.Port = 4370

        'If devST.Connect Then
        '    devST.SetTime(System.DateTime.Now.Minute, System.DateTime.Now.Second, System.DateTime.Now.Hour, System.DateTime.Now.Day, System.DateTime.Now.Year, System.DateTime.Now.Month)
        '    devST.Disconnect()
        '    EndProcess("Time Synced", True)
        'Else
        '    EndProcess("Device not connected", False)
        'End If


    End Sub

    Private Sub RestartDevice(ByVal sender As Object, ByVal e As EventArgs)

        'If MsgBox("Are you sure you want to restart the device?", MsgBoxStyle.YesNo Or MsgBoxStyle.Question) = MsgBoxResult.No Then
        '    Exit Sub
        'End If

        'Dim devRD As New FSDevice.Device
        'BeginProcess("Restarting Device")
        'devRD.IP = myDT.Get(Database.Tables.tLogDevice.Field.IPAddress).ToString
        'devRD.Port = 4370

        'If devRD.IP.ToUpper = "USB" Then
        '    If devRD.ConnectUSB Then
        '        devRD.RestartDevice()
        '        devRD.Disconnect()
        '        EndProcess("Device restarted", True)
        '    Else
        '        EndProcess("Device not connected", False)
        '    End If
        'Else
        '    If devRD.Connect Then
        '        devRD.RestartDevice()
        '        devRD.Disconnect()
        '        EndProcess("Device restarted", True)
        '    Else

        '        EndProcess("Device not connected", False)
        '    End If
        'End If

        


    End Sub

    Private Sub PowerOffDevice(ByVal sender As Object, ByVal e As EventArgs)

        'If MsgBox("Are you sure you want to turn the device off?", MsgBoxStyle.YesNo Or MsgBoxStyle.Question) = MsgBoxResult.No Then
        '    Exit Sub
        'End If

        'Dim devPOD As New FSDevice.Device
        'BeginProcess("Turning off device")
        'devPOD.IP = myDT.Get(Database.Tables.tLogDevice.Field.IPAddress).ToString
        'devPOD.Port = 4370
        'If devPOD.IP.ToUpper = "USB" Then
        '    If devPOD.ConnectUSB Then
        '        devPOD.PowerOffDevice()
        '        devPOD.Disconnect()
        '        EndProcess("Device turned off")
        '    Else
        '        EndProcess("Device not connected", False)
        '    End If
        'Else
        '    If devPOD.Connect Then
        '        devPOD.PowerOffDevice()
        '        devPOD.Disconnect()
        '        EndProcess("Device turned off")
        '    Else
        '        EndProcess("Device not connected", False)
        '    End If
        'End If
        
    End Sub

    Public Overrides Sub LoadInfo(ByVal pID As Integer)
        MyBase.LoadInfo(pID)
        'Me.EnableExtraButtons(pID > 0)
        'mInitID = pID

        'Dim dt As DataTable
        'dt = GSCOM.SQL.TableQuery("SELECT * FROM vzDeviceUserNotInDB WHERE ID_LogDevice = " & myDT.Get(Database.Tables.tLogDevice.Field.ID).ToString, Connection)
        'Dim rd As New CrystalDecisions.CrystalReports.Engine.ReportDocument

        'If pID > 0 Then
        '    Dim LDev As New FSDevice.Device
        '    If myDT.Get(Database.Tables.tLogDevice.Field.IPAddress).ToString.ToUpper = "USB" Then

        '        If LDev.ConnectUSB Then
        '            myDT.Set(Database.Tables.tLogDevice.Field.WithCard, LDev.IsDeviceCard)
        '            myDT.Set(Database.Tables.tLogDevice.Field.MacAddress, LDev.GetMacAdd)
        '            myDT.Set(Database.Tables.tLogDevice.Field.Firmware, LDev.GetFirmwareVersion)
        '            myDT.Set(Database.Tables.tLogDevice.Field.AdminCount, LDev.GetDevStatus(1))
        '            myDT.Set(Database.Tables.tLogDevice.Field.RegUserCount, LDev.GetDevStatus(2))
        '            myDT.Set(Database.Tables.tLogDevice.Field.FingerCount, LDev.GetDevStatus(3))
        '            myDT.Set(Database.Tables.tLogDevice.Field.PassCount, LDev.GetDevStatus(4))
        '            myDT.Set(Database.Tables.tLogDevice.Field.AttCount, LDev.GetDevStatus(6))
        '            myDT.Set(Database.Tables.tLogDevice.Field.FingerCap, LDev.GetDevStatus(7))
        '            myDT.Set(Database.Tables.tLogDevice.Field.UserCap, LDev.GetDevStatus(8))
        '            myDT.Set(Database.Tables.tLogDevice.Field.AttCap, LDev.GetDevStatus(9))
        '            LDev.Disconnect()
        '        End If

        '    Else
        '        PrevIP = myDT.Get(Database.Tables.tLogDevice.Field.IPAddress).ToString
        '        LDev.IP = myDT.Get(Database.Tables.tLogDevice.Field.IPAddress).ToString
        '        LDev.Port = 4370
        '        If My.Computer.Network.Ping(LDev.IP) Then
        '            If LDev.Connect Then
        '                myDT.Set(Database.Tables.tLogDevice.Field.WithCard, LDev.IsDeviceCard)
        '                myDT.Set(Database.Tables.tLogDevice.Field.MacAddress, LDev.GetMacAdd)
        '                myDT.Set(Database.Tables.tLogDevice.Field.Firmware, LDev.GetFirmwareVersion)
        '                myDT.Set(Database.Tables.tLogDevice.Field.AdminCount, LDev.GetDevStatus(1))
        '                myDT.Set(Database.Tables.tLogDevice.Field.RegUserCount, LDev.GetDevStatus(2))
        '                myDT.Set(Database.Tables.tLogDevice.Field.FingerCount, LDev.GetDevStatus(3))
        '                myDT.Set(Database.Tables.tLogDevice.Field.PassCount, LDev.GetDevStatus(4))
        '                myDT.Set(Database.Tables.tLogDevice.Field.AttCount, LDev.GetDevStatus(6))
        '                myDT.Set(Database.Tables.tLogDevice.Field.FingerCap, LDev.GetDevStatus(7))
        '                myDT.Set(Database.Tables.tLogDevice.Field.UserCap, LDev.GetDevStatus(8))
        '                myDT.Set(Database.Tables.tLogDevice.Field.AttCap, LDev.GetDevStatus(9))
        '                LDev.Disconnect()
        '            End If
        '        End If

        '    End If

        'End If
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
            myDT = CType(value, nDB.Tables.tLogDevice)
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
        mInitID = CInt(myDT.Get(Database.Tables.tLogDevice.Field.ID))
    End Sub
#Region "AddEmployee"
    Private Sub AddEmployee(ByVal sender As Object, ByVal e As EventArgs)
        'Dim mT4 As New FSDevice.Device
        'With mT4
        '    .IP = myDT.Get(Database.Tables.tLogDevice.Field.IPAddress).ToString
        '    .Port = 4370

        'End With

        'If My.Computer.Network.Ping(mT4.IP) Then
        '    If mT4.Connect Then

        '        GSCOM.SQL.ExecuteNonQuery("EXEC pLogDeviceUserNotInDBInit " & CInt(myDT.Get(Database.Tables.tLogDevice.Field.ID)), Connection)

        '        Dim dt As FSDevice.UserTable
        '        dt = mT4.GetUserTable
        '        Dim an As Integer
        '        For Each dr As DataRow In dt.Select


        '            an = CInt(dr("EnrollNumber"))

        '            GSCOM.SQL.ExecuteNonQuery("EXEC pLogDeviceUserNotInDB " & CInt(myDT.Get(Database.Tables.tLogDevice.Field.ID)) & "," & an, Connection)

        '        Next
        '    Else
        '        MsgBox(mT4.IP.ToString & " is not Connected.", MsgBoxStyle.Information)
        '        Exit Sub
        '    End If
        'Else
        '    MsgBox(mT4.IP.ToString & " is not Connected.", MsgBoxStyle.Information)
        '    Exit Sub
        'End If

        'MsgBox("Done.", MsgBoxStyle.Information)
        'Me.LoadInfo(CInt(myDT.Get(Database.Tables.tLogDevice.Field.ID)))


    End Sub

#End Region
End Class
