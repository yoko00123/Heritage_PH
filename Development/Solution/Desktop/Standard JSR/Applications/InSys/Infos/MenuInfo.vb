Option Explicit On
Option Strict On



'Friend Class MenuInfo
'Inherits InfoSet

'    Private WithEvents myDT As New Database.Tables.tMenu(Connection)
'    Dim mtMenu As GSCOM.SQL.ZDataTable

'    Private mControl As New InSys.DataControl 'Private mControl As New nDB.JournalVoucherControl
'    Dim WithEvents mGrid As DataGridView
'    Private ID_Reference As Integer

'    Public Sub New(ByVal pMenu As Integer, ByVal c As SqlClient.SqlConnection, ByVal pListing As DataTable, ByVal pID As Integer)
'        MyBase.New(c, pListing, pID)
'        With mDataset.Tables
'            .Add(myDT)
'        End With

'        InitControl(pMenu)

'        mtMenu = DirectCast(Me.mDataset.Tables("tMenu"), GSCOM.SQL.ZDataTable)

'        mGrid = Me.GetDataGridView(mtMenu)

'        Dim a As ToolStripButton = Me.GetStripButton("Send Email Blast")
'        AddHandler a.Click, AddressOf SendEmailBlast

'        AfterNew()

'    End Sub

'    Public Sub SendEmailBlast(ByVal sender As System.Object, ByVal e As System.EventArgs)
'        If MsgBox("Send email blast?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
'            Dim s() As String = Nothing
'            Dim ss(0) As String
'            Dim x As Integer = 0
'            Dim y As Integer = 0
'            For Each dr As DataRow In mtEmailBlast_Detail.Select
'                If dr("EmailAddress").ToString <> "" Then
'                    If dr("EmailAddress").ToString.Contains(" ") Then
'                        ReDim Preserve ss(y)
'                        ss(y) = dr("ClientContactPerson").ToString
'                        y += 1
'                    Else
'                        ReDim Preserve s(x)
'                        s(x) = dr("EmailAddress").ToString
'                    End If
'                End If
'                x += 1
'            Next
'            MsgBox("The following people are not added due to wrong email address:" & vbCrLf & Strings.Join(ss, vbCrLf))
'            Dim sss As String
'            sss = Strings.Join(s, ",")
'            ShowEmailClient(sss)
'        End If
'    End Sub

'    Protected Sub ShowEmailClient(ByVal Email As String)
'        Try
'            Dim s As String
'            s = My.Computer.Registry.GetValue("HKEY_CLASSES_ROOT\mailto\shell\open\command", "", "test").ToString
'            s = Strings.Replace(s, "%1", "mailto:" & Email)
'            Shell(s, AppWinStyle.MinimizedFocus, , 15)
'        Catch ex As Exception
'            MsgBox(ex.Message, MsgBoxStyle.Information)
'        End Try
'    End Sub
'    Public Overrides Sub LoadInfo(ByVal pID As Integer)
'        MyBase.LoadInfo(pID)
'        mGrid.AllowUserToAddRows = False
'    End Sub

'    Protected Overrides Property Table() As GSCOM.SQL.ZDataTable
'        Get
'            Return myDT
'        End Get
'        Set(ByVal value As GSCOM.SQL.ZDataTable)
'            myDT = CType(value, Database.Tables.tEmailBlast)

'        End Set
'    End Property


'#Region "Customized"
'    Function IsNull(ByVal Input As Object, ByVal NullOutput As Object) As Object
'        Return IIf(IsDBNull(Input), NullOutput, Input)
'    End Function
'#End Region

'End Class
