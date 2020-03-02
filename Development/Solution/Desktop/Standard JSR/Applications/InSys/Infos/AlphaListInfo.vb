Option Explicit On
Option Strict On



Friend Class AlphaListInfo
    Inherits InfoSet

    Private myDT As New Database.Tables.tAlphalist(Connection)
    Private myDT_Alphalist_Detail As New Database.Tables.tAlphaList_Detail(Connection)
    Private mUserAlphalist As New Database.Tables.tUserPayrollPeriod(Connection)
    Private merAlphalist As New Database.Tables.tUserPayrollPeriod(Connection)
    Private mControl As New InSys.DataControl 'Private mControl As New nDB.AlphaListControl
    Private mPostButton As ToolStripButton
    Private mReportViewer As CrystalDecisions.Windows.Forms.CrystalReportViewer
    'Private mpostbutton As ToolStripButton
    Private mGenerateButton As ToolStripButton
    Private mGenerateFileButton As ToolStripButton
    Private mGenerateFileAllButton As ToolStripButton



#Region "New"
    Public Sub New(ByVal pMenu As Integer, ByVal c As SqlClient.SqlConnection, ByVal pListing As DataTable, ByVal pID As Integer)
        MyBase.New(c, pListing, pID)
        With mDataset.Tables
            .Add(myDT)
            .Add(myDT_Alphalist_Detail)
        End With
        Dim pdc As DataColumn
        Dim cdc As DataColumn
        Dim rel As DataRelation
        InitControl(pMenu)
        pdc = myDT.Columns(Database.Tables.tAlphaList.Field.ID)
        cdc = myDT_Alphalist_Detail.Columns(Database.Tables.tAlphaList_Detail.Field.ID_Alphalist)
        rel = mDataset.Relations.Add(pdc, cdc)

        mGenerateButton = MyBase.AddButton("Generate", gMainForm.imgList.Images("Generate.png"), AddressOf Generate)
        'Me.GetStripButton("Generate") 
        'AddHandler mGenerateButton.Click, AddressOf Generate

        mPostButton = Me.GetStripButton("Post")

        mGenerateFileButton = MyBase.AddButton("Generate File", gMainForm.imgList.Images("GenerateFile.png"), AddressOf Generate_TextFile)
        Me.GetStripButton("Generate File")
        AddHandler mGenerateFileButton.Click, AddressOf Generate_TextFile

        mGenerateFileAllButton = MyBase.AddButton("Generate All", gMainForm.imgList.Images("GenerateFile.png"), AddressOf Generate_TextFileAll)
        Me.GetStripButton("Generate File")
        AddHandler mGenerateFileAllButton.Click, AddressOf Generate_TextFileAll


        'mpostbutton = MyBase.AddButton("Post", gMainForm.imgList.Images("_post.png"), AddressOf )

        myDT.Columns(Database.Tables.tAlphaList.Field.ID_Company).DefaultValue = nDB.GetCompanyID
        mReportViewer = AddReportViewer("Report")
        mReportViewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
        Me.ReloadAfterCommit = True
        AfterNew()
    End Sub

#End Region

#Region "LoadInfo"
    Public Overrides Sub LoadInfo(ByVal pID As Integer)
        mGenerateFileAllButton.Enabled = Not pID = 0
        mGenerateFileButton.Enabled = Not pID = 0
        mGenerateButton.Enabled = Not pID = 0
        'myDT_Alphalist_Detail.ClearThenFill("ID_Alphalist=" & pID.ToString)
        MyBase.LoadInfo(pID)

        Dim AlphalistType As Integer = 3

        If Not IsDBNull(myDT.Get(Database.Tables.tAlphaList.Field.ID_AlphalistType)) Then
            AlphalistType = CInt(myDT.Get(Database.Tables.tAlphaList.Field.ID_AlphalistType).ToString)
        End If

        'CUSTOMIZED
        Dim dt As DataTable
        Dim s As String
        Dim sb As New System.Text.StringBuilder

        s = "SELECT * FROM vAlphaList_Report "
        s &= "WHERE ID_AlphaList=" & myDT.Get(Database.Tables.tAlphaList.Field.ID).ToString
        s &= " AND dbo.fEmployeeIsUnderUser(ID_Employee," & gUser & ") = 1"
        s &= " order by LastName "
        dt = GSCOM.SQL.TableQuery(s, Connection)

        sb.Append(" dbo.fEmployeeIsUnderUser(ID_Employee," & gUser & ") = 1")

        'myDT_Alphalist_Detail.ClearThenFill(sb.ToString)


        '    dt = GSCOM.SQL.TableQuery("SELECT * FROM fPayrollSummary_Alphalist(" & myDT.Get(Database.Tables.tAlphaList.Field.ID).ToString & ") a", Connection)
        Dim rd As New CrystalDecisions.CrystalReports.Engine.ReportDocument
        If AlphalistType = 1 Then
            rd.Load(nDB.GetSetting(Database.SettingEnum.ReportPath) & "Alphalist_7.1.rpt")
        ElseIf AlphalistType = 2 Then
            rd.Load(nDB.GetSetting(Database.SettingEnum.ReportPath) & "Alphalist_7.2.rpt")
        ElseIf AlphalistType = 4 Then
            rd.Load(nDB.GetSetting(Database.SettingEnum.ReportPath) & "Alphalist_7.4.rpt")
        Else
            rd.Load(nDB.GetSetting(Database.SettingEnum.ReportPath) & "Alphalist_7.3.rpt")
        End If
        '  rd.Load(nDB.GetSetting(Database.SettingEnum.ReportPath) & "Alphalist.rpt")
        rd.SetDataSource(dt)
        mReportViewer.ReportSource = rd
        mReportViewer.Zoom(1)

        If CBool(myDT.Get(Database.Tables.tAlphaList.Field.IsPosted)) Then
            Me.EnableButtons(False, pID)
        Else
            Me.EnableButtons(True, pID)
        End If


    End Sub
    Private Sub EnableButtons(ByVal cbol As Boolean, ByVal pID As Integer)

        mControl.Enabled = cbol
        Me.SaveButton.Enabled = cbol
        Me.GetStripButton("Post").Enabled = cbol
        mGenerateButton.Enabled = cbol

    End Sub
#End Region

    Private Sub Generate(ByVal sender As Object, ByVal e As EventArgs)
        If MsgBox("Generate?", CType(MsgBoxStyle.Question + MsgBoxStyle.YesNo, MsgBoxStyle)) = MsgBoxResult.Yes Then
            BeginProcess("Generating... Please wait.")
            GSCOM.SQL.ExecuteNonQuery("EXEC pPayroll_Alphalist " & myDT.Get(Database.Tables.tAlphaList.Field.ID).ToString, gConnection)

            Dim s As String
            Dim dt As DataTable
            Dim dra As DataRow()
            Dim i As Integer
            s = "Select * from tAlphalist_Detail Where ID_Alphalist = " & GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tAlphaList.Field.ID))
            dt = GSCOM.SQL.TableQuery(s, gConnection, True)
            dra = dt.Select
            'dito un

            For Each dr As DataRow In dra
                i += 1
                Me.SetStatusLabel("Processing " & i.ToString & " of " & dra.Length & " (" & (i * 100 \ dra.Length) & "%)")
                s = dr.Item(Database.Tables.tAlphaList_Detail.Field.ID.ToString).ToString
                s = "EXEC pPayroll_AlphalistUpdate " & s
                GSCOM.SQL.ExecuteNonQuery(s, gConnection)

                Try

                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
                Application.DoEvents()
            Next

            LoadInfo(CInt(myDT.Get(Database.Tables.tAlphaList.Field.ID)))
            Application.DoEvents()
            EndProcess("Done")
        End If
    End Sub

#Region "Overrides"
    Protected Overrides Property Table() As GSCOM.SQL.ZDataTable
        Get
            Return myDT
        End Get
        Set(ByVal value As GSCOM.SQL.ZDataTable)
            myDT = CType(value, Database.Tables.tAlphaList)
        End Set
    End Property



#End Region




#Region "GenerateTextFile"


    Private Sub Generate_TextFile(ByVal sender As Object, ByVal e As EventArgs)
        Dim MyDialog As New SaveFileDialog()
        Dim ExportFileName As String

        '   ExportFileName = GSCOM.SQL.ExecuteScalar("SELECT ISNULL(LEFT(REPLACE(c.TIN,'-',''),8),'') + CASE a.ID_AlphalistType WHEN 1 THEN 'S1' WHEN 2 THEN 'S2' WHEN 3 THEN 'S3' WHEN 4 THEN 'S4' END FROM tCompany c INNER JOIn tAlphalist a ON a.ID_Company = c.ID WHERE a.ID = " & myDT.Get(Database.Tables.tAlphaList.Field.ID).ToString, Connection).ToString
        'ExportFileName = GSCOM.SQL.ExecuteScalar("SELECT ISNULL(LEFT(REPLACE(c.TIN,'-',''),9),'') + '.' + CASE a.ID_AlphalistType WHEN 1 THEN 'S71' WHEN 2 THEN 'S72' WHEN 3 THEN 'S73' WHEN 4 THEN 'S74' END FROM tCompany c INNER JOIn tAlphalist a ON a.ID_Company = c.ID WHERE a.ID = " & myDT.Get(Database.Tables.tAlphaList.Field.ID).ToString, Connection).ToString
        ExportFileName = GSCOM.SQL.ExecuteScalar("SELECT REPLICATE('0',9-LEN(ISNULL(LEFT(REPLACE(c.TIN,'-',''),9),'') )) + ISNULL(LEFT(REPLACE(c.TIN,'-',''),9),'') + REPLICATE('0', 4 - (LEN(LEFT(ISNULL(c.BranchCode,''),3)))) +LEFT(ISNULL(c.BranchCode,''),3)  +CAST(DATEPART(mm,a.ReturnDAte) AS VARCHAR(4)) +CAST(DATEPART(dd,a.ReturnDAte) AS VARCHAR(4)) +CAST(DATEPART(yyyy,a.ReturnDAte) AS VARCHAR(6)) + '1604CF' FROM tCompany c INNER JOIn tAlphalist a ON a.ID_Company = c.ID WHERE a.ID =" & myDT.Get(Database.Tables.tAlphaList.Field.ID).ToString, Connection).ToString

        ' Dim ExportExtentionName As String = "CSV"

        ' MyDialog.Filter = "Default " & "(*." & ExportExtentionName & ")|*." & ExportExtentionName & "|All files (*.*)|*.*"     '|Text files (*.txt)|*.txt"
        '
        MyDialog.FileName = ExportFileName

        MyDialog.FilterIndex = 1
        MyDialog.CheckFileExists = False
        MyDialog.CheckPathExists = True
        If (MyDialog.ShowDialog() = DialogResult.OK) Then
            Save_File(MyDialog.FileName)
        End If
    End Sub

    Private Sub Generate_TextFileAll(ByVal sender As Object, ByVal e As EventArgs)
        Dim MyDialog As New SaveFileDialog()
        Dim ExportFileName As String

        '   ExportFileName = GSCOM.SQL.ExecuteScalar("SELECT ISNULL(LEFT(REPLACE(c.TIN,'-',''),8),'') + CASE a.ID_AlphalistType WHEN 1 THEN 'S1' WHEN 2 THEN 'S2' WHEN 3 THEN 'S3' WHEN 4 THEN 'S4' END FROM tCompany c INNER JOIn tAlphalist a ON a.ID_Company = c.ID WHERE a.ID = " & myDT.Get(Database.Tables.tAlphaList.Field.ID).ToString, Connection).ToString
        'ExportFileName = GSCOM.SQL.ExecuteScalar("SELECT ISNULL(LEFT(REPLACE(c.TIN,'-',''),9),'') + '.' + CASE a.ID_AlphalistType WHEN 1 THEN 'S71' WHEN 2 THEN 'S72' WHEN 3 THEN 'S73' WHEN 4 THEN 'S74' END FROM tCompany c INNER JOIn tAlphalist a ON a.ID_Company = c.ID WHERE a.ID = " & myDT.Get(Database.Tables.tAlphaList.Field.ID).ToString, Connection).ToString
        ExportFileName = GSCOM.SQL.ExecuteScalar("SELECT REPLICATE('0',9-LEN(ISNULL(LEFT(REPLACE(c.TIN,'-',''),9),'') )) + ISNULL(LEFT(REPLACE(c.TIN,'-',''),9),'') + REPLICATE('0', 4 - (LEN(LEFT(ISNULL(c.BranchCode,''),3)))) +LEFT(ISNULL(c.BranchCode,''),3)  +CAST(DATEPART(mm,a.ReturnDAte) AS VARCHAR(4)) +CAST(DATEPART(dd,a.ReturnDAte) AS VARCHAR(4)) +CAST(DATEPART(yyyy,a.ReturnDAte) AS VARCHAR(6)) + '1604CF' FROM tCompany c INNER JOIn tAlphalist a ON a.ID_Company = c.ID WHERE a.ID =" & myDT.Get(Database.Tables.tAlphaList.Field.ID).ToString, Connection).ToString

        ' Dim ExportExtentionName As String = "CSV"

        ' MyDialog.Filter = "Default " & "(*." & ExportExtentionName & ")|*." & ExportExtentionName & "|All files (*.*)|*.*"     '|Text files (*.txt)|*.txt"
        '
        MyDialog.FileName = ExportFileName

        MyDialog.FilterIndex = 1
        MyDialog.CheckFileExists = False
        MyDialog.CheckPathExists = True
        If (MyDialog.ShowDialog() = DialogResult.OK) Then
            Save_FileAll(MyDialog.FileName)
        End If
    End Sub

    Private Sub Save_File(ByVal FileName As String)
        Dim fnum As Integer
        'Dim s As String
        'Dim dr As DataRow
        Dim vNow As Date = nDB.GetServerDate
        Try
            If (Not (myDT_Alphalist_Detail Is Nothing)) Then
                If myDT_Alphalist_Detail.Rows.Count > 0 Then
                    fnum = FreeFile()
                    FileOpen(fnum, FileName, OpenMode.Output, OpenAccess.Write)

                    FileSystem.Print(fnum, Me.GetText(CInt(myDT.Get(Database.Tables.tAlphaList.Field.ID)), gConnection))

                    MsgBox("File has been exported successfully.", MsgBoxStyle.Information)
                Else
                    MsgBox("No record found.", MsgBoxStyle.Information)
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
        Finally
            FileClose(fnum)
        End Try
    End Sub
    Private Sub Save_FileAll(ByVal FileName As String)
        Dim fnum As Integer
        'Dim s As String
        'Dim dr As DataRow
        Dim vNow As Date = nDB.GetServerDate
        Try
            If (Not (myDT_Alphalist_Detail Is Nothing)) Then
                If myDT_Alphalist_Detail.Rows.Count > 0 Then
                    fnum = FreeFile()
                    FileOpen(fnum, FileName, OpenMode.Output, OpenAccess.Write)

                    FileSystem.Print(fnum, Me.GetTextAll(CInt(myDT.Get(Database.Tables.tAlphaList.Field.ID)), gConnection))

                    MsgBox("File has been exported successfully.", MsgBoxStyle.Information)
                Else
                    MsgBox("No record found.", MsgBoxStyle.Information)
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
        Finally
            FileClose(fnum)
        End Try
    End Sub


    'Add Overloads LJ 20140315
    Public Overloads Function GetText(ByVal pID As Integer, ByVal gConnection As SqlClient.SqlConnection) As String
        Dim s As String = ""
        Dim dt As New DataTable

        GSCOM.SQL.FillTable(dt, "EXEC pGenerateTextFileAlphalist " & pID.ToString, gConnection)
        For Each dr As DataRow In dt.Rows
            s &= dr(0).ToString & vbCrLf
        Next
        s = s.Trim
        Return s
    End Function

    Public Overloads Function GetTextAll(ByVal pID As Integer, ByVal gConnection As SqlClient.SqlConnection) As String
        Dim s As String = ""
        Dim dt As New DataTable

        GSCOM.SQL.FillTable(dt, "EXEC pGenerateTextFileAlphalist_Consolidated " & pID.ToString, gConnection)
        For Each dr As DataRow In dt.Rows
            s &= dr(0).ToString & vbCrLf
        Next
        s = s.Trim
        Return s
    End Function


#End Region

End Class

#Region "Old"
'Private Sub Navigate()
'    Dim f As String
'    Dim xw As Xml.XmlWriter
'    Dim t As String
'    Dim xs As New Xml.XmlWriterSettings()
'    Dim sb As New System.Text.StringBuilder
'    xs.Indent = True
'    f = IO.Path.GetTempFileName()
'    f = "d:\cc.xml"
'    xw = Xml.XmlWriter.Create(f, xs)
'    'xw = Xml.XmlWriter.Create(sb, xs)
'    t = "type=""text/xsl"" href=""" & gGetSetting(SettingEnum.StyleSheetPath) & "tPayrollPeriod.xsl"""
'    xw.WriteProcessingInstruction("xml-stylesheet", t)
'    'myDT_Gender.WriteXml(xw, True)
'    DataSet.WriteXml(xw)
'    'xw.Flush()
'    t = "<?xml version=""1.0"" encoding=""utf-8""?>" & vbCrLf
'    'mForm.Browser.Navigate(f)
'    't &= sb.ToString
'    t = "<?xml version=""1.0"" encoding=""Unicode""?>"
'    't = ""
'    t &= "<?xml-stylesheet type=""text/xsl"" href=""C:\Documents and Settings\Robbie\Desktop\GSCOM\Applications\Zurdo\StyleSheets\tPayrollPeriod.xsl""?>"
'    t &= DataSet.GetXml
'    mForm.Browser.DocumentText = t
'End Sub
'Private Sub RefreshListing()
'    Dim dt As DataTable
'    Dim s As String
'    'ROBBIE NOTE: set the primary key so merge function would be able to determine which record would be update
'    If mListing.PrimaryKey.Length = 0 Then
'        Dim keys(0) As DataColumn
'        keys(0) = mListing.Columns("ID")
'        mListing.PrimaryKey = keys
'    End If
'    s = GSCOM.SQL.SelectStatement(mListing)
'    s &= " WHERE ID=" & mDR("ID").ToString
'    dt = GSCOM.SQL.TableQuery(s, Connection)
'    'ROBBIE NOTE: set preservechanges to false to be able to reupdate the values
'    mListing.Merge(dt, False, MissingSchemaAction.Ignore)
'End Sub

'Private Sub InitBindings1()
'    Dim b As Binding
'    With protControl
'        .txtID.DataBindings.Add("Text", myDT, tPayrollPeriod.Field.ID.ToString)
'        .txtLastName.DataBindings.Add("Text", myDT, tPayrollPeriod.Field.LastName.ToString)
'        .txtFirstName.DataBindings.Add("Text", myDT, tPayrollPeriod.Field.FirstName.ToString)
'        .txtMiddleName.DataBindings.Add("Text", myDT, tPayrollPeriod.Field.MiddleName.ToString)
'        .mtbSSSNo.DataBindings.Add("Text", myDT, tPayrollPeriod.Field.SSSNo.ToString)
'        .mtbHDMFNo.DataBindings.Add("Text", myDT, tPayrollPeriod.Field.HDMFNo.ToString)
'        .mtbPhilHealthNo.DataBindings.Add("Text", myDT, tPayrollPeriod.Field.PhilHealthNo.ToString)
'        .cboID_Gender.DataBindings.Add("SelectedValue", myDT, tPayrollPeriod.Field.ID_Gender.ToString)
'        b = New Binding("Text", myDT, tPayrollPeriod.Field.BirthDate.ToString)
'        AddHandler b.Format, AddressOf GSCOM.EventDelegates.BindingFormatTextBox
'        AddHandler b.Parse, AddressOf GSCOM.EventDelegates.BindingParseTextBox
'        .txtBirthDate.DataBindings.Add(b)
'    End With

'End Sub
#End Region