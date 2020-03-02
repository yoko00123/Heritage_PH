Option Explicit On
Option Strict On


Friend Class EmployeeWorkApprovalInfo
    Inherits InfoSet

    'Private mtEmployeeDailyScheduleDetail As New Database.Tables.tEmployeeDailySchedule_Detail(Connection)
    Private myDT As New Database.Tables.tEmployeeDailySchedule_Detail(Connection)
    'Private mtEmployeeDailyScheduleDetail As New Database.Tables.tEmployeeDailySchedule_Detail(Connection)
    Private mControl As New InSys.DataControl
    Private mComputeHeader As ToolStripButton

    Public Sub New(ByVal pMenu As Integer, ByVal c As SqlClient.SqlConnection, ByVal pListing As DataTable, ByVal pID As Integer)
        MyBase.New(c, pListing, pID)
        With mDataset.Tables
            .Add(Table)
            '.Add(mtEmployeeDailyScheduleDetail)
        End With

        InitControl(pMenu)

        '      Dim pdc As DataColumn
        '     Dim cdc As DataColumn
        '    Dim rel As DataRelation
        '   pdc = myDT.Columns(Database.Tables.tEmployeeDailySchedule.Field.ID)

        '  cdc = Me.mtEmployeeDailyScheduleDetail.Columns(Database.Tables.tEmployeeDailySchedule_Detail.Field.ID_EmployeeDailySchedule)
        ' rel = mDataset.Relations.Add(pdc, cdc)

        'Me.ReloadAfterCommit = True
        mComputeHeader = Me.GetStripButton("Compute Header") 'MyBase.AddButton("Compute Header", gMainForm.imgList.Images("_overtime.png"), AddressOf ComputeHeader)
        AddHandler mComputeHeader.Click, AddressOf ComputeHeader
        AfterNew()
    End Sub

    Public Sub ComputeHeader(ByVal sender As Object, ByVal e As EventArgs)
        Dim EDSID As Integer
        Dim strSql As String



        If MsgBox("Compute Header?", CType(MsgBoxStyle.Question + MsgBoxStyle.YesNo, MsgBoxStyle)) = MsgBoxResult.Yes Then
            strSql = "SELECT eds.ID FROM tEmployeeDailySchedule eds INNER JOIN tEmployeeDailySchedule_Detail edsd ON eds.ID = edsd.ID_EmployeeDailySchedule WHERE edsd.ID = " & GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tEmployeeDailySchedule_Detail.Field.ID))
            EDSID = CInt(GSCOM.SQL.ExecuteScalar(strSql, gConnection))
            If EDSID > 0 Then
                GSCOM.SQL.ExecuteNonQuery("EXEC pEmployeeDailySchedule_ComputeHeader " & EDSID.ToString, Connection)
                Application.DoEvents()
                MsgBox("Finished computing header.", MsgBoxStyle.Information)
            Else
                MsgBox("No schedule yet", MsgBoxStyle.Information)

            End If
        End If
    End Sub



#Region "Overrides"
    Protected Overrides Property Table() As GSCOM.SQL.ZDataTable
        Get
            Return myDT
        End Get
        Set(ByVal value As GSCOM.SQL.ZDataTable)
            myDT = CType(value, Database.Tables.tEmployeeDailySchedule_Detail)
        End Set
    End Property

    'Protected Overrides Property Control() As System.Windows.Forms.Control
    '    Get
    '        Return mControl
    '    End Get
    '    Set(ByVal value As System.Windows.Forms.Control)
    '        mControl = CType(value, InSys.DataControl)
    '    End Set
    'End Property

#End Region

End Class
