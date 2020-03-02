Option Explicit On
Option Strict On



Friend Class PurchaseReturnInfo
    '    Inherits InfoSet
    '    Private WithEvents myDT As New Database.Tables.tPurchaseReturn(Connection)
    '    'Private mtInventoryOB_Detail As New Database.Tables.tInventoryOB_Detail(Connection)
    '    Dim mtPurchaseReturn_Detail As GSCOM.SQL.ZDataTable
    '    Private mGenerateButton As ToolStripButton
    '    Private mControl As New InSys.DataControl 'Private mControl As New nDB.JournalVoucherControl
    '    Dim WithEvents mGrid As DataGridView
    '    Private ID_Reference As Integer


    '    Public Sub New(ByVal pMenu As Integer, ByVal c As SqlClient.SqlConnection, ByVal pListing As DataTable, ByVal pID As Integer)
    '        MyBase.New(c, pListing, pID)
    '        With mDataset.Tables
    '            .Add(myDT)
    '        End With

    '        InitControl(pMenu)

    '        mtPurchaseReturn_Detail = DirectCast(Me.mDataset.Tables("tPurchaseReturn_Detail"), GSCOM.SQL.ZDataTable)


    '        mGrid = Me.GetDataGridView(mtPurchaseReturn_Detail)

    '        AddHandler mtPurchaseReturn_Detail.ColumnChanged, AddressOf PRtDetail_ColumnChanged
    '        AddHandler mGrid.EditingControlShowing, AddressOf mGrid_EditingControlShowing

    '        Dim a As ToolStripButton = Me.GetStripButton("Browse Receiving Report")
    '        AddHandler a.Click, AddressOf BrowseISS

    '        AfterNew()
    '    End Sub

    '    Protected Overrides Property Table() As GSCOM.SQL.ZDataTable
    '        Get
    '            Return myDT
    '        End Get
    '        Set(ByVal value As GSCOM.SQL.ZDataTable)
    '            myDT = CType(value, Database.Tables.tPurchaseReturn)
    '        End Set
    '    End Property

    '#Region "Total"


    '    Private Sub PRtDetail_ColumnChanged(ByVal sender As Object, ByVal e As System.Data.DataColumnChangeEventArgs)
    '        'Dim u As Decimal
    '        'Dim q As Integer

    '        'u = CDec(IIf(IsDBNull(e.Row.Item(Database.Tables.tPurchaseOrder_Detail.Field.UnitCost.ToString)), 0, e.Row.Item(Database.Tables.tPurchaseOrder_Detail.Field.UnitCost.ToString)))
    '        'q = CInt(IIf(IsDBNull(e.Row.Item(Database.Tables.tPurchaseOrder_Detail.Field.Qty.ToString)), 0, e.Row.Item(Database.Tables.tPurchaseOrder_Detail.Field.Qty.ToString)))

    '        'Select Case e.Column.ColumnName.ToLower
    '        '    Case Database.Tables.tPurchaseOrder_Detail.Field.Qty.ToString.ToLower
    '        '        'e.Row.Item(Database.Tables.tInventoryOB_Detail.Field.LineTotal.ToString) = CDec(e.Row(Database.Tables.tInventoryOB_Detail.Field.Qty.ToString)) * CDec(e.Row(Database.Tables.tInventoryOB_Detail.Field.UnitCost.ToString))
    '        '        e.Row.Item(Database.Tables.tPurchaseOrder_Detail.Field.LineTotal.ToString) = q * u

    '        '        If mGrid.CurrentCell IsNot Nothing Then
    '        '            mGrid.UpdateCellValue(mGrid.Columns("LineTotal").Index, mGrid.CurrentCell.RowIndex)
    '        '        End If
    '        '        ComputeTotal()

    '        '    Case Database.Tables.tPurchaseOrder_Detail.Field.UnitCost.ToString.ToLower
    '        '        e.Row.Item(Database.Tables.tPurchaseOrder_Detail.Field.LineTotal.ToString) = q * u
    '        '        If mGrid.CurrentCell IsNot Nothing Then
    '        '            mGrid.UpdateCellValue(mGrid.Columns("LineTotal").Index, mGrid.CurrentCell.RowIndex)
    '        '        End If
    '        '        ComputeTotal()

    '        'End Select
    '    End Sub

    '    Private Sub mGrid_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles mGrid.EditingControlShowing

    '        Dim drv As DataRowView
    '        Dim dgv As DataGridView
    '        dgv = CType(sender, DataGridView)
    '        drv = TryCast(dgv.CurrentRow.DataBoundItem, DataRowView)
    '        Dim d As GSCOM.UI.DataLookUp.LookUpEditingControl
    '        d = TryCast(e.Control, GSCOM.UI.DataLookUp.LookUpEditingControl)
    '        'IMPORTANT.... GRID USES SAME EDITING CONTROL SO MAKE SURE THAT THE FIXEDFILTER PROPERTY IS RESET EVERYTIME
    '        If d IsNot Nothing Then
    '            Dim s As String = ""
    '            Select Case d.ColumnName.ToLower
    '                Case "ID_WarehouseArea".ToLower
    '                    'If drv IsNot Nothing Then
    '                    s = "ID_Warehouse =" & CInt(myDT.Rows(0).Item("ID_Warehouse"))
    '                    'Else
    '                    's = "1=0"
    '                    'End If
    '                Case "ID_WarehouseAreaRack".ToLower
    '                    'If drv IsNot Nothing Then
    '                    '    s = "ID_Warehouse =" & CInt(myDT.Rows(0).Item("ID_Warehouse"))
    '                    'Else
    '                    '    s = "1=0"
    '                    'End If
    '            End Select
    '            d.Worker.FixedFilter = s
    '        End If
    '    End Sub

    '    Private Sub ComputeTotal()
    '        Application.DoEvents()
    '        Dim o As Object
    '        Dim d As Object

    '        o = Me.mtPurchaseReturn_Detail.Compute("SUM(LineTotal)", "")
    '        If o Is DBNull.Value Then o = 0
    '        myDT.Set(Database.Tables.tPurchaseReturn.Field.TotalCost, o)

    '        d = myDT.Rows(0).Item("DiscountAmount")
    '        If d Is DBNull.Value Then d = 0
    '        myDT.Rows(0).Item("NetAmount") = CDec(o) - CDec(d)

    '    End Sub

    '    Private Sub myDT_ColumnChanged(ByVal sender As Object, ByVal e As System.Data.DataColumnChangeEventArgs) Handles myDT.ColumnChanged
    '        'Dim d As Object
    '        'Dim v As Object
    '        'Dim n As Object

    '        'Select Case e.Column.ColumnName.ToLower
    '        '    Case Database.Tables.tPurchaseOrder.Field.DiscountPercent.ToString.ToLower
    '        '        If e.Row("TotalCost".ToString) IsNot DBNull.Value Then
    '        '            d = CDec(e.Row.Item("DiscountPercent").ToString) * CDec(e.Row.Item("TotalCost").ToString)
    '        '            myDT.Set(Database.Tables.tPurchaseOrder.Field.DiscountAmount, d)
    '        '            n = CDec(e.Row("TotalCost".ToString)) - CDec(e.Row("DiscountAmount".ToString))
    '        '            myDT.Set(Database.Tables.tPurchaseOrder.Field.NetAmount, n)
    '        '        Else
    '        '            myDT.Set(Database.Tables.tPurchaseOrder.Field.DiscountAmount, 0)
    '        '            myDT.Set(Database.Tables.tPurchaseOrder.Field.NetAmount, 0)
    '        '        End If

    '        '    Case Database.Tables.tPurchaseOrder.Field.VatPercent.ToString.ToLower

    '        '        If e.Row("VatPercent".ToString) IsNot DBNull.Value Then
    '        '            v = CDec(e.Row.Item("VatPercent").ToString) * CDec(e.Row.Item("TotalCost").ToString)
    '        '            myDT.Set(Database.Tables.tPurchaseOrder.Field.VatAmount, v)
    '        '        Else
    '        '            myDT.Set(Database.Tables.tPurchaseOrder.Field.VatAmount, 0)
    '        '        End If


    '        'End Select
    '    End Sub
    '#End Region

    '#Region "AddItem"


    '    Private Sub BrowseISS(ByVal sender As Object, ByVal e As EventArgs)

    '        If myDT.Rows(0).Item("ID_Supplier") IsNot DBNull.Value Then
    '            Dim f As New DataLookUpForm(Database.Menu.ACCOUNTPAYABLE_ReceivingReport)
    '            f.LookUp.Worker.FixedFilter = "ID_filingStatus = 2"
    '            If f.ShowDialog() = DialogResult.OK Then
    '                myDT.Set(Database.Tables.tPurchaseReturn.Field.ID_ReceivingReport, f.Row("ID"))
    '                myDT.Rows(0).Item("RRNo") = f.Row("DocumentNo")
    '                UpdateIRdetail()
    '            End If
    '        Else
    '            MsgBox("Select Supplier", MsgBoxStyle.Exclamation)

    '        End If

    '    End Sub

    '    Private Sub UpdateIRdetail()
    '        Dim s As String
    '        s = "SELECT * FROM vReceivingReport_Detail WHERE ID_ReceivingReport = " & CInt(myDT.Rows(0).Item("ID_receivingreport"))

    '        Dim dt As DataTable
    '        For Each dr As DataRow In Me.mtPurchaseReturn_Detail.Select
    '            dr.Delete()
    '        Next

    '        dt = GSCOM.SQL.TableQuery(s, New SqlClient.SqlConnection(gConnection.ConnectionString))
    '        For Each dr As DataRow In dt.Select
    '            Dim drx As DataRow
    '            drx = mtPurchaseReturn_Detail.NewRow
    '            drx(Database.Tables.tPurchaseReturn_Detail.Field.ID_Item) = dr(Database.Tables.tReceivingReport_Detail.Field.ID_Item)
    '            drx("ID_WarehouseArea") = dr("ID_WarehouseArea")
    '            drx("ID_Item") = dr("ID_Item")
    '            drx("ItemCode") = dr("ItemCode")
    '            drx("Item") = dr("Item")
    '            drx("UOM") = dr("UOM")
    '            drx("QTY") = dr("QTY")
    '            drx("Linetotal") = dr("Linetotal")
    '            mtPurchaseReturn_Detail.Rows.Add(drx)
    '        Next
    '    End Sub



    '#End Region




    '#Region "Customized"
    '    'Private Sub Generate(ByVal sender As Object, ByVal e As EventArgs)
    '    '    If MsgBox("Generate?", CType(MsgBoxStyle.Question + MsgBoxStyle.YesNo, MsgBoxStyle)) = MsgBoxResult.Yes Then
    '    '        BeginProcess("Generating Journal Voucher details... Please wait.")
    '    '        GSCOM.SQL.ExecuteNonQuery("EXEC pJournalVoucher " & GSCOM.SQL.SQLFormat(CInt(myDT.Get(Database.Tables.tJournalVoucher.Field.ID).ToString)), Connection)
    '    '        LoadInfo(CInt(myDT.Get(Database.Tables.tJournalVoucher.Field.ID)))
    '    '        Application.DoEvents()
    '    '        EndProcess("")
    '    '        MsgBox("Finish generating Journal Voucher.", MsgBoxStyle.Information)
    '    '    End If
    '    'End Sub
    '#End Region
End Class

