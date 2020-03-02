Option Explicit On
Option Strict On



Namespace Accounting

    Friend Class PayrollPeriodInfo
        Inherits InfoSet

#Region "Declarations"
        Private myDT As New Database.Tables.tPayrollPeriod(Connection)
        Private mControl As New Control
        Private mGrid As GSDetailDataGridView
        Private mGenerateFile As ToolStripButton
#End Region

#Region "Constructors"
        Public Sub New(ByVal c As SqlClient.SqlConnection, ByVal pListing As DataTable, ByVal pID As Integer)
            MyBase.New(c, pListing, pID)
            With mDataset.Tables
                .Add(myDT)
            End With
            myDT.Columns(Database.Tables.tPayrollPeriod.Field.ID_PayrollPeriodType).DefaultValue = 1 'BASIC PAY
            Me.ReloadAfterCommit = True
            Me.ClearThenFillOnLoadInfo = False 'ROBBIE 20070319
            mGrid = Me.AddGrid("File")
            With mGrid
                .AutoGenerateColumns = True
                .ReadOnly = True
                .AllowUserToAddRows = False
                .AllowUserToDeleteRows = False
            End With
            Me.HideNewAndSaveButtons()
            mGenerateFile = Me.AddButton("Generate File", gImageList.Images("misc.a.ico"), AddressOf GenerateFile)
            AfterNew()
        End Sub

#End Region

        Private Sub GenerateFile(ByVal sender As Object, ByVal e As EventArgs)
            Dim vFileName As String
            Dim vEmployeeName As String = ""
            Dim vDepartment As String = ""
            Dim sa() As String = Nothing
            Dim sfd As New SaveFileDialog
            Dim s As String = ""
            With sfd
                .FileName = Format(CDate(myDT.Get(Database.Tables.tPayrollPeriod.Field.PayDate)), "yyyyMMdd")
                .Filter = "PRN Files (*.prn)|*.prn|All files (*.*)|*.*"
            End With
            If sfd.ShowDialog = Windows.Forms.DialogResult.OK Then
                vFileName = sfd.FileName  'GSCOM.Common.AddPathSeparator(ofd.SelectedPath) & "pc_tranTemplate.xls" 'set the filename
                For Each drx As DataRow In CType(mGrid.DataSource, DataTable).Select
                    s &= drx.Item("Text").ToString & vbCrLf
                Next
                s = s.Trim
                IO.File.AppendAllText(sfd.FileName, s)
                MsgBox("Done", MsgBoxStyle.Information)
            End If
        End Sub

#Region "LoadInfo"
        Public Overrides Sub LoadInfo(ByVal pID As Integer)
            MyBase.LoadInfo(pID)
            Dim s As String
            s = "SELECT * from fPayrollGLEntry (" & pID.ToString & ")"
            mGrid.DataSource = GSCOM.SQL.TableQuery(s, Connection)
            mGenerateFile.Enabled = pID > 0
        End Sub

#End Region

#Region "Overrides"
        Protected Overrides Property Table() As GSCOM.SQL.ZDataTable
            Get
                Return myDT
            End Get
            Set(ByVal value As GSCOM.SQL.ZDataTable)
                myDT = CType(value, Database.Tables.tPayrollPeriod)
            End Set
        End Property

        'Protected Overrides Property Control() As System.Windows.Forms.Control
        '    Get
        '        Return mControl
        '    End Get
        '    Set(ByVal value As System.Windows.Forms.Control)
        '        'mControl = CType(value, nDB.PayrollPeriodControl)
        '        mControl = CType(value, Control)
        '    End Set
        'End Property

#End Region

    End Class
End Namespace