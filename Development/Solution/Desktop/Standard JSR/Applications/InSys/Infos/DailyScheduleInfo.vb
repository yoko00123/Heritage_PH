Option Explicit On
Option Strict On



Friend Class DailyScheduleInfo
    Inherits InfoSet

    Private myDT As New Database.Tables.tDailySchedule(Connection)
    Private mtDailySchedule_Detail As DataTable
    Private mControl As New InSys.DataControl
    Dim WithEvents mDetail As DataGridView
    Dim WithEvents mgrid As DataGridView


    Public Sub New(ByVal pMenu As Integer, ByVal c As SqlClient.SqlConnection, ByVal pListing As DataTable, ByVal pID As Integer)
        MyBase.New(c, pListing, pID)
        With mDataset.Tables
            .Add(myDT)
        End With

        InitControl(pMenu)


        'NOTE: CUSTOMIZED
        Me.ReloadAfterCommit = True
        mtDailySchedule_Detail = Me.mDataset.Tables("tDailySchedule_Detail")
        mtDailySchedule_Detail.DefaultView.Sort = Database.Tables.tDailySchedule_Detail.Field.StartMinute.ToString

        AfterNew()

        mgrid = Me.GetDataGridView(mtDailySchedule_Detail)
        With mgrid
            For Each dgvc As DataGridViewColumn In .Columns
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable
            Next
        End With
        AddHandler mgrid.CellFormatting, AddressOf mgrid_CellFormatting
    
    End Sub


    Protected Overrides Function CanSave() As Boolean
        If mtDailySchedule_Detail.Rows.Count = 0 Then
            MsgBox("Please input at least one detail", MsgBoxStyle.Exclamation)
            Return False
        Else
            Return MyBase.CanSave()
        End If
    End Function

    Protected Overrides Property Table() As GSCOM.SQL.ZDataTable
        Get
            Return myDT
        End Get
        Set(ByVal value As GSCOM.SQL.ZDataTable)
            myDT = CType(value, Database.Tables.tDailySchedule)
        End Set
    End Property



    Private Sub DailyScheduleInfo_Saved(ByVal sender As Object, ByVal e As InfoSet.SavedEventArgs) Handles Me.Saved
        Dim s As String
        s = "pDailySchedule_Update " & GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tDailySchedule.Field.ID))
        GSCOM.SQL.ExecuteNonQuery(s, e.Transaction)
    End Sub


    Private Sub mgrid_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles mgrid.CellFormatting
        Dim dgv As DataGridView
        Dim drv As DataRowView
        Dim o As Object
        dgv = TryCast(sender, DataGridView)
        If dgv IsNot Nothing Then
            drv = TryCast(dgv.Rows(e.RowIndex).DataBoundItem, DataRowView)
            If drv IsNot Nothing Then
                o = drv("ID_HourType")
                If o IsNot DBNull.Value Then
                    Select Case CInt(o)
                        Case 1, 3, 5
                        Case Else
                            e.CellStyle.ForeColor = Color.Gray
                    End Select
                End If
            End If
        End If
    End Sub

    Private Sub mgrid_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles mgrid.EditingControlShowing
        Dim drv As DataRowView
        drv = TryCast(mgrid.CurrentRow.DataBoundItem, DataRowView)
        If drv IsNot Nothing Then
            Dim s As String
            s = "IsActive = 1"
            Dim d As GSCOM.UI.DataLookUp.LookUpEditingControl
            d = TryCast(e.Control, GSCOM.UI.DataLookUp.LookUpEditingControl)
            If d IsNot Nothing Then
                d.Worker.FixedFilter = s
            End If
        End If
    End Sub
End Class
