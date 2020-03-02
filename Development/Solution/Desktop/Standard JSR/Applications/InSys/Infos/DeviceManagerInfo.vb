'Option Explicit On
'Option Strict On

'Imports nDB = GSCOM.Applications.InSys.Database

'Friend Class DeviceManagerInfo
'    Inherits InfoSet
'    Private mControl As New InSys.DataControl
'    Private myDT As New nDB.Tables.tDeviceManager(Connection)
'    Private mtDeviceManager_Detail As GSCOM.SQL.ZDataTable
'    Private mtBatchEmployeeLogDevice As GSCOM.SQL.ZDataTable
'    Public Sub New(ByVal pMenu As Integer, ByVal c As SqlClient.SqlConnection, ByVal pListing As DataTable, ByVal pID As Integer)
'        MyBase.New(c, pListing, pID)
'        With mDataset.Tables
'            .Add(Table)
'        End With

'        InitControl(pMenu)
'        mtBatchEmployeeLogDevice = CType(Me.mDataset.Tables("tDeviceManagerLogDevice"), GSCOM.SQL.ZDataTable)
'        Me.AddButton("Download Logs", gMainForm.imgList.Images("_logdevice.png"), AddressOf DownloadLogs)
'        Me.AddButton("Execute", gMainForm.imgList.Images("applyfile.png"), AddressOf ApplyLogs)
'        AfterNew()
'        mtDeviceManager_Detail = CType(Me.mDataset.Tables("tDeviceManager_Detail"), GSCOM.SQL.ZDataTable)

'    End Sub

'    Private Function IsValidToInsert(ByVal AccessNo As String, ByVal DateTime As String) As Boolean
'        Dim dt As New DataTable
'        'Dim dr As DataRow
'        'Dim comIVT As New SqlCommand, drIVT As SqlDataReader

'        'comIVT.Connection = gConnection
'        'comIVT.Connection.Open()

'        Dim a As String
'        a = "Select * From tEmployeeAttendanceLog Where "
'        a &= "AccessNo =" & AccessNo
'        a &= " And DateTime=" & DateTime

'        'comIVT.CommandText = a 

'        'drIVT = comIVT.ExecuteReader

'        Dim dcmd As New SqlClient.SqlDataAdapter(a, gConnection)
'        'dt.Load(drIVT)
'        dcmd.Fill(dt)
'        If dt.Rows.Count >= 1 Then

'            Return False
'        Else

'            Return True
'        End If
'        'comIVT.Connection.Close()
'    End Function

'    Public Overrides Sub LoadInfo(ByVal pID As Integer)
'        MyBase.LoadInfo(pID)
'        Me.EnableExtraButtons(pID > 0)
'        mInitID = pID

'    End Sub

'    Private Sub ApplyLogs(ByVal sender As Object, ByVal e As EventArgs)
'        Me.BeginProcess("Executing")
'        Apply()
'    End Sub

'    Private Sub DownloadLogs(ByVal sender As Object, ByVal e As EventArgs)
'        Me.BeginProcess("Downloading")
'        Download()
'        CanSave()
'    End Sub

'    Private Sub Apply()
'        Dim idDM As String = myDT.Get(Database.Tables.tDeviceManager.Field.ID).ToString
'        Dim draDMLogs As DataRow()
'        Dim dmfilter As String = "ID_DeviceManager = " & idDM
'        'Dim dtEALogs As New nDB.Tables.tEmployeeAttendanceLog(gConnection)
'        draDMLogs = mtDeviceManager_Detail.Select(dmfilter)
'        Dim strUpdateLogs As String

'        For Each MLogs As DataRow In draDMLogs
'            If IsValidToInsert(GSCOM.SQL.SQLFormat(MLogs("AccessNo")), GSCOM.SQL.SQLFormat(MLogs("DateTime"))) Then
'                Dim strInsertLogs As String

'                strInsertLogs = "INSERT INTO tEmployeeAttendanceLog"
'                strInsertLogs &= vbCrLf
'                strInsertLogs &= "( [" & GSCOM.Applications.InSys.Database.Tables.tEmployeeAttendanceLog.Field.Source.ToString & "]"
'                strInsertLogs &= ", [" & GSCOM.Applications.InSys.Database.Tables.tEmployeeAttendanceLog.Field.AccessNo.ToString & "]"
'                strInsertLogs &= ", [" & GSCOM.Applications.InSys.Database.Tables.tEmployeeAttendanceLog.Field.DateTime.ToString & "]"
'                strInsertLogs &= ", [" & GSCOM.Applications.InSys.Database.Tables.tEmployeeAttendanceLog.Field.ID_AttendanceLogType.ToString & "]"
'                strInsertLogs &= ")"
'                strInsertLogs &= vbCrLf
'                strInsertLogs &= "VALUES"
'                strInsertLogs &= vbCrLf
'                strInsertLogs &= "(" & GSCOM.SQL.SQLFormat(MLogs("Source"))
'                strInsertLogs &= "," & GSCOM.SQL.SQLFormat(MLogs("AccessNo"))
'                strInsertLogs &= "," & GSCOM.SQL.SQLFormat(MLogs("DateTime"))
'                strInsertLogs &= "," & GSCOM.SQL.SQLFormat(MLogs("ID_AttendanceLogType"))
'                strInsertLogs &= ")"
'                GSCOM.SQL.ExecuteNonQuery(strInsertLogs, gConnection)



'            End If
'        Next

'        strUpdateLogs = "UPDATE  T SET "
'        strUpdateLogs &= "ID_Employee = (SELECT "
'        strUpdateLogs &= "		TOP 1 ID "
'        strUpdateLogs &= "	FROM tEmployee E "
'        strUpdateLogs &= "		WHERE "
'        strUpdateLogs &= "			(E.AccessNo = T.AccessNo) "
'        strUpdateLogs &= "		) "
'        strUpdateLogs &= "FROM "
'        strUpdateLogs &= "tEmployeeAttendanceLog T"
'        strUpdateLogs &= " WHERE "
'        strUpdateLogs &= "(T.ID_Employee IS NULL)"
'        GSCOM.SQL.ExecuteNonQuery(strUpdateLogs, gConnection)
'        Me.EndProcess("Executed")

'    End Sub

'    Private Sub Download()
'        Dim vDeviceRows As DataRow()
'        Dim StartDate As String = myDT.Get(Database.Tables.tDeviceManager.Field.StartDate).ToString
'        Dim EndDate As String = myDT.Get(Database.Tables.tDeviceManager.Field.EndDate).ToString
'        Dim idDM As String = myDT.Get(Database.Tables.tDeviceManager.Field.ID).ToString
'        Dim DateFilter As String = "(LOGDATE> = '" & StartDate & "' And LOGDATE<= '" & EndDate & "')"
'        vDeviceRows = mtBatchEmployeeLogDevice.Select()
'        Dim dtlogs As New nDB.Tables.tDeviceManager_Detail(gConnection)
'        GSCOM.SQL.ExecuteNonQuery("delete from tdevicemanager_detail where id_devicemanager = " & idDM, Connection)
'        Dim dmfilter As String = "ID_DeviceManager = " & idDM
'        'dtlogs.ClearThenFill(dmfilter)


'        'dtlogs.Update()
'        If vDeviceRows.Length = 0 Then
'            EndProcess("No device selected", False)
'        Else
'            For Each dr As DataRow In vDeviceRows
'                Dim a As New FSDevice.Device
'                a.IP = dr("IPAddress").ToString
'                a.Port = 4370
'                If a.IP.Trim = "" Then
'                    Continue For
'                End If
'                If a.IP.ToUpper = "USB" Then
'                    If a.ConnectUSB Then
'                        Dim strAlgo As String = a.GetAlgorithm
'                        Dim dt As DataTable
'                        Dim dra As DataRow()
'                        If strAlgo = "9" Or strAlgo = "10" Then
'                            dt = a.GetTimeLogTableUSB
'                        Else
'                            dt = a.GetTimeLogTableUsbOld
'                        End If
'                        If (Not IsNothing(dt)) Then
'                            If dt.Rows.Count > 0 Then
'                                dra = dt.Select(DateFilter)
'                                For Each tr As DataRow In dra
'                                    Dim LogsRow As DataRow
'                                    LogsRow = dtlogs.NewRow
'                                    LogsRow("ID_DeviceManager") = idDM
'                                    If GSCOM.SQL.SQLFormat(tr.Item(GSCOM.FSDevice.TimeLogTable.Fields.InOutMode)) <> "0" And GSCOM.SQL.SQLFormat(tr.Item(GSCOM.FSDevice.TimeLogTable.Fields.InOutMode)) <> "1" Then
'                                        LogsRow("ID_AttendanceLogType") = 2
'                                    Else
'                                        LogsRow("ID_AttendanceLogType") = CInt(tr.Item(GSCOM.FSDevice.TimeLogTable.Fields.InOutMode)) + 1
'                                    End If

'                                    LogsRow("Source") = a.IP
'                                    LogsRow("AccessNo") = CInt(tr.Item(GSCOM.FSDevice.TimeLogTable.Fields.EnrollNumber))
'                                    LogsRow("DateTime") = CDate(tr.Item(GSCOM.FSDevice.TimeLogTable.Fields.LogTime))
'                                    dtlogs.Rows.Add(LogsRow)
'                                    dtlogs.Update()
'                                Next
'                            End If

'                        End If
'                        a.Disconnect()

'                        Me.EndProcess("Done downloading data to device")
'                    Else
'                        Me.EndProcess("Cannot connect to " & dr("LogDevice").ToString & " (" & a.IP & ")", False)
'                    End If
'                Else

'                    If a.Connect Then

'                        Dim dt As DataTable
'                        Dim dra As DataRow()
'                        DevPlat = a.GetPlatform
'                        If DevPlat = ZEM500 Then
'                            dt = a.GetTimeLogTableOldIP
'                        Else
'                            dt = a.GetTimeLogTableIP
'                        End If
'                        If (Not IsNothing(dt)) Then
'                            If dt.Rows.Count > 0 Then
'                                dra = dt.Select(DateFilter)
'                                For Each tr As DataRow In dra
'                                    Dim LogsRow As DataRow
'                                    LogsRow = dtlogs.NewRow
'                                    LogsRow("ID_DeviceManager") = idDM
'                                    If GSCOM.SQL.SQLFormat(tr.Item(GSCOM.FSDevice.TimeLogTable.Fields.InOutMode)) <> "0" And GSCOM.SQL.SQLFormat(tr.Item(GSCOM.FSDevice.TimeLogTable.Fields.InOutMode)) <> "1" Then
'                                        LogsRow("ID_AttendanceLogType") = 2
'                                    Else
'                                        LogsRow("ID_AttendanceLogType") = CInt(tr.Item(GSCOM.FSDevice.TimeLogTable.Fields.InOutMode)) + 1
'                                    End If

'                                    LogsRow("Source") = a.IP
'                                    LogsRow("AccessNo") = CInt(tr.Item(GSCOM.FSDevice.TimeLogTable.Fields.EnrollNumber))
'                                    LogsRow("DateTime") = CDate(tr.Item(GSCOM.FSDevice.TimeLogTable.Fields.LogTime))
'                                    dtlogs.Rows.Add(LogsRow)
'                                    dtlogs.Update()
'                                Next
'                            End If

'                        End If
'                        a.Disconnect()
'                        Me.EndProcess("Done downloading data to device")
'                    Else

'                        Me.EndProcess("Cannot connect to " & dr("LogDevice").ToString & " (" & a.IP & ")", False)
'                    End If
'                End If

'            Next
'        End If
'    End Sub

'#Region "Overrides"
'    Protected Overrides Property Table() As GSCOM.SQL.ZDataTable
'        Get
'            Return myDT
'        End Get
'        Set(ByVal value As GSCOM.SQL.ZDataTable)
'            myDT = CType(value, nDB.Tables.tDeviceManager)
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
'    Protected Overrides Function CanSave() As Boolean
'        Me.EnableExtraButtons(True)
'        Return MyBase.CanSave()
'    End Function

'    Private Sub DeviceManagerInfo_Commited(ByVal sender As Object, ByVal e As InfoSet.CommitedEventArgs) Handles Me.Commited
'        mInitID = CInt(myDT.Get(Database.Tables.tDeviceManager.Field.ID))
'    End Sub
'End Class
