
Friend Class DataSelectorWithList
    Inherits TreeGrid
    Implements GSCOM.Interfaces.IDataListGrid

#Region "IDataList Interface"
#Region "Declarations"
    Public Event ItemActivate(ByVal sender As Object, ByVal e As Interfaces.ItemActivateEventArgs) Implements Interfaces.IDataListGrid.ItemActivate
    Public Shadows Event KeyDown As KeyEventHandler Implements Interfaces.IDataListGrid.KeyDown
    Public Event RefreshedColors(ByVal sender As Object, ByVal e As System.EventArgs) Implements Interfaces.IDataListGrid.RefreshedColors
    Public Property SchemaTable As DataTable Implements Interfaces.IDataListGrid.SchemaTable
#End Region

    Friend Overloads Property DataSource() As DataTable Implements Interfaces.IDataListGrid.DataSource
        Get
            Return MyBase.ListSource
        End Get
        Set(ByVal value As DataTable)
            MyBase.ListSource = CType(value, DataTable)
            LoadTree()
            If DataSource IsNot Nothing Then
                AddHandler MyBase.ListSource.DefaultView.ListChanged, AddressOf ListChanged
            End If
        End Set
    End Property

    Public Property EvenBackColor() As System.Drawing.Color Implements Interfaces.IDataListGrid.EvenBackColor
        Get
            Return Color.White
        End Get
        Set(ByVal value As System.Drawing.Color)

        End Set
    End Property

    Public Shadows Property GridColor() As System.Drawing.Color Implements Interfaces.IDataListGrid.GridColor
        Get
            Return MyBase.GridColor
        End Get
        Set(ByVal value As System.Drawing.Color)
            MyBase.GridColor = value
        End Set
    End Property

    Public ReadOnly Property HasSortedColumn() As Boolean Implements Interfaces.IDataListGrid.HasSortedColumn
        Get
            Return False
        End Get
    End Property

    Public Overloads Property Name() As String Implements Interfaces.IDataListGrid.Name
        Get
            Return MyBase.Name
        End Get
        Set(ByVal value As String)
            MyBase.Name = value
        End Set
    End Property

    Public Property OddBackColor() As System.Drawing.Color Implements Interfaces.IDataListGrid.OddBackColor
        Get
            Return Color.Gainsboro
        End Get
        Set(ByVal value As System.Drawing.Color)

        End Set
    End Property

    Public ReadOnly Property SortedColumnName() As String Implements Interfaces.IDataListGrid.SortedColumnName
        Get
            Return Nothing
        End Get
    End Property

    Public ReadOnly Property SortOrder() As GSCOM.Common.SortOrder Implements Interfaces.IDataListGrid.SortOrder
        Get
            Return GSCOM.Common.SortOrder.Ascending
        End Get
    End Property

    Public ReadOnly Property SelectedRowID As Integer Implements Interfaces.IDataListGrid.SelectedRowID
        Get
            Return 0
        End Get
    End Property

    Public Sub ClearColumns() Implements Interfaces.IDataListGrid.ClearColumns

    End Sub

#End Region

    Private Sub ListChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ListChangedEventArgs)
        RetainIDs()
        LoadTree()
    End Sub

    Friend Overloads Property TrueDataSource() As DataTable
        Get
            Return MyBase.DataSource
        End Get
        Set(ByVal value As DataTable)
            MyBase.DataSource = value
        End Set
    End Property

    Friend Overloads Sub LoadTree()
        RetainChecked()
        If Me.ListSource IsNot Nothing Then
           For Each dr2 As DataRowView In Me.ListSource.DefaultView
                 Dim dv As New DataView(Me.TrueDataSource)
                dv.RowFilter = Me.GetRowFilterString(dr2.Row)
                 If dv.Count = 0 Then
                    GoCopyValuesCore(dr2.Row)
                End If
            Next
        End If
        Me.Populate()
    End Sub

End Class
