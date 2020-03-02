Option Explicit On
Option Strict On

Friend Class DataListTabPage
    Inherits GSCOM.UI.GSTab.GSTabPage 'System.Windows.Forms.TabPage
    Implements GSCOM.Interfaces.ZIDataTabPageList

#Region " Windows Form Designer generated code "

    Public Sub New(ByVal tableName As String, ByVal c As Data.SqlClient.SqlConnection, ByVal pList As DataListControl)
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()

        Me.mMainList = pList
        Me.mMainList.BackColor = System.Drawing.SystemColors.Control
        Me.mMainList.Caption = ""
        Me.mMainList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.mMainList.Location = New System.Drawing.Point(0, 0)
        Me.mMainList.Name = "MainList"
        Me.mMainList.Size = New System.Drawing.Size(500, 375)

        Me.mMainList.TabIndex = 0
        Me.Controls.Add(Me.mMainList)

        mMainList.Init(tableName, c, Me.Name)
        'Add any initialization after the InitializeComponent() call

        If MainList.GetType Is GetType(ZReportViewer) Then
            Me.AddShortCut(Keys.R, AddressOf DirectCast(MainList, ZReportViewer).RequeryButton.PerformClick)
        End If

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub


    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.SuspendLayout()
        '
        'MainList
        '

        '
        'ZReportTabPage
        '
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.Size = New System.Drawing.Size(632, 453)
        Me.Text = "ListForm"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private WithEvents mMainList As DataListControl


    Public Event ItemActivate(ByVal sender As Object, ByVal e As System.EventArgs)

    'Public ReadOnly Property TableName() As String Implements GSCOM.Interfaces.ZIDataTabPageList.TableName
    '    Get
    '        TableName = mMainList.TableName
    '    End Get
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


    'Public Overrides Property Dock() As System.Windows.Forms.DockStyle Implements GSCOM.Interfaces.ZIDataTabPageList.Dock
    '    Get
    '        Dock = MyBase.Dock()
    '    End Get
    '    Set(ByVal Value As System.Windows.Forms.DockStyle)
    '        MyBase.Dock = Value
    '    End Set
    'End Property

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


    Public ReadOnly Property MainList() As GSCOM.Interfaces.ZIDataList Implements GSCOM.Interfaces.ZIDataTabPageList.MainList
        Get
            MainList = mMainList
        End Get
    End Property

    Public Event ButtonClick(ByVal sender As Object, ByVal e As GSCOM.Interfaces.ZIDataList.ButtonClickEventArgs) Implements GSCOM.Interfaces.ZIDataTabPageList.ButtonClick

    Private Sub mMainList_ButtonClick(ByVal sender As Object, ByVal e As Interfaces.ZIDataList.ButtonClickEventArgs) Handles mMainList.ButtonClick
        RaiseEvent ButtonClick(Me, e)
    End Sub


End Class






