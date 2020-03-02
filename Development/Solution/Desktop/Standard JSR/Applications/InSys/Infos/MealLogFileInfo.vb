Option Explicit On
Option Strict On



Friend Class MealLogFileInfo
    Inherits InfoSet

    Private myDT As New Database.Tables.tMealLogFile(Connection)
    Private mtMealLog As New Database.Tables.tMealLog(Connection)
    Private mControl As New InSys.DataControl
    Private mImportButton As ToolStripButton

    Private mGrid As DataGridView

    Public Sub New(ByVal pMenu As Integer, ByVal c As SqlClient.SqlConnection, ByVal pListing As DataTable, ByVal pID As Integer)
        MyBase.New(c, pListing, pID)
        With mDataset.Tables
            .Add(myDT)
            .Add(mtMealLog)
        End With
        Dim pdc As DataColumn
        Dim cdc As DataColumn
        Dim rel As DataRelation
        InitControl(pMenu)
        pdc = myDT.Columns(Database.Tables.tMealLogFile.Field.ID)
        cdc = mtMealLog.Columns(Database.Tables.tMealLog.Field.ID_MealLogFile)
        rel = mDataset.Relations.Add(pdc, cdc)
        myDT.Columns(Database.Tables.tMealLogFile.Field.ID_Company).DefaultValue = nDB.GetCompanyID

        mImportButton = MyBase.AddButton("Import Log File", gMainForm.imgList.Images("TextFile.ico"), AddressOf ImportLogFile)

        Me.ReloadAfterCommit = True
        myDT.Columns(Database.Tables.tMealLogFile.Field.ID_Company).DefaultValue = nDB.GetCompanyID
        AfterNew()
        mGrid = Me.GetDataGridView(mtMealLog)
        With mGrid
            .ReadOnly = True
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
        End With
    End Sub

#Region "LoadInfo"
    Public Overrides Sub LoadInfo(ByVal pID As Integer)
        MyBase.LoadInfo(pID)
        mImportButton.Enabled = Not (mtMealLog.Rows.Count > 0)
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
        ' Dim q As String


        ofd.Filter = "Log Files (*.log)|*.log|All Files|*.*"
        Try

            If ofd.ShowDialog() = Windows.Forms.DialogResult.OK Then

                BeginProcess("Testing " & ofd.FileName)
                If GSCOM.Common.EncryptedA(ofd.FileName, Asc("A"), s) Then
                    ' DecryptLogFile(ofd.FileName, s)
                End If

                BeginProcess("Reading " & ofd.FileName)


                s = IO.Path.GetFileName(ofd.FileName)
                myDT.Set(Database.Tables.tMealLogFile.Field.Name, s)
                s = IO.Path.GetDirectoryName(ofd.FileName)
                myDT.Set(Database.Tables.tMealLogFile.Field.Path, s)

                sa = IO.File.ReadAllLines(ofd.FileName)

                sa = ModifyTextFormat(sa, ofd.FileName)
                sa = CType(GSCOM.Common.GetDistinctStrings(sa).ToArray(GetType(String)), String())
                Dim dr As DataRow
                mGrid.DataSource = Nothing
                Me.BeginProcess("Importing File")
                Dim d As Date
                Dim strd As String = ""
                Dim a As String()
                Dim ctr As Integer

                Dim o As Object
                o = GSCOM.SQL.ExecuteScalar("SELECT ID_LogFileFormat FROM tCompany c WHERE c.ID=" & GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tMealLogFile.Field.ID_Company)), gConnection)
                If o Is DBNull.Value Then
                    mTextFormat = 1
                Else
                    mTextFormat = CInt(o)
                End If

                Select Case mTextFormat
                    Case 1
                        ' Dim vOk As Boolean
                        For Each s In sa
                            i += 1
                            Me.SetStatusLabel("Processing " & i.ToString & " of " & sa.Length & " (" & (i * 100 \ sa.Length) & "%)")
                            dr = mtMealLog.AddRow
                            vAccessNo = CInt(Strings.Mid(s, 1, 7))
                            dr(Database.Tables.tMealLog.Field.AccessNo.ToString) = vAccessNo
                            d = CDate(Strings.Mid(s, 8, 19))
                            dr(Database.Tables.tMealLog.Field.LogDateTime.ToString) = d
                            ' dr(Database.Tables.tEmployeeAttendanceLog.Field.ID_AttendanceLogType.ToString) = IIf((Strings.Mid(s, 29, 1) = "I"), 1, 2)
                            'dr(Database.Tables.tEmployeeAttendanceLog.Field.ID_Employee.ToString) = 1 'temporary: just for constraint. to be updated by a stored proc 
                            'If CBool(nDB.GetSetting(Database.SettingEnum.AutoDetectAttendanceLogType)) Then
                            '    dr(Database.Tables.tEmployeeAttendanceLog.Field.ID_AttendanceLogType.ToString) = 1
                            '    q = "AccessNo=" & GSCOM.SQL.SQLFormat(vAccessNo) & " AND DateTime=" & GSCOM.SQL.SQLFormat(d)
                            '    vOk = mtMealLog.Select(q).Length = 1
                            '    If Not vOk Then
                            '        mtMealLog.Rows.Remove(dr)
                            '    End If

                            'End If
                            Application.DoEvents()



                        Next
                    Case 2 'shopwise?
                        For Each s In sa
                            i += 1
                            Me.SetStatusLabel("Processing " & i.ToString & " of " & sa.Length & " (" & (i * 100 \ sa.Length) & "%)")
                            dr = mtMealLog.AddRow
                            dr(Database.Tables.tEmployeeAttendanceLog.Field.AccessNo.ToString) = CInt(Strings.Mid(s, 1, 8))
                            'd = CDate(Strings.Mid(s, 10, 8))
                            strd = Mid(s, 10, 4)
                            strd &= "/"
                            strd &= Mid(s, 14, 2)
                            strd &= "/"
                            strd &= Mid(s, 16, 2)
                            strd &= Mid(s, 18, 3)
                            strd &= ":"
                            strd &= Mid(s, 21, 2)
                            d = CDate(strd)
                            dr(Database.Tables.tEmployeeAttendanceLog.Field.DateTime.ToString) = d
                            dr(Database.Tables.tEmployeeAttendanceLog.Field.ID_AttendanceLogType.ToString) = IIf((Strings.Right(s, 1) = "1"), 1, 2)
                            Application.DoEvents()
                        Next
                    Case 3 'ABI
                        For Each s In sa
                            ctr = 1
                            a = s.Trim.Split(ChrW(Keys.Tab))

                            i += 1
                            Me.SetStatusLabel("Processing " & i.ToString & " of " & sa.Length & " (" & (i * 100 \ sa.Length) & "%)")
                            dr = mtMealLog.AddRow
                            dr(Database.Tables.tEmployeeAttendanceLog.Field.AccessNo.ToString) = a(0).ToString 'CInt(Strings.Mid(s, 1, 7))
                            'd = CDate(Strings.Mid(s, 9, 19))
                            dr(Database.Tables.tEmployeeAttendanceLog.Field.DateTime.ToString) = a(1).ToString
                            dr(Database.Tables.tEmployeeAttendanceLog.Field.ID_AttendanceLogType.ToString) = IIf((CDbl(a(3)) = 0), 1, 2)
                            'dr(Database.Tables.tEmployeeAttendanceLog.Field.ID_Employee.ToString) = 1 'temporary: just for constraint. to be updated by a stored proc 
                            Application.DoEvents()

                            'For Each b As String In a
                            '    If ctr = 2 Then
                            '        strd &= Format(CDate(b), "yyMMddHHmm").ToString.Trim
                            '    ElseIf ctr = 3 Then
                            '        strd &= ""
                            '    ElseIf ctr = 4 Then
                            '        strd &= IIf(b = "0", "I", "O").ToString.Trim
                            '    Else
                            '        If ctr < 4 Then
                            '            strd &= b.Trim.PadLeft(7, "0"c)
                            '        Else
                            '            Exit For
                            '        End If
                            '    End If
                            '    ctr += 1
                            'Next
                            strd &= vbCrLf
                        Next
                    Case 4 'Creative

                        For n As Integer = 0 To sa.Length - 1
                            s = Strings.Left(sa(n), 75)
                            sa(n) = s
                        Next
                        sa = CType(GSCOM.Common.GetDistinctStrings(sa).ToArray(GetType(String)), String())

                        For Each s In sa
                            If s <> "    USERID   Department Name           No.           Date/Time            C" Then
                                i += 1
                                Me.SetStatusLabel("Processing " & i.ToString & " of " & sa.Length & " (" & (i * 100 \ sa.Length) & "%)")
                                dr = mtMealLog.AddRow
                                dr(Database.Tables.tEmployeeAttendanceLog.Field.AccessNo.ToString) = CInt(Strings.Mid(s, 1, 10))
                                d = CDate(Strings.Mid(s, 54, 21))
                                dr(Database.Tables.tEmployeeAttendanceLog.Field.DateTime.ToString) = d
                                dr(Database.Tables.tEmployeeAttendanceLog.Field.ID_AttendanceLogType.ToString) = IIf((Strings.Mid(s, 75, 1) = "I"), 1, 2)
                                'dr(Database.Tables.tEmployeeAttendanceLog.Field.ID_Employee.ToString) = 1 'temporary: just for constraint. to be updated by a stored proc 
                                Application.DoEvents()
                            End If
                        Next
                    Case 5 'rfm
                        Dim Year As Integer
                        Year = DatePart(DateInterval.Year, Date.Now)

                        For Each s In sa
                            i += 1
                            Me.SetStatusLabel("Processing " & i.ToString & " of " & sa.Length & " (" & (i * 100 \ sa.Length) & "%)")
                            dr = mtMealLog.AddRow
                            dr(Database.Tables.tEmployeeAttendanceLog.Field.AccessNo.ToString) = CInt(Strings.Mid(s, 2, 6))
                            d = CDate(Mid(s, 17, 2) & "/" & Mid(s, 19, 2) & "/" & Year.ToString & " " & Mid(s, 21, 2) & ":" & Mid(s, 23, 2))
                            dr(Database.Tables.tEmployeeAttendanceLog.Field.DateTime.ToString) = d
                            dr(Database.Tables.tEmployeeAttendanceLog.Field.ID_AttendanceLogType.ToString) = CInt(Mid(s, 26, 1))
                            Application.DoEvents()
                        Next

                    Case 6 'NEW
                        Dim Year As Integer
                        Year = DatePart(DateInterval.Year, Date.Now)
                        For Each s In sa

                            a = s.Split(CChar(Microsoft.VisualBasic.vbTab))
                            i += 1
                            Me.SetStatusLabel("Processing " & i.ToString & " of " & sa.Length & " (" & (i * 100 \ sa.Length) & "%)")
                            dr = mtMealLog.AddRow
                            dr(Database.Tables.tEmployeeAttendanceLog.Field.AccessNo.ToString) = a(0)
                            d = CDate(Mid(s, 10, 2) & "/" & Mid(s, 13, 2) & "/" & Mid(s, 5, 4) & "," & Mid(s, 16, 2) & ":" & Mid(s, 19, 2) & ":" & "0")
                            dr(Database.Tables.tEmployeeAttendanceLog.Field.DateTime.ToString) = d 'CDate(Strings.Mid(s, 3, 19))
                            dr(Database.Tables.tEmployeeAttendanceLog.Field.ID_AttendanceLogType.ToString) = IIf((a(5) = "1"), 1, 2)
                        Next


                End Select
                mGrid.DataSource = mtMealLog
                Me.mImportButton.Enabled = False
                ' Me.mImportXMLButton.Enabled = False
                Me.EndProcess("Done")
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
            o = GSCOM.SQL.ExecuteScalar("SELECT ID_LogFileFormat FROM tCompany c WHERE c.ID=" & GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tMealLogFile.Field.ID_Company)), gConnection)
            If o Is DBNull.Value Then
                m = 1
            Else
                m = CInt(o)
            End If
            If m = 1 Then
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
            Else
                Return sa
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            Return Nothing
        End Try
    End Function





#End Region

#Region "Overrides"
    Protected Overrides Property Table() As GSCOM.SQL.ZDataTable
        Get
            Return myDT
        End Get
        Set(ByVal value As GSCOM.SQL.ZDataTable)
            myDT = CType(value, Database.Tables.tMealLogFile)
        End Set
    End Property

#End Region

End Class
