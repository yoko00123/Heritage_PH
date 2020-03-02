Option Explicit On
Option Strict On



Friend Class ZReportViewer
    Inherits DataListControl

    Friend WithEvents mMainView As CrystalDecisions.Windows.Forms.CrystalReportViewer

    Public SubDataSource As DataTable

    Private chk As New ToolStripButton

#Region "New"
    Public Sub New()
        MyBase.New()
        mMainView = New CrystalDecisions.Windows.Forms.CrystalReportViewer
        mMainView.ActiveViewIndex = -1
        mMainView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        mMainView.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
        mMainView.ShowRefreshButton = False
        mMainView.Dock = System.Windows.Forms.DockStyle.Fill
        mMainView.Location = New System.Drawing.Point(0, 0)
        mMainView.Name = "MainView"
        mMainView.SelectionFormula = ""
        mMainView.Size = New System.Drawing.Size(332, 334)
        mMainView.TabIndex = 8
        mMainView.ViewTimeSelectionFormula = ""
        MyBase.ToolStripContainer1.ContentPanel.Controls.Add(mMainView)
        MyBase.ToolStripContainer1.ContentPanel.Controls.Add(MyBase.MainFilter)
        If IO.File.Exists(nDB.GetSetting(Database.SettingEnum.ResourcePath) + "\report.png") Then
            Dim bmp As Image
            bmp = Image.FromFile(nDB.GetSetting(Database.SettingEnum.ResourcePath) + "\report.png")
            chk.Image = bmp.GetThumbnailImage(32, 32, Nothing, IntPtr.Zero)
        End If
        chk.Text = "Prompt Report Parameters"
        chk.CheckOnClick = True
        chk.Alignment = ToolStripItemAlignment.Left
        AddHandler chk.Click, AddressOf chk_Clicked
        'MyBase.ToolStrip1.Items.Add("-")
        'MyBase.ToolStrip1.Items.Add(chk)
    End Sub

    Private Sub chk_Clicked(ByVal sender As Object, ByVal e As System.EventArgs)
        
    End Sub

#End Region



    Private mReportTitle As String

    Public Property ReportTitle() As String
        Get
            Return mReportTitle
        End Get
        Set(ByVal value As String)
            mReportTitle = value
        End Set
    End Property

    Public Sub btnCtrlEventClick(sender As ToolStripButton, e As GSCOM.Interfaces.ZIDataList.ButtonClickEventArgs) Handles Me.CtrlButtonClick
        If e.ButtonText = "Requery" Then
            InputReadOnlyForm.ShowMsg(Me.m_SelectStringBuild)
        End If
    End Sub

#Region "DataSource"

    Public Overrides Property DataSource() As DataTable
        Get
            DataSource = mDataSource
        End Get
        Set(ByVal Value As DataTable)
            Dim RptDoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
            Dim fn As String = ""

            Dim tmp As String = ""

            Try
                mDataSource = Value
                If mInited Then
                    If Not mDataSource Is Nothing Then
                        RptDoc = New CrystalDecisions.CrystalReports.Engine.ReportDocument
                        With RptDoc
                            fn = nDB.GetSetting(Database.SettingEnum.ReportPath) & mReportPath
                            If IO.File.Exists(fn) Then
                                tmp = System.IO.Path.Combine(System.IO.Path.GetTempPath, Guid.NewGuid().ToString() & ".rpt")
                                System.IO.File.Copy(fn, tmp)
                                .Load(tmp)
                            Else
                                MsgBox("Can not find " & fn & vbNewLine & "Make sure the file exists or the specified path is correct.", MsgBoxStyle.Exclamation)
                                Exit Property
                            End If
                            '.SetDatabaseLogon(gDBUser, gDBPassword) ', gDBDataSource, gDBInitialCatalog)
                            .SetDataSource(CType(mDataSource, DataTable))
                            Dim ds As New DataSet
                            Dim s As String
                            For Each dr As DataRow In SubDataSource.Rows
                                s = dr.Item("DataSource").ToString
                                s = Database.gPassParameters(nDB.gParameterTable, s)
                                s = Me.PassParameters(s)
                                Dim dtx As DataTable = GSCOM.SQL.TableQuery("SELECT * FROM " & s, gConnection)
                                dtx.TableName = dr.Item("Name").ToString
                                ds.Tables.Add(dtx)
                            Next
                            If ds.Tables.Count > 0 Then
                                For Each rd As CrystalDecisions.CrystalReports.Engine.ReportDocument In .Subreports
                                    For Each t As CrystalDecisions.CrystalReports.Engine.Table In .Database.Tables
                                        If ds.Tables.Contains(rd.Name) Then Exit For

                                        't.LogOnInfo.ConnectionInfo.DatabaseName = gDBInitialCatalog
                                        't.LogOnInfo.ConnectionInfo.ServerName = gDBDataSource
                                    Next
                                    If ds.Tables.Contains(rd.Name) Then
                                        rd.SetDataSource(ds.Tables(rd.Name))
                                    Else
                                        For Each dsc As CrystalDecisions.Shared.IConnectionInfo In rd.DataSourceConnections
                                            dsc.SetConnection(gDBDataSource, gDBInitialCatalog, gDBUser, gDBPassword)
                                        Next
                                        '.SetDatabaseLogon(gDBUser, gDBPassword) ', gDBDataSource, gDBInitialCatalog)
                                    End If
                                Next
                                Select Case TableName.ToLower
                                    Case ("vzPayrollRegister").ToLower, ("vzPayslip").ToLower
                                        '.RecordSelectionFormula = "{vzPayslip.ID_PayrollPeriod} = 1"
                                        Dim f As GSCOM.UI.DataFilter.LookUpFilter
                                        Dim o As Object = Nothing
                                        f = GetLookUpFilter("Pay Period")
                                        If f IsNot Nothing Then
                                            o = f.UpperFilter.SelectedValue
                                        End If
                                        s = "{vzPayslip.ID_PayrollPeriod} = "
                                        If o Is Nothing Then
                                            s &= "0"
                                        Else
                                            s &= GSCOM.SQL.SQLFormat(o)
                                        End If
                                        s &= " AND {vzPayslip.ID_User}= " & GSCOM.SQL.SQLFormat(nDB.GetUserID)
                                        s &= " AND {vzPayslip.EmployeeISUnderUser}=True"
                                        .RecordSelectionFormula = s

                                End Select
                            End If
                        End With
                        With RptDoc
                            Dim i As Integer
                            Dim o As Object
                            .SummaryInfo.ReportTitle = Me.mReportTitle
                            If Not chk.Checked Then
                                For Each f As CrystalDecisions.Shared.ParameterField In .ParameterFields
                                    i = .ParameterFields.IndexOf(f)
                                    Select Case f.Name.ToLower
                                        Case ("StartDate").ToLower
                                            o = Me.GetDateTimeFilter("Date").UpperFilter.Value
                                            .SetParameterValue(i, o)
                                        Case ("EndDate").ToLower
                                            o = Me.GetDateTimeFilter("Date").LowerFilter.Value
                                            .SetParameterValue(i, o)
                                        Case ("ReportTitle").ToLower
                                            o = ReportTitle
                                            .SetParameterValue(i, o)
                                        Case ("Prepared By").ToLower
                                            o = ""
                                            .SetParameterValue(i, o)
                                        Case ("Approved By").ToLower
                                            o = ""
                                            .SetParameterValue(i, o)
                                        Case ("Checked By").ToLower
                                            o = ""
                                            .SetParameterValue(i, o)
                                        Case Else
                                    End Select
                                Next
                            End If
                        End With
                        mMainView.ReportSource = RptDoc

                        mMainView.Zoom(1)  'rob
                    End If
                End If
            Catch ex As Exception
                If ex.Message = "Load report failed." Then
                    MsgBox("Can not find " & fn, MsgBoxStyle.Exclamation)
                End If
                Throw ex
            Finally
                If System.IO.File.Exists(tmp) Then
                    System.IO.File.Delete(tmp)
                End If

                GC.Collect()
            End Try
        End Set
    End Property

#End Region

#Region "SetAllDatabaseLogOn"

    Private Sub SetAllDatabaseLogOn( _
    ByVal pDocument As CrystalDecisions.CrystalReports.Engine.ReportDocument _
    , ByVal pUser As String _
    , ByVal pPassword As String _
    , ByVal pDataSource As String _
    , ByVal pInitialCatalog As String)
        pDocument.SetDatabaseLogon(pUser, pPassword, pDataSource, pInitialCatalog)
        For Each rpt As CrystalDecisions.CrystalReports.Engine.ReportDocument In pDocument.Subreports
            'SetAllDatabaseLogOn(rpt, pUser, pPassword, pDataSource, pInitialCatalog)
            rpt.SetDatabaseLogon(pUser, pPassword, pDataSource, pInitialCatalog)
        Next
    End Sub

#End Region

End Class
