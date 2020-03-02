Option Explicit On
Option Strict Off



Friend Class AlterInfo
    Inherits InfoSet

    Private WithEvents myDT As New Database.Tables.tAlert(Connection)
    Dim mtAlert As GSCOM.SQL.ZDataTable

    Private mControl As New InSys.DataControl 'Private mControl As New nDB.JournalVoucherControl
    Dim WithEvents mGrid As DataGridView
    Private ID_Reference As Integer

    Private WithEvents EmployeeMoveMentTab As System.Windows.Forms.TabControl
    Private ID_EmployeeMovement As Integer

    Public Sub New(ByVal pMenu As Integer, ByVal c As SqlClient.SqlConnection, ByVal pListing As DataTable, ByVal pID As Integer)
        MyBase.New(c, pListing, pID)
        With mDataset.Tables
            .Add(myDT)
        End With

        InitControl(pMenu)

        mtAlert = DirectCast(Me.mDataset.Tables("tAlert"), GSCOM.SQL.ZDataTable)

        mGrid = Me.GetDataGridView(mtAlert)
        'EmployeeMoveMentTab = CType(Me.GetTabPage("Employee Movement").Parent, System.Windows.Forms.TabControl)
        Dim a As ToolStripButton = Me.GetStripButton("Open Message")
        'AddHandler a.Click, AddressOf OpenMessage

        AfterNew()

    End Sub

    Public Sub OpenMessage(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim aInfoset As InfoSet
        aInfoset = GetInfoSet(CType(Me.Row("ID_Menu"), Database.Menu))
        If aInfoset Is Nothing Then
            aInfoset = New ZInfo(CType(Me.Row("ID_Menu"), Database.Menu), gConnection, Nothing, CInt(Me.Row("ID_Original")))
            AddInfoSet(aInfoset, CType(Me.Row("ID_Menu"), Database.Menu))
            aInfoset.LoadInfo(CInt(Me.Row("ID_Original")))
        Else
            aInfoset.LoadInfo(CInt(Me.Row("ID_Original")))
        End If

        aInfoset.ShowDialog()
        GSCOM.SQL.ExecuteNonQuery("UPDATE tAlert SET IsRead = 1 WHERE ID = " & Me.RowID, gConnection)
    End Sub

    Public Overrides Sub LoadInfo(ByVal pID As Integer)
        MyBase.LoadInfo(pID)

        'If IsDBNull(myDT.Get("ID_EmployeeMovement")) Then
        '    If EmployeeMoveMentTab.TabPages(0).Text = "Employee Movement" Then
        '        EmployeeMoveMentTab.TabPages(0).Dispos()
        '    End If
        'Else
        '    EmployeeMoveMentTab.TabPages(0).Show()
        'End If


    End Sub

    Protected Overrides Property Table() As GSCOM.SQL.ZDataTable
        Get
            Return myDT
        End Get
        Set(ByVal value As GSCOM.SQL.ZDataTable)
            myDT = CType(value, Database.Tables.tAlert)

        End Set
    End Property


#Region "Customized"
    Function IsNull(ByVal Input As Object, ByVal NullOutput As Object) As Object
        Return IIf(IsDBNull(Input), NullOutput, Input)
    End Function
#End Region

    Private Function MainTab() As Object
        Throw New NotImplementedException
    End Function

End Class
