Option Explicit On
Option Strict On

Imports System.Linq
Imports System.Collections.Generic


Friend Class EmployeeAttendanceLogFileInfo
    Inherits InfoSet
    Private myDT As New Database.Tables.tEmployeeAttendanceLogFile(Connection)
    Private mtEmployeeAttendanceLog As GSCOM.SQL.ZDataTable
    Private mtEmployeeAttendanceLog_UnApplied As GSCOM.SQL.ZDataTable
    Private mControl As New InSys.DataControl
    Private mDecryptButton As ToolStripButton
    Private mImportButton As ToolStripButton
    Private mImportXMLButton As ToolStripButton
    Private mSaveUnappliedLogs As ToolStripButton


    Private mGrid As DataGridView
    Private mGrid2 As DataGridView

    Public Sub New(ByVal pMenu As Integer, ByVal c As SqlClient.SqlConnection, ByVal pListing As DataTable, ByVal pID As Integer)
        MyBase.New(c, pListing, pID)
        With mDataset.Tables
            .Add(myDT)
        End With
        InitControl(pMenu)
        mtEmployeeAttendanceLog = DirectCast(mDataset.Tables("tEmployeeAttendanceImportLogs"), GSCOM.SQL.ZDataTable)

        'mtEmployeeAttendanceLog_UnApplied = DirectCast(mDataset.Tables("tEmployeeAttendanceLogFile_Unapplied"), GSCOM.SQL.ZDataTable)
        'mDecryptButton = Me.GetStripButton("Decrypt Log File")
        'AddHandler mDecryptButton.Click, AddressOf DecryptLogFile
        mImportButton = Me.GetStripButton("Import Log File")
        'AddHandler mImportButton.Click, AddressOf ImportLogFile
        'mImportXMLButton = Me.GetStripButton("Import XML File")
        'AddHandler mImportXMLButton.Click, AddressOf ImportXMLFile
        'mSaveUnappliedLogs = Me.GetStripButton("Save UnApplied Logs")
        'AddHandler mSaveUnappliedLogs.Click, AddressOf SaveUnAppliedLogs
        AddHandler Me.GetStripButton("Generate Template").Click, AddressOf GenTemplate
        AddHandler Me.GetStripButton("Import File").Click, AddressOf ImportLogFile 'ImportFile

        Me.ReloadAfterCommit = True

        AfterNew()
        mGrid = Me.GetDataGridView(mtEmployeeAttendanceLog)
        'With mGrid
        '    .ReadOnly = True
        '    .AllowUserToAddRows = False
        '    .AllowUserToDeleteRows = False
        'End With

        'mGrid2 = Me.GetDataGridView(mtEmployeeAttendanceLog_UnApplied)
        'mGrid2.Columns("ID_AttendanceLogType").HeaderCell.Value = "Attendance Log Type"
        'mGrid2.Columns("ID_EmployeeAttendanceLogCreditDate").HeaderCell.Value = "Employee AttendanceLog CreditDate"
    End Sub

#Region "LoadInfo"
    Public Overrides Sub LoadInfo(ByVal pID As Integer)
        'mtEmployeeAttendanceLog.ClearThenFill(Database.Tables.tEmployeeAttendanceLog.Field.ID_EmployeeAttendanceLogFile.ToString & "=" & pID.ToString)
        MyBase.LoadInfo(pID)
        'mImportButton.Enabled = Not (mtEmployeeAttendanceLog.Rows.Count > 0)
        'mImportXMLButton.Enabled = Not (mtEmployeeAttendanceLog.Rows.Count > 0)

        Me.GetStripButton("Generate Template").Enabled = True
        'mPostButton.Enabled = Not CBool(myDT.Get(Database.Tables.tEmployeeAttendanceLogFile.Field.IsPosted)) AndAlso (mtEmployeeAttendanceLog.Rows.Count > 0)
    End Sub
#End Region

    Private mTextFormat As Integer = 1


#Region "ImportFile"

    Private Sub ImportLogFile(ByVal sender As Object, ByVal e As EventArgs)
        Dim sa() As String


        Dim s As String = ""
        Dim ofd As New OpenFileDialog
        Dim qry As String = ""
        Dim i As Integer

        Dim vAccessNo As Integer
        Dim q As String


        ofd.Filter = "Excel Files|*.xls;*.xlsx|Log Files|*.log;*.InSys;*.dat"
        ofd.FilterIndex = 0
        ofd.CheckFileExists = True

        Try

            If ofd.ShowDialog() = Windows.Forms.DialogResult.OK Then

                Select Case System.IO.Path.GetExtension(ofd.FileName).ToUpper
                    Case ".XLS", ".XLSX"
                        TransferExcelData(ofd.FileName)
                    Case Else

                        'BeginProcess("Testing " & ofd.FileName)
                        'If GSCOM.Common.EncryptedA(ofd.FileName, Asc("A"), s) Then
                        '    DecryptLogFile(ofd.FileName, s)
                        'End If

                        BeginProcess("Reading " & ofd.FileName)

                        s = IO.Path.GetFileName(ofd.FileName)
                        myDT.Set(Database.Tables.tEmployeeAttendanceLogFile.Field.Name, s)
                        s = IO.Path.GetDirectoryName(ofd.FileName)
                        myDT.Set(Database.Tables.tEmployeeAttendanceLogFile.Field.Path, s)

                        sa = IO.File.ReadAllLines(ofd.FileName)

                        Dim dr As DataRow
                        mGrid.DataSource = Nothing

                        Me.BeginProcess("Importing File")
                        Dim d As Date

                        Dim strd As String = ""
                        Dim a As String()
                        ' Dim ctr As Integer

                        'Dim o As Object
                        'o = GSCOM.SQL.ExecuteScalar("SELECT ID_LogFileFormat FROM tCompany c WHERE c.ID=" & GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tEmployeeAttendanceLogFile.Field.ID_Company)), gConnection)
                        'If IsNothing(o) OrElse (o Is DBNull.Value) Then
                        '    mTextFormat = 1
                        'Else
                        '    mTextFormat = CInt(o)
                        'End If



                        Select Case System.IO.Path.GetExtension(ofd.FileName).ToUpper
                            Case ".LOG" '1 'Default (FSDM)

                                BeginProcess("Testing " & ofd.FileName)
                                If GSCOM.Common.EncryptedA(ofd.FileName, Asc("A"), s) Then
                                    DecryptLogFile(ofd.FileName, s)
                                End If

                                'Baka Encrypted
                                Dim strencryptChar() As String = {"*", "{", ""}
                                If sa.Length = 1 Then
                                    Dim k As String = (From j In strencryptChar
                                                      Where sa.Contains(j) = True
                                                      Select j).SingleOrDefault
                                    If IsNothing(k) = False Then
                                        If GSCOM.Common.EncryptedA(ofd.FileName, Asc("A"), s) Then
                                            DecryptLogFile(ofd.FileName, s)
                                            sa = IO.File.ReadAllLines(ofd.FileName)
                                        End If
                                    End If
                                End If

                                sa = ModifyTextFormat(sa, ofd.FileName)
                                sa = CType(GSCOM.Common.GetDistinctStrings(sa).ToArray(GetType(String)), String())

                                'Dim vOk As Boolean
                                For Each s In sa
                                    a = s.Trim.Split(CChar(","))
                                    i += 1
                                    Me.SetStatusLabel("Processing " & i.ToString & " of " & sa.Length & " (" & (i * 100 \ sa.Length) & "%)")
                                    dr = mtEmployeeAttendanceLog.AddRow

                                    vAccessNo = CInt(a(0).ToString)
                                    dr(Database.Tables.tEmployeeAttendanceLog.Field.AccessNo.ToString) = vAccessNo

                                    Dim da As String
                                    da = a(1) + "," + a(2)
                                    d = CDate(da)
                                    dr(Database.Tables.tEmployeeAttendanceLog.Field.DateTime.ToString) = d
                                    dr(Database.Tables.tEmployeeAttendanceLog.Field.ID_AttendanceLogType.ToString) = IIf((a(3).ToString = "I"), 1, 2)
                                    If CBool(nDB.GetSetting(Database.SettingEnum.AutoDetectAttendanceLogType)) Then
                                        dr(Database.Tables.tEmployeeAttendanceLog.Field.ID_AttendanceLogType.ToString) = 1

                                        'q = "AccessNo=" & GSCOM.SQL.SQLFormat(vAccessNo) & " AND DateTime=" & GSCOM.SQL.SQLFormat(d)
                                        'vOk = mtEmployeeAttendanceLog.Select(q).Length = 1
                                        'If Not vOk Then
                                        '    mtEmployeeAttendanceLog.Rows.Remove(dr)
                                        'End If

                                        If (From j As DataRow In mtEmployeeAttendanceLog.AsEnumerable()
                                               Where CInt(j("AccessNo")) = vAccessNo AndAlso CDate(j("DateTime")) = d
                                               Select j).Count() = 0 Then
                                            mtEmployeeAttendanceLog.Rows.Remove(dr)
                                        End If

                                    End If
                                    Application.DoEvents()
                                Next
                            Case ".INSYS" '2 'FSCAN Log

                                Dim logs() As String = System.IO.File.ReadAllLines(ofd.FileName)
                                logs = CType(GSCOM.Common.GetDistinctStrings(logs).ToArray(GetType(String)), String())

                                Dim vOk As Boolean
                                For Each s In sa
                                    a = s.Trim.Split(CChar(vbTab))
                                    i += 1
                                    Me.SetStatusLabel("Processing " & i.ToString & " of " & sa.Length & " (" & (i * 100 \ sa.Length) & "%)")
                                    dr = mtEmployeeAttendanceLog.AddRow

                                    vAccessNo = CInt(a(1).ToString)
                                    dr(Database.Tables.tEmployeeAttendanceLog.Field.AccessNo.ToString) = vAccessNo

                                    d = CDate(a(2))
                                    dr(Database.Tables.tEmployeeAttendanceLog.Field.DateTime.ToString) = d
                                    dr(Database.Tables.tEmployeeAttendanceLog.Field.ID_AttendanceLogType.ToString) = a(3)
                                    If CBool(nDB.GetSetting(Database.SettingEnum.AutoDetectAttendanceLogType)) Then
                                        dr(Database.Tables.tEmployeeAttendanceLog.Field.ID_AttendanceLogType.ToString) = 1
                                        q = "AccessNo=" & GSCOM.SQL.SQLFormat(vAccessNo) & " AND DateTime=" & GSCOM.SQL.SQLFormat(d)
                                        vOk = mtEmployeeAttendanceLog.Select(q).Length = 1
                                        If Not vOk Then
                                            mtEmployeeAttendanceLog.Rows.Remove(dr)
                                        End If

                                    End If
                                    Application.DoEvents()
                                Next

                            Case ".DAT" ' 3 'Device Log
                                Dim logs() As String = System.IO.File.ReadAllLines(ofd.FileName)
                                logs = CType(GSCOM.Common.GetDistinctStrings(logs).ToArray(GetType(String)), String())

                                For Each s In sa
                                    a = s.Trim.Split(CChar(vbTab))
                                    i += 1
                                    Me.SetStatusLabel("Processing " & i.ToString & " of " & sa.Length & " (" & (i * 100 \ sa.Length) & "%)")
                                    dr = mtEmployeeAttendanceLog.AddRow

                                    vAccessNo = CInt(a(0).ToString)
                                    dr(Database.Tables.tEmployeeAttendanceLog.Field.AccessNo.ToString) = vAccessNo

                                    d = CDate(a(1))
                                    dr(Database.Tables.tEmployeeAttendanceLog.Field.DateTime.ToString) = d

                                    Dim inout As Int32
                                    If CInt(a(3)) = 0 Then
                                        inout = 1
                                    Else
                                        inout = 2
                                    End If

                                    dr(Database.Tables.tEmployeeAttendanceLog.Field.ID_AttendanceLogType.ToString) = inout
                                    If CBool(nDB.GetSetting(Database.SettingEnum.AutoDetectAttendanceLogType)) Then
                                        dr(Database.Tables.tEmployeeAttendanceLog.Field.ID_AttendanceLogType.ToString) = 1
                                        q = "AccessNo=" & GSCOM.SQL.SQLFormat(vAccessNo) & " AND DateTime=" & GSCOM.SQL.SQLFormat(d)

                                        If (From j As DataRow In mtEmployeeAttendanceLog.AsEnumerable()
                                                Where CInt(j("AccessNo")) = vAccessNo AndAlso CDate(j("DateTime")) = d
                                                Select j).Count() = 0 Then

                                            mtEmployeeAttendanceLog.Rows.Remove(dr)
                                        End If
                                    End If
                                    Application.DoEvents()
                                Next

                        End Select
                        mGrid.DataSource = mtEmployeeAttendanceLog
                        'Me.mImportButton.Enabled = False
                        ' Me.mImportXMLButton.Enabled = False
                        Me.EndProcess("Done")
                End Select
            End If
        Catch ex As Exception
            Throw ex
        End Try


    End Sub

    Private Function ModifyTextFormat(ByVal sa As String(), ByVal FileName As String) As String()
        Dim s As String
        Dim b As String() = Nothing
        Dim i As Integer
        Try
            'must check first the supposed file format
            'must revise to remove duplicate codes. see ImportLogFile
            Dim m As Integer
            Dim o As Object
            o = GSCOM.SQL.ExecuteScalar("SELECT ID_LogFileFormat FROM tCompany c WHERE c.ID=" & GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tEmployeeAttendanceLogFile.Field.ID_Company)), gConnection)
            If o Is DBNull.Value Then
                m = 1
            Else
                m = CInt(o)
            End If
            Select Case m
                Case 1
                    sa = IO.File.ReadAllLines(FileName)
                    For i = 0 To sa.Length - 1
                        If sa(i).Length <> 28 Then
                            b = sa(i).Split(","c)
                            b(0) = Format(CType(b(0), Integer), "000000")
                            b(1) = "," & Format(CType(b(1), DateTime), "MM/dd/yyyy")
                            b(2) = "," & Format(CType(b(2), DateTime), "HH:mm:00")
                            b(3) = ",I"
                            s = b(0) & b(1) & b(2) & b(3)
                            sa(i) = s
                        End If
                    Next
                    Return sa
                Case 7 ' Onesimus
                    Dim modsa(sa.Length - 1) As String

                    For i = 0 To sa.Length - 1

                        s = sa(i)

                        modsa(i) = Strings.Left(s, 28)

                        For a As Integer = sa.Length - 1 To 0 Step -1
                            If modsa(i) = Strings.Left(sa(a), 28) Then
                                modsa(i) &= Mid(sa(a), 30, 1)
                            End If
                        Next


                    Next
                    Return modsa

                Case Else
                    Return sa
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
            Return Nothing
        End Try
    End Function

    Private Sub ImportXMLFile(ByVal sender As Object, ByVal e As EventArgs)
        Dim sa() As String
        Dim s As String
        Dim ofd As New OpenFileDialog
        Dim qry As String = ""
        Dim i As Integer
        ofd.Filter = "XML Files (*.xml)|*.xml|All Files|*.*"
        If ofd.ShowDialog() = Windows.Forms.DialogResult.OK Then
            s = IO.Path.GetFileName(ofd.FileName)

            myDT.Set(Database.Tables.tEmployeeAttendanceLogFile.Field.Name, s)
            s = IO.Path.GetDirectoryName(ofd.FileName)
            myDT.Set(Database.Tables.tEmployeeAttendanceLogFile.Field.Path, s)

            sa = IO.File.ReadAllLines(ofd.FileName)
            sa = CType(GSCOM.Common.GetDistinctStrings(sa).ToArray(GetType(String)), String())
            sa(0) = ""
            sa(1) = ""
            sa(sa.Length - 1) = ""
            For i = 2 To sa.Length - 2
                s = sa(i)
                sa(i) = Replace(s, ":data", "")

            Next

            Dim ds As New DataSet
            Dim f As String
            f = IO.Path.GetTempFileName
            IO.File.WriteAllLines(f, sa)

            ds.ReadXml(f)

            Dim dr As DataRow
            mGrid.DataSource = Nothing
            Me.BeginProcess("Importing File")

            For Each drxml As DataRow In ds.Tables(0).Rows
                i += 1
                Me.SetStatusLabel("Processing " & i.ToString & " of " & sa.Length & " (" & (i * 100 \ sa.Length) & "%)")
                dr = mtEmployeeAttendanceLog.AddRow
                dr(Database.Tables.tEmployeeAttendanceLog.Field.AccessNo.ToString) = CInt(drxml("PIN").ToString())
                dr(Database.Tables.tEmployeeAttendanceLog.Field.DateTime.ToString) = CDate(drxml("time_second").ToString())
                dr(Database.Tables.tEmployeeAttendanceLog.Field.ID_AttendanceLogType.ToString) = CInt(drxml("status").ToString()) + 1
                'dr(Database.Tables.tEmployeeAttendanceLog.Field.ID_Employee.ToString) = 1 'temporary: just for constraint. to be updated by a stored proc 
                Application.DoEvents()
            Next
            mGrid.DataSource = mtEmployeeAttendanceLog
            Me.mImportButton.Enabled = False
            Me.mImportXMLButton.Enabled = False
            Me.EndProcess("Done")
        End If
    End Sub



#End Region

#Region "ValidateFile"
    Private Sub ValidateFile()
        Dim i As Integer
        Me.BeginProcess("Validating File")
        Dim dra() As DataRow
        Dim vAccessNo As String
        Dim vDateTime As String
        Dim vID_AttendanceLogType As String
        Dim o As Object
        Dim s As String
        dra = mtEmployeeAttendanceLog.Select("", "", DataViewRowState.Added) 'should consider modifieds
        mGrid.DataSource = Nothing

        For Each dr As DataRow In dra
            i += 1
            Me.SetStatusLabel("Processing " & i.ToString & " of " & dra.Length & " (" & (i * 100 \ dra.Length) & "%)")
            vAccessNo = GSCOM.SQL.SQLFormat(dr.Item(Database.Tables.tEmployeeAttendanceLog.Field.AccessNo.ToString))
            vDateTime = GSCOM.SQL.SQLFormat(dr.Item(Database.Tables.tEmployeeAttendanceLog.Field.DateTime.ToString))
            'If CBool(myDT.Get(Database.Tables.tEmployeeAttendanceLogFile.Field.AutoDetectAttendanceLogType)) Then
            If CBool(nDB.GetSetting(Database.SettingEnum.AutoDetectAttendanceLogType)) Then
                vID_AttendanceLogType = GSCOM.SQL.SQLFormat(DBNull.Value)
            Else
                vID_AttendanceLogType = GSCOM.SQL.SQLFormat(dr.Item(Database.Tables.tEmployeeAttendanceLog.Field.ID_AttendanceLogType.ToString))
            End If
            s = "SELECT dbo.fGetEmployeeAttendanceLogID(" & vAccessNo & "," & vDateTime & "," & vID_AttendanceLogType & ")"
            o = GSCOM.SQL.ExecuteScalar(s, Connection)
            If Not IsDBNull(o) Then
                dr.Delete()
            End If
            Application.DoEvents()
        Next

        mGrid.DataSource = mtEmployeeAttendanceLog
        Me.EndProcess("Done validating")
    End Sub
#End Region

#Region "Overrides"
    Protected Overrides Property Table() As GSCOM.SQL.ZDataTable
        Get
            Return myDT
        End Get
        Set(ByVal value As GSCOM.SQL.ZDataTable)
            myDT = CType(value, Database.Tables.tEmployeeAttendanceLogFile)
        End Set
    End Property



#End Region

    Private Sub EmployeeAttendanceLogFileInfo_Commited(ByVal sender As Object, ByVal e As CommitedEventArgs) Handles Me.Commited
        Dim s As String
        s = "EXEC pEmployeeAttendaceLogImport " & GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tEmployeeAttendanceLogFile.Field.ID))
        GSCOM.SQL.ExecuteNonQuery(s, Connection, 0)
    End Sub

    Protected Overrides Function CanSave() As Boolean
        ValidateFile()
        Return MyBase.CanSave()
    End Function

    Private Sub DecryptLogFile(ByVal sender As Object, ByVal e As EventArgs)
        DecryptLogFile()
    End Sub

    Private Sub DecLog()
    End Sub

#Region "DecryptLogFile"
    'same with PayboxT4.MainForm.DecryptLogFile
    Private Function DecryptLogFile() As Boolean
        Dim s As String = ""
        Dim fnum As Integer
        Dim ofd As New OpenFileDialog
        Dim c As Integer = Asc("A")
        Try
            ofd.Filter = "Log Files (*.log)|*.log|All Files|*.*"
            If ofd.ShowDialog() = Windows.Forms.DialogResult.OK Then
                BeginProcess("Testing " & ofd.FileName & "..")
                If GSCOM.Common.EncryptedA(ofd.FileName, c, s) Then
                    DecryptLogFile(ofd.FileName, s)
                Else
                    EndProcess("Can not continue. The file is modified", False)
                End If
            End If
        Catch ex As Exception
            EndProcess(ex.Message, False)
        Finally
            FileClose(fnum)
        End Try
    End Function

    Private Function DecryptLogFile(ByVal pFileName As String, ByVal vData As String) As Boolean
        Dim fnum As Integer
        Dim c As Integer = Asc("A")
        Try
            BeginProcess("Decrypting " & pFileName & "..")
            vData = GSCOM.Common.EncryptA(vData, c)
            FileSystem.SetAttr(pFileName, FileSystem.GetAttr(pFileName) And (Not FileAttribute.ReadOnly))
            fnum = FreeFile()
            FileOpen(fnum, pFileName, OpenMode.Output, OpenAccess.Write)
            FileSystem.Print(fnum, vData)
            FileClose(fnum)
            FileSystem.SetAttr(pFileName, FileSystem.GetAttr(pFileName) Or (FileAttribute.ReadOnly))
            EndProcess(pFileName & " decrypted")
            DecryptLogFile = True
        Catch ex As Exception
            EndProcess(ex.Message, False)
        Finally
            FileClose(fnum)
        End Try
    End Function

#End Region


    Private Sub SaveUnAppliedLogs(ByVal sender As Object, ByVal e As EventArgs)
        Dim fnum As Integer = FreeFile()
        Dim sfd As New SaveFileDialog
        Dim s As String
        Dim AccessNo As Integer
        Dim vDate As Date
        Dim LogType As Integer
        Dim dt As GSCOM.SQL.ZDataTable
        dt = mtEmployeeAttendanceLog_UnApplied
        Try
            sfd.Filter = "Log File  s (*.log)|*.log|All Files|*.*"
            If sfd.ShowDialog() = Windows.Forms.DialogResult.OK Then
                BeginProcess("Saving " & sfd.FileName & "..")
                For Each dr As DataRow In dt.Select

                    AccessNo = CInt(dr.Item(Database.Tables.tEmployeeAttendanceLogFile_Unapplied.Field.AccessNo.ToString))
                    vDate = CType(dr(Database.Tables.tEmployeeAttendanceLogFile_Unapplied.Field.DateTime.ToString), Date)
                    LogType = CType(dr(Database.Tables.tEmployeeAttendanceLogFile_Unapplied.Field.ID_AttendanceLogType.ToString), Integer)

                    s = Format(AccessNo, "0000000") & "," & Format(vDate, "MM/dd/yyyy,HH:mm:ss") & "," & IIf(LogType = 1, "I", "O").ToString
                    FileOpen(fnum, sfd.FileName, OpenMode.Append, OpenAccess.Write)
                    FileSystem.PrintLine(fnum, s)
                    FileSystem.FileClose(fnum)
                Next
                FileSystem.SetAttr(sfd.FileName, FileSystem.GetAttr(sfd.FileName) Or (FileAttribute.ReadOnly))
                EndProcess(sfd.FileName & " saved")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

#Region "Exce File"
    Private Sub GenTemplate(ByVal sender As Object, ByVal e As EventArgs)
        Dim vScheduleTable As New GSCOM.Applications.InSys.Database.Templates.LeaveFileTable
        'Dim dr As DataRow
        Dim sfd As New SaveFileDialog
        Dim a As New GSCOM.Applications.InSys.Database.Templates.LeaveFileAdapter

        sfd.FileName = myDT.Get(Database.Tables.tEmployeeAttendanceLogFile.Field.Name).ToString & ".xls"
        sfd.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*"
        sfd.FilterIndex = 0

        If sfd.ShowDialog = Windows.Forms.DialogResult.OK Then
            IO.File.Copy(nDB.GetSetting(Database.SettingEnum.ExcelTemplatePath) & "Employee Attendance Log File.xls", sfd.FileName, True)
            a.DataSource = sfd.FileName 'initialize datasource (filename)
            Dim dt As New DataTable
            Dim s As String = Me.PassParameters("SELECT '''' + AccessNo,Name FROM dbo.fSessionEmployee(" & nDB.GetUserID & "," & CInt(nDB.GetCompanyID) & ") where IsActive = 1")

            Dim HDT As DataTable = GSCOM.SQL.TableQuery("SELECT 'AccessNo', 'Employee Name'", Connection)




            dt = GSCOM.SQL.TableQuery(s, Connection)
            AddToExcelTemplate(sfd.FileName, HDT, dt, "A", "B", "Access No")
            MsgBox("Done", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub ImportFile(ByVal sender As Object, ByVal e As EventArgs)
        Dim MyDialog As New OpenFileDialog()
        MyDialog.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*"
        MyDialog.FilterIndex = 0
        MyDialog.CheckFileExists = True
        MyDialog.CheckPathExists = True
        If (MyDialog.ShowDialog() = DialogResult.OK) Then
            TransferExcelData(MyDialog.FileName)
        End If
    End Sub

    Private Sub TransferExcelData(ByVal FileName As String)
        Dim dt As New DataTable
        'Dim i As Integer
        Dim bFooterFound As Boolean = False
        Try
            Me.BeginProcess("Transferring from excel file, please wait...")
            mGrid.DataSource = Nothing  'mtScheduleFile_Detail.Clear()
            Dim s As String
            s = IO.Path.GetFileName(FileName)
            myDT.Set(Database.Tables.tEmployeeAttendanceLogFile.Field.Name, s)

            s = GetSelectString()

            Using dtattlog As DataTable = GetAttendanceTable()

                GSCOM.SQL.GetExcelTable(FileName, "Sheet1", dtattlog, s) 'mtEmployeeAttendanceLog

                For Each dr As DataRow In dtattlog.Rows
                    Dim idr As DataRow = mtEmployeeAttendanceLog.AddRow
                    idr("Source") = dr("Source")
                    idr("DateTime") = CDate(String.Format("{0} {1}", CDate(dr("Date")).ToShortDateString(), CDate(dr("Time")).ToShortTimeString))
                    idr("AccessNo") = dr("AccessNo")
                    idr("ID_AttendanceLogType") = 1
                    If idr.RowState = DataRowState.Unchanged Then ' SetAdded is for Unchangeds only
                        idr.SetAdded()
                    End If
                Next
            End Using

            'mtEmployeeAttendanceLog.AcceptChanges()

            'For Each dr As DataRow In mtEmployeeAttendanceLog.Select()
            '    dr.Item(4) = CDate(Left(dr.Item(12).ToString, dr.Item(12).ToString.IndexOf(" ")) & Right(dr.Item(13).ToString, (Len(dr.Item(13).ToString) - (dr.Item(13).ToString.IndexOf(" ")))))
            '    If dr.RowState = DataRowState.Unchanged Then ' SetAdded is for Unchangeds only
            '        dr.SetAdded()
            '    End If
            'Next

            'mtEmployeeAttendanceLog.Columns.Remove("Time")
            'mtEmployeeAttendanceLog.Columns.Remove("Date")

            mGrid.DataSource = mtEmployeeAttendanceLog
            Me.EndProcess()

        Catch ex As Exception
            Me.EndProcess(ex.Message, False)
        End Try
    End Sub

    Private Function GetAttendanceTable() As DataTable
        Dim dt As New DataTable("EmployeeAttendanceLog")
        dt.Columns.Add("Source", GetType(String))
        dt.Columns.Add("AccessNo", GetType(Int32))
        dt.Columns.Add("Date", GetType(String))
        dt.Columns.Add("Time", GetType(String))
        Return dt
    End Function

    Private Function GetSelectString() As String
        Dim s As String
        s = Database.Tables.tEmployeeAttendanceLog.Field.Source.ToString
        s &= ", " & Database.Tables.tEmployeeAttendanceLog.Field.AccessNo.ToString
        s &= ", " & Database.Tables.tEmployeeAttendanceLog.Field.Date.ToString
        s &= ", Time" '& Database.Tables.tEmployeeAttendanceLog.Field.DateTime.ToString
        Return s
    End Function
#End Region

End Class

'Private Sub Post(ByVal sender As Object, ByVal e As EventArgs)
'    Try
'        Dim s As String = "EXEC pEmployeeAttendanceLogFile_Post " & GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tEmployeeAttendanceLogFile.Field.ID))
'        Me.BeginProcess("Posting")
'        GSCOM.SQL.ExecuteNonQuery(s, Connection, 0)
'        mPostButton.Enabled = False
'        Me.EndProcess("Posted successfully")
'    Catch ex As Exception
'        MsgBox(ex.Message, MsgBoxStyle.Exclamation)
'    End Try
'End Sub

'Private Sub Void(ByVal sender As Object, ByVal e As EventArgs)
'    If MsgBox("Do you want to void attendance file?", CType(MsgBoxStyle.Question + MsgBoxStyle.YesNo, MsgBoxStyle)) = MsgBoxResult.Yes Then
'        GSCOM.SQL.ExecuteNonQuery("EXEC pImport_Attendance_Void " & myDT.Get(Database.Tables.tEmployeeAttendanceLogFile.Field.ID).ToString, Connection)
'        LoadInfo(CInt(myDT.Get(Database.Tables.tEmployeeAttendanceLogFile.Field.ID)))
'        MsgBox("Finish voiding attendance.", MsgBoxStyle.Information)
'    End If
'End Sub

'Private Sub TransferExcelData(ByVal FileName As String)
'    Dim dt As New DataTable
'    Dim cn As System.Data.OleDb.OleDbConnection
'    Dim cmd As System.Data.OleDb.OleDbDataAdapter
'    Try
'        Me.BeginProcess("Transferring from excel file, please wait...")
'        cn = New System.Data.OleDb.OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0;" & _
'                    "data source=" & FileName & ";Extended Properties=Excel 8.0;")
'        cmd = New System.Data.OleDb.OleDbDataAdapter("SELECT * FROM [Sheet1$]", cn)
'        cn.Open()
'        cmd.Fill(mtEmployeeAttendanceLog)
'        For Each dr As DataRow In mtEmployeeAttendanceLog.Rows
'            'SetAdded is for Unchangeds only
'            If dr.RowState = DataRowState.Unchanged Then
'                dr.SetAdded()
'            End If
'        Next
'        cn.Close()
'        Me.EndProcess("")
'    Catch ex As Exception
'        Me.EndProcess("Error occur while importing data.", False)
'    End Try
'End Sub

'Private Sub ValidateFile(ByVal sender As Object, ByVal e As EventArgs)
'    ValidateFile()
'End Sub