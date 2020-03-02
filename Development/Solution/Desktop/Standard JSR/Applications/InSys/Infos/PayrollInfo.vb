Option Explicit On
Option Strict On



Friend Class PayrollInfo
    Inherits InfoSet

    Private myDT As New Database.Tables.tPayroll(Connection)
    Private myDT_Payroll_Detail As DataTable ' New Database.Tables.tPayroll_Detail(Connection)
    Private mControl As New InSys.DataControl
    Private mHideZero As ToolStripButton
    Private mComputePayroll As ToolStripButton

    Public Sub New(ByVal pMenu As Integer, ByVal c As SqlClient.SqlConnection, ByVal pListing As DataTable, ByVal pID As Integer)
        MyBase.New(c, pListing, pID)
        With mDataset.Tables
            .Add(myDT)
            '.Add(myDT_Payroll_Detail)
        End With

        'Dim pdc As DataColumn
        'Dim cdc As DataColumn
        'Dim rel As DataRelation

        'pdc = myDT.Columns(Database.Tables.tPayroll.Field.ID)
        'cdc = myDT_Payroll_Detail.Columns(Database.Tables.tPayroll_Detail.Field.ID_Payroll)
        'rel = mDataset.Relations.Add(pdc, cdc)

        'NOTE: CUSTOMIZED
        Me.ReloadAfterCommit = True


        'mHideZero = MyBase.AddButton("Hide Zero Amount", gMainForm.imgList.Images("HideZeroAmt.png"), AddressOf HideZeroButton)
        
        InitControl(pMenu)
        AfterNew()


        mHideZero = MyBase.GetStripButton("Hide Zero Amount")
        AddHandler mHideZero.Click, AddressOf HideZeroButton
        mHideZero.CheckOnClick = True

        mComputePayroll = MyBase.GetStripButton("Compute Payroll")
        'AddHandler mComputePayroll.Click, AddressOf ComputePayrollButton


        myDT_Payroll_Detail = Me.mDataset.Tables("tPayroll_Detail")
        With Me.GetDataGridView(myDT_Payroll_Detail)
            For Each dgvc As DataGridViewColumn In .Columns
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable
            Next
        End With

    End Sub

    Private Sub HideZeroButton(ByVal sender As Object, ByVal e As EventArgs)
        If mHideZero.Checked Then
            myDT_Payroll_Detail.DefaultView.RowFilter = "Total <> 0"
        Else
            myDT_Payroll_Detail.DefaultView.RowFilter = ""
        End If
    End Sub
    'Private Sub ComputePayrollButton(ByVal sender As Object, ByVal e As EventArgs)

    'End Sub
    Public Overrides Sub LoadInfo(ByVal pID As Integer)
        'myDT_Payroll_Detail.ClearThenFill("ID_Payroll=" & pID.ToString)
        MyBase.LoadInfo(pID)
        Dim CheckIfPosted As DataTable = GSCOM.SQL.TableQuery("SELECT * from tUserPayrollPeriod where ID_User = " & nDB.GetUserID & " and ID_PayrollPeriod = " & myDT.Get(Database.Tables.tPayroll.Field.ID_PayrollPeriod).ToString, Connection)

        If CheckIfPosted.Rows.Count <> 0 Then
            Me.SaveButton.Enabled = False
            ' Me.GetStripButton("Compute Hours").Enabled = False
            ' Me.GetStripButton("Compute Summary").Enabled = False
        End If
    End Sub

    'Protected Overrides Sub SetDefaultValues()
    '    Dim vID As Integer
    '    vID = CInt(myDT.Get(Database.Tables.tPayroll.Field.ID))
    '    myDT_Payroll_Detail.Columns(Database.Tables.tPayroll_Detail.Field.ID_Payroll).DefaultValue = vID
    'End Sub

#Region "Overrides"
    Protected Overrides Property Table() As GSCOM.SQL.ZDataTable
        Get
            Return myDT
        End Get
        Set(ByVal value As GSCOM.SQL.ZDataTable)
            myDT = CType(value, Database.Tables.tPayroll)
        End Set
    End Property


#End Region

    Private Sub PayrollInfo_Saved(ByVal sender As Object, ByVal e As InfoSet.SavedEventArgs) Handles Me.Saved
        Dim s As String
        s = myDT.Get(Database.Tables.tPayroll.Field.ID_PayrollPeriod).ToString
        s &= ", " & myDT.Get(Database.Tables.tPayroll.Field.ID).ToString
        s = myDT.Get(Database.Tables.tPayroll.Field.ID).ToString
        s &= ", " & nDB.GetUserID.ToString

        GSCOM.SQL.ExecuteNonQuery("EXEC pPayroll_EditPerEmp " & s, e.Transaction)
    End Sub

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