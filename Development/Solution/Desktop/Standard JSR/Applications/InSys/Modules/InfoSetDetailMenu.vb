Option Explicit On
Option Strict On



Friend Class InfoSetDetailMenu
    Public mPage As BrowserDataListTabPage
    Protected mList As BrowserDataList
    Public Property InfoSet As InfoSet

    Private Property FixedFilter As String
        Get
            Return mPage.MainList.FixedFilter
        End Get
        Set(ByVal value As String)
            mPage.MainList.FixedFilter = value
        End Set
    End Property

    Friend Property Enabled As Boolean
        Get
            Return mList.Enabled
        End Get
        Set(ByVal value As Boolean)
            mList.Enabled = value
        End Set
    End Property

    Protected ReadOnly Property mID As String
        Get
            Return Me.InfoSet.Row(Me.ParentColumnName).ToString
        End Get
    End Property

    Protected mChildColumnName As String

    Friend Property ParentColumnName As String



    Protected Friend Sub Go()
        Dim s As String
        'mID = pID
        If IsNumeric(mID) Then
            s = mChildColumnName & "=" & mID
        Else
            s = mChildColumnName & "=" & GSCOM.SQL.SQLFormat(mID)
        End If
        Me.FixedFilter = s
        mList.FillDatabase(True, False)
    End Sub

    Private mMenu As Database.Menu
    Private mMenuDetailTabRow As Database.MenuDetailTabRow

    Sub New(ByVal pInfoSet As InfoSet, ByVal pMenuDetailTabRow As Database.MenuDetailTabRow, ByVal pMenu As Database.Menu, ByVal pParentColumnName As String, ByVal pChildColumnName As String)
        Me.InfoSet = pInfoSet
        Me.mMenu = pMenu
        Me.mMenuDetailTabRow = pMenuDetailTabRow
        Me.ParentColumnName = pParentColumnName
        Me.mChildColumnName = pChildColumnName

        Dim mr As New Database.MenuRow
        Dim dra() As DataRow = nDB.MenuTable.Select("ID=" & CInt(mMenu).ToString)
        If dra.Length > 0 Then
            mr.InnerRow = dra(0)
            Dim vDisplayColumnsTable As DataTable = nDB.GetDisplayColumnsTable(mMenu) 'vSettings.GetSetting(UI.DataList.Settings.KeyEnum.SelectString, "* ")
            mPage = New BrowserDataListTabPage(mr.DataSource, gConnection, "1=0", vDisplayColumnsTable, mr.Sort, mMenu)
            mList = CType(mPage.MainList, BrowserDataList)
            mList.CloseButton.Enabled = False

            InitDataListBase(mPage, mr)
            InitZIDataTabPageList(mPage, mr)

            AddHandler mPage.ButtonClick, AddressOf Me.ButtonClick

        Else
            ' MsgBox("New InfoSetDetailMenu", MsgBoxStyle.Exclamation)
        End If

    End Sub

    Protected Overridable Sub ButtonClick(ByVal sender As Object, ByVal e As GSCOM.Interfaces.ZIDataList.ButtonClickEventArgs)
  
        Dim dt As DataTable = CType(mPage.MainList.DataSource, DataTable)
        Select Case e.ButtonText
            Case "New"
                Dim pInfoSet As InfoSet
                pInfoSet = GetInfoSet(mMenu)
                'IMPORTANT: SAVE THE ID OF THE RECORD THAT 
                'TRIGGERED THE LOADING OF INFO BECAUSE mID IS 
                'RESET TO 0 ON LOADINFO() FUNCTION IF SAME MODULE
                Dim vCallingRecordID As Integer = CInt(mID) 'mID is string for GUID only.. e.g. FileBrowser
                If pInfoSet Is Nothing Then
                    pInfoSet = NewInfo(mMenu, dt, 0)
                Else
                    pInfoSet.LoadInfo(0) 'mID IS RESET TO 0 IF SAME MODULE
                End If
                Dim dr As DataRow = pInfoSet.mDataset.Tables(0).Rows(0)
                dr.Item(Me.mChildColumnName) = vCallingRecordID 'INITIALIZE THE COLUMN. SET TO PARENT

                Dim dv As New DataView(Me.InfoSet.mInfoMenuSet.tMenuDetailTabField)
                dv.RowFilter = "ID_MenuDetailTab=" & Me.mMenuDetailTabRow.ID
                dv.RowFilter &= " AND ListColumn IS NOT NULL"
                Dim vReadOnly As Boolean
                For Each drv As DataRowView In dv
                    vReadOnly = dr.Table.Columns(drv("Name").ToString).ReadOnly
                    If vReadOnly Then
                        dr.Table.Columns(drv("Name").ToString).ReadOnly = False
                    End If
                    dr(drv("Name").ToString) = Me.InfoSet.Row(drv("ListColumn").ToString)
                    If vReadOnly Then
                        dr.Table.Columns(drv("Name").ToString).ReadOnly = True
                    End If

                Next


                Application.DoEvents()
                If pInfoSet IsNot Nothing Then
                    pInfoSet.ShowDialog()
                    Me.Go()
                End If
            Case Else
                MainModule.SelectStandardMenu(mPage, dt, e, mPage)
        End Select

    End Sub



End Class
