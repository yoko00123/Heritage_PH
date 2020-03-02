Option Explicit On
Option Strict On



Friend Class JournalVoucherInfo
    Inherits InfoSet

    Private myDT As New Database.Tables.tJournalVoucher(Connection)
    'Private mtJournalVoucher_Detail As New Database.Tables.tJournalVoucher_Detail(Connection)
    Private mtJournalVoucher_Detail As GSCOM.SQL.ZDataTable
    Private mGenerateButton As ToolStripButton
    Private WithEvents mGrid As DataGridView

    Private mControl As New InSys.DataControl 'Private mControl As New nDB.JournalVoucherControl

    Public Sub New(ByVal pMenu As Integer, ByVal c As SqlClient.SqlConnection, ByVal pListing As DataTable, ByVal pID As Integer)
        MyBase.New(c, pListing, pID)
        With mDataset.Tables
            .Add(myDT)
        End With

        InitControl(pMenu)
        mGrid = Me.GetDataGridView(mtJournalVoucher_Detail)
        ' mGenerateButton = Me.GetStripButton("Generate")
        ' AddHandler mtJournalVoucher_Detail.ColumnChanged, AddressOf JVDet_columnchanged
        'AddHandler Me.GetStripButton("Generate Report").Click, AddressOf GenerateReport
        'AddHandler Me.GetStripButton("Generate").Click, AddressOf Generate

        'Me.ReloadAfterCommit = True
        'mGenerateButton = MyBase.AddButton("Generate", gMainForm.imgList.Images("misc.a.ico"), AddressOf Generate)
        AfterNew()
    End Sub

    Protected Overrides Property Table() As GSCOM.SQL.ZDataTable
        Get
            Return myDT
        End Get
        Set(ByVal value As GSCOM.SQL.ZDataTable)
            myDT = CType(value, Database.Tables.tJournalVoucher)
        End Set
    End Property




#Region "Customized"
    Private Sub Generate(ByVal sender As Object, ByVal e As EventArgs)
        If MsgBox("Generate?", CType(MsgBoxStyle.Question + MsgBoxStyle.YesNo, MsgBoxStyle)) = MsgBoxResult.Yes Then
            BeginProcess("Generating Journal Voucher details... Please wait.")
            GSCOM.SQL.ExecuteNonQuery("EXEC pJournalVoucher " & GSCOM.SQL.SQLFormat(CInt(myDT.Get(Database.Tables.tJournalVoucher.Field.ID).ToString)) & "," & nDB.GetUserID.ToString, Connection)
            LoadInfo(CInt(myDT.Get(Database.Tables.tJournalVoucher.Field.ID)))
            Application.DoEvents()
            EndProcess("")
            MsgBox("Finish generating Journal Voucher.", MsgBoxStyle.Information)
        End If
    End Sub
#End Region
    Public Overrides Sub ButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
        'MyBase.ButtonClick(sender, e)
    End Sub
    Private Sub GenerateReport(ByVal sender As Object, ByVal e As EventArgs)
        'SPLASH
        If MsgBox("Generate Report", MsgBoxStyle.YesNo Or MsgBoxStyle.Question) = MsgBoxResult.Yes Then
            Me.SetStatusLabel("Generating Journal Voucher report.")
            Dim TemplatePath As String = nDB.GetSetting(Database.SettingEnum.ExcelTemplatePath) & "ExcelTemplate_JV01.xls"
            Dim Dt As DataTable = GSCOM.SQL.TableQuery("Select * From fzJournalVoucher2(" & GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tJournalVoucher.Field.ID)) & ") Where ID_JournalType = 1", Connection)
            Dim Dt2 As DataTable = GSCOM.SQL.TableQuery("Select * From fzJournalVoucher2(" & GSCOM.SQL.SQLFormat(myDT.Get(Database.Tables.tJournalVoucher.Field.ID)) & ") Where ID_JournalType = 2", Connection)
            Dim Ds As New DataSet
            Dim Ds2 As New DataSet

            Ds.Tables.Add(Dt.Copy)
            Ds2.Tables.Add(Dt2.Copy)


            Dim TmpPath1 As String
            TmpPath1 = My.Computer.FileSystem.SpecialDirectories.Temp & Now.ToBinary & "1"
            InteropExcelReport.Methods_Excel.CreateExcelDocument_V3(TemplatePath, New String() {}, New Object() {}, Ds, False, False, TmpPath1)
            'InteropExcelReport.Methods_Excel.CreateExcelDocument_V3(TemplatePath, ParamName.ToArray, ParamValue.ToArray, Ds, False, False, TmpPath1)

            Dim TmpPath2 As String
            TmpPath2 = My.Computer.FileSystem.SpecialDirectories.Temp & Now.ToBinary & "2"
            InteropExcelReport.Methods_Excel.CreateExcelDocument_V3(TemplatePath, New String() {}, New Object() {}, Ds2, False, False, TmpPath2)

            Dim List_File As New Generic.List(Of String)
            List_File.Add(TmpPath1)
            List_File.Add(TmpPath2)

            InteropExcelReport.Methods_Excel.CreateExcelDocument_Sheets(List_File.ToArray)
            Me.SetStatusLabel("Finish Generating Report.")
        End If
    End Sub
End Class

