Option Explicit On
Option Strict On



Friend Class LeaveInfo
    Inherits InfoSet
    Private myDT As New Database.Tables.tLeave(Connection)
    'Private mtLeave_Detail As New Database.Tables.tLeave_Detail(Connection)
    'Private mtEmployeeLeaveCredit As New Database.Tables.tEmployeeLeaveCredit(Connection)
    Private mControl As New InSys.DataControl
    Dim msd, med As TextBox

    'Private mFileButton As ToolStripButton
    'Private mApproveButton As ToolStripButton
    'Private mRejectButton As ToolStripButton
    Private dgv As GSDetailDataGridView
    Public Sub New(ByVal pMenu As Integer, ByVal c As SqlClient.SqlConnection, ByVal pListing As DataTable, ByVal pID As Integer)
        MyBase.New(c, pListing, pID)
        With mDataset.Tables
            .Add(Table)
            '.Add(mtLeave_Detail)
            '.Add(mtEmployeeLeaveCredit)
        End With
        InitControl(pMenu)
        'Dim pdc As DataColumn
        'Dim cdc As DataColumn
        'Dim rel As DataRelation
        'pdc = myDT.Columns(Database.Tables.tLeave.Field.ID)
        'cdc = mtLeave_Detail.Columns(Database.Tables.tLeave_Detail.Field.ID_Leave)
        'rel = mDataset.Relations.Add(pdc, cdc)

        myDT.Columns(Database.Tables.tLeave.Field.ID_FilingStatus).DefaultValue = 1
        'mFileButton = Me.AddButton("Reset Status", gMainForm.imgList.Images("misc.a.ico"), AddressOf mFileButton_Click)
        'mApproveButton = Me.AddButton("Approve", gMainForm.imgList.Images("misc.a.ico"), AddressOf mApproveButton_Click)
        'mRejectButton = Me.AddButton("Reject", gMainForm.imgList.Images("misc.a.ico"), AddressOf mRejectButton_Click)
        Me.ReloadAfterCommit = True

        dgv = Me.AddGrid("Leave Credit")
        With dgv
            .AutoGenerateColumns = True
            .ReadOnly = True
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
        End With

        AfterNew()

        msd = CType(Me.GetControl("_StartDate"), TextBox)
        med = CType(Me.GetControl("_EndDate"), TextBox)
        AddHandler msd.Validated, AddressOf msd_Validated
    End Sub
    Private mID As Integer
    Private Sub msd_Validated(ByVal sender As Object, ByVal e As System.EventArgs)
        If med.Text = "" Then
            med.Text = msd.Text
        End If
    End Sub

    Public Overrides Sub LoadInfo(ByVal pID As Integer)
        MyBase.LoadInfo(pID)
        Dim b As Boolean
        b = (pID <> 0)
        dgv.DataSource = GSCOM.SQL.TableQuery("SELECT ID,LeavePayrollItem, Alloted, Used, Balance FROM vzEmployeeLeaveCredit WHERE ID_Employee=" & GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tLeave.Field.ID_Employee)), gConnection)
    End Sub

    Private Sub mFileButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        UpdateStatus(1)
    End Sub

    Private Sub mApproveButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        'If GSCOM.SQL.TableQuery("SELECT * FROM temployeeleavecredit elc inner join tleavefile_detail lfd on lfd.Name = elc.Name where id = " & GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tLeave.Field.ID_Employee))) Then
        UpdateStatus(2)
    End Sub

    Private Sub mRejectButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        UpdateStatus(3)
    End Sub

    Private Sub UpdateStatus(ByVal pStatus As Integer)
        'Dim s As String
        'Dim vID As String
        'vID = GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tLeave.Field.ID))
        's = "EXEC pLeave_UpdateLeaveCredit " & vID & "," & pStatus.ToString
        'GSCOM.SQL.ExecuteNonQuery(s, gConnection)
    End Sub

    Protected Overrides Function CanSave() As Boolean
        Dim o As Object
        Dim s As String
        s = "SELECT DATEDIFF(year,e.StartDate,getdate()) FROM tEmployee e WHERE e.ID=" & GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tLeave.Field.ID_Employee))
        o = GSCOM.SQL.ExecuteScalar(s, gConnection)
        If IsDBNull(o) OrElse CDec(o) < 1 Then
            MsgBox("The employee is not yet one year in service.", MsgBoxStyle.Information)
        End If

        Dim d As String

        d = GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tLeave.Field.ID))

        If d = "0" Then
            d = "NULL"
        Else
            d = d
        End If

        Dim sq, sd, ed, emp, lpi, fs As String
        Dim bq As Boolean

        sd = GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tLeave.Field.StartDate))
        ed = GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tLeave.Field.EndDate))
        emp = GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tLeave.Field.ID_Employee))
        lpi = GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tLeave.Field.ID_LeavePayrollItem))
        fs = GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tLeave.Field.ID_FilingStatus))

        sq = "SELECT dbo.fCheckLeave(" & d & "," & sd & "," & ed & "," & emp & "," & lpi & "," & fs & ")"

        Try
            bq = CType(GSCOM.SQL.ExecuteScalar(sq, gConnection), Boolean)
        Catch ex As Exception
            bq = False
        End Try
        ' If mID = 0 Then
        If Not bq Then
            MsgBox("Duplicate Leave Filing")
        Else
            Return MyBase.CanSave()
        End If
        ' End If


    End Function
#Region "Overrides"
    Protected Overrides Property Table() As GSCOM.SQL.ZDataTable
        Get
            Return myDT
        End Get
        Set(ByVal value As GSCOM.SQL.ZDataTable)
            myDT = CType(value, Database.Tables.tLeave)
        End Set
    End Property

#End Region

    Private Sub LeaveInfo_Saved(ByVal sender As Object, ByVal e As InfoSet.SavedEventArgs) Handles Me.Saved
        GSCOM.SQL.ExecuteNonQuery("EXEC pLeave " & GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tLeave.Field.ID)), e.Transaction)
    End Sub

End Class


'Private Sub UpdateStatus()
'    If Not (mApproveButton.Checked Or mRejectButton.Checked) Then
'        myDT.Set(Database.Tables.tLeave.Field.ID_FilingStatus, 1)
'    ElseIf mApproveButton.Checked Then
'        myDT.Set(Database.Tables.tLeave.Field.ID_FilingStatus, 2)
'    Else
'        myDT.Set(Database.Tables.tLeave.Field.ID_FilingStatus, 3)
'    End If
'End Sub

'Private Sub mApproveButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)
'    mApproveButton.Checked = Not mApproveButton.Checked
'    If mApproveButton.Checked Then
'        mRejectButton.Checked = False
'    End If
'    UpdateStatus()
'End Sub

'Private Sub mRejectButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)
'    mRejectButton.Checked = Not mRejectButton.Checked
'    If mRejectButton.Checked Then
'        mApproveButton.Checked = False
'    End If
'    UpdateStatus()
'End Sub