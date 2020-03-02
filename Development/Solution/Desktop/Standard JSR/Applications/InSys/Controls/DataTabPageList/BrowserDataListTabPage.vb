Option Explicit On
Option Strict On



Public Class BrowserDataListTabPage
    Inherits GSCOM.UI.GSTab.GSTabPage 'System.Windows.Forms.TabPage
    Implements GSCOM.Interfaces.ZIDataTabPageList

#Region " Windows Form Designer generated code "

    Friend Sub New(ByVal pDataSource As String, ByVal pConnection As SqlClient.SqlConnection, ByVal pFixedFilter As String, ByVal pColumnTable As DataTable, ByVal pSort As String, ByVal pMenu As Database.Menu)
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'Add any initialization after the InitializeComponent() call
        mMainList.Init(pDataSource, pConnection, pFixedFilter, pColumnTable, "", pSort, pMenu)

        Me.AddShortCut(Keys.N, AddressOf mMainList.NewButton.PerformClick)
        Me.AddShortCut(Keys.O, AddressOf mMainList.OpenInfoButton.PerformClick)
        Me.AddShortCut(Keys.R, AddressOf mMainList.RequeryButton.PerformClick)
        ' Me.AddShortCut(Keys.X, AddressOf mMainList.CloseButton.PerformClick)
    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                'ROBBIE TEMP
                MainList.DataSource = Nothing
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    Private WithEvents mMainList As BrowserDataList

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.mMainList = New BrowserDataList
        Me.SuspendLayout()
        '
        'mMainList
        '
        Me.mMainList.BackColor = System.Drawing.SystemColors.Control
        Me.mMainList.Text = ""
        Me.mMainList.DataSource = Nothing
        Me.mMainList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.mMainList.Location = New System.Drawing.Point(0, 0)
        Me.mMainList.Name = "mMainList"
        Me.mMainList.ReportPath = Nothing
        Me.mMainList.Size = New System.Drawing.Size(632, 453)
        Me.mMainList.TabIndex = 0
        '
        'AdvancedTabPage
        '
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.Controls.Add(Me.mMainList)
        Me.Size = New System.Drawing.Size(632, 453)
        Me.Text = "ListForm"
        Me.UseVisualStyleBackColor = True
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Event ItemActivate(ByVal sender As Object, ByVal e As System.EventArgs)

    'Public Overrides Property Dock() As System.Windows.Forms.DockStyle Implements GSCOM.Interfaces.ZIDataTabPageList.Dock
    '    Get
    '        Dock = MyBase.Dock()
    '    End Get
    '    Set(ByVal Value As System.Windows.Forms.DockStyle)
    '        MyBase.Dock = Value
    '    End Set
    'End Property

    Private mRow As DataRow
    Public Property Row() As DataRow Implements Interfaces.ZIDataTabPageList.Row
        Get
            Return mRow
        End Get
        Set(ByVal value As DataRow)
            mRow = value
        End Set
    End Property


    Public Shadows Property ImageIndex() As Integer Implements GSCOM.Interfaces.ZIDataTabPageList.ImageIndex
        Get
            ImageIndex = MyBase.ImageIndex
        End Get
        Set(ByVal Value As Integer)
            MyBase.ImageIndex = Value
        End Set
    End Property

    Public Overrides Property Text() As String Implements GSCOM.Interfaces.ZIDataTabPageList.Text
        Get
            Text = MyBase.Text
        End Get
        Set(ByVal Value As String)
            MyBase.Text = Value
        End Set
    End Property

    Friend ReadOnly Property MainList() As GSCOM.Interfaces.ZIDataList Implements GSCOM.Interfaces.ZIDataTabPageList.MainList
        Get
            MainList = mMainList
        End Get
    End Property

    'Private Sub MainList_ItemActivate(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    RaiseEvent ItemActivate(sender, e)
    'End Sub

    Friend Event ButtonClick(ByVal sender As Object, ByVal e As GSCOM.Interfaces.ZIDataList.ButtonClickEventArgs) Implements GSCOM.Interfaces.ZIDataTabPageList.ButtonClick

    Private Sub mMainList_ButtonClick(ByVal sender As Object, ByVal e As Interfaces.ZIDataList.ButtonClickEventArgs) Handles mMainList.ButtonClick
        
        Select Case e.ButtonText
            Case "Print"
                CType(mMainList.MainView, WebBrowser).ShowPrintPreviewDialog()
            Case Else
                RaiseEvent ButtonClick(Me, e)
        End Select
    End Sub

End Class

